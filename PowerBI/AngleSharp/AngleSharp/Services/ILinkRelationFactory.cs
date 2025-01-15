using System;
using AngleSharp.Dom.Html;
using AngleSharp.Html.LinkRels;

namespace AngleSharp.Services
{
	// Token: 0x02000030 RID: 48
	internal interface ILinkRelationFactory
	{
		// Token: 0x06000133 RID: 307
		BaseLinkRelation Create(HtmlLinkElement link, string rel);
	}
}
