using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001518 RID: 5400
	public struct outNumber_constNumber : IProgramNodeBuilder, IEquatable<outNumber_constNumber>
	{
		// Token: 0x17001E76 RID: 7798
		// (get) Token: 0x0600AFE7 RID: 45031 RVA: 0x0026E4CA File Offset: 0x0026C6CA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600AFE8 RID: 45032 RVA: 0x0026E4D2 File Offset: 0x0026C6D2
		private outNumber_constNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600AFE9 RID: 45033 RVA: 0x0026E4DB File Offset: 0x0026C6DB
		public static outNumber_constNumber CreateUnsafe(ProgramNode node)
		{
			return new outNumber_constNumber(node);
		}

		// Token: 0x0600AFEA RID: 45034 RVA: 0x0026E4E4 File Offset: 0x0026C6E4
		public static outNumber_constNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.outNumber_constNumber)
			{
				return null;
			}
			return new outNumber_constNumber?(outNumber_constNumber.CreateUnsafe(node));
		}

		// Token: 0x0600AFEB RID: 45035 RVA: 0x0026E519 File Offset: 0x0026C719
		public outNumber_constNumber(GrammarBuilders g, constNumber value0)
		{
			this._node = g.UnnamedConversion.outNumber_constNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600AFEC RID: 45036 RVA: 0x0026E538 File Offset: 0x0026C738
		public static implicit operator outNumber(outNumber_constNumber arg)
		{
			return outNumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E77 RID: 7799
		// (get) Token: 0x0600AFED RID: 45037 RVA: 0x0026E546 File Offset: 0x0026C746
		public constNumber constNumber
		{
			get
			{
				return constNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600AFEE RID: 45038 RVA: 0x0026E55A File Offset: 0x0026C75A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600AFEF RID: 45039 RVA: 0x0026E570 File Offset: 0x0026C770
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600AFF0 RID: 45040 RVA: 0x0026E59A File Offset: 0x0026C79A
		public bool Equals(outNumber_constNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045C6 RID: 17862
		private ProgramNode _node;
	}
}
