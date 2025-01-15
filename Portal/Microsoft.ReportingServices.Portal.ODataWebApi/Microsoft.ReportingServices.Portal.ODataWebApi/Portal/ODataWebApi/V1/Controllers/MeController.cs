using System;
using System.Linq;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Microsoft.ReportingServices.Portal.Services.Extensions;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers
{
	// Token: 0x0200002D RID: 45
	public class MeController : SingletonReflectionODataController<User>
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00008903 File Offset: 0x00006B03
		protected ICatalogRepository CatalogRepository
		{
			get
			{
				return this._catalogRepository;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000890B File Offset: 0x00006B0B
		protected IUserInfoService UserInfoService
		{
			get
			{
				return this._userInfoService;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008913 File Offset: 0x00006B13
		public MeController(ILogger logger, ICatalogRepository catalogRepository, IUserInfoService userInfoService)
			: base(logger)
		{
			if (catalogRepository == null)
			{
				throw new ArgumentNullException("catalogRepository");
			}
			if (userInfoService == null)
			{
				throw new ArgumentNullException("userInfoService");
			}
			this._userInfoService = userInfoService;
			this._catalogRepository = catalogRepository;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008948 File Offset: 0x00006B48
		protected override User GetSingleton()
		{
			if (base.User != null && base.User.Identity != null)
			{
				AuthenticationType authenticationType = base.User.Identity.ToAuthenticationType();
				Guid userIdFromName = this._catalogRepository.GetUserIdFromName(base.User.Identity.Name, authenticationType);
				return new User
				{
					Id = userIdFromName,
					Username = base.User.Identity.Name,
					DisplayName = this._userInfoService.GetUserDisplayName(base.User.Identity),
					HasFavoriteItems = this._catalogRepository.GetFavoriteItems(base.User).Any<CatalogItem>(),
					MyReportsPath = this._catalogRepository.GetMyReportsPath(base.User)
				};
			}
			return null;
		}

		// Token: 0x0400007F RID: 127
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x04000080 RID: 128
		private readonly IUserInfoService _userInfoService;
	}
}
