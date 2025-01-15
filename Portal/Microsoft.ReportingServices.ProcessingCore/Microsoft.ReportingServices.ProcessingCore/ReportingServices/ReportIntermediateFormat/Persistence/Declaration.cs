using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000551 RID: 1361
	public class Declaration
	{
		// Token: 0x060049D1 RID: 18897 RVA: 0x001378B8 File Offset: 0x00135AB8
		internal Declaration(ObjectType type, ObjectType baseType, List<MemberInfo> memberInfoList)
		{
			this.m_type = type;
			this.m_baseType = baseType;
			this.m_memberInfoList = memberInfoList;
		}

		// Token: 0x17001DE8 RID: 7656
		// (get) Token: 0x060049D2 RID: 18898 RVA: 0x001378E0 File Offset: 0x00135AE0
		internal List<MemberInfo> MemberInfoList
		{
			get
			{
				return this.m_memberInfoList;
			}
		}

		// Token: 0x17001DE9 RID: 7657
		// (get) Token: 0x060049D3 RID: 18899 RVA: 0x001378E8 File Offset: 0x00135AE8
		internal ObjectType ObjectType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17001DEA RID: 7658
		// (get) Token: 0x060049D4 RID: 18900 RVA: 0x001378F0 File Offset: 0x00135AF0
		internal ObjectType BaseObjectType
		{
			get
			{
				return this.m_baseType;
			}
		}

		// Token: 0x17001DEB RID: 7659
		// (get) Token: 0x060049D5 RID: 18901 RVA: 0x001378F8 File Offset: 0x00135AF8
		internal bool RegisteredCurrentDeclaration
		{
			get
			{
				return this.m_usableMembers != null;
			}
		}

		// Token: 0x060049D6 RID: 18902 RVA: 0x00137903 File Offset: 0x00135B03
		internal bool IsMemberSkipped(int index)
		{
			return this.m_hasSkippedMembers && this.m_usableMembers[index].First;
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x00137920 File Offset: 0x00135B20
		internal int MembersToSkip(int index)
		{
			if (this.m_hasSkippedMembers)
			{
				return this.m_usableMembers[index].Second;
			}
			return 0;
		}

		// Token: 0x17001DEC RID: 7660
		// (get) Token: 0x060049D8 RID: 18904 RVA: 0x0013793D File Offset: 0x00135B3D
		internal bool HasSkippedMembers
		{
			get
			{
				return this.m_hasSkippedMembers;
			}
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x00137948 File Offset: 0x00135B48
		internal void RegisterCurrentDeclaration(Declaration currentDeclaration)
		{
			this.m_hasSkippedMembers = false;
			this.m_usableMembers = new Pair<bool, int>[this.m_memberInfoList.Count];
			int num = 0;
			for (int i = this.m_memberInfoList.Count - 1; i >= 0; i--)
			{
				if (currentDeclaration.Contains(this.m_memberInfoList[i]))
				{
					num = 0;
				}
				else
				{
					this.m_hasSkippedMembers = true;
					num++;
					this.m_usableMembers[i].Second = num;
					this.m_usableMembers[i].First = true;
				}
			}
			if (!this.m_hasSkippedMembers)
			{
				this.m_usableMembers = new Pair<bool, int>[0];
			}
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x001379E7 File Offset: 0x00135BE7
		private bool Contains(MemberInfo otherMember)
		{
			return this.m_memberInfoList.Contains(otherMember);
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x001379F8 File Offset: 0x00135BF8
		internal Declaration CreateFilteredDeclarationForWriteVersion(int compatVersion)
		{
			List<MemberInfo> list = new List<MemberInfo>(this.m_memberInfoList.Count);
			for (int i = 0; i < this.m_memberInfoList.Count; i++)
			{
				MemberInfo memberInfo = this.m_memberInfoList[i];
				if (memberInfo.IsWrittenForCompatVersion(compatVersion))
				{
					list.Add(memberInfo);
				}
			}
			return new Declaration(this.m_type, this.m_baseType, list);
		}

		// Token: 0x040020B4 RID: 8372
		private List<MemberInfo> m_memberInfoList = new List<MemberInfo>();

		// Token: 0x040020B5 RID: 8373
		private ObjectType m_type;

		// Token: 0x040020B6 RID: 8374
		private ObjectType m_baseType;

		// Token: 0x040020B7 RID: 8375
		private Pair<bool, int>[] m_usableMembers;

		// Token: 0x040020B8 RID: 8376
		private bool m_hasSkippedMembers;
	}
}
