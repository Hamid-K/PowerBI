using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.MqClient
{
	// Token: 0x0200051A RID: 1306
	public class QueueManagerBehavior : ConfigurationElement
	{
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x000973CB File Offset: 0x000955CB
		// (set) Token: 0x06002C2A RID: 11306 RVA: 0x000973DD File Offset: 0x000955DD
		[Description("Allow different client calls to the same WebSphere MQ Queue Manager to use the same infrastructure")]
		[Category("General")]
		[ConfigurationProperty("pool", IsRequired = false, DefaultValue = true)]
		public bool Pool
		{
			get
			{
				return (bool)base["pool"];
			}
			set
			{
				base["pool"] = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x000973F0 File Offset: 0x000955F0
		// (set) Token: 0x06002C2C RID: 11308 RVA: 0x00097402 File Offset: 0x00095602
		[Description("Allow client calls to the same WebSphere MQ Queue Manager via different Channels to use the same TCP Socket")]
		[Category("General")]
		[ConfigurationProperty("allowDifferentChannels", IsRequired = false, DefaultValue = true)]
		public bool AllowDifferentChannels
		{
			get
			{
				return (bool)base["allowDifferentChannels"];
			}
			set
			{
				base["allowDifferentChannels"] = value;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x00097415 File Offset: 0x00095615
		// (set) Token: 0x06002C2E RID: 11310 RVA: 0x00097427 File Offset: 0x00095627
		[Description("Force client calls from threads with different UserIds to use a different infrastructure when talking to the same WebSphere MQ Queue Manager")]
		[Category("General")]
		[ConfigurationProperty("differentUserDifferentConversation", IsRequired = false, DefaultValue = true)]
		public bool DifferentUserDifferentConversation
		{
			get
			{
				return (bool)base["differentUserDifferentConversation"];
			}
			set
			{
				base["differentUserDifferentConversation"] = value;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x00019FAC File Offset: 0x000181AC
		// (set) Token: 0x06002C30 RID: 11312 RVA: 0x00019FBE File Offset: 0x000181BE
		[Description("The number of seconds to keep a connection to a WebSphere MQ Queue Manager after the last Disconnect")]
		[Category("General")]
		[ConfigurationProperty("timeout", IsRequired = false, DefaultValue = 60)]
		public int Timeout
		{
			get
			{
				return (int)base["timeout"];
			}
			set
			{
				base["timeout"] = value;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x0009743A File Offset: 0x0009563A
		// (set) Token: 0x06002C32 RID: 11314 RVA: 0x0009744C File Offset: 0x0009564C
		[Description("The number of ClassLibrary Queue Manager instances which share a single Conversation")]
		[Category("General")]
		[ConfigurationProperty("queueManagersPerConversation", IsRequired = false, DefaultValue = 5)]
		public int QueueManagersPerConversation
		{
			get
			{
				return (int)base["queueManagersPerConversation"];
			}
			set
			{
				base["queueManagersPerConversation"] = value;
			}
		}
	}
}
