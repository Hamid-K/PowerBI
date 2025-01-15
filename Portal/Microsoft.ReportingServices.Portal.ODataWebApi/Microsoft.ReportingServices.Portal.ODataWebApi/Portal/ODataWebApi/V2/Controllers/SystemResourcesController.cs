using System;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000025 RID: 37
	public class SystemResourcesController : SystemResourcesController
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00007753 File Offset: 0x00005953
		public SystemResourcesController(ISystemResourceService systemResourceService, ILogger logger)
			: base(systemResourceService, logger)
		{
			this._systemResourceService = base.SystemResourceService;
			this._logger = base.Logger;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007775 File Offset: 0x00005975
		public new static void RegisterModel(ODataConventionModelBuilder builder)
		{
			SystemResourcesController.RegisterModel(builder);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000777D File Offset: 0x0000597D
		protected override IQueryable<SystemResource> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007786 File Offset: 0x00005986
		protected override SystemResource GetEntity(string Id, string castName)
		{
			return base.GetEntity(Id, castName);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007790 File Offset: 0x00005990
		protected override bool AddEntity(SystemResource entity, out SystemResource createdEntity)
		{
			return base.AddEntity(entity, out createdEntity);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000779A File Offset: 0x0000599A
		protected override bool AddEntity(SystemResource entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000077A3 File Offset: 0x000059A3
		protected override bool DeleteEntity(string Id)
		{
			return base.DeleteEntity(Id);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000077AC File Offset: 0x000059AC
		protected override bool PatchEntity(string Id, SystemResource entity, string[] delta)
		{
			return base.PatchEntity(Id, entity, delta);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000077B7 File Offset: 0x000059B7
		protected override bool PutEntity(string key, SystemResource entity)
		{
			return base.PutEntity(key, entity);
		}

		// Token: 0x0400006D RID: 109
		private readonly ISystemResourceService _systemResourceService;

		// Token: 0x0400006E RID: 110
		private readonly ILogger _logger;
	}
}
