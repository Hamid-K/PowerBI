using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001517 RID: 5399
	public struct outNumber_number : IProgramNodeBuilder, IEquatable<outNumber_number>
	{
		// Token: 0x17001E74 RID: 7796
		// (get) Token: 0x0600AFDD RID: 45021 RVA: 0x0026E3E6 File Offset: 0x0026C5E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600AFDE RID: 45022 RVA: 0x0026E3EE File Offset: 0x0026C5EE
		private outNumber_number(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600AFDF RID: 45023 RVA: 0x0026E3F7 File Offset: 0x0026C5F7
		public static outNumber_number CreateUnsafe(ProgramNode node)
		{
			return new outNumber_number(node);
		}

		// Token: 0x0600AFE0 RID: 45024 RVA: 0x0026E400 File Offset: 0x0026C600
		public static outNumber_number? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.outNumber_number)
			{
				return null;
			}
			return new outNumber_number?(outNumber_number.CreateUnsafe(node));
		}

		// Token: 0x0600AFE1 RID: 45025 RVA: 0x0026E435 File Offset: 0x0026C635
		public outNumber_number(GrammarBuilders g, number value0)
		{
			this._node = g.UnnamedConversion.outNumber_number.BuildASTNode(value0.Node);
		}

		// Token: 0x0600AFE2 RID: 45026 RVA: 0x0026E454 File Offset: 0x0026C654
		public static implicit operator outNumber(outNumber_number arg)
		{
			return outNumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E75 RID: 7797
		// (get) Token: 0x0600AFE3 RID: 45027 RVA: 0x0026E462 File Offset: 0x0026C662
		public number number
		{
			get
			{
				return number.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600AFE4 RID: 45028 RVA: 0x0026E476 File Offset: 0x0026C676
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600AFE5 RID: 45029 RVA: 0x0026E48C File Offset: 0x0026C68C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600AFE6 RID: 45030 RVA: 0x0026E4B6 File Offset: 0x0026C6B6
		public bool Equals(outNumber_number other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045C5 RID: 17861
		private ProgramNode _node;
	}
}
