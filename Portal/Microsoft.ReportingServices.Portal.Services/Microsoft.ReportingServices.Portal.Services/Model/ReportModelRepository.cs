using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;

namespace Model
{
	// Token: 0x0200000D RID: 13
	internal class ReportModelRepository : ReportModel
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000028DA File Offset: 0x00000ADA
		public ReportModelRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository)
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

		// Token: 0x04000041 RID: 65
		private readonly IPrincipal _userPrincipal;

		// Token: 0x04000042 RID: 66
		private readonly ICatalogRepository _catalogRepository;
	}
}
