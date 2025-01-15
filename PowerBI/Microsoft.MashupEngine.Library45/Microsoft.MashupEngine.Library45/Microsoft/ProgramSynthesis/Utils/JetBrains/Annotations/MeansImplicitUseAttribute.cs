using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000555 RID: 1365
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.GenericParameter)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class MeansImplicitUseAttribute : Attribute
	{
		// Token: 0x06001EC2 RID: 7874 RVA: 0x00059922 File Offset: 0x00057B22
		public MeansImplicitUseAttribute()
			: this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0005992C File Offset: 0x00057B2C
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
			: this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00059936 File Offset: 0x00057B36
		public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
			: this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00059940 File Offset: 0x00057B40
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x00059956 File Offset: 0x00057B56
		public ImplicitUseKindFlags UseKindFlags { get; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0005995E File Offset: 0x00057B5E
		public ImplicitUseTargetFlags TargetFlags { get; }
	}
}
