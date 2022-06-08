using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Trips_and_Travel.CustomValidation
{
    public class ValidDateAttribute : ValidationAttribute

    {
        public ValidDateAttribute() : base("{0} Invalid Date.")
        {
        }

        public override bool IsValid(object value)
        {
            DateTime date = Convert.ToDateTime(value);
            if (date <= DateTime.Now)
                return true;
            else
                return false;
        }
    }
}