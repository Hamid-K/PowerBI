using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E14 RID: 3604
	public struct aboveOrOutput_aboveOrHeader : IProgramNodeBuilder, IEquatable<aboveOrOutput_aboveOrHeader>
	{
		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06006008 RID: 24584 RVA: 0x0013D1C2 File Offset: 0x0013B3C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006009 RID: 24585 RVA: 0x0013D1CA File Offset: 0x0013B3CA
		private aboveOrOutput_aboveOrHeader(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600600A RID: 24586 RVA: 0x0013D1D3 File Offset: 0x0013B3D3
		public static aboveOrOutput_aboveOrHeader CreateUnsafe(ProgramNode node)
		{
			return new aboveOrOutput_aboveOrHeader(node);
		}

		// Token: 0x0600600B RID: 24587 RVA: 0x0013D1DC File Offset: 0x0013B3DC
		public static aboveOrOutput_aboveOrHeader? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.aboveOrOutput_aboveOrHeader)
			{
				return null;
			}
			return new aboveOrOutput_aboveOrHeader?(aboveOrOutput_aboveOrHeader.CreateUnsafe(node));
		}

		// Token: 0x0600600C RID: 24588 RVA: 0x0013D211 File Offset: 0x0013B411
		public aboveOrOutput_aboveOrHeader(GrammarBuilders g, aboveOrHeader value0)
		{
			this._node = g.UnnamedConversion.aboveOrOutput_aboveOrHeader.BuildASTNode(value0.Node);
		}

		// Token: 0x0600600D RID: 24589 RVA: 0x0013D230 File Offset: 0x0013B430
		public static implicit operator aboveOrOutput(aboveOrOutput_aboveOrHeader arg)
		{
			return aboveOrOutput.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x0600600E RID: 24590 RVA: 0x0013D23E File Offset: 0x0013B43E
		public aboveOrHeader aboveOrHeader
		{
			get
			{
				return aboveOrHeader.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600600F RID: 24591 RVA: 0x0013D252 File Offset: 0x0013B452
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x0013D268 File Offset: 0x0013B468
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x0013D292 File Offset: 0x0013B492
		public bool Equals(aboveOrOutput_aboveOrHeader other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BBE RID: 11198
		private ProgramNode _node;
	}
}
