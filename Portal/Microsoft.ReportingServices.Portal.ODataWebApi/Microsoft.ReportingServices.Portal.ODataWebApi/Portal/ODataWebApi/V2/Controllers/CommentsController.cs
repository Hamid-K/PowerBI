using System;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000012 RID: 18
	public class CommentsController : CommentsController
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00004F35 File Offset: 0x00003135
		public CommentsController(ICatalogRepository catalogRepository, ILogger logger)
			: base(catalogRepository, logger)
		{
			this._catalogRepository = base.CatalogRepository;
			this._logger = base.Logger;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004F57 File Offset: 0x00003157
		public new static void RegisterModel(ODataConventionModelBuilder builder)
		{
			CommentsController.RegisterModel(builder);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004F5F File Offset: 0x0000315F
		protected override IQueryable<Comment> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004F68 File Offset: 0x00003168
		protected override Comment GetEntity(string Id, string castName)
		{
			return base.GetEntity(Id, castName);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004F72 File Offset: 0x00003172
		protected override bool AddEntity(Comment entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004F7B File Offset: 0x0000317B
		protected override bool AddEntity(Comment entity, out Comment createdEntity)
		{
			return base.AddEntity(entity, out createdEntity);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004F85 File Offset: 0x00003185
		protected override bool PutEntity(string key, Comment entity)
		{
			return base.PutEntity(key, entity);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004F8F File Offset: 0x0000318F
		protected override bool PatchEntity(string key, Comment entity, string[] delta)
		{
			return base.PatchEntity(key, entity, delta);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004F9A File Offset: 0x0000319A
		protected override bool DeleteEntity(string Id)
		{
			return base.DeleteEntity(Id);
		}

		// Token: 0x04000053 RID: 83
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x04000054 RID: 84
		private readonly ILogger _logger;
	}
}
