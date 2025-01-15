using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200009A RID: 154
	public sealed class DrillthroughContextBuilder
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x00018861 File Offset: 0x00016A61
		public DrillthroughContextBuilder()
		{
			this.m_xmlWriterSettings = XmlRWFactory.GetWriterSettings();
			this.m_xmlWriterSettings.OmitXmlDeclaration = true;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00018880 File Offset: 0x00016A80
		public string CreateAsString(ICollection<string> selectedItems, string selectedPathXml, IDictionary<string, object> groupingValues)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				DrillthroughContext.WriteXml(XmlWriter.Create(stringWriter, this.m_xmlWriterSettings), selectedItems, selectedPathXml, groupingValues);
				stringWriter.Flush();
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000188D8 File Offset: 0x00016AD8
		public static string GetSelectedPathXml(ExpressionPath selectedPath)
		{
			return XmlFragmentUtil.ToXmlString(delegate(XmlWriter xw)
			{
				DrillthroughContext.WriteSelectedPathXml(xw, selectedPath);
			});
		}

		// Token: 0x0400038F RID: 911
		private readonly XmlWriterSettings m_xmlWriterSettings;
	}
}
