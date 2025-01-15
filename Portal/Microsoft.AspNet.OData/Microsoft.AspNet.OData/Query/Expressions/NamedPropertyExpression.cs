using System;
using System.Linq.Expressions;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000EF RID: 239
	internal class NamedPropertyExpression
	{
		// Token: 0x0600080C RID: 2060 RVA: 0x0001EB12 File Offset: 0x0001CD12
		public NamedPropertyExpression(Expression name, Expression value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001EB28 File Offset: 0x0001CD28
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x0001EB30 File Offset: 0x0001CD30
		public Expression Name { get; private set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001EB39 File Offset: 0x0001CD39
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x0001EB41 File Offset: 0x0001CD41
		public Expression Value { get; private set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001EB4A File Offset: 0x0001CD4A
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x0001EB52 File Offset: 0x0001CD52
		public Expression TotalCount { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001EB5B File Offset: 0x0001CD5B
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x0001EB63 File Offset: 0x0001CD63
		public Expression NullCheck { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001EB6C File Offset: 0x0001CD6C
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x0001EB74 File Offset: 0x0001CD74
		public int? PageSize { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001EB7D File Offset: 0x0001CD7D
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x0001EB85 File Offset: 0x0001CD85
		public bool AutoSelected { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001EB8E File Offset: 0x0001CD8E
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x0001EB96 File Offset: 0x0001CD96
		public bool? CountOption { get; set; }
	}
}
