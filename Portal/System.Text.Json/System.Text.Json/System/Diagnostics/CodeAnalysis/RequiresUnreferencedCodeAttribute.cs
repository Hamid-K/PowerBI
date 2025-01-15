using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class RequiresUnreferencedCodeAttribute : Attribute
	{
		// Token: 0x06000106 RID: 262 RVA: 0x000030B4 File Offset: 0x000012B4
		public RequiresUnreferencedCodeAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000030C3 File Offset: 0x000012C3
		public string Message { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000030CB File Offset: 0x000012CB
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000030D3 File Offset: 0x000012D3
		public string Url { get; set; }
	}
}
