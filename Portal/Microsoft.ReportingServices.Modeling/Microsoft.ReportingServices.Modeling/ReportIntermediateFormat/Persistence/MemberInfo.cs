using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000031 RID: 49
	internal class MemberInfo
	{
		// Token: 0x060001CC RID: 460 RVA: 0x000082FD File Offset: 0x000064FD
		internal MemberInfo(MemberName name, Token token)
		{
			this.m_name = name;
			this.m_token = token;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008333 File Offset: 0x00006533
		internal MemberInfo(MemberName name, Token token, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_lifetime = lifetime;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008370 File Offset: 0x00006570
		internal MemberInfo(MemberName name, ObjectType type)
		{
			this.m_name = name;
			this.m_type = type;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000083A6 File Offset: 0x000065A6
		internal MemberInfo(MemberName name, ObjectType type, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_type = type;
			this.m_lifetime = lifetime;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000083E3 File Offset: 0x000065E3
		internal MemberInfo(MemberName name, ObjectType type, ObjectType containedType)
		{
			this.m_name = name;
			this.m_type = type;
			this.m_containedType = containedType;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008420 File Offset: 0x00006620
		internal MemberInfo(MemberName name, ObjectType type, ObjectType containedType, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_type = type;
			this.m_containedType = containedType;
			this.m_lifetime = lifetime;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008470 File Offset: 0x00006670
		internal MemberInfo(MemberName name, ObjectType type, Token token)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000084B0 File Offset: 0x000066B0
		internal MemberInfo(MemberName name, ObjectType type, Token token, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
			this.m_lifetime = lifetime;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008500 File Offset: 0x00006700
		internal MemberInfo(MemberName name, ObjectType type, Token token, ObjectType containedType)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
			this.m_containedType = containedType;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008550 File Offset: 0x00006750
		internal MemberInfo(MemberName name, ObjectType type, Token token, ObjectType containedType, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
			this.m_containedType = containedType;
			this.m_lifetime = lifetime;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000085A8 File Offset: 0x000067A8
		internal MemberName MemberName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000085B0 File Offset: 0x000067B0
		internal Token Token
		{
			get
			{
				return this.m_token;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000085B8 File Offset: 0x000067B8
		internal ObjectType ObjectType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000085C0 File Offset: 0x000067C0
		internal ObjectType ContainedType
		{
			get
			{
				return this.m_containedType;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000085C8 File Offset: 0x000067C8
		internal virtual bool IsWrittenForCompatVersion(int compatVersion)
		{
			return this.m_lifetime.IncludesVersion(compatVersion);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000085D6 File Offset: 0x000067D6
		internal Lifetime Lifetime
		{
			get
			{
				return this.m_lifetime;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000085DE File Offset: 0x000067DE
		public override int GetHashCode()
		{
			return (int)(this.m_name ^ (MemberName)((int)this.m_token << 8) ^ (MemberName)((int)this.m_type << 16) ^ (MemberName)((int)this.m_containedType << 24));
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008603 File Offset: 0x00006803
		public override bool Equals(object obj)
		{
			return obj != null && obj is MemberInfo && this.Equals((MemberInfo)obj);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000861E File Offset: 0x0000681E
		internal bool Equals(MemberInfo otherMember)
		{
			return otherMember != null && this.m_name == otherMember.m_name && this.m_token == otherMember.m_token && this.m_type == otherMember.m_type && this.m_containedType == otherMember.m_containedType;
		}

		// Token: 0x0400012F RID: 303
		private MemberName m_name;

		// Token: 0x04000130 RID: 304
		private Token m_token = Token.Object;

		// Token: 0x04000131 RID: 305
		private ObjectType m_type = ObjectType.None;

		// Token: 0x04000132 RID: 306
		private ObjectType m_containedType = ObjectType.None;

		// Token: 0x04000133 RID: 307
		private Lifetime m_lifetime = Lifetime.Unspecified;
	}
}
