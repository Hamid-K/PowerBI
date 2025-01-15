using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Dom.Svg;
using AngleSharp.Html;

namespace AngleSharp.Services.Default
{
	// Token: 0x02000055 RID: 85
	internal sealed class SvgElementFactory : IElementFactory<SvgElement>
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x0000CC54 File Offset: 0x0000AE54
		public SvgElement Create(Document document, string localName, string prefix = null)
		{
			SvgElementFactory.Creator creator = null;
			if (this.creators.TryGetValue(localName, out creator))
			{
				return creator(document, prefix);
			}
			return new SvgElement(document, localName, null, NodeFlags.None);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000CC88 File Offset: 0x0000AE88
		public SvgElementFactory()
		{
			Dictionary<string, SvgElementFactory.Creator> dictionary = new Dictionary<string, SvgElementFactory.Creator>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add(TagNames.Svg, (Document document, string prefix) => new SvgSvgElement(document, prefix));
			dictionary.Add(TagNames.Circle, (Document document, string prefix) => new SvgCircleElement(document, prefix));
			dictionary.Add(TagNames.Desc, (Document document, string prefix) => new SvgDescElement(document, prefix));
			dictionary.Add(TagNames.ForeignObject, (Document document, string prefix) => new SvgForeignObjectElement(document, prefix));
			dictionary.Add(TagNames.Title, (Document document, string prefix) => new SvgTitleElement(document, prefix));
			this.creators = dictionary;
			base..ctor();
		}

		// Token: 0x040001D2 RID: 466
		private readonly Dictionary<string, SvgElementFactory.Creator> creators;

		// Token: 0x02000436 RID: 1078
		// (Invoke) Token: 0x06002322 RID: 8994
		private delegate SvgElement Creator(Document owner, string prefix);
	}
}
