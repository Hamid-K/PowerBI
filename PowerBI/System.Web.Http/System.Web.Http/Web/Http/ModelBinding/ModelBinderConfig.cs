using System;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000060 RID: 96
	public static class ModelBinderConfig
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00007D3E File Offset: 0x00005F3E
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00007D4E File Offset: 0x00005F4E
		public static string ResourceClassKey
		{
			get
			{
				return ModelBinderConfig._resourceClassKey ?? string.Empty;
			}
			set
			{
				ModelBinderConfig._resourceClassKey = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00007D56 File Offset: 0x00005F56
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00007D75 File Offset: 0x00005F75
		public static ModelBinderErrorMessageProvider TypeConversionErrorMessageProvider
		{
			get
			{
				if (ModelBinderConfig._typeConversionErrorMessageProvider == null)
				{
					ModelBinderConfig._typeConversionErrorMessageProvider = new ModelBinderErrorMessageProvider(ModelBinderConfig.DefaultTypeConversionErrorMessageProvider);
				}
				return ModelBinderConfig._typeConversionErrorMessageProvider;
			}
			set
			{
				ModelBinderConfig._typeConversionErrorMessageProvider = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00007D7D File Offset: 0x00005F7D
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00007D9C File Offset: 0x00005F9C
		public static ModelBinderErrorMessageProvider ValueRequiredErrorMessageProvider
		{
			get
			{
				if (ModelBinderConfig._valueRequiredErrorMessageProvider == null)
				{
					ModelBinderConfig._valueRequiredErrorMessageProvider = new ModelBinderErrorMessageProvider(ModelBinderConfig.DefaultValueRequiredErrorMessageProvider);
				}
				return ModelBinderConfig._valueRequiredErrorMessageProvider;
			}
			set
			{
				ModelBinderConfig._valueRequiredErrorMessageProvider = value;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007DA4 File Offset: 0x00005FA4
		private static string DefaultTypeConversionErrorMessageProvider(HttpActionContext actionContext, ModelMetadata modelMetadata, object incomingValue)
		{
			return ModelBinderConfig.GetResourceCommon(actionContext, modelMetadata, incomingValue, new Func<HttpActionContext, string>(ModelBinderConfig.GetValueInvalidResource));
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007DBA File Offset: 0x00005FBA
		private static string DefaultValueRequiredErrorMessageProvider(HttpActionContext actionContext, ModelMetadata modelMetadata, object incomingValue)
		{
			return ModelBinderConfig.GetResourceCommon(actionContext, modelMetadata, incomingValue, new Func<HttpActionContext, string>(ModelBinderConfig.GetValueRequiredResource));
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007DD0 File Offset: 0x00005FD0
		private static string GetResourceCommon(HttpActionContext actionContext, ModelMetadata modelMetadata, object incomingValue, Func<HttpActionContext, string> resourceAccessor)
		{
			string displayName = modelMetadata.GetDisplayName();
			return Error.Format(resourceAccessor(actionContext), new object[] { incomingValue, displayName });
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00007DFE File Offset: 0x00005FFE
		private static string GetUserResourceString(HttpActionContext actionContext, string resourceName)
		{
			return ModelBinderConfig.GetUserResourceString(actionContext, resourceName, ModelBinderConfig.ResourceClassKey);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000413B File Offset: 0x0000233B
		internal static string GetUserResourceString(HttpActionContext actionContext, string resourceName, string resourceClassKey)
		{
			return null;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00007E0C File Offset: 0x0000600C
		private static string GetValueInvalidResource(HttpActionContext actionContext)
		{
			return ModelBinderConfig.GetUserResourceString(actionContext, "PropertyValueInvalid") ?? SRResources.ModelBinderConfig_ValueInvalid;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00007E22 File Offset: 0x00006022
		private static string GetValueRequiredResource(HttpActionContext actionContext)
		{
			return ModelBinderConfig.GetUserResourceString(actionContext, "PropertyValueRequired") ?? SRResources.ModelBinderConfig_ValueRequired;
		}

		// Token: 0x04000098 RID: 152
		private static string _resourceClassKey;

		// Token: 0x04000099 RID: 153
		private static ModelBinderErrorMessageProvider _typeConversionErrorMessageProvider;

		// Token: 0x0400009A RID: 154
		private static ModelBinderErrorMessageProvider _valueRequiredErrorMessageProvider;
	}
}
