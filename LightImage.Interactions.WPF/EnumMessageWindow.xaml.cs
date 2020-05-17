using System.Linq;
using LightImage.Interactions.Enums;

namespace LightImage.Interactions
{
    /// <summary>
    /// Interaction logic for EnumMessageWindow.xaml.
    /// </summary>
    public partial class EnumMessageWindow : EnumMessageWindowBase
    {
        private EnumViewModel _input;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumMessageWindow"/> class.
        /// </summary>
        public EnumMessageWindow()
        {
            InitializeComponent();
        }

        /// <inheritdoc/>
        protected override EnumMemberViewModel GetOutput()
        {
            var result = _input.Members.FirstOrDefault(m => m.IsSelected);
            return DialogResult == true ? result : null;
        }

        /// <inheritdoc/>
        protected override void SetInput(EnumViewModel input)
        {
            Title = input.Title;
            TextLabel.Text = input.Message;
            RadioItems.ItemsSource = input.Members;
            _input = input;
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }

#pragma warning disable SA1402 // File may only contain a single type

    /// <summary>
    /// Base class for the <see cref="EnumMessageWindow"/>.
    /// </summary>
    public abstract class EnumMessageWindowBase : InteractionWindow<EnumViewModel, EnumMemberViewModel>
    {
    }

#pragma warning restore SA1402 // File may only contain a single type
}