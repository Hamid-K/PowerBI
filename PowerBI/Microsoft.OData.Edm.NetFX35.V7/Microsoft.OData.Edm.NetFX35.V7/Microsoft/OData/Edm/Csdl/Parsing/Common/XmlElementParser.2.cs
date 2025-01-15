using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001B6 RID: 438
	internal class XmlElementParser<TResult> : XmlElementParser
	{
		// Token: 0x06000C48 RID: 3144 RVA: 0x000235B0 File Offset: 0x000217B0
		internal XmlElementParser(string elementName, Dictionary<string, XmlElementParser> children, Func<XmlElementInfo, XmlElementValueCollection, TResult> parser)
			: base(elementName, children)
		{
			this.parserFunc = parser;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000235C4 File Offset: 0x000217C4
		internal override XmlElementValue Parse(XmlElementInfo element, IList<XmlElementValue> children)
		{
			TResult tresult = this.parserFunc.Invoke(element, XmlElementValueCollection.FromList(children));
			return new XmlElementValue<TResult>(element.Name, element.Location, tresult);
		}

		// Token: 0x040006BD RID: 1725
		private readonly Func<XmlElementInfo, XmlElementValueCollection, TResult> parserFunc;
	}
}
