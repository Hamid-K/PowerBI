using System;
using System.Xml;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000323 RID: 803
	public class ParameterFieldReference : ParameterValueOrFieldReference
	{
		// Token: 0x06001B54 RID: 6996 RVA: 0x0006F760 File Offset: 0x0006D960
		internal static void WriteThisToXml(ParameterFieldReference parameter, XmlTextWriter xml)
		{
			xml.WriteStartElement("ParameterValue");
			if (parameter.ParameterName != null)
			{
				xml.WriteElementString("Name", parameter.ParameterName);
			}
			if (parameter.FieldAlias != null)
			{
				xml.WriteElementString("Field", parameter.FieldAlias);
			}
			xml.WriteEndElement();
		}

		// Token: 0x04000AE2 RID: 2786
		public string ParameterName;

		// Token: 0x04000AE3 RID: 2787
		public string FieldAlias;
	}
}
