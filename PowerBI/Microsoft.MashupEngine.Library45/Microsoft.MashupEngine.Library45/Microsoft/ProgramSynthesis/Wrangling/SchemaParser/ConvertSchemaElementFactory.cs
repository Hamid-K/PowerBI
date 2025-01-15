using System;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200014C RID: 332
	public abstract class ConvertSchemaElementFactory<TSequenceProgramParent, TRegionProgramParent, TRegionParent, TSequenceProgramChild, TRegionProgramChild, TRegionChild> : ConvertSchemaElementFactoryGeneric2<TSequenceProgramParent, TRegionProgramParent, TRegionParent> where TSequenceProgramParent : SequenceExtractionProgram<TSequenceProgramParent, TRegionParent> where TRegionProgramParent : RegionExtractionProgram<TRegionProgramParent, TRegionParent> where TRegionParent : IRegion<TRegionParent> where TSequenceProgramChild : SequenceExtractionProgram<TSequenceProgramChild, TRegionChild> where TRegionProgramChild : RegionExtractionProgram<TRegionProgramChild, TRegionChild> where TRegionChild : IRegion<TRegionChild>
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x00017013 File Offset: 0x00015213
		protected ConvertSchemaElementFactory(string converterName)
			: base(converterName)
		{
		}

		// Token: 0x0600075D RID: 1885
		public abstract TRegionChild ConvertToChildRegion(TRegionParent regionParent);

		// Token: 0x0600075E RID: 1886
		protected internal abstract ExtractionLearner<TSequenceProgramChild, TRegionProgramChild, TRegionChild> GetChildLearner();

		// Token: 0x0600075F RID: 1887 RVA: 0x0001701C File Offset: 0x0001521C
		private ConvertSchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent, TSequenceProgramChild, TRegionProgramChild, TRegionChild> CreateInternal(string name, SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> parent, bool nullable, ExtractionLearner<TSequenceProgramParent, TRegionProgramParent, TRegionParent> learner)
		{
			return new ConvertSchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent, TSequenceProgramChild, TRegionProgramChild, TRegionChild>(this, name, parent, nullable, learner);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00017029 File Offset: 0x00015229
		private SchemaElement<TSequenceProgramChild, TRegionProgramChild, TRegionChild> ParseChid(XElement element, ConvertSchemaElementInterface[] converters = null, ASTSerializationFormat formatPrograms = ASTSerializationFormat.XML, DeserializationContext context = default(DeserializationContext), Func<XElement, ProgramSet> getProgramSet = null)
		{
			return SchemaElement<TSequenceProgramChild, TRegionProgramChild, TRegionChild>.Parse(element, null, this.GetChildLearner(), converters, formatPrograms, context, getProgramSet);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001703E File Offset: 0x0001523E
		public override SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> CreateAndParseChild(string name, SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> parent, bool nullable, ExtractionLearner<TSequenceProgramParent, TRegionProgramParent, TRegionParent> learner, XElement child, ConvertSchemaElementInterface[] converters, ASTSerializationFormat formatPrograms, DeserializationContext context)
		{
			ConvertSchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent, TSequenceProgramChild, TRegionProgramChild, TRegionChild> convertSchemaElement = this.CreateInternal(name, parent, nullable, learner);
			convertSchemaElement.AddChildConverter(this.ParseChid(child, converters, formatPrograms, context, null));
			return convertSchemaElement;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00017060 File Offset: 0x00015260
		public override SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> Create(string name, SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> parent, bool nullable, ExtractionLearner<TSequenceProgramParent, TRegionProgramParent, TRegionParent> learner)
		{
			return this.CreateInternal(name, parent, nullable, learner);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00017070 File Offset: 0x00015270
		public override SchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent> CreateAndParseChildGrammar(string name, XElement childMemberXml, bool nullable, ExtractionLearner<TSequenceProgramParent, TRegionProgramParent, TRegionParent> learner, ConvertSchemaElementInterface[] converters)
		{
			SchemaElement<TSequenceProgramChild, TRegionProgramChild, TRegionChild> schemaElement = SchemaGrammar<TSequenceProgramChild, TRegionProgramChild, TRegionChild>.ParseElement(childMemberXml, this.GetChildLearner(), converters);
			return new ConvertSchemaElement<TSequenceProgramParent, TRegionProgramParent, TRegionParent, TSequenceProgramChild, TRegionProgramChild, TRegionChild>(this, name, nullable, schemaElement, learner);
		}
	}
}
