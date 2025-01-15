using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000098 RID: 152
	internal sealed class MemberCollection : MetadataCollection<EdmMember>
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x000195AE File Offset: 0x000177AE
		public MemberCollection(StructuralType declaringType)
			: this(declaringType, null)
		{
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000195B8 File Offset: 0x000177B8
		public MemberCollection(StructuralType declaringType, IEnumerable<EdmMember> items)
			: base(items)
		{
			this._declaringType = declaringType;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x000195C8 File Offset: 0x000177C8
		public override ReadOnlyCollection<EdmMember> AsReadOnly
		{
			get
			{
				return new ReadOnlyCollection<EdmMember>(this);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000195D0 File Offset: 0x000177D0
		public override int Count
		{
			get
			{
				return this.GetBaseTypeMemberCount() + base.Count;
			}
		}

		// Token: 0x170003DB RID: 987
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
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x170003DC RID: 988
		public override EdmMember this[string identity]
		{
			get
			{
				return this.GetValue(identity, false);
			}
			set
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00019634 File Offset: 0x00017834
		public override void Add(EdmMember member)
		{
			this.ValidateMemberForAdd(member, "member");
			base.Add(member);
			member.ChangeDeclaringTypeWithoutCollectionFixup(this._declaringType);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00019658 File Offset: 0x00017858
		public override bool ContainsIdentity(string identity)
		{
			if (base.ContainsIdentity(identity))
			{
				return true;
			}
			EdmType baseType = this._declaringType.BaseType;
			return baseType != null && ((StructuralType)baseType).Members.Contains(identity);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00019698 File Offset: 0x00017898
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

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000196DC File Offset: 0x000178DC
		public override void CopyTo(EdmMember[] array, int arrayIndex)
		{
			if (arrayIndex < 0)
			{
				throw EntityUtil.ArgumentOutOfRange("arrayIndex");
			}
			int baseTypeMemberCount = this.GetBaseTypeMemberCount();
			if (base.Count + baseTypeMemberCount > array.Length - arrayIndex)
			{
				throw EntityUtil.Argument("arrayIndex");
			}
			if (baseTypeMemberCount > 0)
			{
				((StructuralType)this._declaringType.BaseType).Members.CopyTo(array, arrayIndex);
			}
			base.CopyTo(array, arrayIndex + baseTypeMemberCount);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00019744 File Offset: 0x00017944
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

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00019784 File Offset: 0x00017984
		public override EdmMember GetValue(string identity, bool ignoreCase)
		{
			EdmMember edmMember = null;
			if (!this.TryGetValue(identity, ignoreCase, out edmMember))
			{
				throw EntityUtil.ItemInvalidIdentity(identity, "identity");
			}
			return edmMember;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000197AC File Offset: 0x000179AC
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
			return metadataCollection.AsReadOnlyMetadataCollection();
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000197F8 File Offset: 0x000179F8
		private int GetBaseTypeMemberCount()
		{
			StructuralType structuralType = this._declaringType.BaseType as StructuralType;
			if (structuralType != null)
			{
				return structuralType.Members.Count;
			}
			return 0;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00019828 File Offset: 0x00017A28
		private int GetRelativeIndex(int index)
		{
			int baseTypeMemberCount = this.GetBaseTypeMemberCount();
			int count = base.Count;
			if (index < 0 || index >= baseTypeMemberCount + count)
			{
				throw EntityUtil.ArgumentOutOfRange("index");
			}
			return index - baseTypeMemberCount;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001985B File Offset: 0x00017A5B
		private void ValidateMemberForAdd(EdmMember member, string argumentName)
		{
			EntityUtil.GenericCheckArgumentNull<EdmMember>(member, argumentName);
			this._declaringType.ValidateMemberForAdd(member);
		}

		// Token: 0x04000854 RID: 2132
		private StructuralType _declaringType;
	}
}
