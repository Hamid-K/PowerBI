using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000041 RID: 65
	public sealed class FilterQueryNode : CollectionQueryNode
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000095DF File Offset: 0x000077DF
		// (set) Token: 0x06000191 RID: 401 RVA: 0x000095E7 File Offset: 0x000077E7
		public CollectionQueryNode Collection { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000095F0 File Offset: 0x000077F0
		// (set) Token: 0x06000193 RID: 403 RVA: 0x000095F8 File Offset: 0x000077F8
		public SingleValueQueryNode Expression { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00009601 File Offset: 0x00007801
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00009609 File Offset: 0x00007809
		public ParameterQueryNode Parameter { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00009612 File Offset: 0x00007812
		public override IEdmTypeReference ItemType
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00009629 File Offset: 0x00007829
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.Filter;
			}
		}
	}
}
