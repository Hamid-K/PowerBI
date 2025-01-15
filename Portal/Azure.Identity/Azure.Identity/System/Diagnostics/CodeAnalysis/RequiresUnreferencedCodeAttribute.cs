using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class RequiresUnreferencedCodeAttribute : Attribute
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020D5 File Offset: 0x000002D5
		public RequiresUnreferencedCodeAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020E4 File Offset: 0x000002E4
		public string Message { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020EC File Offset: 0x000002EC
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020F4 File Offset: 0x000002F4
		[Nullable(2)]
		public string Url
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}
	}
}
