using System;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000106 RID: 262
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public abstract class PropertyTermBaseBinding : ModelEntityTermBinding
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000092A4 File Offset: 0x000074A4
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x000092AC File Offset: 0x000074AC
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string ConceptualProperty { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000092B5 File Offset: 0x000074B5
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x000092BD File Offset: 0x000074BD
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 13)]
		public string ConceptualColumn { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000092C6 File Offset: 0x000074C6
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x000092CE File Offset: 0x000074CE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 16)]
		public string ConceptualMeasure { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000092D7 File Offset: 0x000074D7
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x000092DF File Offset: 0x000074DF
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string ConceptualHierarchy { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x000092E8 File Offset: 0x000074E8
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x000092F0 File Offset: 0x000074F0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string ConceptualHierarchyLevel { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x000092F9 File Offset: 0x000074F9
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x00009301 File Offset: 0x00007501
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public string ConceptualVariationSource { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000930A File Offset: 0x0000750A
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x00009312 File Offset: 0x00007512
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public string ConceptualVariationSet { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000931B File Offset: 0x0000751B
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00009323 File Offset: 0x00007523
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public DataType DataType { get; set; }

		// Token: 0x0600052A RID: 1322 RVA: 0x0000932C File Offset: 0x0000752C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString(), 50);
			if (!string.IsNullOrEmpty(this.ConceptualVariationSource))
			{
				stringBuilder.Append("_");
				stringBuilder.Append(this.ConceptualVariationSource);
				stringBuilder.Append("_");
				stringBuilder.Append(this.ConceptualVariationSet);
			}
			if (!string.IsNullOrEmpty(this.ConceptualProperty))
			{
				stringBuilder.Append("_");
				stringBuilder.Append(this.ConceptualProperty);
			}
			else
			{
				if (string.IsNullOrEmpty(this.ConceptualHierarchy))
				{
					throw Contract.ExceptNotSupported("Invalid PropertyTermBaseBinding");
				}
				stringBuilder.Append("_");
				stringBuilder.Append(this.ConceptualHierarchy);
				if (!string.IsNullOrEmpty(this.ConceptualHierarchyLevel))
				{
					stringBuilder.Append("_");
					stringBuilder.Append(this.ConceptualHierarchyLevel);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
