using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000015 RID: 21
	public sealed class ConvertQueryNode : SingleValueQueryNode
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000035F3 File Offset: 0x000017F3
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000035FB File Offset: 0x000017FB
		public SingleValueQueryNode Source { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003604 File Offset: 0x00001804
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.TargetType;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000360C File Offset: 0x0000180C
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003614 File Offset: 0x00001814
		public IEdmTypeReference TargetType { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000361D File Offset: 0x0000181D
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.Convert;
			}
		}
	}
}
