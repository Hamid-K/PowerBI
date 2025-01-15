using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001EF RID: 495
	internal class CsdlNavigationProperty : CsdlNamedElement
	{
		// Token: 0x06000D1E RID: 3358 RVA: 0x00024230 File Offset: 0x00022430
		public CsdlNavigationProperty(string name, string type, bool? nullable, string partner, bool containsTarget, CsdlOnDelete onDelete, IEnumerable<CsdlReferentialConstraint> referentialConstraints, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.type = type;
			this.nullable = nullable;
			this.partnerPath = ((partner == null) ? null : new EdmPathExpression(partner));
			this.containsTarget = containsTarget;
			this.onDelete = onDelete;
			this.referentialConstraints = referentialConstraints;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00024282 File Offset: 0x00022482
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0002428A File Offset: 0x0002248A
		public bool? Nullable
		{
			get
			{
				return this.nullable;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x00024292 File Offset: 0x00022492
		public IEdmPathExpression PartnerPath
		{
			get
			{
				return this.partnerPath;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0002429A File Offset: 0x0002249A
		public bool ContainsTarget
		{
			get
			{
				return this.containsTarget;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x000242A2 File Offset: 0x000224A2
		public CsdlOnDelete OnDelete
		{
			get
			{
				return this.onDelete;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x000242AA File Offset: 0x000224AA
		public IEnumerable<CsdlReferentialConstraint> ReferentialConstraints
		{
			get
			{
				return this.referentialConstraints;
			}
		}

		// Token: 0x0400071E RID: 1822
		private readonly string type;

		// Token: 0x0400071F RID: 1823
		private readonly bool? nullable;

		// Token: 0x04000720 RID: 1824
		private readonly IEdmPathExpression partnerPath;

		// Token: 0x04000721 RID: 1825
		private readonly bool containsTarget;

		// Token: 0x04000722 RID: 1826
		private readonly CsdlOnDelete onDelete;

		// Token: 0x04000723 RID: 1827
		private readonly IEnumerable<CsdlReferentialConstraint> referentialConstraints;
	}
}
