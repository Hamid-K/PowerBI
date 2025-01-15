using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007B8 RID: 1976
	public class ColumnMapping
	{
		// Token: 0x06003EDE RID: 16094 RVA: 0x000D2900 File Offset: 0x000D0B00
		public ColumnMapping(string columnName, string dataType, bool isVar, string value)
		{
			this.ColumnName = columnName;
			this.DataType = dataType;
			this.IsVariable = isVar;
			this.Value = value;
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06003EDF RID: 16095 RVA: 0x000D2925 File Offset: 0x000D0B25
		// (set) Token: 0x06003EE0 RID: 16096 RVA: 0x000D292D File Offset: 0x000D0B2D
		public string ColumnName { get; set; }

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x000D2936 File Offset: 0x000D0B36
		// (set) Token: 0x06003EE2 RID: 16098 RVA: 0x000D293E File Offset: 0x000D0B3E
		public string DataType { get; set; }

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003EE3 RID: 16099 RVA: 0x000D2947 File Offset: 0x000D0B47
		// (set) Token: 0x06003EE4 RID: 16100 RVA: 0x000D294F File Offset: 0x000D0B4F
		public bool IsVariable { get; set; }

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x000D2958 File Offset: 0x000D0B58
		// (set) Token: 0x06003EE6 RID: 16102 RVA: 0x000D2960 File Offset: 0x000D0B60
		public string Value { get; set; }
	}
}
