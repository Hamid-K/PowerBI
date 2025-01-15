using System;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000F3 RID: 243
	public sealed class HttpControllerSettings
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x00010022 File Offset: 0x0000E222
		public HttpControllerSettings(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._configuration = configuration;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001003F File Offset: 0x0000E23F
		public MediaTypeFormatterCollection Formatters
		{
			get
			{
				if (this._formatters == null)
				{
					this._formatters = new MediaTypeFormatterCollection(this._configuration.Formatters);
				}
				return this._formatters;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00010068 File Offset: 0x0000E268
		public ParameterBindingRulesCollection ParameterBindingRules
		{
			get
			{
				if (this._parameterBindingRules == null)
				{
					this._parameterBindingRules = new ParameterBindingRulesCollection();
					foreach (Func<HttpParameterDescriptor, HttpParameterBinding> func in this._configuration.ParameterBindingRules)
					{
						this._parameterBindingRules.Add(func);
					}
				}
				return this._parameterBindingRules;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x000100D8 File Offset: 0x0000E2D8
		public ServicesContainer Services
		{
			get
			{
				if (this._services == null)
				{
					this._services = new ControllerServices(this._configuration.Services);
				}
				return this._services;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x000100FE File Offset: 0x0000E2FE
		internal bool IsFormatterCollectionInitialized
		{
			get
			{
				return this._formatters != null;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00010109 File Offset: 0x0000E309
		internal bool IsParameterBindingRuleCollectionInitialized
		{
			get
			{
				return this._parameterBindingRules != null;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00010114 File Offset: 0x0000E314
		internal bool IsServiceCollectionInitialized
		{
			get
			{
				return this._services != null;
			}
		}

		// Token: 0x0400018D RID: 397
		private MediaTypeFormatterCollection _formatters;

		// Token: 0x0400018E RID: 398
		private ParameterBindingRulesCollection _parameterBindingRules;

		// Token: 0x0400018F RID: 399
		private ServicesContainer _services;

		// Token: 0x04000190 RID: 400
		private HttpConfiguration _configuration;
	}
}
