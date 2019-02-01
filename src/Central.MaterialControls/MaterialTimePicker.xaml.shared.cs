using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Central.MaterialControls
{
    public partial class MaterialTimePicker : MaterialEntryBase
    {


        public static BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan?), typeof(MaterialTimePicker), defaultBindingMode: BindingMode.TwoWay);

        public TimeSpan? Time
        {
            get
            {
                return (TimeSpan?)GetValue(TimeProperty);
            }
            set
            {
                SetValue(TimeProperty, value);
            }
        }

        public MaterialTimePicker()
        {
            InitializeComponent();
            base.InitialiseBase();
            
            EntryField.Focused += (s, a) =>
            {
				Device.BeginInvokeOnMainThread(() => {
					EntryField.Unfocus();
					Picker.Focus();
				});
            };
            Picker.Focused += async (s, a) =>
            {
                await CalculateLayoutFocused();
            };
            Picker.Unfocused += async (s, a) =>
            {
                await CalculateLayoutUnfocused();
            };

            Picker.PropertyChanged += Picker_PropertyChanged; ;
        }

        private void Picker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Picker.Time))
            {
                EntryField.Text = DateTime.Today.Add(Picker.Time).ToString("hh:mm tt");
                Time = Picker.Time;
            }
        }

        public TimePicker GetUnderlyingPicker()
        {
            return Picker;
        }



        /// <summary>
        /// Updates view based on validation state
        /// </summary>
        private void UpdateValidation()
        {
            if (IsValid)
            {

                BottomBorder.BackgroundColor = DefaultColor;
                HiddenBottomBorder.BackgroundColor = AccentColor;
                if (IsFocused)
                {
                    HiddenLabel.TextColor = AccentColor;
                }
                else
                {
                    HiddenLabel.TextColor = DefaultColor;
                }
            }
            else
            {
                BottomBorder.BackgroundColor = InvalidColor;
                HiddenBottomBorder.BackgroundColor = InvalidColor;
                HiddenLabel.TextColor = InvalidColor;
            }
        }

    }
}