using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003E0 RID: 992
	public abstract class EnumString : IXmlSerializable, IVoluntarySerializable
	{
		// Token: 0x06001F9E RID: 8094 RVA: 0x0007E95E File Offset: 0x0007CB5E
		protected void Copy(EnumString from)
		{
			this.m_Key = from.m_Key;
			this.m_RawValue = from.m_RawValue;
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0007E978 File Offset: 0x0007CB78
		public void Set(string rawValue)
		{
			this.m_RawValue = rawValue;
			foreach (KeyValuePair<int, string> keyValuePair in EnumString.m_Dictionary)
			{
				if (keyValuePair.Value == rawValue)
				{
					this.m_Key = keyValuePair.Key;
					return;
				}
			}
			this.m_Key = -2;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x0007E9EC File Offset: 0x0007CBEC
		protected void Set(int key)
		{
			foreach (KeyValuePair<int, string> keyValuePair in EnumString.m_Dictionary)
			{
				if (keyValuePair.Key == key)
				{
					this.m_RawValue = keyValuePair.Value;
					return;
				}
			}
			this.m_Key = -2;
			this.m_RawValue = string.Empty;
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x0007EA60 File Offset: 0x0007CC60
		public override string ToString()
		{
			return this.m_RawValue;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0007EA68 File Offset: 0x0007CC68
		bool IVoluntarySerializable.ShouldBeSerialized()
		{
			return !string.IsNullOrEmpty(this.m_RawValue);
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00005C88 File Offset: 0x00003E88
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0007EA78 File Offset: 0x0007CC78
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.Set(text);
			reader.Skip();
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x0007EA99 File Offset: 0x0007CC99
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteString(this.m_RawValue);
		}

		// Token: 0x04000DC8 RID: 3528
		internal const int InvalidEnumValue = -2;

		// Token: 0x04000DC9 RID: 3529
		internal const int ExpressionEnumValue = -1;

		// Token: 0x04000DCA RID: 3530
		protected static IDictionary<int, string> m_Dictionary;

		// Token: 0x04000DCB RID: 3531
		protected string m_RawValue = string.Empty;

		// Token: 0x04000DCC RID: 3532
		protected int m_Key = -2;
	}
}
