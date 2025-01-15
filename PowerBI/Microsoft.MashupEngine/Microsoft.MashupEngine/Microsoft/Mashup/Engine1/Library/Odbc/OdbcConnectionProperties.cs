using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005AF RID: 1455
	internal class OdbcConnectionProperties
	{
		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x0008BC91 File Offset: 0x00089E91
		// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x0008BC99 File Offset: 0x00089E99
		public string ConnectionString { get; set; }

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x0008BCA2 File Offset: 0x00089EA2
		// (set) Token: 0x06002DD3 RID: 11731 RVA: 0x0008BCAA File Offset: 0x00089EAA
		public string Catalog { get; set; }

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x0008BCB3 File Offset: 0x00089EB3
		// (set) Token: 0x06002DD5 RID: 11733 RVA: 0x0008BCBB File Offset: 0x00089EBB
		public int? ConnectionTimeout { get; set; }

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x0008BCC4 File Offset: 0x00089EC4
		// (set) Token: 0x06002DD7 RID: 11735 RVA: 0x0008BCCC File Offset: 0x00089ECC
		public int? CommandTimeout { get; set; }

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x0008BCD5 File Offset: 0x00089ED5
		// (set) Token: 0x06002DD9 RID: 11737 RVA: 0x0008BCDD File Offset: 0x00089EDD
		public bool IsMetadataConnection { get; set; }

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06002DDA RID: 11738 RVA: 0x0008BCE6 File Offset: 0x00089EE6
		// (set) Token: 0x06002DDB RID: 11739 RVA: 0x0008BCEE File Offset: 0x00089EEE
		public Dictionary<int, object> ConnectionAttributes { get; set; }

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x0008BCF7 File Offset: 0x00089EF7
		// (set) Token: 0x06002DDD RID: 11741 RVA: 0x0008BCFF File Offset: 0x00089EFF
		public OdbcFetchPlanFactory FetchPlanFactory { get; set; }

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x0008BD08 File Offset: 0x00089F08
		// (set) Token: 0x06002DDF RID: 11743 RVA: 0x0008BD10 File Offset: 0x00089F10
		public string SetUserQuery { get; set; }

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x0008BD19 File Offset: 0x00089F19
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x0008BD21 File Offset: 0x00089F21
		public string ClearUserQuery { get; set; }

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x0008BD2A File Offset: 0x00089F2A
		// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x0008BD32 File Offset: 0x00089F32
		public IDictionary<string, string> UserContextCredentialProperties { get; set; }

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x0008BD3B File Offset: 0x00089F3B
		// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x0008BD62 File Offset: 0x00089F62
		public OdbcCacheContext CacheContext
		{
			get
			{
				if (this.cacheContext == null)
				{
					return new OdbcCacheContext(this.ConnectionString, this.Catalog, new ResourceCredentialCollection());
				}
				return this.cacheContext;
			}
			set
			{
				this.cacheContext = value;
			}
		}

		// Token: 0x04001401 RID: 5121
		private OdbcCacheContext cacheContext;
	}
}
