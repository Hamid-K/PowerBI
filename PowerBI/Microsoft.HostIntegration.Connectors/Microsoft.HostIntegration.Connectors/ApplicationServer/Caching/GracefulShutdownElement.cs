using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000E3 RID: 227
	[Serializable]
	internal class GracefulShutdownElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x00015607 File Offset: 0x00013807
		internal GracefulShutdownElement()
		{
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00019F69 File Offset: 0x00018169
		internal GracefulShutdownElement(bool gracefulShutdownEnabled, int timeoutInMilliseconds)
		{
			this.Enabled = gracefulShutdownEnabled;
			if (this.Enabled)
			{
				this.Timeout = timeoutInMilliseconds;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00019F87 File Offset: 0x00018187
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x00019F99 File Offset: 0x00018199
		[ConfigurationProperty("enabled", DefaultValue = false, IsRequired = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base["enabled"];
			}
			set
			{
				base["enabled"] = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00019FAC File Offset: 0x000181AC
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x00019FBE File Offset: 0x000181BE
		[ConfigurationProperty("timeout", DefaultValue = 0, IsRequired = false)]
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

		// Token: 0x06000689 RID: 1673 RVA: 0x00019FD1 File Offset: 0x000181D1
		protected GracefulShutdownElement(SerializationInfo info, StreamingContext context)
		{
			this.Enabled = info.GetBoolean("enabled");
			this.Timeout = info.GetInt32("timeout");
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00019FFB File Offset: 0x000181FB
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new SerializationException();
			}
			info.AddValue("enabled", this.Enabled);
			info.AddValue("timeout", this.Timeout);
		}

		// Token: 0x040003ED RID: 1005
		internal const string ENABLED = "enabled";

		// Token: 0x040003EE RID: 1006
		internal const string TIMEOUT = "timeout";
	}
}
