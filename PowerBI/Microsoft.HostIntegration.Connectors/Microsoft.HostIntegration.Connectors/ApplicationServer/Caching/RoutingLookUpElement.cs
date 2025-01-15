using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	internal class RoutingLookUpElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x00015607 File Offset: 0x00013807
		internal RoutingLookUpElement()
		{
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00015BBE File Offset: 0x00013DBE
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x00015BD0 File Offset: 0x00013DD0
		[ConfigurationProperty("maxAttempts", DefaultValue = 600, IsRequired = false)]
		public int MaxAttempts
		{
			get
			{
				return (int)base["maxAttempts"];
			}
			set
			{
				base["maxAttempts"] = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00015BE3 File Offset: 0x00013DE3
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x00015BF5 File Offset: 0x00013DF5
		[ConfigurationProperty("waitInterval", DefaultValue = 1000, IsRequired = false)]
		public int WaitInterval
		{
			get
			{
				return (int)base["waitInterval"];
			}
			set
			{
				base["waitInterval"] = value;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00015C08 File Offset: 0x00013E08
		protected RoutingLookUpElement(SerializationInfo info, StreamingContext context)
		{
			this.MaxAttempts = (int)info.GetValue("maxAttempts", typeof(int));
			this.WaitInterval = (int)info.GetValue("waitInterval", typeof(int));
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00015C5B File Offset: 0x00013E5B
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("maxAttempts", this.MaxAttempts);
			info.AddValue("waitInterval", this.WaitInterval);
		}

		// Token: 0x0400035B RID: 859
		internal const string MAX_ATTEMPT = "maxAttempts";

		// Token: 0x0400035C RID: 860
		internal const string WAIT_INTERVAL = "waitInterval";

		// Token: 0x0400035D RID: 861
		internal const int MAX_ATTEMPT_DEFAULT = 600;

		// Token: 0x0400035E RID: 862
		internal const int WAIT_INTERVAL_DEFAULT = 1000;
	}
}
