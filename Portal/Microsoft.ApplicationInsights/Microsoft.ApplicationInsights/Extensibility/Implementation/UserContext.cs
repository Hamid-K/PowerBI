using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000088 RID: 136
	public sealed class UserContext
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x000131D8 File Offset: 0x000113D8
		internal UserContext()
		{
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000131E0 File Offset: 0x000113E0
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x000131F7 File Offset: 0x000113F7
		public string Id
		{
			get
			{
				if (!string.IsNullOrEmpty(this.id))
				{
					return this.id;
				}
				return null;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00013200 File Offset: 0x00011400
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x00013217 File Offset: 0x00011417
		public string AccountId
		{
			get
			{
				if (!string.IsNullOrEmpty(this.accountId))
				{
					return this.accountId;
				}
				return null;
			}
			set
			{
				this.accountId = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00013220 File Offset: 0x00011420
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00013237 File Offset: 0x00011437
		public string UserAgent
		{
			get
			{
				if (!string.IsNullOrEmpty(this.userAgent))
				{
					return this.userAgent;
				}
				return null;
			}
			set
			{
				this.userAgent = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00013240 File Offset: 0x00011440
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x00013257 File Offset: 0x00011457
		public string AuthenticatedUserId
		{
			get
			{
				if (!string.IsNullOrEmpty(this.authenticatedUserId))
				{
					return this.authenticatedUserId;
				}
				return null;
			}
			set
			{
				this.authenticatedUserId = value;
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00013260 File Offset: 0x00011460
		internal void UpdateTags(IDictionary<string, string> tags)
		{
			tags.UpdateTagValue(ContextTagKeys.Keys.UserId, this.Id);
			tags.UpdateTagValue(ContextTagKeys.Keys.UserAccountId, this.AccountId);
			tags.UpdateTagValue(ContextTagKeys.Keys.UserAuthUserId, this.AuthenticatedUserId);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000132B0 File Offset: 0x000114B0
		internal void CopyTo(UserContext target)
		{
			Tags.CopyTagValue(this.Id, ref target.id);
			Tags.CopyTagValue(this.AccountId, ref target.accountId);
			Tags.CopyTagValue(this.UserAgent, ref target.userAgent);
			Tags.CopyTagValue(this.AuthenticatedUserId, ref target.authenticatedUserId);
		}

		// Token: 0x040001AB RID: 427
		private string id;

		// Token: 0x040001AC RID: 428
		private string accountId;

		// Token: 0x040001AD RID: 429
		private string userAgent;

		// Token: 0x040001AE RID: 430
		private string authenticatedUserId;
	}
}
