using System;

namespace Microsoft.Mashup.Engine1.Library.Sql
{
	// Token: 0x020003B0 RID: 944
	public class SqlAadAuthException : Exception
	{
		// Token: 0x06002118 RID: 8472 RVA: 0x000593CA File Offset: 0x000575CA
		public SqlAadAuthException(string authority, string resource)
		{
			this.authority = authority;
			this.resource = resource;
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x000593E0 File Offset: 0x000575E0
		public string Authority
		{
			get
			{
				return this.authority;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x000593E8 File Offset: 0x000575E8
		public string Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x04000C7E RID: 3198
		private readonly string authority;

		// Token: 0x04000C7F RID: 3199
		private readonly string resource;
	}
}
