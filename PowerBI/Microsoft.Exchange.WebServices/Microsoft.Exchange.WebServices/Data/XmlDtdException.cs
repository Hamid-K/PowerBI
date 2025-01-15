using System;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000304 RID: 772
	internal class XmlDtdException : XmlException
	{
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x00049853 File Offset: 0x00048853
		public override string Message
		{
			get
			{
				return "For security reasons DTD is prohibited in this XML document.";
			}
		}
	}
}
