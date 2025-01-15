using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000BC RID: 188
	[DataContract(Name = "TranslatedSelect")]
	public sealed class TranslatedSelect
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000BB9B File Offset: 0x00009D9B
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x0000BBA3 File Offset: 0x00009DA3
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public string Name { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000BBAC File Offset: 0x00009DAC
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string ColumnName { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000BBBD File Offset: 0x00009DBD
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0000BBC5 File Offset: 0x00009DC5
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<TranslatedColumn> GroupColumns { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000BBCE File Offset: 0x00009DCE
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x0000BBD6 File Offset: 0x00009DD6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<TranslatedColumn> SortColumns { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000BBDF File Offset: 0x00009DDF
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x0000BBE7 File Offset: 0x00009DE7
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public TranslatedDynamicFormat DynamicFormat { get; set; }
	}
}
