using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Properties;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x0200005C RID: 92
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public class ModelBinderAttribute : ParameterBindingAttribute
	{
		// Token: 0x06000275 RID: 629 RVA: 0x000079C6 File Offset: 0x00005BC6
		public ModelBinderAttribute()
			: this(null)
		{
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000079CF File Offset: 0x00005BCF
		public ModelBinderAttribute(Type binderType)
		{
			this.BinderType = binderType;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000079DE File Offset: 0x00005BDE
		// (set) Token: 0x06000278 RID: 632 RVA: 0x000079E6 File Offset: 0x00005BE6
		public Type BinderType { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000079EF File Offset: 0x00005BEF
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000079F7 File Offset: 0x00005BF7
		public string Name { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00007A00 File Offset: 0x00005C00
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00007A08 File Offset: 0x00005C08
		public bool SuppressPrefixCheck { get; set; }

		// Token: 0x0600027D RID: 637 RVA: 0x00007A14 File Offset: 0x00005C14
		public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
		{
			HttpConfiguration configuration = parameter.Configuration;
			IModelBinder modelBinder = this.GetModelBinder(configuration, parameter.ParameterType);
			IEnumerable<ValueProviderFactory> valueProviderFactories = this.GetValueProviderFactories(configuration);
			return new ModelBinderParameterBinding(parameter, modelBinder, valueProviderFactories);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00007A48 File Offset: 0x00005C48
		public ModelBinderProvider GetModelBinderProvider(HttpConfiguration configuration)
		{
			if (this.BinderType != null)
			{
				object orInstantiate = ModelBinderAttribute.GetOrInstantiate(configuration, this.BinderType);
				if (orInstantiate != null)
				{
					ModelBinderAttribute.VerifyBinderType(orInstantiate.GetType());
					return (ModelBinderProvider)orInstantiate;
				}
			}
			IEnumerable<ModelBinderProvider> modelBinderProviders = configuration.Services.GetModelBinderProviders();
			if (modelBinderProviders.Count<ModelBinderProvider>() == 1)
			{
				return modelBinderProviders.First<ModelBinderProvider>();
			}
			return new CompositeModelBinderProvider(modelBinderProviders);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00007AA8 File Offset: 0x00005CA8
		public IModelBinder GetModelBinder(HttpConfiguration configuration, Type modelType)
		{
			if (this.BinderType == null)
			{
				return this.GetModelBinderProvider(configuration).GetBinder(configuration, modelType);
			}
			object orInstantiate = ModelBinderAttribute.GetOrInstantiate(configuration, this.BinderType);
			IModelBinder modelBinder = orInstantiate as IModelBinder;
			if (modelBinder != null)
			{
				return modelBinder;
			}
			ModelBinderProvider modelBinderProvider = orInstantiate as ModelBinderProvider;
			if (modelBinderProvider != null)
			{
				return modelBinderProvider.GetBinder(configuration, modelType);
			}
			Type typeFromHandle = typeof(IModelBinder);
			throw Error.InvalidOperation(SRResources.ValueProviderFactory_Cannot_Create, new object[]
			{
				typeFromHandle.Name,
				orInstantiate.GetType().Name,
				typeFromHandle.Name
			});
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00007B39 File Offset: 0x00005D39
		public virtual IEnumerable<ValueProviderFactory> GetValueProviderFactories(HttpConfiguration configuration)
		{
			return configuration.Services.GetValueProviderFactories();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007B48 File Offset: 0x00005D48
		private static void VerifyBinderType(Type attemptedType)
		{
			Type typeFromHandle = typeof(ModelBinderProvider);
			if (!typeFromHandle.IsAssignableFrom(attemptedType))
			{
				throw Error.InvalidOperation(SRResources.ValueProviderFactory_Cannot_Create, new object[] { typeFromHandle.Name, attemptedType.Name, typeFromHandle.Name });
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007B98 File Offset: 0x00005D98
		private static object GetOrInstantiate(HttpConfiguration configuration, Type type)
		{
			object service = configuration.DependencyResolver.GetService(type);
			if (service != null)
			{
				return service;
			}
			return Activator.CreateInstance(type);
		}
	}
}
