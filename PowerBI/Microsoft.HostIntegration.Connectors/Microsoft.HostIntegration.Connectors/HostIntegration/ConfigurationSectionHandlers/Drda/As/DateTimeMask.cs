using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000545 RID: 1349
	public class DateTimeMask : ConfigurationElement
	{
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x0009892E File Offset: 0x00096B2E
		public DateAndTimeUsage DateAndTimeUsage
		{
			get
			{
				if (this.Db2ToSql.ElementInformation.IsPresent || this.typeDefined == DateAndTimeUsage.Db2ToSql)
				{
					return DateAndTimeUsage.Db2ToSql;
				}
				if (this.SqlToDb2.ElementInformation.IsPresent || this.typeDefined == DateAndTimeUsage.SqlToDb2)
				{
					return DateAndTimeUsage.SqlToDb2;
				}
				return DateAndTimeUsage.Undefined;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x0009896B File Offset: 0x00096B6B
		// (set) Token: 0x06002D93 RID: 11667 RVA: 0x0009897D File Offset: 0x00096B7D
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sqlToDb2", IsRequired = false)]
		public DateTimeFormat SqlToDb2
		{
			get
			{
				return (DateTimeFormat)base["sqlToDb2"];
			}
			set
			{
				base["sqlToDb2"] = value;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x0009898B File Offset: 0x00096B8B
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x0009899D File Offset: 0x00096B9D
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("db2ToSql", IsRequired = false)]
		public DateTimeFormat Db2ToSql
		{
			get
			{
				return (DateTimeFormat)base["db2ToSql"];
			}
			set
			{
				base["db2ToSql"] = value;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000989AC File Offset: 0x00096BAC
		public string DateTimeFormatString
		{
			get
			{
				DateTimeFormats dateTimeFormats;
				if (this.DateAndTimeUsage == DateAndTimeUsage.Db2ToSql)
				{
					dateTimeFormats = this.Db2ToSql.SourceFormat;
				}
				else
				{
					dateTimeFormats = this.SqlToDb2.TargetFormat;
				}
				switch (dateTimeFormats)
				{
				case DateTimeFormats.Db2TimestampFormat:
					return "yyyy-MM-dd-HH.mm.ss.ffffff";
				case DateTimeFormats.IsoTimestampFormat:
					return "yyyy-MM-dd HH.mm.ss.ffffff";
				case DateTimeFormats.SqlServerTimestampFormat:
					return "yyyy-MM-dd HH:mm:ss.ffffff";
				case DateTimeFormats.AnyTimeStampFormat:
					return "yyyy?MM?dd?HH?mm?ss?ffffff";
				default:
					return "";
				}
			}
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x00098A14 File Offset: 0x00096C14
		public object GetElementKey()
		{
			return this.Db2ToSql.SourceFormat.ToString() + this.SqlToDb2.TargetFormat.ToString();
		}

		// Token: 0x04001BF6 RID: 7158
		private DateAndTimeUsage typeDefined;
	}
}
