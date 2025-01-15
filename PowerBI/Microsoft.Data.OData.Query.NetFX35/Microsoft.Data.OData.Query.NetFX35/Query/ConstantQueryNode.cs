using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Experimental.OData.Metadata;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000014 RID: 20
	public sealed class ConstantQueryNode : SingleValueQueryNode
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000035BB File Offset: 0x000017BB
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000035C3 File Offset: 0x000017C3
		public object Value { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000035CC File Offset: 0x000017CC
		public override IEdmTypeReference TypeReference
		{
			get
			{
				if (this.Value == null)
				{
					return null;
				}
				return EdmLibraryExtensions.GetPrimitiveTypeReference(this.Value.GetType());
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000035E8 File Offset: 0x000017E8
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.Constant;
			}
		}
	}
}
