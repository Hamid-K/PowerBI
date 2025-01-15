using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers
{
	// Token: 0x02000033 RID: 51
	public class SystemResourcesController : EntitySetReflectionODataController<SystemResource>
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000A9C9 File Offset: 0x00008BC9
		protected ISystemResourceService SystemResourceService
		{
			get
			{
				return this._systemResourceService;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A9D1 File Offset: 0x00008BD1
		public SystemResourcesController(ISystemResourceService systemResourceService, ILogger logger)
			: base(logger)
		{
			if (systemResourceService == null)
			{
				throw new ArgumentNullException("systemResourceService");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._systemResourceService = systemResourceService;
			this._logger = logger;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000AA04 File Offset: 0x00008C04
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntityType<SystemResource>().Ignore<Guid>((SystemResource x) => x.PackageId);
			builder.EntityType<SystemResourceItem>().Ignore<string>((SystemResourceItem x) => x.TypeName);
			builder.EntityType<SystemResourceItem>().Ignore<bool>((SystemResourceItem x) => x.IsEmbedded);
			builder.EnumType<SystemResourceType>();
			builder.EnumType<SystemResourceType>().Member(SystemResourceType.Brand);
			builder.EntitySet<SystemResource>("SystemResources");
			builder.EntitySet<SystemResourceItem>("SystemResourceItems");
			builder.OnModelCreating = delegate(ODataConventionModelBuilder b)
			{
				b.EntitySet<SystemResource>("SystemResources").HasReadLink(delegate(ResourceContext<SystemResource> ctx)
				{
					object obj;
					if (!ctx.EdmObject.TryGetPropertyValue("TypeName", out obj))
					{
						throw new InvalidOperationException("TypeName not found for SystemResources context");
					}
					IEdmEntityContainerElement edmEntityContainerElement = ctx.NavigationSource as IEdmEntitySet;
					EdmEntityType edmEntityType = new EdmEntityType("SystemResources", "SafeGetSystemResourceContent");
					EdmEntitySet edmEntitySet = new EdmEntitySet(edmEntityContainerElement.Container, "SafeGetSystemResourceContent", edmEntityType);
					return new Uri(ctx.Url.CreateODataLink(new ODataPathSegment[]
					{
						new EntitySetSegment(edmEntitySet),
						new KeySegment(new KeyValuePair<string, object>[]
						{
							new KeyValuePair<string, object>("type", obj),
							new KeyValuePair<string, object>("key", string.Empty)
						}, ctx.NavigationSource.EntityType(), ctx.NavigationSource)
					}));
				}, false);
				b.EntityType<SystemResource>().NavigationProperties.First((NavigationPropertyConfiguration x) => x.Name == "Items").AutoExpand = true;
				b.EntitySet<SystemResourceItem>("SystemResourceItems").HasReadLink(delegate(ResourceContext<SystemResourceItem> ctx)
				{
					object obj2;
					bool flag = ctx.EdmObject.TryGetPropertyValue("TypeName", out obj2);
					object obj3;
					bool flag2 = ctx.EdmObject.TryGetPropertyValue("Key", out obj3);
					if (!flag || !flag2)
					{
						throw new InvalidOperationException("TypeName and or Key not found for SystemResources context");
					}
					IEdmEntityContainerElement edmEntityContainerElement2 = ctx.NavigationSource as IEdmEntitySet;
					EdmEntityType edmEntityType2 = new EdmEntityType("SystemResources", "SafeGetSystemResourceContent");
					EdmEntitySet edmEntitySet2 = new EdmEntitySet(edmEntityContainerElement2.Container, "SafeGetSystemResourceContent", edmEntityType2);
					return new Uri(ctx.Url.CreateODataLink(new ODataPathSegment[]
					{
						new EntitySetSegment(edmEntitySet2),
						new KeySegment(new KeyValuePair<string, object>[]
						{
							new KeyValuePair<string, object>("type", obj2),
							new KeyValuePair<string, object>("key", obj3)
						}, ctx.NavigationSource.EntityType(), ctx.NavigationSource)
					}));
				}, false);
			};
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000AB2E File Offset: 0x00008D2E
		protected override IQueryable<SystemResource> GetEntitySet(string castName)
		{
			return this._systemResourceService.GetSystemResources(base.User);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000882B File Offset: 0x00006A2B
		protected override SystemResource GetEntity(string key, string castName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000AB44 File Offset: 0x00008D44
		protected override bool AddEntity(SystemResource entity, out SystemResource createdEntity)
		{
			SystemResourcePackage systemResourcePackage = entity as SystemResourcePackage;
			if (systemResourcePackage == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.NotExpectedTypeException, "SystemResourcePackage"), "entity");
			}
			if (string.IsNullOrEmpty(systemResourcePackage.PackageFileName))
			{
				throw new ArgumentException(SR.PropertyNullException, "PackageFileName");
			}
			if (systemResourcePackage.Content == null || systemResourcePackage.Content.Length == 0)
			{
				throw new ArgumentException(SR.PropertyNullException, "Content");
			}
			if (string.IsNullOrEmpty(systemResourcePackage.TypeName))
			{
				throw new ArgumentException(SR.PropertyNullException, "TypeName");
			}
			createdEntity = this._systemResourceService.InstallSystemResourcePackage(base.User, systemResourcePackage.Content, systemResourcePackage.PackageFileName, entity.TypeName);
			return true;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000ABFC File Offset: 0x00008DFC
		protected override bool AddEntity(SystemResource entity)
		{
			SystemResource systemResource;
			return this.AddEntity(entity, out systemResource);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000AC14 File Offset: 0x00008E14
		protected override bool DeleteEntity(string key)
		{
			Guid guid;
			return Guid.TryParse(key, out guid) && this._systemResourceService.DeleteSystemResource(base.User, guid);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000882B File Offset: 0x00006A2B
		protected override bool PatchEntity(string key, SystemResource entity, string[] delta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000882B File Offset: 0x00006A2B
		protected override bool PutEntity(string key, SystemResource entity)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000090 RID: 144
		protected const string SafeGetSystemResourceContentEndpointFormat = "SafeGetSystemResourceContent({0})";

		// Token: 0x04000091 RID: 145
		protected const string SafeGetSystemResourceContentEndpointParameterFormat = "type='{0}',key='{1}'";

		// Token: 0x04000092 RID: 146
		private readonly ISystemResourceService _systemResourceService;

		// Token: 0x04000093 RID: 147
		private readonly ILogger _logger;
	}
}
