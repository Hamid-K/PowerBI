using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000107 RID: 263
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpActionContextExtensions
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x000112FF File Offset: 0x0000F4FF
		public static ModelMetadataProvider GetMetadataProvider(this HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			return actionContext.ControllerContext.Configuration.Services.GetModelMetadataProvider();
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00011324 File Offset: 0x0000F524
		public static IEnumerable<ModelValidatorProvider> GetValidatorProviders(this HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			return actionContext.ControllerContext.Configuration.Services.GetModelValidatorProviders();
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001134C File Offset: 0x0000F54C
		public static IEnumerable<ModelValidator> GetValidators(this HttpActionContext actionContext, ModelMetadata metadata)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			IModelValidatorCache validatorCache = actionContext.GetValidatorCache();
			return actionContext.GetValidators(metadata, validatorCache);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00011376 File Offset: 0x0000F576
		internal static IEnumerable<ModelValidator> GetValidators(this HttpActionContext actionContext, ModelMetadata metadata, IModelValidatorCache validatorCache)
		{
			if (validatorCache == null)
			{
				return metadata.GetValidators(actionContext.GetValidatorProviders());
			}
			return validatorCache.GetValidators(metadata);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001138F File Offset: 0x0000F58F
		internal static IModelValidatorCache GetValidatorCache(this HttpActionContext actionContext)
		{
			return actionContext.ControllerContext.Configuration.Services.GetModelValidatorCache();
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public static bool TryBindStrongModel<TModel>(this HttpActionContext actionContext, ModelBindingContext parentBindingContext, string propertyName, ModelMetadataProvider metadataProvider, out TModel model)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			ModelBindingContext modelBindingContext = new ModelBindingContext(parentBindingContext)
			{
				ModelMetadata = metadataProvider.GetMetadataForType(null, typeof(TModel)),
				ModelName = ModelBindingHelper.CreatePropertyModelName(parentBindingContext.ModelName, propertyName)
			};
			if (actionContext.Bind(modelBindingContext))
			{
				object model2 = modelBindingContext.Model;
				model = ModelBindingHelper.CastOrDefault<TModel>(model2);
				parentBindingContext.ValidationNode.ChildNodes.Add(modelBindingContext.ValidationNode);
				return true;
			}
			model = default(TModel);
			return false;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00011434 File Offset: 0x0000F634
		public static bool Bind(this HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			Type modelType = bindingContext.ModelType;
			HttpConfiguration config = actionContext.ControllerContext.Configuration;
			IEnumerable<IModelBinder> enumerable = from provider in config.Services.GetModelBinderProviders()
				select provider.GetBinder(config, modelType);
			return actionContext.Bind(bindingContext, enumerable);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00011490 File Offset: 0x0000F690
		public static bool Bind(this HttpActionContext actionContext, ModelBindingContext bindingContext, IEnumerable<IModelBinder> binders)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (bindingContext == null)
			{
				throw Error.ArgumentNull("bindingContext");
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Type modelType = bindingContext.ModelType;
			HttpConfiguration configuration = actionContext.ControllerContext.Configuration;
			ModelBinderProvider modelBinderProvider;
			if (ModelBindingHelper.TryGetProviderFromAttributes(modelType, out modelBinderProvider))
			{
				IModelBinder binder = modelBinderProvider.GetBinder(configuration, modelType);
				if (binder != null)
				{
					return binder.BindModel(actionContext, bindingContext);
				}
			}
			foreach (IModelBinder modelBinder in binders)
			{
				if (modelBinder != null && modelBinder.BindModel(actionContext, bindingContext))
				{
					return true;
				}
			}
			return false;
		}
	}
}
