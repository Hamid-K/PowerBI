using System;

namespace Microsoft.ReportingServices.ReportProcessing.Persistence
{
	// Token: 0x020007A3 RID: 1955
	internal sealed class MemberInfo
	{
		// Token: 0x06006C6A RID: 27754 RVA: 0x001B7912 File Offset: 0x001B5B12
		internal MemberInfo(MemberName memberName, Token token)
		{
			this.m_memberName = memberName;
			this.m_token = token;
			this.m_objectType = ObjectType.None;
		}

		// Token: 0x06006C6B RID: 27755 RVA: 0x001B792F File Offset: 0x001B5B2F
		internal MemberInfo(MemberName memberName, ObjectType objectType)
		{
			this.m_memberName = memberName;
			this.m_token = Token.Object;
			this.m_objectType = objectType;
		}

		// Token: 0x06006C6C RID: 27756 RVA: 0x001B794C File Offset: 0x001B5B4C
		internal MemberInfo(MemberName memberName, Token token, ObjectType objectType)
		{
			this.m_memberName = memberName;
			this.m_token = token;
			this.m_objectType = objectType;
		}

		// Token: 0x06006C6D RID: 27757 RVA: 0x001B7969 File Offset: 0x001B5B69
		internal static bool Equals(MemberInfo a, MemberInfo b)
		{
			return a != null && b != null && (a.MemberName == b.MemberName && a.Token == b.Token) && a.ObjectType == b.ObjectType;
		}

		// Token: 0x170025BC RID: 9660
		// (get) Token: 0x06006C6E RID: 27758 RVA: 0x001B799F File Offset: 0x001B5B9F
		// (set) Token: 0x06006C6F RID: 27759 RVA: 0x001B79A7 File Offset: 0x001B5BA7
		internal MemberName MemberName
		{
			get
			{
				return this.m_memberName;
			}
			set
			{
				this.m_memberName = value;
			}
		}

		// Token: 0x170025BD RID: 9661
		// (get) Token: 0x06006C70 RID: 27760 RVA: 0x001B79B0 File Offset: 0x001B5BB0
		internal Token Token
		{
			get
			{
				return this.m_token;
			}
		}

		// Token: 0x170025BE RID: 9662
		// (get) Token: 0x06006C71 RID: 27761 RVA: 0x001B79B8 File Offset: 0x001B5BB8
		internal ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
		}

		// Token: 0x04003966 RID: 14694
		private MemberName m_memberName;

		// Token: 0x04003967 RID: 14695
		private Token m_token;

		// Token: 0x04003968 RID: 14696
		private ObjectType m_objectType;
	}
}
