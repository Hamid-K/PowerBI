using System;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200014B RID: 331
	public abstract class ConvertSchemaElementFactoryGeneric2<TSequenceProgramParent, TRegionProgramParent, TRegionParent> : ConvertSchemaElementInterface where TSequenceProgramParent : SequenceExtractionProgram<TSequenceProgramParent, TRegionParent> where TRegionProgramParent : RegionExtractionProgram<TRegionProgramParent, TRegionParent> where TRegionParent : IRegion<TRegionParent>
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x0001700A File Offset: 0x0001520A
		protected ConvertSchemaElementFactoryGeneric2(string converterName)
			: base(converterName)
		{
		}

		// Token: 0x06000759 RID: 1881
		public abstract SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> CreateAndParseChild(string name, SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> parent, bool nullable, ExtractionLearner<TSequenceProgramParent, TRegionProgramParent, TRegionParent> learner, XElement child, ConvertSchemaElementInterface[] converters, ASTSerializationFormat formatPrograms, DeserializationContext context);

		// Token: 0x0600075A RID: 1882
		public abstract SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> Create(string name, SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> parent, bool nullable, ExtractionLearner<TSequenceProgramParent, TRegionProgramParent, TRegionParent> learner);

		// Token: 0x0600075B RID: 1883
		public abstract SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> CreateAndParseChildGrammar(string name, XElement childMemberXml, bool isNullable, ExtractionLearner<TSequenceProgramParent, TRegionProgramParent, TRegionParent> learner, ConvertSchemaElementInterface[] converters);
	}
}
