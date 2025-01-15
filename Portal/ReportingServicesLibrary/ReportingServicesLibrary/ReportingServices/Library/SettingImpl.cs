using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000040 RID: 64
	internal sealed class SettingImpl : Setting
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000F570 File Offset: 0x0000D770
		public string FieldName
		{
			get
			{
				return base.Field;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000F578 File Offset: 0x0000D778
		internal SettingImpl(Settings parent)
		{
			this.m_parent = parent;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000F588 File Offset: 0x0000D788
		internal SettingImpl(ParameterValueOrFieldReference param, Settings parent)
		{
			this.m_parent = parent;
			if (param is ParameterFieldReference)
			{
				ParameterFieldReference parameterFieldReference = (ParameterFieldReference)param;
				base.Name = parameterFieldReference.ParameterName;
				base.Field = parameterFieldReference.FieldAlias;
				this.m_parent.AddField(base.Field);
				return;
			}
			ParameterValue parameterValue = (ParameterValue)param;
			base.Name = parameterValue.Name;
			base.Value = parameterValue.Value;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000F5FA File Offset: 0x0000D7FA
		public bool HasValidValues()
		{
			return base.ValidValues.Length != 0;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000F606 File Offset: 0x0000D806
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000F60E File Offset: 0x0000D80E
		public bool EncryptFieldValue
		{
			get
			{
				return this.m_encryptFieldValue;
			}
			set
			{
				this.m_encryptFieldValue = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000F617 File Offset: 0x0000D817
		public string FieldValue
		{
			get
			{
				if (!this.m_encryptFieldValue)
				{
					return this.m_parent.GetFieldValue(this.FieldName);
				}
				return CatalogEncryption.Instance.EncryptToString(this.m_parent.GetFieldValue(this.FieldName), base.Name);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000F654 File Offset: 0x0000D854
		public bool IsValid()
		{
			bool flag = true;
			if (base.Error != null && base.Error != "")
			{
				flag = false;
			}
			else if (this.FieldName != null && this.FieldName != "" && base.Value != null && base.Value != "")
			{
				flag = false;
			}
			if (this.UseValue && flag && !this.IsValidValue)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public bool UseValue
		{
			get
			{
				return !this.UseField;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000F6D9 File Offset: 0x0000D8D9
		public bool UseField
		{
			get
			{
				return this.FieldName != null && this.FieldName != "";
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		public bool IsValidValue
		{
			get
			{
				bool flag = false;
				if (!this.HasValidValues())
				{
					flag = true;
				}
				else
				{
					Microsoft.ReportingServices.Interfaces.ValidValue[] validValues = base.ValidValues;
					for (int i = 0; i < validValues.Length; i++)
					{
						if (validValues[i].Value == base.Value)
						{
							flag = true;
							break;
						}
					}
				}
				return flag;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000F744 File Offset: 0x0000D944
		public string ToXml(bool outputFieldValues)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			string text;
			try
			{
				xmlTextWriter.WriteStartElement("Setting");
				xmlTextWriter.WriteElementString("Name", base.Name);
				if (outputFieldValues)
				{
					if (this.UseField)
					{
						xmlTextWriter.WriteElementString("Field", this.FieldName);
					}
					else
					{
						xmlTextWriter.WriteElementString("Value", base.Value);
					}
				}
				else
				{
					xmlTextWriter.WriteStartElement("Value");
					if (this.UseField)
					{
						xmlTextWriter.WriteString(this.FieldValue);
					}
					else
					{
						xmlTextWriter.WriteString(base.Value);
					}
					xmlTextWriter.WriteEndElement();
				}
				xmlTextWriter.WriteEndElement();
				text = stringWriter.ToString();
			}
			finally
			{
				stringWriter.Close();
				xmlTextWriter.Close();
			}
			return text;
		}

		// Token: 0x0400013A RID: 314
		internal const string _SettingElement = "Setting";

		// Token: 0x0400013B RID: 315
		internal const string _NameElement = "Name";

		// Token: 0x0400013C RID: 316
		internal const string _ValueElement = "Value";

		// Token: 0x0400013D RID: 317
		internal const string _FieldElement = "Field";

		// Token: 0x0400013E RID: 318
		internal const string _ErrorElement = "Error";

		// Token: 0x0400013F RID: 319
		internal const string _Required = "Required";

		// Token: 0x04000140 RID: 320
		internal const string _ReadOnly = "ReadOnly";

		// Token: 0x04000141 RID: 321
		internal const string _ValidValuesElement = "ValidValues";

		// Token: 0x04000142 RID: 322
		private Settings m_parent;

		// Token: 0x04000143 RID: 323
		private bool m_encryptFieldValue;
	}
}
