using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000043 RID: 67
	public sealed class KeyLookupQueryNode : SingleValueQueryNode
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000967C File Offset: 0x0000787C
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00009684 File Offset: 0x00007884
		public CollectionQueryNode Collection { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000968D File Offset: 0x0000788D
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00009695 File Offset: 0x00007895
		public IEnumerable<KeyPropertyValue> KeyPropertyValues { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000969E File Offset: 0x0000789E
		public override IEdmTypeReference TypeReference
		{
			get
			{
				if (this.Collection == null)
				{
					return null;
				}
				return this.Collection.ItemType;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000096B5 File Offset: 0x000078B5
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.KeyLookup;
			}
		}
	}
}
