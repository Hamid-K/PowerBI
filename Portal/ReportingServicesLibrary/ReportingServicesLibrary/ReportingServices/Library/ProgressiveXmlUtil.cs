using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200018D RID: 397
	internal static class ProgressiveXmlUtil
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x00035B90 File Offset: 0x00033D90
		internal static XmlReaderSettings CreateXmlReaderSettings(string xsdResourceName)
		{
			XmlSchema xmlSchema = null;
			using (Stream manifestResourceStream = typeof(ProgressiveXmlUtil).Assembly.GetManifestResourceStream(xsdResourceName))
			{
				ValidationEventHandler validationEventHandler = delegate(object sender, ValidationEventArgs e)
				{
					RSTrace.CatalogTrace.Assert(false, e.Message);
				};
				xmlSchema = XmlSchema.Read(manifestResourceStream, validationEventHandler);
			}
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.Schemas.Add(xmlSchema);
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.ValidationEventHandler += delegate(object sender, ValidationEventArgs e)
			{
				throw e.Exception;
			};
			xmlReaderSettings.CheckCharacters = false;
			XmlUtil.ApplyDtdDosDefense(xmlReaderSettings);
			return xmlReaderSettings;
		}
	}
}
