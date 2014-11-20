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
    [System.Serializable]
    public class Lang : IComparable<Lang>
    {
        public string language;
        public string code;
        
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

        public static bool operator ==(Lang l1, Lang l2)
        {
            return l1.code.ToLower() == l2.code.ToLower();
        }

        public static bool operator !=(Lang l1, Lang l2)
        {
            return l1.code.ToLower() != l2.code.ToLower();
        }

        int IComparable<Lang>.CompareTo(Lang other)
        {
            return code.CompareTo(other.code);
        }
    }
}
