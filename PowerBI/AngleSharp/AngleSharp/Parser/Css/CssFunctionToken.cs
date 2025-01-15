using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000083 RID: 131
	internal sealed class CssFunctionToken : CssToken, IEnumerable<CssToken>, IEnumerable
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0001BB37 File Offset: 0x00019D37
		public CssFunctionToken(string data, TextPosition position)
			: base(CssTokenType.Function, data, position)
		{
			this._arguments = new List<CssToken>();
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001BB50 File Offset: 0x00019D50
		public IEnumerable<CssToken> ArgumentTokens
		{
			get
			{
				int num = this._arguments.Count - 1;
				if (num >= 0 && this._arguments[num].Type == CssTokenType.RoundBracketClose)
				{
					num--;
				}
				return this._arguments.Take(1 + num);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001BB96 File Offset: 0x00019D96
		public void AddArgumentToken(CssToken token)
		{
			this._arguments.Add(token);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001BBA4 File Offset: 0x00019DA4
		public IEnumerator<CssToken> GetEnumerator()
		{
			return this._arguments.GetEnumerator();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001BBB6 File Offset: 0x00019DB6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001BBBE File Offset: 0x00019DBE
		public override string ToValue()
		{
			return base.Data + "(" + this._arguments.ToText();
		}

		// Token: 0x04000330 RID: 816
		private readonly List<CssToken> _arguments;
	}
}
