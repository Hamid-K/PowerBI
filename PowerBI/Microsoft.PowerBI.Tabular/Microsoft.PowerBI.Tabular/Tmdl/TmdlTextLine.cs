using System;
using Microsoft.AnalysisServices.Tabular.Extensions;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200014D RID: 333
	internal struct TmdlTextLine
	{
		// Token: 0x06001565 RID: 5477 RVA: 0x0008FB2C File Offset: 0x0008DD2C
		public TmdlTextLine(string line)
		{
			this.OriginalText = line;
			if (line.Length == 0)
			{
				this.Type = TmdlLineType.Empty;
				this.Indentation = Indentation.Empty;
				this.StartColumn = 1;
				this.Text = string.Empty;
				this.Keyword = null;
				this.FirstTokenIndex = -1;
				return;
			}
			int num = 0;
			while (num < line.Length && (line[num] == ' ' || line[num] == '\t'))
			{
				num++;
			}
			this.Indentation = new Indentation(line.Substring(0, num));
			this.StartColumn = this.Indentation.Size + 1;
			if (num == line.Length)
			{
				this.Type = TmdlLineType.Empty;
				this.Text = string.Empty;
				this.Keyword = null;
				this.FirstTokenIndex = -1;
				return;
			}
			if (num > 0)
			{
				this.Text = line.SubstringAndTrim(num, false, true);
			}
			else
			{
				this.Text = line.TrimEnd(Array.Empty<char>());
			}
			if (this.Text.StartsWith("///"))
			{
				this.Type = TmdlLineType.Description;
				this.Text = this.Text.SubstringAndTrim(TmdlTextLine.DescriptionMarkerLength, true, false);
				this.Keyword = null;
				this.FirstTokenIndex = -1;
				return;
			}
			bool flag;
			if (this.Text.StartsWith("ref", StringComparison.InvariantCultureIgnoreCase) && this.Text.Length > TmdlTextLine.ReferenceMarkerLength && (this.Text[TmdlTextLine.ReferenceMarkerLength] == ' ' || this.Text[TmdlTextLine.ReferenceMarkerLength] == '\t'))
			{
				num = TmdlTextLine.ReferenceMarkerLength + 1;
				while (num < this.Text.Length && char.IsWhiteSpace(this.Text, num))
				{
					num++;
				}
				flag = true;
			}
			else
			{
				flag = false;
				num = 0;
			}
			this.FirstTokenIndex = num;
			while (this.FirstTokenIndex < this.Text.Length && char.IsLetterOrDigit(this.Text, this.FirstTokenIndex))
			{
				this.FirstTokenIndex++;
			}
			this.Keyword = null;
			if (this.FirstTokenIndex > num && this.FirstTokenIndex <= this.Text.Length && (this.FirstTokenIndex == this.Text.Length || char.IsWhiteSpace(this.Text, this.FirstTokenIndex) || this.Text[this.FirstTokenIndex] == ':' || this.Text[this.FirstTokenIndex] == '='))
			{
				this.Keyword = this.Text.Substring(num, this.FirstTokenIndex - num);
			}
			while (this.FirstTokenIndex < this.Text.Length && char.IsWhiteSpace(this.Text, this.FirstTokenIndex))
			{
				this.FirstTokenIndex++;
			}
			if (flag)
			{
				this.Type = TmdlLineType.ReferenceObject;
				return;
			}
			if (string.IsNullOrEmpty(this.Keyword))
			{
				this.Type = TmdlLineType.Other;
				this.FirstTokenIndex = -1;
				return;
			}
			if (this.FirstTokenIndex == this.Text.Length)
			{
				this.Type = TmdlLineType.UnnamedObjectOrSimplifiedProperty;
				return;
			}
			if (this.Text[this.FirstTokenIndex] == ':' && this.FirstTokenIndex + 1 < this.Text.Length && this.Text[this.FirstTokenIndex + 1] == '=')
			{
				this.Type = TmdlLineType.OldSyntaxExpressionProperty;
				this.FirstTokenIndex += 2;
				while (this.FirstTokenIndex < this.Text.Length)
				{
					if (!char.IsWhiteSpace(this.Text, this.FirstTokenIndex))
					{
						return;
					}
					this.FirstTokenIndex++;
				}
			}
			else if (this.Text[this.FirstTokenIndex] == ':')
			{
				this.Type = TmdlLineType.Property;
				this.FirstTokenIndex++;
				while (this.FirstTokenIndex < this.Text.Length)
				{
					if (!char.IsWhiteSpace(this.Text, this.FirstTokenIndex))
					{
						return;
					}
					this.FirstTokenIndex++;
				}
			}
			else if (this.Text[this.FirstTokenIndex] == '=')
			{
				this.Type = TmdlLineType.ElementWithDefaultPropertyOrExpression;
				this.FirstTokenIndex++;
				while (this.FirstTokenIndex < this.Text.Length)
				{
					if (!char.IsWhiteSpace(this.Text, this.FirstTokenIndex))
					{
						return;
					}
					this.FirstTokenIndex++;
				}
			}
			else
			{
				num = this.FirstTokenIndex;
				bool flag2 = false;
				while (num < this.Text.Length && (flag2 || (!char.IsWhiteSpace(this.Text, num) && this.Text[num] != '=')))
				{
					if (this.Text[num] == '\'')
					{
						flag2 = !flag2;
					}
					num++;
				}
				if (flag2)
				{
					this.Type = TmdlLineType.Other;
					return;
				}
				while (num < this.Text.Length && char.IsWhiteSpace(this.Text, num))
				{
					num++;
				}
				if (num == this.Text.Length)
				{
					this.Type = TmdlLineType.NamedObject;
					return;
				}
				if (this.Text[num] == '=')
				{
					this.Type = TmdlLineType.NamedObjectWithDefaultProperty;
					return;
				}
				this.Type = TmdlLineType.Other;
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0009002C File Offset: 0x0008E22C
		internal TmdlTextLine(string text, string keyword, TmdlLineType type, int? firstTokenIndex)
		{
			this.OriginalText = text;
			this.Type = type;
			this.Indentation = Indentation.Empty;
			this.StartColumn = 1;
			this.Text = text;
			this.Keyword = keyword;
			if (firstTokenIndex != null)
			{
				this.FirstTokenIndex = firstTokenIndex.Value;
				return;
			}
			this.FirstTokenIndex = text.IndexOf(keyword, StringComparison.InvariantCultureIgnoreCase) + keyword.Length;
			while (this.FirstTokenIndex < text.Length && (char.IsWhiteSpace(text, this.FirstTokenIndex) || text[this.FirstTokenIndex] == ':' || text[this.FirstTokenIndex] == '='))
			{
				this.FirstTokenIndex++;
			}
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x000900DF File Offset: 0x0008E2DF
		public override string ToString()
		{
			return this.OriginalText;
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x000900E7 File Offset: 0x0008E2E7
		internal bool IsValid
		{
			get
			{
				return this.Type > TmdlLineType.Unknown;
			}
		}

		// Token: 0x040003B6 RID: 950
		public readonly string OriginalText;

		// Token: 0x040003B7 RID: 951
		public readonly TmdlLineType Type;

		// Token: 0x040003B8 RID: 952
		public readonly Indentation Indentation;

		// Token: 0x040003B9 RID: 953
		public readonly int StartColumn;

		// Token: 0x040003BA RID: 954
		public readonly string Text;

		// Token: 0x040003BB RID: 955
		public readonly string Keyword;

		// Token: 0x040003BC RID: 956
		internal readonly int FirstTokenIndex;

		// Token: 0x040003BD RID: 957
		private static readonly int DescriptionMarkerLength = "///".Length;

		// Token: 0x040003BE RID: 958
		private static readonly int ReferenceMarkerLength = "ref".Length;

		// Token: 0x0200032F RID: 815
		public static class TypeMarker
		{
			// Token: 0x04000DF1 RID: 3569
			public const string Description = "///";

			// Token: 0x04000DF2 RID: 3570
			public const string Reference = "ref";
		}
	}
}
