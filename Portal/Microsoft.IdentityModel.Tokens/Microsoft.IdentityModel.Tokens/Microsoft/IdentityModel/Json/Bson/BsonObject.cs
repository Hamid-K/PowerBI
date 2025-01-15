using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x0200010A RID: 266
	internal class BsonObject : BsonToken, IEnumerable<BsonProperty>, IEnumerable
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x00036EA4 File Offset: 0x000350A4
		public void Add(string name, BsonToken token)
		{
			this._children.Add(new BsonProperty
			{
				Name = new BsonString(name, false),
				Value = token
			});
			token.Parent = this;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00036ED1 File Offset: 0x000350D1
		public override BsonType Type
		{
			get
			{
				return BsonType.Object;
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00036ED4 File Offset: 0x000350D4
		public IEnumerator<BsonProperty> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00036EE6 File Offset: 0x000350E6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400043B RID: 1083
		private readonly List<BsonProperty> _children = new List<BsonProperty>();
	}
}
