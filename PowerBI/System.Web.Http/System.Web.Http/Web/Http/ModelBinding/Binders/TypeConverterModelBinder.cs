using System;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000078 RID: 120
	public sealed class TypeConverterModelBinder : IModelBinder
	{
		// Token: 0x06000322 RID: 802 RVA: 0x00009100 File Offset: 0x00007300
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ModelBindingHelper.ValidateBindingContext(bindingContext);
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (value == null)
			{
				return false;
			}
			bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);
			object obj;
			try
			{
				obj = value.ConvertTo(bindingContext.ModelType);
			}
			catch (Exception ex)
			{
				if (TypeConverterModelBinder.IsFormatException(ex))
				{
					string text = ModelBinderConfig.TypeConversionErrorMessageProvider(actionContext, bindingContext.ModelMetadata, value.AttemptedValue);
					if (text != null)
					{
						bindingContext.ModelState.AddModelError(bindingContext.ModelName, text);
					}
				}
				else
				{
					bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex);
				}
				return false;
			}
			ModelBindingHelper.ReplaceEmptyStringWithNull(bindingContext.ModelMetadata, ref obj);
			bindingContext.Model = obj;
			return true;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000091C4 File Offset: 0x000073C4
		private static bool IsFormatException(Exception ex)
		{
			while (ex != null)
			{
				if (ex is FormatException)
				{
					return true;
				}
				ex = ex.InnerException;
			}
			return false;
		}
	}
}
