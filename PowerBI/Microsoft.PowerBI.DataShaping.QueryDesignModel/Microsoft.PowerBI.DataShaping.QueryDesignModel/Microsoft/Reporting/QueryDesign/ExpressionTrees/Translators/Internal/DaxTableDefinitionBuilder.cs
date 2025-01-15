using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000146 RID: 326
	internal sealed class DaxTableDefinitionBuilder : IDaxVisualShapeBuilder, IDaxVisualAxisBuilder
	{
		// Token: 0x060011A0 RID: 4512 RVA: 0x000311AB File Offset: 0x0002F3AB
		private DaxTableDefinitionBuilder()
		{
			this._stringBuilder = new StringBuilder();
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x000311BE File Offset: 0x0002F3BE
		public static DaxTableDefinitionBuilder Table(DaxTableRef tableName, DaxExpression expression)
		{
			DaxTableDefinitionBuilder daxTableDefinitionBuilder = new DaxTableDefinitionBuilder();
			daxTableDefinitionBuilder.TableInternal(tableName, expression);
			return daxTableDefinitionBuilder;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000311D0 File Offset: 0x0002F3D0
		private void TableInternal(DaxTableRef tableName, DaxExpression expression)
		{
			this._stringBuilder.Append("TABLE ");
			this._stringBuilder.Append(tableName.ToString());
			this._stringBuilder.Append(" = ");
			this.Newline();
			this.Indent(1);
			this._stringBuilder.Append(DaxFormat.IncreaseIndent(expression.Text));
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0003123C File Offset: 0x0002F43C
		public DaxExpression ToDax()
		{
			return DaxExpression.Void(this._stringBuilder.ToString());
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0003124E File Offset: 0x0002F44E
		public IDaxVisualShapeBuilder VisualShape()
		{
			this.Newline();
			this.Indent(1);
			this._stringBuilder.Append("WITH VISUAL SHAPE");
			return this;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0003126F File Offset: 0x0002F46F
		void IDaxVisualShapeBuilder.Densify(DaxExpression isDensifiedColumnName)
		{
			this.Newline();
			this.Indent(2);
			this._stringBuilder.Append("DENSIFY ");
			this._stringBuilder.Append(isDensifiedColumnName.Text);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000312A1 File Offset: 0x0002F4A1
		IDaxVisualAxisBuilder IDaxVisualShapeBuilder.Axis(QueryVisualAxisName name)
		{
			this.Newline();
			this.Indent(2);
			this._stringBuilder.Append("AXIS ");
			this._stringBuilder.Append(DaxTableDefinitionBuilder.ToDaxText(name));
			return this;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000312D4 File Offset: 0x0002F4D4
		void IDaxVisualAxisBuilder.Group(IReadOnlyList<DaxExpression> keys, DaxExpression isSubtotal)
		{
			this.Newline();
			this.Indent(3);
			this._stringBuilder.Append("GROUP");
			bool flag = keys.Count > 1;
			this.Whitespace(flag, 4);
			for (int i = 0; i < keys.Count; i++)
			{
				if (i > 0)
				{
					this._stringBuilder.Append(",");
					this.Newline();
					this.Indent(4);
				}
				this._stringBuilder.Append(keys[i].Text);
			}
			if (isSubtotal != null)
			{
				this.Whitespace(flag, 4);
				this._stringBuilder.Append("TOTAL ");
				this._stringBuilder.Append(isSubtotal.Text);
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0003138C File Offset: 0x0002F58C
		void IDaxVisualAxisBuilder.OrderBy([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "Direction" })] IReadOnlyList<global::System.ValueTuple<DaxExpression, SortDirection>> orderBy)
		{
			this.Newline();
			this.Indent(3);
			this._stringBuilder.Append("ORDER BY");
			for (int i = 0; i < orderBy.Count; i++)
			{
				global::System.ValueTuple<DaxExpression, SortDirection> valueTuple = orderBy[i];
				if (i > 0)
				{
					this._stringBuilder.Append(",");
				}
				this.Newline();
				this.Indent(4);
				this._stringBuilder.Append(valueTuple.Item1.Text);
				this._stringBuilder.Append(" ");
				this._stringBuilder.Append(DaxFunctions.ToDaxAsStringDirection(valueTuple.Item2));
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00031431 File Offset: 0x0002F631
		internal static string ToDaxText(QueryVisualAxisName axisName)
		{
			switch (axisName)
			{
			case QueryVisualAxisName.Rows:
				return "rows";
			case QueryVisualAxisName.Columns:
				return "columns";
			case QueryVisualAxisName.RowPages:
				return "rowpages";
			default:
				throw new DaxTranslationException("Unknown visual axis name");
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00031463 File Offset: 0x0002F663
		private void Newline()
		{
			this._stringBuilder.Append(DaxFormat.NewLine);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00031478 File Offset: 0x0002F678
		private void Indent(int count)
		{
			for (int i = 0; i < count; i++)
			{
				this._stringBuilder.Append('\t');
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0003149F File Offset: 0x0002F69F
		private void Whitespace(bool useNewline, int indent)
		{
			if (useNewline)
			{
				this.Newline();
				this.Indent(indent);
				return;
			}
			this._stringBuilder.Append(" ");
		}

		// Token: 0x04000ADE RID: 2782
		private readonly StringBuilder _stringBuilder;
	}
}
