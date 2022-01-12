using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using LightImage.Interactions.Messages;
using LightImage.Interactions.Prompts;
using System.Collections.Generic;
using Material.Icons;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Avalonia.Input;

namespace LightImage.Interactions.Avalonia
{
    public partial class MessageWindow : Window,
        IInteractionHandler<MessageOptions, MessageResult>,
        IInteractionHandler<PromptOptions, PromptResult>
    {
        private bool? _result;

        public MessageWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private readonly IReadOnlyDictionary<MessageIcons, MaterialIconKind> _iconMap = new Dictionary<MessageIcons, MaterialIconKind>
        {
            { MessageIcons.Edit, MaterialIconKind.Pencil },
            { MessageIcons.Information, MaterialIconKind.Information },
            { MessageIcons.Question, MaterialIconKind.CommentQuestion },
            { MessageIcons.Warning, MaterialIconKind.AlertCircleOutline },
            { MessageIcons.Error, MaterialIconKind.AlertOctagon },
        };

        private Predicate<string> _accept;

        /// <summary>
        /// Gets the button that has been selected.
        /// </summary>
        public MessageButton Button { get; private set; } = default;

        /// <inheritdoc/>
        public async Task<MessageResult> Handle(MessageOptions request, CancellationToken cancellationToken)
        {
            SetInput(request, null, false, string.Empty);
            await this.ShowDialog(AvaloniaWindows.GetMainWindow());
            return new MessageResult(Button);
        }

        /// <inheritdoc/>
        public async Task<PromptResult> Handle(PromptOptions request, CancellationToken cancellationToken)
        {
            SetInput(request, request.Predicate, true, request.Value);
            await this.ShowDialog(AvaloniaWindows.GetMainWindow());
            return new PromptResult(_result == true ? TheTextBox.Text : null, Button);
        }

        private void HandleButton(MessageButton button)
        {
            Button = button;
            _result = button == MessageButton.Yes || button == MessageButton.Ok;
            Close();
        }

        private void InitializeButton(Button control, MessageButton button, MessageButton show)
        {
            control.IsVisible = (show & button) == button;
            control.Click += (sender, args) => HandleButton(button);
        }

        private void SetInput(MessageOptionsBase options, Predicate<string> predicate, bool prompt, string defaultValue)
        {
            Title = options.Title;
            TheLabel.Content = options.Message;
            TheTextBox.Text = defaultValue;
            TheIcon.Kind = _iconMap[options.Icon];
            _accept = predicate ?? new Predicate<string>(_ => true);

            TheTextBox.IsVisible = prompt;

            InitializeButton(Yes, MessageButton.Yes, options.Buttons);
            InitializeButton(No, MessageButton.No, options.Buttons);
            InitializeButton(OK, MessageButton.Ok, options.Buttons);
            InitializeButton(Cancel, MessageButton.Cancel, options.Buttons);

            var buttons = new[] { Yes, No, OK, Cancel };
            var firstButton = buttons.First(b => b.IsVisible);
            firstButton.IsDefault = true;
            if (!prompt)
            {
                firstButton.Focus();
            }

            UpdateOK();
            TheTextBox.GetObservable(TextBox.TextProperty).Subscribe(HandleTextChanged);
            TheTextBox.AttachedToVisualTree += (s, e) => { TheTextBox.Focus(); TheTextBox.SelectAll(); };
        }

        public override void Show()
        {
            base.Show();            
        }

        private void HandleTextChanged(string text)
        {
            if (OK != null && _accept != null)
            {
                UpdateOK();
            }
        }

        private void UpdateOK()
        {
            var accept = _accept(TheTextBox.Text);
            OK.IsEnabled = accept;
            Yes.IsEnabled = accept;
        }
    }
}
