using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;

namespace Model
{
	// Token: 0x02000006 RID: 6
	internal sealed class DataSetRepository : DataSet
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000237E File Offset: 0x0000057E
		public DataSetRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository)
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

		// Token: 0x06000010 RID: 16 RVA: 0x000023B0 File Offset: 0x000005B0
		protected override IList<DataSource> LoadDataSources()
		{
			return this._catalogRepository.GetDataSourcesForCatalogItem(this._userPrincipal, base.Path);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023C9 File Offset: 0x000005C9
		protected override IList<CacheRefreshPlan> LoadCacheRefreshPlans()
		{
			return this._catalogRepository.GetCacheRefreshPlans(this._userPrincipal, base.Path);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023E2 File Offset: 0x000005E2
		protected override IList<ReportParameterDefinition> LoadParameterDefinitions()
		{
			return this._catalogRepository.GetSimpleParameterDefinitions(this._userPrincipal, base.Path);
		}

		// Token: 0x0400002F RID: 47
		private readonly IPrincipal _userPrincipal;

		// Token: 0x04000030 RID: 48
		private readonly ICatalogRepository _catalogRepository;
	}
}
