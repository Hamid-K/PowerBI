using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C39 RID: 7225
	public struct pred : IProgramNodeBuilder, IEquatable<pred>
	{
		// Token: 0x170028CF RID: 10447
		// (get) Token: 0x0600F382 RID: 62338 RVA: 0x00342AC2 File Offset: 0x00340CC2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F383 RID: 62339 RVA: 0x00342ACA File Offset: 0x00340CCA
		private pred(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F384 RID: 62340 RVA: 0x00342AD3 File Offset: 0x00340CD3
		public static pred CreateUnsafe(ProgramNode node)
		{
			return new pred(node);
		}

		// Token: 0x0600F385 RID: 62341 RVA: 0x00342ADC File Offset: 0x00340CDC
		public static pred? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pred)
			{
				return null;
			}
			return new pred?(pred.CreateUnsafe(node));
		}

		// Token: 0x0600F386 RID: 62342 RVA: 0x00342B16 File Offset: 0x00340D16
		public static pred CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pred(new Hole(g.Symbol.pred, holeId));
		}

		// Token: 0x0600F387 RID: 62343 RVA: 0x00342B2E File Offset: 0x00340D2E
		public Predicate Cast_Predicate()
		{
			return Predicate.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F388 RID: 62344 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Predicate(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F389 RID: 62345 RVA: 0x00342B3B File Offset: 0x00340D3B
		public bool Is_Predicate(GrammarBuilders g, out Predicate value)
		{
			value = Predicate.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F38A RID: 62346 RVA: 0x00342B4F File Offset: 0x00340D4F
		public Predicate? As_Predicate(GrammarBuilders g)
		{
			return new Predicate?(Predicate.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F38B RID: 62347 RVA: 0x00342B61 File Offset: 0x00340D61
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F38C RID: 62348 RVA: 0x00342B74 File Offset: 0x00340D74
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F38D RID: 62349 RVA: 0x00342B9E File Offset: 0x00340D9E
		public bool Equals(pred other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B28 RID: 23336
		private ProgramNode _node;
	}
}
