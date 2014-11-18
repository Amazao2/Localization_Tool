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
        public Lang defaultLang = new Lang("English", "en");
        public Lang currentLange;

        // where are the messages files stored?
        private string path;

        public Messages(Lang lang, string pathToMessagesFiles)
        {
            currentLange = lang;
            path = pathToMessagesFiles;
        }

        /** Retrives a variable from the messages file of the current language.
         * 
         *      - if an entry for the currently requested language cannot be found
         *      the default will be provided.
         */
        public string get(string messageVariableName)
        {
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(Application.dataPath);
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }

            return "";
        }

        public List<Lang> supportedLanguages()
        {
            List<String> fileNames = new List<String>( System.IO.Directory.GetFiles(path) );

            var messages = fileNames.Select(f => 
                { 
                    var split = f.Split('/');
                    return split.Last<string>(); 
                });
        }
    }
}
