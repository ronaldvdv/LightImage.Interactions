namespace LightImage.Interactions.Files
{
    public abstract class FileInput<TOutput> : IInteractionInput<TOutput>
    {
        public string DefaultExtension { get; set; }
        public string Filter { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
    }
}