using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000550 RID: 1360
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class ContractAnnotationAttribute : Attribute
	{
		// Token: 0x06001EB2 RID: 7858 RVA: 0x00059877 File Offset: 0x00057A77
		public ContractAnnotationAttribute(string contract)
			: this(contract, false)
		{
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00059881 File Offset: 0x00057A81
		public ContractAnnotationAttribute(string contract, bool forceFullStates)
		{
			this.Contract = contract;
			this.ForceFullStates = forceFullStates;
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x00059897 File Offset: 0x00057A97
		public string Contract { get; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x0005989F File Offset: 0x00057A9F
		public bool ForceFullStates { get; }
	}
}
