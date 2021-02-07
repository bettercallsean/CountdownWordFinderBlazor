using System;
using System.Collections.Generic;

namespace CountdownWordFinderBlazor
{
    // Generated using http://jsonutils.com/, what a lovely utility
    // This class replicates the structure of the data returned by https://dictionaryapi.dev/

    public class Phonetic
    {
        public string text { get; set; }
        public string audio { get; set; }
    }

    public class Definition
    {
        public string definition { get; set; }
        public string example { get; set; }
        public IList<string> synonyms { get; set; }
    }

    public class Meaning
    {
        public string partOfSpeech { get; set; }
        public IList<Definition> definitions { get; set; }
    }

    public class WordDefinition
    {
        public string word { get; set; }
        public IList<Phonetic> phonetics { get; set; }
        public IList<Meaning> meanings { get; set; }
    }

}
