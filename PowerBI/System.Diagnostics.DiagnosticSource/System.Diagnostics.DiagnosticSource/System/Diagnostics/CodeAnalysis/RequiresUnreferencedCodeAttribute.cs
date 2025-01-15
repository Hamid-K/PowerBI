using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000061 RID: 97
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class RequiresUnreferencedCodeAttribute : Attribute
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0000B2A1 File Offset: 0x000094A1
		public RequiresUnreferencedCodeAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B2B0 File Offset: 0x000094B0
		public string Message { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000B2B8 File Offset: 0x000094B8
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000B2C0 File Offset: 0x000094C0
		public string Url { get; set; }
	}
}
