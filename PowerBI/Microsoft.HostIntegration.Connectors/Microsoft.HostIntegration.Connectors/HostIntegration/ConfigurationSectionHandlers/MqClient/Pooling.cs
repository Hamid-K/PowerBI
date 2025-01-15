using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.MqClient
{
	// Token: 0x02000515 RID: 1301
	public class Pooling : ConfigurationElement
	{
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x00097179 File Offset: 0x00095379
		// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x0009718B File Offset: 0x0009538B
		[Description("Pooling behavior for Queue Managers")]
		[Category("General")]
		[ConfigurationProperty("queueManagerBehavior", IsRequired = false)]
		public QueueManagerBehavior QueueManagerBehavior
		{
			get
			{
				return (QueueManagerBehavior)base["queueManagerBehavior"];
			}
			set
			{
				base["queueManagerBehavior"] = value;
			}
		}
	}
}
