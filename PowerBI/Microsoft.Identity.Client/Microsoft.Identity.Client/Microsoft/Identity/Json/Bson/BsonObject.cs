using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Identity.Json.Bson
{
	// Token: 0x02000109 RID: 265
	internal class BsonObject : BsonToken, IEnumerable<BsonProperty>, IEnumerable
	{
		// Token: 0x06000D89 RID: 3465 RVA: 0x000366A8 File Offset: 0x000348A8
		public void Add(string name, BsonToken token)
		{
			this._children.Add(new BsonProperty
			{
				Name = new BsonString(name, false),
				Value = token
			});
			token.Parent = this;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x000366D5 File Offset: 0x000348D5
		public override BsonType Type
		{
			get
			{
				return BsonType.Object;
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x000366D8 File Offset: 0x000348D8
		public IEnumerator<BsonProperty> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000366EA File Offset: 0x000348EA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400041E RID: 1054
		private readonly List<BsonProperty> _children = new List<BsonProperty>();
	}
}
