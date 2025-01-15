using System;

namespace System.Web.Http.Description
{
	// Token: 0x020000E7 RID: 231
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class ApiExplorerSettingsAttribute : Attribute
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0000FA1D File Offset: 0x0000DC1D
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0000FA25 File Offset: 0x0000DC25
		public bool IgnoreApi { get; set; }
	}
}
