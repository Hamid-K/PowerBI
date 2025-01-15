using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200058C RID: 1420
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class RazorPageBaseTypeAttribute : Attribute
	{
		// Token: 0x06001F1F RID: 7967 RVA: 0x00059BB1 File Offset: 0x00057DB1
		public RazorPageBaseTypeAttribute(string baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x00059BC0 File Offset: 0x00057DC0
		public RazorPageBaseTypeAttribute(string baseType, string pageName)
		{
			this.BaseType = baseType;
			this.PageName = pageName;
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x00059BD6 File Offset: 0x00057DD6
		public string BaseType { get; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x00059BDE File Offset: 0x00057DDE
		public string PageName { get; }
	}
}
