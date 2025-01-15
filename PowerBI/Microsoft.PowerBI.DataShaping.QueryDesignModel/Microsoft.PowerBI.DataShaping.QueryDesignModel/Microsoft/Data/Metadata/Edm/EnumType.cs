using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200008E RID: 142
	internal sealed class EnumType : SimpleType
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x000184EF File Offset: 0x000166EF
		internal EnumType()
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00018507 File Offset: 0x00016707
		internal EnumType(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00018522 File Offset: 0x00016722
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EnumType;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00018526 File Offset: 0x00016726
		[MetadataProperty(BuiltInTypeKind.EnumMember, true)]
		public ReadOnlyMetadataCollection<EnumMember> EnumMembers
		{
			get
			{
				return this._enumMembers;
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001852E File Offset: 0x0001672E
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.EnumMembers.Source.SetReadOnly();
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001854F File Offset: 0x0001674F
		internal void AddMember(EnumMember enumMember)
		{
			this.EnumMembers.Source.Add(enumMember);
		}

		// Token: 0x04000833 RID: 2099
		private readonly ReadOnlyMetadataCollection<EnumMember> _enumMembers = new ReadOnlyMetadataCollection<EnumMember>(new MetadataCollection<EnumMember>());
	}
}
