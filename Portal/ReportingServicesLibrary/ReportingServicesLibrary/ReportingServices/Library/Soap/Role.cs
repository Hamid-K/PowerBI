using System;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200034D RID: 845
	public class Role
	{
		// Token: 0x06001C07 RID: 7175 RVA: 0x00071BA0 File Offset: 0x0006FDA0
		internal static Role[] XmlToRoleArray(string roles)
		{
			if (roles == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, roles);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/Roles/Role");
			if (xmlNodeList == null)
			{
				return null;
			}
			Role[] array = new Role[xmlNodeList.Count];
			int num = 0;
			foreach (object obj in xmlNodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				Role role = new Role();
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					string name = xmlNode2.Name;
					if (!(name == "Name"))
					{
						if (name == "Description")
						{
							role.Description = xmlNode2.InnerText;
						}
					}
					else
					{
						role.Name = xmlNode2.InnerText;
					}
				}
				array[num] = role;
				num++;
			}
			return array;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x00071CC4 File Offset: 0x0006FEC4
		internal static void WriteArrayToXml(Role[] roles, XmlTextWriter xml)
		{
			if (roles == null)
			{
				return;
			}
			xml.WriteStartElement("Roles");
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] == null)
				{
					throw new MissingElementException("Role");
				}
				xml.WriteStartElement("Role");
				xml.WriteElementString("Name", roles[i].Name);
				if (roles[i].Description != null)
				{
					xml.WriteElementString("Description", roles[i].Description);
				}
				xml.WriteEndElement();
			}
			xml.WriteEndElement();
		}

		// Token: 0x04000B93 RID: 2963
		public string Name;

		// Token: 0x04000B94 RID: 2964
		public string Description;
	}
}
