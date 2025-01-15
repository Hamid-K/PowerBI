using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x0200019A RID: 410
	internal class XmlElementParser<TResult> : XmlElementParser
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x00013A78 File Offset: 0x00011C78
		internal XmlElementParser(string elementName, Dictionary<string, XmlElementParser> children, Func<XmlElementInfo, XmlElementValueCollection, TResult> parser)
			: base(elementName, children)
		{
			this.parserFunc = parser;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00013A8C File Offset: 0x00011C8C
		internal override XmlElementValue Parse(XmlElementInfo element, IList<XmlElementValue> children)
		{
			TResult tresult = this.parserFunc.Invoke(element, XmlElementValueCollection.FromList(children));
			return new XmlElementValue<TResult>(element.Name, element.Location, tresult);
		}

		// Token: 0x04000415 RID: 1045
		private readonly Func<XmlElementInfo, XmlElementValueCollection, TResult> parserFunc;
	}
}
