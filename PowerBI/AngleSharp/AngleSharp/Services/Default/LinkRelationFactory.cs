using System;
using System.Collections.Generic;
using AngleSharp.Dom.Html;
using AngleSharp.Html;
using AngleSharp.Html.LinkRels;

namespace AngleSharp.Services.Default
{
	// Token: 0x0200004C RID: 76
	internal sealed class LinkRelationFactory : ILinkRelationFactory
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000B628 File Offset: 0x00009828
		public BaseLinkRelation Create(HtmlLinkElement link, string rel)
		{
			LinkRelationFactory.Creator creator = null;
			if (rel != null && this.creators.TryGetValue(rel, out creator))
			{
				return creator(link);
			}
			return null;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000B654 File Offset: 0x00009854
		public LinkRelationFactory()
		{
			Dictionary<string, LinkRelationFactory.Creator> dictionary = new Dictionary<string, LinkRelationFactory.Creator>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add(LinkRelNames.StyleSheet, (HtmlLinkElement link) => new StyleSheetLinkRelation(link));
			dictionary.Add(LinkRelNames.Import, (HtmlLinkElement link) => new ImportLinkRelation(link));
			this.creators = dictionary;
			base..ctor();
		}

		// Token: 0x040001CA RID: 458
		private readonly Dictionary<string, LinkRelationFactory.Creator> creators;

		// Token: 0x0200042E RID: 1070
		// (Invoke) Token: 0x060022B6 RID: 8886
		private delegate BaseLinkRelation Creator(HtmlLinkElement link);
	}
}
