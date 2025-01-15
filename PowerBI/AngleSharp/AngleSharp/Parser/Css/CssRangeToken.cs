using System;
using System.Collections.Generic;
using System.Globalization;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000086 RID: 134
	internal sealed class CssRangeToken : CssToken
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0001BCAF File Offset: 0x00019EAF
		public CssRangeToken(string range, TextPosition position)
			: base(CssTokenType.Range, range, position)
		{
			this._start = range.Replace('?', '0');
			this._end = range.Replace('?', 'F');
			this._range = this.GetRange();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001BCE7 File Offset: 0x00019EE7
		public CssRangeToken(string start, string end, TextPosition position)
			: base(CssTokenType.Range, start + "-" + end, position)
		{
			this._start = start;
			this._end = end;
			this._range = this.GetRange();
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001BD18 File Offset: 0x00019F18
		public bool IsEmpty
		{
			get
			{
				return this._range == null || this._range.Length == 0;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001BD2E File Offset: 0x00019F2E
		public string Start
		{
			get
			{
				return this._start;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001BD36 File Offset: 0x00019F36
		public string End
		{
			get
			{
				return this._end;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001BD3E File Offset: 0x00019F3E
		public string[] SelectedRange
		{
			get
			{
				return this._range;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001BD48 File Offset: 0x00019F48
		private string[] GetRange()
		{
			int i = int.Parse(this._start, NumberStyles.HexNumber);
			if (i > 1114111)
			{
				return null;
			}
			if (this._end != null)
			{
				List<string> list = new List<string>();
				int num = int.Parse(this._end, NumberStyles.HexNumber);
				if (num > 1114111)
				{
					num = 1114111;
				}
				while (i <= num)
				{
					list.Add(i.ConvertFromUtf32());
					i++;
				}
				return list.ToArray();
			}
			return new string[] { i.ConvertFromUtf32() };
		}

		// Token: 0x04000332 RID: 818
		private readonly string[] _range;

		// Token: 0x04000333 RID: 819
		private readonly string _start;

		// Token: 0x04000334 RID: 820
		private readonly string _end;
	}
}
