using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000533 RID: 1331
	public class IdentityTextColumn : TextColumnBase, IDynamicWidthTextColumn, ITextColumn
	{
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x00058B83 File Offset: 0x00056D83
		// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x00058B8B File Offset: 0x00056D8B
		public int? LeftPadding { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x00058B94 File Offset: 0x00056D94
		// (set) Token: 0x06001DF9 RID: 7673 RVA: 0x00058B9C File Offset: 0x00056D9C
		public int? MaximumWidth { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0000A5FD File Offset: 0x000087FD
		public int MinimumWidth
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x00058BA5 File Offset: 0x00056DA5
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x00058BAD File Offset: 0x00056DAD
		public int? RightPadding { get; set; }

		// Token: 0x06001DFD RID: 7677 RVA: 0x00058BB8 File Offset: 0x00056DB8
		public virtual string FormatValue(ITextRow row)
		{
			return row.Id.GetValueOrDefault().ToString("N0").PadLeft(base.Width);
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x00058BEB File Offset: 0x00056DEB
		public override IReadOnlyList<string> Lines(ITextRow row)
		{
			return new string[] { this.FormatValue(row) };
		}
	}
}
