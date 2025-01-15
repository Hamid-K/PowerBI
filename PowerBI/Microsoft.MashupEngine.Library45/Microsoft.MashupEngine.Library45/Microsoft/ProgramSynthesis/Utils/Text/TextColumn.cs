using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000530 RID: 1328
	public class TextColumn : TextColumnBase, IDynamicWidthTextColumn, ITextColumn
	{
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x000587A1 File Offset: 0x000569A1
		// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x000587A9 File Offset: 0x000569A9
		public int DataColumnIndex { get; set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x000587B2 File Offset: 0x000569B2
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x000587BA File Offset: 0x000569BA
		public string Format { get; set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x000587C3 File Offset: 0x000569C3
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x000587CB File Offset: 0x000569CB
		public int? LeftPadding { get; set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000587D4 File Offset: 0x000569D4
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x000587DC File Offset: 0x000569DC
		public int? MaximumWidth { get; set; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x000587E5 File Offset: 0x000569E5
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x000587ED File Offset: 0x000569ED
		public int MinimumWidth { get; set; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x000587F6 File Offset: 0x000569F6
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x000587FE File Offset: 0x000569FE
		public string NullProjection { get; set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x00058807 File Offset: 0x00056A07
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x0005880F File Offset: 0x00056A0F
		public int? RightPadding { get; set; }

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00058818 File Offset: 0x00056A18
		public string FormatValue(ITextRow row)
		{
			DataTextRow dataTextRow = row as DataTextRow;
			if (dataTextRow == null)
			{
				return string.Empty;
			}
			object obj = dataTextRow[this.DataColumnIndex] ?? this.NullProjection;
			if (this.Format != null)
			{
				return string.Format("{0:" + this.Format + "}", obj);
			}
			return ((obj != null) ? obj.ToString() : null) ?? string.Empty;
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x00058888 File Offset: 0x00056A88
		public override IReadOnlyList<string> Lines(ITextRow row)
		{
			IReadOnlyList<string> readOnlyList;
			if (this._lineCache.TryGetValue(row, out readOnlyList))
			{
				return readOnlyList;
			}
			string text = this.FormatValue(row);
			string[] array;
			if (!text.Contains(Environment.NewLine))
			{
				(array = new string[1])[0] = text;
			}
			else
			{
				array = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			}
			string[] array2 = array;
			if (this.MaximumWidth == null)
			{
				return this._lineCache[row] = array2;
			}
			if (array2.Length == 1)
			{
				int length = array2[0].Length;
				int? maximumWidth = this.MaximumWidth;
				if ((length < maximumWidth.GetValueOrDefault()) & (maximumWidth != null))
				{
					return this._lineCache[row] = array2;
				}
			}
			return this._lineCache[row] = array2.SelectMany((string line) => TextColumn.Partition(line, this.MaximumWidth.Value)).ToList<string>();
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00058964 File Offset: 0x00056B64
		private static IReadOnlyList<string> Partition(string text, int maxLength)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrEmpty(text))
			{
				list.Add(string.Empty);
				return list;
			}
			while (text.Length > 0)
			{
				if (text.Length <= maxLength)
				{
					list.Add(text);
					break;
				}
				string text2 = text.Substring(0, maxLength);
				if (char.IsWhiteSpace(text[maxLength]))
				{
					list.Add(text2);
					text = text.Substring(text2.Length + 1);
				}
				else
				{
					int num = text2.LastIndexOf(TextColumnBase.Space);
					if (num != -1)
					{
						text2 = text2.Substring(0, num);
					}
					text = text.Substring(text2.Length + ((num != -1) ? 1 : 0));
					list.Add(text2);
				}
			}
			return list;
		}

		// Token: 0x04000E69 RID: 3689
		private readonly Dictionary<ITextRow, IReadOnlyList<string>> _lineCache = new Dictionary<ITextRow, IReadOnlyList<string>>();
	}
}
