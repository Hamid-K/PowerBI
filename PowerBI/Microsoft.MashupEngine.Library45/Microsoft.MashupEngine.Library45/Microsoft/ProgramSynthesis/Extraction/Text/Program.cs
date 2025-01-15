using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Extraction.Text.Translation;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Text
{
	// Token: 0x02000EFB RID: 3835
	public class Program : TransformationProgram<Program, StringRegion, ITable<StringRegion>>
	{
		// Token: 0x0600684B RID: 26699 RVA: 0x00153DA7 File Offset: 0x00151FA7
		internal Program(ProgramNode node)
			: base(node, node.GetFeatureValue<double>(Learner.Instance.ScoreFeature, null), null)
		{
		}

		// Token: 0x0600684C RID: 26700 RVA: 0x00153DC2 File Offset: 0x00151FC2
		public override ITable<StringRegion> Run(StringRegion input)
		{
			return base.ProgramNode.Invoke(State.CreateForExecution(Language.Grammar.InputSymbol, input)) as ITable<StringRegion>;
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x00153DE4 File Offset: 0x00151FE4
		public ITable<string> Run(string input)
		{
			string text = Example.NormalizeNewLines(input);
			ITable<StringRegion> table = this.Run(Semantics.CreateStringRegion(text));
			if (table == null)
			{
				return null;
			}
			return new Table<string>(table.ColumnNames, table.Select((IEnumerable<StringRegion> row) => row.Select(delegate(StringRegion cell)
			{
				if (cell == null)
				{
					return null;
				}
				return cell.Value;
			}).ToList<string>()).ToList<List<string>>(), null);
		}

		// Token: 0x0600684E RID: 26702 RVA: 0x00153E40 File Offset: 0x00152040
		public string GetMCode(string binaryContent, string encoding, Microsoft.ProgramSynthesis.Extraction.Text.Translation.ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escape)
		{
			return PowerQueryMGenerator.Generate(base.ProgramNode, binaryContent, encoding, localizedStrings, escape);
		}

		// Token: 0x0600684F RID: 26703 RVA: 0x00153E52 File Offset: 0x00152052
		public string GetPythonCode(string s, bool readFile = true)
		{
			return PythonGenerator.Generate(base.ProgramNode, s, readFile);
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06006850 RID: 26704 RVA: 0x00153E64 File Offset: 0x00152064
		public IReadOnlyList<string> ColumnNames
		{
			get
			{
				if (Table.CreateSafe(GrammarBuilders.Instance(Language.Grammar), base.ProgramNode) == null)
				{
					return null;
				}
				Table? table;
				return table.GetValueOrDefault().columnNames.Value;
			}
		}

		// Token: 0x06006851 RID: 26705 RVA: 0x00153EA9 File Offset: 0x001520A9
		public string Serialize()
		{
			return base.ProgramNode.PrintAST(ASTSerializationFormat.XML);
		}

		// Token: 0x06006852 RID: 26706 RVA: 0x00153EB7 File Offset: 0x001520B7
		public bool Equals(Program other)
		{
			return other != null && (this == other || object.Equals(base.ProgramNode, other.ProgramNode));
		}

		// Token: 0x06006853 RID: 26707 RVA: 0x00153ED5 File Offset: 0x001520D5
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Program)obj)));
		}

		// Token: 0x06006854 RID: 26708 RVA: 0x00153F03 File Offset: 0x00152103
		public override int GetHashCode()
		{
			ProgramNode programNode = base.ProgramNode;
			if (programNode == null)
			{
				return 0;
			}
			return programNode.GetHashCode();
		}

		// Token: 0x06006855 RID: 26709 RVA: 0x00153F16 File Offset: 0x00152116
		public override string ToString()
		{
			return base.ProgramNode.ToString();
		}
	}
}
