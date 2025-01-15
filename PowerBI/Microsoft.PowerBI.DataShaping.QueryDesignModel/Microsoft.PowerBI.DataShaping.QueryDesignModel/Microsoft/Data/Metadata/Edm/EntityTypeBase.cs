using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200008C RID: 140
	public abstract class EntityTypeBase : StructuralType
	{
		// Token: 0x06000A30 RID: 2608 RVA: 0x000182E3 File Offset: 0x000164E3
		internal EntityTypeBase(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
			this._keyMembers = new ReadOnlyMetadataCollection<EdmMember>(new MetadataCollection<EdmMember>());
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x000182FE File Offset: 0x000164FE
		[MetadataProperty(BuiltInTypeKind.EdmMember, true)]
		public ReadOnlyMetadataCollection<EdmMember> KeyMembers
		{
			get
			{
				if (base.BaseType != null && ((EntityTypeBase)base.BaseType).KeyMembers.Count != 0)
				{
					return ((EntityTypeBase)base.BaseType).KeyMembers;
				}
				return this._keyMembers;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00018338 File Offset: 0x00016538
		internal string[] KeyMemberNames
		{
			get
			{
				if (this._keyMemberNames == null)
				{
					string[] array = new string[this.KeyMembers.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this.KeyMembers[i].Name;
					}
					this._keyMemberNames = array;
				}
				return this._keyMemberNames;
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001838F File Offset: 0x0001658F
		internal void AddKeyMember(EdmMember member)
		{
			EntityUtil.GenericCheckArgumentNull<EdmMember>(member, "member");
			Util.ThrowIfReadOnly(this);
			if (!base.Members.Contains(member))
			{
				base.AddMember(member);
			}
			this._keyMembers.Source.Add(member);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000183C9 File Offset: 0x000165C9
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				this._keyMembers.Source.SetReadOnly();
				base.SetReadOnly();
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000183EC File Offset: 0x000165EC
		internal static void CheckAndAddMembers(IEnumerable<EdmMember> members, EntityType entityType)
		{
			foreach (EdmMember edmMember in members)
			{
				if (edmMember == null)
				{
					throw EntityUtil.CollectionParameterElementIsNull("members");
				}
				entityType.AddMember(edmMember);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00018444 File Offset: 0x00016644
		internal void CheckAndAddKeyMembers(IEnumerable<string> keyMembers)
		{
			foreach (string text in keyMembers)
			{
				if (text == null)
				{
					throw EntityUtil.CollectionParameterElementIsNull("keyMembers");
				}
				EdmMember edmMember;
				if (!base.Members.TryGetValue(text, false, out edmMember))
				{
					throw EntityUtil.Argument(Strings.InvalidKeyMember(text));
				}
				this.AddKeyMember(edmMember);
			}
		}

		// Token: 0x04000830 RID: 2096
		private readonly ReadOnlyMetadataCollection<EdmMember> _keyMembers;

		// Token: 0x04000831 RID: 2097
		private string[] _keyMemberNames;
	}
}
