using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotEduWeb.Tools
{
    public class DecimalModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            string attemptedValue = valueProviderResult.AttemptedValue;

            if(valueProviderResult != null)
            {
                if (attemptedValue.Contains(","))
                {
                    return Convert.ToDecimal(attemptedValue, new CultureInfo("fr-FR"));
                }
                else if (attemptedValue.Contains("."))
                {
                    return Convert.ToDecimal(attemptedValue, new CultureInfo("us-US"));
                }
            }
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}