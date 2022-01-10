using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using LightImage.Interactions.Messages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LightImage.Interactions.Samples
{
    public class AppViewModel : ReactiveObject
    {
        private readonly IInteractionService _interactions;

        public AppViewModel(IInteractionService interactions)
        {
            _interactions = interactions;

            Info = ReactiveCommand.CreateFromTask(InfoImpl);
            YesNo = ReactiveCommand.CreateFromTask(YesNoImpl);
            File = ReactiveCommand.CreateFromTask(FileImpl);
            Folder = ReactiveCommand.CreateFromTask(FolderImpl);
            Enum = ReactiveCommand.CreateFromTask(EnumImpl);
            Number = ReactiveCommand.CreateFromTask(NumberImpl);
            NumberRange = ReactiveCommand.CreateFromTask(NumberRangeImpl);
            String = ReactiveCommand.CreateFromTask(StringImpl);
        }

        public enum EnumOptions
        {
            [Display(Name = "Copy")]
            [Description("Create a duplicate")]
            Copy,

            [Display(Name = "Move")]
            [Description("Move to another position")]
            Move,

            [Display(Name = "Swap")]
            [Description("Exchange position with another element")]
            Swap
        }

        public ReactiveCommand<Unit, Unit> Enum { get; }
        public ReactiveCommand<Unit, Unit> File { get; }
        public ReactiveCommand<Unit, Unit> Folder { get; }
        public ReactiveCommand<Unit, Unit> Info { get; }
        public ReactiveCommand<Unit, Unit> Number { get; }
        public ReactiveCommand<Unit, Unit> NumberRange { get; }
        public ReactiveCommand<Unit, Unit> String { get; }
        public ReactiveCommand<Unit, Unit> YesNo { get; }

        private async Task EnumImpl()
        {
            var option = await _interactions.Show<EnumOptions>("Enum window", "What behaviour would you like best?");
        }

        private async Task FileImpl()
        {
            var result = await _interactions.OpenFile();
        }

        private async Task FolderImpl()
        {
            var result = await _interactions.SelectFolder();
        }

        private async Task InfoImpl()
        {
            await _interactions.ShowMessage("Info window", "Hey, here's some useful info for you!", MessageIcons.Information, MessageButton.Ok);
        }

        private async Task NumberImpl()
        {
            var result = await _interactions.Input("Pick a number", "Enter any number:", 0);
        }

        private async Task NumberRangeImpl()
        {
            var result = await _interactions.Input("Pick a number", "Enter a number between 3 and 7 (inclusive):", 0, predicate: i => i >= 3 && i <= 7);
        }

        private async Task StringImpl()
        {
            var result = await _interactions.Prompt("Prompt window", "Enter some text", string.Empty);
        }

        private async Task YesNoImpl()
        {
            await _interactions.YesNo("Info window", "Do you like this dialog?", MessageIcons.Information);
        }
    }
}