using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using Assets.Scripts.Localization;
using System.IO;

public class LocalizationMenuItems : MonoBehaviour {

    /** Allows the developer to export all messages file in a CSV for translation.
     */ 
    [MenuItem("Tools/Localization/Export Messages")]
	private static void ExportMessages()
    {
        string messagesFolder = EditorUtility.OpenFolderPanel("Select the folder containing Messages files", "", "");

        string filePath = EditorUtility.SaveFilePanel("Select where to save the Exported CSV", Application.dataPath, "for_translation", "csv");

        print("Messages folder: " + messagesFolder);
        print("File Path Specified: " + filePath);

        string csv = generateCSVText(messagesFolder);

        File.WriteAllText(filePath, csv);
    }

    /** Allows the developer to import a translated CSV file previously generated
     * 
     *      - will create new variables if they do not already exist
     *      - will overright existing text for variables
     */ 
    [MenuItem("Tools/Localization/Import Translated Messages")]
    private static void ImportTranslations()
    {
        // TODO
    }

    /** Create the string that can be written to a file to form the csv
     */ 
    private static string generateCSVText(string messagesFolderPath)
    {
        var variableNames = getAllVariables(messagesFolderPath); // all variables accross all messages files
        var messagesFiles = getMessagesFiles(messagesFolderPath); // all messages files without preceding path

        List<CSVLine> csvContent = new List<CSVLine>();

        var languages = messagesFiles.Select(m => { return new Lang(m.Split('.').Last()); }).ToList();

        // initialize each line of the csv file to ensure columns are correct
        variableNames.ToList().ForEach(v => 
        {
            csvContent.Add( new CSVLine(v, languages) );
        });

        messagesFiles.ToList().ForEach( messageFile => 
            {
                string line;
                Lang language = new Lang(messageFile.Split('.').Last());

                System.IO.StreamReader file = new System.IO.StreamReader(messagesFolderPath + "/" + messageFile);

                while ((line = file.ReadLine()) != null)
                {
                    string[] split = line.Split('=');
                    string varName = split.First<String>();
                    string content = string.Join("", split.Skip(1).ToArray()); // concatenate the tail
                    LangAndValue langAndVal = new LangAndValue(language, content);

                    csvContent.ForEach( c => 
                        {
                            if (c.variableName == varName)
                                c.addContent(langAndVal);
                        } );
                }
            });

        var langHeadings = csvContent.First().contentByLang.Keys.ToList().Select(k => { return k.code; });
        string headings = "VARIABLE NAME," + string.Join(",", langHeadings.ToArray());

        return headings + "\n" + string.Join( "\n", csvContent.Select(c => { return c.ToString(); }).ToArray() );
    }

    /** Fetch the list of distinct variables accross all messages files given the folder
     * 
     */ 
    private static IEnumerable<string> getAllVariables(string messagesFolderPath)
    {
        var messagesFiles = getMessagesFiles(messagesFolderPath);

        // read each file and grab all the variables - concatenate them all
        var variableNames = messagesFiles.SelectMany( messageFileName => 
            {
                string line;
                List<string> variables = new List<string>();

                System.IO.StreamReader file = new System.IO.StreamReader(messagesFolderPath + "/" + messageFileName);

                while ((line = file.ReadLine()) != null)
                {
                    String[] split = line.Split('=');
                    String varName = split.First<String>();

                    variables.Add(varName);
                }

                return variables;
            });

        return variableNames.Distinct();
    }

    private static IEnumerable<string> getMessagesFiles(string messagesFolderPath)
    {
        String[] fileNamesWithPath = System.IO.Directory.GetFiles(messagesFolderPath);

        print( "All files in current folder: " + string.Join("\n", fileNamesWithPath) );

        // remove the path
        var fileNames = fileNamesWithPath.Select(f =>
        {
            return f.Split('\\').Last<String>();
        });

        // filters files that are meta or not messages
        var messagesFiles = fileNames.Where(f =>
        {
            var split = f.Split('.');

            return split.First() == "messages" && split.Last() != "meta";
        });

        print("Found the following messages files: " + string.Join("\n", messagesFiles.ToArray()) );

        return messagesFiles;
    }

    private class CSVLine
    {
        public string variableName;
        private Dictionary<Lang, string> contentByLanguage = new Dictionary<Lang, String>();

        public SortedDictionary<Lang, string>contentByLang
        {
            get { return new SortedDictionary<Lang, string>(contentByLanguage); }
        }

        public CSVLine(string variable, List<Lang> languages)
        {
            variableName = variable;
            languages.ForEach(l => { contentByLanguage.Add(l, ""); });
        }

        public void addContent(LangAndValue content)
        {
            contentByLanguage = 
                contentByLanguage.Select(c => 
                    {
                        KeyValuePair<Lang, string> result;

                        if (c.Key == content.language)
                            result = new KeyValuePair<Lang, string>(content.language, content.content);
                        else
                            result = c;

                        return result;
                    }).ToDictionary( k => k.Key, k => k.Value );
        }

        public override string ToString()
        {
            return variableName + "," + String.Join(",", contentByLanguage.Select(c => { return c.Value; }).ToArray());
        }
    }

    private class LangAndValue : IComparable<LangAndValue>
    {
        public Lang language;
        public string content;

        public LangAndValue(Lang lang, string cont)
        {
            language = lang;
            content = cont;
        }

        int IComparable<LangAndValue>.CompareTo(LangAndValue other)
        {
            return language.code.CompareTo( other.language.code ); 
        }
    }
}
