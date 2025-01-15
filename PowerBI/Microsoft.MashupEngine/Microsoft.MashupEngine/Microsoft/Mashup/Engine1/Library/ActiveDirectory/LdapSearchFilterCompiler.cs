using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FFE RID: 4094
	internal class LdapSearchFilterCompiler
	{
		// Token: 0x06006B51 RID: 27473 RVA: 0x001718DE File Offset: 0x0016FADE
		public LdapSearchFilterCompiler(ActiveDirectoryEnvironment environment, string objectCategory, ActiveDirectoryColumnInfo[] columns)
		{
			this.environment = environment;
			this.columns = columns;
		}

		// Token: 0x06006B52 RID: 27474 RVA: 0x001718F4 File Offset: 0x0016FAF4
		public bool TryBuild(QueryExpression expression, out Filter filter, out bool isPartial)
		{
			try
			{
				filter = this.VisitQueryExpression(expression, out isPartial);
				return true;
			}
			catch (NotSupportedException)
			{
			}
			filter = null;
			isPartial = true;
			return false;
		}

		// Token: 0x06006B53 RID: 27475 RVA: 0x0017192C File Offset: 0x0016FB2C
		private Filter VisitQueryExpression(QueryExpression expression, out bool isPartial)
		{
			switch (expression.Kind)
			{
			case QueryExpressionKind.Binary:
				return this.VisitBinaryExpression((BinaryQueryExpression)expression, out isPartial);
			case QueryExpressionKind.Constant:
			{
				Value value;
				this.VisitConstantExpression((ConstantQueryExpression)expression, ValueKind.Logical, out value);
				isPartial = false;
				if (!value.AsBoolean)
				{
					return LdapSearchFilterCompiler.CreateAlwaysFalseFilter();
				}
				return LdapSearchFilterCompiler.CreateAlwaysTrueFilter();
			}
			case QueryExpressionKind.If:
				return this.VisitIfExpression((IfQueryExpression)expression, out isPartial);
			case QueryExpressionKind.Invocation:
				return this.VisitInvocationExpression((InvocationQueryExpression)expression, out isPartial);
			case QueryExpressionKind.Unary:
				return this.VisitUnaryExpression((UnaryQueryExpression)expression, out isPartial);
			case QueryExpressionKind.ArgumentAccess:
				return this.VisitQueryExpression((ArgumentAccessQueryExpression)expression, out isPartial);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006B54 RID: 27476 RVA: 0x001719D8 File Offset: 0x0016FBD8
		private Filter VisitInvocationExpression(InvocationQueryExpression expression, out bool isPartial)
		{
			FunctionValue asFunction = LdapSearchFilterCompiler.ExpectType<ConstantQueryExpression>(expression.Function).Value.AsFunction;
			string text = this.VisitColumnAccessExpression(LdapSearchFilterCompiler.ExpectType<ColumnAccessQueryExpression>(expression.Arguments[0]));
			this.EnsureAttributeSupportsWildcards(text);
			string text2 = LdapSearchFilterCompiler.ExpectType<ConstantQueryExpression>(expression.Arguments[1]).Value.AsText.ToString();
			if (text2.Length == 0)
			{
				isPartial = false;
				return new AttributeValueAssertionFilter(text, RelationalOperator.Equal, AttributeValue.Any);
			}
			isPartial = true;
			if (asFunction.Equals(Library.Text.Contains))
			{
				return new AttributeValueAssertionFilter(text, RelationalOperator.Equal, new AttributeValue("*" + AttributeValue.Escape(text2) + "*"));
			}
			if (asFunction.Equals(Library.Text.StartsWith))
			{
				return new AttributeValueAssertionFilter(text, RelationalOperator.Equal, new AttributeValue(AttributeValue.Escape(text2) + "*"));
			}
			if (asFunction.Equals(Library.Text.EndsWith))
			{
				return new AttributeValueAssertionFilter(text, RelationalOperator.Equal, new AttributeValue("*" + AttributeValue.Escape(text2)));
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006B55 RID: 27477 RVA: 0x00171AE0 File Offset: 0x0016FCE0
		private Filter VisitIfExpression(IfQueryExpression expression, out bool isPartial)
		{
			Filter filter = this.VisitQueryExpression(expression.Condition, out isPartial);
			if (isPartial)
			{
				throw new NotSupportedException();
			}
			bool flag;
			Filter filter2 = this.VisitQueryExpression(expression.TrueCase, out flag);
			bool flag2;
			Filter filter3 = this.VisitQueryExpression(expression.FalseCase, out flag2);
			isPartial = flag || flag2;
			return new SetFilter(BooleanOperator.Or, new Filter[]
			{
				new SetFilter(BooleanOperator.And, new Filter[] { filter, filter2 }),
				new SetFilter(BooleanOperator.And, new Filter[]
				{
					new NotFilter(filter),
					filter3
				})
			});
		}

		// Token: 0x06006B56 RID: 27478 RVA: 0x00171B6C File Offset: 0x0016FD6C
		private Filter VisitUnaryExpression(UnaryQueryExpression unaryExpression, out bool isPartial)
		{
			if (unaryExpression.Operator == UnaryOperator2.Not)
			{
				Filter filter = this.VisitQueryExpression(unaryExpression.Expression, out isPartial);
				if (!isPartial)
				{
					return new NotFilter(filter);
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006B57 RID: 27479 RVA: 0x00171BA0 File Offset: 0x0016FDA0
		private Filter VisitBinaryExpression(BinaryQueryExpression binaryExpression, out bool isPartial)
		{
			switch (binaryExpression.Operator)
			{
			case BinaryOperator2.GreaterThan:
				return this.VisitBinaryExpressionRelationalNotEqual(binaryExpression, RelationalOperator.GreaterThanOrEquals, out isPartial);
			case BinaryOperator2.LessThan:
				return this.VisitBinaryExpressionRelationalNotEqual(binaryExpression, RelationalOperator.LesserThanOrEquals, out isPartial);
			case BinaryOperator2.GreaterThanOrEquals:
				return this.CreateValueAssertionFilter(binaryExpression, RelationalOperator.GreaterThanOrEquals, out isPartial);
			case BinaryOperator2.LessThanOrEquals:
				return this.CreateValueAssertionFilter(binaryExpression, RelationalOperator.LesserThanOrEquals, out isPartial);
			case BinaryOperator2.Equals:
				return this.CreateValueAssertionFilter(binaryExpression, RelationalOperator.Equal, out isPartial);
			case BinaryOperator2.NotEquals:
				return this.VisitBinaryExpressionNotEquals(binaryExpression, out isPartial);
			case BinaryOperator2.And:
				return this.VisitBinaryExpressionBoolean(binaryExpression, BooleanOperator.And, out isPartial);
			case BinaryOperator2.Or:
				return this.VisitBinaryExpressionBoolean(binaryExpression, BooleanOperator.Or, out isPartial);
			case BinaryOperator2.Coalesce:
				return this.VisitIfExpression(new IfQueryExpression(new BinaryQueryExpression(BinaryOperator2.NotEquals, binaryExpression.Left, new ConstantQueryExpression(Value.Null)), binaryExpression.Left, binaryExpression.Right), out isPartial);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006B58 RID: 27480 RVA: 0x00171C7C File Offset: 0x0016FE7C
		private Filter VisitBinaryExpressionRelationalNotEqual(BinaryQueryExpression binaryExpression, RelationalOperator op, out bool isPartial)
		{
			string text;
			ConstantQueryExpression constantQueryExpression;
			this.GetValueAssertionArguments(binaryExpression, out text, out constantQueryExpression);
			this.EnsureAttributeSupportsOrdering(text);
			TypeValue typeValue = this.environment.ValueBuilder.CreateAttributeTypeValue(text);
			Value value;
			AttributeValue attributeValue = this.VisitConstantExpression(constantQueryExpression, typeValue.TypeKind, out value);
			isPartial = false;
			if (op == RelationalOperator.GreaterThanOrEquals || op == RelationalOperator.LesserThanOrEquals)
			{
				return new SetFilter(BooleanOperator.And, new Filter[]
				{
					new NotFilter(new AttributeValueAssertionFilter(text, RelationalOperator.Equal, attributeValue)),
					new AttributeValueAssertionFilter(text, op, attributeValue)
				});
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06006B59 RID: 27481 RVA: 0x00171CF8 File Offset: 0x0016FEF8
		private Filter VisitBinaryExpressionNotEquals(BinaryQueryExpression binaryExpression, out bool isPartial)
		{
			string text;
			ConstantQueryExpression constantQueryExpression;
			this.GetValueAssertionArguments(binaryExpression, out text, out constantQueryExpression);
			if (constantQueryExpression.Value.Kind == ValueKind.Null)
			{
				isPartial = false;
				return new AttributeValueAssertionFilter(text, RelationalOperator.Equal, AttributeValue.Any);
			}
			Filter filter = this.CreateValueAssertionFilter(binaryExpression, RelationalOperator.Equal, out isPartial);
			if (!isPartial)
			{
				return new NotFilter(filter);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006B5A RID: 27482 RVA: 0x00171D48 File Offset: 0x0016FF48
		private string VisitColumnAccessExpression(ColumnAccessQueryExpression columnAccess)
		{
			ActiveDirectoryColumnInfo activeDirectoryColumnInfo = this.columns[columnAccess.Column];
			if (activeDirectoryColumnInfo.ColumnType == ColumnType.Attribute)
			{
				string text = activeDirectoryColumnInfo.AttributeNames[0];
				this.EnsureSupportsFilters(text);
				return text;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006B5B RID: 27483 RVA: 0x00171D84 File Offset: 0x0016FF84
		private SetFilter VisitBinaryExpressionBoolean(BinaryQueryExpression binaryExpression, BooleanOperator op, out bool isPartial)
		{
			bool flag;
			Filter filter = this.VisitQueryExpression(binaryExpression.Left, out flag);
			bool flag2;
			Filter filter2 = this.VisitQueryExpression(binaryExpression.Right, out flag2);
			List<Filter> list = new List<Filter>();
			this.FlattenSetFilter(op, filter, list);
			this.FlattenSetFilter(op, filter2, list);
			isPartial = flag || flag2;
			if (op == BooleanOperator.And || op == BooleanOperator.Or)
			{
				return new SetFilter(op, list.ToArray());
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06006B5C RID: 27484 RVA: 0x00171DEC File Offset: 0x0016FFEC
		private void FlattenSetFilter(BooleanOperator op, Filter filter, List<Filter> operands)
		{
			SetFilter setFilter = filter as SetFilter;
			if (setFilter != null && setFilter.Operator == op)
			{
				operands.AddRange(setFilter.Operands);
				return;
			}
			operands.Add(filter);
		}

		// Token: 0x06006B5D RID: 27485 RVA: 0x00171E20 File Offset: 0x00170020
		private AttributeValue VisitConstantExpression(ConstantQueryExpression constantQueryExpression, ValueKind allowedKind, out Value value)
		{
			value = constantQueryExpression.Value;
			if (value.Kind == allowedKind)
			{
				switch (value.Kind)
				{
				case ValueKind.DateTimeZone:
					return LdapSearchFilterCompiler.VisitDateTime(value.AsDateTimeZone);
				case ValueKind.Number:
				{
					long num;
					if (value.AsNumber.TryGetInt64(out num))
					{
						return AttributeValue.New(num);
					}
					break;
				}
				case ValueKind.Logical:
					if (!value.AsLogical.Boolean)
					{
						return AttributeValue.False;
					}
					return AttributeValue.True;
				case ValueKind.Text:
					return new AttributeValue(AttributeValue.Escape(value.AsString));
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006B5E RID: 27486 RVA: 0x00171EBC File Offset: 0x001700BC
		private Filter CreateValueAssertionFilter(BinaryQueryExpression binaryExpression, RelationalOperator op, out bool isPartial)
		{
			string text;
			ConstantQueryExpression constantQueryExpression;
			this.GetValueAssertionArguments(binaryExpression, out text, out constantQueryExpression);
			if (op - RelationalOperator.GreaterThanOrEquals <= 1)
			{
				this.EnsureAttributeSupportsOrdering(text);
			}
			ValueKind kind = constantQueryExpression.Value.Kind;
			if (kind == ValueKind.Null)
			{
				isPartial = false;
				return new NotFilter(new AttributeValueAssertionFilter(text, RelationalOperator.Equal, AttributeValue.Any));
			}
			if (kind == ValueKind.Text)
			{
				if (constantQueryExpression.Value.AsText.Length <= 0)
				{
					isPartial = false;
					return LdapSearchFilterCompiler.CreateAlwaysFalseFilter();
				}
			}
			isPartial = this.IsMatchPartial(text);
			TypeValue typeValue = this.environment.ValueBuilder.CreateAttributeTypeValue(text);
			Value value;
			AttributeValue attributeValue = this.VisitConstantExpression(constantQueryExpression, typeValue.TypeKind, out value);
			if (op > RelationalOperator.LesserThanOrEquals)
			{
				throw new InvalidOperationException();
			}
			return new AttributeValueAssertionFilter(text, op, attributeValue);
		}

		// Token: 0x06006B5F RID: 27487 RVA: 0x00171F69 File Offset: 0x00170169
		private static Filter CreateAlwaysFalseFilter()
		{
			return new SetFilter(BooleanOperator.And, new Filter[]
			{
				new NotFilter(new AttributeValueAssertionFilter("displayName", RelationalOperator.Equal, AttributeValue.Any)),
				new AttributeValueAssertionFilter("displayName", RelationalOperator.Equal, AttributeValue.Any)
			});
		}

		// Token: 0x06006B60 RID: 27488 RVA: 0x00171FA2 File Offset: 0x001701A2
		private static Filter CreateAlwaysTrueFilter()
		{
			return new SetFilter(BooleanOperator.Or, new Filter[]
			{
				new NotFilter(new AttributeValueAssertionFilter("displayName", RelationalOperator.Equal, AttributeValue.Any)),
				new AttributeValueAssertionFilter("displayName", RelationalOperator.Equal, AttributeValue.Any)
			});
		}

		// Token: 0x06006B61 RID: 27489 RVA: 0x00171FDC File Offset: 0x001701DC
		private void GetValueAssertionArguments(BinaryQueryExpression binaryExpression, out string attributeName, out ConstantQueryExpression constantQueryExpression)
		{
			ColumnAccessQueryExpression columnAccessQueryExpression;
			if (binaryExpression.Left.Kind == QueryExpressionKind.ColumnAccess && binaryExpression.Right.Kind == QueryExpressionKind.Constant)
			{
				columnAccessQueryExpression = (ColumnAccessQueryExpression)binaryExpression.Left;
				constantQueryExpression = (ConstantQueryExpression)binaryExpression.Right;
			}
			else
			{
				if (binaryExpression.Left.Kind != QueryExpressionKind.Constant || binaryExpression.Right.Kind != QueryExpressionKind.ColumnAccess)
				{
					throw new NotSupportedException();
				}
				columnAccessQueryExpression = (ColumnAccessQueryExpression)binaryExpression.Right;
				constantQueryExpression = (ConstantQueryExpression)binaryExpression.Left;
			}
			attributeName = this.VisitColumnAccessExpression(columnAccessQueryExpression);
		}

		// Token: 0x06006B62 RID: 27490 RVA: 0x00172066 File Offset: 0x00170266
		private void EnsureAttributeSupportsOrdering(string attributeName)
		{
			this.EnsureAttributeSyntax(attributeName, new string[] { "2.5.5.9", "2.5.5.11", "2.5.5.16" });
		}

		// Token: 0x06006B63 RID: 27491 RVA: 0x0017208D File Offset: 0x0017028D
		private void EnsureAttributeSupportsWildcards(string attributeName)
		{
			this.EnsureAttributeSyntax(attributeName, new string[] { "2.5.5.2", "2.5.5.3", "2.5.5.4", "2.5.5.5", "2.5.5.12", "2.5.5.13" });
		}

		// Token: 0x06006B64 RID: 27492 RVA: 0x001720CC File Offset: 0x001702CC
		private void EnsureAttributeSyntax(string attributeName, params string[] supportedSyntaxes)
		{
			if (!this.IsSyntax(attributeName, supportedSyntaxes))
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06006B65 RID: 27493 RVA: 0x001720DE File Offset: 0x001702DE
		private void EnsureSupportsFilters(string attributeName)
		{
			if (this.environment.TypeCatalog.GetAttribute(attributeName).IsConstructed)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06006B66 RID: 27494 RVA: 0x00172100 File Offset: 0x00170300
		private bool IsMatchPartial(string attributeName)
		{
			return this.IsSyntax(attributeName, new string[] { "2.5.5.1", "2.5.5.2", "2.5.5.4", "2.5.5.6", "2.5.5.7", "2.5.5.12", "2.5.5.14" });
		}

		// Token: 0x06006B67 RID: 27495 RVA: 0x00172154 File Offset: 0x00170354
		private bool IsSyntax(string attributeName, params string[] syntaxes)
		{
			ActiveDirectoryAttributeSchema attribute = this.environment.TypeCatalog.GetAttribute(attributeName);
			return Array.IndexOf<string>(syntaxes, attribute.Syntax) >= 0;
		}

		// Token: 0x06006B68 RID: 27496 RVA: 0x00172188 File Offset: 0x00170388
		private static AttributeValue VisitDateTime(DateTimeZoneValue dateTime)
		{
			return new AttributeValue(dateTime.AsClrDateTimeOffset.ToUniversalTime().ToString("yyMMddhhmmssZ", CultureInfo.InvariantCulture));
		}

		// Token: 0x06006B69 RID: 27497 RVA: 0x001721BA File Offset: 0x001703BA
		private static T ExpectType<T>(QueryExpression expression) where T : QueryExpression
		{
			if (expression is T)
			{
				return (T)((object)expression);
			}
			throw new NotSupportedException();
		}

		// Token: 0x04003BB1 RID: 15281
		private readonly ActiveDirectoryColumnInfo[] columns;

		// Token: 0x04003BB2 RID: 15282
		private readonly ActiveDirectoryEnvironment environment;
	}
}
