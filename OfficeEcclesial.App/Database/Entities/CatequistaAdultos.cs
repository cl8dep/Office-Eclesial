using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using OfficeEcclesial.App.Annotations;
using OfficeEcclesial.App.Database.Enums;

namespace OfficeEcclesial.App.Database.Entities
{
    public class CatequistaAdultos : IPerson, INotifyPropertyChanged
    {
        #region Fields
        private string _name;
        private string _lastName;
        private string _address;
        private string _phone;
        private string _cellular;
        private int _age;
        private bool _isSelected;
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public Genre Genre { get; set; }
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public string Cellular
        {
            get => _cellular;
            set
            {
                _cellular = value;
                OnPropertyChanged(nameof(Cellular));
            }
        }
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }
        [NotMapped]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        #endregion
        public override string ToString()
        {
            return $"{Name}";
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Assign(IPerson person)
        {
            Name = person.Name;
            LastName = person.LastName;
            Phone = person.Phone;
            Cellular = person.Cellular;
            Age = person.Age;
            Genre = person.Genre;
            Address = person.Address;
        }
    }
}
