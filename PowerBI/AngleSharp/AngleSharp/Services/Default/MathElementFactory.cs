using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Dom.Mathml;
using AngleSharp.Html;

namespace AngleSharp.Services.Default
{
	// Token: 0x0200004E RID: 78
	internal sealed class MathElementFactory : IElementFactory<MathElement>
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000B900 File Offset: 0x00009B00
		public MathElement Create(Document document, string localName, string prefix = null)
		{
			MathElementFactory.Creator creator = null;
			if (this.creators.TryGetValue(localName, out creator))
			{
				return creator(document, prefix);
			}
			return new MathElement(document, localName, prefix, NodeFlags.None);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000B934 File Offset: 0x00009B34
		public MathElementFactory()
		{
			Dictionary<string, MathElementFactory.Creator> dictionary = new Dictionary<string, MathElementFactory.Creator>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add(TagNames.Mn, (Document document, string prefix) => new MathNumberElement(document, prefix));
			dictionary.Add(TagNames.Mo, (Document document, string prefix) => new MathOperatorElement(document, prefix));
			dictionary.Add(TagNames.Mi, (Document document, string prefix) => new MathIdentifierElement(document, prefix));
			dictionary.Add(TagNames.Ms, (Document document, string prefix) => new MathStringElement(document, prefix));
			dictionary.Add(TagNames.Mtext, (Document document, string prefix) => new MathTextElement(document, prefix));
			dictionary.Add(TagNames.AnnotationXml, (Document document, string prefix) => new MathAnnotationXmlElement(document, prefix));
			this.creators = dictionary;
			base..ctor();
		}

		// Token: 0x040001CC RID: 460
		private readonly Dictionary<string, MathElementFactory.Creator> creators;

		// Token: 0x02000430 RID: 1072
		// (Invoke) Token: 0x060022BE RID: 8894
		private delegate MathElement Creator(Document owner, string prefix);
	}
}
