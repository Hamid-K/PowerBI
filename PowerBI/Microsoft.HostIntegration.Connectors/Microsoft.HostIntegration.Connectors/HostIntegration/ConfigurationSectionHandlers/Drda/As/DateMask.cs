using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000541 RID: 1345
	public class DateMask : ConfigurationElement
	{
		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x00098689 File Offset: 0x00096889
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

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x000986C6 File Offset: 0x000968C6
		// (set) Token: 0x06002D7A RID: 11642 RVA: 0x000986D8 File Offset: 0x000968D8
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sqlToDb2", IsRequired = false)]
		public DateFormat SqlToDb2
		{
			get
			{
				return (DateFormat)base["sqlToDb2"];
			}
			set
			{
				base["sqlToDb2"] = value;
				this.typeDefined = DateAndTimeUsage.SqlToDb2;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x000986ED File Offset: 0x000968ED
		// (set) Token: 0x06002D7C RID: 11644 RVA: 0x000986FF File Offset: 0x000968FF
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("db2ToSql", IsRequired = false)]
		public DateFormat Db2ToSql
		{
			get
			{
				return (DateFormat)base["db2ToSql"];
			}
			set
			{
				base["db2ToSql"] = value;
				this.typeDefined = DateAndTimeUsage.Db2ToSql;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x00098714 File Offset: 0x00096914
		public string DateFormatString
		{
			get
			{
				DateFormats dateFormats;
				if (this.DateAndTimeUsage == DateAndTimeUsage.Db2ToSql)
				{
					dateFormats = this.Db2ToSql.SourceFormat;
				}
				else
				{
					dateFormats = this.SqlToDb2.TargetFormat;
				}
				switch (dateFormats)
				{
				case DateFormats.Iso:
					return "yyyy-MM-dd";
				case DateFormats.Usa:
					return "MM/dd/yyyy";
				case DateFormats.Eur:
					return "dd.MM.yyyy";
				case DateFormats.Jis:
					return "yyyy-MM-dd";
				case DateFormats.DmyBlank:
					return "dd MM yy";
				case DateFormats.DmyComma:
					return "dd,MM,yy";
				case DateFormats.DmyHyphen:
					return "dd-MM-yy";
				case DateFormats.DmyPeriod:
					return "dd.MM.yy";
				case DateFormats.DmySlash:
					return "dd/MM/yy";
				case DateFormats.JulBlank:
					return "yy ddd";
				case DateFormats.JulComma:
					return "yy,ddd";
				case DateFormats.JulHyphen:
					return "yy-ddd";
				case DateFormats.JulPeriod:
					return "yy.ddd";
				case DateFormats.JulSlash:
					return "yy/ddd";
				case DateFormats.MdyBlank:
					return "MM dd yy";
				case DateFormats.MdyComma:
					return "MM,dd,yy";
				case DateFormats.MdyHyphen:
					return "MM-dd-yy";
				case DateFormats.MdyPeriod:
					return "MM.dd.yy";
				case DateFormats.MdySlash:
					return "MM/dd/yy";
				case DateFormats.YmdBlank:
					return "yy MM dd";
				case DateFormats.YmdComma:
					return "yy,MM,dd";
				case DateFormats.YmdHyphen:
					return "yy-MM-dd";
				case DateFormats.YmdPeriod:
					return "yy.MM.dd";
				case DateFormats.YmdSlash:
					return "yy/MM/dd";
				default:
					return "";
				}
			}
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x00098848 File Offset: 0x00096A48
		public object GetElementKey()
		{
			return this.Db2ToSql.SourceFormat.ToString() + this.SqlToDb2.TargetFormat.ToString();
		}

		// Token: 0x04001BEE RID: 7150
		private DateAndTimeUsage typeDefined;
	}
}
