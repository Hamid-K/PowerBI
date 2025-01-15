using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class RequiresDynamicCodeAttribute : Attribute
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002139 File Offset: 0x00000339
		public RequiresDynamicCodeAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002148 File Offset: 0x00000348
		public string Message { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002150 File Offset: 0x00000350
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002158 File Offset: 0x00000358
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
