using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001516 RID: 5398
	public struct result_inull : IProgramNodeBuilder, IEquatable<result_inull>
	{
		// Token: 0x17001E72 RID: 7794
		// (get) Token: 0x0600AFD3 RID: 45011 RVA: 0x0026E302 File Offset: 0x0026C502
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600AFD4 RID: 45012 RVA: 0x0026E30A File Offset: 0x0026C50A
		private result_inull(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600AFD5 RID: 45013 RVA: 0x0026E313 File Offset: 0x0026C513
		public static result_inull CreateUnsafe(ProgramNode node)
		{
			return new result_inull(node);
		}

		// Token: 0x0600AFD6 RID: 45014 RVA: 0x0026E31C File Offset: 0x0026C51C
		public static result_inull? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.result_inull)
			{
				return null;
			}
			return new result_inull?(result_inull.CreateUnsafe(node));
		}

		// Token: 0x0600AFD7 RID: 45015 RVA: 0x0026E351 File Offset: 0x0026C551
		public result_inull(GrammarBuilders g, inull value0)
		{
			this._node = g.UnnamedConversion.result_inull.BuildASTNode(value0.Node);
		}

		// Token: 0x0600AFD8 RID: 45016 RVA: 0x0026E370 File Offset: 0x0026C570
		public static implicit operator result(result_inull arg)
		{
			return result.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E73 RID: 7795
		// (get) Token: 0x0600AFD9 RID: 45017 RVA: 0x0026E37E File Offset: 0x0026C57E
		public inull inull
		{
			get
			{
				return inull.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600AFDA RID: 45018 RVA: 0x0026E392 File Offset: 0x0026C592
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600AFDB RID: 45019 RVA: 0x0026E3A8 File Offset: 0x0026C5A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600AFDC RID: 45020 RVA: 0x0026E3D2 File Offset: 0x0026C5D2
		public bool Equals(result_inull other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045C4 RID: 17860
		private ProgramNode _node;
	}
}
