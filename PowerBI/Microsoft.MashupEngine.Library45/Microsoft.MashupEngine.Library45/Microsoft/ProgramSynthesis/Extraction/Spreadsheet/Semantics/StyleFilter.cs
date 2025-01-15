using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000ED9 RID: 3801
	public class StyleFilter
	{
		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x0600677C RID: 26492 RVA: 0x00151708 File Offset: 0x0014F908
		public bool Bold { get; }

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x0600677D RID: 26493 RVA: 0x00151710 File Offset: 0x0014F910
		public bool Italic { get; }

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x0600677E RID: 26494 RVA: 0x00151718 File Offset: 0x0014F918
		public bool Underline { get; }

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x0600677F RID: 26495 RVA: 0x00151720 File Offset: 0x0014F920
		public bool Merged { get; }

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06006780 RID: 26496 RVA: 0x00151728 File Offset: 0x0014F928
		public bool NonNumeric { get; }

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06006781 RID: 26497 RVA: 0x00151730 File Offset: 0x0014F930
		public string FontName { get; }

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06006782 RID: 26498 RVA: 0x00151738 File Offset: 0x0014F938
		public int? FontSize { get; }

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06006783 RID: 26499 RVA: 0x00151740 File Offset: 0x0014F940
		public AxisAligned<string> Alignment { get; }

		// Token: 0x06006784 RID: 26500 RVA: 0x00151748 File Offset: 0x0014F948
		public StyleFilter(bool bold = false, bool italic = false, bool underline = false, bool merged = false, bool nonNumeric = false, string fontName = null, int? fontSize = null, AxisAligned<string> alignment = null)
		{
			if (!bold && !italic && !underline && !merged && fontName == null && fontSize == null && (alignment == null || alignment.Horizontal == null) && (alignment == null || alignment.Vertical == null))
			{
				throw new ArgumentException("Filter may not be empty.");
			}
			this.Bold = bold;
			this.Italic = italic;
			this.Underline = underline;
			this.Merged = merged;
			this.NonNumeric = nonNumeric;
			this.FontName = fontName;
			this.FontSize = fontSize;
			this.Alignment = (((alignment == null || alignment.Horizontal == null) && (alignment == null || alignment.Vertical == null)) ? null : alignment);
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x001517F4 File Offset: 0x0014F9F4
		public bool Matches(ISpreadsheetCell cell)
		{
			if (this.Merged && cell != null && cell.Span.Width() == 1)
			{
				return false;
			}
			double num;
			if (this.NonNumeric && double.TryParse((cell != null) ? cell.AsString : null, out num))
			{
				return false;
			}
			ICellStyleInfo cellStyleInfo = ((cell != null) ? cell.StyleInfo : null);
			if (cellStyleInfo == null && (this.Bold || this.Italic || this.Underline || this.FontName != null || this.FontSize != null || this.Alignment != null))
			{
				return false;
			}
			if (this.Bold && !cellStyleInfo.Bold)
			{
				return false;
			}
			if (this.Italic && !cellStyleInfo.Italic)
			{
				return false;
			}
			if (this.Underline && !cellStyleInfo.Underline)
			{
				return false;
			}
			if (this.FontName != null && cellStyleInfo.FontName != this.FontName)
			{
				return false;
			}
			if (this.FontSize != null)
			{
				int? fontSize = cellStyleInfo.FontSize;
				int? fontSize2 = this.FontSize;
				if (!((fontSize.GetValueOrDefault() == fontSize2.GetValueOrDefault()) & (fontSize != null == (fontSize2 != null))))
				{
					return false;
				}
			}
			if (this.Alignment != null)
			{
				if (cellStyleInfo.Alignment == null)
				{
					return false;
				}
				if (this.Alignment.Horizontal != null && cellStyleInfo.Alignment.Horizontal != this.Alignment.Horizontal)
				{
					return false;
				}
				if (this.Alignment.Vertical != null && cellStyleInfo.Alignment.Vertical != this.Alignment.Vertical)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006786 RID: 26502 RVA: 0x0015198A File Offset: 0x0014FB8A
		public override string ToString()
		{
			return string.Join("|", this.<ToString>g__Filters|26_0());
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x0015199C File Offset: 0x0014FB9C
		[CompilerGenerated]
		private IEnumerable<string> <ToString>g__Filters|26_0()
		{
			if (this.Bold)
			{
				yield return "bold";
			}
			if (this.Italic)
			{
				yield return "italic";
			}
			if (this.Underline)
			{
				yield return "underline";
			}
			if (this.FontName != null)
			{
				yield return "name=" + this.FontName;
			}
			if (this.FontSize != null)
			{
				yield return "size=" + this.FontSize.ToString();
			}
			if (this.Merged)
			{
				yield return "merged";
			}
			if (this.NonNumeric)
			{
				yield return "nonNumeric";
			}
			AxisAligned<string> alignment = this.Alignment;
			if (((alignment != null) ? alignment.Horizontal : null) != null)
			{
				yield return "horizontal=" + this.Alignment.Horizontal;
			}
			AxisAligned<string> alignment2 = this.Alignment;
			if (((alignment2 != null) ? alignment2.Vertical : null) != null)
			{
				yield return "vertical=" + this.Alignment.Vertical;
			}
			yield break;
		}
	}
}
