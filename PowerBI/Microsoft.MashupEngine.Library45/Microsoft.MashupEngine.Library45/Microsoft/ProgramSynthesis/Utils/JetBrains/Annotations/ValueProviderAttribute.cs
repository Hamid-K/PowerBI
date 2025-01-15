using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200054D RID: 1357
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class ValueProviderAttribute : Attribute
	{
		// Token: 0x06001EAC RID: 7852 RVA: 0x00059849 File Offset: 0x00057A49
		public ValueProviderAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x00059858 File Offset: 0x00057A58
		public string Name { get; }
	}
}
