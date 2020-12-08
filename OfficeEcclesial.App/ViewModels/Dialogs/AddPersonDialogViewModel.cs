using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.App.Database.Services;
using OfficeEcclesial.App.Extensions.Mvvm;

namespace OfficeEcclesial.App.ViewModels.Dialogs
{
    public sealed class AddPersonDialogViewModel : BindableBase, IDataErrorInfo
    {
        #region Fields
        private string _name;
        private string _lastName;
        private string _phone;
        private string _cellular;
        private string _age;
        private int _genre;
        private string _address;
        private ObservableCollection<Person> _personsCollection;
        private ICollectionView _persons;
        #endregion

        #region Properties
        public int Id { get; set; }
        public int EditionId { get; set; }
        #endregion

        #region Bindable properties
        [RegularExpression("^[a-zA-Z ]{1,20}$", ErrorMessage = "El texto no cumple el formato especificado.")]
        [Required(ErrorMessage = "Usted debe proveer un valor para este campo.")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        [RegularExpression("^[a-zA-Z ]{1,20}$", ErrorMessage = "El texto no cumple el formato especificado.")]
        [Required(ErrorMessage = "Usted debe proveer un valor para este campo.")]
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged(nameof(LastName));
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                RaisePropertyChanged(nameof(Phone));
            }
        }
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "El texto no cumple el formato especificado.")]
        public string Cellular
        {
            get => _cellular;
            set
            {
                _cellular = value;
                RaisePropertyChanged(nameof(Cellular));
            }
        }
        [Required(ErrorMessageResourceName = "Required_Message", ErrorMessageResourceType = typeof(string))]
        [RegularExpression("^[0-9]{1,2}$", ErrorMessageResourceName = "Age_Invalid_Message", ErrorMessageResourceType = typeof(string))]
        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                RaisePropertyChanged(nameof(Age));
            }
        }
        [Required(ErrorMessage = "Usted debe proveer un valor para este campo.")]
        public int Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                RaisePropertyChanged(nameof(Genre));
            }
        }
        [Required(ErrorMessage = "Usted debe proveer un valor para este campo.")]
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                RaisePropertyChanged(nameof(Address));
            }
        }

        
        public ObservableCollection<Person> PersonsCollection
        {
            get => _personsCollection;
            set
            {
                _personsCollection = value;
                RaisePropertyChanged(nameof(PersonsCollection));
            }
        }
        public ICollectionView Persons
        {
            get => _persons;
            set
            {
                _persons = value;
                RaisePropertyChanged(nameof(Persons));
            }
        }
        #endregion

        #region Constructor
        public AddPersonDialogViewModel()
        {
        }
        #endregion



        #region Override functions

        public override void InitializeComponent()
        {
            var bgWorker = new BackgroundWorker();
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.RunWorkerAsync();
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //var persons = PersonsService.GetAll();
            Dispatcher.InvokeAsync(() =>
            {
                //PersonsCollection.AddRange(persons);
            });
        }

        #endregion

        #region IDataErrorInfo implementation
        public string this[string columnName]
        {
            get
            {
                try
                {
                    var property = this.GetType().GetProperty(columnName);
                    var results = new List<ValidationResult>();
                    var result = property != null && Validator.TryValidateProperty(property.GetValue(this, null),
                        new ValidationContext(this, null, null)
                        {
                            MemberName = columnName
                        }, results);
                    return !result ? results.First().ErrorMessage : string.Empty;
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
            }
        }
        public string Error { get; }

        

        #endregion

        public void Edit(IPerson person)
        {
            Id = person.Id;
            Name = person.Name;
            LastName = person.LastName;
            Phone = person.Phone;
            Cellular = person.Cellular;
            Age = person.Age.ToString();
            Genre = (int)person.Genre;
            Address = person.Address;
        }

        public Person GetPerson()
        {
            return new Person
            {
                Id = Id,
                Name = Name,
                LastName = LastName,
                Phone = Phone,
                Cellular = Cellular,
                Age = int.Parse(Age),
                Genre = Genre == 0 ? Database.Enums.Genre.M : Database.Enums.Genre.F,
                Address = Address
            };
        }
    }
}
