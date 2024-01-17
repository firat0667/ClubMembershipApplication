using FieldValidatorAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication.FieldValidators
{
    // const değeri bir kez tanımlanır sonradan değiştirilmez
    public class UserRegistrationValidator:IFieldValidator
    {
        const int FirstName_Min_Length = 2;
        const int FirstName_Max_Length = 100;
        const int LastName_Min_Length = 2;
        const int LastName_Max_Length = 100;

        delegate bool EmailExistsDel(string email);

         FieldValidatorDel _fieldValidatorDel = null;

         RequiredValidDel _requiredValiDel = null;
         StringLengthValidDel _stringLengthValidDel = null;
         DateValidDel _dateValidDel = null;
         PatternMatchDel _patternMatchDel = null;
         CompareFieldsValidDel _compareFieldsValidDel = null;

        EmailExistsDel _emailExistsDel = null;

        string[] _fieldArray = null;

        public string[] FieldArray
        {
            get
            {
                if( _fieldArray == null )
                    _fieldArray= new string[Enum.GetValues(typeof(FieldConstant.UserRegistrationField)).Length];
                return _fieldArray;
            }
        }
        public FieldValidatorDel validatorDel => _fieldValidatorDel;

        public string[] fieldArray => throw new NotImplementedException();

        public FieldValidatorDel FieldValidatorDel => throw new NotImplementedException();

        public void InitialiseValidatorDelegates()
        {
            _fieldValidatorDel = new FieldValidatorDel(ValidField);

            _requiredValiDel = CommonFieldValidatorFunctions.RequiredValidDel;
            _stringLengthValidDel=CommonFieldValidatorFunctions.StringLengthValidDel;
            _dateValidDel=CommonFieldValidatorFunctions.DateValidDel;
            _patternMatchDel=CommonFieldValidatorFunctions.PatternMatchDel;
            _compareFieldsValidDel=CommonFieldValidatorFunctions.CompareFieldsValidDel;
        }
        private bool ValidField(int fieldIndex,string fieldValue, string[] fieldArray,out string fieldInvalidMessage) 
        {
            fieldInvalidMessage = "";

            FieldConstant.UserRegistrationField userRegistrationField = (FieldConstant.UserRegistrationField)fieldIndex;

            switch (userRegistrationField)
            {
                case FieldConstant.UserRegistrationField.EmailAdress:
                    fieldInvalidMessage = (!_requiredValiDel(fieldValue)) ? $"You must enter a value for field:" +
                        $"{Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage ==""&&_patternMatchDel
                        (fieldValue,CommonRegularExpressionsValidationPatterns.Email_Address_RegEx_Pattern)) ? $"You must enter a valid email adress:" +
                       $"{Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :fieldInvalidMessage;
                    break;
            }

            return (fieldInvalidMessage == "");
        }
    }

}
