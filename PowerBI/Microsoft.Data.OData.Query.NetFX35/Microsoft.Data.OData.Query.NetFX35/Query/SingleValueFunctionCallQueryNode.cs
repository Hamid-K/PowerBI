using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000031 RID: 49
	public sealed class SingleValueFunctionCallQueryNode : SingleValueQueryNode
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005F4D File Offset: 0x0000414D
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00005F55 File Offset: 0x00004155
		public string Name { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005F5E File Offset: 0x0000415E
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00005F66 File Offset: 0x00004166
		public IEnumerable<QueryNode> Arguments { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005F6F File Offset: 0x0000416F
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00005F77 File Offset: 0x00004177
		public IEdmTypeReference ReturnType { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005F80 File Offset: 0x00004180
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.ReturnType;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005F88 File Offset: 0x00004188
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.SingleValueFunctionCall;
			}
		}
	}
}
