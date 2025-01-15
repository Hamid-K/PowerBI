using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200021F RID: 543
	internal class MemberContext
	{
		// Token: 0x06001242 RID: 4674 RVA: 0x000293ED File Offset: 0x000275ED
		internal MemberContext(string memberName, MemberContext.MemberContextTypes memberContextType)
		{
			this.m_name = memberName;
			this.m_memberContextType = memberContextType;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00029403 File Offset: 0x00027603
		internal MemberContext(TypeContext owningType, MemberInfo memberInfo)
		{
			this.m_memberInfos = new List<MemberInfo>();
			this.m_memberInfos.Add(memberInfo);
			this.m_owningType = owningType;
			this.m_memberContextType = MemberContext.MapMemberContextType(memberInfo.MemberType);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0002943A File Offset: 0x0002763A
		private static MemberContext.MemberContextTypes MapMemberContextType(MemberTypes memberType)
		{
			if (memberType == MemberTypes.Field)
			{
				return MemberContext.MemberContextTypes.Field;
			}
			if (memberType == MemberTypes.Method)
			{
				return MemberContext.MemberContextTypes.Method;
			}
			if (memberType == MemberTypes.Property)
			{
				return MemberContext.MemberContextTypes.Property;
			}
			return MemberContext.MemberContextTypes.Unknown;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00029450 File Offset: 0x00027650
		internal void AddOverload(MemberInfo memberInfo)
		{
			this.m_memberInfos.Add(memberInfo);
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0002945E File Offset: 0x0002765E
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00029466 File Offset: 0x00027666
		internal List<MemberInfo> MemberInfos
		{
			get
			{
				return this.m_memberInfos;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x0002946E File Offset: 0x0002766E
		internal TypeContext ReturnTypeContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00029471 File Offset: 0x00027671
		internal MemberContext.MemberContextTypes MemberContextType
		{
			get
			{
				return this.m_memberContextType;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00029479 File Offset: 0x00027679
		internal TypeContext OwningType
		{
			get
			{
				return this.m_owningType;
			}
		}

		// Token: 0x040005CE RID: 1486
		private readonly string m_name;

		// Token: 0x040005CF RID: 1487
		private readonly MemberContext.MemberContextTypes m_memberContextType;

		// Token: 0x040005D0 RID: 1488
		private readonly TypeContext m_owningType;

		// Token: 0x040005D1 RID: 1489
		private readonly List<MemberInfo> m_memberInfos;

		// Token: 0x0200040E RID: 1038
		internal enum MemberContextTypes
		{
			// Token: 0x040007CD RID: 1997
			Method,
			// Token: 0x040007CE RID: 1998
			Field,
			// Token: 0x040007CF RID: 1999
			Property,
			// Token: 0x040007D0 RID: 2000
			Unknown
		}
	}
}
