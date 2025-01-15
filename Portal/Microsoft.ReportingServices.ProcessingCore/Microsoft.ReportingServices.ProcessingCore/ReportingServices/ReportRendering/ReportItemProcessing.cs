using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000028 RID: 40
	internal sealed class ReportItemProcessing : MemberBase
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x0000D5E5 File Offset: 0x0000B7E5
		internal ReportItemProcessing()
			: base(true)
		{
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
		internal ReportItemProcessing DeepClone()
		{
			ReportItemProcessing reportItemProcessing = new ReportItemProcessing();
			if (this.DefinitionName != null)
			{
				reportItemProcessing.DefinitionName = string.Copy(this.DefinitionName);
			}
			if (this.Label != null)
			{
				reportItemProcessing.Label = string.Copy(this.Label);
			}
			if (this.Bookmark != null)
			{
				reportItemProcessing.Bookmark = string.Copy(this.Bookmark);
			}
			if (this.Tooltip != null)
			{
				reportItemProcessing.Tooltip = string.Copy(this.Tooltip);
			}
			if (this.Height != null)
			{
				reportItemProcessing.Height = this.Height.DeepClone();
			}
			if (this.Width != null)
			{
				reportItemProcessing.Width = this.Width.DeepClone();
			}
			if (this.Top != null)
			{
				reportItemProcessing.Top = this.Top.DeepClone();
			}
			if (this.Left != null)
			{
				reportItemProcessing.Left = this.Left.DeepClone();
			}
			reportItemProcessing.ZIndex = this.ZIndex;
			reportItemProcessing.Hidden = this.Hidden;
			reportItemProcessing.SharedHidden = this.SharedHidden;
			Global.Tracer.Assert(this.SharedStyles == null && this.NonSharedStyles == null);
			return reportItemProcessing;
		}

		// Token: 0x040000D2 RID: 210
		internal string DefinitionName;

		// Token: 0x040000D3 RID: 211
		internal string Label;

		// Token: 0x040000D4 RID: 212
		internal string Bookmark;

		// Token: 0x040000D5 RID: 213
		internal string Tooltip;

		// Token: 0x040000D6 RID: 214
		internal ReportSize Height;

		// Token: 0x040000D7 RID: 215
		internal ReportSize Width;

		// Token: 0x040000D8 RID: 216
		internal ReportSize Top;

		// Token: 0x040000D9 RID: 217
		internal ReportSize Left;

		// Token: 0x040000DA RID: 218
		internal int ZIndex;

		// Token: 0x040000DB RID: 219
		internal bool Hidden;

		// Token: 0x040000DC RID: 220
		internal SharedHiddenState SharedHidden = SharedHiddenState.Never;

		// Token: 0x040000DD RID: 221
		internal DataValueInstanceList SharedStyles;

		// Token: 0x040000DE RID: 222
		internal DataValueInstanceList NonSharedStyles;
	}
}
