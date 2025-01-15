using System;

namespace Microsoft.PowerBI.ReportServer.PbixLib.Parsing
{
	// Token: 0x02000005 RID: 5
	public class PbixDataSource
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002188 File Offset: 0x00000388
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002190 File Offset: 0x00000390
		public string ConnectionString { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002199 File Offset: 0x00000399
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021A1 File Offset: 0x000003A1
		public string Username { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021AA File Offset: 0x000003AA
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021B2 File Offset: 0x000003B2
		public string Secret { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021BB File Offset: 0x000003BB
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021C3 File Offset: 0x000003C3
		public string DataSourceIdentifier { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021CC File Offset: 0x000003CC
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000021D4 File Offset: 0x000003D4
		public AuthorizationType AuthType { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021DD File Offset: 0x000003DD
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000021E5 File Offset: 0x000003E5
		public AccessType Type { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021EE File Offset: 0x000003EE
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000021F6 File Offset: 0x000003F6
		public SourceKind Kind { get; set; }
	}
}
