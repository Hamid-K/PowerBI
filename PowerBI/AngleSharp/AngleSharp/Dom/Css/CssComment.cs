using System;
using System.IO;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020001FB RID: 507
	internal sealed class CssComment : CssNode, ICssComment, ICssNode, IStyleFormattable
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x00047B32 File Offset: 0x00045D32
		public CssComment(string data)
		{
			this._data = data;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x00047B41 File Offset: 0x00045D41
		public string Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00047B49 File Offset: 0x00045D49
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write(formatter.Comment(this._data));
		}

		// Token: 0x04000A89 RID: 2697
		private readonly string _data;
	}
}
