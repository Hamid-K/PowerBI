using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000128 RID: 296
	[ImmutableObject(true)]
	public sealed class ConceptualTranslation
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x000101F5 File Offset: 0x0000E3F5
		public ConceptualTranslation(string locale, string caption, string description)
		{
			this.Locale = locale;
			this.Caption = caption;
			this.Description = description;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00010212 File Offset: 0x0000E412
		public string Locale { get; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0001021A File Offset: 0x0000E41A
		public string Caption { get; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00010222 File Offset: 0x0000E422
		public string Description { get; }
	}
}
