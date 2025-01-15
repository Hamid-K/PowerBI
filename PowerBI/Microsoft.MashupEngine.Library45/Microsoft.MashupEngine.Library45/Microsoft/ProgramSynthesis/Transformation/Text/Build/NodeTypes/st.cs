using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C3A RID: 7226
	public struct st : IProgramNodeBuilder, IEquatable<st>
	{
		// Token: 0x170028D0 RID: 10448
		// (get) Token: 0x0600F38E RID: 62350 RVA: 0x00342BB2 File Offset: 0x00340DB2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F38F RID: 62351 RVA: 0x00342BBA File Offset: 0x00340DBA
		private st(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F390 RID: 62352 RVA: 0x00342BC3 File Offset: 0x00340DC3
		public static st CreateUnsafe(ProgramNode node)
		{
			return new st(node);
		}

		// Token: 0x0600F391 RID: 62353 RVA: 0x00342BCC File Offset: 0x00340DCC
		public static st? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.st)
			{
				return null;
			}
			return new st?(st.CreateUnsafe(node));
		}

		// Token: 0x0600F392 RID: 62354 RVA: 0x00342C06 File Offset: 0x00340E06
		public static st CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new st(new Hole(g.Symbol.st, holeId));
		}

		// Token: 0x0600F393 RID: 62355 RVA: 0x00342C1E File Offset: 0x00340E1E
		public Transformation Cast_Transformation()
		{
			return Transformation.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F394 RID: 62356 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Transformation(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F395 RID: 62357 RVA: 0x00342C2B File Offset: 0x00340E2B
		public bool Is_Transformation(GrammarBuilders g, out Transformation value)
		{
			value = Transformation.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F396 RID: 62358 RVA: 0x00342C3F File Offset: 0x00340E3F
		public Transformation? As_Transformation(GrammarBuilders g)
		{
			return new Transformation?(Transformation.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F397 RID: 62359 RVA: 0x00342C51 File Offset: 0x00340E51
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F398 RID: 62360 RVA: 0x00342C64 File Offset: 0x00340E64
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F399 RID: 62361 RVA: 0x00342C8E File Offset: 0x00340E8E
		public bool Equals(st other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B29 RID: 23337
		private ProgramNode _node;
	}
}
