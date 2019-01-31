using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Central.MaterialControls
{
    public partial class MaterialEntry : ContentView
    {
        #region Events
        public event EventHandler<FocusEventArgs> EntryFocused;
        public event EventHandler<FocusEventArgs> EntryUnfocused;
        public event EventHandler<TextChangedEventArgs> TextChanged;
        #endregion

        #region Bindable Properties
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialEntry), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialEntry), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.Placeholder = (string)newval;
            matEntry.HiddenLabel.Text = (string)newval;
        });

        public static BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialEntry), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.IsPassword = (bool)newVal;
        });
        public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialEntry), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.Keyboard = (Keyboard)newVal;
        });
        public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialEntry), defaultValue: Color.Accent);
        public static BindableProperty InvalidColorProperty = BindableProperty.Create(nameof(InvalidColor), typeof(Color), typeof(MaterialEntry), Color.Red, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.UpdateColours();
        });
        public static BindableProperty DefaultColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntry), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.UpdateColours();
        });
        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColorProperty), typeof(Color), typeof(MaterialEntry), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.PlaceholderColor = (Color)newVal;
        });
        public static BindableProperty TextBackgroundColorProperty = BindableProperty.Create(nameof(TextBackgroundColorProperty), typeof(Color), typeof(MaterialEntry), Color.Transparent, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.BackgroundColor = (Color)newVal;
        });
        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntry), Color.Black, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.TextColor = (Color)newVal;
        });
        public static BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(MaterialEntry), true, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.UpdateColours();
        });
        public static BindableProperty HiddenLabelTextSizeProperty = BindableProperty.Create(nameof(HiddenLabelTextSizeProperty), typeof(double), typeof(MaterialEntry), 10.0, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.HiddenLabel.FontSize = (double)newVal;
        });
        public static BindableProperty ShowTitleProperty = BindableProperty.Create(nameof(ShowTitle), typeof(bool), typeof(MaterialEntry), Device.RuntimePlatform == Device.iOS ? false:true, propertyChanged: (bindable, oldVal, newVal) =>
        {            
        });

        public static BindableProperty CompletedProperty = BindableProperty.Create(nameof(Completed), typeof(EventHandler), typeof(MaterialEntry), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.Completed = (EventHandler)newValue;
        });
        public static BindableProperty UnderlineWithBackgroundColorProperty = BindableProperty.Create(nameof(UnderlineWithBackgroundColor), typeof(Color), typeof(MaterialEntry), Color.FromHex("#EEEEEE"), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var matEntry = (MaterialEntry)bindable;
        });
        public static BindableProperty BorderStyleProperty = BindableProperty.Create(nameof(BorderStyle), typeof(BorderStyleEnum), typeof(MaterialEntry), BorderStyleEnum.Underline, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var matEntry = (MaterialEntry)bindable;
            var borderStyle = (BorderStyleEnum)newValue;
            switch(borderStyle)
            {
                case BorderStyleEnum.Underline:
                case BorderStyleEnum.UnderlineWithBackground:
                    matEntry.OutlineFrame.IsVisible = false;
                    matEntry.HiddenBottomBorder.IsVisible = true;
                    matEntry.BottomBorder.IsVisible = true;
                    matEntry.BackgroundBox.IsVisible = borderStyle == BorderStyleEnum.UnderlineWithBackground;
                    break;
                case BorderStyleEnum.Outline:
                    matEntry.HiddenBottomBorder.IsVisible = false;
                    matEntry.BottomBorder.IsVisible = false;
                    matEntry.OutlineFrame.IsVisible = true;
                    matEntry.BackgroundBox.IsVisible = false;
                    break;
            }

            matEntry.UpdateColours();
        });


        public enum BorderStyleEnum
        {
            Underline,
            UnderlineWithBackground,
            Outline,
        }

        #endregion

        #region Public Properties

        public Color UnderlineWithBackgroundColor
        {
            get
            {
                return (Color)GetValue(UnderlineWithBackgroundColorProperty);
            }
            set
            {
                SetValue(UnderlineWithBackgroundColorProperty, value);
            }
        }

        public BorderStyleEnum BorderStyle
        {
            get
            {
                return (BorderStyleEnum)GetValue(BorderStyleProperty);
            }
            set
            {
                SetValue(BorderStyleProperty, value);
            }
        }

        public bool ShowTitle
        {
            get
            {
                return (bool)GetValue(ShowTitleProperty);
            }
            set
            {
                SetValue(ShowTitleProperty, value);
            }
        }

        public bool IsValid
        {
            get
            {
                return (bool)GetValue(IsValidProperty);
            }
            set
            {
                SetValue(IsValidProperty, value);
            }
        }
        public Color DefaultColor
        {
            get
            {
                return (Color)GetValue(DefaultColorProperty);
            }
            set
            {
                SetValue(DefaultColorProperty, value);
            }
        }
        public Color InvalidColor
        {
            get
            {
                return (Color)GetValue(InvalidColorProperty);
            }
            set
            {
                SetValue(InvalidColorProperty, value);
            }
        }

        public Color AccentColor
        {
            get
            {
                return (Color)GetValue(AccentColorProperty);
            }
            set
            {
                SetValue(AccentColorProperty, value);
            }
        }

        public Color TextBackgroundColor
        {
            get
            {
                return (Color)GetValue(TextBackgroundColorProperty);
            }
            set
            {
                SetValue(TextBackgroundColorProperty, value);
            }
        }

        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double HiddenLabelTextSize
        {
            get
            {
                return (double)GetValue(HiddenLabelTextSizeProperty);
            }
            set
            {
                SetValue(HiddenLabelTextSizeProperty, value);
            }
        }

        public Color PlaceholderColor
        {
            get
            {
                return (Color)GetValue(PlaceholderColorProperty);
            }
            set
            {
                SetValue(PlaceholderColorProperty, value);
            }
        }
        public Keyboard Keyboard
        {
            get
            {
                return (Keyboard)GetValue(KeyboardProperty);
            }
            set
            {
                SetValue(KeyboardProperty, value);
            }
        }

        public bool IsPassword
        {
            get
            {
                return (bool)GetValue(IsPasswordProperty);
            }
            set
            {
                SetValue(IsPasswordProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        public EventHandler Completed
        {
            get => null;
            set => EntryField.Completed += value;
        }

        #endregion

        public MaterialEntry()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
            BottomBorder.BackgroundColor = DefaultColor;
            EntryField.TextChanged += (s, a) =>
            {
                TextChanged?.Invoke(s, a);
            };

            EntryField.Focused += async (s, a) =>
            {
                EntryFocused?.Invoke(this, a);
                await CalculateLayoutFocused();

            };
            EntryField.Unfocused += async (s, a) =>
            {
                EntryUnfocused?.Invoke(this, a);
                await CalculateLayoutUnfocused();
            };
            EntryField.PropertyChanged += async (sender, args) =>
            {
                if (args.PropertyName == nameof(EntryField.Text) && !EntryField.IsFocused && !String.IsNullOrEmpty(EntryField.Text))
                {
                    await CalculateLayoutUnfocused();
                }
            };

            UpdateColours();
        }

        /// <summary>
        /// Calculates the layout when unfocused. Includes running the animation to update the bottom border color and the floating label
        /// </summary>
        /// <returns>The layout unfocused.</returns>
        private async Task CalculateLayoutUnfocused()
        {
            if (string.IsNullOrEmpty(EntryField.Text))
            {
                if (ShowTitle)
                {
                    if (BorderStyle == BorderStyleEnum.Underline || BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                    {
                        // animate both at the same time
                        await Task.WhenAll(
                            HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200),
                            HiddenLabel.FadeTo(0, 180),
                            HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y, 200, Easing.Linear),
                            EntryField.TranslateTo(EntryField.TranslationX, _prevEntryY, 200, Easing.Linear)
                        );
                    }
                    else 
                    {
                        HiddenLabel.Text = HiddenLabel.Text.Trim();
                        await Task.WhenAll(
                            HiddenLabel.FadeTo(0, 180),
                            HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y, 200, Easing.Linear)
                        );
                    }
                }
             
                EntryField.Placeholder = Placeholder;
            }
            else
            {
                HiddenLabel.IsVisible = false;
                if (ShowTitle)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200);
                    }
                }
            }
            UpdateColours();
        }

        double _prevEntryY;

        /// <summary>
        /// Calculates the layout when focused. Includes running the animation to update the bottom border color and the floating label
        /// </summary>
        private async Task CalculateLayoutFocused()
        {
            HiddenLabel.Opacity = 0;
            HiddenLabel.IsVisible = ShowTitle;
            //HiddenLabel.TextColor = AccentColor;
            //BottomBorder.BackgroundColor = AccentColor;
            
            if (string.IsNullOrEmpty(EntryField.Text))
            {
                if (ShowTitle)
                {
                    // animate both at the same time
                    if (BorderStyle == BorderStyleEnum.Underline || BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                    {
                        _prevEntryY = EntryField.TranslationY;
                        await Task.WhenAll(
                            HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200),
                            HiddenLabel.FadeTo(1, 60),
                            HiddenLabel.TranslateTo(HiddenLabel.TranslationX, 5, 200, Easing.Linear),
                            EntryField.TranslateTo(EntryField.TranslationX, OutlineFrame.TranslationY + 5, 200, Easing.Linear)
                        );
                    }
                    else 
                    {
                        HiddenLabel.Text = $" {HiddenLabel.Text.Trim()} ";
                        await Task.WhenAll(
                            HiddenLabel.FadeTo(1, 60),
                            HiddenLabel.TranslateTo(HiddenLabel.TranslationX, -7, 200, Easing.Linear)
                        );
                    }
                }
                EntryField.Placeholder = null;
            }
            else
            {
                if (ShowTitle)
                {
                    await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200);
                }
            }
            UpdateColours();
        }

        /// <summary>
        /// Updates view based state
        /// </summary>
        private void UpdateColours()
        {
            HiddenLabel.BackgroundColor = BackgroundColor;
            if (IsValid)
            {
                if (EntryField.IsFocused)
                {
                    HiddenLabel.TextColor = AccentColor;
                    if (BorderStyle == BorderStyleEnum.Underline || BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                    {
                        BottomBorder.BackgroundColor = DefaultColor;
                        HiddenBottomBorder.BackgroundColor = AccentColor;
                        
                        if (BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                        {
                            BackgroundBox.BackgroundColor = UnderlineWithBackgroundColor;
                            HiddenLabel.BackgroundColor = UnderlineWithBackgroundColor;
                        }
                    }
                    else if (BorderStyle == BorderStyleEnum.Outline)
                    {
                        OutlineFrame.BorderColor = AccentColor;
                    }
                }
                else
                {
                    HiddenLabel.TextColor = DefaultColor;
                    if (BorderStyle == BorderStyleEnum.Underline || BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                    {
                        BottomBorder.BackgroundColor = DefaultColor;
                        HiddenBottomBorder.BackgroundColor = DefaultColor;
                        if (BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                        {
                            BackgroundBox.BackgroundColor = UnderlineWithBackgroundColor;
                            HiddenLabel.BackgroundColor = UnderlineWithBackgroundColor;
                        }
                    }
                    else if (BorderStyle == BorderStyleEnum.Outline)
                    {
                        OutlineFrame.BorderColor = DefaultColor;
                    }
                }
            }
            else // not valid
            {
                if (BorderStyle == BorderStyleEnum.Underline || BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                {
                    BottomBorder.BackgroundColor = InvalidColor;
                    HiddenBottomBorder.BackgroundColor = InvalidColor;
                    if (BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                    {
                        HiddenLabel.BackgroundColor = UnderlineWithBackgroundColor;
                    }
                }
                else if (BorderStyle == BorderStyleEnum.Outline)
                {
                    OutlineFrame.BorderColor = InvalidColor;
                }
                HiddenLabel.TextColor = InvalidColor;
            }
        }
    }
}