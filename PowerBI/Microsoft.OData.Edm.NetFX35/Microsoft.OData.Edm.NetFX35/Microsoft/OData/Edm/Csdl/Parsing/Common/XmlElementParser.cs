using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x02000199 RID: 409
	internal abstract class XmlElementParser
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x000139E5 File Offset: 0x00011BE5
		protected XmlElementParser(string elementName, Dictionary<string, XmlElementParser> children)
		{
			this.ElementName = elementName;
			this.childParsers = children;
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x000139FB File Offset: 0x00011BFB
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x00013A03 File Offset: 0x00011C03
		internal string ElementName { get; private set; }

		// Token: 0x060007EC RID: 2028 RVA: 0x00013A0C File Offset: 0x00011C0C
		public void AddChildParser(XmlElementParser child)
		{
			this.childParsers[child.ElementName] = child;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00013A28 File Offset: 0x00011C28
		internal static XmlElementParser<TResult> Create<TResult>(string elementName, Func<XmlElementInfo, XmlElementValueCollection, TResult> parserFunc, IEnumerable<XmlElementParser> childParsers, IEnumerable<XmlElementParser> descendantParsers)
		{
			Func<XmlElementParser, string> func = null;
			Dictionary<string, XmlElementParser> dictionary = null;
			if (childParsers != null)
			{
				if (func == null)
				{
					func = (XmlElementParser p) => p.ElementName;
				}
				dictionary = Enumerable.ToDictionary<XmlElementParser, string>(childParsers, func);
			}
			return new XmlElementParser<TResult>(elementName, dictionary, parserFunc);
		}

		// Token: 0x060007EE RID: 2030
		internal abstract XmlElementValue Parse(XmlElementInfo element, IList<XmlElementValue> children);

		// Token: 0x060007EF RID: 2031 RVA: 0x00013A5C File Offset: 0x00011C5C
		internal bool TryGetChildElementParser(string elementName, out XmlElementParser elementParser)
		{
			elementParser = null;
			return this.childParsers != null && this.childParsers.TryGetValue(elementName, ref elementParser);
		}

		// Token: 0x04000413 RID: 1043
		private readonly Dictionary<string, XmlElementParser> childParsers;
	}
}
