using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200061E RID: 1566
	public sealed class ParamValues : Hashtable
	{
		// Token: 0x17001F87 RID: 8071
		public ParamValueList this[string name]
		{
			get
			{
				return (ParamValueList)base[name];
			}
		}

		// Token: 0x17001F88 RID: 8072
		// (get) Token: 0x0600563B RID: 22075 RVA: 0x0016B6FC File Offset: 0x001698FC
		public Hashtable Fields
		{
			get
			{
				return this.m_fields;
			}
		}

		// Token: 0x17001F89 RID: 8073
		// (get) Token: 0x0600563C RID: 22076 RVA: 0x0016B704 File Offset: 0x00169904
		public string[] FieldKeys
		{
			get
			{
				string[] array = new string[this.m_fields.Keys.Count];
				int num = 0;
				foreach (object obj in this.m_fields.Keys)
				{
					string text = (string)obj;
					array[num++] = text;
				}
				return array;
			}
		}

		// Token: 0x0600563D RID: 22077 RVA: 0x0016B780 File Offset: 0x00169980
		internal void AddField(string fieldName)
		{
			if (this.m_fields[fieldName] != null)
			{
				return;
			}
			this.m_fields.Add(fieldName, fieldName);
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x0016B79E File Offset: 0x0016999E
		internal string GetFieldValue(string fieldName)
		{
			return (string)this.m_fields[fieldName];
		}

		// Token: 0x0600563F RID: 22079 RVA: 0x0016B7B1 File Offset: 0x001699B1
		internal void AddFieldValue(string fieldName, string fieldValue)
		{
			if (this.m_fields[fieldName] == null)
			{
				return;
			}
			this.m_fields[fieldName] = fieldValue;
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x0016B7D0 File Offset: 0x001699D0
		public void FromXml(string xml)
		{
			if (xml == null || xml == "")
			{
				return;
			}
			XmlReader xmlReader = XmlUtil.SafeCreateXmlTextReader(xml);
			try
			{
				if (!xmlReader.Read() || xmlReader.Name != "ParameterValues")
				{
					throw new InvalidXmlException();
				}
				while (xmlReader.Read())
				{
					if (xmlReader.IsStartElement("ParameterValue"))
					{
						ParamValue paramValue = new ParamValue(xmlReader, this);
						bool flag = false;
						ParamValueList paramValueList = this[paramValue.Name];
						if (paramValueList == null)
						{
							flag = true;
							paramValueList = new ParamValueList();
						}
						paramValueList.Add(paramValue);
						if (flag)
						{
							this.Add(paramValue.Name, paramValueList);
						}
					}
					else if (xmlReader.NodeType == XmlNodeType.Element)
					{
						throw new InvalidXmlException();
					}
				}
			}
			catch (XmlException ex)
			{
				throw new MalformedXmlException(ex);
			}
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x0016B890 File Offset: 0x00169A90
		public string ToXml(bool outputFieldElements)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			string text;
			try
			{
				xmlTextWriter.WriteStartElement("ParameterValues");
				foreach (object obj in this.Values)
				{
					ParamValueList paramValueList = (ParamValueList)obj;
					for (int i = 0; i < paramValueList.Count; i++)
					{
						paramValueList[i].ToXml(xmlTextWriter, outputFieldElements);
					}
				}
				xmlTextWriter.WriteEndElement();
				text = stringWriter.ToString();
			}
			finally
			{
				xmlTextWriter.Close();
				stringWriter.Close();
			}
			return text;
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x0016B954 File Offset: 0x00169B54
		public string ToOldParameterXml()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			string text;
			try
			{
				xmlTextWriter.WriteStartElement("Parameters");
				foreach (object obj in this.Values)
				{
					ParamValueList paramValueList = (ParamValueList)obj;
					for (int i = 0; i < paramValueList.Count; i++)
					{
						paramValueList[i].ToOldParameterXml(xmlTextWriter);
					}
				}
				xmlTextWriter.WriteEndElement();
				text = stringWriter.ToString();
			}
			finally
			{
				xmlTextWriter.Close();
				stringWriter.Close();
			}
			return text;
		}

		// Token: 0x17001F8A RID: 8074
		// (get) Token: 0x06005643 RID: 22083 RVA: 0x0016BA14 File Offset: 0x00169C14
		public NameValueCollection AsNameValueCollection
		{
			get
			{
				NameValueCollection nameValueCollection = new NameValueCollection(this.Count, StringComparer.CurrentCulture);
				foreach (object obj in this.Values)
				{
					ParamValueList paramValueList = (ParamValueList)obj;
					for (int i = 0; i < paramValueList.Count; i++)
					{
						nameValueCollection.Add(paramValueList[i].Name, paramValueList[i].Value);
					}
				}
				return nameValueCollection;
			}
		}

		// Token: 0x04002DB5 RID: 11701
		internal Hashtable m_fields = new Hashtable();

		// Token: 0x04002DB6 RID: 11702
		private const string _ParameterValues = "ParameterValues";
	}
}
