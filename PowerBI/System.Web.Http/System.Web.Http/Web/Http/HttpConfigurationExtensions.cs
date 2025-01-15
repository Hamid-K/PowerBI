using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Routing;

namespace System.Web.Http
{
	// Token: 0x0200001E RID: 30
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpConfigurationExtensions
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00003C6C File Offset: 0x00001E6C
		public static void BindParameter(this HttpConfiguration configuration, Type type, IModelBinder binder)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (binder == null)
			{
				throw Error.ArgumentNull("binder");
			}
			configuration.Services.Insert(typeof(ModelBinderProvider), 0, new SimpleModelBinderProvider(type, binder));
			configuration.ParameterBindingRules.Insert(0, type, (HttpParameterDescriptor param) => param.BindWithModelBinding(binder));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003CF6 File Offset: 0x00001EF6
		public static void MapHttpAttributeRoutes(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			AttributeRoutingMapper.MapAttributeRoutes(configuration, new DefaultInlineConstraintResolver(), new DefaultDirectRouteProvider());
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003D16 File Offset: 0x00001F16
		public static void MapHttpAttributeRoutes(this HttpConfiguration configuration, IInlineConstraintResolver constraintResolver)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (constraintResolver == null)
			{
				throw new ArgumentNullException("constraintResolver");
			}
			AttributeRoutingMapper.MapAttributeRoutes(configuration, constraintResolver, new DefaultDirectRouteProvider());
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003D40 File Offset: 0x00001F40
		public static void MapHttpAttributeRoutes(this HttpConfiguration configuration, IDirectRouteProvider directRouteProvider)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (directRouteProvider == null)
			{
				throw new ArgumentNullException("directRouteProvider");
			}
			AttributeRoutingMapper.MapAttributeRoutes(configuration, new DefaultInlineConstraintResolver(), directRouteProvider);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003D6A File Offset: 0x00001F6A
		public static void MapHttpAttributeRoutes(this HttpConfiguration configuration, IInlineConstraintResolver constraintResolver, IDirectRouteProvider directRouteProvider)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (constraintResolver == null)
			{
				throw new ArgumentNullException("constraintResolver");
			}
			if (directRouteProvider == null)
			{
				throw new ArgumentNullException("directRouteProvider");
			}
			AttributeRoutingMapper.MapAttributeRoutes(configuration, constraintResolver, directRouteProvider);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003DA0 File Offset: 0x00001FA0
		internal static IReadOnlyCollection<IHttpRoute> GetAttributeRoutes(this HttpConfiguration configuration)
		{
			configuration.EnsureInitialized();
			foreach (IHttpRoute httpRoute in configuration.Routes)
			{
				IReadOnlyCollection<IHttpRoute> readOnlyCollection = httpRoute as IReadOnlyCollection<IHttpRoute>;
				if (readOnlyCollection != null)
				{
					return readOnlyCollection;
				}
			}
			return null;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003DFC File Offset: 0x00001FFC
		public static void SuppressHostPrincipal(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			configuration.MessageHandlers.Insert(0, new SuppressHostPrincipalMessageHandler());
		}
	}
}
