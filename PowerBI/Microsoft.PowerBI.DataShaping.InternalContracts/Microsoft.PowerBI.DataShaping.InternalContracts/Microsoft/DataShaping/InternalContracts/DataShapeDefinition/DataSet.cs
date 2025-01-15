using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000118 RID: 280
	[DataContract]
	internal sealed class DataSet
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0000F76B File Offset: 0x0000D96B
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0000F773 File Offset: 0x0000D973
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Id { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0000F77C File Offset: 0x0000D97C
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0000F784 File Offset: 0x0000D984
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal string DataSourceId { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0000F78D File Offset: 0x0000D98D
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x0000F795 File Offset: 0x0000D995
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal string Query { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0000F79E File Offset: 0x0000D99E
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x0000F7A6 File Offset: 0x0000D9A6
		[DataMember(EmitDefaultValue = false, Order = 4)]
		internal IList<ResultTable> ResultTables { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0000F7AF File Offset: 0x0000D9AF
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x0000F7B7 File Offset: 0x0000D9B7
		[DataMember(EmitDefaultValue = false, Order = 5)]
		internal IList<ItemSourceLocation> QuerySourceMap { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		[DataMember(EmitDefaultValue = false, Order = 6)]
		internal IList<QueryParameter> QueryParameters { get; set; }
	}
}
