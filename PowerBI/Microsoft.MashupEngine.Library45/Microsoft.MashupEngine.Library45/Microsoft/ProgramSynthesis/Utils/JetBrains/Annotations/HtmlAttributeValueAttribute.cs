using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000575 RID: 1397
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class HtmlAttributeValueAttribute : Attribute
	{
		// Token: 0x06001EFF RID: 7935 RVA: 0x00059AC4 File Offset: 0x00057CC4
		public HtmlAttributeValueAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00059AD3 File Offset: 0x00057CD3
		public string Name { get; }
	}
}
