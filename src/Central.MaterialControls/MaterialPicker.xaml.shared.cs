using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Central.MaterialControls
{
    public partial class MaterialPicker : MaterialEntryBase
    {
        public event EventHandler SelectedIndexChanged;

        public override void OnPlaceholderChanged(string placeholder)
        {
            base.OnPlaceholderChanged(placeholder);
            Picker.Title = placeholder;
        }

        public static BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(IList), typeof(MaterialPicker), null);
        public static BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(MaterialPicker), 0, BindingMode.TwoWay);
        
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MaterialPicker), null, BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var matPicker = (MaterialPicker)bindable;
            matPicker.HiddenLabel.IsVisible = !string.IsNullOrEmpty(newValue?.ToString());
        });
        public static BindableProperty SelectedIndexChangedCommandProperty = BindableProperty.Create(nameof(SelectedIndexChangedCommand), typeof(ICommand), typeof(MaterialPicker), null);

        public ICommand SelectedIndexChangedCommand
        {
            get { return (ICommand)GetValue(SelectedIndexChangedCommandProperty); }
            set { SetValue(SelectedIndexChangedCommandProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }

        public IList Items
        {
            get
            {
                return (IList)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        public MaterialPicker()
        {
            InitializeComponent();
            base.InitialiseBase();
            Picker.BindingContext = this;
            //BottomBorder.BackgroundColor = DefaultColor;
            // TODO: Possible memory leak?
            Picker.SelectedIndexChanged += (sender, e) =>
            {
                SelectedIndexChangedCommand?.Execute(Picker.SelectedItem);
                SelectedIndexChanged?.Invoke(sender, e);
            };

            Picker.Focused += async (s, a) =>
            {
                await base.CalculateLayoutFocused();
            };
            Picker.Unfocused += async (s, a) =>
            {
                await base.CalculateLayoutUnfocused();
            };

        }

        public override bool ValueIsNullOrEmpty { get => Picker.SelectedItem == null; }

    }
}