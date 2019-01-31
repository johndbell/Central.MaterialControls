using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Central.MaterialControls
{
    public abstract class MaterialEntryBase : ContentView
    {
        protected void InitialiseBase()
        {
            // These are the controls we care about for settings colours etc.
            _OutlineFrame = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Frame>(this, "OutlineFrame");
            _BackgroundBox = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.BoxView>(this, "BackgroundBox");
            _HiddenLabel = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "HiddenLabel");
            _EntryField = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Central.MaterialControls.BorderlessEntry>(this, "EntryField");
            _BottomBorder = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.BoxView>(this, "BottomBorder");
            _HiddenBottomBorder = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.BoxView>(this, "HiddenBottomBorder");

            _EntryField.BindingContext = this;
            _BottomBorder.BackgroundColor = DefaultColor;
        }

        public Frame _OutlineFrame { get; private set; }
        public BoxView _BackgroundBox { get; private set; }
        public Label _HiddenLabel { get; private set; }
        public BorderlessEntry _EntryField { get; private set; }
        public BoxView _BottomBorder { get; private set; }
        public BoxView _HiddenBottomBorder { get; private set; }

        public static BindableProperty UnderlineWithBackgroundColorProperty = BindableProperty.Create(nameof(UnderlineWithBackgroundColor), typeof(Color), typeof(MaterialEntryBase), Color.FromHex("#EEEEEE"), propertyChanged: (bindable, oldValue, newValue) =>
        {    
        });

        public static BindableProperty ShowTitleProperty = BindableProperty.Create(nameof(ShowTitle), typeof(bool), typeof(MaterialEntryBase), Device.RuntimePlatform == Device.iOS ? false : true, propertyChanged: (bindable, oldVal, newVal) =>
        {
        });
        public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialEntryBase), defaultValue: Color.Accent);
        public static BindableProperty InvalidColorProperty = BindableProperty.Create(nameof(InvalidColor), typeof(Color), typeof(MaterialEntryBase), Color.Red, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntryBase)bindable;
            matEntry.UpdateColors();
        });
        public static BindableProperty DefaultColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntryBase), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntryBase)bindable;
            matEntry.UpdateColors();
        });
        public static BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(MaterialEntryBase), true, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntryBase)bindable;
            matEntry.UpdateColors();
        });
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialEntryBase), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (MaterialEntryBase)bindable;
            matEntry._EntryField.Placeholder = (string)newval;
            matEntry._HiddenLabel.Text = (string)newval;
        });
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialEntryBase), defaultBindingMode: BindingMode.TwoWay);


        public static BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialEntryBase), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntryBase)bindable;
            matEntry._EntryField.IsPassword = (bool)newVal;
        });
        public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialEntryBase), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntryBase)bindable;
            matEntry._EntryField.Keyboard = (Keyboard)newVal;
        });

        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColorProperty), typeof(Color), typeof(MaterialEntryBase), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntryBase)bindable;
            matEntry._EntryField.PlaceholderColor = (Color)newVal;
        });
        public static BindableProperty TextBackgroundColorProperty = BindableProperty.Create(nameof(TextBackgroundColorProperty), typeof(Color), typeof(MaterialEntryBase), Color.Transparent, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntryBase)bindable;
            matEntry._EntryField.BackgroundColor = (Color)newVal;
        });
        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntryBase), Color.Black, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntryBase)bindable;
            matEntry._EntryField.TextColor = (Color)newVal;
        });

        public static BindableProperty HiddenLabelTextSizeProperty = BindableProperty.Create(nameof(HiddenLabelTextSizeProperty), typeof(double), typeof(MaterialEntryBase), 10.0, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntryBase)bindable;
            matEntry._HiddenLabel.FontSize = (double)newVal;
        });

        public static BindableProperty CompletedProperty = BindableProperty.Create(nameof(Completed), typeof(EventHandler), typeof(MaterialEntryBase), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var matEntry = (MaterialEntryBase)bindable;
            matEntry.Completed = (EventHandler)newValue;
        });

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

        public EventHandler Completed
        {
            get => null;
            set => _EntryField.Completed += value;
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

        public static BindableProperty BorderStyleProperty = BindableProperty.Create(nameof(BorderStyle), typeof(BorderStyleEnum), typeof(MaterialEntryBase), BorderStyleEnum.Underline, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var matEntry = bindable as MaterialEntryBase;
            matEntry.UpdateBorderStyle();
        });

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

        public void UpdateBorderStyle()
        {
            switch (BorderStyle)
            {
                case BorderStyleEnum.Underline:
                case BorderStyleEnum.UnderlineWithBackground:
                    _OutlineFrame.IsVisible = false;
                    _HiddenBottomBorder.IsVisible = true;
                    _BottomBorder.IsVisible = true;
                    _BackgroundBox.IsVisible = BorderStyle == BorderStyleEnum.UnderlineWithBackground;
                    break;
                case BorderStyleEnum.Outline:
                    _HiddenBottomBorder.IsVisible = false;
                    _BottomBorder.IsVisible = false;
                    _OutlineFrame.IsVisible = true;
                    _BackgroundBox.IsVisible = false;
                    break;
            }

            UpdateColors();
        }

        public void UpdateColors()
        {

            _HiddenLabel.BackgroundColor = BackgroundColor;
            if (IsValid)
            {
                if (_EntryField.IsFocused)
                {
                    _HiddenLabel.TextColor = AccentColor;
                    if (BorderStyle == BorderStyleEnum.Underline || BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                    {
                        _BottomBorder.BackgroundColor = DefaultColor;
                        _HiddenBottomBorder.BackgroundColor = AccentColor;

                        if (BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                        {
                            _BackgroundBox.BackgroundColor = UnderlineWithBackgroundColor;
                            _HiddenLabel.BackgroundColor = UnderlineWithBackgroundColor;
                        }
                    }
                    else if (BorderStyle == BorderStyleEnum.Outline)
                    {
                        _OutlineFrame.BorderColor = AccentColor;
                    }
                }
                else
                {
                    _HiddenLabel.TextColor = DefaultColor;
                    if (BorderStyle == BorderStyleEnum.Underline || BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                    {
                        _BottomBorder.BackgroundColor = DefaultColor;
                        _HiddenBottomBorder.BackgroundColor = DefaultColor;
                        if (BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                        {
                            _BackgroundBox.BackgroundColor = UnderlineWithBackgroundColor;
                            _HiddenLabel.BackgroundColor = UnderlineWithBackgroundColor;
                        }
                    }
                    else if (BorderStyle == BorderStyleEnum.Outline)
                    {
                        _OutlineFrame.BorderColor = DefaultColor;
                    }
                }
            }
            else // not valid
            {
                if (BorderStyle == BorderStyleEnum.Underline || BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                {
                    _BottomBorder.BackgroundColor = InvalidColor;
                    _HiddenBottomBorder.BackgroundColor = InvalidColor;
                    if (BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                    {
                        _HiddenLabel.BackgroundColor = UnderlineWithBackgroundColor;
                    }
                }
                else if (BorderStyle == BorderStyleEnum.Outline)
                {
                    _OutlineFrame.BorderColor = InvalidColor;
                }
                _HiddenLabel.TextColor = InvalidColor;
            }
        }



        /// <summary>
        /// Calculates the layout when unfocused. Includes running the animation to update the bottom border color and the floating label
        /// </summary>
        /// <returns>The layout unfocused.</returns>
        protected async Task CalculateLayoutUnfocused()
        {
            if (string.IsNullOrEmpty(_EntryField.Text))
            {
                if (ShowTitle)
                {
                    if (BorderStyle == BorderStyleEnum.Underline || BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                    {
                        // animate both at the same time
                        await Task.WhenAll(
                            _HiddenBottomBorder.LayoutTo(new Rectangle(_BottomBorder.X, _BottomBorder.Y, 0, _BottomBorder.Height), 200),
                            _HiddenLabel.FadeTo(0, 180),
                            _HiddenLabel.TranslateTo(_HiddenLabel.TranslationX, _EntryField.Y, 200, Easing.Linear),
                            _EntryField.TranslateTo(_EntryField.TranslationX, _prevEntryY, 200, Easing.Linear)
                        );
                    }
                    else
                    {
                        _HiddenLabel.Text = _HiddenLabel.Text.Trim();
                        await Task.WhenAll(
                            _HiddenLabel.FadeTo(0, 180),
                            _HiddenLabel.TranslateTo(_HiddenLabel.TranslationX, _EntryField.Y, 200, Easing.Linear)
                        );
                    }
                }

                _EntryField.Placeholder = Placeholder;
            }
            else
            {
                _HiddenLabel.IsVisible = false;
                if (ShowTitle)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        await _HiddenBottomBorder.LayoutTo(new Rectangle(_BottomBorder.X, _BottomBorder.Y, 0, _BottomBorder.Height), 200);
                    }
                }
            }
            this.UpdateColors();
        }

        double _prevEntryY;

        /// <summary>
        /// Calculates the layout when focused. Includes running the animation to update the bottom border color and the floating label
        /// </summary>
        protected async Task CalculateLayoutFocused()
        {
            _HiddenLabel.Opacity = 0;
            _HiddenLabel.IsVisible = ShowTitle;

            if (string.IsNullOrEmpty(_EntryField.Text))
            {
                if (ShowTitle)
                {
                    // animate both at the same time
                    if (BorderStyle == BorderStyleEnum.Underline || BorderStyle == BorderStyleEnum.UnderlineWithBackground)
                    {
                        _prevEntryY = _EntryField.TranslationY;
                        await Task.WhenAll(
                            _HiddenBottomBorder.LayoutTo(new Rectangle(_BottomBorder.X, _BottomBorder.Y, _BottomBorder.Width, _BottomBorder.Height), 200),
                            _HiddenLabel.FadeTo(1, 60),
                            _HiddenLabel.TranslateTo(_HiddenLabel.TranslationX, 5, 200, Easing.Linear),
                            _EntryField.TranslateTo(_EntryField.TranslationX, _OutlineFrame.TranslationY + 5, 200, Easing.Linear)
                        );
                    }
                    else
                    {
                        _HiddenLabel.Text = $" {_HiddenLabel.Text.Trim()} ";
                        await Task.WhenAll(
                            _HiddenLabel.FadeTo(1, 60),
                            _HiddenLabel.TranslateTo(_HiddenLabel.TranslationX, -7, 200, Easing.Linear)
                        );
                    }
                }
                _EntryField.Placeholder = null;
            }
            else
            {
                if (ShowTitle)
                {
                    await _HiddenBottomBorder.LayoutTo(new Rectangle(_BottomBorder.X, _BottomBorder.Y, _BottomBorder.Width, _BottomBorder.Height), 200);
                }
            }
            this.UpdateColors();
        }

    }
}
