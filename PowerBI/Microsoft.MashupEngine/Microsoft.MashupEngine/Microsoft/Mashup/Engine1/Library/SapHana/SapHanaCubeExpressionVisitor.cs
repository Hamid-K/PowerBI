using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200042A RID: 1066
	internal abstract class SapHanaCubeExpressionVisitor
	{
		// Token: 0x0600245E RID: 9310 RVA: 0x00067027 File Offset: 0x00065227
		protected SapHanaCubeExpressionVisitor(SapHanaCubeBase cube, OdbcQueryDomain queryDomain)
		{
			this.cube = cube;
			this.queryDomain = queryDomain;
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x00067040 File Offset: 0x00065240
		public bool TryVisit(QueryCubeExpression expression, IList<ParameterArguments> arguments, out Query query)
		{
			if (expression.DimensionAttributes.Count + expression.Measures.Count == 0)
			{
				query = new TableQuery(TableValue.Empty, this.queryDomain.DataSource.Host);
				return true;
			}
			try
			{
				List<ParameterArguments> list = new List<ParameterArguments>();
				List<ParameterArguments> list2 = new List<ParameterArguments>();
				foreach (ParameterArguments parameterArguments in arguments)
				{
					SapHanaParameter sapHanaParameter;
					if (!this.cube.Parameters.TryGetParameter(parameterArguments.Parameter.Identifier, out sapHanaParameter))
					{
						throw new NotSupportedException();
					}
					if (sapHanaParameter.Kind == SapHanaParameterKind.Variable)
					{
						list.Add(parameterArguments);
					}
					else
					{
						list2.Add(parameterArguments);
					}
				}
				this.inputParameters = list2;
				query = this.CompileQuery(expression, list);
				return true;
			}
			catch (NotSupportedException)
			{
			}
			query = null;
			return false;
		}

		// Token: 0x06002460 RID: 9312
		protected abstract Query CompileQuery(QueryCubeExpression queryExpression, IList<ParameterArguments> variableArguments);

		// Token: 0x06002461 RID: 9313 RVA: 0x00067130 File Offset: 0x00065330
		protected virtual QueryExpression GetMeasureExpr(Keys columns, SapHanaMeasure measure)
		{
			if (measure.AggregationType == SapHanaAggregationType.Count)
			{
				return new InvocationQueryExpression(new ConstantQueryExpression(LanguageLibrary.List.Count), new QueryExpression[]
				{
					new InvocationQueryExpression(new ConstantQueryExpression(LanguageLibrary.List.Select), new QueryExpression[]
					{
						new ColumnAccessQueryExpression(columns.IndexOfKey(measure.Column.Name)),
						new ConstantQueryExpression(SapHanaCubeExpressionVisitor.NotNullFunction.Instance)
					})
				});
			}
			return new InvocationQueryExpression(new ConstantQueryExpression(SapHanaCubeExpressionVisitor.GetAggregateFunction(measure)), new QueryExpression[]
			{
				new ColumnAccessQueryExpression(columns.IndexOfKey(measure.Column.Name))
			});
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000671CC File Offset: 0x000653CC
		protected FromItem NewFrom(string schema, string identifier)
		{
			Alias alias = Alias.NewNativeAlias(identifier);
			TableReference tableReference = new TableReference(Alias.NewNativeAlias(schema), alias);
			if (this.inputParameters.Count != 0)
			{
				string[] array = new string[this.inputParameters.Count];
				string[] array2 = new string[this.inputParameters.Count];
				for (int i = 0; i < array.Length; i++)
				{
					ParameterArguments parameterArguments = this.inputParameters[i];
					SapHanaParameter sapHanaParameter = this.cube.Parameters[parameterArguments.Parameter.Identifier];
					array[i] = sapHanaParameter.PlaceholderName;
					string text = SapHanaValueFormatter.FormatParameterArguments(parameterArguments.Values[0], sapHanaParameter.IsMultiline);
					array2[i] = "'" + text + "'";
				}
				return new SapHanaCubeExpressionVisitor.FromParameterizedTable(tableReference, array, array2);
			}
			return new FromTable
			{
				Table = tableReference
			};
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000672AC File Offset: 0x000654AC
		private static FunctionValue GetAggregateFunction(SapHanaMeasure measure)
		{
			switch (measure.AggregationType)
			{
			case SapHanaAggregationType.Sum:
				return Library.List.Sum;
			case SapHanaAggregationType.Min:
				return Library.List.Min;
			case SapHanaAggregationType.Max:
				return Library.List.Max;
			case SapHanaAggregationType.Avg:
				return Library.List.Average;
			}
			throw new NotSupportedException();
		}

		// Token: 0x04000EB9 RID: 3769
		protected readonly SapHanaCubeBase cube;

		// Token: 0x04000EBA RID: 3770
		protected readonly OdbcQueryDomain queryDomain;

		// Token: 0x04000EBB RID: 3771
		private IList<ParameterArguments> inputParameters;

		// Token: 0x0200042B RID: 1067
		protected class CubeExpressionTranslator : CubeExpressionVisitor<QueryExpression, object>
		{
			// Token: 0x06002464 RID: 9316 RVA: 0x000672FB File Offset: 0x000654FB
			public CubeExpressionTranslator(SapHanaCubeExpressionVisitor visitor, Keys queryColumns)
			{
				this.visitor = visitor;
				this.queryColumns = queryColumns;
			}

			// Token: 0x06002465 RID: 9317 RVA: 0x00064F1B File Offset: 0x0006311B
			public QueryExpression GetQueryExpression(CubeExpression expression)
			{
				return this.Visit(expression);
			}

			// Token: 0x06002466 RID: 9318 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override object NewSortOrder(QueryExpression expression, bool ascending)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06002467 RID: 9319 RVA: 0x00064F6A File Offset: 0x0006316A
			protected override QueryExpression NewIf(QueryExpression condition, QueryExpression trueCase, QueryExpression falseCase)
			{
				return new IfQueryExpression(condition, trueCase, falseCase);
			}

			// Token: 0x06002468 RID: 9320 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override QueryExpression NewQuery(QueryExpression from, IList<IdentifierCubeExpression> dimensionAttributes, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> cellProperties, QueryExpression filter, object[] sortOrders, RowRange rowRange)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06002469 RID: 9321 RVA: 0x00064F2E File Offset: 0x0006312E
			protected override QueryExpression NewConstant(Value constant)
			{
				return new ConstantQueryExpression(constant);
			}

			// Token: 0x0600246A RID: 9322 RVA: 0x00067311 File Offset: 0x00065511
			protected override QueryExpression NewIdentifier(IdentifierCubeExpression identifier)
			{
				return new ColumnAccessQueryExpression(this.queryColumns.IndexOfKey(identifier.Identifier));
			}

			// Token: 0x0600246B RID: 9323 RVA: 0x00064F74 File Offset: 0x00063174
			protected override QueryExpression NewInvocation(QueryExpression function, QueryExpression[] arguments)
			{
				return new InvocationQueryExpression(function, arguments);
			}

			// Token: 0x0600246C RID: 9324 RVA: 0x00064F24 File Offset: 0x00063124
			protected override QueryExpression NewBinary(BinaryOperator2 op, QueryExpression left, QueryExpression right)
			{
				return new BinaryQueryExpression(op, left, right);
			}

			// Token: 0x0600246D RID: 9325 RVA: 0x0006732C File Offset: 0x0006552C
			protected override QueryExpression VisitBinary(BinaryCubeExpression binary)
			{
				if (binary.Operator == BinaryOperator2.Equals)
				{
					ConstantCubeExpression constantCubeExpression = binary.Left as ConstantCubeExpression;
					ConstantCubeExpression constantCubeExpression2 = binary.Right as ConstantCubeExpression;
					if (constantCubeExpression != null && constantCubeExpression.Value.IsLogical && !constantCubeExpression.Value.AsBoolean)
					{
						return SapHanaCubeExpressionVisitor.CubeExpressionTranslator.NewNot(this.Visit(binary.Right));
					}
					if (constantCubeExpression2 != null && constantCubeExpression2.Value.IsLogical && !constantCubeExpression2.Value.AsBoolean)
					{
						return SapHanaCubeExpressionVisitor.CubeExpressionTranslator.NewNot(this.Visit(binary.Left));
					}
				}
				return base.VisitBinary(binary);
			}

			// Token: 0x0600246E RID: 9326 RVA: 0x000673BF File Offset: 0x000655BF
			private static QueryExpression NewNot(QueryExpression expr)
			{
				return new UnaryQueryExpression(UnaryOperator2.Not, expr);
			}

			// Token: 0x04000EBC RID: 3772
			protected readonly SapHanaCubeExpressionVisitor visitor;

			// Token: 0x04000EBD RID: 3773
			protected readonly Keys queryColumns;
		}

		// Token: 0x0200042C RID: 1068
		private sealed class FromParameterizedTable : FromItem
		{
			// Token: 0x0600246F RID: 9327 RVA: 0x000673C8 File Offset: 0x000655C8
			public FromParameterizedTable(TableReference tableReference, IList<string> placeholderNames, IList<string> arguments)
			{
				this.tableReference = tableReference;
				this.placeholderNames = placeholderNames;
				this.arguments = arguments;
			}

			// Token: 0x06002470 RID: 9328 RVA: 0x000033E7 File Offset: 0x000015E7
			public override FromItem ShallowCopy()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06002471 RID: 9329 RVA: 0x000673E8 File Offset: 0x000655E8
			public override void WriteCreateScript(ScriptWriter writer)
			{
				this.tableReference.WriteCreateScript(writer);
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				for (int i = 0; i < this.arguments.Count; i++)
				{
					if (i != 0)
					{
						writer.Write(SqlLanguageSymbols.CommaSqlString);
						writer.WriteSpace();
					}
					writer.WriteLiteral(ConstantType.AnsiString, "PLACEHOLDER");
					writer.Write(SqlLanguageSymbols.EqualsSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					writer.WriteLiteral(ConstantType.AnsiString, this.placeholderNames[i]);
					writer.Write(SqlLanguageSymbols.CommaSqlString);
					writer.Write(new ConstantSqlString(this.arguments[i]));
					writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
				}
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			}

			// Token: 0x04000EBE RID: 3774
			private readonly TableReference tableReference;

			// Token: 0x04000EBF RID: 3775
			private readonly IList<string> placeholderNames;

			// Token: 0x04000EC0 RID: 3776
			private readonly IList<string> arguments;
		}

		// Token: 0x0200042D RID: 1069
		private sealed class NotNullFunction : NativeFunctionValue1
		{
			// Token: 0x06002472 RID: 9330 RVA: 0x000674A6 File Offset: 0x000656A6
			public NotNullFunction()
				: base(Identifier.Underscore.Name)
			{
			}

			// Token: 0x06002473 RID: 9331 RVA: 0x000674B8 File Offset: 0x000656B8
			public override Value Invoke(Value value)
			{
				return LogicalValue.New(!value.IsNull);
			}

			// Token: 0x17000EEC RID: 3820
			// (get) Token: 0x06002474 RID: 9332 RVA: 0x000674C8 File Offset: 0x000656C8
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, BinaryExpressionSyntaxNode.New(BinaryOperator2.NotEquals, new ExclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), new ConstantExpressionSyntaxNode(Value.Null), TokenRange.Null));
					}
					return this.expression;
				}
			}

			// Token: 0x04000EC1 RID: 3777
			public static readonly FunctionValue Instance = new SapHanaCubeExpressionVisitor.NotNullFunction();

			// Token: 0x04000EC2 RID: 3778
			private IExpression expression;
		}

		// Token: 0x0200042E RID: 1070
		protected static class VariableFilterCompiler
		{
			// Token: 0x06002476 RID: 9334 RVA: 0x00067514 File Offset: 0x00065714
			public static CubeExpression Compile(SapHanaParameter parameter, Value[] args)
			{
				switch (parameter.SelectionType)
				{
				case SapHanaSelectionType.SingleValue:
					return SapHanaCubeExpressionVisitor.VariableFilterCompiler.CompileSingleValue(parameter, args);
				case SapHanaSelectionType.Interval:
					return SapHanaCubeExpressionVisitor.VariableFilterCompiler.CompileInterval(parameter, args);
				case SapHanaSelectionType.Range:
					return SapHanaCubeExpressionVisitor.VariableFilterCompiler.CompileRange(parameter, args);
				default:
					throw new InvalidOperationException(parameter.SelectionType.ToString());
				}
			}

			// Token: 0x06002477 RID: 9335 RVA: 0x00067570 File Offset: 0x00065770
			private static CubeExpression CompileInterval(SapHanaParameter parameter, Value[] args)
			{
				IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(parameter.ModelElementName);
				return new BinaryCubeExpression(BinaryOperator2.And, new BinaryCubeExpression(BinaryOperator2.GreaterThanOrEquals, identifierCubeExpression, new ConstantCubeExpression(args[0])), new BinaryCubeExpression(BinaryOperator2.LessThanOrEquals, identifierCubeExpression, new ConstantCubeExpression(args[1])));
			}

			// Token: 0x06002478 RID: 9336 RVA: 0x000675B0 File Offset: 0x000657B0
			private static CubeExpression CompileRange(SapHanaParameter parameter, Value[] args)
			{
				int asInteger = args[0].AsNumber.AsInteger32;
				if (asInteger == 6)
				{
					return SapHanaCubeExpressionVisitor.VariableFilterCompiler.CompileInterval(parameter, new Value[]
					{
						args[1],
						args[2]
					});
				}
				IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(parameter.ModelElementName);
				return new BinaryCubeExpression(SapHanaCubeExpressionVisitor.VariableFilterCompiler.CompileRangeOperator(asInteger), identifierCubeExpression, new ConstantCubeExpression(args[1]));
			}

			// Token: 0x06002479 RID: 9337 RVA: 0x00067608 File Offset: 0x00065808
			private static CubeExpression CompileSingleValue(SapHanaParameter parameter, Value[] args)
			{
				IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(parameter.ModelElementName);
				if (parameter.IsMultiline)
				{
					CubeExpression cubeExpression = null;
					foreach (IValueReference valueReference in args[0].AsList)
					{
						if (cubeExpression == null)
						{
							cubeExpression = SapHanaCubeExpressionVisitor.VariableFilterCompiler.CompileEquals(identifierCubeExpression, valueReference.Value);
						}
						else
						{
							cubeExpression = new BinaryCubeExpression(BinaryOperator2.Or, cubeExpression, SapHanaCubeExpressionVisitor.VariableFilterCompiler.CompileEquals(identifierCubeExpression, valueReference.Value));
						}
					}
					return cubeExpression;
				}
				return SapHanaCubeExpressionVisitor.VariableFilterCompiler.CompileEquals(identifierCubeExpression, args[0]);
			}

			// Token: 0x0600247A RID: 9338 RVA: 0x00067698 File Offset: 0x00065898
			private static CubeExpression CompileEquals(IdentifierCubeExpression left, Value right)
			{
				return new BinaryCubeExpression(BinaryOperator2.Equals, left, new ConstantCubeExpression(right));
			}

			// Token: 0x0600247B RID: 9339 RVA: 0x000676A8 File Offset: 0x000658A8
			private static BinaryOperator2 CompileRangeOperator(int value)
			{
				switch (value)
				{
				case 0:
					return BinaryOperator2.GreaterThan;
				case 1:
					return BinaryOperator2.LessThan;
				case 2:
					return BinaryOperator2.GreaterThanOrEquals;
				case 3:
					return BinaryOperator2.LessThanOrEquals;
				case 4:
					return BinaryOperator2.Equals;
				case 5:
					return BinaryOperator2.NotEquals;
				default:
					throw ValueException.NewExpressionError<Message1>(Strings.SapHana_InvalidRangeOperator(value), null, null);
				}
			}
		}
	}
}
