using System;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FB9 RID: 4025
	internal class ActiveDirectoryAttributeSchema
	{
		// Token: 0x060069C6 RID: 27078 RVA: 0x0016BDE4 File Offset: 0x00169FE4
		public ActiveDirectoryAttributeSchema(string ldapDisplayName, string syntax, bool isSingleValued, int systemFlags)
		{
			this.ldapDisplayName = ldapDisplayName;
			this.syntax = syntax;
			this.isSingleValued = isSingleValued;
			this.systemFlags = systemFlags;
		}

		// Token: 0x17001E61 RID: 7777
		// (get) Token: 0x060069C7 RID: 27079 RVA: 0x0016BE09 File Offset: 0x0016A009
		public string LdapDisplayName
		{
			get
			{
				return this.ldapDisplayName;
			}
		}

		// Token: 0x17001E62 RID: 7778
		// (get) Token: 0x060069C8 RID: 27080 RVA: 0x0016BE11 File Offset: 0x0016A011
		public string Syntax
		{
			get
			{
				return this.syntax;
			}
		}

		// Token: 0x17001E63 RID: 7779
		// (get) Token: 0x060069C9 RID: 27081 RVA: 0x0016BE19 File Offset: 0x0016A019
		public bool IsSingleValued
		{
			get
			{
				return this.isSingleValued;
			}
		}

		// Token: 0x17001E64 RID: 7780
		// (get) Token: 0x060069CA RID: 27082 RVA: 0x0016BE21 File Offset: 0x0016A021
		public bool IsConstructed
		{
			get
			{
				return (this.systemFlags & 4) != 0;
			}
		}

		// Token: 0x04003A95 RID: 14997
		private readonly string ldapDisplayName;

		// Token: 0x04003A96 RID: 14998
		private readonly string syntax;

		// Token: 0x04003A97 RID: 14999
		private readonly bool isSingleValued;

		// Token: 0x04003A98 RID: 15000
		private readonly int systemFlags;

		// Token: 0x02000FBA RID: 4026
		private enum SystemFlags
		{
			// Token: 0x04003A9A RID: 15002
			ADS_SYSTEMFLAG_ATTR_NOT_REPLICATED = 1,
			// Token: 0x04003A9B RID: 15003
			ADS_SYSTEMFLAG_ATTR_IS_CONSTRUCTED = 4
		}
	}
}
