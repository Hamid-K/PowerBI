using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017E8 RID: 6120
	internal class FormulaFunc : FormulaExpression
	{
		// Token: 0x0600C99D RID: 51613 RVA: 0x002B2946 File Offset: 0x002B0B46
		public FormulaFunc(string name, IEnumerable<FormulaExpression> arguments)
			: this(name, ",", arguments)
		{
		}

		// Token: 0x0600C99E RID: 51614 RVA: 0x002B2958 File Offset: 0x002B0B58
		public FormulaFunc(string name, string argumentSeparator, IEnumerable<FormulaExpression> arguments)
		{
			this.Name = name;
			this.ArgumentSeparator = argumentSeparator;
			this.Arguments = arguments.TakeWhile((FormulaExpression i) => i != null).ToReadOnlyList<FormulaExpression>();
			base.Children = this.Arguments;
		}

		// Token: 0x170021EE RID: 8686
		// (get) Token: 0x0600C99F RID: 51615 RVA: 0x002B29B5 File Offset: 0x002B0BB5
		// (set) Token: 0x0600C9A0 RID: 51616 RVA: 0x002B29BD File Offset: 0x002B0BBD
		public IReadOnlyList<FormulaExpression> Arguments { get; protected set; }

		// Token: 0x170021EF RID: 8687
		// (get) Token: 0x0600C9A1 RID: 51617 RVA: 0x002B29C6 File Offset: 0x002B0BC6
		// (set) Token: 0x0600C9A2 RID: 51618 RVA: 0x002B29CE File Offset: 0x002B0BCE
		public string ArgumentSeparator { get; protected set; }

		// Token: 0x170021F0 RID: 8688
		// (get) Token: 0x0600C9A3 RID: 51619 RVA: 0x002B29D7 File Offset: 0x002B0BD7
		// (set) Token: 0x0600C9A4 RID: 51620 RVA: 0x002B29DF File Offset: 0x002B0BDF
		public string Name { get; protected set; }

		// Token: 0x0600C9A5 RID: 51621 RVA: 0x002B29E8 File Offset: 0x002B0BE8
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new FormulaFunc(this.Name, this.ArgumentSeparator, base.Children.Accept(visitor));
		}

		// Token: 0x0600C9A6 RID: 51622 RVA: 0x002B2A07 File Offset: 0x002B0C07
		public void Deconstruct(out FormulaExpression p0, out FormulaExpression p1)
		{
			p0 = base.Children.ElementAtOrDefault(0);
			p1 = base.Children.ElementAtOrDefault(1);
		}

		// Token: 0x0600C9A7 RID: 51623 RVA: 0x002B2A25 File Offset: 0x002B0C25
		public void Deconstruct(out FormulaExpression p0, out FormulaExpression p1, out FormulaExpression p2)
		{
			p0 = base.Children.ElementAtOrDefault(0);
			p1 = base.Children.ElementAtOrDefault(1);
			p2 = base.Children.ElementAtOrDefault(2);
		}

		// Token: 0x0600C9A8 RID: 51624 RVA: 0x002B2A51 File Offset: 0x002B0C51
		public void Deconstruct(out FormulaExpression p0, out FormulaExpression p1, out FormulaExpression p2, out FormulaExpression p3)
		{
			p0 = base.Children.ElementAtOrDefault(0);
			p1 = base.Children.ElementAtOrDefault(1);
			p2 = base.Children.ElementAtOrDefault(2);
			p3 = base.Children.ElementAtOrDefault(3);
		}

		// Token: 0x0600C9A9 RID: 51625 RVA: 0x002B2A8C File Offset: 0x002B0C8C
		protected override string ToCodeString()
		{
			string text = string.Join(this.ArgumentSeparator + " ", from a in base.Children.TakeWhile((FormulaExpression a) => a != null)
				select a.ToString());
			return this.Name + "(" + text + ")";
		}
	}
}
