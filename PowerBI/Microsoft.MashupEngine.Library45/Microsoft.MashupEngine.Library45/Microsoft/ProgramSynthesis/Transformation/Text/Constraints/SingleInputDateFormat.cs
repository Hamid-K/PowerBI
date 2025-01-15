using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DF8 RID: 7672
	public class SingleInputDateFormat : Constraint<IRow, object>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x060100FF RID: 65791 RVA: 0x002DD5BA File Offset: 0x002DB7BA
		private SingleInputDateFormat()
		{
		}

		// Token: 0x17002AA1 RID: 10913
		// (get) Token: 0x06010100 RID: 65792 RVA: 0x00372F6A File Offset: 0x0037116A
		public static SingleInputDateFormat Instance { get; } = new SingleInputDateFormat();

		// Token: 0x06010101 RID: 65793 RVA: 0x00372F71 File Offset: 0x00371171
		public void SetOptions(Witnesses.Options options)
		{
			options.ParseSingleDateFormat = true;
		}

		// Token: 0x06010102 RID: 65794 RVA: 0x00372F7A File Offset: 0x0037117A
		public override bool Equals(Constraint<IRow, object> other)
		{
			return other is SingleInputDateFormat;
		}

		// Token: 0x06010103 RID: 65795 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return false;
		}

		// Token: 0x06010104 RID: 65796 RVA: 0x00372F85 File Offset: 0x00371185
		public override bool Valid(Program<IRow, object> program)
		{
			return program.ProgramNode.AcceptVisitor<IList<DateTimeFormat[]>>(SingleInputDateFormat.InputDateFormatCollector.Instance).All((DateTimeFormat[] inputDtFormats) => inputDtFormats.Length == 1 || inputDtFormats.Any((DateTimeFormat f) => inputDtFormats.All((DateTimeFormat g) => g.FormatString.Contains(f.FormatString))));
		}

		// Token: 0x06010105 RID: 65797 RVA: 0x00372FBB File Offset: 0x003711BB
		public override int GetHashCode()
		{
			return 143489;
		}

		// Token: 0x02001DF9 RID: 7673
		private class InputDateFormatCollector : ProgramNodeVisitor<IList<DateTimeFormat[]>>
		{
			// Token: 0x17002AA2 RID: 10914
			// (get) Token: 0x06010107 RID: 65799 RVA: 0x00372FCE File Offset: 0x003711CE
			public static SingleInputDateFormat.InputDateFormatCollector Instance { get; } = new SingleInputDateFormat.InputDateFormatCollector();

			// Token: 0x06010108 RID: 65800 RVA: 0x00372FD8 File Offset: 0x003711D8
			public override IList<DateTimeFormat[]> VisitNonterminal(NonterminalNode node)
			{
				return (from child in node.Children
					let childRes = child.AcceptVisitor<IList<DateTimeFormat[]>>(this)
					where childRes != null
					from res in childRes
					select res).ToList<DateTimeFormat[]>();
			}

			// Token: 0x06010109 RID: 65801 RVA: 0x00373068 File Offset: 0x00371268
			public override IList<DateTimeFormat[]> VisitLet(LetNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x0601010A RID: 65802 RVA: 0x00373068 File Offset: 0x00371268
			public override IList<DateTimeFormat[]> VisitLambda(LambdaNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x0601010B RID: 65803 RVA: 0x00373074 File Offset: 0x00371274
			public override IList<DateTimeFormat[]> VisitLiteral(LiteralNode node)
			{
				inputDtFormats? inputDtFormats = Language.Build.Node.As.inputDtFormats(node);
				if (inputDtFormats != null)
				{
					return new List<DateTimeFormat[]> { inputDtFormats.Value.Value };
				}
				return null;
			}

			// Token: 0x0601010C RID: 65804 RVA: 0x00002188 File Offset: 0x00000388
			public override IList<DateTimeFormat[]> VisitVariable(VariableNode node)
			{
				return null;
			}

			// Token: 0x0601010D RID: 65805 RVA: 0x00002188 File Offset: 0x00000388
			public override IList<DateTimeFormat[]> VisitHole(Hole node)
			{
				return null;
			}
		}
	}
}
