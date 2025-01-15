using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000BB RID: 187
	[Serializable]
	internal class RequestRetryElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00015607 File Offset: 0x00013807
		internal RequestRetryElement()
		{
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00015BBE File Offset: 0x00013DBE
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00015BD0 File Offset: 0x00013DD0
		[ConfigurationProperty("maxAttempts", DefaultValue = 2, IsRequired = false)]
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

		// Token: 0x0600049A RID: 1178 RVA: 0x00015C7F File Offset: 0x00013E7F
		protected RequestRetryElement(SerializationInfo info, StreamingContext context)
		{
			this.MaxAttempts = (int)info.GetValue("maxAttempts", typeof(int));
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015CA7 File Offset: 0x00013EA7
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("maxAttempts", this.MaxAttempts);
		}

		// Token: 0x0400035F RID: 863
		internal const string MAX_ATTEMPT = "maxAttempts";

		// Token: 0x04000360 RID: 864
		internal const int MAX_ATTEMPT_DEFAULT = 2;
	}
}
