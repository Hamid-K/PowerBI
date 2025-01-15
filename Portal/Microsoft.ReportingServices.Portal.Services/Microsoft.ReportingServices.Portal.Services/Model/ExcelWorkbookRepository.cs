using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.Web.Http;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;

namespace Model
{
	// Token: 0x02000008 RID: 8
	internal class ExcelWorkbookRepository : ExcelWorkbook
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002448 File Offset: 0x00000648
		public ExcelWorkbookRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository, ISystemService systemService)
		{
			if (userPrincipal == null)
			{
				throw new ArgumentNullException("userPrincipal");
			}
			if (catalogRepository == null)
			{
				throw new ArgumentNullException("catalogRepository");
			}
			if (systemService == null)
			{
				throw new ArgumentNullException("systemService");
			}
			this._userPrincipal = userPrincipal;
			this._catalogRepository = catalogRepository;
			this._systemService = systemService;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000249C File Offset: 0x0000069C
		protected override IList<Comment> LoadComments()
		{
			if (!this._systemService.IsBiServer())
			{
				throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
			}
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

		// Token: 0x04000033 RID: 51
		private readonly IPrincipal _userPrincipal;

		// Token: 0x04000034 RID: 52
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x04000035 RID: 53
		private readonly ISystemService _systemService;
	}
}
