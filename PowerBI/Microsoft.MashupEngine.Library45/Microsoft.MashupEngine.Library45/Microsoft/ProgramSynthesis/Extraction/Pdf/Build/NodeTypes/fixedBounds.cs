using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C03 RID: 3075
	public struct fixedBounds : IProgramNodeBuilder, IEquatable<fixedBounds>
	{
		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06004F5B RID: 20315 RVA: 0x000FAA0A File Offset: 0x000F8C0A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x000FAA12 File Offset: 0x000F8C12
		private fixedBounds(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x000FAA1B File Offset: 0x000F8C1B
		public static fixedBounds CreateUnsafe(ProgramNode node)
		{
			return new fixedBounds(node);
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x000FAA24 File Offset: 0x000F8C24
		public static fixedBounds? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fixedBounds)
			{
				return null;
			}
			return new fixedBounds?(fixedBounds.CreateUnsafe(node));
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x000FAA5E File Offset: 0x000F8C5E
		public static fixedBounds CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fixedBounds(new Hole(g.Symbol.fixedBounds, holeId));
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x000FAA76 File Offset: 0x000F8C76
		public fixedBounds_tableIdentifier Cast_fixedBounds_tableIdentifier()
		{
			return fixedBounds_tableIdentifier.CreateUnsafe(this.Node);
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_fixedBounds_tableIdentifier(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x000FAA83 File Offset: 0x000F8C83
		public bool Is_fixedBounds_tableIdentifier(GrammarBuilders g, out fixedBounds_tableIdentifier value)
		{
			value = fixedBounds_tableIdentifier.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x000FAA97 File Offset: 0x000F8C97
		public fixedBounds_tableIdentifier? As_fixedBounds_tableIdentifier(GrammarBuilders g)
		{
			return new fixedBounds_tableIdentifier?(fixedBounds_tableIdentifier.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x000FAAA9 File Offset: 0x000F8CA9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x000FAABC File Offset: 0x000F8CBC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x000FAAE6 File Offset: 0x000F8CE6
		public bool Equals(fixedBounds other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400232B RID: 9003
		private ProgramNode _node;
	}
}
