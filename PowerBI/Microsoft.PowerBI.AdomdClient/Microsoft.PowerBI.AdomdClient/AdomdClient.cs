using System;
using System.Collections;
using System.Xml;
using Microsoft.AnalysisServices.AdomdClient.Hosting;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200004F RID: 79
	internal class AdomdClient : XmlaClient
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x0001E5F3 File Offset: 0x0001C7F3
		internal AdomdClient(IConnectivityOwner owner)
			: base(owner)
		{
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001E5FC File Offset: 0x0001C7FC
		private protected override void WriteProperties(IDictionary connectionProperties, IDictionary commandProperties)
		{
			this.writer.WriteStartElement("Properties");
			this.writer.WriteStartElement("PropertyList");
			if (connectionProperties != null && connectionProperties.Count > 0)
			{
				foreach (object obj in connectionProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value != null && (commandProperties == null || commandProperties.Count <= 0 || (dictionaryEntry.Key is IXmlaPropertyKey && !commandProperties.Contains(dictionaryEntry.Key)) || (!(dictionaryEntry.Key is IXmlaPropertyKey) && !commandProperties.Contains(new XmlaPropertyKey((string)dictionaryEntry.Key, null)))))
					{
						this.WriteVersionSafeXmlaProperty(dictionaryEntry);
					}
				}
			}
			if (commandProperties != null && commandProperties.Count > 0)
			{
				foreach (object obj2 in commandProperties)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (dictionaryEntry2.Value != null)
					{
						this.WriteVersionSafeXmlaProperty(dictionaryEntry2);
					}
				}
			}
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001E74C File Offset: 0x0001C94C
		private protected override void WriteXmlaProperty(DictionaryEntry entry)
		{
			if (entry.Key is IXmlaPropertyKey)
			{
				IXmlaPropertyKey xmlaPropertyKey = (IXmlaPropertyKey)entry.Key;
				if (entry.Value != null && xmlaPropertyKey.Name != null && xmlaPropertyKey.Name.Length > 0)
				{
					string text = XmlConvert.EncodeLocalName(xmlaPropertyKey.Name);
					if (xmlaPropertyKey.Namespace == null || xmlaPropertyKey.Namespace.Length == 0)
					{
						this.writer.WriteElementString(text, FormattersHelpers.ConvertToXml(entry.Value));
						return;
					}
					this.writer.WriteElementString(text, xmlaPropertyKey.Namespace, FormattersHelpers.ConvertToXml(entry.Value));
					return;
				}
			}
			else
			{
				this.writer.WriteElementString((string)entry.Key, FormattersHelpers.ConvertToXml(entry.Value));
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001E818 File Offset: 0x0001CA18
		private void WriteVersionSafeXmlaProperty(DictionaryEntry propertyEntry)
		{
			if (propertyEntry.Key is IXmlaPropertyKey)
			{
				IXmlaPropertyKey xmlaPropertyKey = (IXmlaPropertyKey)propertyEntry.Key;
				if (this.IsVersionSafeProperty(xmlaPropertyKey.Name))
				{
					this.WriteXmlaProperty(propertyEntry);
					return;
				}
			}
			else if (this.IsVersionSafeProperty((string)propertyEntry.Key))
			{
				this.WriteXmlaProperty(propertyEntry);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001E874 File Offset: 0x0001CA74
		private bool IsVersionSafeProperty(string propertyName)
		{
			if (propertyName.Equals("DbpropMsmdCurrentActivityID", StringComparison.OrdinalIgnoreCase))
			{
				return base.SupportsCurrentActivityID;
			}
			if (propertyName.Equals("DbpropMsmdRequestID", StringComparison.OrdinalIgnoreCase))
			{
				return base.SupportsActivityIDAndRequestID;
			}
			if (propertyName.Equals("DbpropMsmdActivityID", StringComparison.OrdinalIgnoreCase))
			{
				return base.SupportsActivityIDAndRequestID;
			}
			return !propertyName.Equals("ApplicationContext", StringComparison.OrdinalIgnoreCase) || base.SupportsApplicationContext;
		}
	}
}
