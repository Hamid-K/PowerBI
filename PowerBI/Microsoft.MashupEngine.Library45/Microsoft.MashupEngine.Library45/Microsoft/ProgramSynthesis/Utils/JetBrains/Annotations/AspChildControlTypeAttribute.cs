using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000583 RID: 1411
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspChildControlTypeAttribute : Attribute
	{
		// Token: 0x06001F0E RID: 7950 RVA: 0x00059B09 File Offset: 0x00057D09
		public AspChildControlTypeAttribute(string tagName, Type controlType)
		{
			this.TagName = tagName;
			this.ControlType = controlType;
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x00059B1F File Offset: 0x00057D1F
		public string TagName { get; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00059B27 File Offset: 0x00057D27
		public Type ControlType { get; }
	}
}
