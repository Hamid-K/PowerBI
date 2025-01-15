using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, Inherited = false)]
	internal sealed class DynamicallyAccessedMembersAttribute : Attribute
	{
		// Token: 0x06000104 RID: 260 RVA: 0x0000309D File Offset: 0x0000129D
		public DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes memberTypes)
		{
			this.MemberTypes = memberTypes;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000030AC File Offset: 0x000012AC
		public DynamicallyAccessedMemberTypes MemberTypes { get; }
	}
}
