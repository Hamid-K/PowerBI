using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.PowerBI.DataExtension.Contracts
{
	// Token: 0x02000007 RID: 7
	public sealed class BatchXmlaCommandBuilder
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002088 File Offset: 0x00000288
		public BatchXmlaCommandBuilder()
		{
			this._builder = new StringBuilder();
			this._writer = new XmlTextWriter(new StringWriter(this._builder));
			this._writer.Formatting = Formatting.Indented;
			this._writer.WriteStartElement("Batch", "http://schemas.microsoft.com/analysisservices/2003/engine");
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DD File Offset: 0x000002DD
		public BatchXmlaCommandBuilder AddCommand(string name)
		{
			this.AddCommand(name, new KeyValuePair<string, string>[0], null);
			return this;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020EF File Offset: 0x000002EF
		public BatchXmlaCommandBuilder AddCommand(string name, IReadOnlyList<KeyValuePair<string, string>> restrictions, IReadOnlyList<KeyValuePair<string, string>> properties)
		{
			this._writer.WriteStartElement("Discover", "urn:schemas-microsoft-com:xml-analysis");
			this.AddRequestType(name);
			this.AddRestrictions(restrictions);
			this.AddProperties(properties);
			this._writer.WriteEndElement();
			return this;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002127 File Offset: 0x00000327
		public BatchXmlaCommandBuilder AddCommand(string name, IReadOnlyList<KeyValuePair<string, IReadOnlyList<string>>> restrictions, IReadOnlyList<KeyValuePair<string, string>> properties)
		{
			this._writer.WriteStartElement("Discover", "urn:schemas-microsoft-com:xml-analysis");
			this.AddRequestType(name);
			this.AddRestrictions(restrictions);
			this.AddProperties(properties);
			this._writer.WriteEndElement();
			return this;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000215F File Offset: 0x0000035F
		private void AddRequestType(string name)
		{
			this._writer.WriteStartElement("RequestType");
			this._writer.WriteString(name);
			this._writer.WriteEndElement();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002188 File Offset: 0x00000388
		private void AddRestrictions(IReadOnlyList<KeyValuePair<string, string>> restrictions)
		{
			this._writer.WriteStartElement("Restrictions");
			this._writer.WriteStartElement("RestrictionList");
			this.AddXmlNodes(restrictions);
			this._writer.WriteEndElement();
			this._writer.WriteEndElement();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021C7 File Offset: 0x000003C7
		private void AddRestrictions(IReadOnlyList<KeyValuePair<string, IReadOnlyList<string>>> restrictions)
		{
			this._writer.WriteStartElement("Restrictions");
			this._writer.WriteStartElement("RestrictionList");
			this.AddXmlNodes(restrictions);
			this._writer.WriteEndElement();
			this._writer.WriteEndElement();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002206 File Offset: 0x00000406
		private void AddProperties(IReadOnlyList<KeyValuePair<string, string>> properties)
		{
			this._writer.WriteStartElement("Properties");
			this._writer.WriteStartElement("PropertyList");
			this.AddXmlNodes(properties);
			this._writer.WriteEndElement();
			this._writer.WriteEndElement();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002248 File Offset: 0x00000448
		private void AddXmlNodes(IReadOnlyList<KeyValuePair<string, string>> itemPairs)
		{
			if (itemPairs == null || itemPairs.Count == 0)
			{
				return;
			}
			foreach (KeyValuePair<string, string> keyValuePair in itemPairs)
			{
				this._writer.WriteStartElement(keyValuePair.Key);
				this._writer.WriteString(keyValuePair.Value);
				this._writer.WriteEndElement();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C4 File Offset: 0x000004C4
		private void AddXmlNodes(IReadOnlyList<KeyValuePair<string, IReadOnlyList<string>>> itemPairs)
		{
			if (itemPairs == null || itemPairs.Count == 0)
			{
				return;
			}
			foreach (KeyValuePair<string, IReadOnlyList<string>> keyValuePair in itemPairs)
			{
				this._writer.WriteStartElement(keyValuePair.Key);
				foreach (string text in keyValuePair.Value)
				{
					this._writer.WriteStartElement("Value");
					this._writer.WriteString(text);
					this._writer.WriteEndElement();
				}
				this._writer.WriteEndElement();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000238C File Offset: 0x0000058C
		public string BuildCommandText()
		{
			this._writer.WriteEndElement();
			this._writer.Close();
			return this._builder.ToString();
		}

		// Token: 0x0400003D RID: 61
		private const string Batch = "Batch";

		// Token: 0x0400003E RID: 62
		private const string BatchNs = "http://schemas.microsoft.com/analysisservices/2003/engine";

		// Token: 0x0400003F RID: 63
		private const string Discover = "Discover";

		// Token: 0x04000040 RID: 64
		private const string DiscoverNs = "urn:schemas-microsoft-com:xml-analysis";

		// Token: 0x04000041 RID: 65
		private const string RequestType = "RequestType";

		// Token: 0x04000042 RID: 66
		private const string Restrictions = "Restrictions";

		// Token: 0x04000043 RID: 67
		private const string RestrictionList = "RestrictionList";

		// Token: 0x04000044 RID: 68
		private const string Properties = "Properties";

		// Token: 0x04000045 RID: 69
		private const string PropertyList = "PropertyList";

		// Token: 0x04000046 RID: 70
		private const string Value = "Value";

		// Token: 0x04000047 RID: 71
		private readonly StringBuilder _builder;

		// Token: 0x04000048 RID: 72
		private readonly XmlTextWriter _writer;
	}
}
