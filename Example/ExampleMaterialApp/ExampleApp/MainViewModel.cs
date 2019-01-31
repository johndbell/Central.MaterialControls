using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExampleMaterialApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _isValid = true;
        public ObservableCollection<Fruit> PickerData { get; set; }
        public Fruit PickerSelectedItem { get; set; }
        public ICommand PickerSelectedIndexChangedCmd { get; }
        public ICommand SwitchValidCommand { get; }
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
            }
        }


            private bool _showTitle = true;
        public bool ShowTitle
        {
            get => _showTitle;
            set
            {
                _showTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowTitle)));
            }
        }

        private bool _underlineOnly = true;
        public bool UnderlineOnly
        {
            get => _underlineOnly;
            set
            {
                _underlineOnly = value;
                _outline = _underlineWithBackground = !UnderlineOnly;
                UpdateProps();
            }
        }
        private bool _underlineWithBackground = false;
        public bool UnderlineWithBackground
        {
            get => _underlineWithBackground;
            set
            {
                _underlineWithBackground = value;
                _underlineOnly = _outline = !UnderlineWithBackground;
                UpdateProps();
            }
        }
        private bool _outline = false;
        public bool OutlineOnly
        {
            get => _outline;
            set
            {
                _outline = value;
                _underlineOnly = _underlineWithBackground = !OutlineOnly;
                UpdateProps();
            }
        }

        private void UpdateProps()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnderlineOnly)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnderlineWithBackground)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OutlineOnly)));
        }
    

        public MainViewModel()
        {
            PickerData = new ObservableCollection<Fruit>
            {
                new Fruit { Id = 1, Name ="Apple" },
                new Fruit { Id = 2, Name = "Banana" },
                new Fruit { Id = 3, Name = "Orange" }
            };

            PickerSelectedItem = PickerData[PickerData.Count - 1];

            PickerSelectedIndexChangedCmd = new Command<Fruit>((selectedFruit) => Debug.WriteLine($"PickerSelectedIndexChangedCmd => {selectedFruit}"));
            SwitchValidCommand = new Command(() => IsValid = !IsValid);
        }

        // Fody will take care of that
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Fruit
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
