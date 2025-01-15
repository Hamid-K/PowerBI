using System;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000620 RID: 1568
	internal sealed class ParamValue
	{
		// Token: 0x17001F8C RID: 8076
		// (get) Token: 0x06005648 RID: 22088 RVA: 0x0016BADE File Offset: 0x00169CDE
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17001F8D RID: 8077
		// (get) Token: 0x06005649 RID: 22089 RVA: 0x0016BAE6 File Offset: 0x00169CE6
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17001F8E RID: 8078
		// (get) Token: 0x0600564A RID: 22090 RVA: 0x0016BAEE File Offset: 0x00169CEE
		public string FieldName
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x17001F8F RID: 8079
		// (get) Token: 0x0600564B RID: 22091 RVA: 0x0016BAF6 File Offset: 0x00169CF6
		public bool UseField
		{
			get
			{
				return this.FieldName != null;
			}
		}

		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x0600564C RID: 22092 RVA: 0x0016BB01 File Offset: 0x00169D01
		public bool UseValue
		{
			get
			{
				return !this.UseField;
			}
		}

		// Token: 0x0600564D RID: 22093 RVA: 0x0016BB0C File Offset: 0x00169D0C
		public bool IsValid()
		{
			return !this.UseField || this.Value == null;
		}

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x0600564E RID: 22094 RVA: 0x0016BB21 File Offset: 0x00169D21
		public string FieldValue
		{
			get
			{
				Global.Tracer.Assert(this.UseField, "Trying to get a field value for a non field setting.");
				return this.m_parent.GetFieldValue(this.FieldName);
			}
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x0016BB4C File Offset: 0x00169D4C
		public ParamValue(XmlReader reader, ParamValues parent)
		{
			this.m_parent = parent;
			while (reader.Read() && (reader.NodeType != XmlNodeType.EndElement || !(reader.Name == "ParameterValue")))
			{
				if (reader.IsStartElement("Name"))
				{
					this.m_name = reader.ReadString();
				}
				else if (reader.IsStartElement("Value"))
				{
					this.m_value = reader.ReadString();
				}
				else if (reader.IsStartElement("Field"))
				{
					this.m_fieldName = reader.ReadString();
					this.m_parent.AddField(this.FieldName);
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					throw new InvalidXmlException();
				}
			}
			if (!this.IsValid())
			{
				throw new InvalidXmlException();
			}
		}

		// Token: 0x06005650 RID: 22096 RVA: 0x0016BC14 File Offset: 0x00169E14
		internal void ToXml(XmlTextWriter writer, bool outputFieldElements)
		{
			writer.WriteStartElement("ParameterValue");
			writer.WriteElementString("Name", this.Name);
			if (this.UseField)
			{
				if (outputFieldElements)
				{
					writer.WriteElementString("Field", this.FieldName);
				}
				else if (this.FieldValue != null)
				{
					writer.WriteElementString("Value", this.FieldValue);
				}
			}
			else if (this.Value != null)
			{
				writer.WriteElementString("Value", this.Value);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06005651 RID: 22097 RVA: 0x0016BC95 File Offset: 0x00169E95
		internal void ToOldParameterXml(XmlTextWriter writer)
		{
			writer.WriteStartElement("Parameter");
			writer.WriteElementString("Name", this.Name);
			if (this.Value != null)
			{
				writer.WriteElementString("Value", this.Value);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04002DB7 RID: 11703
		internal const string _ParameterValue = "ParameterValue";

		// Token: 0x04002DB8 RID: 11704
		private const string _Name = "Name";

		// Token: 0x04002DB9 RID: 11705
		private const string _Value = "Value";

		// Token: 0x04002DBA RID: 11706
		private const string _Field = "Field";

		// Token: 0x04002DBB RID: 11707
		private ParamValues m_parent;

		// Token: 0x04002DBC RID: 11708
		private string m_name;

		// Token: 0x04002DBD RID: 11709
		private string m_value;

		// Token: 0x04002DBE RID: 11710
		private string m_fieldName;
	}
}
