using System;
using System.Diagnostics;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002A0 RID: 672
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal sealed class AdalTokenCacheKey : IEquatable<AdalTokenCacheKey>
	{
		// Token: 0x06001959 RID: 6489 RVA: 0x00053116 File Offset: 0x00051316
		internal AdalTokenCacheKey(string authority, string resource, string clientId, TokenSubjectType tokenSubjectType, AdalUserInfo adalUserInfo)
			: this(authority, resource, clientId, tokenSubjectType, (adalUserInfo != null) ? adalUserInfo.UniqueId : null, (adalUserInfo != null) ? adalUserInfo.DisplayableId : null)
		{
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0005313F File Offset: 0x0005133F
		internal AdalTokenCacheKey(string authority, string resource, string clientId, TokenSubjectType tokenSubjectType, string uniqueId, string displayableId)
		{
			this.Authority = authority;
			this.Resource = resource;
			this.ClientId = clientId;
			this.TokenSubjectType = tokenSubjectType;
			this.UniqueId = uniqueId;
			this.DisplayableId = displayableId;
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x00053174 File Offset: 0x00051374
		public string Authority { get; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x0005317C File Offset: 0x0005137C
		public string Resource { get; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00053184 File Offset: 0x00051384
		public string ClientId { get; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x0005318C File Offset: 0x0005138C
		public string UniqueId { get; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00053194 File Offset: 0x00051394
		public string DisplayableId { get; }

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x0005319C File Offset: 0x0005139C
		public TokenSubjectType TokenSubjectType { get; }

		// Token: 0x06001961 RID: 6497 RVA: 0x000531A4 File Offset: 0x000513A4
		public override bool Equals(object obj)
		{
			AdalTokenCacheKey adalTokenCacheKey = obj as AdalTokenCacheKey;
			return adalTokenCacheKey != null && this.Equals(adalTokenCacheKey);
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000531C4 File Offset: 0x000513C4
		public bool Equals(AdalTokenCacheKey other)
		{
			return this == other || (other != null && other.Authority == this.Authority && this.ClientIdEquals(other.ClientId) && other.UniqueId == this.UniqueId && this.DisplayableIdEquals(other.DisplayableId) && other.TokenSubjectType == this.TokenSubjectType);
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0005322C File Offset: 0x0005142C
		public override int GetHashCode()
		{
			return string.Concat(new string[]
			{
				this.Authority,
				":::",
				this.ClientId,
				":::",
				this.UniqueId,
				":::",
				this.DisplayableId,
				":::",
				((int)this.TokenSubjectType).ToString()
			}).GetHashCode();
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0005329F File Offset: 0x0005149F
		private bool ClientIdEquals(string otherClientId)
		{
			return string.Equals(otherClientId, this.ClientId, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000532AE File Offset: 0x000514AE
		private bool DisplayableIdEquals(string otherDisplayableId)
		{
			return string.Equals(otherDisplayableId, this.DisplayableId, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x000532C0 File Offset: 0x000514C0
		private string DebuggerDisplay
		{
			get
			{
				return string.Concat(new string[] { "AdalTokenCacheKey: ", this.Authority, " ", this.Resource, " ", this.ClientId, " ", this.UniqueId, " ", this.DisplayableId });
			}
		}
	}
}
