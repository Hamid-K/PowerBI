using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Identity.Json.Bson
{
	// Token: 0x0200010A RID: 266
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x06000D8E RID: 3470 RVA: 0x00036705 File Offset: 0x00034905
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0003671A File Offset: 0x0003491A
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0003671D File Offset: 0x0003491D
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003672F File Offset: 0x0003492F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400041F RID: 1055
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
