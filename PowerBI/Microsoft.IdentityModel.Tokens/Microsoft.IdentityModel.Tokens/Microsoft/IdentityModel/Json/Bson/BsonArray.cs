using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x0200010B RID: 267
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x06000D9E RID: 3486 RVA: 0x00036F01 File Offset: 0x00035101
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00036F16 File Offset: 0x00035116
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00036F19 File Offset: 0x00035119
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00036F2B File Offset: 0x0003512B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400043C RID: 1084
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
