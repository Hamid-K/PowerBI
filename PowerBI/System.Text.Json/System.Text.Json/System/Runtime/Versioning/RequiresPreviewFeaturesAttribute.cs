using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	internal sealed class RequiresPreviewFeaturesAttribute : Attribute
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00003301 File Offset: 0x00001501
		public RequiresPreviewFeaturesAttribute()
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00003309 File Offset: 0x00001509
		public RequiresPreviewFeaturesAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003318 File Offset: 0x00001518
		public string Message { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00003320 File Offset: 0x00001520
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00003328 File Offset: 0x00001528
		public string Url { get; set; }
	}
}
