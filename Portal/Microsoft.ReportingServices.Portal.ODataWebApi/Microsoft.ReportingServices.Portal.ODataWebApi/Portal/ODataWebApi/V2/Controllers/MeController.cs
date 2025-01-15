using System;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x0200001F RID: 31
	public class MeController : MeController
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00006D24 File Offset: 0x00004F24
		public MeController(ILogger logger, ICatalogRepository catalogRepository, IUserInfoService userInfoService)
			: base(logger, catalogRepository, userInfoService)
		{
			this._catalogRepository = base.CatalogRepository;
			this._userInfoService = base.UserInfoService;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006D47 File Offset: 0x00004F47
		protected override User GetSingleton()
		{
			return base.GetSingleton();
		}

		// Token: 0x04000063 RID: 99
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x04000064 RID: 100
		private readonly IUserInfoService _userInfoService;
	}
}
