using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000038 RID: 56
	public sealed class OrderByQueryNode : CollectionQueryNode
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00008794 File Offset: 0x00006994
		// (set) Token: 0x0600014A RID: 330 RVA: 0x0000879C File Offset: 0x0000699C
		public CollectionQueryNode Collection { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000087A5 File Offset: 0x000069A5
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000087AD File Offset: 0x000069AD
		public SingleValueQueryNode Expression { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000087B6 File Offset: 0x000069B6
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000087BE File Offset: 0x000069BE
		public OrderByDirection Direction { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000087C7 File Offset: 0x000069C7
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000087CF File Offset: 0x000069CF
		public ParameterQueryNode Parameter { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000087D8 File Offset: 0x000069D8
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000087EF File Offset: 0x000069EF
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.OrderBy;
			}
		}
	}
}
