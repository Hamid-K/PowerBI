using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001F3 RID: 499
	[Obsolete("Use [ContractAnnotation('=> halt')] instead")]
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class TerminatesProgramAttribute : Attribute
	{
	}
}
