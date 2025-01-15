using System;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001DF RID: 479
	public sealed class AssociationEndMember : EdmMember
	{
		// Token: 0x060016C4 RID: 5828 RVA: 0x0003EB69 File Offset: 0x0003CD69
		internal AssociationEndMember(AssociationEndMember assocEndMember, StructuralType declaringType)
			: base(declaringType, null)
		{
			this._assocEndMember = ArgumentValidation.CheckNotNull<AssociationEndMember>(assocEndMember, "assocEndMember");
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0003EB84 File Offset: 0x0003CD84
		public RelationshipMultiplicity RelationshipMultiplicity
		{
			get
			{
				return (RelationshipMultiplicity)this._assocEndMember.RelationshipMultiplicity;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0003EB91 File Offset: 0x0003CD91
		internal AssociationEndMember InternalAssociationEndMember
		{
			get
			{
				return this._assocEndMember;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0003EB99 File Offset: 0x0003CD99
		internal override EdmMember InternalEdmMember
		{
			get
			{
				return this._assocEndMember;
			}
		}

		// Token: 0x04000C39 RID: 3129
		private readonly AssociationEndMember _assocEndMember;
	}
}
