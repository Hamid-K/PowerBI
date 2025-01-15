using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000B3 RID: 179
	[DataContract(Name = "TranslatedQuery")]
	public sealed class TranslatedQuery
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000B845 File Offset: 0x00009A45
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0000B84D File Offset: 0x00009A4D
		[DataMember(Name = "Version", IsRequired = true, Order = 0)]
		public int Version { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000B856 File Offset: 0x00009A56
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0000B85E File Offset: 0x00009A5E
		[DataMember(Name = "Query", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string Query { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000B867 File Offset: 0x00009A67
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0000B86F File Offset: 0x00009A6F
		[DataMember(Name = "Schema", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public TranslatedQuerySchema Schema { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000B878 File Offset: 0x00009A78
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0000B880 File Offset: 0x00009A80
		[DataMember(Name = "Warnings", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<TranslateQueryMessage> Warnings { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000B889 File Offset: 0x00009A89
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0000B891 File Offset: 0x00009A91
		[DataMember(Name = "Error", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public ODataError Error { get; set; }
	}
}
