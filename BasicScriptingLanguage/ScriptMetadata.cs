using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScriptingLanguage
{
    class ScriptMetadata
    {
        public static string SCRIPT_TITLE;

        public static string RetrieveMetadata(METADATATYPE whatToRetrieve)
        {
            switch(whatToRetrieve)
            {
                case(METADATATYPE.SCRIPT_TITLE):
                    return SCRIPT_TITLE;
            }
            return null;
        }

        public static void SetMetadata(string value, METADATATYPE whatToSet)
        {
            switch(whatToSet)
            {
                case(METADATATYPE.SCRIPT_TITLE):
                    SCRIPT_TITLE = value;
                    break;
            }

        }
    }

    public enum METADATATYPE
    {
        SCRIPT_TITLE, AUTHOR, CONTACT_EMAIL
    }
}
