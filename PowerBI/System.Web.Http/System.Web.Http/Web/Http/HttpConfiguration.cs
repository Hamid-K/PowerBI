using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Services;
using System.Web.Http.Validation;

namespace System.Web.Http
{
	// Token: 0x02000037 RID: 55
	public class HttpConfiguration : IDisposable
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00004EA0 File Offset: 0x000030A0
		public HttpConfiguration()
			: this(new HttpRouteCollection(string.Empty))
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004EB4 File Offset: 0x000030B4
		public HttpConfiguration(HttpRouteCollection routes)
		{
			this._properties = new ConcurrentDictionary<object, object>();
			this._messageHandlers = new Collection<DelegatingHandler>();
			this._filters = new HttpFilterCollection();
			this._dependencyResolver = EmptyResolver.Instance;
			this._initializer = new Action<HttpConfiguration>(HttpConfiguration.DefaultInitializer);
			base..ctor();
			if (routes == null)
			{
				throw Error.ArgumentNull("routes");
			}
			this._routes = routes;
			this._formatters = HttpConfiguration.DefaultFormatters(this);
			this.Services = new DefaultServices(this);
			this.ParameterBindingRules = DefaultActionValueBinder.GetDefaultParameterBinders();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004F40 File Offset: 0x00003140
		private HttpConfiguration(HttpConfiguration configuration, HttpControllerSettings settings)
		{
			this._properties = new ConcurrentDictionary<object, object>();
			this._messageHandlers = new Collection<DelegatingHandler>();
			this._filters = new HttpFilterCollection();
			this._dependencyResolver = EmptyResolver.Instance;
			this._initializer = new Action<HttpConfiguration>(HttpConfiguration.DefaultInitializer);
			base..ctor();
			this._routes = configuration.Routes;
			this._filters = configuration.Filters;
			this._messageHandlers = configuration.MessageHandlers;
			this._properties = configuration.Properties;
			this._dependencyResolver = configuration.DependencyResolver;
			this.IncludeErrorDetailPolicy = configuration.IncludeErrorDetailPolicy;
			this.Services = (settings.IsServiceCollectionInitialized ? settings.Services : configuration.Services);
			this._formatters = (settings.IsFormatterCollectionInitialized ? settings.Formatters : configuration.Formatters);
			this.ParameterBindingRules = (settings.IsParameterBindingRuleCollectionInitialized ? settings.ParameterBindingRules : configuration.ParameterBindingRules);
			this.Initializer = configuration.Initializer;
			if (settings.IsServiceCollectionInitialized && !settings.Services.GetModelValidatorProviders().SequenceEqual(configuration.Services.GetModelValidatorProviders()))
			{
				ModelValidatorCache modelValidatorCache = new ModelValidatorCache(new Lazy<IEnumerable<ModelValidatorProvider>>(() => this.Services.GetModelValidatorProviders()));
				settings.Services.Replace(typeof(IModelValidatorCache), modelValidatorCache);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000508B File Offset: 0x0000328B
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00005093 File Offset: 0x00003293
		public Action<HttpConfiguration> Initializer
		{
			get
			{
				return this._initializer;
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._initializer = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000050AA File Offset: 0x000032AA
		public HttpFilterCollection Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000050B2 File Offset: 0x000032B2
		public Collection<DelegatingHandler> MessageHandlers
		{
			get
			{
				return this._messageHandlers;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000050BA File Offset: 0x000032BA
		public HttpRouteCollection Routes
		{
			get
			{
				return this._routes;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000050C2 File Offset: 0x000032C2
		public ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000050CA File Offset: 0x000032CA
		public string VirtualPathRoot
		{
			get
			{
				return this._routes.VirtualPathRoot;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000050D7 File Offset: 0x000032D7
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000050DF File Offset: 0x000032DF
		public IDependencyResolver DependencyResolver
		{
			get
			{
				return this._dependencyResolver;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._dependencyResolver = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000050F1 File Offset: 0x000032F1
		// (set) Token: 0x0600015E RID: 350 RVA: 0x000050F9 File Offset: 0x000032F9
		public ServicesContainer Services { get; internal set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005102 File Offset: 0x00003302
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000510A File Offset: 0x0000330A
		public ParameterBindingRulesCollection ParameterBindingRules { get; internal set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005113 File Offset: 0x00003313
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000511B File Offset: 0x0000331B
		public IncludeErrorDetailPolicy IncludeErrorDetailPolicy { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005124 File Offset: 0x00003324
		public MediaTypeFormatterCollection Formatters
		{
			get
			{
				return this._formatters;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000512C File Offset: 0x0000332C
		private static MediaTypeFormatterCollection DefaultFormatters(HttpConfiguration config)
		{
			return new MediaTypeFormatterCollection
			{
				new JQueryMvcFormUrlEncodedFormatter(config)
			};
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005140 File Offset: 0x00003340
		internal static HttpConfiguration ApplyControllerSettings(HttpControllerSettings settings, HttpConfiguration configuration)
		{
			if (!settings.IsFormatterCollectionInitialized && !settings.IsParameterBindingRuleCollectionInitialized && !settings.IsServiceCollectionInitialized)
			{
				return configuration;
			}
			HttpConfiguration httpConfiguration = new HttpConfiguration(configuration, settings);
			httpConfiguration.Initializer(httpConfiguration);
			return httpConfiguration;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000517C File Offset: 0x0000337C
		private static void DefaultInitializer(HttpConfiguration configuration)
		{
			ModelMetadataProvider modelMetadataProvider = configuration.Services.GetModelMetadataProvider();
			IEnumerable<ModelValidatorProvider> modelValidatorProviders = configuration.Services.GetModelValidatorProviders();
			IRequiredMemberSelector requiredMemberSelector = new ModelValidationRequiredMemberSelector(modelMetadataProvider, modelValidatorProviders);
			foreach (MediaTypeFormatter mediaTypeFormatter in configuration.Formatters)
			{
				if (mediaTypeFormatter.RequiredMemberSelector == null)
				{
					mediaTypeFormatter.RequiredMemberSelector = requiredMemberSelector;
				}
			}
			configuration.Services.GetTraceManager().Initialize(configuration);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005200 File Offset: 0x00003400
		public void EnsureInitialized()
		{
			if (this._initialized)
			{
				return;
			}
			this._initialized = true;
			this.Initializer(this);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000521E File Offset: 0x0000341E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000522D File Offset: 0x0000342D
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (disposing)
				{
					this._routes.Dispose();
					this.DependencyResolver.Dispose();
				}
			}
		}

		// Token: 0x04000045 RID: 69
		private readonly HttpRouteCollection _routes;

		// Token: 0x04000046 RID: 70
		private readonly ConcurrentDictionary<object, object> _properties;

		// Token: 0x04000047 RID: 71
		private readonly MediaTypeFormatterCollection _formatters;

		// Token: 0x04000048 RID: 72
		private readonly Collection<DelegatingHandler> _messageHandlers;

		// Token: 0x04000049 RID: 73
		private readonly HttpFilterCollection _filters;

		// Token: 0x0400004A RID: 74
		private IDependencyResolver _dependencyResolver;

		// Token: 0x0400004B RID: 75
		private Action<HttpConfiguration> _initializer;

		// Token: 0x0400004C RID: 76
		private bool _initialized;

		// Token: 0x0400004D RID: 77
		private bool _disposed;
	}
}
