using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001CF RID: 463
	[MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
	internal sealed class PublicAPIAttribute : Attribute
	{
		// Token: 0x0600142A RID: 5162 RVA: 0x00036808 File Offset: 0x00034A08
		public PublicAPIAttribute()
		{
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00036810 File Offset: 0x00034A10
		public PublicAPIAttribute([NotNull] string comment)
		{
			this.Comment = comment;
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0003681F File Offset: 0x00034A1F
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x00036827 File Offset: 0x00034A27
		[CanBeNull]
		public string Comment { get; private set; }
	}
}
