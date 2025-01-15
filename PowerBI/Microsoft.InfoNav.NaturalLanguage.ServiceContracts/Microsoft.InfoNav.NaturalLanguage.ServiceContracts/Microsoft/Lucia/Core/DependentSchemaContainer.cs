using System;
using System.ComponentModel;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core.DomainModel.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200007E RID: 126
	[ImmutableObject(true)]
	public sealed class DependentSchemaContainer
	{
		// Token: 0x0600023D RID: 573 RVA: 0x000053CF File Offset: 0x000035CF
		public DependentSchemaContainer(IConceptualSchema dependentConceptualSchema, LsdlDocument lsdlDocument)
		{
			this.DependentConceptualSchema = dependentConceptualSchema;
			this.LsdlDocument = lsdlDocument;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000053E5 File Offset: 0x000035E5
		public IConceptualSchema DependentConceptualSchema { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000053ED File Offset: 0x000035ED
		public LsdlDocument LsdlDocument { get; }
	}
}
