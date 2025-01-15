using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Internal;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000100 RID: 256
	public class HttpControllerDescriptor
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		public HttpControllerDescriptor(HttpConfiguration configuration, string controllerName, Type controllerType)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (controllerName == null)
			{
				throw Error.ArgumentNull("controllerName");
			}
			if (controllerType == null)
			{
				throw Error.ArgumentNull("controllerType");
			}
			this._configuration = configuration;
			this._controllerName = controllerName;
			this._controllerType = controllerType;
			this.Initialize();
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00011041 File Offset: 0x0000F241
		public HttpControllerDescriptor()
		{
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00011054 File Offset: 0x0000F254
		internal HttpControllerDescriptor(HttpConfiguration configuration)
		{
			this.Initialize(configuration);
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001106E File Offset: 0x0000F26E
		public virtual ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00011076 File Offset: 0x0000F276
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001107E File Offset: 0x0000F27E
		public HttpConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._configuration = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00011090 File Offset: 0x0000F290
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x00011098 File Offset: 0x0000F298
		public string ControllerName
		{
			get
			{
				return this._controllerName;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controllerName = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x000110AA File Offset: 0x0000F2AA
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x000110B2 File Offset: 0x0000F2B2
		public Type ControllerType
		{
			get
			{
				return this._controllerType;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controllerType = value;
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000110CA File Offset: 0x0000F2CA
		public virtual IHttpController CreateController(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return this.Configuration.Services.GetHttpControllerActivator().Create(request, this, this.ControllerType);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000110F7 File Offset: 0x0000F2F7
		public virtual Collection<IFilter> GetFilters()
		{
			return this.GetCustomAttributes<IFilter>();
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000110FF File Offset: 0x0000F2FF
		public virtual Collection<T> GetCustomAttributes<T>() where T : class
		{
			return this.GetCustomAttributes<T>(true);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00011108 File Offset: 0x0000F308
		public virtual Collection<T> GetCustomAttributes<T>(bool inherit) where T : class
		{
			object[] array;
			if (inherit)
			{
				if (this._attributeCache == null)
				{
					this._attributeCache = this.ControllerType.GetCustomAttributes(true);
				}
				array = this._attributeCache;
			}
			else
			{
				if (this._declaredOnlyAttributeCache == null)
				{
					this._declaredOnlyAttributeCache = this.ControllerType.GetCustomAttributes(false);
				}
				array = this._declaredOnlyAttributeCache;
			}
			return new Collection<T>(TypeHelper.OfType<T>(array));
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00011167 File Offset: 0x0000F367
		internal void Initialize(HttpConfiguration configuration)
		{
			this._configuration = configuration;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00011170 File Offset: 0x0000F370
		private void Initialize()
		{
			HttpControllerDescriptor.InvokeAttributesOnControllerType(this, this.ControllerType);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00011180 File Offset: 0x0000F380
		private static void InvokeAttributesOnControllerType(HttpControllerDescriptor controllerDescriptor, Type type)
		{
			if (type == null)
			{
				return;
			}
			HttpControllerDescriptor.InvokeAttributesOnControllerType(controllerDescriptor, type.BaseType);
			object[] customAttributes = type.GetCustomAttributes(false);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				IControllerConfiguration controllerConfiguration = customAttributes[i] as IControllerConfiguration;
				if (controllerConfiguration != null)
				{
					HttpConfiguration configuration = controllerDescriptor.Configuration;
					HttpControllerSettings httpControllerSettings = new HttpControllerSettings(configuration);
					controllerConfiguration.Initialize(httpControllerSettings, controllerDescriptor);
					controllerDescriptor.Configuration = HttpConfiguration.ApplyControllerSettings(httpControllerSettings, configuration);
				}
			}
		}

		// Token: 0x040001BB RID: 443
		private readonly ConcurrentDictionary<object, object> _properties = new ConcurrentDictionary<object, object>();

		// Token: 0x040001BC RID: 444
		private HttpConfiguration _configuration;

		// Token: 0x040001BD RID: 445
		private string _controllerName;

		// Token: 0x040001BE RID: 446
		private Type _controllerType;

		// Token: 0x040001BF RID: 447
		private object[] _attributeCache;

		// Token: 0x040001C0 RID: 448
		private object[] _declaredOnlyAttributeCache;
	}
}
