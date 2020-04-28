namespace LightImage.Interactions.Files
{
    public class OpenFileInput : FileInput<OpenFileOutput>
    {
        public bool MultiSelect { get; set; } = false;
    }
}