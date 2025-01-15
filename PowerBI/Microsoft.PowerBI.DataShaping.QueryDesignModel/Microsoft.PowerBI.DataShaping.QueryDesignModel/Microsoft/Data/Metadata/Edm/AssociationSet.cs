using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000074 RID: 116
	public sealed class AssociationSet : RelationshipSet
	{
		// Token: 0x0600092E RID: 2350 RVA: 0x00014E39 File Offset: 0x00013039
		internal AssociationSet(string name, AssociationType associationType)
			: base(name, null, null, null, associationType)
		{
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00014E56 File Offset: 0x00013056
		public new AssociationType ElementType
		{
			get
			{
				return (AssociationType)base.ElementType;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00014E63 File Offset: 0x00013063
		[MetadataProperty(BuiltInTypeKind.AssociationSetEnd, true)]
		public ReadOnlyMetadataCollection<AssociationSetEnd> AssociationSetEnds
		{
			get
			{
				return this._associationSetEnds;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00014E6B File Offset: 0x0001306B
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationSet;
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00014E6E File Offset: 0x0001306E
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.AssociationSetEnds.Source.SetReadOnly();
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00014E8F File Offset: 0x0001308F
		internal void AddAssociationSetEnd(AssociationSetEnd associationSetEnd)
		{
			this.AssociationSetEnds.Source.Add(associationSetEnd);
		}

		// Token: 0x04000731 RID: 1841
		private readonly ReadOnlyMetadataCollection<AssociationSetEnd> _associationSetEnds = new ReadOnlyMetadataCollection<AssociationSetEnd>(new MetadataCollection<AssociationSetEnd>());
	}
}
