using System;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001DB RID: 475
	public sealed class AssociationSetEnd : EdmItem
	{
		// Token: 0x060016B5 RID: 5813 RVA: 0x0003EA24 File Offset: 0x0003CC24
		internal AssociationSetEnd(AssociationSetEnd assocSetEnd, EntitySet entitySet, AssociationSet parentAssocSet, AssociationEndMember assocEndMember)
		{
			this._assocSetEnd = ArgumentValidation.CheckNotNull<AssociationSetEnd>(assocSetEnd, "assocSetEnd");
			this._entitySet = ArgumentValidation.CheckNotNull<EntitySet>(entitySet, "entitySet");
			this._parentAssocSet = ArgumentValidation.CheckNotNull<AssociationSet>(parentAssocSet, "parentAssocSet");
			this._assocEndMember = ArgumentValidation.CheckNotNull<AssociationEndMember>(assocEndMember, "assocEndMember");
			ArgumentValidation.CheckCondition(entitySet.InternalEntitySet == assocSetEnd.EntitySet, "entitySet");
			ArgumentValidation.CheckCondition(parentAssocSet.InternalAssociationSet == assocSetEnd.ParentAssociationSet, "parentAssocSet");
			ArgumentValidation.CheckCondition(assocEndMember.InternalAssociationEndMember == assocSetEnd.CorrespondingAssociationEndMember, "assocEndMember");
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0003EAC5 File Offset: 0x0003CCC5
		public string Name
		{
			get
			{
				return this._assocSetEnd.Name;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0003EAD2 File Offset: 0x0003CCD2
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0003EADA File Offset: 0x0003CCDA
		public AssociationSet ParentAssociationSet
		{
			get
			{
				return this._parentAssocSet;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0003EAE2 File Offset: 0x0003CCE2
		public AssociationEndMember CorrespondingAssociationEndMember
		{
			get
			{
				return this._assocEndMember;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0003EAEA File Offset: 0x0003CCEA
		internal AssociationSetEnd InternalAssociationSetEnd
		{
			get
			{
				return this._assocSetEnd;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x0003EAF2 File Offset: 0x0003CCF2
		internal override MetadataItem InternalEdmItem
		{
			get
			{
				return this._assocSetEnd;
			}
		}

		// Token: 0x04000C2F RID: 3119
		private readonly AssociationSetEnd _assocSetEnd;

		// Token: 0x04000C30 RID: 3120
		private readonly EntitySet _entitySet;

		// Token: 0x04000C31 RID: 3121
		private readonly AssociationSet _parentAssocSet;

		// Token: 0x04000C32 RID: 3122
		private readonly AssociationEndMember _assocEndMember;
	}
}
