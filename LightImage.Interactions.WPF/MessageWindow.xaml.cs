using LightImage.Interactions.Messages;
using LightImage.Interactions.Prompts;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LightImage.Interactions
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window,
        IInteractionHandler<MessageOptions, MessageResult>,
        IInteractionHandler<PromptOptions, PromptResult>
    {
        private readonly IReadOnlyDictionary<MessageIcons, PackIconMaterialKind> _iconMap = new Dictionary<MessageIcons, PackIconMaterialKind>
        {
            { MessageIcons.Edit, PackIconMaterialKind.Pencil },
            { MessageIcons.Information, PackIconMaterialKind.Information },
            { MessageIcons.Question, PackIconMaterialKind.CommentQuestion },
            { MessageIcons.Warning, PackIconMaterialKind.AlertCircleOutline },
            { MessageIcons.Error, PackIconMaterialKind.AlertOctagon },
        };

        private Predicate<string> _accept;

        public MessageWindow()
        {
            InitializeComponent();
        }

        public MessageButton Button { get; private set; } = default;

        public async Task<MessageResult> Handle(MessageOptions request, CancellationToken cancellationToken)
        {
            SetInput(request, null, false, string.Empty);
            await this.ShowDialogAsync();
            return new MessageResult(Button);
        }

        public async Task<PromptResult> Handle(PromptOptions request, CancellationToken cancellationToken)
        {
            SetInput(request, request.Predicate, true, request.Value);
            var ok = true == await this.ShowDialogAsync();
            return new PromptResult(ok ? TheTextBox.Text : null, Button);
        }

        private void HandleButton(MessageButton button)
        {
            Button = button;
            DialogResult = button == MessageButton.Yes || button == MessageButton.Ok;
            Close();
        }

        private void InitializeButton(Button control, MessageButton button, MessageButton show)
        {
            control.Visibility = (show & button) == button ? Visibility.Visible : Visibility.Collapsed;
            control.Click += (sender, args) => HandleButton(button);
        }

        private void SetInput(MessageOptionsBase options, Predicate<string> predicate, bool prompt, string defaultValue)
        {
            Title = options.Title;
            TheLabel.Content = options.Message;
            TheTextBox.Text = defaultValue;
            TheIcon.Kind = _iconMap[options.Icon];
            _accept = predicate ?? new Predicate<string>(_ => true);

            TheTextBox.Visibility = prompt ? Visibility.Visible : Visibility.Collapsed;

            InitializeButton(Yes, MessageButton.Yes, options.Buttons);
            InitializeButton(No, MessageButton.No, options.Buttons);
            InitializeButton(OK, MessageButton.Ok, options.Buttons);
            InitializeButton(Cancel, MessageButton.Cancel, options.Buttons);

            var buttons = new[] { Yes, No, OK, Cancel };
            var firstButton = buttons.First(b => b.Visibility == Visibility.Visible);
            firstButton.IsDefault = true;
            if (!prompt)
            {
                firstButton.Focus();
            }

            UpdateOK();
            TheTextBox.Focus();
        }

        private void TheTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (OK != null && _accept != null)
                UpdateOK();
        }

        private void UpdateOK()
        {
            var accept = _accept(TheTextBox.Text);
            OK.IsEnabled = accept;
            Yes.IsEnabled = accept;
        }
    }
}