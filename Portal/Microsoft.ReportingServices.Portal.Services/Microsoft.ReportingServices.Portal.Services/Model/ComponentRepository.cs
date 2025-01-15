using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;

namespace Model
{
	// Token: 0x02000005 RID: 5
	internal class ComponentRepository : Component
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000234C File Offset: 0x0000054C
		public ComponentRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository)
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

		// Token: 0x0400002D RID: 45
		private readonly IPrincipal _userPrincipal;

		// Token: 0x0400002E RID: 46
		private readonly ICatalogRepository _catalogRepository;
	}
}
