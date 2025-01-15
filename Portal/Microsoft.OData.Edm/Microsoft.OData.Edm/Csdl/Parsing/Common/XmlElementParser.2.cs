using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001C3 RID: 451
	internal class XmlElementParser<TResult> : XmlElementParser
	{
		// Token: 0x06000CFA RID: 3322 RVA: 0x00025778 File Offset: 0x00023978
		internal XmlElementParser(string elementName, Dictionary<string, XmlElementParser> children, Func<XmlElementInfo, XmlElementValueCollection, TResult> parser)
			: base(elementName, children)
		{
			this.parserFunc = parser;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002578C File Offset: 0x0002398C
		internal override XmlElementValue Parse(XmlElementInfo element, IList<XmlElementValue> children)
		{
			TResult tresult = this.parserFunc(element, XmlElementValueCollection.FromList(children));
			return new XmlElementValue<TResult>(element.Name, element.Location, tresult);
		}

		// Token: 0x04000736 RID: 1846
		private readonly Func<XmlElementInfo, XmlElementValueCollection, TResult> parserFunc;
	}
}
