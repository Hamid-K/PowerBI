using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000588 RID: 1416
	[AttributeUsage(AttributeTargets.Property)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspTypePropertyAttribute : Attribute
	{
		// Token: 0x06001F16 RID: 7958 RVA: 0x00059B46 File Offset: 0x00057D46
		public AspTypePropertyAttribute(bool createConstructorReferences)
		{
			this.CreateConstructorReferences = createConstructorReferences;
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x00059B55 File Offset: 0x00057D55
		public bool CreateConstructorReferences { get; }
	}
}
