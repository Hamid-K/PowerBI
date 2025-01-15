using System;

namespace Microsoft.PowerBI.ReportServer.AsServer.Mashup
{
	// Token: 0x02000024 RID: 36
	public class DataSourceKey
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004585 File Offset: 0x00002785
		// (set) Token: 0x060000BF RID: 191 RVA: 0x0000458D File Offset: 0x0000278D
		public string Kind { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004596 File Offset: 0x00002796
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000459E File Offset: 0x0000279E
		public string ConnectionString { get; set; }

		// Token: 0x060000C2 RID: 194 RVA: 0x000045A7 File Offset: 0x000027A7
		public override string ToString()
		{
			return this.Kind + "-" + this.ConnectionString;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000045BF File Offset: 0x000027BF
		public override int GetHashCode()
		{
			return this.Kind.GetHashCode() ^ this.ConnectionString.GetHashCode();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000045D8 File Offset: 0x000027D8
		public override bool Equals(object obj)
		{
			DataSourceKey dataSourceKey = obj as DataSourceKey;
			return dataSourceKey != null && dataSourceKey.Kind.Equals(this.Kind, StringComparison.InvariantCultureIgnoreCase) && dataSourceKey.ConnectionString.Equals(this.ConnectionString, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
