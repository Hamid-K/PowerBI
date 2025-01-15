using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E48 RID: 3656
	internal sealed class CubeExpressionToQueryExpressionVisitor : CubeExpressionVisitor<QueryExpression, object>
	{
		// Token: 0x0600624D RID: 25165 RVA: 0x00151AB3 File Offset: 0x0014FCB3
		public CubeExpressionToQueryExpressionVisitor(Keys columns, string timestampColumnName, Func<IdentifierCubeExpression, bool> isTimestampColumn)
		{
			this.columns = columns;
			this.timestampColumnName = timestampColumnName;
			this.isTimestampColumn = isTimestampColumn;
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x00064F1B File Offset: 0x0006311B
		public QueryExpression Translate(CubeExpression expression)
		{
			return this.Visit(expression);
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override object NewSortOrder(QueryExpression expression, bool ascending)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x00064F2E File Offset: 0x0006312E
		protected override QueryExpression NewConstant(Value constant)
		{
			return new ConstantQueryExpression(constant);
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x00151AD0 File Offset: 0x0014FCD0
		protected override QueryExpression NewIdentifier(IdentifierCubeExpression identifier)
		{
			string text = (this.isTimestampColumn(identifier) ? this.timestampColumnName : identifier.Identifier);
			return new ColumnAccessQueryExpression(this.columns.IndexOfKey(text));
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x00064F6A File Offset: 0x0006316A
		protected override QueryExpression NewIf(QueryExpression condition, QueryExpression trueCase, QueryExpression falseCase)
		{
			return new IfQueryExpression(condition, trueCase, falseCase);
		}

		// Token: 0x06006253 RID: 25171 RVA: 0x00064F74 File Offset: 0x00063174
		protected override QueryExpression NewInvocation(QueryExpression function, QueryExpression[] arguments)
		{
			return new InvocationQueryExpression(function, arguments);
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x00064F24 File Offset: 0x00063124
		protected override QueryExpression NewBinary(BinaryOperator2 op, QueryExpression left, QueryExpression right)
		{
			return new BinaryQueryExpression(op, left, right);
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override QueryExpression NewQuery(QueryExpression from, IList<IdentifierCubeExpression> dimensionAttributes, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> measureProperties, QueryExpression filter, object[] sortOrders, RowRange rowRange)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04003599 RID: 13721
		private readonly Keys columns;

		// Token: 0x0400359A RID: 13722
		private readonly string timestampColumnName;

		// Token: 0x0400359B RID: 13723
		private readonly Func<IdentifierCubeExpression, bool> isTimestampColumn;
	}
}
