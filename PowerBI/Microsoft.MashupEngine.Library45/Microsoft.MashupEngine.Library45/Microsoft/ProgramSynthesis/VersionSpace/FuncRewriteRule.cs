using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x02000295 RID: 661
	public class FuncRewriteRule<TNode, TBuilder> : IFuncRewriteRule where TNode : struct, IProgramNodeBuilder
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x00029CC9 File Offset: 0x00027EC9
		public FuncRewriteRule(TNode source, Func<TNode, IReadOnlyDictionary<Hole, ProgramNode>, TNode> target, Func<TBuilder, ProgramNode, TNode?> createSafe, TBuilder builder)
		{
			this.Source = source;
			this.Target = target;
			this._createSafe = createSafe;
			this._builder = builder;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00029CEE File Offset: 0x00027EEE
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0} -> {1}", new object[] { this.Source, this.Target }));
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00029D1C File Offset: 0x00027F1C
		public TNode Source { get; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00029D24 File Offset: 0x00027F24
		public Func<TNode, IReadOnlyDictionary<Hole, ProgramNode>, TNode> Target { get; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00029D2C File Offset: 0x00027F2C
		ProgramNode IFuncRewriteRule.Source
		{
			get
			{
				TNode source = this.Source;
				return source.Node;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00029D4D File Offset: 0x00027F4D
		Func<ProgramNode, IReadOnlyDictionary<Hole, ProgramNode>, ProgramNode> IFuncRewriteRule.Target
		{
			get
			{
				return delegate(ProgramNode node, IReadOnlyDictionary<Hole, ProgramNode> map)
				{
					TNode tnode = this.Target(this._createSafe(this._builder, node).Value, map);
					return tnode.Node;
				};
			}
		}

		// Token: 0x040006EE RID: 1774
		private Func<TBuilder, ProgramNode, TNode?> _createSafe;

		// Token: 0x040006EF RID: 1775
		private TBuilder _builder;
	}
}
