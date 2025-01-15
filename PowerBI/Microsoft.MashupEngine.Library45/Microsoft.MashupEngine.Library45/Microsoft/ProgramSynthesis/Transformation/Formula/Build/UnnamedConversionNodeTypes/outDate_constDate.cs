using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200151A RID: 5402
	public struct outDate_constDate : IProgramNodeBuilder, IEquatable<outDate_constDate>
	{
		// Token: 0x17001E7A RID: 7802
		// (get) Token: 0x0600AFFB RID: 45051 RVA: 0x0026E692 File Offset: 0x0026C892
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600AFFC RID: 45052 RVA: 0x0026E69A File Offset: 0x0026C89A
		private outDate_constDate(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600AFFD RID: 45053 RVA: 0x0026E6A3 File Offset: 0x0026C8A3
		public static outDate_constDate CreateUnsafe(ProgramNode node)
		{
			return new outDate_constDate(node);
		}

		// Token: 0x0600AFFE RID: 45054 RVA: 0x0026E6AC File Offset: 0x0026C8AC
		public static outDate_constDate? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.outDate_constDate)
			{
				return null;
			}
			return new outDate_constDate?(outDate_constDate.CreateUnsafe(node));
		}

		// Token: 0x0600AFFF RID: 45055 RVA: 0x0026E6E1 File Offset: 0x0026C8E1
		public outDate_constDate(GrammarBuilders g, constDate value0)
		{
			this._node = g.UnnamedConversion.outDate_constDate.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B000 RID: 45056 RVA: 0x0026E700 File Offset: 0x0026C900
		public static implicit operator outDate(outDate_constDate arg)
		{
			return outDate.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E7B RID: 7803
		// (get) Token: 0x0600B001 RID: 45057 RVA: 0x0026E70E File Offset: 0x0026C90E
		public constDate constDate
		{
			get
			{
				return constDate.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B002 RID: 45058 RVA: 0x0026E722 File Offset: 0x0026C922
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B003 RID: 45059 RVA: 0x0026E738 File Offset: 0x0026C938
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B004 RID: 45060 RVA: 0x0026E762 File Offset: 0x0026C962
		public bool Equals(outDate_constDate other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045C8 RID: 17864
		private ProgramNode _node;
	}
}
