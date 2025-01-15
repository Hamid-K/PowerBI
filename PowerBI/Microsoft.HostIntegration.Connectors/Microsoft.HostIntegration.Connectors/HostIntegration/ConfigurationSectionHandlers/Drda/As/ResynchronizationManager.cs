using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x0200054A RID: 1354
	public class ResynchronizationManager : ConfigurationElement
	{
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x0002038A File Offset: 0x0001E58A
		// (set) Token: 0x06002DAD RID: 11693 RVA: 0x0002039C File Offset: 0x0001E59C
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x0009AB4C File Offset: 0x00098D4C
		// (set) Token: 0x06002DAF RID: 11695 RVA: 0x0009AB5E File Offset: 0x00098D5E
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("transactionExpiryDuration", IsRequired = false, DefaultValue = "P3D")]
		public string TransactionExpiryDurationString
		{
			get
			{
				return (string)base["transactionExpiryDuration"];
			}
			set
			{
				base["transactionExpiryDuration"] = value;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x0009AB6C File Offset: 0x00098D6C
		// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x0009AB79 File Offset: 0x00098D79
		public TimeSpan TransactionExpiryDuration
		{
			get
			{
				return SoapDuration.Parse(this.TransactionExpiryDurationString);
			}
			set
			{
				this.TransactionExpiryDurationString = SoapDuration.ToString(value);
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x0009AB87 File Offset: 0x00098D87
		// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x0009AB99 File Offset: 0x00098D99
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("resyncRetryDurationInMinutes", IsRequired = false, DefaultValue = 3)]
		public int ResyncRetryDurationInMinutes
		{
			get
			{
				return (int)base["resyncRetryDurationInMinutes"];
			}
			set
			{
				base["resyncRetryDurationInMinutes"] = value;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x0009ABAC File Offset: 0x00098DAC
		// (set) Token: 0x06002DB5 RID: 11701 RVA: 0x0009ABBE File Offset: 0x00098DBE
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("resyncIntervalInMinutes", IsRequired = false, DefaultValue = 1)]
		public int ResyncIntervalInMinutes
		{
			get
			{
				return (int)base["resyncIntervalInMinutes"];
			}
			set
			{
				base["resyncIntervalInMinutes"] = value;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x0009ABD1 File Offset: 0x00098DD1
		// (set) Token: 0x06002DB7 RID: 11703 RVA: 0x0009ABE3 File Offset: 0x00098DE3
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("transactionLogLocation", IsRequired = false, DefaultValue = "")]
		public string TransactionLogLocation
		{
			get
			{
				return (string)base["transactionLogLocation"];
			}
			set
			{
				base["transactionLogLocation"] = value;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x0009ABF1 File Offset: 0x00098DF1
		// (set) Token: 0x06002DB9 RID: 11705 RVA: 0x0009AC03 File Offset: 0x00098E03
		[Description("Controls the locking and row versioning behavior of Transact-SQL statements in case of XA DUW")]
		[Category("General")]
		[ConfigurationProperty("xaIsolationLevel", IsRequired = false, DefaultValue = "SZ")]
		public string XaIsolationLevel
		{
			get
			{
				return (string)base["xaIsolationLevel"];
			}
			set
			{
				base["xaIsolationLevel"] = value;
			}
		}
	}
}
