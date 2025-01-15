using System;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200196A RID: 6506
	public class FirewallGroupInfo
	{
		// Token: 0x0600A52C RID: 42284 RVA: 0x00222F03 File Offset: 0x00221103
		public FirewallGroupInfo(bool trusted, params FirewallGroupType[] groupTypes)
		{
			this.groupTypes = groupTypes;
			this.isTrusted = trusted;
		}

		// Token: 0x17002A2D RID: 10797
		// (get) Token: 0x0600A52D RID: 42285 RVA: 0x00222F19 File Offset: 0x00221119
		public bool IsFixed
		{
			get
			{
				return this.groupTypes.Length == 1;
			}
		}

		// Token: 0x17002A2E RID: 10798
		// (get) Token: 0x0600A52E RID: 42286 RVA: 0x00222F26 File Offset: 0x00221126
		public bool IsTrusted
		{
			get
			{
				return this.isTrusted;
			}
		}

		// Token: 0x17002A2F RID: 10799
		// (get) Token: 0x0600A52F RID: 42287 RVA: 0x00222F2E File Offset: 0x0022112E
		public FirewallGroupType DefaultGroupType
		{
			get
			{
				return this.groupTypes[0];
			}
		}

		// Token: 0x17002A30 RID: 10800
		// (get) Token: 0x0600A530 RID: 42288 RVA: 0x00222F38 File Offset: 0x00221138
		public FirewallGroupType[] GroupTypes
		{
			get
			{
				return this.groupTypes;
			}
		}

		// Token: 0x17002A31 RID: 10801
		// (get) Token: 0x0600A531 RID: 42289 RVA: 0x00222F40 File Offset: 0x00221140
		public static FirewallGroupType[] SettableGroupTypes
		{
			get
			{
				return FirewallGroupInfo.settableGroupTypes;
			}
		}

		// Token: 0x0600A532 RID: 42290 RVA: 0x00222F48 File Offset: 0x00221148
		public static FirewallGroupInfo GetFirewallGroupInfo(Resource resource)
		{
			string kind = resource.Kind;
			if (kind == "File" || kind == "Folder")
			{
				if (FilePath.IsLocalFilePath(resource.Path))
				{
					return FirewallGroupInfo.trustedInfo;
				}
				return FirewallGroupInfo.untrustedInfo;
			}
			else
			{
				if (kind == "CurrentWorkbook" || kind == "MicrosoftInformationProtection")
				{
					return FirewallGroupInfo.trustedInfo;
				}
				return FirewallGroupInfo.untrustedInfo;
			}
		}

		// Token: 0x040055FF RID: 22015
		private readonly bool isTrusted;

		// Token: 0x04005600 RID: 22016
		private readonly FirewallGroupType[] groupTypes;

		// Token: 0x04005601 RID: 22017
		private static readonly FirewallGroupType[] settableGroupTypes = new FirewallGroupType[]
		{
			FirewallGroupType.Public,
			FirewallGroupType.Organizational,
			FirewallGroupType.SeparatePrivate
		};

		// Token: 0x04005602 RID: 22018
		private static FirewallGroupInfo untrustedInfo = new FirewallGroupInfo(false, new FirewallGroupType[]
		{
			FirewallGroupType.Public,
			FirewallGroupType.Organizational,
			FirewallGroupType.SeparatePrivate,
			FirewallGroupType.Named
		});

		// Token: 0x04005603 RID: 22019
		private static FirewallGroupInfo trustedInfo = new FirewallGroupInfo(true, new FirewallGroupType[]
		{
			FirewallGroupType.Public,
			FirewallGroupType.Organizational,
			FirewallGroupType.SeparatePrivate,
			FirewallGroupType.Named
		});
	}
}
