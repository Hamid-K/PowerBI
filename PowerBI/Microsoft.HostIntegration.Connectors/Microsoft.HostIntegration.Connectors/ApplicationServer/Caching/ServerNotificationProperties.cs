using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000138 RID: 312
	[Serializable]
	internal class ServerNotificationProperties : ConfigurationElement, ISerializable
	{
		// Token: 0x06000950 RID: 2384 RVA: 0x00015607 File Offset: 0x00013807
		public ServerNotificationProperties()
		{
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00015FAF File Offset: 0x000141AF
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x00015FC1 File Offset: 0x000141C1
		[ConfigurationProperty("isEnabled", DefaultValue = false, IsRequired = false)]
		public bool IsEnabled
		{
			get
			{
				return (bool)base["isEnabled"];
			}
			set
			{
				base["isEnabled"] = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0002009D File Offset: 0x0001E29D
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x000200AF File Offset: 0x0001E2AF
		[ConfigurationProperty("maxEvents", DefaultValue = 1024, IsRequired = false)]
		[IntegerValidator(MinValue = 1)]
		public int MaxEvents
		{
			get
			{
				return (int)base["maxEvents"];
			}
			set
			{
				base["maxEvents"] = value;
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x000200C2 File Offset: 0x0001E2C2
		public ServerNotificationProperties(SerializationInfo info, StreamingContext context)
		{
			this.IsEnabled = info.GetBoolean("isEnabled");
			this.MaxEvents = (int)info.GetValue("maxEvents", typeof(int));
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000200FB File Offset: 0x0001E2FB
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("maxEvents", this.MaxEvents);
			info.AddValue("isEnabled", this.IsEnabled);
		}

		// Token: 0x040006C8 RID: 1736
		internal const string MAX_EVENTS = "maxEvents";

		// Token: 0x040006C9 RID: 1737
		internal const string IS_ENABLED = "isEnabled";
	}
}
