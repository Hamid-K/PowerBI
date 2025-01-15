using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001D5 RID: 469
	internal class WSSecurityUtilityIdSignedXml : SignedXml
	{
		// Token: 0x0600155D RID: 5469 RVA: 0x0003C3B1 File Offset: 0x0003B3B1
		public WSSecurityUtilityIdSignedXml(XmlDocument document)
			: base(document)
		{
			this.document = document;
			this.ids = new Dictionary<string, XmlElement>();
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0003C3CC File Offset: 0x0003B3CC
		public static string GetUniqueId()
		{
			return WSSecurityUtilityIdSignedXml.commonPrefix + Interlocked.Increment(ref WSSecurityUtilityIdSignedXml.nextId).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0003C3FC File Offset: 0x0003B3FC
		public void AddReference(string xpath)
		{
			XmlElement xmlElement = this.document.SelectSingleNode(xpath, WSSecurityBasedCredentials.NamespaceManager) as XmlElement;
			if (xmlElement != null)
			{
				string uniqueId = WSSecurityUtilityIdSignedXml.GetUniqueId();
				XmlAttribute xmlAttribute = this.document.CreateAttribute("wsu", "Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
				xmlAttribute.Value = uniqueId;
				xmlElement.Attributes.Append(xmlAttribute);
				Reference reference = new Reference();
				reference.Uri = "#" + uniqueId;
				reference.AddTransform(new XmlDsigExcC14NTransform());
				base.AddReference(reference);
				this.ids.Add(uniqueId, xmlElement);
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0003C48E File Offset: 0x0003B48E
		public override XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			return this.ids[idValue];
		}

		// Token: 0x04000CF1 RID: 3313
		private static long nextId = 0L;

		// Token: 0x04000CF2 RID: 3314
		private static string commonPrefix = "uuid-" + Guid.NewGuid().ToString() + "-";

		// Token: 0x04000CF3 RID: 3315
		private XmlDocument document;

		// Token: 0x04000CF4 RID: 3316
		private Dictionary<string, XmlElement> ids;
	}
}
