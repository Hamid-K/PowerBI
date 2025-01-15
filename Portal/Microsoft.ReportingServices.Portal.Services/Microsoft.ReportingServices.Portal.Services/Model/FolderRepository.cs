using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;

namespace Model
{
	// Token: 0x02000009 RID: 9
	internal class FolderRepository : Folder
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000024FC File Offset: 0x000006FC
		public FolderRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository)
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

		// Token: 0x06000018 RID: 24 RVA: 0x0000252E File Offset: 0x0000072E
		protected override IList<CatalogItem> LoadCatalogItems()
		{
			return new List<CatalogItem>(this._catalogRepository.TraverseFolder(this._userPrincipal, base.Path, false));
		}

		// Token: 0x04000036 RID: 54
		private readonly IPrincipal _userPrincipal;

		// Token: 0x04000037 RID: 55
		private readonly ICatalogRepository _catalogRepository;
	}
}
