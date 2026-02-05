using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptPlayer
{
    public partial class DebugWindow : Window
    {
        private DebugTraceListener _traceListener;
        private Queue<string> _debugMessages;
        private const int MaxMessages = 1000;

        public DebugWindow()
        {
            InitializeComponent();
            _debugMessages = new Queue<string>();
            _traceListener = new DebugTraceListener(this);
            Trace.Listeners.Add(_traceListener);
        }

        public void AddDebugMessage(string message)
        {
            _debugMessages.Enqueue($"[{DateTime.Now:HH:mm:ss.fff}] {message}");
            
            if (_debugMessages.Count > MaxMessages)
                _debugMessages.Dequeue();

            Dispatcher.Invoke(() =>
            {
                DebugTextBox.Text = string.Join(Environment.NewLine, _debugMessages);
                DebugTextBox.CaretIndex = DebugTextBox.Text.Length;
                DebugTextBox.ScrollToEnd();
            });
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _debugMessages.Clear();
            DebugTextBox.Text = "";
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(DebugTextBox.Text);
            MessageBox.Show("Debug output copied to clipboard!");
        }

        protected override void OnClosed(EventArgs e)
        {
            Trace.Listeners.Remove(_traceListener);
            base.OnClosed(e);
        }
    }

    public class DebugTraceListener : TraceListener
    {
        private readonly DebugWindow _window;

        public DebugTraceListener(DebugWindow window)
        {
            _window = window;
        }

        public override void Write(string message)
        {
            _window.AddDebugMessage(message);
        }

        public override void WriteLine(string message)
        {
            _window.AddDebugMessage(message);
        }
    }
}
