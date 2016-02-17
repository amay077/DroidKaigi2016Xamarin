using System;
using Android.Support.V7.Widget;
using Android.Text;
using Java.Lang;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class AppCompatEditTextExtension
    {
        public static void AddTextChangedActions(this AppCompatEditText self, 
            Action<ICharSequence, int, int, int> beforeTextChangedHandler,
            Action<IEditable> afterTextChangedHandler,
            Action<ICharSequence, int, int, int> onTextChangedHandler)
        
        {
            self.AddTextChangedListener(new DelegateTextWatcher(
                beforeTextChangedHandler, 
                afterTextChangedHandler, 
                onTextChangedHandler));
        }

        public static void AddTextChangedAction(this AppCompatEditText self, 
            Action<ICharSequence, int, int, int> onTextChangedHandler)

        {
            self.AddTextChangedActions(
                (s, start, count, after)=>{}, 
                (s)=>{}, 
                onTextChangedHandler);
        }

        class DelegateTextWatcher : Java.Lang.Object, ITextWatcher
        {
            private readonly Action<ICharSequence, int, int, int> beforeTextChangedHandler;
            private readonly Action<IEditable> afterTextChangedHandler;
            private readonly Action<ICharSequence, int, int, int> onTextChangedHandler;

            public DelegateTextWatcher(
                Action<ICharSequence, int, int, int> beforeTextChangedHandler,
                Action<IEditable> afterTextChangedHandler,
                Action<ICharSequence, int, int, int> onTextChangedHandler)
            {
                this.afterTextChangedHandler = afterTextChangedHandler;
                this.beforeTextChangedHandler = beforeTextChangedHandler;
                this.onTextChangedHandler = onTextChangedHandler;
            }

            public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
            {
                beforeTextChangedHandler(s, start, count, after);
            }

            public void AfterTextChanged(IEditable s)
            {
                afterTextChangedHandler(s);
            }

            public void OnTextChanged(ICharSequence s, int start, int before, int count)
            {
                onTextChangedHandler(s, start, before, count);
            }
        }
    }
}

