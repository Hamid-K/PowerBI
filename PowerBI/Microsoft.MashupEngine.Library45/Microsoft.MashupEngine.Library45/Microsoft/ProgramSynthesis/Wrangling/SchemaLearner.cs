using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.SchemaParser;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000CA RID: 202
	public abstract class SchemaLearner<TSchemaProgram, TSequenceProgram, TRegionProgram, TSelector, TRegion> where TSchemaProgram : SchemaProgram<TSequenceProgram, TRegionProgram, TSelector, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x0600047E RID: 1150
		public abstract TRegion Select(TRegion input, TSelector selector);

		// Token: 0x0600047F RID: 1151
		public abstract TRegion StringToInput(string input);

		// Token: 0x06000480 RID: 1152
		protected abstract TSchemaProgram Wrap(SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion> grammar);

		// Token: 0x06000481 RID: 1153
		protected abstract ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> GetLearner();

		// Token: 0x06000482 RID: 1154 RVA: 0x0000FC34 File Offset: 0x0000DE34
		public IEnumerable<TSchemaProgram> LearnSchema(string schema, IEnumerable<DocumentSpecInterface> specs, int k = 1, bool learnAll = false, ConvertSchemaElementInterface[] converters = null)
		{
			SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion> schemaGrammar = SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion>.Load(schema, this.GetLearner(), converters);
			schemaGrammar.Root.LearnElementAndChildren(specs, k, learnAll);
			return new TSchemaProgram[] { this.Wrap(schemaGrammar) };
		}
	}
}
