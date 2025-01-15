using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000009 RID: 9
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, Inherited = false, AllowMultiple = false)]
	internal sealed class ExcludeFromCodeCoverageAttribute : Attribute
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002373 File Offset: 0x00000573
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000237B File Offset: 0x0000057B
		public string Justification { get; set; }
	}
}
