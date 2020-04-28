using LightImage.Interactions.Enums;
using System.Linq;

namespace LightImage.Interactions
{
    /// <summary>
    /// Interaction logic for EnumMessageWindow.xaml
    /// </summary>
    public partial class EnumMessageWindow : EnumMessageWindowBase
    {
        private EnumViewModel _input;

        public EnumMessageWindow()
        {
            InitializeComponent();
        }

        protected override EnumMemberViewModel GetOutput()
        {
            var result = _input.Members.FirstOrDefault(m => m.IsSelected);
            return DialogResult == true ? result : null;
        }

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

    public abstract class EnumMessageWindowBase : InteractionWindow<EnumViewModel, EnumMemberViewModel>
    {
    }
}