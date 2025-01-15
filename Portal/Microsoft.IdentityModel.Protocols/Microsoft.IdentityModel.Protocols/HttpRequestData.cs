using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Microsoft.IdentityModel.Protocols
{
	// Token: 0x0200000C RID: 12
	public class HttpRequestData
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000028B9 File Offset: 0x00000AB9
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000028C1 File Offset: 0x00000AC1
		public Uri Uri { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000028CA File Offset: 0x00000ACA
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000028D2 File Offset: 0x00000AD2
		public string Method { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000028DB File Offset: 0x00000ADB
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000028E3 File Offset: 0x00000AE3
		public byte[] Body { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028EC File Offset: 0x00000AEC
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000028F4 File Offset: 0x00000AF4
		public IDictionary<string, IEnumerable<string>> Headers
		{
			get
			{
				return this._headers;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Headers");
				}
				this._headers = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000290C File Offset: 0x00000B0C
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002914 File Offset: 0x00000B14
		public IDictionary<string, object> PropertyBag { get; set; }

		// Token: 0x06000045 RID: 69 RVA: 0x00002920 File Offset: 0x00000B20
		public void AppendHeaders(HttpHeaders headers)
		{
			if (headers == null)
			{
				return;
			}
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in headers)
			{
				if (this.Headers.ContainsKey(keyValuePair.Key))
				{
					this.Headers[keyValuePair.Key] = this.Headers[keyValuePair.Key].Concat(keyValuePair.Value);
				}
				else
				{
					this.Headers.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x04000020 RID: 32
		private IDictionary<string, IEnumerable<string>> _headers = new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase);
	}
}
