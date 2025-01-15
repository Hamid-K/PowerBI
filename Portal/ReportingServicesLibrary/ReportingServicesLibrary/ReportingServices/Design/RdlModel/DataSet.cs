using System;
using System.ComponentModel;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003C7 RID: 967
	public sealed class DataSet
	{
		// Token: 0x06001F2A RID: 7978 RVA: 0x0007E188 File Offset: 0x0007C388
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0007E190 File Offset: 0x0007C390
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x0007E198 File Offset: 0x0007C398
		public FieldCollection Fields
		{
			get
			{
				return this.m_fields;
			}
			set
			{
				this.m_fields = value;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0007E1A1 File Offset: 0x0007C3A1
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x0007E1A9 File Offset: 0x0007C3A9
		[DefaultValue(typeof(AutoTrueFalseString), "")]
		public AutoTrueFalseString CaseSensitivity
		{
			get
			{
				return this.m_caseSensitivity;
			}
			set
			{
				this.m_caseSensitivity = value;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0007E1B2 File Offset: 0x0007C3B2
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x0007E1BA File Offset: 0x0007C3BA
		[DefaultValue("Default")]
		public string Collation
		{
			get
			{
				return this.m_collation;
			}
			set
			{
				StringListConverter.ValidStandardValue("Collation", Constants.CollationTypes, "Default", value, ref this.m_collation);
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x0007E1D7 File Offset: 0x0007C3D7
		// (set) Token: 0x06001F32 RID: 7986 RVA: 0x0007E1DF File Offset: 0x0007C3DF
		[DefaultValue(typeof(AutoTrueFalseString), "")]
		public AutoTrueFalseString AccentSensitivity
		{
			get
			{
				return this.m_accentSensitivity;
			}
			set
			{
				this.m_accentSensitivity = value;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x0007E1E8 File Offset: 0x0007C3E8
		// (set) Token: 0x06001F34 RID: 7988 RVA: 0x0007E1F0 File Offset: 0x0007C3F0
		[DefaultValue(typeof(AutoTrueFalseString), "")]
		public AutoTrueFalseString KanatypeSensitivity
		{
			get
			{
				return this.m_kanatypeSensitivity;
			}
			set
			{
				this.m_kanatypeSensitivity = value;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x0007E1F9 File Offset: 0x0007C3F9
		// (set) Token: 0x06001F36 RID: 7990 RVA: 0x0007E201 File Offset: 0x0007C401
		[DefaultValue(typeof(AutoTrueFalseString), "")]
		public AutoTrueFalseString WidthSensitivity
		{
			get
			{
				return this.m_widthSensitivity;
			}
			set
			{
				this.m_widthSensitivity = value;
			}
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x0007E20C File Offset: 0x0007C40C
		public DataSet()
		{
			this.m_fields = new FieldCollection();
			this.m_caseSensitivity = new AutoTrueFalseString();
			this.m_collation = "Default";
			this.m_accentSensitivity = new AutoTrueFalseString();
			this.m_kanatypeSensitivity = new AutoTrueFalseString();
			this.m_widthSensitivity = new AutoTrueFalseString();
			this.CommandType = CommandType.Text;
			this.Timeout = 0;
		}

		// Token: 0x04000D8F RID: 3471
		private AutoTrueFalseString m_caseSensitivity;

		// Token: 0x04000D90 RID: 3472
		private string m_collation;

		// Token: 0x04000D91 RID: 3473
		private AutoTrueFalseString m_accentSensitivity;

		// Token: 0x04000D92 RID: 3474
		private AutoTrueFalseString m_kanatypeSensitivity;

		// Token: 0x04000D93 RID: 3475
		private AutoTrueFalseString m_widthSensitivity;

		// Token: 0x04000D94 RID: 3476
		private FieldCollection m_fields;

		// Token: 0x04000D95 RID: 3477
		public string Name;

		// Token: 0x04000D96 RID: 3478
		public Filters Filters;

		// Token: 0x04000D97 RID: 3479
		[XmlParentElement("Query")]
		public string DataSourceName;

		// Token: 0x04000D98 RID: 3480
		[XmlParentElement("Query")]
		[DefaultValue(CommandType.Text)]
		public CommandType CommandType;

		// Token: 0x04000D99 RID: 3481
		[XmlParentElement("Query")]
		public Expression CommandText = new Expression();

		// Token: 0x04000D9A RID: 3482
		[XmlParentElement("Query")]
		public QueryParameters QueryParameters;

		// Token: 0x04000D9B RID: 3483
		[XmlParentElement("Query")]
		[DefaultValue(0)]
		public int Timeout;
	}
}
