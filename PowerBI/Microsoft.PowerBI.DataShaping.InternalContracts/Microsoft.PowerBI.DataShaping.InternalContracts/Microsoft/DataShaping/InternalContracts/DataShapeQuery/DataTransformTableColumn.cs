using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000080 RID: 128
	internal sealed class DataTransformTableColumn : IIdentifiable
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00006A81 File Offset: 0x00004C81
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00006A89 File Offset: 0x00004C89
		public Identifier Id { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00006A92 File Offset: 0x00004C92
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00006A9A File Offset: 0x00004C9A
		public Candidate<string> Role { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00006AA3 File Offset: 0x00004CA3
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00006AAB File Offset: 0x00004CAB
		public Expression Value { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.DataTransformTableColumn;
			}
		}
	}
}
