using System;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000321 RID: 801
	public class ParameterValueOrFieldReference
	{
		// Token: 0x06001B48 RID: 6984 RVA: 0x0006F2D8 File Offset: 0x0006D4D8
		internal static void WriteThisToXml(ParameterValueOrFieldReference parameter, XmlTextWriter xml)
		{
			if (parameter == null)
			{
				return;
			}
			if (parameter is ParameterValue)
			{
				ParameterValue.WriteThisToXml((ParameterValue)parameter, xml);
				return;
			}
			if (parameter is ParameterFieldReference)
			{
				ParameterFieldReference.WriteThisToXml((ParameterFieldReference)parameter, xml);
			}
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0006F308 File Offset: 0x0006D508
		internal static void WriteThisArrayToXml(ParameterValueOrFieldReference[] parameters, XmlTextWriter xml)
		{
			if (parameters == null)
			{
				return;
			}
			xml.WriteStartElement("ParameterValues");
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterValueOrFieldReference.WriteThisToXml(parameters[i], xml);
			}
			xml.WriteEndElement();
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0006F344 File Offset: 0x0006D544
		internal static string ThisArrayToXml(ParameterValueOrFieldReference[] parameters)
		{
			if (parameters == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			ParameterValueOrFieldReference.WriteThisArrayToXml(parameters, xmlTextWriter);
			return stringWriter.ToString();
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0006F374 File Offset: 0x0006D574
		internal static ParameterValueOrFieldReference[] XmlNodesToThisArray(XmlNodeList nodes, bool createParameterValueArray)
		{
			if (nodes == null)
			{
				return null;
			}
			ParameterValueOrFieldReference[] array2;
			if (createParameterValueArray)
			{
				ParameterValueOrFieldReference[] array = new ParameterValue[nodes.Count];
				array2 = array;
			}
			else
			{
				array2 = new ParameterValueOrFieldReference[nodes.Count];
			}
			int num = 0;
			foreach (object obj in nodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string text = null;
				string text2 = null;
				string text3 = null;
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					string name = xmlNode2.Name;
					if (!(name == "Name"))
					{
						if (!(name == "Value"))
						{
							if (name == "Field")
							{
								text3 = xmlNode2.InnerText;
							}
						}
						else
						{
							text2 = xmlNode2.InnerText;
						}
					}
					else
					{
						text = xmlNode2.InnerText;
					}
				}
				ParameterValueOrFieldReference parameterValueOrFieldReference;
				if (text3 != null)
				{
					parameterValueOrFieldReference = new ParameterFieldReference
					{
						ParameterName = text,
						FieldAlias = text3
					};
				}
				else
				{
					parameterValueOrFieldReference = new ParameterValue
					{
						Name = text,
						Value = text2
					};
				}
				array2[num] = parameterValueOrFieldReference;
				num++;
			}
			return array2;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0006F4D4 File Offset: 0x0006D6D4
		internal static ParameterValueOrFieldReference[] XmlToThisArray(string parameters, bool createParameterValueArray)
		{
			if (parameters == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, parameters);
			return ParameterValueOrFieldReference.XmlNodesToThisArray(xmlDocument.SelectNodes("/ParameterValues/ParameterValue"), createParameterValueArray);
		}
	}
}
