using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001532 RID: 5426
	public struct number_rowNumberTransform : IProgramNodeBuilder, IEquatable<number_rowNumberTransform>
	{
		// Token: 0x17001EAA RID: 7850
		// (get) Token: 0x0600B0EB RID: 45291 RVA: 0x0026FBF2 File Offset: 0x0026DDF2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B0EC RID: 45292 RVA: 0x0026FBFA File Offset: 0x0026DDFA
		private number_rowNumberTransform(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B0ED RID: 45293 RVA: 0x0026FC03 File Offset: 0x0026DE03
		public static number_rowNumberTransform CreateUnsafe(ProgramNode node)
		{
			return new number_rowNumberTransform(node);
		}

		// Token: 0x0600B0EE RID: 45294 RVA: 0x0026FC0C File Offset: 0x0026DE0C
		public static number_rowNumberTransform? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.number_rowNumberTransform)
			{
				return null;
			}
			return new number_rowNumberTransform?(number_rowNumberTransform.CreateUnsafe(node));
		}

		// Token: 0x0600B0EF RID: 45295 RVA: 0x0026FC41 File Offset: 0x0026DE41
		public number_rowNumberTransform(GrammarBuilders g, rowNumberTransform value0)
		{
			this._node = g.UnnamedConversion.number_rowNumberTransform.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B0F0 RID: 45296 RVA: 0x0026FC60 File Offset: 0x0026DE60
		public static implicit operator number(number_rowNumberTransform arg)
		{
			return number.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EAB RID: 7851
		// (get) Token: 0x0600B0F1 RID: 45297 RVA: 0x0026FC6E File Offset: 0x0026DE6E
		public rowNumberTransform rowNumberTransform
		{
			get
			{
				return rowNumberTransform.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B0F2 RID: 45298 RVA: 0x0026FC82 File Offset: 0x0026DE82
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B0F3 RID: 45299 RVA: 0x0026FC98 File Offset: 0x0026DE98
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B0F4 RID: 45300 RVA: 0x0026FCC2 File Offset: 0x0026DEC2
		public bool Equals(number_rowNumberTransform other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045E0 RID: 17888
		private ProgramNode _node;
	}
}
