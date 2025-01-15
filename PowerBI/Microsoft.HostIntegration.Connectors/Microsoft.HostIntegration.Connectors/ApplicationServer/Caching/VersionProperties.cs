using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	internal class VersionProperties : ConfigurationElement, ISerializable
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x00015607 File Offset: 0x00013807
		internal VersionProperties()
		{
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00017686 File Offset: 0x00015886
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x00017698 File Offset: 0x00015898
		[ConfigurationProperty("beginClientVersion", IsRequired = false, DefaultValue = 1000L)]
		internal long BeginClientVersion
		{
			get
			{
				return (long)base["beginClientVersion"];
			}
			set
			{
				base["beginClientVersion"] = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x000176AB File Offset: 0x000158AB
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x000176BD File Offset: 0x000158BD
		[ConfigurationProperty("endClientVersion", IsRequired = false, DefaultValue = 1002L)]
		internal long EndClientVersion
		{
			get
			{
				return (long)base["endClientVersion"];
			}
			set
			{
				base["endClientVersion"] = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000176D0 File Offset: 0x000158D0
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x000176E2 File Offset: 0x000158E2
		[ConfigurationProperty("beginServerVersion", IsRequired = false, DefaultValue = 1002L)]
		internal long BeginServerVersion
		{
			get
			{
				return (long)base["beginServerVersion"];
			}
			set
			{
				base["beginServerVersion"] = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000176F5 File Offset: 0x000158F5
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00017707 File Offset: 0x00015907
		[ConfigurationProperty("endServerVersion", IsRequired = false, DefaultValue = 1002L)]
		internal long EndServerVersion
		{
			get
			{
				return (long)base["endServerVersion"];
			}
			set
			{
				base["endServerVersion"] = value;
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001771C File Offset: 0x0001591C
		protected VersionProperties(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this.BeginClientVersion = info.GetInt64("beginClientVersion");
				this.EndClientVersion = info.GetInt64("endClientVersion");
				this.BeginServerVersion = info.GetInt64("beginServerVersion");
				this.EndServerVersion = info.GetInt64("endServerVersion");
			}
			catch (SerializationException)
			{
				this.BeginClientVersion = 1002L;
				this.EndClientVersion = 1002L;
				this.BeginServerVersion = 1002L;
				this.EndServerVersion = 1002L;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000177B8 File Offset: 0x000159B8
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("beginClientVersion", this.BeginClientVersion);
			info.AddValue("endClientVersion", this.EndClientVersion);
			info.AddValue("beginServerVersion", this.BeginServerVersion);
			info.AddValue("endServerVersion", this.EndServerVersion);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001780C File Offset: 0x00015A0C
		public VersionPropertiesChange ComputeDifferences(VersionProperties other)
		{
			VersionPropertiesChange versionPropertiesChange = default(VersionPropertiesChange);
			versionPropertiesChange[VersionPropertiesChanges.BeginClientVersionChange] = this.BeginClientVersion != other.BeginClientVersion;
			versionPropertiesChange[VersionPropertiesChanges.EndClientVersionChange] = this.EndClientVersion != other.EndClientVersion;
			versionPropertiesChange[VersionPropertiesChanges.BeginServerVersionChange] = this.BeginServerVersion != other.BeginServerVersion;
			versionPropertiesChange[VersionPropertiesChanges.EndServerVersionChange] = this.EndServerVersion != other.EndServerVersion;
			return versionPropertiesChange;
		}

		// Token: 0x040003A5 RID: 933
		internal const string BEGIN_CLIENT_VERSION = "beginClientVersion";

		// Token: 0x040003A6 RID: 934
		internal const string END_CLIENT_VERSION = "endClientVersion";

		// Token: 0x040003A7 RID: 935
		internal const string BEGIN_SERVER_VERSION = "beginServerVersion";

		// Token: 0x040003A8 RID: 936
		internal const string END_SERVER_VERSION = "endServerVersion";
	}
}
