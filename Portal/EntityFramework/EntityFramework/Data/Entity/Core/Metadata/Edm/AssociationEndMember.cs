using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Utilities;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000486 RID: 1158
	public sealed class AssociationEndMember : RelationshipEndMember
	{
		// Token: 0x0600397F RID: 14719 RVA: 0x000BD67E File Offset: 0x000BB87E
		internal AssociationEndMember(string name, RefType endRefType, RelationshipMultiplicity multiplicity)
			: base(name, endRefType, multiplicity)
		{
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000BD689 File Offset: 0x000BB889
		internal AssociationEndMember(string name, EntityType entityType)
			: base(name, new RefType(entityType), RelationshipMultiplicity.ZeroOrOne)
		{
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x000BD699 File Offset: 0x000BB899
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationEndMember;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06003982 RID: 14722 RVA: 0x000BD69C File Offset: 0x000BB89C
		// (set) Token: 0x06003983 RID: 14723 RVA: 0x000BD6A4 File Offset: 0x000BB8A4
		internal Func<RelationshipManager, RelatedEnd, RelatedEnd> GetRelatedEnd
		{
			get
			{
				return this._getRelatedEndMethod;
			}
			set
			{
				Interlocked.CompareExchange<Func<RelationshipManager, RelatedEnd, RelatedEnd>>(ref this._getRelatedEndMethod, value, null);
			}
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000BD6B4 File Offset: 0x000BB8B4
		public static AssociationEndMember Create(string name, RefType endRefType, RelationshipMultiplicity multiplicity, OperationAction deleteAction, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<RefType>(endRefType, "endRefType");
			AssociationEndMember associationEndMember = new AssociationEndMember(name, endRefType, multiplicity);
			associationEndMember.DeleteBehavior = deleteAction;
			if (metadataProperties != null)
			{
				associationEndMember.AddMetadataProperties(metadataProperties);
			}
			associationEndMember.SetReadOnly();
			return associationEndMember;
		}

		// Token: 0x0400130E RID: 4878
		private Func<RelationshipManager, RelatedEnd, RelatedEnd> _getRelatedEndMethod;
	}
}
