using System;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002A2 RID: 674
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal sealed class AdalUserInfo
	{
		// Token: 0x0600196C RID: 6508 RVA: 0x0005339E File Offset: 0x0005159E
		public AdalUserInfo()
		{
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000533A8 File Offset: 0x000515A8
		public AdalUserInfo(AdalUserInfo other)
		{
			if (other != null)
			{
				this.UniqueId = other.UniqueId;
				this.DisplayableId = other.DisplayableId;
				this.GivenName = other.GivenName;
				this.FamilyName = other.FamilyName;
				this.IdentityProvider = other.IdentityProvider;
				this.PasswordChangeUrl = other.PasswordChangeUrl;
				this.PasswordExpiresOn = other.PasswordExpiresOn;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00053412 File Offset: 0x00051612
		// (set) Token: 0x0600196F RID: 6511 RVA: 0x0005341A File Offset: 0x0005161A
		[JsonProperty]
		public string UniqueId { get; internal set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00053423 File Offset: 0x00051623
		// (set) Token: 0x06001971 RID: 6513 RVA: 0x0005342B File Offset: 0x0005162B
		[JsonProperty]
		public string DisplayableId { get; internal set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00053434 File Offset: 0x00051634
		// (set) Token: 0x06001973 RID: 6515 RVA: 0x0005343C File Offset: 0x0005163C
		[JsonProperty]
		public string GivenName { get; internal set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x00053445 File Offset: 0x00051645
		// (set) Token: 0x06001975 RID: 6517 RVA: 0x0005344D File Offset: 0x0005164D
		[JsonProperty]
		public string FamilyName { get; internal set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x00053456 File Offset: 0x00051656
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x0005345E File Offset: 0x0005165E
		[JsonProperty]
		public DateTimeOffset? PasswordExpiresOn { get; internal set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x00053467 File Offset: 0x00051667
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x0005346F File Offset: 0x0005166F
		[JsonProperty]
		public Uri PasswordChangeUrl { get; internal set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x00053478 File Offset: 0x00051678
		// (set) Token: 0x0600197B RID: 6523 RVA: 0x00053480 File Offset: 0x00051680
		[JsonProperty]
		public string IdentityProvider { get; internal set; }
	}
}
