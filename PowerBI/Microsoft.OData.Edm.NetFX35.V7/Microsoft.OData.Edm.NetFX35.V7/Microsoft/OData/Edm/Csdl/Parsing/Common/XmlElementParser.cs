using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001B5 RID: 437
	internal abstract class XmlElementParser
	{
		// Token: 0x06000C41 RID: 3137 RVA: 0x00023516 File Offset: 0x00021716
		protected XmlElementParser(string elementName, Dictionary<string, XmlElementParser> children)
		{
			this.ElementName = elementName;
			this.childParsers = children;
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0002352C File Offset: 0x0002172C
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x00023534 File Offset: 0x00021734
		internal string ElementName { get; private set; }

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002353D File Offset: 0x0002173D
		public void AddChildParser(XmlElementParser child)
		{
			this.childParsers[child.ElementName] = child;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00023554 File Offset: 0x00021754
		internal static XmlElementParser<TResult> Create<TResult>(string elementName, Func<XmlElementInfo, XmlElementValueCollection, TResult> parserFunc, IEnumerable<XmlElementParser> childParsers, IEnumerable<XmlElementParser> descendantParsers)
		{
			Dictionary<string, XmlElementParser> dictionary = null;
			if (childParsers != null)
			{
				dictionary = Enumerable.ToDictionary<XmlElementParser, string>(childParsers, (XmlElementParser p) => p.ElementName);
			}
			return new XmlElementParser<TResult>(elementName, dictionary, parserFunc);
		}

		// Token: 0x06000C46 RID: 3142
		internal abstract XmlElementValue Parse(XmlElementInfo element, IList<XmlElementValue> children);

		// Token: 0x06000C47 RID: 3143 RVA: 0x00023594 File Offset: 0x00021794
		internal bool TryGetChildElementParser(string elementName, out XmlElementParser elementParser)
		{
			elementParser = null;
			return this.childParsers != null && this.childParsers.TryGetValue(elementName, ref elementParser);
		}

		// Token: 0x040006BB RID: 1723
		private readonly Dictionary<string, XmlElementParser> childParsers;
	}
}
