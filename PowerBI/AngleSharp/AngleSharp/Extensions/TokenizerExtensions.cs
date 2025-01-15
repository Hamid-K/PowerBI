using System;
using System.Collections.Generic;
using AngleSharp.Dom.Events;
using AngleSharp.Html;
using AngleSharp.Parser.Html;
using AngleSharp.Services;

namespace AngleSharp.Extensions
{
	// Token: 0x020000F9 RID: 249
	public static class TokenizerExtensions
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x0003700E File Offset: 0x0003520E
		public static IEnumerable<HtmlToken> Tokenize(this TextSource source, IEntityProvider provider = null, EventHandler<HtmlErrorEvent> errorHandler = null)
		{
			IEntityProvider entityProvider = provider ?? HtmlEntityService.Resolver;
			HtmlTokenizer htmlTokenizer = new HtmlTokenizer(source, entityProvider);
			HtmlToken token = null;
			if (errorHandler != null)
			{
				htmlTokenizer.Error += errorHandler;
			}
			do
			{
				token = htmlTokenizer.Get();
				yield return token;
			}
			while (token.Type != HtmlTokenType.EndOfFile);
			yield break;
		}
	}
}
