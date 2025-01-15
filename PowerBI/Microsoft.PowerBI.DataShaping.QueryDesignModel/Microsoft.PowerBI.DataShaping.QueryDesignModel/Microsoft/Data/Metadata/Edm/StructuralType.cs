using System;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000B2 RID: 178
	public abstract class StructuralType : EdmType
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x0001D734 File Offset: 0x0001B934
		internal StructuralType()
		{
			this._members = new MemberCollection(this);
			this._readOnlyMembers = this._members.AsReadOnlyMetadataCollection();
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0001D759 File Offset: 0x0001B959
		internal StructuralType(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
			this._members = new MemberCollection(this);
			this._readOnlyMembers = this._members.AsReadOnlyMetadataCollection();
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0001D781 File Offset: 0x0001B981
		[MetadataProperty(BuiltInTypeKind.EdmMember, true)]
		public ReadOnlyMetadataCollection<EdmMember> Members
		{
			get
			{
				return this._readOnlyMembers;
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0001D789 File Offset: 0x0001B989
		internal ReadOnlyMetadataCollection<T> GetDeclaredOnlyMembers<T>() where T : EdmMember
		{
			return this._members.GetDeclaredOnlyMembers<T>();
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0001D796 File Offset: 0x0001B996
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.Members.Source.SetReadOnly();
			}
		}

		// Token: 0x06000B88 RID: 2952
		internal abstract void ValidateMemberForAdd(EdmMember member);

		// Token: 0x06000B89 RID: 2953 RVA: 0x0001D7B8 File Offset: 0x0001B9B8
		internal void AddMember(EdmMember member)
		{
			EntityUtil.GenericCheckArgumentNull<EdmMember>(member, "member");
			Util.ThrowIfReadOnly(this);
			if (BuiltInTypeKind.RowType == this.BuiltInTypeKind)
			{
				if (this._members.Count == 0)
				{
					base.DataSpace = member.TypeUsage.EdmType.DataSpace;
				}
				else if (base.DataSpace != (DataSpace)(-1) && member.TypeUsage.EdmType.DataSpace != base.DataSpace)
				{
					base.DataSpace = (DataSpace)(-1);
				}
			}
			this._members.Add(member);
		}

		// Token: 0x040008B0 RID: 2224
		private readonly MemberCollection _members;

		// Token: 0x040008B1 RID: 2225
		private readonly ReadOnlyMetadataCollection<EdmMember> _readOnlyMembers;
	}
}
