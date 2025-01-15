using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;

namespace Model
{
	// Token: 0x02000007 RID: 7
	internal class DataSourceRepository : DataSource
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000023FB File Offset: 0x000005FB
		public DataSourceRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository)
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

		// Token: 0x06000014 RID: 20 RVA: 0x0000242D File Offset: 0x0000062D
		protected override IList<Subscription> LoadSubscriptions()
		{
			return this._catalogRepository.GetSubscriptionsUsingDataSource(this._userPrincipal, base.Path);
		}

		// Token: 0x04000031 RID: 49
		private readonly IPrincipal _userPrincipal;

		// Token: 0x04000032 RID: 50
		private readonly ICatalogRepository _catalogRepository;
	}
}
