using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200058A RID: 1418
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class RazorInjectionAttribute : Attribute
	{
		// Token: 0x06001F1A RID: 7962 RVA: 0x00059B74 File Offset: 0x00057D74
		public RazorInjectionAttribute(string type, string fieldName)
		{
			this.Type = type;
			this.FieldName = fieldName;
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x00059B8A File Offset: 0x00057D8A
		public string Type { get; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x00059B92 File Offset: 0x00057D92
		public string FieldName { get; }
	}
}
