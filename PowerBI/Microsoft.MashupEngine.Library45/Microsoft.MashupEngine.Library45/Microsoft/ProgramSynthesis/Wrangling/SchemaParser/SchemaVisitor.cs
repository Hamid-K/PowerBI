using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x02000161 RID: 353
	public abstract class SchemaVisitor<T, TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x060007DF RID: 2015
		public abstract T VisitBot(BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion> node);

		// Token: 0x060007E0 RID: 2016
		public abstract T VisitField(FieldSchemaElement<TSequenceProgram, TRegionProgram, TRegion> field);

		// Token: 0x060007E1 RID: 2017
		public abstract T VisitStruct(StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion> node);

		// Token: 0x060007E2 RID: 2018
		public abstract T VisitSequence(SequenceSchemaElement<TSequenceProgram, TRegionProgram, TRegion> node);

		// Token: 0x060007E3 RID: 2019
		public abstract T VisitUnion(UnionSchemaElement<TSequenceProgram, TRegionProgram, TRegion> node);

		// Token: 0x060007E4 RID: 2020
		public abstract T VisitConvert<TSequenceProgramChild, TRegionProgramChild, TRegionChild>(ConvertSchemaElement<TSequenceProgram, TRegionProgram, TRegion, TSequenceProgramChild, TRegionProgramChild, TRegionChild> convertSchemaElement) where TSequenceProgramChild : SequenceExtractionProgram<TSequenceProgramChild, TRegionChild> where TRegionProgramChild : RegionExtractionProgram<TRegionProgramChild, TRegionChild> where TRegionChild : IRegion<TRegionChild>;
	}
}
