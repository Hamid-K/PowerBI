using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001941 RID: 6465
	internal class CSharpMethodParameter : FormulaVariable
	{
		// Token: 0x0600D370 RID: 54128 RVA: 0x002D14F4 File Offset: 0x002CF6F4
		public CSharpMethodParameter(string name, Type type, bool nullable, FormulaExpression defaultValue, bool thisModifier)
			: base(name)
		{
			this.Nullable = nullable;
			this.Type = type;
			this.DefaultValue = defaultValue;
			this.ThisModifier = thisModifier;
			FormulaExpression defaultValue2 = this.DefaultValue;
			IReadOnlyList<FormulaExpression> readOnlyList;
			if (defaultValue2 == null)
			{
				readOnlyList = null;
			}
			else
			{
				IEnumerable<FormulaExpression> enumerable = defaultValue2.Yield<FormulaExpression>();
				readOnlyList = ((enumerable != null) ? enumerable.ToReadOnlyList<FormulaExpression>() : null);
			}
			base.Children = readOnlyList;
		}

		// Token: 0x1700231D RID: 8989
		// (get) Token: 0x0600D371 RID: 54129 RVA: 0x002D154A File Offset: 0x002CF74A
		public FormulaExpression DefaultValue { get; }

		// Token: 0x1700231E RID: 8990
		// (get) Token: 0x0600D372 RID: 54130 RVA: 0x002D1552 File Offset: 0x002CF752
		public bool Nullable { get; }

		// Token: 0x1700231F RID: 8991
		// (get) Token: 0x0600D373 RID: 54131 RVA: 0x002D155A File Offset: 0x002CF75A
		public bool ThisModifier { get; }

		// Token: 0x17002320 RID: 8992
		// (get) Token: 0x0600D374 RID: 54132 RVA: 0x002D1562 File Offset: 0x002CF762
		public Type Type { get; }

		// Token: 0x0600D375 RID: 54133 RVA: 0x002D156A File Offset: 0x002CF76A
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			string name = base.Name;
			Type type = this.Type;
			bool nullable = this.Nullable;
			FormulaExpression defaultValue = this.DefaultValue;
			return new CSharpMethodParameter(name, type, nullable, (defaultValue != null) ? defaultValue.Accept<FormulaExpression>(visitor) : null, this.ThisModifier);
		}

		// Token: 0x0600D376 RID: 54134 RVA: 0x002D159C File Offset: 0x002CF79C
		public override FormulaExpression CloneWith(string name)
		{
			return new CSharpMethodParameter(name, this.Type, this.Nullable, this.DefaultValue, this.ThisModifier);
		}

		// Token: 0x0600D377 RID: 54135 RVA: 0x002D15BC File Offset: 0x002CF7BC
		protected override string ToCodeString()
		{
			string text = (this.ThisModifier ? "this " : "");
			string text2 = (this.Nullable ? "?" : "");
			string text3 = ((this.DefaultValue != null) ? string.Format(" = {0}", this.DefaultValue) : "");
			return string.Concat(new string[]
			{
				text,
				this.Type.CsName(false),
				text2,
				" ",
				base.Name,
				text3
			});
		}
	}
}
