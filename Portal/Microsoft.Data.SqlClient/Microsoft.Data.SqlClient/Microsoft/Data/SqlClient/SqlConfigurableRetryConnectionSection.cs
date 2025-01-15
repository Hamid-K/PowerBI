using System;
using System.Configuration;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000032 RID: 50
	internal class SqlConfigurableRetryConnectionSection : ConfigurationSection, ISqlConfigurableRetryConnectionSection
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0000E5DE File Offset: 0x0000C7DE
		[ConfigurationProperty("retryLogicType")]
		public string RetryLogicType
		{
			get
			{
				return base["retryLogicType"] as string;
			}
			set
			{
				base["retryLogicType"] = value;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0000E5FE File Offset: 0x0000C7FE
		[ConfigurationProperty("retryMethod", IsRequired = true)]
		public string RetryMethod
		{
			get
			{
				return base["retryMethod"] as string;
			}
			set
			{
				base["retryMethod"] = value;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0000E60C File Offset: 0x0000C80C
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0000E61E File Offset: 0x0000C81E
		[ConfigurationProperty("numberOfTries", IsRequired = true, DefaultValue = 1)]
		[IntegerValidator(MinValue = 1, MaxValue = 60, ExcludeRange = false)]
		public int NumberOfTries
		{
			get
			{
				return (int)base["numberOfTries"];
			}
			set
			{
				base["numberOfTries"] = value;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0000E631 File Offset: 0x0000C831
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0000E643 File Offset: 0x0000C843
		[ConfigurationProperty("deltaTime", IsRequired = true)]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "00:02:00", ExcludeRange = false)]
		public TimeSpan DeltaTime
		{
			get
			{
				return (TimeSpan)base["deltaTime"];
			}
			set
			{
				base["deltaTime"] = value;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0000E656 File Offset: 0x0000C856
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0000E668 File Offset: 0x0000C868
		[ConfigurationProperty("minTime", IsRequired = false, DefaultValue = "00:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "00:02:00", ExcludeRange = false)]
		public TimeSpan MinTimeInterval
		{
			get
			{
				return (TimeSpan)base["minTime"];
			}
			set
			{
				base["minTime"] = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0000E67B File Offset: 0x0000C87B
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0000E68D File Offset: 0x0000C88D
		[ConfigurationProperty("maxTime", IsRequired = false)]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "00:02:00", ExcludeRange = false)]
		public TimeSpan MaxTimeInterval
		{
			get
			{
				return (TimeSpan)base["maxTime"];
			}
			set
			{
				base["maxTime"] = value;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0000E6B2 File Offset: 0x0000C8B2
		[ConfigurationProperty("transientErrors", IsRequired = false)]
		[RegexStringValidator("^([ \\t]*(|-)\\d+(?:[ \\t]*,[ \\t]*(|-)\\d+)*[ \\t]*)*$")]
		public string TransientErrors
		{
			get
			{
				return base["transientErrors"] as string;
			}
			set
			{
				base["transientErrors"] = value;
			}
		}

		// Token: 0x040000B1 RID: 177
		public const string Name = "SqlConfigurableRetryLogicConnection";
	}
}
