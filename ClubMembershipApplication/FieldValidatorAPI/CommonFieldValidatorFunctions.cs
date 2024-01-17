using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FieldValidatorAPI
{
    public delegate bool RequiredValidDel(string fieldVal);
    public delegate bool StringLengthValidDel(string fieldVal, int min, int max);
    public delegate bool DateValidDel(string fieldVal, out DateTime valiDateTime);
    public delegate bool PatternMatchDel(string fieldVal, string pattern);
    public delegate bool CompareFieldsValidDel(string fieldVal, string fieldValCompare);
    public class CommonFieldValidatorFunctions
    {
        private static RequiredValidDel _requiredValiDel = null;
        private static StringLengthValidDel _stringLengthValidDel = null;
        private static DateValidDel _dateValidDel = null;
        private static PatternMatchDel _patternMatchDel = null;
        private static CompareFieldsValidDel _compareFieldsValidDel = null;
        public static RequiredValidDel RequiredValidDel
        {
            get
            {
                if (_requiredValiDel == null)
                    _requiredValiDel = new RequiredValidDel(RequiredValidDel);
                return _requiredValiDel;
            }
        }
        public static StringLengthValidDel StringLengthValidDel
        {
            get
            {
                if (_stringLengthValidDel == null)
                    _stringLengthValidDel = new StringLengthValidDel(StringLengthValidDel);
                return _stringLengthValidDel;
            }
        }
        public static DateValidDel DateValidDel
        {
            get
            {
                if (_dateValidDel == null)
                    _dateValidDel = new DateValidDel(DateValidDel);
                return _dateValidDel;
            }
        }
        public static PatternMatchDel PatternMatchDel
        {
            get
            {
                if (_patternMatchDel == null)
                    _patternMatchDel = new PatternMatchDel(PatternMatchDel);
                return _patternMatchDel;
            }
        }
        public static CompareFieldsValidDel CompareFieldsValidDel
        {
            get
            {
                if (_compareFieldsValidDel == null)
                    _compareFieldsValidDel = new CompareFieldsValidDel(CompareFieldsValidDel);
                return _compareFieldsValidDel;
            }
        }

        private static bool RequiredFieldValid(string fieldVal)
        { 
            if(!string.IsNullOrEmpty(fieldVal)) 
               return true; 
            return false;
        }

        private static bool StringFieldLengthValid(string fieldVal,int min,int max) 
        {
            if(fieldVal.Length>=min && fieldVal.Length<=max)
                return true;
            return false;
        }
        private static bool DateFieldValid(string dateTime,out DateTime valiDateTime)
        {
            if(DateTime.TryParse(dateTime,out valiDateTime))
                return true;
            return false;
        }
        private static bool FieldPatternValid(string fieldVal,string regularExpressionPattern)
        {
            Regex regex = new Regex (regularExpressionPattern);

            if(regex.IsMatch(fieldVal))
                return true;
            return false;
        }
        private static bool FieldComparisonValid(string field1,string field2)
        {
            if(field1.Equals(field2)) 
                return true;
            return false;
        }


    }
}
