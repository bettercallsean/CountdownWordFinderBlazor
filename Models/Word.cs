namespace CountdownWordFinderBlazor.Models
{
    public class Word
    {
        public int Length { get; private set; }
        public string WordString { get; private set; }
        public string Definition { get; private set; }

        public Word(string word)
        {
            WordString = word;
            Length = word.Length;
        }

        public void SetDefinition(WordDefinition wd)
        {
            Definition = wd.meanings[0].definitions[0].definition;
        }

    }
}
