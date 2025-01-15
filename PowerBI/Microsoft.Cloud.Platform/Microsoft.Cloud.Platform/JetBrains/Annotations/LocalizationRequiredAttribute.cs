using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000558 RID: 1368
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class LocalizationRequiredAttribute : Attribute
	{
		// Token: 0x06002A14 RID: 10772 RVA: 0x00098130 File Offset: 0x00096330
		public LocalizationRequiredAttribute()
			: this(true)
		{
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x00098139 File Offset: 0x00096339
		public LocalizationRequiredAttribute(bool required)
		{
			this.Required = required;
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x00098148 File Offset: 0x00096348
		// (set) Token: 0x06002A17 RID: 10775 RVA: 0x00098150 File Offset: 0x00096350
		[UsedImplicitly]
		public bool Required { get; private set; }

		// Token: 0x06002A18 RID: 10776 RVA: 0x0009815C File Offset: 0x0009635C
		public override bool Equals(object obj)
		{
			LocalizationRequiredAttribute localizationRequiredAttribute = obj as LocalizationRequiredAttribute;
			return localizationRequiredAttribute != null && localizationRequiredAttribute.Required == this.Required;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x00098183 File Offset: 0x00096383
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
