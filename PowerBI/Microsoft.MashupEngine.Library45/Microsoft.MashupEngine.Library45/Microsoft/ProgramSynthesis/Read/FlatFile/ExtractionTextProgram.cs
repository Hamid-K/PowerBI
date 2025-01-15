using System;
using System.IO;
using Microsoft.ProgramSynthesis.Extraction.Text;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile
{
	// Token: 0x02001253 RID: 4691
	public class ExtractionTextProgram : Program
	{
		// Token: 0x06008D1A RID: 36122 RVA: 0x001DA517 File Offset: 0x001D8717
		internal ExtractionTextProgram(Program wrappedProgram)
			: base(ExtractionTextProgram.BuildProgram(output.CreateUnsafe(wrappedProgram.ProgramNode)), wrappedProgram.ColumnNames, wrappedProgram.ColumnNames)
		{
			this.WrappedProgram = wrappedProgram;
		}

		// Token: 0x06008D1B RID: 36123 RVA: 0x001DA542 File Offset: 0x001D8742
		internal ExtractionTextProgram(output output)
			: this(Loader.Instance.Create(output.Node))
		{
		}

		// Token: 0x06008D1C RID: 36124 RVA: 0x001DA55C File Offset: 0x001D875C
		private static readFlatFile BuildProgram(output output)
		{
			return Program.Builder.Node.Rule.StringRegionToStringTable(Program.Builder.Node.Rule.LetEText(Program.Builder.Node.Rule.CreateStringRegion(Program.Builder.Node.Variable.file), Program.Builder.Node.Rule.ETextOutput(output)));
		}

		// Token: 0x06008D1D RID: 36125 RVA: 0x001DA5D0 File Offset: 0x001D87D0
		internal ExtractionTextProgram(eText eText)
			: this(eText.Cast_LetEText()._LetB1.Cast_ETextOutput().output)
		{
		}

		// Token: 0x06008D1E RID: 36126 RVA: 0x001DA602 File Offset: 0x001D8802
		internal ExtractionTextProgram(StringRegionToStringTable node)
			: this(node.eText)
		{
		}

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x06008D1F RID: 36127 RVA: 0x001DA611 File Offset: 0x001D8811
		public Program WrappedProgram { get; }

		// Token: 0x06008D20 RID: 36128 RVA: 0x001DA619 File Offset: 0x001D8819
		public override string GetMCode(string binaryContent, string encoding, Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM.ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escape)
		{
			return this.WrappedProgram.GetMCode(binaryContent, encoding, localizedStrings, escape);
		}

		// Token: 0x06008D21 RID: 36129 RVA: 0x001DA62B File Offset: 0x001D882B
		protected override ITable<string> RunInternal(string input, bool trim)
		{
			return this.WrappedProgram.Run(input);
		}

		// Token: 0x06008D22 RID: 36130 RVA: 0x001DA639 File Offset: 0x001D8839
		protected override ITable<string> RunInternal(TextReader input, bool trim)
		{
			return this.WrappedProgram.Run(input.ReadToEnd());
		}
	}
}
