using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005C7 RID: 1479
	internal abstract class SerializableValue : IXmlSerializable
	{
		// Token: 0x0600535A RID: 21338 RVA: 0x0015FD4D File Offset: 0x0015DF4D
		protected SerializableValue()
		{
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0015FD5C File Offset: 0x0015DF5C
		protected SerializableValue(object value)
		{
			this.m_value = value;
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x0015FD72 File Offset: 0x0015DF72
		protected SerializableValue(object value, DataTypeCode dataTypeCode)
		{
			this.m_value = value;
			this.m_dataTypeCode = dataTypeCode;
		}

		// Token: 0x17001EEC RID: 7916
		// (get) Token: 0x0600535D RID: 21341 RVA: 0x0015FD8F File Offset: 0x0015DF8F
		internal DataTypeCode TypeCode
		{
			get
			{
				return this.m_dataTypeCode;
			}
		}

		// Token: 0x17001EED RID: 7917
		// (get) Token: 0x0600535E RID: 21342 RVA: 0x0015FD97 File Offset: 0x0015DF97
		internal object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0015FD9F File Offset: 0x0015DF9F
		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005360 RID: 21344
		protected abstract void ReadDerivedXml(XmlReader xmlReader);

		// Token: 0x06005361 RID: 21345 RVA: 0x0015FDA8 File Offset: 0x0015DFA8
		public void ReadXml(XmlReader xmlReader)
		{
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element)
				{
					string name = xmlReader.Name;
					if (name == "TypeCode")
					{
						xmlReader.Read();
						this.m_dataTypeCode = (DataTypeCode)Enum.Parse(typeof(DataTypeCode), xmlReader.Value, false);
						continue;
					}
					if (name == "Value")
					{
						xmlReader.Read();
						this.m_value = ObjectSerializer.Read(xmlReader, this.m_dataTypeCode);
						continue;
					}
				}
				this.ReadDerivedXml(xmlReader);
			}
		}

		// Token: 0x06005362 RID: 21346
		public abstract void WriteXml(XmlWriter writer);

		// Token: 0x06005363 RID: 21347 RVA: 0x0015FE38 File Offset: 0x0015E038
		protected void WriteBaseXml(XmlWriter writer)
		{
			writer.WriteElementString("TypeCode", this.m_dataTypeCode.ToString());
			if (this.m_value != null)
			{
				writer.WriteStartElement("Value");
				ObjectSerializer.Write(writer, this.m_value, this.m_dataTypeCode);
				writer.WriteEndElement();
			}
		}

		// Token: 0x04002A0F RID: 10767
		private object m_value;

		// Token: 0x04002A10 RID: 10768
		private DataTypeCode m_dataTypeCode = DataTypeCode.Unknown;

		// Token: 0x04002A11 RID: 10769
		internal const string TYPECODE = "TypeCode";

		// Token: 0x04002A12 RID: 10770
		internal const string VALUE = "Value";
	}
}
