using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Localization
{
    /** Will retreive text based on the currently specified language.
     *      
     */
    public class Messages
    {
        public Lang defaultLang = new Lang("en");
        public Lang currentLang;

        // where are the messages files stored?
        private string path;
        private string defaultPath;

        public Messages(Lang lang, string pathToMessagesFiles)
        {
            currentLang = lang;
            defaultPath = pathToMessagesFiles + "/messages." + defaultLang.code;
            path = pathToMessagesFiles + "/messages." + lang.code;
        }

        /** Retrives a variable from the messages file of the current language.
         * 
         *      - if an entry for the currently requested language cannot be found
         *      the default will be provided.
         *      
         *      - if no variable is found in either the requested or default language
         *      the variable name will be returned
         */
        public string get(string messageVariableName)
        {
            String messageCurrentLang = findVariable(messageVariableName, path);
            String messageDefaultLang = findVariable(messageVariableName, defaultPath); // why can't I make this Lazy<String>?

            // we will return the message variable name if we cannot find it in either file
            String result = messageVariableName;

            if ( messageCurrentLang != null )
            {
                result = messageCurrentLang;
            }
            else if( messageDefaultLang != null )
            {
                result = messageDefaultLang;
            }

            return result;
        }

        /* Fetch a message from the given file path.
         * 
         *      - ** WARNING: RETURNS NULL IF NOTHING IS FOUND! **
         *      
         *      - not excited about returning null but I don't know a C# construct
         *      around this (See Scala's Option[T] or Haskell's Maybe[T]) and an
         *      exception is a bit heavy for this private method
         * 
         */
        private String findVariable(String variableName, String filePath)
        {
            string line;
            string result = null;

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            
            while ((line = file.ReadLine()) != null)
            {
                String[] split = line.Split('=');
                String varName = split.First<String>();

                if (varName == variableName)
                {
                    var content = split.Skip<String>(1).ToArray<String>();
                    result = String.Join("", content);
                    break;
                }
            }

            return result;
        }

        /** Determine the languages currently supported based on the files present in
         *  the specified messages folder.
         */
        public List<Lang> supportedLanguages()
        {
            List<String> fileNames = new List<String>( System.IO.Directory.GetFiles(path) );

            IEnumerable<String> langCodes = fileNames.Select( f => 
                {
                    String fileName = f.Split('/').Last<String>();
                    String langCode = fileName.Split('.').Last<String>();

                    return langCode;
                });

            return langCodes.Select( l => { return new Lang(l); } ).ToList<Lang>();
        }
    }
}
