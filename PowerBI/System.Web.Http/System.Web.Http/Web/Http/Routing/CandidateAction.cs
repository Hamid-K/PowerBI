using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.Routing
{
	// Token: 0x0200014A RID: 330
	[DebuggerDisplay("{DebuggerToString()}")]
	internal class CandidateAction
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x000167E3 File Offset: 0x000149E3
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x000167EB File Offset: 0x000149EB
		public HttpActionDescriptor ActionDescriptor { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x000167F4 File Offset: 0x000149F4
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x000167FC File Offset: 0x000149FC
		public int Order { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00016805 File Offset: 0x00014A05
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x0001680D File Offset: 0x00014A0D
		public decimal Precedence { get; set; }

		// Token: 0x06000900 RID: 2304 RVA: 0x00016816 File Offset: 0x00014A16
		public bool MatchName(string actionName)
		{
			return string.Equals(this.ActionDescriptor.ActionName, actionName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001682A File Offset: 0x00014A2A
		public bool MatchVerb(HttpMethod method)
		{
			return this.ActionDescriptor.SupportedHttpMethods.Contains(method);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00016840 File Offset: 0x00014A40
		internal string DebuggerToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}, Order={1}, Prec={2}", new object[]
			{
				this.ActionDescriptor.ActionName,
				this.Order,
				this.Precedence
			});
		}
	}
}
