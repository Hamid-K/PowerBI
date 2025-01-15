using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000574 RID: 1396
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class HtmlElementAttributesAttribute : Attribute
	{
		// Token: 0x06001EFC RID: 7932 RVA: 0x0000957E File Offset: 0x0000777E
		public HtmlElementAttributesAttribute()
		{
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00059AAD File Offset: 0x00057CAD
		public HtmlElementAttributesAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00059ABC File Offset: 0x00057CBC
		public string Name { get; }
	}
}
