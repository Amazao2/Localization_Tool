using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Localization
{
    /** Languages are represneted by a string name and their code.
     * 
     *      eg. "English", "en"
     *      
     *      The important part of this class is the Language Code, the 
     *      text representation of the language is just here for
     *      convenience.
     */
    public class Lang
    {
        public string language;
        public string code;

        public string lang
        {
            get { return language; }
        }

        
        public Lang(String languageCode, String languageName)
        {
            language = languageName;
            code = languageCode;
        }

        public Lang( String languageCode )
        {
            language = languageCode;
            code = languageCode;
        }
    }
}
