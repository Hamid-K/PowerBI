using System;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Edm;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000054 RID: 84
	internal sealed class ConceptualSchemaAndCapabilities
	{
		// Token: 0x060001DB RID: 475 RVA: 0x000057A4 File Offset: 0x000039A4
		internal ConceptualSchemaAndCapabilities(IConceptualSchema conceptualSchema, ModelDaxCapabilities capabilities)
		{
			this.ConceptualSchema = conceptualSchema;
			this.Capabilities = capabilities;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000057BA File Offset: 0x000039BA
		internal IConceptualSchema ConceptualSchema { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000057C2 File Offset: 0x000039C2
		internal ModelDaxCapabilities Capabilities { get; }
	}
}
