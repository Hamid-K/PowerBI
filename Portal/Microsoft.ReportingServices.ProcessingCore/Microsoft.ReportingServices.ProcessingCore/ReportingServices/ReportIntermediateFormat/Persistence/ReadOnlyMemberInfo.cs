using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000550 RID: 1360
	internal class ReadOnlyMemberInfo : MemberInfo
	{
		// Token: 0x060049CB RID: 18891 RVA: 0x0013787E File Offset: 0x00135A7E
		internal ReadOnlyMemberInfo(MemberName name, Token token)
			: base(name, token)
		{
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x00137888 File Offset: 0x00135A88
		internal ReadOnlyMemberInfo(MemberName name, ObjectType type)
			: base(name, type)
		{
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x00137892 File Offset: 0x00135A92
		internal ReadOnlyMemberInfo(MemberName name, ObjectType type, ObjectType containedType)
			: base(name, type, containedType)
		{
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x0013789D File Offset: 0x00135A9D
		internal ReadOnlyMemberInfo(MemberName name, ObjectType type, Token token)
			: base(name, type, token)
		{
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x001378A8 File Offset: 0x00135AA8
		internal ReadOnlyMemberInfo(MemberName name, ObjectType type, Token token, ObjectType containedType)
			: base(name, type, token, containedType)
		{
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x001378B5 File Offset: 0x00135AB5
		internal override bool IsWrittenForCompatVersion(int compatVersion)
		{
			return false;
		}
	}
}
