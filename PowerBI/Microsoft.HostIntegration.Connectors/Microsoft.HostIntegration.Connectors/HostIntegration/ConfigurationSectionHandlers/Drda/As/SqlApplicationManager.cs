using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000550 RID: 1360
	public class SqlApplicationManager : ConfigurationElement
	{
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x0002038A File Offset: 0x0001E58A
		// (set) Token: 0x06002E07 RID: 11783 RVA: 0x0002039C File Offset: 0x0001E59C
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

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x0009AF23 File Offset: 0x00099123
		// (set) Token: 0x06002E09 RID: 11785 RVA: 0x0009AF35 File Offset: 0x00099135
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("rollbackTransactionOnError", IsRequired = false, DefaultValue = "true")]
		public bool RollbackTransactionOnError
		{
			get
			{
				return (bool)base["rollbackTransactionOnError"];
			}
			set
			{
				base["rollbackTransactionOnError"] = value;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x0009AF48 File Offset: 0x00099148
		// (set) Token: 0x06002E0B RID: 11787 RVA: 0x0009AF5A File Offset: 0x0009915A
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("minServerPriority", IsRequired = false, DefaultValue = 1)]
		public int MinServerPriority
		{
			get
			{
				return (int)base["minServerPriority"];
			}
			set
			{
				base["minServerPriority"] = value;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x0009AF6D File Offset: 0x0009916D
		// (set) Token: 0x06002E0D RID: 11789 RVA: 0x0009AF7F File Offset: 0x0009917F
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("maxServerPriority", IsRequired = false, DefaultValue = 1)]
		public int MaxServerPriority
		{
			get
			{
				return (int)base["maxServerPriority"];
			}
			set
			{
				base["maxServerPriority"] = value;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x0009AF92 File Offset: 0x00099192
		// (set) Token: 0x06002E0F RID: 11791 RVA: 0x0009AFA4 File Offset: 0x000991A4
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("disableXaTransaction", IsRequired = false, DefaultValue = "false")]
		public bool DisableXaTransaction
		{
			get
			{
				return (bool)base["disableXaTransaction"];
			}
			set
			{
				base["disableXaTransaction"] = value;
			}
		}
	}
}
