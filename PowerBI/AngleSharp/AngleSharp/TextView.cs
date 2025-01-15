using System;

namespace AngleSharp
{
	// Token: 0x02000018 RID: 24
	public class TextView
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000435A File Offset: 0x0000255A
		internal TextView(TextRange range, TextSource source)
		{
			this._range = range;
			this._source = source;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004370 File Offset: 0x00002570
		public TextRange Range
		{
			get
			{
				return this._range;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004378 File Offset: 0x00002578
		public string Text
		{
			get
			{
				int num = Math.Max(this._range.Start.Position - 1, 0);
				int num2 = this._range.End.Position + 1 - this._range.Start.Position;
				string text = this._source.Text;
				if (num + num2 > text.Length)
				{
					num2 = text.Length - num;
				}
				return text.Substring(num, num2);
			}
		}

		// Token: 0x0400003D RID: 61
		private readonly TextRange _range;

		// Token: 0x0400003E RID: 62
		private readonly TextSource _source;
	}
}
