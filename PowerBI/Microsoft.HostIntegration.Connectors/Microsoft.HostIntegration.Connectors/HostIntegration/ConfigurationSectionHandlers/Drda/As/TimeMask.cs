using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000555 RID: 1365
	public class TimeMask : ConfigurationElement
	{
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x0009B033 File Offset: 0x00099233
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

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x0009B070 File Offset: 0x00099270
		// (set) Token: 0x06002E1C RID: 11804 RVA: 0x0009897D File Offset: 0x00096B7D
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sqlToDb2", IsRequired = false)]
		public TimeFormat SqlToDb2
		{
			get
			{
				return (TimeFormat)base["sqlToDb2"];
			}
			set
			{
				base["sqlToDb2"] = value;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x0009B082 File Offset: 0x00099282
		// (set) Token: 0x06002E1E RID: 11806 RVA: 0x0009899D File Offset: 0x00096B9D
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("db2ToSql", IsRequired = false)]
		public TimeFormat Db2ToSql
		{
			get
			{
				return (TimeFormat)base["db2ToSql"];
			}
			set
			{
				base["db2ToSql"] = value;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x0009B094 File Offset: 0x00099294
		public string TimeFormatString
		{
			get
			{
				TimeFormats timeFormats;
				if (this.DateAndTimeUsage == DateAndTimeUsage.Db2ToSql)
				{
					timeFormats = this.Db2ToSql.SourceFormat;
				}
				else
				{
					timeFormats = this.SqlToDb2.TargetFormat;
				}
				switch (timeFormats)
				{
				case TimeFormats.HmsBlank:
					return "HH mm ss";
				case TimeFormats.HmsColon:
					return "HH:mm:ss";
				case TimeFormats.HmsComma:
					return "HH,mm,ss";
				case TimeFormats.HmsPeriod:
					return "HH.mm.ss";
				default:
					return "";
				}
			}
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x0009B0FC File Offset: 0x000992FC
		public object GetElementKey()
		{
			return this.Db2ToSql.SourceFormat.ToString() + this.SqlToDb2.TargetFormat.ToString();
		}

		// Token: 0x04001C06 RID: 7174
		private DateAndTimeUsage typeDefined;
	}
}
