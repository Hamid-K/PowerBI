using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000002 RID: 2
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class RequiresUnreferencedCodeAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public RequiresUnreferencedCodeAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205F File Offset: 0x0000025F
		public string Message { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		public string Url { get; set; }
	}
}
