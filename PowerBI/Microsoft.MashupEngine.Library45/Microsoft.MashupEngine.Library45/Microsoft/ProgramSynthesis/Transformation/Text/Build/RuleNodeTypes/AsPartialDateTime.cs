using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C11 RID: 7185
	public struct AsPartialDateTime : IProgramNodeBuilder, IEquatable<AsPartialDateTime>
	{
		// Token: 0x1700285C RID: 10332
		// (get) Token: 0x0600F1C3 RID: 61891 RVA: 0x003400DE File Offset: 0x0033E2DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F1C4 RID: 61892 RVA: 0x003400E6 File Offset: 0x0033E2E6
		private AsPartialDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F1C5 RID: 61893 RVA: 0x003400EF File Offset: 0x0033E2EF
		public static AsPartialDateTime CreateUnsafe(ProgramNode node)
		{
			return new AsPartialDateTime(node);
		}

		// Token: 0x0600F1C6 RID: 61894 RVA: 0x003400F8 File Offset: 0x0033E2F8
		public static AsPartialDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.AsPartialDateTime)
			{
				return null;
			}
			return new AsPartialDateTime?(AsPartialDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F1C7 RID: 61895 RVA: 0x0034012D File Offset: 0x0033E32D
		public AsPartialDateTime(GrammarBuilders g, cell value0)
		{
			this._node = g.Rule.AsPartialDateTime.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F1C8 RID: 61896 RVA: 0x0034014C File Offset: 0x0033E34C
		public static implicit operator inputDateTime(AsPartialDateTime arg)
		{
			return inputDateTime.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700285D RID: 10333
		// (get) Token: 0x0600F1C9 RID: 61897 RVA: 0x0034015A File Offset: 0x0033E35A
		public cell cell
		{
			get
			{
				return cell.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F1CA RID: 61898 RVA: 0x0034016E File Offset: 0x0033E36E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F1CB RID: 61899 RVA: 0x00340184 File Offset: 0x0033E384
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F1CC RID: 61900 RVA: 0x003401AE File Offset: 0x0033E3AE
		public bool Equals(AsPartialDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B00 RID: 23296
		private ProgramNode _node;
	}
}
