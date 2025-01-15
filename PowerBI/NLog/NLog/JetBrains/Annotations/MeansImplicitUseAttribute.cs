using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001CC RID: 460
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.GenericParameter)]
	internal sealed class MeansImplicitUseAttribute : Attribute
	{
		// Token: 0x06001422 RID: 5154 RVA: 0x000367B2 File Offset: 0x000349B2
		public MeansImplicitUseAttribute()
			: this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x000367BC File Offset: 0x000349BC
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
			: this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x000367C6 File Offset: 0x000349C6
		public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
			: this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x000367D0 File Offset: 0x000349D0
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x000367E6 File Offset: 0x000349E6
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x000367EE File Offset: 0x000349EE
		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x000367F7 File Offset: 0x000349F7
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x000367FF File Offset: 0x000349FF
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
