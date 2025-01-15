using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;

namespace Microsoft.Identity.Client.Http
{
	// Token: 0x0200028C RID: 652
	internal class HttpResponse
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x00052983 File Offset: 0x00050B83
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x0005298B File Offset: 0x00050B8B
		public HttpResponseHeaders Headers { get; set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x00052994 File Offset: 0x00050B94
		public IDictionary<string, string> HeadersAsDictionary
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				if (this.Headers != null)
				{
					foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in this.Headers)
					{
						dictionary[keyValuePair.Key] = keyValuePair.Value.First<string>();
					}
				}
				return dictionary;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x00052A04 File Offset: 0x00050C04
		// (set) Token: 0x0600190B RID: 6411 RVA: 0x00052A0C File Offset: 0x00050C0C
		public HttpStatusCode StatusCode { get; set; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x00052A15 File Offset: 0x00050C15
		// (set) Token: 0x0600190D RID: 6413 RVA: 0x00052A1D File Offset: 0x00050C1D
		public string UserAgent { get; set; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x00052A26 File Offset: 0x00050C26
		// (set) Token: 0x0600190F RID: 6415 RVA: 0x00052A2E File Offset: 0x00050C2E
		public string Body { get; set; }
	}
}
