using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000561 RID: 1377
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class UsedImplicitlyAttribute : Attribute
	{
		// Token: 0x06002A2E RID: 10798 RVA: 0x00098236 File Offset: 0x00096436
		[UsedImplicitly]
		public UsedImplicitlyAttribute()
			: this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x00098240 File Offset: 0x00096440
		[UsedImplicitly]
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x00098256 File Offset: 0x00096456
		[UsedImplicitly]
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
			: this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x00098260 File Offset: 0x00096460
		[UsedImplicitly]
		public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
			: this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x0009826A File Offset: 0x0009646A
		// (set) Token: 0x06002A33 RID: 10803 RVA: 0x00098272 File Offset: 0x00096472
		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x0009827B File Offset: 0x0009647B
		// (set) Token: 0x06002A35 RID: 10805 RVA: 0x00098283 File Offset: 0x00096483
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
