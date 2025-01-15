using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004FD RID: 1277
	public abstract class StructuralType : EdmType
	{
		// Token: 0x06003F12 RID: 16146 RVA: 0x000D1EC2 File Offset: 0x000D00C2
		internal StructuralType()
		{
			this._members = new MemberCollection(this);
			this._readOnlyMembers = this._members.AsReadOnlyMetadataCollection();
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x000D1EE7 File Offset: 0x000D00E7
		internal StructuralType(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
			this._members = new MemberCollection(this);
			this._readOnlyMembers = this._members.AsReadOnlyMetadataCollection();
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06003F14 RID: 16148 RVA: 0x000D1F0F File Offset: 0x000D010F
		[MetadataProperty(BuiltInTypeKind.EdmMember, true)]
		public ReadOnlyMetadataCollection<EdmMember> Members
		{
			get
			{
				return this._readOnlyMembers;
			}
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x000D1F17 File Offset: 0x000D0117
		internal ReadOnlyMetadataCollection<T> GetDeclaredOnlyMembers<T>() where T : EdmMember
		{
			return this._members.GetDeclaredOnlyMembers<T>();
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x000D1F24 File Offset: 0x000D0124
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.Members.Source.SetReadOnly();
			}
		}

		// Token: 0x06003F17 RID: 16151
		internal abstract void ValidateMemberForAdd(EdmMember member);

		// Token: 0x06003F18 RID: 16152 RVA: 0x000D1F45 File Offset: 0x000D0145
		public void AddMember(EdmMember member)
		{
			this.AddMember(member, false);
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x000D1F50 File Offset: 0x000D0150
		internal void AddMember(EdmMember member, bool forceAdd)
		{
			Check.NotNull<EdmMember>(member, "member");
			if (!forceAdd)
			{
				Util.ThrowIfReadOnly(this);
			}
			if (this.DataSpace != member.TypeUsage.EdmType.DataSpace && this.BuiltInTypeKind != BuiltInTypeKind.RowType)
			{
				throw new ArgumentException(Strings.AttemptToAddEdmMemberFromWrongDataSpace(member.Name, this.Name, member.TypeUsage.EdmType.DataSpace, this.DataSpace), "member");
			}
			if (BuiltInTypeKind.RowType == this.BuiltInTypeKind)
			{
				if (this._members.Count == 0)
				{
					this.DataSpace = member.TypeUsage.EdmType.DataSpace;
				}
				else if (this.DataSpace != (DataSpace)(-1) && member.TypeUsage.EdmType.DataSpace != this.DataSpace)
				{
					this.DataSpace = (DataSpace)(-1);
				}
			}
			if (this._members.IsReadOnly && forceAdd)
			{
				this._members.ResetReadOnly();
				this._members.Add(member);
				this._members.SetReadOnly();
				return;
			}
			this._members.Add(member);
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x000D2066 File Offset: 0x000D0266
		public virtual void RemoveMember(EdmMember member)
		{
			Check.NotNull<EdmMember>(member, "member");
			Util.ThrowIfReadOnly(this);
			this._members.Remove(member);
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x000D2087 File Offset: 0x000D0287
		internal virtual bool HasMember(EdmMember member)
		{
			return this._members.Contains(member);
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x000D2095 File Offset: 0x000D0295
		internal virtual void NotifyItemIdentityChanged(EdmMember item, string initialIdentity)
		{
			this._members.HandleIdentityChange(item, initialIdentity);
		}

		// Token: 0x04001588 RID: 5512
		private readonly MemberCollection _members;

		// Token: 0x04001589 RID: 5513
		private readonly ReadOnlyMetadataCollection<EdmMember> _readOnlyMembers;
	}
}
