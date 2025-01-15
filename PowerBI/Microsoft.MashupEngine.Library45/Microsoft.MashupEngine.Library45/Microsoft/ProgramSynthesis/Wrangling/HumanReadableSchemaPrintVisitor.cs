using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000A8 RID: 168
	public class HumanReadableSchemaPrintVisitor<TSequenceProgram, TRegionProgram, TRegion> : SchemaPrintVisitor<TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000DF85 File Offset: 0x0000C185
		protected override string GetProgramRepresentation(ProgramNode programNode)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("<![CDATA[{0}]]>", new object[] { programNode.PrintAST(ASTSerializationFormat.HumanReadable) }));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000DFA6 File Offset: 0x0000C1A6
		protected override SchemaPrintVisitor<TSequenceProgramChild, TRegionProgramChild, TRegionChild> GetConvertChildVisitor<TSequenceProgramChild, TRegionProgramChild, TRegionChild>()
		{
			return new HumanReadableSchemaPrintVisitor<TSequenceProgramChild, TRegionProgramChild, TRegionChild>();
		}
	}
}
