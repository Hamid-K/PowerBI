using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000203 RID: 515
	internal sealed class CssValue : CssNode, IEnumerable<CssToken>, IEnumerable
	{
		// Token: 0x0600136F RID: 4975 RVA: 0x0004A6BB File Offset: 0x000488BB
		private CssValue(CssToken token)
		{
			this._tokens = new List<CssToken> { token };
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0004A6D5 File Offset: 0x000488D5
		public CssValue(IEnumerable<CssToken> tokens)
		{
			this._tokens = new List<CssToken>(tokens);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0004A6E9 File Offset: 0x000488E9
		public static CssValue FromString(string text)
		{
			return new CssValue(new CssToken(CssTokenType.Ident, text, TextPosition.Empty));
		}

		// Token: 0x170004D3 RID: 1235
		public CssToken this[int index]
		{
			get
			{
				return this._tokens[index];
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0004A70A File Offset: 0x0004890A
		public int Count
		{
			get
			{
				return this._tokens.Count;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0004810B File Offset: 0x0004630B
		public string CssText
		{
			get
			{
				return this.ToCss();
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0004A717 File Offset: 0x00048917
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write(this._tokens.ToText());
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0004A72A File Offset: 0x0004892A
		public IEnumerator<CssToken> GetEnumerator()
		{
			return this._tokens.GetEnumerator();
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0004A73C File Offset: 0x0004893C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000AA4 RID: 2724
		private readonly List<CssToken> _tokens;

		// Token: 0x04000AA5 RID: 2725
		public static CssValue Initial = CssValue.FromString(Keywords.Initial);

		// Token: 0x04000AA6 RID: 2726
		public static CssValue Empty = new CssValue(Enumerable.Empty<CssToken>());
	}
}
