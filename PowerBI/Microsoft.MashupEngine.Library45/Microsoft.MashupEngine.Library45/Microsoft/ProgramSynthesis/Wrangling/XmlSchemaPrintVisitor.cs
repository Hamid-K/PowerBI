using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000D8 RID: 216
	public class XmlSchemaPrintVisitor<TSequenceProgram, TRegionProgram, TRegion> : SchemaPrintVisitor<TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x00010625 File Offset: 0x0000E825
		protected override string GetProgramRepresentation(ProgramNode programNode)
		{
			return programNode.PrintAST(ASTSerializationFormat.XML);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001062E File Offset: 0x0000E82E
		protected override SchemaPrintVisitor<TSequenceProgramChild, TRegionProgramChild, TRegionChild> GetConvertChildVisitor<TSequenceProgramChild, TRegionProgramChild, TRegionChild>()
		{
			return new XmlSchemaPrintVisitor<TSequenceProgramChild, TRegionProgramChild, TRegionChild>();
		}
	}
}
