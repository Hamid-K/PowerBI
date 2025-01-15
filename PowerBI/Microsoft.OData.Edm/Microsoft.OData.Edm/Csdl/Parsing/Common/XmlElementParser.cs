using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001C2 RID: 450
	internal abstract class XmlElementParser
	{
		// Token: 0x06000CF3 RID: 3315 RVA: 0x000256DE File Offset: 0x000238DE
		protected XmlElementParser(string elementName, Dictionary<string, XmlElementParser> children)
		{
			this.ElementName = elementName;
			this.childParsers = children;
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x000256F4 File Offset: 0x000238F4
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x000256FC File Offset: 0x000238FC
		internal string ElementName { get; private set; }

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00025705 File Offset: 0x00023905
		public void AddChildParser(XmlElementParser child)
		{
			this.childParsers[child.ElementName] = child;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002571C File Offset: 0x0002391C
		internal static XmlElementParser<TResult> Create<TResult>(string elementName, Func<XmlElementInfo, XmlElementValueCollection, TResult> parserFunc, IEnumerable<XmlElementParser> childParsers, IEnumerable<XmlElementParser> descendantParsers)
		{
			Dictionary<string, XmlElementParser> dictionary = null;
			if (childParsers != null)
			{
				dictionary = childParsers.ToDictionary((XmlElementParser p) => p.ElementName);
			}
			return new XmlElementParser<TResult>(elementName, dictionary, parserFunc);
		}

		// Token: 0x06000CF8 RID: 3320
		internal abstract XmlElementValue Parse(XmlElementInfo element, IList<XmlElementValue> children);

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002575C File Offset: 0x0002395C
		internal bool TryGetChildElementParser(string elementName, out XmlElementParser elementParser)
		{
			elementParser = null;
			return this.childParsers != null && this.childParsers.TryGetValue(elementName, out elementParser);
		}

		// Token: 0x04000734 RID: 1844
		private readonly Dictionary<string, XmlElementParser> childParsers;
	}
}
