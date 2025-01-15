using System;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;
using System.Web.Http.Validation;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200007A RID: 122
	public sealed class TypeMatchModelBinder : IModelBinder
	{
		// Token: 0x06000327 RID: 807 RVA: 0x00009204 File Offset: 0x00007404
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ValueProviderResult compatibleValueProviderResult = TypeMatchModelBinder.GetCompatibleValueProviderResult(bindingContext);
			if (compatibleValueProviderResult == null)
			{
				return false;
			}
			bindingContext.ModelState.SetModelValue(bindingContext.ModelName, compatibleValueProviderResult);
			object rawValue = compatibleValueProviderResult.RawValue;
			ModelBindingHelper.ReplaceEmptyStringWithNull(bindingContext.ModelMetadata, ref rawValue);
			bindingContext.Model = rawValue;
			if (bindingContext.ModelMetadata.IsComplexType)
			{
				HttpControllerContext controllerContext = actionContext.ControllerContext;
				if (controllerContext == null)
				{
					throw Error.Argument("actionContext", SRResources.TypePropertyMustNotBeNull, new object[]
					{
						typeof(HttpActionContext).Name,
						"ControllerContext"
					});
				}
				HttpConfiguration configuration = controllerContext.Configuration;
				if (configuration == null)
				{
					throw Error.Argument("actionContext", SRResources.TypePropertyMustNotBeNull, new object[]
					{
						typeof(HttpControllerContext).Name,
						"Configuration"
					});
				}
				ServicesContainer services = configuration.Services;
				IBodyModelValidator bodyModelValidator = services.GetBodyModelValidator();
				ModelMetadataProvider modelMetadataProvider = services.GetModelMetadataProvider();
				if (bodyModelValidator != null && modelMetadataProvider != null)
				{
					bodyModelValidator.Validate(rawValue, bindingContext.ModelType, modelMetadataProvider, actionContext, bindingContext.ModelName);
				}
			}
			return true;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00009304 File Offset: 0x00007504
		internal static ValueProviderResult GetCompatibleValueProviderResult(ModelBindingContext bindingContext)
		{
			ModelBindingHelper.ValidateBindingContext(bindingContext);
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (value == null)
			{
				return null;
			}
			if (!TypeHelper.IsCompatibleObject(bindingContext.ModelType, value.RawValue))
			{
				return null;
			}
			return value;
		}
	}
}
