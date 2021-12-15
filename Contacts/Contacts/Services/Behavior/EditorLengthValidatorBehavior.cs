using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contacts.Services.Behavior
{
    public class EditorLengthValidatorBehavior : Behavior<Editor>
    {
        public int MaxLength { get; set; }

        protected override void OnAttachedTo(Editor bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Editor bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var editorText = sender as Editor;

            if (editorText.Text.Length > this.MaxLength)
            {
                string entryText = editorText.Text;

                entryText = entryText.Remove(entryText.Length - 1); 

                editorText.Text = entryText;
            }
        }
    }
}
