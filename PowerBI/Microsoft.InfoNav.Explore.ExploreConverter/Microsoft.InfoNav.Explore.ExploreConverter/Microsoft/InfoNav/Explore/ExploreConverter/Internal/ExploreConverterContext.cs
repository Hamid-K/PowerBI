using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200000B RID: 11
	internal sealed class ExploreConverterContext
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002489 File Offset: 0x00000689
		internal ExploreConverterContext(IConceptualSchema conceptualSchema)
		{
			this.ConceptualSchema = conceptualSchema;
			this.SectionDisplayText = new Dictionary<string, string>();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024A3 File Offset: 0x000006A3
		internal ExploreConverterContext(IConceptualSchema conceptualSchema, IDictionary<string, string> sectionDisplayText)
		{
			this.ConceptualSchema = conceptualSchema;
			this.SectionDisplayText = sectionDisplayText;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000024B9 File Offset: 0x000006B9
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000024C1 File Offset: 0x000006C1
		internal IConceptualSchema ConceptualSchema { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000024CA File Offset: 0x000006CA
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000024D2 File Offset: 0x000006D2
		internal IDictionary<string, string> SectionDisplayText { get; private set; }
	}
}
