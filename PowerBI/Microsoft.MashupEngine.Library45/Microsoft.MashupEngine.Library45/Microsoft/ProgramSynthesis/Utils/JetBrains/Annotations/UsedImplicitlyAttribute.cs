using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000554 RID: 1364
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class UsedImplicitlyAttribute : Attribute
	{
		// Token: 0x06001EBC RID: 7868 RVA: 0x000598DE File Offset: 0x00057ADE
		public UsedImplicitlyAttribute()
			: this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x000598E8 File Offset: 0x00057AE8
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
			: this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x000598F2 File Offset: 0x00057AF2
		public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
			: this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000598FC File Offset: 0x00057AFC
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x00059912 File Offset: 0x00057B12
		public ImplicitUseKindFlags UseKindFlags { get; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0005991A File Offset: 0x00057B1A
		public ImplicitUseTargetFlags TargetFlags { get; }
	}
}
