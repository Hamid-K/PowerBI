using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000032 RID: 50
	internal class ReadOnlyMemberInfo : MemberInfo
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000865E File Offset: 0x0000685E
		internal ReadOnlyMemberInfo(MemberName name, Token token)
			: base(name, token)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008668 File Offset: 0x00006868
		internal ReadOnlyMemberInfo(MemberName name, ObjectType type)
			: base(name, type)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008672 File Offset: 0x00006872
		internal ReadOnlyMemberInfo(MemberName name, ObjectType type, ObjectType containedType)
			: base(name, type, containedType)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000867D File Offset: 0x0000687D
		internal ReadOnlyMemberInfo(MemberName name, ObjectType type, Token token)
			: base(name, type, token)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008688 File Offset: 0x00006888
		internal ReadOnlyMemberInfo(MemberName name, ObjectType type, Token token, ObjectType containedType)
			: base(name, type, token, containedType)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008695 File Offset: 0x00006895
		internal override bool IsWrittenForCompatVersion(int compatVersion)
		{
			return false;
		}
	}
}
