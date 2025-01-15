using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002E9 RID: 745
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	internal sealed class XmlElementClassAttribute : XmlElementAttribute
	{
		// Token: 0x060016E5 RID: 5861 RVA: 0x000363BF File Offset: 0x000345BF
		public XmlElementClassAttribute(string elementName)
			: base(elementName)
		{
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x000363C8 File Offset: 0x000345C8
		public XmlElementClassAttribute(string elementName, Type type)
			: base(elementName, type)
		{
		}
	}
}
