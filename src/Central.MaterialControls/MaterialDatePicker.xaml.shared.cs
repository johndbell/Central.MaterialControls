using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Central.MaterialControls
{
    public partial class MaterialDatePicker : MaterialEntryBase
    {

        public static BindableProperty CustomDateFormatProperty = BindableProperty.Create(nameof(CustomDateFormat), typeof(string), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay);
        public string CustomDateFormat
        {
            get
            {
                return (string)GetValue(CustomDateFormatProperty);
            }
            set
            {
                SetValue(CustomDateFormatProperty, value);
            }
        }
        private static string _defaultDateFormat = "dddd, MMMM d, yyyy";
        public static BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay);

        public DateTime? Date
        {
            get
            {
                return (DateTime?)GetValue(DateProperty);
            }
            set
            {
                SetValue(DateProperty, value);
            }
        }
        
        public MaterialDatePicker()
        {
            InitializeComponent();
            base.InitialiseBase();
            
            EntryField.Focused += (s, a) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await base.CalculateLayoutFocused();
                    EntryField.Unfocus();
                    Picker.Focus();
                });
            };
            EntryField.Unfocused += (s, a) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await base.CalculateLayoutFocused();
                });
            };
            Picker.Focused += (s, a) =>
            {
                //await base.CalculateLayoutFocused();
            };
            Picker.Unfocused += (s, a) =>
            {
                //await CalculateLayoutUnfocused();
                Picker_DateSelected(s, new DateChangedEventArgs(Picker.Date, Picker.Date));
            };

            Picker.DateSelected += Picker_DateSelected;
        }

        private void Picker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomDateFormat))
                CustomDateFormat = _defaultDateFormat;
            EntryField.Text = e.NewDate.ToString(CustomDateFormat, CultureInfo.CurrentCulture);
            Date = e.NewDate;
        }
    }
}
