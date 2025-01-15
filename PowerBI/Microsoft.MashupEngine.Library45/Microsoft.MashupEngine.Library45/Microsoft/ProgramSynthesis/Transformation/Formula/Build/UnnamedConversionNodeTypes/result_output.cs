using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001515 RID: 5397
	public struct result_output : IProgramNodeBuilder, IEquatable<result_output>
	{
		// Token: 0x17001E70 RID: 7792
		// (get) Token: 0x0600AFC9 RID: 45001 RVA: 0x0026E21D File Offset: 0x0026C41D
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600AFCA RID: 45002 RVA: 0x0026E225 File Offset: 0x0026C425
		private result_output(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600AFCB RID: 45003 RVA: 0x0026E22E File Offset: 0x0026C42E
		public static result_output CreateUnsafe(ProgramNode node)
		{
			return new result_output(node);
		}

		// Token: 0x0600AFCC RID: 45004 RVA: 0x0026E238 File Offset: 0x0026C438
		public static result_output? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.result_output)
			{
				return null;
			}
			return new result_output?(result_output.CreateUnsafe(node));
		}

		// Token: 0x0600AFCD RID: 45005 RVA: 0x0026E26D File Offset: 0x0026C46D
		public result_output(GrammarBuilders g, output value0)
		{
			this._node = g.UnnamedConversion.result_output.BuildASTNode(value0.Node);
		}

		// Token: 0x0600AFCE RID: 45006 RVA: 0x0026E28C File Offset: 0x0026C48C
		public static implicit operator result(result_output arg)
		{
			return result.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E71 RID: 7793
		// (get) Token: 0x0600AFCF RID: 45007 RVA: 0x0026E29A File Offset: 0x0026C49A
		public output output
		{
			get
			{
				return output.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600AFD0 RID: 45008 RVA: 0x0026E2AE File Offset: 0x0026C4AE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600AFD1 RID: 45009 RVA: 0x0026E2C4 File Offset: 0x0026C4C4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600AFD2 RID: 45010 RVA: 0x0026E2EE File Offset: 0x0026C4EE
		public bool Equals(result_output other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045C3 RID: 17859
		private ProgramNode _node;
	}
}
