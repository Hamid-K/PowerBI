using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x0200159B RID: 5531
	public struct outNumber : IProgramNodeBuilder, IEquatable<outNumber>
	{
		// Token: 0x17001FC1 RID: 8129
		// (get) Token: 0x0600B56C RID: 46444 RVA: 0x002769B2 File Offset: 0x00274BB2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B56D RID: 46445 RVA: 0x002769BA File Offset: 0x00274BBA
		private outNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B56E RID: 46446 RVA: 0x002769C3 File Offset: 0x00274BC3
		public static outNumber CreateUnsafe(ProgramNode node)
		{
			return new outNumber(node);
		}

		// Token: 0x0600B56F RID: 46447 RVA: 0x002769CC File Offset: 0x00274BCC
		public static outNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.outNumber)
			{
				return null;
			}
			return new outNumber?(outNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B570 RID: 46448 RVA: 0x00276A06 File Offset: 0x00274C06
		public static outNumber CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new outNumber(new Hole(g.Symbol.outNumber, holeId));
		}

		// Token: 0x0600B571 RID: 46449 RVA: 0x00276A1E File Offset: 0x00274C1E
		public bool Is_outNumber_number(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.outNumber_number;
		}

		// Token: 0x0600B572 RID: 46450 RVA: 0x00276A38 File Offset: 0x00274C38
		public bool Is_outNumber_number(GrammarBuilders g, out outNumber_number value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outNumber_number)
			{
				value = outNumber_number.CreateUnsafe(this.Node);
				return true;
			}
			value = default(outNumber_number);
			return false;
		}

		// Token: 0x0600B573 RID: 46451 RVA: 0x00276A70 File Offset: 0x00274C70
		public outNumber_number? As_outNumber_number(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.outNumber_number)
			{
				return null;
			}
			return new outNumber_number?(outNumber_number.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B574 RID: 46452 RVA: 0x00276AB0 File Offset: 0x00274CB0
		public outNumber_number Cast_outNumber_number(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outNumber_number)
			{
				return outNumber_number.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_outNumber_number is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B575 RID: 46453 RVA: 0x00276B05 File Offset: 0x00274D05
		public bool Is_outNumber_constNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.outNumber_constNumber;
		}

		// Token: 0x0600B576 RID: 46454 RVA: 0x00276B1F File Offset: 0x00274D1F
		public bool Is_outNumber_constNumber(GrammarBuilders g, out outNumber_constNumber value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outNumber_constNumber)
			{
				value = outNumber_constNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(outNumber_constNumber);
			return false;
		}

		// Token: 0x0600B577 RID: 46455 RVA: 0x00276B54 File Offset: 0x00274D54
		public outNumber_constNumber? As_outNumber_constNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.outNumber_constNumber)
			{
				return null;
			}
			return new outNumber_constNumber?(outNumber_constNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B578 RID: 46456 RVA: 0x00276B94 File Offset: 0x00274D94
		public outNumber_constNumber Cast_outNumber_constNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outNumber_constNumber)
			{
				return outNumber_constNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_outNumber_constNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B579 RID: 46457 RVA: 0x00276BEC File Offset: 0x00274DEC
		public T Switch<T>(GrammarBuilders g, Func<outNumber_number, T> func0, Func<outNumber_constNumber, T> func1)
		{
			outNumber_number outNumber_number;
			if (this.Is_outNumber_number(g, out outNumber_number))
			{
				return func0(outNumber_number);
			}
			outNumber_constNumber outNumber_constNumber;
			if (this.Is_outNumber_constNumber(g, out outNumber_constNumber))
			{
				return func1(outNumber_constNumber);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol outNumber");
		}

		// Token: 0x0600B57A RID: 46458 RVA: 0x00276C44 File Offset: 0x00274E44
		public void Switch(GrammarBuilders g, Action<outNumber_number> func0, Action<outNumber_constNumber> func1)
		{
			outNumber_number outNumber_number;
			if (this.Is_outNumber_number(g, out outNumber_number))
			{
				func0(outNumber_number);
				return;
			}
			outNumber_constNumber outNumber_constNumber;
			if (this.Is_outNumber_constNumber(g, out outNumber_constNumber))
			{
				func1(outNumber_constNumber);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol outNumber");
		}

		// Token: 0x0600B57B RID: 46459 RVA: 0x00276C9B File Offset: 0x00274E9B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B57C RID: 46460 RVA: 0x00276CB0 File Offset: 0x00274EB0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B57D RID: 46461 RVA: 0x00276CDA File Offset: 0x00274EDA
		public bool Equals(outNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004649 RID: 17993
		private ProgramNode _node;
	}
}
