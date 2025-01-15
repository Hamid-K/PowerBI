using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class RequiresDynamicCodeAttribute : Attribute
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00003186 File Offset: 0x00001386
		public RequiresDynamicCodeAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003195 File Offset: 0x00001395
		public string Message { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000319D File Offset: 0x0000139D
		// (set) Token: 0x0600011C RID: 284 RVA: 0x000031A5 File Offset: 0x000013A5
		public string Url { get; set; }
	}
}
