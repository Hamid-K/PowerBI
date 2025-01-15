using System;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Utils
{
	// Token: 0x02000037 RID: 55
	public class PowerBIDataSource
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B5F0 File Offset: 0x000097F0
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000B5F8 File Offset: 0x000097F8
		public string ConnectionString { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000B601 File Offset: 0x00009801
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000B609 File Offset: 0x00009809
		public string Username { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000B612 File Offset: 0x00009812
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000B61A File Offset: 0x0000981A
		public string Secret { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000B623 File Offset: 0x00009823
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000B62B File Offset: 0x0000982B
		public string DataSourceIdentifier { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000B634 File Offset: 0x00009834
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000B63C File Offset: 0x0000983C
		public DataModelDataSourceAuthType AuthType { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000B645 File Offset: 0x00009845
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000B64D File Offset: 0x0000984D
		public DataModelDataSourceType Type { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000B656 File Offset: 0x00009856
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000B65E File Offset: 0x0000985E
		public DataModelDataSourceKind Kind { get; set; }

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B668 File Offset: 0x00009868
		public DataSource ToModelDataSource()
		{
			return new DataSource
			{
				ConnectionString = this.ConnectionString,
				DataModelDataSource = new DataModelDataSource
				{
					AuthType = this.AuthType,
					Type = this.Type,
					Kind = this.Kind,
					Username = this.Username,
					Secret = this.Secret,
					ModelConnectionName = this.DataSourceIdentifier
				}
			};
		}
	}
}
