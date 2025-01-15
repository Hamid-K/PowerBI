using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000186 RID: 390
	internal class CsdlNavigationProperty : CsdlNamedElement
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x000119AA File Offset: 0x0000FBAA
		public CsdlNavigationProperty(string name, string type, bool? nullable, string partner, bool containsTarget, CsdlOnDelete onDelete, IEnumerable<CsdlReferentialConstraint> referentialConstraints, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.type = type;
			this.nullable = nullable;
			this.partner = partner;
			this.containsTarget = containsTarget;
			this.onDelete = onDelete;
			this.referentialConstraints = referentialConstraints;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x000119E5 File Offset: 0x0000FBE5
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x000119ED File Offset: 0x0000FBED
		public bool? Nullable
		{
			get
			{
				return this.nullable;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x000119F5 File Offset: 0x0000FBF5
		public string Partner
		{
			get
			{
				return this.partner;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x000119FD File Offset: 0x0000FBFD
		public bool ContainsTarget
		{
			get
			{
				return this.containsTarget;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00011A05 File Offset: 0x0000FC05
		public CsdlOnDelete OnDelete
		{
			get
			{
				return this.onDelete;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00011A0D File Offset: 0x0000FC0D
		public IEnumerable<CsdlReferentialConstraint> ReferentialConstraints
		{
			get
			{
				return this.referentialConstraints;
			}
		}

		// Token: 0x040003CA RID: 970
		private readonly string type;

		// Token: 0x040003CB RID: 971
		private readonly bool? nullable;

		// Token: 0x040003CC RID: 972
		private readonly string partner;

		// Token: 0x040003CD RID: 973
		private readonly bool containsTarget;

		// Token: 0x040003CE RID: 974
		private readonly CsdlOnDelete onDelete;

		// Token: 0x040003CF RID: 975
		private readonly IEnumerable<CsdlReferentialConstraint> referentialConstraints;
	}
}
