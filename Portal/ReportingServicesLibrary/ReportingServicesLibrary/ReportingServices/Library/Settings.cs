using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200003F RID: 63
	internal sealed class Settings : CollectionBase
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000F10F File Offset: 0x0000D30F
		public Settings()
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000F130 File Offset: 0x0000D330
		public Settings(Setting[] settings)
		{
			foreach (Setting setting in settings)
			{
				SettingImpl settingImpl = new SettingImpl(this);
				settingImpl.Error = setting.Error;
				settingImpl.Field = setting.Field;
				settingImpl.Name = setting.Name;
				settingImpl.Required = setting.Required;
				settingImpl.Value = setting.Value;
				base.InnerList.Add(settingImpl);
				this.m_allSettings.Add(settingImpl.Name, settingImpl);
			}
		}

		// Token: 0x17000048 RID: 72
		public Setting this[int i]
		{
			get
			{
				return (Setting)base.InnerList[i];
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000F1E2 File Offset: 0x0000D3E2
		public Setting[] SettingsArray
		{
			get
			{
				return (Setting[])base.InnerList.ToArray(typeof(Setting));
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000F200 File Offset: 0x0000D400
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

		// Token: 0x060001E1 RID: 481 RVA: 0x0000F27C File Offset: 0x0000D47C
		internal void AddField(string fieldName)
		{
			if (this.m_fields.Contains(fieldName))
			{
				return;
			}
			this.m_fields.Add(fieldName, null);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000F29A File Offset: 0x0000D49A
		internal string GetFieldValue(string fieldName)
		{
			return (string)this.m_fields[fieldName];
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000F2AD File Offset: 0x0000D4AD
		internal void AddFieldValue(string fieldName, string fieldValue)
		{
			if (this.m_fields[fieldName] == null)
			{
				return;
			}
			this.m_fields[fieldName] = fieldValue;
		}

		// Token: 0x1700004B RID: 75
		internal SettingImpl this[string name]
		{
			get
			{
				return (SettingImpl)this.m_allSettings[name];
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		internal void RemoveSetting(string name)
		{
			SettingImpl settingImpl = this[name];
			Global.m_Tracer.Assert(!settingImpl.UseField, "Only static field settings should be removed before sending extension settings to the client");
			this.m_allSettings.Remove(name);
			base.InnerList.Remove(settingImpl);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000F328 File Offset: 0x0000D528
		public void FromSoapParameterValueArray(ParameterValueOrFieldReference[] parameters)
		{
			if (parameters == null)
			{
				return;
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				SettingImpl settingImpl = new SettingImpl(parameters[i], this);
				base.InnerList.Add(settingImpl);
				this.m_allSettings.Add(settingImpl.Name, settingImpl);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000F374 File Offset: 0x0000D574
		public ParameterValue[] ToSoapParameterValueArray()
		{
			ParameterValue[] array = new ParameterValue[base.Count];
			int num = 0;
			foreach (object obj in this)
			{
				SettingImpl settingImpl = (SettingImpl)obj;
				array[num] = new ParameterValue();
				array[num].Name = settingImpl.Name;
				if (settingImpl.UseField)
				{
					array[num].Value = settingImpl.FieldValue;
				}
				else
				{
					array[num].Value = settingImpl.Value;
				}
				num++;
			}
			return array;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000F414 File Offset: 0x0000D614
		public ParameterValueOrFieldReference[] ToSoapParameterValueOrFieldReferenceArray()
		{
			ParameterValueOrFieldReference[] array = new ParameterValueOrFieldReference[base.Count];
			int num = 0;
			foreach (object obj in this)
			{
				SettingImpl settingImpl = (SettingImpl)obj;
				ParameterValueOrFieldReference parameterValueOrFieldReference;
				if (settingImpl.UseField)
				{
					parameterValueOrFieldReference = new ParameterFieldReference
					{
						FieldAlias = settingImpl.Field,
						ParameterName = settingImpl.Name
					};
				}
				else
				{
					parameterValueOrFieldReference = new ParameterValue
					{
						Name = settingImpl.Name,
						Value = settingImpl.Value
					};
				}
				array[num] = parameterValueOrFieldReference;
				num++;
			}
			return array;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		public string ToXml(bool outputFieldValues)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			string text;
			try
			{
				xmlTextWriter.WriteStartElement("Settings");
				foreach (object obj in this)
				{
					SettingImpl settingImpl = (SettingImpl)obj;
					xmlTextWriter.WriteRaw(settingImpl.ToXml(outputFieldValues));
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

		// Token: 0x04000137 RID: 311
		internal const string _SettingsElement = "Settings";

		// Token: 0x04000138 RID: 312
		internal Hashtable m_fields = new Hashtable();

		// Token: 0x04000139 RID: 313
		private Hashtable m_allSettings = new Hashtable();
	}
}
