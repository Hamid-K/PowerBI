using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class RequiresDynamicCodeAttribute : Attribute
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020AD File Offset: 0x000002AD
		public RequiresDynamicCodeAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020BC File Offset: 0x000002BC
		public string Message { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020C4 File Offset: 0x000002C4
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020CC File Offset: 0x000002CC
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
