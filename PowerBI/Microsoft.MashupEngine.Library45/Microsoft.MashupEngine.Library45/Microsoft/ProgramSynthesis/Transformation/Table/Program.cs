using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table
{
	// Token: 0x02001A81 RID: 6785
	public class Program : Program<ITable<object>, ITable<object>>, IEquatable<Program>
	{
		// Token: 0x0600DF47 RID: 57159 RVA: 0x002F5FD6 File Offset: 0x002F41D6
		internal Program(@out program, double score)
			: base(program.Node, score, null)
		{
			this.TypedProgram = program;
		}

		// Token: 0x17002541 RID: 9537
		// (get) Token: 0x0600DF48 RID: 57160 RVA: 0x002F5FEE File Offset: 0x002F41EE
		public bool IsNonDestructiveTransformation
		{
			get
			{
				return this.Operations.All((table op) => op.Is_LabelEncode(Program.Builder) || op.Is_AddSplitColumns(Program.Builder));
			}
		}

		// Token: 0x17002542 RID: 9538
		// (get) Token: 0x0600DF49 RID: 57161 RVA: 0x002F601A File Offset: 0x002F421A
		public @out TypedProgram { get; }

		// Token: 0x0600DF4A RID: 57162 RVA: 0x002F6022 File Offset: 0x002F4222
		public sealed override ITable<object> Run(ITable<object> input)
		{
			return base.ProgramNode.Invoke(State.CreateForExecution(Language.Grammar.InputSymbol, input)) as ITable<object>;
		}

		// Token: 0x17002543 RID: 9539
		// (get) Token: 0x0600DF4B RID: 57163 RVA: 0x002F6044 File Offset: 0x002F4244
		internal IReadOnlyList<table> Operations
		{
			get
			{
				TTableProgram? ttableProgram;
				table? table = ((Language.Build.Node.AsRule.TTableProgram(base.ProgramNode) != null) ? new table?(ttableProgram.GetValueOrDefault().table) : null);
				List<table> list = new List<table>();
				while (table != null && !table.Value.Is_table_inputTable(Program.Builder))
				{
					list.Add(table.Value);
					table = Program.Builder.Node.As.table(table.Value.Node.Children[0]);
				}
				list.Reverse();
				return list;
			}
		}

		// Token: 0x0600DF4C RID: 57164 RVA: 0x002F60FE File Offset: 0x002F42FE
		public bool Equals(Program other)
		{
			return base.ProgramNode.Equals(other.ProgramNode);
		}

		// Token: 0x0600DF4D RID: 57165 RVA: 0x002F6111 File Offset: 0x002F4311
		public override int GetHashCode()
		{
			return base.ProgramNode.GetHashCode();
		}

		// Token: 0x040054BB RID: 21691
		public const string DSLName = "Transformation.Table";

		// Token: 0x040054BC RID: 21692
		private static readonly GrammarBuilders Builder = GrammarBuilders.Instance(Language.Grammar);
	}
}
