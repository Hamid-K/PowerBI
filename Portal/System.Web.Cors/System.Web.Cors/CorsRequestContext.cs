using System;
using System.Collections.Generic;
using System.Text;

namespace System.Web.Cors
{
	// Token: 0x02000006 RID: 6
	public class CorsRequestContext
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000026B0 File Offset: 0x000008B0
		public CorsRequestContext()
		{
			this.AccessControlRequestHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.Properties = new Dictionary<string, object>();
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000026D3 File Offset: 0x000008D3
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000026DB File Offset: 0x000008DB
		public Uri RequestUri { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000026E4 File Offset: 0x000008E4
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000026EC File Offset: 0x000008EC
		public string HttpMethod { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000026F5 File Offset: 0x000008F5
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000026FD File Offset: 0x000008FD
		public string Origin { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002706 File Offset: 0x00000906
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000270E File Offset: 0x0000090E
		public string Host { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002717 File Offset: 0x00000917
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000271F File Offset: 0x0000091F
		public string AccessControlRequestMethod { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002728 File Offset: 0x00000928
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002730 File Offset: 0x00000930
		public ISet<string> AccessControlRequestHeaders { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002739 File Offset: 0x00000939
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002741 File Offset: 0x00000941
		public IDictionary<string, object> Properties { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000274A File Offset: 0x0000094A
		public bool IsPreflight
		{
			get
			{
				return this.Origin != null && this.AccessControlRequestMethod != null && string.Equals(this.HttpMethod, CorsConstants.PreflightHttpMethod, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002770 File Offset: 0x00000970
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Origin: ");
			stringBuilder.Append(this.Origin ?? "null");
			stringBuilder.Append(", HttpMethod: ");
			stringBuilder.Append(this.HttpMethod ?? "null");
			stringBuilder.Append(", IsPreflight: ");
			stringBuilder.Append(this.IsPreflight);
			stringBuilder.Append(", Host: ");
			stringBuilder.Append(this.Host);
			stringBuilder.Append(", AccessControlRequestMethod: ");
			stringBuilder.Append(this.AccessControlRequestMethod ?? "null");
			stringBuilder.Append(", RequestUri: ");
			stringBuilder.Append(this.RequestUri);
			stringBuilder.Append(", AccessControlRequestHeaders: {");
			stringBuilder.Append(string.Join(",", this.AccessControlRequestHeaders));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
