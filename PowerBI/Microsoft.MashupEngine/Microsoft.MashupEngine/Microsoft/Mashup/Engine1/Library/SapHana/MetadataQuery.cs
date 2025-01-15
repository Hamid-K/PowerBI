using System;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000419 RID: 1049
	internal class MetadataQuery : MetadataQueryBase
	{
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x00065092 File Offset: 0x00063292
		// (set) Token: 0x060023D5 RID: 9173 RVA: 0x0006509A File Offset: 0x0006329A
		public string Query { get; set; }

		// Token: 0x060023D6 RID: 9174 RVA: 0x000650A3 File Offset: 0x000632A3
		public override string GetQuery(SapHanaOdbcDataSource dataSource)
		{
			return this.Query;
		}
	}
}
