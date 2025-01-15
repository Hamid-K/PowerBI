using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000AF RID: 175
	internal sealed class LimitWindowExpansionInstance : IStructuredToString
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000775F File Offset: 0x0000595F
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x00007767 File Offset: 0x00005967
		public List<Expression> Values { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00007770 File Offset: 0x00005970
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x00007778 File Offset: 0x00005978
		public List<LimitWindowExpansionValue> WindowValues { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00007781 File Offset: 0x00005981
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x00007789 File Offset: 0x00005989
		public List<LimitWindowExpansionInstance> Children { get; set; }

		// Token: 0x06000424 RID: 1060 RVA: 0x00007794 File Offset: 0x00005994
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.WriteProperty<List<Expression>>("PathValues", this.Values, false);
			if (!this.WindowValues.IsNullOrEmpty<LimitWindowExpansionValue>())
			{
				foreach (LimitWindowExpansionValue limitWindowExpansionValue in this.WindowValues)
				{
					builder.WriteProperty<List<Expression>>("WindowValues", limitWindowExpansionValue.Values, false);
					builder.WriteProperty<WindowKind>("WindowKind", limitWindowExpansionValue.WindowKind, false);
				}
			}
			if (!this.Children.IsNullOrEmpty<LimitWindowExpansionInstance>())
			{
				foreach (LimitWindowExpansionInstance limitWindowExpansionInstance in this.Children)
				{
					limitWindowExpansionInstance.WriteTo(builder);
				}
			}
		}
	}
}
