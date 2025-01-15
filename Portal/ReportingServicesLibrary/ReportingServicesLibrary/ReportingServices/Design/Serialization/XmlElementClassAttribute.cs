using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x02000390 RID: 912
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	internal class XmlElementClassAttribute : XmlElementAttribute
	{
		// Token: 0x06001E1C RID: 7708 RVA: 0x0007B208 File Offset: 0x00079408
		public XmlElementClassAttribute(string elementName, Type type)
			: base(elementName, type)
		{
		}
	}
}
