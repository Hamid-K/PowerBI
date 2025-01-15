using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.Web.Http;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;

namespace Model
{
	// Token: 0x0200000E RID: 14
	internal class ReportRepository : Report
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000290C File Offset: 0x00000B0C
		public ReportRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository)
		{
			if (userPrincipal == null)
			{
				throw new ArgumentNullException("userPrincipal");
			}
			if (catalogRepository == null)
			{
				throw new ArgumentNullException("catalogRepository");
			}
			this._userPrincipal = userPrincipal;
			this._catalogRepository = catalogRepository;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000293E File Offset: 0x00000B3E
		protected override IList<Subscription> LoadSubscriptions()
		{
			return this._catalogRepository.GetSubscriptions(this._userPrincipal, base.Path);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002957 File Offset: 0x00000B57
		protected override IList<CacheRefreshPlan> LoadCacheRefreshPlans()
		{
			return this._catalogRepository.GetCacheRefreshPlans(this._userPrincipal, base.Path);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002970 File Offset: 0x00000B70
		protected override IList<DataSource> LoadDataSources()
		{
			return this._catalogRepository.GetDataSourcesForCatalogItem(this._userPrincipal, base.Path);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002989 File Offset: 0x00000B89
		protected override IList<DataSet> LoadSharedDataSets()
		{
			return this._catalogRepository.GetDataSetsForCatalogItem(this._userPrincipal, base.Path);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000029A2 File Offset: 0x00000BA2
		protected override IList<ReportHistorySnapshot> LoadReportHistorySnapshots()
		{
			return this._catalogRepository.GetReportHistorySnapshots(this._userPrincipal, base.Path);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029BB File Offset: 0x00000BBB
		protected override IList<HistorySnapshot> LoadHistorySnapshots()
		{
			return this._catalogRepository.GetHistorySnapshots(this._userPrincipal, base.Path);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029D4 File Offset: 0x00000BD4
		protected override HistorySnapshotOptions LoadHistorySnapshotOptions()
		{
			ReportHistorySnapshotsOptions reportHistorySnapshotsOptions = this._catalogRepository.GetReportHistorySnapshotsOptions(this._userPrincipal, base.Path);
			return new HistorySnapshotOptions
			{
				CatalogItemId = base.Id,
				HistorySnapshotsOptions = reportHistorySnapshotsOptions
			};
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A11 File Offset: 0x00000C11
		protected override IList<ReportParameterDefinition> LoadParameterDefinitions()
		{
			return this._catalogRepository.GetReportParameterDefinitionsWithQuery(this._userPrincipal, base.Path);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A2C File Offset: 0x00000C2C
		protected override IList<Comment> LoadComments()
		{
			IList<Comment> commentsByItem;
			try
			{
				commentsByItem = this._catalogRepository.GetCommentsByItem(this._userPrincipal, base.Id);
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return commentsByItem;
		}

		// Token: 0x04000043 RID: 67
		private readonly IPrincipal _userPrincipal;

		// Token: 0x04000044 RID: 68
		private readonly ICatalogRepository _catalogRepository;
	}
}
