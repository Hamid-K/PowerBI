using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005A9 RID: 1449
	internal sealed class MemberProjectionIndex : InternalBase
	{
		// Token: 0x06004669 RID: 18025 RVA: 0x000F8B40 File Offset: 0x000F6D40
		internal static MemberProjectionIndex Create(EntitySetBase extent, EdmItemCollection edmItemCollection)
		{
			MemberProjectionIndex memberProjectionIndex = new MemberProjectionIndex();
			MemberProjectionIndex.GatherPartialSignature(memberProjectionIndex, edmItemCollection, new MemberPath(extent), false);
			return memberProjectionIndex;
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x000F8B55 File Offset: 0x000F6D55
		private MemberProjectionIndex()
		{
			this.m_indexMap = new Dictionary<MemberPath, int>(MemberPath.EqualityComparer);
			this.m_members = new List<MemberPath>();
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x0600466B RID: 18027 RVA: 0x000F8B78 File Offset: 0x000F6D78
		internal int Count
		{
			get
			{
				return this.m_members.Count;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		internal MemberPath this[int index]
		{
			get
			{
				return this.m_members[index];
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x0600466D RID: 18029 RVA: 0x000F8B94 File Offset: 0x000F6D94
		internal IEnumerable<int> KeySlots
		{
			get
			{
				List<int> list = new List<int>();
				for (int i = 0; i < this.Count; i++)
				{
					if (this.IsKeySlot(i, 0))
					{
						list.Add(i);
					}
				}
				return list;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x0600466E RID: 18030 RVA: 0x000F8BCA File Offset: 0x000F6DCA
		internal IEnumerable<MemberPath> Members
		{
			get
			{
				return this.m_members;
			}
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x000F8BD4 File Offset: 0x000F6DD4
		internal int IndexOf(MemberPath member)
		{
			int num;
			if (this.m_indexMap.TryGetValue(member, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x000F8BF4 File Offset: 0x000F6DF4
		internal int CreateIndex(MemberPath member)
		{
			int count;
			if (!this.m_indexMap.TryGetValue(member, out count))
			{
				count = this.m_indexMap.Count;
				this.m_indexMap[member] = count;
				this.m_members.Add(member);
			}
			return count;
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x000F8C37 File Offset: 0x000F6E37
		internal MemberPath GetMemberPath(int slotNum, int numBoolSlots)
		{
			if (!this.IsBoolSlot(slotNum, numBoolSlots))
			{
				return this[slotNum];
			}
			return null;
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x000F8C4C File Offset: 0x000F6E4C
		internal int BoolIndexToSlot(int boolIndex, int numBoolSlots)
		{
			return this.Count + boolIndex;
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x000F8C56 File Offset: 0x000F6E56
		internal int SlotToBoolIndex(int slotNum, int numBoolSlots)
		{
			return slotNum - this.Count;
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x000F8C60 File Offset: 0x000F6E60
		internal bool IsKeySlot(int slotNum, int numBoolSlots)
		{
			return slotNum < this.Count && this[slotNum].IsPartOfKey;
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x000F8C79 File Offset: 0x000F6E79
		internal bool IsBoolSlot(int slotNum, int numBoolSlots)
		{
			return slotNum >= this.Count;
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x000F8C87 File Offset: 0x000F6E87
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append('<');
			StringUtil.ToCommaSeparatedString(builder, this.m_members);
			builder.Append('>');
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x000F8CA8 File Offset: 0x000F6EA8
		private static void GatherPartialSignature(MemberProjectionIndex index, EdmItemCollection edmItemCollection, MemberPath member, bool needKeysOnly)
		{
			EdmType edmType = member.EdmType;
			if (edmType is ComplexType && needKeysOnly)
			{
				return;
			}
			index.CreateIndex(member);
			foreach (EdmType edmType2 in MetadataHelper.GetTypeAndSubtypesOf(edmType, edmItemCollection, false))
			{
				StructuralType structuralType = edmType2 as StructuralType;
				MemberProjectionIndex.GatherSignatureFromTypeStructuralMembers(index, edmItemCollection, member, structuralType, needKeysOnly);
			}
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x000F8D1C File Offset: 0x000F6F1C
		private static void GatherSignatureFromTypeStructuralMembers(MemberProjectionIndex index, EdmItemCollection edmItemCollection, MemberPath member, StructuralType possibleType, bool needKeysOnly)
		{
			foreach (object obj in Helper.GetAllStructuralMembers(possibleType))
			{
				EdmMember edmMember = (EdmMember)obj;
				if (MetadataHelper.IsNonRefSimpleMember(edmMember))
				{
					if (!needKeysOnly || MetadataHelper.IsPartOfEntityTypeKey(edmMember))
					{
						MemberPath memberPath = new MemberPath(member, edmMember);
						index.CreateIndex(memberPath);
					}
				}
				else
				{
					MemberPath memberPath2 = new MemberPath(member, edmMember);
					MemberProjectionIndex.GatherPartialSignature(index, edmItemCollection, memberPath2, needKeysOnly || Helper.IsAssociationEndMember(edmMember));
				}
			}
		}

		// Token: 0x0400191B RID: 6427
		private readonly Dictionary<MemberPath, int> m_indexMap;

		// Token: 0x0400191C RID: 6428
		private readonly List<MemberPath> m_members;
	}
}
