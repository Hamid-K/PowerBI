using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Properties;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000052 RID: 82
	public static class FormDataCollectionExtensions
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00006C9C File Offset: 0x00004E9C
		internal static string NormalizeJQueryToMvc(string key)
		{
			if (key == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = null;
			int num = 0;
			for (;;)
			{
				int num2 = key.IndexOf('[', num);
				if (num2 < 0)
				{
					break;
				}
				stringBuilder = stringBuilder ?? new StringBuilder();
				stringBuilder.Append(key, num, num2 - num);
				int num3 = key.IndexOf(']', num2);
				if (num3 == -1)
				{
					goto Block_6;
				}
				if (num3 != num2 + 1)
				{
					if (char.IsDigit(key[num2 + 1]))
					{
						stringBuilder.Append(key, num2, num3 - num2 + 1);
					}
					else
					{
						stringBuilder.Append('.');
						stringBuilder.Append(key, num2 + 1, num3 - num2 - 1);
					}
				}
				num = num3 + 1;
				if (num >= key.Length)
				{
					goto IL_00CB;
				}
			}
			if (num == 0)
			{
				return key;
			}
			stringBuilder = stringBuilder ?? new StringBuilder();
			stringBuilder.Append(key, num, key.Length - num);
			goto IL_00CB;
			Block_6:
			throw Error.Argument("key", SRResources.JQuerySyntaxMissingClosingBracket, new object[0]);
			IL_00CB:
			return stringBuilder.ToString();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006D7A File Offset: 0x00004F7A
		internal static IEnumerable<KeyValuePair<string, string>> GetJQueryNameValuePairs(this FormDataCollection formData)
		{
			if (formData == null)
			{
				throw Error.ArgumentNull("formData");
			}
			int count = 0;
			foreach (KeyValuePair<string, string> keyValuePair in formData)
			{
				FormDataCollectionExtensions.ThrowIfMaxHttpCollectionKeysExceeded(count);
				string text = FormDataCollectionExtensions.NormalizeJQueryToMvc(keyValuePair.Key);
				string text2 = keyValuePair.Value ?? string.Empty;
				yield return new KeyValuePair<string, string>(text, text2);
				int num = count;
				count = num + 1;
			}
			IEnumerator<KeyValuePair<string, string>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006D8A File Offset: 0x00004F8A
		private static void ThrowIfMaxHttpCollectionKeysExceeded(int count)
		{
			if (count >= MediaTypeFormatter.MaxHttpCollectionKeys)
			{
				throw Error.InvalidOperation(SRResources.MaxHttpCollectionKeyLimitReached, new object[]
				{
					MediaTypeFormatter.MaxHttpCollectionKeys,
					typeof(MediaTypeFormatter)
				});
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006DBF File Offset: 0x00004FBF
		internal static IValueProvider GetJQueryValueProvider(this FormDataCollection formData)
		{
			if (formData == null)
			{
				throw Error.ArgumentNull("formData");
			}
			return new NameValuePairsValueProvider(formData.GetJQueryNameValuePairs(), CultureInfo.InvariantCulture);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006DDF File Offset: 0x00004FDF
		public static T ReadAs<T>(this FormDataCollection formData)
		{
			return (T)((object)formData.ReadAs(typeof(T)));
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006DF6 File Offset: 0x00004FF6
		public static T ReadAs<T>(this FormDataCollection formData, HttpActionContext actionContext)
		{
			return (T)((object)formData.ReadAs(typeof(T), string.Empty, actionContext));
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006E13 File Offset: 0x00005013
		public static object ReadAs(this FormDataCollection formData, Type type)
		{
			return formData.ReadAs(type, string.Empty, null, null);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006E23 File Offset: 0x00005023
		public static object ReadAs(this FormDataCollection formData, Type type, HttpActionContext actionContext)
		{
			return formData.ReadAs(type, string.Empty, actionContext);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006E32 File Offset: 0x00005032
		public static T ReadAs<T>(this FormDataCollection formData, string modelName, IRequiredMemberSelector requiredMemberSelector, IFormatterLogger formatterLogger)
		{
			return (T)((object)formData.ReadAs(typeof(T), modelName, requiredMemberSelector, formatterLogger));
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006E4C File Offset: 0x0000504C
		public static T ReadAs<T>(this FormDataCollection formData, string modelName, HttpActionContext actionContext)
		{
			return (T)((object)formData.ReadAs(typeof(T), modelName, actionContext));
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00006E65 File Offset: 0x00005065
		public static object ReadAs(this FormDataCollection formData, Type type, string modelName, HttpActionContext actionContext)
		{
			if (formData == null)
			{
				throw Error.ArgumentNull("formData");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			return formData.ReadAsInternal(type, modelName, actionContext);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006EA0 File Offset: 0x000050A0
		public static object ReadAs(this FormDataCollection formData, Type type, string modelName, IRequiredMemberSelector requiredMemberSelector, IFormatterLogger formatterLogger)
		{
			return formData.ReadAs(type, modelName, requiredMemberSelector, formatterLogger, null);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006EB0 File Offset: 0x000050B0
		public static object ReadAs(this FormDataCollection formData, Type type, string modelName, IRequiredMemberSelector requiredMemberSelector, IFormatterLogger formatterLogger, HttpConfiguration config)
		{
			if (formData == null)
			{
				throw Error.ArgumentNull("formData");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			object obj = null;
			HttpActionContext httpActionContext = null;
			if (requiredMemberSelector != null && formatterLogger != null)
			{
				using (HttpConfiguration httpConfiguration = new HttpConfiguration())
				{
					config = ((config == null) ? httpConfiguration : config);
					httpConfiguration.Services = new FormDataCollectionExtensions.ServicesContainerWrapper(config, new RequiredMemberModelValidatorProvider(requiredMemberSelector));
					httpActionContext = FormDataCollectionExtensions.CreateActionContextForModelBinding(httpConfiguration);
					obj = formData.ReadAs(type, modelName, httpActionContext);
					goto IL_00B1;
				}
			}
			if (config == null)
			{
				HttpConfiguration httpConfiguration2;
				config = (httpConfiguration2 = new HttpConfiguration());
				try
				{
					httpActionContext = FormDataCollectionExtensions.CreateActionContextForModelBinding(config);
					obj = formData.ReadAs(type, modelName, httpActionContext);
					goto IL_00B1;
				}
				finally
				{
					if (httpConfiguration2 != null)
					{
						((IDisposable)httpConfiguration2).Dispose();
					}
				}
			}
			httpActionContext = FormDataCollectionExtensions.CreateActionContextForModelBinding(config);
			obj = formData.ReadAs(type, modelName, httpActionContext);
			IL_00B1:
			if (formatterLogger != null)
			{
				foreach (KeyValuePair<string, ModelState> keyValuePair in httpActionContext.ModelState)
				{
					foreach (ModelError modelError in keyValuePair.Value.Errors)
					{
						if (modelError.Exception != null)
						{
							formatterLogger.LogError(keyValuePair.Key, modelError.Exception);
						}
						else
						{
							formatterLogger.LogError(keyValuePair.Key, modelError.ErrorMessage);
						}
					}
				}
			}
			return obj;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007044 File Offset: 0x00005244
		private static object ReadAsInternal(this FormDataCollection formData, Type type, string modelName, HttpActionContext actionContext)
		{
			IValueProvider jqueryValueProvider = formData.GetJQueryValueProvider();
			ModelBindingContext modelBindingContext = FormDataCollectionExtensions.CreateModelBindingContext(actionContext, modelName ?? string.Empty, type, jqueryValueProvider);
			if (FormDataCollectionExtensions.CreateModelBindingProvider(actionContext).GetBinder(actionContext.ControllerContext.Configuration, type).BindModel(actionContext, modelBindingContext))
			{
				return modelBindingContext.Model;
			}
			return MediaTypeFormatter.GetDefaultValueForType(type);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007098 File Offset: 0x00005298
		private static ModelBinderProvider CreateModelBindingProvider(HttpActionContext actionContext)
		{
			return new CompositeModelBinderProvider(actionContext.ControllerContext.Configuration.Services.GetModelBinderProviders());
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000070B4 File Offset: 0x000052B4
		private static ModelBindingContext CreateModelBindingContext(HttpActionContext actionContext, string modelName, Type type, IValueProvider vp)
		{
			ModelMetadataProvider modelMetadataProvider = actionContext.ControllerContext.Configuration.Services.GetModelMetadataProvider();
			return new ModelBindingContext
			{
				ModelName = modelName,
				FallbackToEmptyPrefix = false,
				ModelMetadata = modelMetadataProvider.GetMetadataForType(null, type),
				ModelState = actionContext.ModelState,
				ValueProvider = vp
			};
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000710C File Offset: 0x0000530C
		private static HttpActionContext CreateActionContextForModelBinding(HttpConfiguration config)
		{
			HttpControllerContext httpControllerContext = new HttpControllerContext
			{
				Configuration = config
			};
			httpControllerContext.ControllerDescriptor = new HttpControllerDescriptor(config);
			return new HttpActionContext
			{
				ControllerContext = httpControllerContext
			};
		}

		// Token: 0x020001A7 RID: 423
		internal class ServicesContainerWrapper : ServicesContainer
		{
			// Token: 0x06000A89 RID: 2697 RVA: 0x0001B53A File Offset: 0x0001973A
			public ServicesContainerWrapper(HttpConfiguration originalConfig, ModelValidatorProvider requiredMemberModelValidatorProvider)
			{
				this._originalConfig = originalConfig;
				this._requiredMemberModelValidatorProvider = requiredMemberModelValidatorProvider;
			}

			// Token: 0x06000A8A RID: 2698 RVA: 0x0001B550 File Offset: 0x00019750
			public override object GetService(Type serviceType)
			{
				if (serviceType == typeof(IModelValidatorCache))
				{
					return new ModelValidatorCache(new Lazy<IEnumerable<ModelValidatorProvider>>(() => this.GetServices<ModelValidatorProvider>()));
				}
				if (serviceType == typeof(ModelValidatorProvider))
				{
					return this._requiredMemberModelValidatorProvider;
				}
				return this._originalConfig.Services.GetService(serviceType);
			}

			// Token: 0x06000A8B RID: 2699 RVA: 0x0001B5B0 File Offset: 0x000197B0
			public override IEnumerable<object> GetServices(Type serviceType)
			{
				if (serviceType == typeof(ModelValidatorProvider))
				{
					return new ModelValidatorProvider[] { this._requiredMemberModelValidatorProvider };
				}
				return this._originalConfig.Services.GetServices(serviceType);
			}

			// Token: 0x06000A8C RID: 2700 RVA: 0x0001B5E5 File Offset: 0x000197E5
			protected override List<object> GetServiceInstances(Type serviceType)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A8D RID: 2701 RVA: 0x0001B5EC File Offset: 0x000197EC
			public override bool IsSingleService(Type serviceType)
			{
				return this._originalConfig.Services.IsSingleService(serviceType);
			}

			// Token: 0x06000A8E RID: 2702 RVA: 0x0001B5FF File Offset: 0x000197FF
			protected override void ClearSingle(Type serviceType)
			{
				this._originalConfig.Services.Clear(serviceType);
			}

			// Token: 0x06000A8F RID: 2703 RVA: 0x0001B612 File Offset: 0x00019812
			protected override void ReplaceSingle(Type serviceType, object service)
			{
				this._originalConfig.Services.Replace(serviceType, service);
			}

			// Token: 0x04000311 RID: 785
			private HttpConfiguration _originalConfig;

			// Token: 0x04000312 RID: 786
			private ModelValidatorProvider _requiredMemberModelValidatorProvider;
		}
	}
}
