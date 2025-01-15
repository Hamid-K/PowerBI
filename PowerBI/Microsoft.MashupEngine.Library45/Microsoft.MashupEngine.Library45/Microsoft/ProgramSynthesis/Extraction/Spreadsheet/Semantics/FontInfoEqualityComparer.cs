using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000ECF RID: 3791
	public class FontInfoEqualityComparer : IEqualityComparer<ICellStyleInfo>
	{
		// Token: 0x06006727 RID: 26407 RVA: 0x00002130 File Offset: 0x00000330
		private FontInfoEqualityComparer()
		{
		}

		// Token: 0x06006728 RID: 26408 RVA: 0x001502B4 File Offset: 0x0014E4B4
		public bool Equals(ICellStyleInfo x, ICellStyleInfo y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.FontName == y.FontName)
			{
				int? fontSize = x.FontSize;
				int? fontSize2 = y.FontSize;
				if (((fontSize.GetValueOrDefault() == fontSize2.GetValueOrDefault()) & (fontSize != null == (fontSize2 != null))) && x.Bold == y.Bold && x.Italic == y.Italic && x.Underline == y.Underline && x.Strikethrough == y.Strikethrough)
				{
					return object.Equals(x.Color, y.Color);
				}
			}
			return false;
		}

		// Token: 0x06006729 RID: 26409 RVA: 0x00150360 File Offset: 0x0014E560
		public int GetHashCode(ICellStyleInfo obj)
		{
			string fontName = obj.FontName;
			int? num2;
			int num = ((fontName != null) ? fontName.GetHashCode() : 0) ^ ((obj.FontSize != null) ? num2.GetValueOrDefault().GetHashCode() : 0) ^ obj.Bold.GetHashCode() ^ obj.Italic.GetHashCode() ^ obj.Underline.GetHashCode() ^ obj.Strikethrough.GetHashCode();
			ColorInfo color = obj.Color;
			return num ^ ((color != null) ? color.GetHashCode() : 0);
		}

		// Token: 0x04002D9F RID: 11679
		public static FontInfoEqualityComparer Instance = new FontInfoEqualityComparer();
	}
}
