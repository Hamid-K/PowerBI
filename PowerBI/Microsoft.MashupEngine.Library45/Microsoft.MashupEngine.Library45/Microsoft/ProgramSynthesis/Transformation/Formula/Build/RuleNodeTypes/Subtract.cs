using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200156C RID: 5484
	public struct Subtract : IProgramNodeBuilder, IEquatable<Subtract>
	{
		// Token: 0x17001F40 RID: 8000
		// (get) Token: 0x0600B351 RID: 45905 RVA: 0x0027333A File Offset: 0x0027153A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B352 RID: 45906 RVA: 0x00273342 File Offset: 0x00271542
		private Subtract(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B353 RID: 45907 RVA: 0x0027334B File Offset: 0x0027154B
		public static Subtract CreateUnsafe(ProgramNode node)
		{
			return new Subtract(node);
		}

		// Token: 0x0600B354 RID: 45908 RVA: 0x00273354 File Offset: 0x00271554
		public static Subtract? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Subtract)
			{
				return null;
			}
			return new Subtract?(Subtract.CreateUnsafe(node));
		}

		// Token: 0x0600B355 RID: 45909 RVA: 0x00273389 File Offset: 0x00271589
		public Subtract(GrammarBuilders g, arithmeticLeft value0, subtractRight value1)
		{
			this._node = g.Rule.Subtract.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B356 RID: 45910 RVA: 0x002733AF File Offset: 0x002715AF
		public static implicit operator arithmetic(Subtract arg)
		{
			return arithmetic.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F41 RID: 8001
		// (get) Token: 0x0600B357 RID: 45911 RVA: 0x002733BD File Offset: 0x002715BD
		public arithmeticLeft arithmeticLeft
		{
			get
			{
				return arithmeticLeft.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F42 RID: 8002
		// (get) Token: 0x0600B358 RID: 45912 RVA: 0x002733D1 File Offset: 0x002715D1
		public subtractRight subtractRight
		{
			get
			{
				return subtractRight.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B359 RID: 45913 RVA: 0x002733E5 File Offset: 0x002715E5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B35A RID: 45914 RVA: 0x002733F8 File Offset: 0x002715F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B35B RID: 45915 RVA: 0x00273422 File Offset: 0x00271622
		public bool Equals(Subtract other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400461A RID: 17946
		private ProgramNode _node;
	}
}
