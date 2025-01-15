using System;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200034E RID: 846
	public class Policy
	{
		// Token: 0x06001C0A RID: 7178 RVA: 0x00071D48 File Offset: 0x0006FF48
		internal static string PolicyArrayToXml(Policy[] policies)
		{
			if (policies == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.WriteStartElement("Policies");
			for (int i = 0; i < policies.Length; i++)
			{
				xmlTextWriter.WriteStartElement("Policy");
				Policy policy = policies[i];
				if (policy == null)
				{
					throw new MissingElementException("Policy");
				}
				if (policy.GroupUserName != null)
				{
					xmlTextWriter.WriteElementString("GroupUserName", policy.GroupUserName);
				}
				if (policy.Roles != null)
				{
					Role.WriteArrayToXml(policy.Roles, xmlTextWriter);
				}
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00071DE4 File Offset: 0x0006FFE4
		internal static Policy[] XmlToPolicyArray(string xmlPolicies)
		{
			if (xmlPolicies == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, xmlPolicies);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/Policies/Policy");
			Policy[] array = new Policy[xmlNodeList.Count];
			int num = 0;
			foreach (object obj in xmlNodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				Policy policy = new Policy();
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("GroupUserName");
				policy.GroupUserName = xmlNode2.InnerText;
				XmlNode xmlNode3 = xmlNode.SelectSingleNode("Roles");
				if (xmlNode3 != null)
				{
					policy.Roles = Role.XmlToRoleArray(xmlNode3.OuterXml);
				}
				array[num] = policy;
				num++;
			}
			return array;
		}

		// Token: 0x04000B95 RID: 2965
		public string GroupUserName;

		// Token: 0x04000B96 RID: 2966
		public Role[] Roles;
	}
}
