using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
	[Embedded]
	[CompilerGenerated]
	internal sealed class NativeIntegerAttribute : Attribute
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000245F File Offset: 0x0000065F
		public NativeIntegerAttribute()
		{
			this.TransformFlags = new bool[] { true };
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002477 File Offset: 0x00000677
		public NativeIntegerAttribute(bool[] A_0)
		{
			this.TransformFlags = A_0;
		}

		// Token: 0x04000001 RID: 1
		public readonly bool[] TransformFlags;
	}
}
