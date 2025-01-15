using System;
using System.Linq;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001DD RID: 477
	public sealed class AssociationType : StructuralType
	{
		// Token: 0x060016BE RID: 5822 RVA: 0x0003EB0B File Offset: 0x0003CD0B
		private AssociationType(AssociationType associationType)
		{
			this._assocType = ArgumentValidation.CheckNotNull<AssociationType>(associationType, "associationType");
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x0003EB24 File Offset: 0x0003CD24
		public EdmMemberCollection<AssociationEndMember> AssociationEndMembers
		{
			get
			{
				return this._assocEndMembers;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0003EB2C File Offset: 0x0003CD2C
		internal AssociationType InternalAssociationType
		{
			get
			{
				return this._assocType;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0003EB34 File Offset: 0x0003CD34
		internal override StructuralType InternalStructuralType
		{
			get
			{
				return this._assocType;
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0003EB3C File Offset: 0x0003CD3C
		internal override void InternalInit(Version version)
		{
			base.InternalInit(version);
			this._assocEndMembers = new EdmMemberCollection<AssociationEndMember>(base.Members.OfType<AssociationEndMember>());
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0003EB5B File Offset: 0x0003CD5B
		internal static AssociationType Create(AssociationType associationType)
		{
			AssociationType associationType2 = new AssociationType(associationType);
			associationType2.InternalInit();
			return associationType2;
		}

		// Token: 0x04000C33 RID: 3123
		private readonly AssociationType _assocType;

		// Token: 0x04000C34 RID: 3124
		private EdmMemberCollection<AssociationEndMember> _assocEndMembers;
	}
}
