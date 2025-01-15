using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, Inherited = false)]
	internal sealed class DynamicallyAccessedMembersAttribute : Attribute
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000020E2 File Offset: 0x000002E2
		public DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes memberTypes)
		{
			this.MemberTypes = memberTypes;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020F1 File Offset: 0x000002F1
		public DynamicallyAccessedMemberTypes MemberTypes { get; }
	}
}
