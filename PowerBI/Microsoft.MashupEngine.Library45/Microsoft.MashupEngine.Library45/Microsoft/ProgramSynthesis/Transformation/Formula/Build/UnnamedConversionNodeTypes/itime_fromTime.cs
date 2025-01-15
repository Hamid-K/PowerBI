using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200153F RID: 5439
	public struct itime_fromTime : IProgramNodeBuilder, IEquatable<itime_fromTime>
	{
		// Token: 0x17001EC4 RID: 7876
		// (get) Token: 0x0600B16D RID: 45421 RVA: 0x00270786 File Offset: 0x0026E986
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B16E RID: 45422 RVA: 0x0027078E File Offset: 0x0026E98E
		private itime_fromTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B16F RID: 45423 RVA: 0x00270797 File Offset: 0x0026E997
		public static itime_fromTime CreateUnsafe(ProgramNode node)
		{
			return new itime_fromTime(node);
		}

		// Token: 0x0600B170 RID: 45424 RVA: 0x002707A0 File Offset: 0x0026E9A0
		public static itime_fromTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.itime_fromTime)
			{
				return null;
			}
			return new itime_fromTime?(itime_fromTime.CreateUnsafe(node));
		}

		// Token: 0x0600B171 RID: 45425 RVA: 0x002707D5 File Offset: 0x0026E9D5
		public itime_fromTime(GrammarBuilders g, fromTime value0)
		{
			this._node = g.UnnamedConversion.itime_fromTime.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B172 RID: 45426 RVA: 0x002707F4 File Offset: 0x0026E9F4
		public static implicit operator itime(itime_fromTime arg)
		{
			return itime.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EC5 RID: 7877
		// (get) Token: 0x0600B173 RID: 45427 RVA: 0x00270802 File Offset: 0x0026EA02
		public fromTime fromTime
		{
			get
			{
				return fromTime.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B174 RID: 45428 RVA: 0x00270816 File Offset: 0x0026EA16
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B175 RID: 45429 RVA: 0x0027082C File Offset: 0x0026EA2C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B176 RID: 45430 RVA: 0x00270856 File Offset: 0x0026EA56
		public bool Equals(itime_fromTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045ED RID: 17901
		private ProgramNode _node;
	}
}
