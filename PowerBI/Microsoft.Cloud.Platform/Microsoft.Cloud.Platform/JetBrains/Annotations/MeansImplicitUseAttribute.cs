using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000562 RID: 1378
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class MeansImplicitUseAttribute : Attribute
	{
		// Token: 0x06002A36 RID: 10806 RVA: 0x0009828C File Offset: 0x0009648C
		[UsedImplicitly]
		public MeansImplicitUseAttribute()
			: this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x00098296 File Offset: 0x00096496
		[UsedImplicitly]
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000982AC File Offset: 0x000964AC
		[UsedImplicitly]
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
			: this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000982B6 File Offset: 0x000964B6
		[UsedImplicitly]
		public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
			: this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002A3A RID: 10810 RVA: 0x000982C0 File Offset: 0x000964C0
		// (set) Token: 0x06002A3B RID: 10811 RVA: 0x000982C8 File Offset: 0x000964C8
		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002A3C RID: 10812 RVA: 0x000982D1 File Offset: 0x000964D1
		// (set) Token: 0x06002A3D RID: 10813 RVA: 0x000982D9 File Offset: 0x000964D9
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
