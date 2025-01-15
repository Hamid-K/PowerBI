using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001CB RID: 459
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class UsedImplicitlyAttribute : Attribute
	{
		// Token: 0x0600141A RID: 5146 RVA: 0x0003675C File Offset: 0x0003495C
		public UsedImplicitlyAttribute()
			: this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00036766 File Offset: 0x00034966
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
			: this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00036770 File Offset: 0x00034970
		public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
			: this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0003677A File Offset: 0x0003497A
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00036790 File Offset: 0x00034990
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x00036798 File Offset: 0x00034998
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x000367A1 File Offset: 0x000349A1
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x000367A9 File Offset: 0x000349A9
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
