using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers
{
	// Token: 0x02000034 RID: 52
	public class UnboundFunctionController : ODataController
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000AC3F File Offset: 0x00008E3F
		protected ICatalogRepository CatalogRepository
		{
			get
			{
				return this._catalogRepository;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000AC47 File Offset: 0x00008E47
		protected ISystemService SystemService
		{
			get
			{
				return this._systemService;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000AC4F File Offset: 0x00008E4F
		protected ISystemResourceService SystemResourceService
		{
			get
			{
				return this._systemResourceService;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000AC58 File Offset: 0x00008E58
		public UnboundFunctionController(ICatalogRepository catalogRepository, ISystemService systemService, ISystemResourceService systemResourceService)
		{
			if (catalogRepository == null)
			{
				throw new ArgumentNullException("catalogRepository");
			}
			if (systemService == null)
			{
				throw new ArgumentNullException("systemService");
			}
			if (systemResourceService == null)
			{
				throw new ArgumentNullException("systemResourceService");
			}
			this._catalogRepository = catalogRepository;
			this._systemService = systemService;
			this._systemResourceService = systemResourceService;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000ACAC File Offset: 0x00008EAC
		[HttpGet]
		[EnableQuery]
		public virtual IHttpActionResult CatalogItemByPath(string path)
		{
			path = this.CleanPath(path);
			CatalogItem catalogItem = this._catalogRepository.GetCatalogItem(base.User, path);
			if (catalogItem == null)
			{
				return this.NotFound();
			}
			IList<DataSource> list = null;
			CatalogItemType type = catalogItem.Type;
			switch (type)
			{
			case CatalogItemType.Report:
				list = ((Report)catalogItem).DataSources;
				break;
			case CatalogItemType.DataSource:
				list = new List<DataSource> { (DataSource)catalogItem };
				break;
			case CatalogItemType.DataSet:
				list = ((DataSet)catalogItem).DataSources;
				break;
			default:
				if (type == CatalogItemType.ReportModel)
				{
					list = new List<DataSource> { (ReportModel)catalogItem };
				}
				break;
			}
			this.SetPasswordToNull(list);
			catalogItem.Properties = new List<Property>();
			return this.Ok<CatalogItem>(catalogItem);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000AD60 File Offset: 0x00008F60
		[HttpGet]
		[EnableQuery]
		public virtual IHttpActionResult AllowedActions(string path)
		{
			path = this.CleanPath(path);
			List<string> allowedActions = this._catalogRepository.GetAllowedActions(base.User, path);
			return this.Ok<List<string>>(allowedActions);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000AD90 File Offset: 0x00008F90
		[HttpGet]
		[EnableQuery]
		public virtual IHttpActionResult FavoriteItems()
		{
			IQueryable<CatalogItem> favoriteItems = this._catalogRepository.GetFavoriteItems(base.User);
			return this.Ok<IQueryable<CatalogItem>>(favoriteItems);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		[HttpGet]
		public virtual IHttpActionResult ServiceState()
		{
			ServiceState serviceState = new ServiceState
			{
				IsAvailable = true,
				RestrictedFeatures = this._systemService.GetRestrictedFeatures(),
				AllowedSystemActions = this._systemService.GetAllowedSystemActions(base.User),
				TimeZone = UnboundFunctionController.GetTimeZoneFullName(),
				UserHasFavorites = this._catalogRepository.GetFavoriteItems(base.User).Any<CatalogItem>(),
				AcceptLanguage = this.GetAcceptLanguage(),
				RequireIntune = this._systemService.GetSystemProperties(base.User, new string[] { "RequireIntune" }).Any((Property prop) => prop.Name == "RequireIntune" && bool.Parse(prop.Value))
			};
			return this.Ok<ServiceState>(serviceState);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000AEEC File Offset: 0x000090EC
		[HttpGet]
		public virtual IHttpActionResult SafeGetSystemResourceContent(string type, string key)
		{
			if (string.IsNullOrEmpty(type))
			{
				return this.BadRequest(string.Format(CultureInfo.InvariantCulture, SR.ParameterValueNotSupplied, "type"));
			}
			if (key != null)
			{
				key = key.ToLowerInvariant();
			}
			string text;
			string text2;
			byte[] array;
			if (!this._systemResourceService.TryGetPayload(base.User, type.ToLowerInvariant(), key, out text, out text2, out array))
			{
				return this.ResponseMessage(this.CreateByteArrayHttpOkResponseMessage(null, null, key));
			}
			MediaTypeHeaderValue mediaTypeHeaderValue;
			MediaTypeHeaderValue.TryParse(text, out mediaTypeHeaderValue);
			return this.ResponseMessage(this.CreateByteArrayHttpOkResponseMessage(array, mediaTypeHeaderValue, text2));
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000AF70 File Offset: 0x00009170
		[HttpGet]
		[EnableQuery]
		public virtual IHttpActionResult RestrictedFeatures()
		{
			IEnumerable<string> enumerable = from x in this._systemService.GetRestrictedItemTypes()
				select x.ToString();
			return this.Ok<IEnumerable<string>>(enumerable);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000AFB4 File Offset: 0x000091B4
		protected internal string CleanPath(string path)
		{
			return path.Trim(new char[] { '/' }).Insert(0, "/");
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000AFD4 File Offset: 0x000091D4
		private HttpResponseMessage CreateByteArrayHttpOkResponseMessage(byte[] bytes, MediaTypeHeaderValue mediaType, string fileName)
		{
			ByteArrayContent byteArrayContent = new ByteArrayContent(bytes ?? new byte[0]);
			byteArrayContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
			{
				FileName = fileName
			};
			byteArrayContent.Headers.ContentType = mediaType ?? new MediaTypeHeaderValue("application/octet-stream");
			return new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = byteArrayContent
			};
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B040 File Offset: 0x00009240
		protected static string GetTimeZoneFullName()
		{
			string text = (TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now) ? TimeZoneInfo.Local.DaylightName : TimeZoneInfo.Local.StandardName);
			TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
			return string.Format("(UTC{0:+00;-00;+00}:{1:00}) {2}", utcOffset.Hours, utcOffset.Minutes, text);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B0A8 File Offset: 0x000092A8
		protected string GetAcceptLanguage()
		{
			if (base.Request != null)
			{
				HttpHeaderValueCollection<StringWithQualityHeaderValue> acceptLanguage = base.Request.Headers.AcceptLanguage;
				if (acceptLanguage != null)
				{
					return acceptLanguage.ToString();
				}
			}
			return null;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B0DC File Offset: 0x000092DC
		private void SetPasswordToNull(IList<DataSource> dataSources)
		{
			if (dataSources == null)
			{
				return;
			}
			foreach (DataSource dataSource in dataSources)
			{
				if (dataSource.CredentialsInServer != null)
				{
					dataSource.CredentialsInServer.Password = null;
				}
			}
		}

		// Token: 0x04000094 RID: 148
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x04000095 RID: 149
		private readonly ISystemService _systemService;

		// Token: 0x04000096 RID: 150
		private readonly ISystemResourceService _systemResourceService;
	}
}
