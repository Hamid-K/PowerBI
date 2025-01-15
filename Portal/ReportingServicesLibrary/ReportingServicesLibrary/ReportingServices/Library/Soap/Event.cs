using System;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200034A RID: 842
	public class Event
	{
		// Token: 0x06001BFF RID: 7167 RVA: 0x00071880 File Offset: 0x0006FA80
		internal static Event[] XmlToEventArray(string events)
		{
			if (events == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, events);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/Events/Event");
			if (xmlNodeList == null)
			{
				return null;
			}
			Event[] array = new Event[xmlNodeList.Count];
			int num = 0;
			foreach (object obj in xmlNodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				Event @event = new Event();
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					string name = xmlNode2.Name;
					if (name == "Type")
					{
						@event.Type = xmlNode2.InnerText;
					}
				}
				array[num] = @event;
				num++;
			}
			return array;
		}

		// Token: 0x04000B8A RID: 2954
		public string Type;
	}
}
