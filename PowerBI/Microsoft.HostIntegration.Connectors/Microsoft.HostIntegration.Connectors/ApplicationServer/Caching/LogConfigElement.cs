using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000129 RID: 297
	internal class LogConfigElement : ConfigurationElement
	{
		// Token: 0x0600088B RID: 2187 RVA: 0x00015607 File Offset: 0x00013807
		internal LogConfigElement()
		{
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001E9FD File Offset: 0x0001CBFD
		internal LogConfigElement(string location)
		{
			this.Location = location;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0001EA0C File Offset: 0x0001CC0C
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x0001EA1E File Offset: 0x0001CC1E
		[ConfigurationProperty("location", DefaultValue = ".", IsRequired = true)]
		internal string Location
		{
			get
			{
				return (string)base["location"];
			}
			set
			{
				base["location"] = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0001EA2C File Offset: 0x0001CC2C
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x0001EA3E File Offset: 0x0001CC3E
		[IntegerValidator(MinValue = -1, MaxValue = 3)]
		[ConfigurationProperty("logLevel", DefaultValue = -1, IsRequired = true)]
		internal int Level
		{
			get
			{
				return (int)base["logLevel"];
			}
			set
			{
				base["logLevel"] = value;
			}
		}

		// Token: 0x0400068C RID: 1676
		internal const string LOCATION = "location";

		// Token: 0x0400068D RID: 1677
		internal const string LEVEL = "logLevel";

		// Token: 0x0400068E RID: 1678
		internal const int DefaultLogLevel = -1;

		// Token: 0x0400068F RID: 1679
		internal const string DefaultLogLocation = ".";
	}
}
