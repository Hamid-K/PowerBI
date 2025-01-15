using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;
using System.Web.Http.Metadata;
using System.Web.Http.Metadata.Providers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Properties;
using System.Web.Http.Tracing;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace System.Web.Http.Services
{
	// Token: 0x020000A6 RID: 166
	public class DefaultServices : ServicesContainer
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000B71E File Offset: 0x0000991E
		protected DefaultServices()
		{
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000B752 File Offset: 0x00009952
		private void SetSingle<T>(T instance) where T : class
		{
			this._defaultServicesSingle[typeof(T)] = instance;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000B770 File Offset: 0x00009970
		private void SetMultiple<T>(params T[] instances) where T : class
		{
			this._defaultServicesMulti[typeof(T)] = new List<object>(instances);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000B79C File Offset: 0x0000999C
		public DefaultServices(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._configuration = configuration;
			this.SetSingle<IActionValueBinder>(new DefaultActionValueBinder());
			this.SetSingle<IApiExplorer>(new ApiExplorer(configuration));
			this.SetSingle<IAssembliesResolver>(new DefaultAssembliesResolver());
			this.SetSingle<IBodyModelValidator>(new DefaultBodyModelValidator());
			this.SetSingle<IContentNegotiator>(new DefaultContentNegotiator());
			this.SetSingle<IDocumentationProvider>(null);
			this.SetMultiple<IFilterProvider>(new IFilterProvider[]
			{
				new ConfigurationFilterProvider(),
				new ActionDescriptorFilterProvider()
			});
			this.SetSingle<IHostBufferPolicySelector>(null);
			this.SetSingle<IHttpActionInvoker>(new ApiControllerActionInvoker());
			this.SetSingle<IHttpActionSelector>(new ApiControllerActionSelector());
			this.SetSingle<IHttpControllerActivator>(new DefaultHttpControllerActivator());
			this.SetSingle<IHttpControllerSelector>(new DefaultHttpControllerSelector(configuration));
			this.SetSingle<IHttpControllerTypeResolver>(new DefaultHttpControllerTypeResolver());
			this.SetSingle<ITraceManager>(new TraceManager());
			this.SetSingle<ITraceWriter>(null);
			this.SetMultiple<ModelBinderProvider>(new ModelBinderProvider[]
			{
				new TypeConverterModelBinderProvider(),
				new TypeMatchModelBinderProvider(),
				new KeyValuePairModelBinderProvider(),
				new ComplexModelDtoModelBinderProvider(),
				new ArrayModelBinderProvider(),
				new DictionaryModelBinderProvider(),
				new CollectionModelBinderProvider(),
				new MutableObjectModelBinderProvider()
			});
			this.SetSingle<ModelMetadataProvider>(new DataAnnotationsModelMetadataProvider());
			this.SetMultiple<ModelValidatorProvider>(new ModelValidatorProvider[]
			{
				new DataAnnotationsModelValidatorProvider(),
				new DataMemberModelValidatorProvider()
			});
			this.SetMultiple<ValueProviderFactory>(new ValueProviderFactory[]
			{
				new QueryStringValueProviderFactory(),
				new RouteDataValueProviderFactory()
			});
			ModelValidatorCache modelValidatorCache = new ModelValidatorCache(new Lazy<IEnumerable<ModelValidatorProvider>>(() => this.GetModelValidatorProviders()));
			this.SetSingle<IModelValidatorCache>(modelValidatorCache);
			this.SetSingle<IExceptionHandler>(new DefaultExceptionHandler());
			this.SetMultiple<IExceptionLogger>(new IExceptionLogger[0]);
			this._serviceTypesSingle = new HashSet<Type>(this._defaultServicesSingle.Keys);
			this._serviceTypesMulti = new HashSet<Type>(this._defaultServicesMulti.Keys);
			this.ResetCache();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000B992 File Offset: 0x00009B92
		public override bool IsSingleService(Type serviceType)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			return this._serviceTypesSingle.Contains(serviceType);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		public override object GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (this._lastKnownDependencyResolver != this._configuration.DependencyResolver)
			{
				this.ResetCache();
			}
			object obj;
			if (this._cacheSingle.TryGetValue(serviceType, out obj))
			{
				return obj;
			}
			if (!this._serviceTypesSingle.Contains(serviceType))
			{
				throw Error.Argument("serviceType", SRResources.DefaultServices_InvalidServiceType, new object[] { serviceType.Name });
			}
			object service = this._lastKnownDependencyResolver.GetService(serviceType);
			if (!this._cacheSingle.TryGetValue(serviceType, out obj))
			{
				obj = service ?? this._defaultServicesSingle[serviceType];
				this._cacheSingle.TryAdd(serviceType, obj);
			}
			return obj;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000BA6C File Offset: 0x00009C6C
		public override IEnumerable<object> GetServices(Type serviceType)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (this._lastKnownDependencyResolver != this._configuration.DependencyResolver)
			{
				this.ResetCache();
			}
			object[] array;
			if (this._cacheMulti.TryGetValue(serviceType, out array))
			{
				return array;
			}
			if (!this._serviceTypesMulti.Contains(serviceType))
			{
				throw Error.Argument("serviceType", SRResources.DefaultServices_InvalidServiceType, new object[] { serviceType.Name });
			}
			IEnumerable<object> services = this._lastKnownDependencyResolver.GetServices(serviceType);
			if (!this._cacheMulti.TryGetValue(serviceType, out array))
			{
				array = services.Where((object s) => s != null).Concat(this._defaultServicesMulti[serviceType]).ToArray<object>();
				this._cacheMulti.TryAdd(serviceType, array);
			}
			return array;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000BB50 File Offset: 0x00009D50
		protected override List<object> GetServiceInstances(Type serviceType)
		{
			List<object> list;
			if (!this._defaultServicesMulti.TryGetValue(serviceType, out list))
			{
				throw Error.Argument("serviceType", SRResources.DefaultServices_InvalidServiceType, new object[] { serviceType.Name });
			}
			return list;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000BB8D File Offset: 0x00009D8D
		protected override void ClearSingle(Type serviceType)
		{
			this._defaultServicesSingle[serviceType] = null;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000BB9C File Offset: 0x00009D9C
		protected override void ReplaceSingle(Type serviceType, object service)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			this._defaultServicesSingle[serviceType] = service;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000BBBF File Offset: 0x00009DBF
		private void ResetCache()
		{
			this._cacheSingle = new ConcurrentDictionary<Type, object>();
			this._cacheMulti = new ConcurrentDictionary<Type, object[]>();
			this._lastKnownDependencyResolver = this._configuration.DependencyResolver;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		protected override void ResetCache(Type serviceType)
		{
			object obj;
			this._cacheSingle.TryRemove(serviceType, out obj);
			object[] array;
			this._cacheMulti.TryRemove(serviceType, out array);
		}

		// Token: 0x040000E6 RID: 230
		private ConcurrentDictionary<Type, object[]> _cacheMulti = new ConcurrentDictionary<Type, object[]>();

		// Token: 0x040000E7 RID: 231
		private ConcurrentDictionary<Type, object> _cacheSingle = new ConcurrentDictionary<Type, object>();

		// Token: 0x040000E8 RID: 232
		private readonly HttpConfiguration _configuration;

		// Token: 0x040000E9 RID: 233
		private readonly Dictionary<Type, object> _defaultServicesSingle = new Dictionary<Type, object>();

		// Token: 0x040000EA RID: 234
		private readonly Dictionary<Type, List<object>> _defaultServicesMulti = new Dictionary<Type, List<object>>();

		// Token: 0x040000EB RID: 235
		private IDependencyResolver _lastKnownDependencyResolver;

		// Token: 0x040000EC RID: 236
		private readonly HashSet<Type> _serviceTypesSingle;

		// Token: 0x040000ED RID: 237
		private readonly HashSet<Type> _serviceTypesMulti;
	}
}
