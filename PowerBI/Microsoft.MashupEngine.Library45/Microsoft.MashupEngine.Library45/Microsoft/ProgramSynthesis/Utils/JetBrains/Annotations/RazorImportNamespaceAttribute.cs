using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000589 RID: 1417
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class RazorImportNamespaceAttribute : Attribute
	{
		// Token: 0x06001F18 RID: 7960 RVA: 0x00059B5D File Offset: 0x00057D5D
		public RazorImportNamespaceAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x00059B6C File Offset: 0x00057D6C
		public string Name { get; }
	}
}
