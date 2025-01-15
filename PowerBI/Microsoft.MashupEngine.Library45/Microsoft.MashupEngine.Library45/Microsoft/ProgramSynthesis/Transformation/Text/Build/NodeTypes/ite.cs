using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C38 RID: 7224
	public struct ite : IProgramNodeBuilder, IEquatable<ite>
	{
		// Token: 0x170028CE RID: 10446
		// (get) Token: 0x0600F376 RID: 62326 RVA: 0x003429D2 File Offset: 0x00340BD2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F377 RID: 62327 RVA: 0x003429DA File Offset: 0x00340BDA
		private ite(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F378 RID: 62328 RVA: 0x003429E3 File Offset: 0x00340BE3
		public static ite CreateUnsafe(ProgramNode node)
		{
			return new ite(node);
		}

		// Token: 0x0600F379 RID: 62329 RVA: 0x003429EC File Offset: 0x00340BEC
		public static ite? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.ite)
			{
				return null;
			}
			return new ite?(ite.CreateUnsafe(node));
		}

		// Token: 0x0600F37A RID: 62330 RVA: 0x00342A26 File Offset: 0x00340C26
		public static ite CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new ite(new Hole(g.Symbol.ite, holeId));
		}

		// Token: 0x0600F37B RID: 62331 RVA: 0x00342A3E File Offset: 0x00340C3E
		public IfThenElse Cast_IfThenElse()
		{
			return IfThenElse.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F37C RID: 62332 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_IfThenElse(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F37D RID: 62333 RVA: 0x00342A4B File Offset: 0x00340C4B
		public bool Is_IfThenElse(GrammarBuilders g, out IfThenElse value)
		{
			value = IfThenElse.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F37E RID: 62334 RVA: 0x00342A5F File Offset: 0x00340C5F
		public IfThenElse? As_IfThenElse(GrammarBuilders g)
		{
			return new IfThenElse?(IfThenElse.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F37F RID: 62335 RVA: 0x00342A71 File Offset: 0x00340C71
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F380 RID: 62336 RVA: 0x00342A84 File Offset: 0x00340C84
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F381 RID: 62337 RVA: 0x00342AAE File Offset: 0x00340CAE
		public bool Equals(ite other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B27 RID: 23335
		private ProgramNode _node;
	}
}
