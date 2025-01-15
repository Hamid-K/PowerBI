using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.SchemaParser;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000D2 RID: 210
	public abstract class SchemaProgram<TSequenceProgram, TRegionProgram, TSelector, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0001032D File Offset: 0x0000E52D
		protected SchemaProgram(SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion> sg)
		{
			this.LearnedSchemaProgram = sg;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001033C File Offset: 0x0000E53C
		protected SchemaProgram(string schema, bool learnAll, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner, ConvertSchemaElementInterface[] converters = null)
		{
			this.LearnedSchemaProgram = SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion>.Load(schema, learner, converters);
			this.LearnAllPrograms = learnAll;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001035A File Offset: 0x0000E55A
		public SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion> LearnedSchemaProgram { get; }

		// Token: 0x060004AB RID: 1195 RVA: 0x00010362 File Offset: 0x0000E562
		public string Format(ASTSerializationFormat format = ASTSerializationFormat.XML)
		{
			SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion> learnedSchemaProgram = this.LearnedSchemaProgram;
			if (((learnedSchemaProgram != null) ? learnedSchemaProgram.Root : null) == null)
			{
				return "No program learned";
			}
			return this.LearnedSchemaProgram.Root.Serialize(format);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001038F File Offset: 0x0000E58F
		public override string ToString()
		{
			return this.Format(ASTSerializationFormat.HumanReadable);
		}

		// Token: 0x060004AD RID: 1197
		public abstract TRegion Select(TRegion input, TSelector selector);

		// Token: 0x060004AE RID: 1198 RVA: 0x00010398 File Offset: 0x0000E598
		public TreeElement<TRegion> Run(TRegion s)
		{
			return this.LearnedSchemaProgram.RunTree(s);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000103A8 File Offset: 0x0000E5A8
		public IEnumerable<SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion>> LearnElementField(string elementName, IEnumerable<TSelector> positiveExamples, IEnumerable<TSelector> negativeExamples, TRegion s = default(TRegion), int k = 1)
		{
			return this.LearnElementField(elementName, positiveExamples.Select((TSelector x) => this.Select(s, x)), negativeExamples.Select((TSelector x) => this.Select(s, x)), s, k);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00010400 File Offset: 0x0000E600
		public IEnumerable<SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion>> LearnElementField(string elementName, IEnumerable<TRegion> positiveExamples, IEnumerable<TRegion> negativeExamples, TRegion input, int k = 1)
		{
			SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement = this.LearnedSchemaProgram.FindElement(elementName);
			if (schemaElement == null)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Learning fails. Element {0} could not be found!", new object[] { elementName })));
			}
			if (this.CurrentElement != null && schemaElement != this.CurrentElement && schemaElement.Programs.IsAny<IExtractionProgram<TRegion>>())
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Learning fails. Element {0} has been learned previously!", new object[] { elementName })));
			}
			this.CurrentElement = schemaElement;
			if (input == null)
			{
				throw new ArgumentException("provided input must not be null");
			}
			Dictionary<string, IEnumerable<TRegion>> dictionary = new Dictionary<string, IEnumerable<TRegion>>();
			dictionary[elementName] = positiveExamples;
			Dictionary<string, IEnumerable<TRegion>> dictionary2 = dictionary;
			Dictionary<string, IEnumerable<TRegion>> dictionary3 = new Dictionary<string, IEnumerable<TRegion>>();
			dictionary3[elementName] = negativeExamples;
			Dictionary<string, IEnumerable<TRegion>> dictionary4 = dictionary3;
			DocumentSpec<TRegion> documentSpec = new DocumentSpec<TRegion>(input, dictionary2, dictionary4);
			return schemaElement.LearnElementField(Seq.Of<DocumentSpec<TRegion>>(new DocumentSpec<TRegion>[] { documentSpec }), k, this.LearnAllPrograms);
		}

		// Token: 0x04000203 RID: 515
		protected SchemaElement<TSequenceProgram, TRegionProgram, TRegion> CurrentElement;

		// Token: 0x04000204 RID: 516
		protected readonly bool LearnAllPrograms;
	}
}
