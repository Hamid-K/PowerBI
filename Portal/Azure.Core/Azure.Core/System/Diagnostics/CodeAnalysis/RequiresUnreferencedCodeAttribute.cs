using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class RequiresUnreferencedCodeAttribute : Attribute
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002161 File Offset: 0x00000361
		public RequiresUnreferencedCodeAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002170 File Offset: 0x00000370
		public string Message { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002178 File Offset: 0x00000378
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002180 File Offset: 0x00000380
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
