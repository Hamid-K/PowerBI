using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001FC RID: 508
	internal class CsdlNavigationProperty : CsdlNamedElement
	{
		// Token: 0x06000DCD RID: 3533 RVA: 0x00026370 File Offset: 0x00024570
		public CsdlNavigationProperty(string name, string type, bool? nullable, string partner, bool containsTarget, CsdlOnDelete onDelete, IEnumerable<CsdlReferentialConstraint> referentialConstraints, CsdlLocation location)
			: base(name, location)
		{
			this.type = type;
			this.nullable = nullable;
			this.partnerPath = ((partner == null) ? null : new EdmPathExpression(partner));
			this.containsTarget = containsTarget;
			this.onDelete = onDelete;
			this.referentialConstraints = referentialConstraints;
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x000263C0 File Offset: 0x000245C0
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x000263C8 File Offset: 0x000245C8
		public bool? Nullable
		{
			get
			{
				return this.nullable;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x000263D0 File Offset: 0x000245D0
		public IEdmPathExpression PartnerPath
		{
			get
			{
				return this.partnerPath;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x000263D8 File Offset: 0x000245D8
		public bool ContainsTarget
		{
			get
			{
				return this.containsTarget;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x000263E0 File Offset: 0x000245E0
		public CsdlOnDelete OnDelete
		{
			get
			{
				return this.onDelete;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x000263E8 File Offset: 0x000245E8
		public IEnumerable<CsdlReferentialConstraint> ReferentialConstraints
		{
			get
			{
				return this.referentialConstraints;
			}
		}

		// Token: 0x04000794 RID: 1940
		private readonly string type;

		// Token: 0x04000795 RID: 1941
		private readonly bool? nullable;

		// Token: 0x04000796 RID: 1942
		private readonly IEdmPathExpression partnerPath;

		// Token: 0x04000797 RID: 1943
		private readonly bool containsTarget;

		// Token: 0x04000798 RID: 1944
		private readonly CsdlOnDelete onDelete;

		// Token: 0x04000799 RID: 1945
		private readonly IEnumerable<CsdlReferentialConstraint> referentialConstraints;
	}
}
