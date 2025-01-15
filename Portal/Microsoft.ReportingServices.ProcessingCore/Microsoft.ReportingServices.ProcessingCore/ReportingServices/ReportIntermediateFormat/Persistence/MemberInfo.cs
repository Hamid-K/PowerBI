using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200054F RID: 1359
	internal class MemberInfo
	{
		// Token: 0x060049B8 RID: 18872 RVA: 0x0013751D File Offset: 0x0013571D
		internal MemberInfo(MemberName name, Token token)
		{
			this.m_name = name;
			this.m_token = token;
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x00137553 File Offset: 0x00135753
		internal MemberInfo(MemberName name, Token token, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_lifetime = lifetime;
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x00137590 File Offset: 0x00135790
		internal MemberInfo(MemberName name, ObjectType type)
		{
			this.m_name = name;
			this.m_type = type;
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x001375C6 File Offset: 0x001357C6
		internal MemberInfo(MemberName name, ObjectType type, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_type = type;
			this.m_lifetime = lifetime;
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x00137603 File Offset: 0x00135803
		internal MemberInfo(MemberName name, ObjectType type, ObjectType containedType)
		{
			this.m_name = name;
			this.m_type = type;
			this.m_containedType = containedType;
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x00137640 File Offset: 0x00135840
		internal MemberInfo(MemberName name, ObjectType type, ObjectType containedType, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_type = type;
			this.m_containedType = containedType;
			this.m_lifetime = lifetime;
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x00137690 File Offset: 0x00135890
		internal MemberInfo(MemberName name, ObjectType type, Token token)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x001376D0 File Offset: 0x001358D0
		internal MemberInfo(MemberName name, ObjectType type, Token token, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
			this.m_lifetime = lifetime;
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x00137720 File Offset: 0x00135920
		internal MemberInfo(MemberName name, ObjectType type, Token token, ObjectType containedType)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
			this.m_containedType = containedType;
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x00137770 File Offset: 0x00135970
		internal MemberInfo(MemberName name, ObjectType type, Token token, ObjectType containedType, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
			this.m_containedType = containedType;
			this.m_lifetime = lifetime;
		}

		// Token: 0x17001DE3 RID: 7651
		// (get) Token: 0x060049C2 RID: 18882 RVA: 0x001377C8 File Offset: 0x001359C8
		internal MemberName MemberName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17001DE4 RID: 7652
		// (get) Token: 0x060049C3 RID: 18883 RVA: 0x001377D0 File Offset: 0x001359D0
		internal Token Token
		{
			get
			{
				return this.m_token;
			}
		}

		// Token: 0x17001DE5 RID: 7653
		// (get) Token: 0x060049C4 RID: 18884 RVA: 0x001377D8 File Offset: 0x001359D8
		internal ObjectType ObjectType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17001DE6 RID: 7654
		// (get) Token: 0x060049C5 RID: 18885 RVA: 0x001377E0 File Offset: 0x001359E0
		internal ObjectType ContainedType
		{
			get
			{
				return this.m_containedType;
			}
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x001377E8 File Offset: 0x001359E8
		internal virtual bool IsWrittenForCompatVersion(int compatVersion)
		{
			return this.m_lifetime.IncludesVersion(compatVersion);
		}

		// Token: 0x17001DE7 RID: 7655
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x001377F6 File Offset: 0x001359F6
		internal Lifetime Lifetime
		{
			get
			{
				return this.m_lifetime;
			}
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x001377FE File Offset: 0x001359FE
		public override int GetHashCode()
		{
			return (int)(this.m_name ^ (MemberName)((int)this.m_token << 8) ^ (MemberName)((int)this.m_type << 16) ^ (MemberName)((int)this.m_containedType << 24));
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x00137823 File Offset: 0x00135A23
		public override bool Equals(object obj)
		{
			return obj != null && obj is MemberInfo && this.Equals((MemberInfo)obj);
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x0013783E File Offset: 0x00135A3E
		internal bool Equals(MemberInfo otherMember)
		{
			return otherMember != null && this.m_name == otherMember.m_name && this.m_token == otherMember.m_token && this.m_type == otherMember.m_type && this.m_containedType == otherMember.m_containedType;
		}

		// Token: 0x040020AF RID: 8367
		private MemberName m_name;

		// Token: 0x040020B0 RID: 8368
		private Token m_token = Token.Object;

		// Token: 0x040020B1 RID: 8369
		private ObjectType m_type = ObjectType.None;

		// Token: 0x040020B2 RID: 8370
		private ObjectType m_containedType = ObjectType.None;

		// Token: 0x040020B3 RID: 8371
		private Lifetime m_lifetime = Lifetime.Unspecified;
	}
}
