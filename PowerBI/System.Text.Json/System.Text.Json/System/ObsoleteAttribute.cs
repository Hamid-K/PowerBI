using System;

namespace System
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	internal sealed class ObsoleteAttribute : Attribute
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00002F90 File Offset: 0x00001190
		public ObsoleteAttribute()
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00002F98 File Offset: 0x00001198
		public ObsoleteAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002FA7 File Offset: 0x000011A7
		public ObsoleteAttribute(string message, bool error)
		{
			this.Message = message;
			this.IsError = error;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002FBD File Offset: 0x000011BD
		public string Message { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002FC5 File Offset: 0x000011C5
		public bool IsError { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002FCD File Offset: 0x000011CD
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00002FD5 File Offset: 0x000011D5
		public string DiagnosticId { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002FDE File Offset: 0x000011DE
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00002FE6 File Offset: 0x000011E6
		public string UrlFormat { get; set; }
	}
}
