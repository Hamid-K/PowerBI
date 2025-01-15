using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000029 RID: 41
	internal sealed class Fields
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00005990 File Offset: 0x00003B90
		public Fields()
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000059A4 File Offset: 0x00003BA4
		public Fields(Field[] soapFields)
		{
			if (soapFields == null)
			{
				return;
			}
			foreach (Field field in soapFields)
			{
				if (field == null)
				{
					throw new InvalidXmlException();
				}
				string name = field.Name;
				string alias = field.Alias;
				if (name == null || alias == null)
				{
					throw new InvalidXmlException();
				}
				if (this.m_fields[alias] != null)
				{
					throw new InvalidXmlException();
				}
				this.m_fields.Add(alias, name);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005A1C File Offset: 0x00003C1C
		public Field[] ToSoapFieldsArray()
		{
			Field[] array = new Field[this.m_fields.AllKeys.Length];
			int num = 0;
			foreach (string text in this.m_fields.AllKeys)
			{
				string text2 = this.m_fields[text];
				Field field = new Field();
				field.Name = text2;
				field.Alias = text;
				array[num++] = field;
			}
			return array;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005A8F File Offset: 0x00003C8F
		public string[] FieldAliases
		{
			get
			{
				return this.m_fields.AllKeys;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005A9C File Offset: 0x00003C9C
		public string[] FieldNames
		{
			get
			{
				string[] array = new string[this.m_fields.Count];
				int num = 0;
				foreach (string text in this.m_fields.AllKeys)
				{
					array[num++] = this.m_fields[text];
				}
				return array;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public NameValueCollection FieldSet
		{
			get
			{
				return this.m_fields;
			}
		}

		// Token: 0x040000FA RID: 250
		internal const string _Fields = "Fields";

		// Token: 0x040000FB RID: 251
		private const string _Field = "Field";

		// Token: 0x040000FC RID: 252
		private const string _Alias = "Alias";

		// Token: 0x040000FD RID: 253
		private const string _Name = "Name";

		// Token: 0x040000FE RID: 254
		private NameValueCollection m_fields = new NameValueCollection();
	}
}
