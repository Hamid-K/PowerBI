using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace System.Web.Http.Description
{
	// Token: 0x020000E5 RID: 229
	public class ApiDescription
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x0000E97C File Offset: 0x0000CB7C
		public ApiDescription()
		{
			this.SupportedRequestBodyFormatters = new Collection<MediaTypeFormatter>();
			this.SupportedResponseFormatters = new Collection<MediaTypeFormatter>();
			this.ParameterDescriptions = new Collection<ApiParameterDescription>();
			this.ResponseDescription = new ResponseDescription();
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		public HttpMethod HttpMethod { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000E9C1 File Offset: 0x0000CBC1
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0000E9C9 File Offset: 0x0000CBC9
		public string RelativePath { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000E9D2 File Offset: 0x0000CBD2
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000E9DA File Offset: 0x0000CBDA
		public HttpActionDescriptor ActionDescriptor { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000E9E3 File Offset: 0x0000CBE3
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0000E9EB File Offset: 0x0000CBEB
		public IHttpRoute Route { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string Documentation { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0000EA05 File Offset: 0x0000CC05
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x0000EA0D File Offset: 0x0000CC0D
		public Collection<MediaTypeFormatter> SupportedResponseFormatters { get; internal set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000EA16 File Offset: 0x0000CC16
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0000EA1E File Offset: 0x0000CC1E
		public Collection<MediaTypeFormatter> SupportedRequestBodyFormatters { get; internal set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000EA27 File Offset: 0x0000CC27
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0000EA2F File Offset: 0x0000CC2F
		public Collection<ApiParameterDescription> ParameterDescriptions { get; internal set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000EA38 File Offset: 0x0000CC38
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x0000EA40 File Offset: 0x0000CC40
		public ResponseDescription ResponseDescription { get; internal set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000EA49 File Offset: 0x0000CC49
		public string ID
		{
			get
			{
				return ((this.HttpMethod != null) ? this.HttpMethod.Method : string.Empty) + (this.RelativePath ?? string.Empty);
			}
		}
	}
}
