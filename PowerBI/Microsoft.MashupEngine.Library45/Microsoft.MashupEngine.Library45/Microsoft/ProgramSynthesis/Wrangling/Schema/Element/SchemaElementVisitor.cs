using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.Element
{
	// Token: 0x02000145 RID: 325
	public abstract class SchemaElementVisitor<TTranslation, TSchemaRegion>
	{
		// Token: 0x06000738 RID: 1848
		public abstract TTranslation VisitSequenceElement(SequenceElement<TSchemaRegion> sequenceElement);

		// Token: 0x06000739 RID: 1849
		public abstract TTranslation VisitStructElement(StructElement<TSchemaRegion> structElement);
	}
}
