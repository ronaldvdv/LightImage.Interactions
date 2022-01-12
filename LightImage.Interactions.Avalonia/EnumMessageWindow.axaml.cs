using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LightImage.Interactions.Enums;
using System.Linq;

namespace LightImage.Interactions
{
    public partial class EnumMessageWindow : InteractionWindow<EnumViewModel, EnumMemberViewModel>
    {
        private EnumViewModel _input;
        private bool? _result;

        public EnumMessageWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        /// <inheritdoc/>
        protected override EnumMemberViewModel GetOutput()
        {
            var result = _input.Members.FirstOrDefault(m => m.IsSelected);
            return _result == true ? result : null;
        }

        /// <inheritdoc/>
        protected override void SetInput(EnumViewModel input)
        {
            Title = input.Title;
            TextLabel.Text = input.Message;
            RadioItems.Items = input.Members;
            _input = input;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _result = false;
            Close(false);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _result = true;
            Close(true);
        }
    }
}
