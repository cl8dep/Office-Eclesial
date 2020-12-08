using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Data;
using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.App.Database.Services;
using OfficeEcclesial.App.Extensions.Mvvm;

namespace OfficeEcclesial.App.ViewModels.Dialogs
{
    public sealed class AddCatequesisMemberDialogViewModel : BindableBase, IDataErrorInfo
    {
        #region Fields
        private string _name;
        private string _lastName;
        private string _phone;
        private string _cellular;
        private string _age;
        private int _genre;
        private string _address;
        private DateTime _birthDate;
        private string _birthPlace;
        private string _motherName;
        private string _fatherName;
        #endregion

        #region Properties
        private int Id { get; set; }
        public int EditionId { get; set; }
        private PersonService Service { get; }
        #endregion

        #region Bindable properties
        [RegularExpression("^[a-zA-Z ]{1,20}$", ErrorMessage = "El texto no cumple el formato especificado.")]
        [Required(ErrorMessageResourceName = "Required_Message", ErrorMessageResourceType = typeof(Properties.Resources))]
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
        [Required(ErrorMessageResourceName = "Required_Message", ErrorMessageResourceType = typeof(Properties.Resources))]
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
        [Required(ErrorMessageResourceName = "Required_Message", ErrorMessageResourceType = typeof(Properties.Resources))]
        [RegularExpression("^[0-9]{1,2}$", ErrorMessageResourceName = "Age_Invalid_Message", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                RaisePropertyChanged(nameof(Age));
            }
        }
        [Required(ErrorMessageResourceName = "Required_Message", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                RaisePropertyChanged(nameof(Genre));
            }
        }
        [Required(ErrorMessageResourceName = "Required_Message", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                RaisePropertyChanged(nameof(Address));
            }
        }
        

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                RaisePropertyChanged(nameof(BirthDate));
            }
        }
        public string BirthPlace
        {
            get => _birthPlace;
            set
            {
                _birthPlace = value;
                RaisePropertyChanged(nameof(BirthPlace));
            }
        }
        public string FatherName
        {
            get => _fatherName;
            set
            {
                _fatherName = value;
                RaisePropertyChanged(nameof(FatherName));
            }
        }
        public string MotherName
        {
            get => _motherName;
            set
            {
                _motherName = value;
                RaisePropertyChanged(nameof(MotherName));
            }
        }
        #endregion

        #region Constructor
        public AddCatequesisMemberDialogViewModel(PersonService service)
        {
            Service = service;

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
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
        public string Error { get; }
        #endregion

        public void Edit(CatequesisMember person)
        {
            Id = person.Id;
            Name = person.Name;
            LastName = person.LastName;
            Phone = person.Phone;
            Cellular = person.Cellular;
            Age = person.Age.ToString();
            Genre = (int)person.Genre;
            Address = person.Address;
            BirthDate = person.FechadeNacimiento.Value;
            BirthPlace = person.LugarDeNacimiento;
            MotherName = person.NombreMadre;
            FatherName = person.NombrePadre;
        }

        public CatequesisMember GetEntity()
        {
            return new CatequesisMember
            {
                Id = Id,
                Name = Name,
                LastName = LastName,
                Phone = Phone,
                Cellular = Cellular,
                Age = int.Parse(Age),
                Genre = Genre == 0 ? Database.Enums.Genre.M : Database.Enums.Genre.F,
                Address = Address,
                FechadeNacimiento = BirthDate,
                LugarDeNacimiento = BirthPlace,
                NombreMadre = MotherName,
                NombrePadre = FatherName
            };
        }
    }
}
