using System;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200034C RID: 844
	public class Task
	{
		// Token: 0x06001C01 RID: 7169 RVA: 0x00071980 File Offset: 0x0006FB80
		internal static string TaskArrayToXml(string[] taskIDs)
		{
			if (taskIDs == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.WriteStartElement("Tasks");
			for (int i = 0; i < taskIDs.Length; i++)
			{
				xmlTextWriter.WriteStartElement("Task");
				if (taskIDs[i] != null)
				{
					xmlTextWriter.WriteElementString("TaskID", taskIDs[i]);
				}
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000719F0 File Offset: 0x0006FBF0
		internal static string[] XmlToTaskArray(string tasks)
		{
			if (tasks == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, tasks);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/Tasks/Task");
			if (xmlNodeList == null)
			{
				return null;
			}
			string[] array = new string[xmlNodeList.Count];
			int num = 0;
			foreach (object obj in xmlNodeList)
			{
				foreach (object obj2 in ((XmlNode)obj).ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					string name = xmlNode.Name;
					if (name == "TaskID")
					{
						array[num] = xmlNode.InnerText;
					}
				}
				num++;
			}
			return array;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00071ADC File Offset: 0x0006FCDC
		internal static Task[] TaskListToThisArray(AuthzData.TaskList tasks)
		{
			Task[] array = new Task[tasks.Count];
			for (int i = 0; i < tasks.Count; i++)
			{
				array[i] = new Task
				{
					TaskID = tasks[i].ID
				};
			}
			return array;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00071B23 File Offset: 0x0006FD23
		internal static AuthzData.SecurityScope SoapTypeToSecurityScope(SecurityScopeEnum scope, out bool limitScope)
		{
			limitScope = true;
			switch (scope)
			{
			case SecurityScopeEnum.System:
				return AuthzData.SecurityScope.Catalog;
			case SecurityScopeEnum.Catalog:
				return AuthzData.SecurityScope.CatalogItem;
			case SecurityScopeEnum.Model:
				return AuthzData.SecurityScope.ModelItem;
			case SecurityScopeEnum.All:
				limitScope = false;
				return AuthzData.SecurityScope.Catalog;
			default:
				throw new InternalCatalogException("Unknown Soap Security Scope Enum value.");
			}
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00071B58 File Offset: 0x0006FD58
		internal static string[] TaskIDArrayFromTaskArray(Task[] tasks)
		{
			if (tasks == null)
			{
				return null;
			}
			string[] array = new string[tasks.Length];
			for (int i = 0; i < tasks.Length; i++)
			{
				if (tasks[i] == null)
				{
					throw new MissingElementException("Task");
				}
				array[i] = tasks[i].TaskID;
			}
			return array;
		}

		// Token: 0x04000B90 RID: 2960
		public string TaskID;

		// Token: 0x04000B91 RID: 2961
		public string Name;

		// Token: 0x04000B92 RID: 2962
		public string Description;
	}
}
