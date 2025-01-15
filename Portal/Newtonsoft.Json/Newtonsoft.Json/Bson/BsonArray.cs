﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010B RID: 267
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x06000DA8 RID: 3496 RVA: 0x00037059 File Offset: 0x00035259
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0003706E File Offset: 0x0003526E
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00037071 File Offset: 0x00035271
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00037083 File Offset: 0x00035283
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400043D RID: 1085
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
