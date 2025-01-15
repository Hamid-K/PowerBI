using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter.Deserialization;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000183 RID: 387
	public class ODataModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06000CD1 RID: 3281 RVA: 0x000326A0 File Offset: 0x000308A0
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			return new ODataModelBinderProvider.ODataModelBinder();
		}

		// Token: 0x02000344 RID: 836
		internal class ODataModelBinder : IModelBinder
		{
			// Token: 0x060014AC RID: 5292 RVA: 0x000449B8 File Offset: 0x00042BB8
			public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
			{
				if (bindingContext == null)
				{
					throw Error.ArgumentNull("bindingContext");
				}
				if (bindingContext.ModelMetadata == null)
				{
					throw Error.Argument("bindingContext", SRResources.ModelBinderUtil_ModelMetadataCannotBeNull, new object[0]);
				}
				string text = "DF908045-6922-46A0-82F2-2F6E7F43D1B1_" + bindingContext.ModelName;
				ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(text);
				if (valueProviderResult == null)
				{
					valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
					if (valueProviderResult == null)
					{
						return false;
					}
				}
				bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
				bool flag;
				try
				{
					ODataParameterValue odataParameterValue = valueProviderResult.RawValue as ODataParameterValue;
					if (odataParameterValue != null)
					{
						bindingContext.Model = ODataModelBinderProvider.ODataModelBinder.ConvertTo(odataParameterValue, actionContext, bindingContext, actionContext.Request.GetRequestContainer());
						flag = true;
					}
					else
					{
						string text2 = valueProviderResult.RawValue as string;
						if (text2 != null)
						{
							bindingContext.Model = ODataModelBinderConverter.ConvertTo(text2, bindingContext.ModelType);
							flag = true;
						}
						else
						{
							flag = false;
						}
					}
				}
				catch (ODataException ex)
				{
					bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex.Message);
					flag = false;
				}
				catch (ValidationException ex2)
				{
					bindingContext.ModelState.AddModelError(bindingContext.ModelName, Error.Format(SRResources.ValueIsInvalid, new object[] { valueProviderResult.RawValue, ex2.Message }));
					flag = false;
				}
				catch (FormatException ex3)
				{
					bindingContext.ModelState.AddModelError(bindingContext.ModelName, Error.Format(SRResources.ValueIsInvalid, new object[] { valueProviderResult.RawValue, ex3.Message }));
					flag = false;
				}
				catch (Exception ex4)
				{
					bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex4);
					flag = false;
				}
				return flag;
			}

			// Token: 0x060014AD RID: 5293 RVA: 0x00044B84 File Offset: 0x00042D84
			internal static object ConvertTo(ODataParameterValue parameterValue, HttpActionContext actionContext, ModelBindingContext bindingContext, IServiceProvider requestContainer)
			{
				object value = parameterValue.Value;
				if (value == null || value is ODataNullValue)
				{
					return null;
				}
				IEdmTypeReference edmType = parameterValue.EdmType;
				ODataDeserializerContext odataDeserializerContext = ODataModelBinderProvider.ODataModelBinder.BuildDeserializerContext(actionContext, bindingContext, edmType);
				return ODataModelBinderConverter.Convert(value, edmType, bindingContext.ModelType, bindingContext.ModelName, odataDeserializerContext, requestContainer);
			}

			// Token: 0x060014AE RID: 5294 RVA: 0x00044BCC File Offset: 0x00042DCC
			internal static ODataDeserializerContext BuildDeserializerContext(HttpActionContext actionContext, ModelBindingContext bindingContext, IEdmTypeReference edmTypeReference)
			{
				HttpRequestMessage request = actionContext.Request;
				ODataPath path = request.ODataProperties().Path;
				IEdmModel model = request.GetModel();
				return new ODataDeserializerContext
				{
					Path = path,
					Model = model,
					Request = request,
					ResourceType = bindingContext.ModelType,
					ResourceEdmType = edmTypeReference
				};
			}
		}
	}
}
