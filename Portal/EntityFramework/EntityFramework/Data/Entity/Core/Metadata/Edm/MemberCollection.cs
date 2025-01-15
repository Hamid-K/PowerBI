using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CE RID: 1230
	internal sealed class MemberCollection : MetadataCollection<EdmMember>
	{
		// Token: 0x06003CE9 RID: 15593 RVA: 0x000C9A8C File Offset: 0x000C7C8C
		public MemberCollection(StructuralType declaringType)
			: this(declaringType, null)
		{
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x000C9A96 File Offset: 0x000C7C96
		public MemberCollection(StructuralType declaringType, IEnumerable<EdmMember> items)
			: base(items)
		{
			this._declaringType = declaringType;
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x000C9AA6 File Offset: 0x000C7CA6
		public override ReadOnlyCollection<EdmMember> AsReadOnly
		{
			get
			{
				return new ReadOnlyCollection<EdmMember>(this);
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06003CEC RID: 15596 RVA: 0x000C9AAE File Offset: 0x000C7CAE
		public override int Count
		{
			get
			{
				return this.GetBaseTypeMemberCount() + base.Count;
			}
		}

		// Token: 0x17000C00 RID: 3072
		public override EdmMember this[int index]
		{
			get
			{
				int relativeIndex = this.GetRelativeIndex(index);
				if (relativeIndex < 0)
				{
					return ((StructuralType)this._declaringType.BaseType).Members[index];
				}
				return base[relativeIndex];
			}
			set
			{
				int relativeIndex = this.GetRelativeIndex(index);
				if (relativeIndex < 0)
				{
					((StructuralType)this._declaringType.BaseType).Members.Source[index] = value;
					return;
				}
				base[relativeIndex] = value;
			}
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000C9B3F File Offset: 0x000C7D3F
		public override void Add(EdmMember member)
		{
			this.ValidateMemberForAdd(member, "member");
			base.Add(member);
			member.ChangeDeclaringTypeWithoutCollectionFixup(this._declaringType);
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x000C9B60 File Offset: 0x000C7D60
		public override bool ContainsIdentity(string identity)
		{
			if (base.ContainsIdentity(identity))
			{
				return true;
			}
			EdmType baseType = this._declaringType.BaseType;
			return baseType != null && ((StructuralType)baseType).Members.Contains(identity);
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x000C9BA0 File Offset: 0x000C7DA0
		public override int IndexOf(EdmMember item)
		{
			int num = base.IndexOf(item);
			if (num != -1)
			{
				return num + this.GetBaseTypeMemberCount();
			}
			StructuralType structuralType = this._declaringType.BaseType as StructuralType;
			if (structuralType != null)
			{
				return structuralType.Members.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000C9BE4 File Offset: 0x000C7DE4
		public override void CopyTo(EdmMember[] array, int arrayIndex)
		{
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			int baseTypeMemberCount = this.GetBaseTypeMemberCount();
			if (base.Count + baseTypeMemberCount > array.Length - arrayIndex)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (baseTypeMemberCount > 0)
			{
				((StructuralType)this._declaringType.BaseType).Members.CopyTo(array, arrayIndex);
			}
			base.CopyTo(array, arrayIndex + baseTypeMemberCount);
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000C9C4C File Offset: 0x000C7E4C
		public override bool TryGetValue(string identity, bool ignoreCase, out EdmMember item)
		{
			if (!base.TryGetValue(identity, ignoreCase, out item))
			{
				EdmType baseType = this._declaringType.BaseType;
				if (baseType != null)
				{
					((StructuralType)baseType).Members.TryGetValue(identity, ignoreCase, out item);
				}
			}
			return item != null;
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x000C9C8C File Offset: 0x000C7E8C
		internal ReadOnlyMetadataCollection<T> GetDeclaredOnlyMembers<T>() where T : EdmMember
		{
			MetadataCollection<T> metadataCollection = new MetadataCollection<T>();
			for (int i = 0; i < base.Count; i++)
			{
				T t = base[i] as T;
				if (t != null)
				{
					metadataCollection.Add(t);
				}
			}
			return new ReadOnlyMetadataCollection<T>(metadataCollection);
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000C9CD8 File Offset: 0x000C7ED8
		private int GetBaseTypeMemberCount()
		{
			StructuralType structuralType = this._declaringType.BaseType as StructuralType;
			if (structuralType != null)
			{
				return structuralType.Members.Count;
			}
			return 0;
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000C9D08 File Offset: 0x000C7F08
		private int GetRelativeIndex(int index)
		{
			int baseTypeMemberCount = this.GetBaseTypeMemberCount();
			int count = base.Count;
			if (index < 0 || index >= baseTypeMemberCount + count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return index - baseTypeMemberCount;
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x000C9D3B File Offset: 0x000C7F3B
		private void ValidateMemberForAdd(EdmMember member, string argumentName)
		{
			Check.NotNull<EdmMember>(member, argumentName);
			this._declaringType.ValidateMemberForAdd(member);
		}

		// Token: 0x040014E0 RID: 5344
		private readonly StructuralType _declaringType;
	}
}
