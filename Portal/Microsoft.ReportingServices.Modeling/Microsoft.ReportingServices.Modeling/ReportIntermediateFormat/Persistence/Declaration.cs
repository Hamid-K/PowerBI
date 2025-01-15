using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000033 RID: 51
	public class Declaration
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00008698 File Offset: 0x00006898
		internal Declaration(ObjectType type, ObjectType baseType, List<MemberInfo> memberInfoList)
		{
			this.m_type = type;
			this.m_baseType = baseType;
			this.m_memberInfoList = memberInfoList;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000086C0 File Offset: 0x000068C0
		internal List<MemberInfo> MemberInfoList
		{
			get
			{
				return this.m_memberInfoList;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000086C8 File Offset: 0x000068C8
		internal ObjectType ObjectType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000086D0 File Offset: 0x000068D0
		internal ObjectType BaseObjectType
		{
			get
			{
				return this.m_baseType;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000086D8 File Offset: 0x000068D8
		internal bool RegisteredCurrentDeclaration
		{
			get
			{
				return this.m_usableMembers != null;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000086E3 File Offset: 0x000068E3
		internal bool IsMemberSkipped(int index)
		{
			return this.m_hasSkippedMembers && this.m_usableMembers[index].First;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008700 File Offset: 0x00006900
		internal int MembersToSkip(int index)
		{
			if (this.m_hasSkippedMembers)
			{
				return this.m_usableMembers[index].Second;
			}
			return 0;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000871D File Offset: 0x0000691D
		internal bool HasSkippedMembers
		{
			get
			{
				return this.m_hasSkippedMembers;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008728 File Offset: 0x00006928
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

		// Token: 0x060001EE RID: 494 RVA: 0x000087C7 File Offset: 0x000069C7
		private bool Contains(MemberInfo otherMember)
		{
			return this.m_memberInfoList.Contains(otherMember);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000087D8 File Offset: 0x000069D8
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

		// Token: 0x04000134 RID: 308
		private List<MemberInfo> m_memberInfoList = new List<MemberInfo>();

		// Token: 0x04000135 RID: 309
		private ObjectType m_type;

		// Token: 0x04000136 RID: 310
		private ObjectType m_baseType;

		// Token: 0x04000137 RID: 311
		private Pair<bool, int>[] m_usableMembers;

		// Token: 0x04000138 RID: 312
		private bool m_hasSkippedMembers;
	}
}
