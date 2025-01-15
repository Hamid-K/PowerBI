using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x0200120D RID: 4621
	internal class SqlExpressionToMAstVisitor
	{
		// Token: 0x060079CD RID: 31181 RVA: 0x001A4C2F File Offset: 0x001A2E2F
		public static RecordValue ToMAst(SqlExpression expression)
		{
			return SqlExpressionToMAstVisitor.VisitSqlExpression(expression).AsRecord;
		}

		// Token: 0x060079CE RID: 31182 RVA: 0x001A4C3C File Offset: 0x001A2E3C
		private static Value VisitOrderByItem(IScriptable scriptable)
		{
			if (scriptable == null)
			{
				return Value.Null;
			}
			if (scriptable is FromItem)
			{
				return SqlExpressionToMAstVisitor.VisitFromItem((FromItem)scriptable);
			}
			if (scriptable is GroupByClause)
			{
				return SqlExpressionToMAstVisitor.VisitGroupByClause((GroupByClause)scriptable);
			}
			if (scriptable is GroupByItem)
			{
				return SqlExpressionToMAstVisitor.VisitGroupByItem((GroupByItem)scriptable);
			}
			if (scriptable is SqlDataType)
			{
				return SqlExpressionToMAstVisitor.VisitSqlDataType((SqlDataType)scriptable);
			}
			if (scriptable is OrderByClause)
			{
				return SqlExpressionToMAstVisitor.VisitOrderByClause((OrderByClause)scriptable);
			}
			if (scriptable is OrderByItem)
			{
				return SqlExpressionToMAstVisitor.VisitOrderByItem((OrderByItem)scriptable);
			}
			if (scriptable is OutputClause)
			{
				return SqlExpressionToMAstVisitor.VisitOutputClause((OutputClause)scriptable);
			}
			if (scriptable is RotationClause)
			{
				return SqlExpressionToMAstVisitor.VisitRotationClause((RotationClause)scriptable);
			}
			if (scriptable is SelectItem)
			{
				return SqlExpressionToMAstVisitor.VisitSelectItem((SelectItem)scriptable);
			}
			if (scriptable is SqlColumnConstraint)
			{
				return SqlExpressionToMAstVisitor.VisitSqlColumnConstraint((SqlColumnConstraint)scriptable);
			}
			if (scriptable is SqlColumnDefinition)
			{
				return SqlExpressionToMAstVisitor.VisitSqlColumnDefinition((SqlColumnDefinition)scriptable);
			}
			if (scriptable is SqlExpression)
			{
				return SqlExpressionToMAstVisitor.VisitSqlExpression((SqlExpression)scriptable);
			}
			if (scriptable is SqlStatement)
			{
				return SqlExpressionToMAstVisitor.VisitSqlStatement((SqlStatement)scriptable);
			}
			if (scriptable is TableReference)
			{
				return SqlExpressionToMAstVisitor.VisitTableReference((TableReference)scriptable);
			}
			return SqlExpressionToMAstVisitor.Opaque(scriptable);
		}

		// Token: 0x060079CF RID: 31183 RVA: 0x001A4D70 File Offset: 0x001A2F70
		private static Value VisitFromItem(FromItem fromItem)
		{
			if (fromItem == null)
			{
				return Value.Null;
			}
			if (fromItem is FromFunction)
			{
				return SqlExpressionToMAstVisitor.Visit((FromFunction)fromItem);
			}
			if (fromItem is FromQuery)
			{
				return SqlExpressionToMAstVisitor.Visit((FromQuery)fromItem);
			}
			if (fromItem is FromTable)
			{
				return SqlExpressionToMAstVisitor.Visit((FromTable)fromItem);
			}
			if (fromItem is JoinOperation)
			{
				return SqlExpressionToMAstVisitor.Visit((JoinOperation)fromItem);
			}
			return SqlExpressionToMAstVisitor.Opaque(fromItem);
		}

		// Token: 0x060079D0 RID: 31184 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(FromFunction fromFunction)
		{
			return SqlExpressionToMAstVisitor.Opaque(fromFunction);
		}

		// Token: 0x060079D1 RID: 31185 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(FromQuery fromQuery)
		{
			return SqlExpressionToMAstVisitor.Opaque(fromQuery);
		}

		// Token: 0x060079D2 RID: 31186 RVA: 0x001A4DE4 File Offset: 0x001A2FE4
		private static RecordValue Visit(FromTable fromTable)
		{
			return RecordValue.New(Keys.New("Kind", "Table", "Alias", "Rotation"), new Value[]
			{
				TextValue.New("FromTable"),
				SqlExpressionToMAstVisitor.VisitTableReference(fromTable.Table),
				SqlExpressionToMAstVisitor.Visit(fromTable.Alias),
				SqlExpressionToMAstVisitor.VisitRotationClause(fromTable.RotationClause)
			});
		}

		// Token: 0x060079D3 RID: 31187 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(JoinOperation joinOperation)
		{
			return SqlExpressionToMAstVisitor.Opaque(joinOperation);
		}

		// Token: 0x060079D4 RID: 31188 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value VisitGroupByClause(GroupByClause groupByClause)
		{
			return SqlExpressionToMAstVisitor.Opaque(groupByClause);
		}

		// Token: 0x060079D5 RID: 31189 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value VisitGroupByItem(GroupByItem groupByItem)
		{
			return SqlExpressionToMAstVisitor.Opaque(groupByItem);
		}

		// Token: 0x060079D6 RID: 31190 RVA: 0x001A4E4C File Offset: 0x001A304C
		private static TypeValue VisitSqlDataType(SqlDataType sqlType)
		{
			return sqlType.TypeValue;
		}

		// Token: 0x060079D7 RID: 31191 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value VisitOrderByClause(OrderByClause orderByClause)
		{
			return SqlExpressionToMAstVisitor.Opaque(orderByClause);
		}

		// Token: 0x060079D8 RID: 31192 RVA: 0x001A4E54 File Offset: 0x001A3054
		private static Value VisitOutputClause(OutputClause outputClause)
		{
			if (outputClause == null)
			{
				return Value.Null;
			}
			if (outputClause is SelectOutputClause)
			{
				return SqlExpressionToMAstVisitor.Visit((SelectOutputClause)outputClause);
			}
			return SqlExpressionToMAstVisitor.Opaque(outputClause);
		}

		// Token: 0x060079D9 RID: 31193 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(SelectOutputClause selectOutputClause)
		{
			return SqlExpressionToMAstVisitor.Opaque(selectOutputClause);
		}

		// Token: 0x060079DA RID: 31194 RVA: 0x001A4E79 File Offset: 0x001A3079
		private static Value VisitRotationClause(RotationClause rotationClause)
		{
			if (rotationClause == null)
			{
				return Value.Null;
			}
			if (rotationClause is PivotClause)
			{
				return SqlExpressionToMAstVisitor.Visit((PivotClause)rotationClause);
			}
			if (rotationClause is UnpivotClause)
			{
				return SqlExpressionToMAstVisitor.Visit((UnpivotClause)rotationClause);
			}
			return SqlExpressionToMAstVisitor.Opaque(rotationClause);
		}

		// Token: 0x060079DB RID: 31195 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(PivotClause pivotClause)
		{
			return SqlExpressionToMAstVisitor.Opaque(pivotClause);
		}

		// Token: 0x060079DC RID: 31196 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(UnpivotClause unpivotClause)
		{
			return SqlExpressionToMAstVisitor.Opaque(unpivotClause);
		}

		// Token: 0x060079DD RID: 31197 RVA: 0x001A4EB4 File Offset: 0x001A30B4
		private static RecordValue VisitSelectItem(SelectItem selectItem)
		{
			return RecordValue.New(Keys.New("Kind", "Expression", "Alias"), new Value[]
			{
				TextValue.New("SelectItem"),
				SqlExpressionToMAstVisitor.VisitSqlExpression(selectItem.Expression),
				SqlExpressionToMAstVisitor.Visit(selectItem.Alias)
			});
		}

		// Token: 0x060079DE RID: 31198 RVA: 0x001A4F09 File Offset: 0x001A3109
		private static Value Visit(Alias alias)
		{
			if (alias == null)
			{
				return Value.Null;
			}
			if (alias.IsMitigated)
			{
				return new OpaqueProxyFunctionValue<Alias>(alias);
			}
			return TextValue.New(alias.Name);
		}

		// Token: 0x060079DF RID: 31199 RVA: 0x001A4F2E File Offset: 0x001A312E
		private static Value VisitSqlColumnConstraint(SqlColumnConstraint sqlColumnConstraint)
		{
			if (sqlColumnConstraint == null)
			{
				return Value.Null;
			}
			if (sqlColumnConstraint is NotNullConstraint)
			{
				return SqlExpressionToMAstVisitor.VisitSqlColumnConstraint((NotNullConstraint)sqlColumnConstraint);
			}
			return SqlExpressionToMAstVisitor.Opaque(sqlColumnConstraint);
		}

		// Token: 0x060079E0 RID: 31200 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value VisitSqlColumnDefinition(SqlColumnDefinition sqlColumnDefinition)
		{
			return SqlExpressionToMAstVisitor.Opaque(sqlColumnDefinition);
		}

		// Token: 0x060079E1 RID: 31201 RVA: 0x001A4F54 File Offset: 0x001A3154
		private static Value VisitSqlExpression(SqlExpression expression)
		{
			if (expression == null)
			{
				return Value.Null;
			}
			if (expression is AggregateFunctionCall)
			{
				return SqlExpressionToMAstVisitor.Visit((AggregateFunctionCall)expression);
			}
			if (expression is Condition)
			{
				return SqlExpressionToMAstVisitor.Visit((Condition)expression);
			}
			if (expression is DatePart)
			{
				return SqlExpressionToMAstVisitor.Visit((DatePart)expression);
			}
			if (expression is InArrayExpression)
			{
				return SqlExpressionToMAstVisitor.Visit((InArrayExpression)expression);
			}
			if (expression is ScalarExpression)
			{
				return SqlExpressionToMAstVisitor.Visit((ScalarExpression)expression);
			}
			if (expression is SqlQueryExpression)
			{
				return SqlExpressionToMAstVisitor.Visit((SqlQueryExpression)expression);
			}
			if (expression is WhenItem)
			{
				return SqlExpressionToMAstVisitor.Visit((WhenItem)expression);
			}
			return SqlExpressionToMAstVisitor.Opaque(expression);
		}

		// Token: 0x060079E2 RID: 31202 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(AggregateFunctionCall aggregateFuctionCall)
		{
			return SqlExpressionToMAstVisitor.Opaque(aggregateFuctionCall);
		}

		// Token: 0x060079E3 RID: 31203 RVA: 0x001A4FFC File Offset: 0x001A31FC
		private static Value Visit(Condition condition)
		{
			if (condition == null)
			{
				return Value.Null;
			}
			if (condition is BetweenOperation)
			{
				return SqlExpressionToMAstVisitor.Visit((BetweenOperation)condition);
			}
			if (condition is BinaryLogicalOperation)
			{
				return SqlExpressionToMAstVisitor.Visit((BinaryLogicalOperation)condition);
			}
			if (condition is ConditionOperation)
			{
				return SqlExpressionToMAstVisitor.Visit((ConditionOperation)condition);
			}
			if (condition is LikePredicate)
			{
				return SqlExpressionToMAstVisitor.Visit((LikePredicate)condition);
			}
			if (condition is UnaryLogicalOperation)
			{
				return SqlExpressionToMAstVisitor.Visit((UnaryLogicalOperation)condition);
			}
			return SqlExpressionToMAstVisitor.Opaque(condition);
		}

		// Token: 0x060079E4 RID: 31204 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(BetweenOperation betweenOperation)
		{
			return SqlExpressionToMAstVisitor.Opaque(betweenOperation);
		}

		// Token: 0x060079E5 RID: 31205 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(BinaryLogicalOperation binaryLogicalOperation)
		{
			return SqlExpressionToMAstVisitor.Opaque(binaryLogicalOperation);
		}

		// Token: 0x060079E6 RID: 31206 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(ConditionOperation conditionOperation)
		{
			return SqlExpressionToMAstVisitor.Opaque(conditionOperation);
		}

		// Token: 0x060079E7 RID: 31207 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(LikePredicate likePredicate)
		{
			return SqlExpressionToMAstVisitor.Opaque(likePredicate);
		}

		// Token: 0x060079E8 RID: 31208 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(UnaryLogicalOperation unaryLogicalOperation)
		{
			return SqlExpressionToMAstVisitor.Opaque(unaryLogicalOperation);
		}

		// Token: 0x060079E9 RID: 31209 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(DatePart datePart)
		{
			return SqlExpressionToMAstVisitor.Opaque(datePart);
		}

		// Token: 0x060079EA RID: 31210 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(InArrayExpression inArrayExpression)
		{
			return SqlExpressionToMAstVisitor.Opaque(inArrayExpression);
		}

		// Token: 0x060079EB RID: 31211 RVA: 0x001A507C File Offset: 0x001A327C
		private static Value Visit(ScalarExpression scalarExpression)
		{
			if (scalarExpression == null)
			{
				return Value.Null;
			}
			if (scalarExpression is BinaryScalarOperation)
			{
				return SqlExpressionToMAstVisitor.Visit((BinaryScalarOperation)scalarExpression);
			}
			if (scalarExpression is CaseFunction)
			{
				return SqlExpressionToMAstVisitor.Visit((CaseFunction)scalarExpression);
			}
			if (scalarExpression is CastCall)
			{
				return SqlExpressionToMAstVisitor.Visit((CastCall)scalarExpression);
			}
			if (scalarExpression is ColumnReference)
			{
				return SqlExpressionToMAstVisitor.Visit((ColumnReference)scalarExpression);
			}
			if (scalarExpression is ColumnTypeExpression)
			{
				return SqlExpressionToMAstVisitor.Visit((ColumnTypeExpression)scalarExpression);
			}
			if (scalarExpression is ConvertCall)
			{
				return SqlExpressionToMAstVisitor.Visit((ConvertCall)scalarExpression);
			}
			if (scalarExpression is DynamicParameter)
			{
				return SqlExpressionToMAstVisitor.Visit((DynamicParameter)scalarExpression);
			}
			if (scalarExpression is FieldAccessExpression)
			{
				return SqlExpressionToMAstVisitor.Visit((FieldAccessExpression)scalarExpression);
			}
			if (scalarExpression is FunctionParameterValue)
			{
				return SqlExpressionToMAstVisitor.Visit((FunctionParameterValue)scalarExpression);
			}
			if (scalarExpression is FunctionReferenceBase)
			{
				return SqlExpressionToMAstVisitor.Visit((FunctionReferenceBase)scalarExpression);
			}
			if (scalarExpression is LiteralExpression)
			{
				return SqlExpressionToMAstVisitor.Visit((LiteralExpression)scalarExpression);
			}
			if (scalarExpression is SqlConstant)
			{
				return SqlExpressionToMAstVisitor.Visit((SqlConstant)scalarExpression);
			}
			if (scalarExpression is SqlDefault)
			{
				return SqlExpressionToMAstVisitor.Visit((SqlDefault)scalarExpression);
			}
			if (scalarExpression is UnaryScalarOperation)
			{
				return SqlExpressionToMAstVisitor.Visit((UnaryScalarOperation)scalarExpression);
			}
			if (scalarExpression is VariableReference)
			{
				return SqlExpressionToMAstVisitor.Visit((VariableReference)scalarExpression);
			}
			return SqlExpressionToMAstVisitor.Opaque(scalarExpression);
		}

		// Token: 0x060079EC RID: 31212 RVA: 0x001A51C4 File Offset: 0x001A33C4
		private static RecordValue Visit(BinaryScalarOperation binaryScalarOperation)
		{
			return RecordValue.New(Keys.New("Kind", "Operator", "Left", "Right"), new Value[]
			{
				TextValue.New("Binary"),
				TextValue.New(binaryScalarOperation.Operator.ToString()),
				SqlExpressionToMAstVisitor.VisitSqlExpression(binaryScalarOperation.Left),
				SqlExpressionToMAstVisitor.VisitSqlExpression(binaryScalarOperation.Right)
			});
		}

		// Token: 0x060079ED RID: 31213 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(CaseFunction caseFunction)
		{
			return SqlExpressionToMAstVisitor.Opaque(caseFunction);
		}

		// Token: 0x060079EE RID: 31214 RVA: 0x001A523C File Offset: 0x001A343C
		private static RecordValue Visit(CastCall castCall)
		{
			Value value = SqlExpressionToMAstVisitor.VisitSqlDataType(castCall.Type);
			return RecordValue.New(Keys.New("Kind", "Expression", "Type"), new Value[]
			{
				TextValue.New("Cast"),
				SqlExpressionToMAstVisitor.VisitSqlExpression(castCall.Expression),
				value
			});
		}

		// Token: 0x060079EF RID: 31215 RVA: 0x001A5294 File Offset: 0x001A3494
		private static RecordValue Visit(ColumnReference columnReference)
		{
			return RecordValue.New(Keys.New("Kind", "Name", "Qualifier"), new Value[]
			{
				TextValue.New("ColumnReference"),
				SqlExpressionToMAstVisitor.Visit(columnReference.Name),
				SqlExpressionToMAstVisitor.Visit(columnReference.Qualifier)
			});
		}

		// Token: 0x060079F0 RID: 31216 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(ColumnTypeExpression columnTypeExpression)
		{
			return SqlExpressionToMAstVisitor.Opaque(columnTypeExpression);
		}

		// Token: 0x060079F1 RID: 31217 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(ConvertCall convertCall)
		{
			return SqlExpressionToMAstVisitor.Opaque(convertCall);
		}

		// Token: 0x060079F2 RID: 31218 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(DynamicParameter dynamicParameter)
		{
			return SqlExpressionToMAstVisitor.Opaque(dynamicParameter);
		}

		// Token: 0x060079F3 RID: 31219 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(FieldAccessExpression fieldAccessExpression)
		{
			return SqlExpressionToMAstVisitor.Opaque(fieldAccessExpression);
		}

		// Token: 0x060079F4 RID: 31220 RVA: 0x001A52E9 File Offset: 0x001A34E9
		private static RecordValue Visit(FunctionParameterValue functionParameterValue)
		{
			return RecordValue.New(Keys.New("Expression", "Type"), new Value[]
			{
				SqlExpressionToMAstVisitor.VisitSqlExpression(functionParameterValue.Value),
				SqlExpressionToMAstVisitor.VisitSqlDataType(functionParameterValue.ParameterType)
			});
		}

		// Token: 0x060079F5 RID: 31221 RVA: 0x001A5324 File Offset: 0x001A3524
		private static Value Visit(FunctionReferenceBase functionReferenceBase)
		{
			if (functionReferenceBase == null)
			{
				return Value.Null;
			}
			if (functionReferenceBase is BuiltInFunctionReference)
			{
				return SqlExpressionToMAstVisitor.Visit((BuiltInFunctionReference)functionReferenceBase);
			}
			if (functionReferenceBase is StoredFunctionReference)
			{
				return SqlExpressionToMAstVisitor.Visit((StoredFunctionReference)functionReferenceBase);
			}
			if (functionReferenceBase is StoredProcedureReference)
			{
				return SqlExpressionToMAstVisitor.Visit((StoredProcedureReference)functionReferenceBase);
			}
			return SqlExpressionToMAstVisitor.Opaque(functionReferenceBase);
		}

		// Token: 0x060079F6 RID: 31222 RVA: 0x001A537C File Offset: 0x001A357C
		private static Value Visit(BuiltInFunctionReference builtinFunctionReference)
		{
			if (builtinFunctionReference == null)
			{
				return Value.Null;
			}
			if (builtinFunctionReference.GetType() == typeof(BuiltInFunctionReference))
			{
				return SqlExpressionToMAstVisitor.VisitBuiltinFunctionReference(builtinFunctionReference);
			}
			return SqlExpressionToMAstVisitor.Opaque(builtinFunctionReference);
		}

		// Token: 0x060079F7 RID: 31223 RVA: 0x001A53AC File Offset: 0x001A35AC
		private static RecordValue VisitBuiltinFunctionReference(BuiltInFunctionReference builtinFunctionReference)
		{
			return RecordValue.New(Keys.New("Kind", "Function", "Arguments"), new Value[]
			{
				TextValue.New("Invocation"),
				RecordValue.New(Keys.New("Kind", "Name", "VerbatimPrefix"), new Value[]
				{
					TextValue.New("Function"),
					SqlExpressionToMAstVisitor.Visit(new ConstantSqlString?(builtinFunctionReference.Name)),
					SqlExpressionToMAstVisitor.Visit(new ConstantSqlString?(builtinFunctionReference.VerbatimPrefix))
				}),
				SqlExpressionToMAstVisitor.Visit<FunctionParameterValue>(builtinFunctionReference.Parameters)
			});
		}

		// Token: 0x060079F8 RID: 31224 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(StoredFunctionReference storedFunctionReference)
		{
			return SqlExpressionToMAstVisitor.Opaque(storedFunctionReference);
		}

		// Token: 0x060079F9 RID: 31225 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(StoredProcedureReference storedProcedureReference)
		{
			return SqlExpressionToMAstVisitor.Opaque(storedProcedureReference);
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x001A5448 File Offset: 0x001A3648
		private static RecordValue Visit(LiteralExpression literalExpression)
		{
			return RecordValue.New(Keys.New("Kind", "Value"), new Value[]
			{
				TextValue.New("Literal"),
				SqlExpressionToMAstVisitor.Visit(new ConstantSqlString?(literalExpression.Value))
			});
		}

		// Token: 0x060079FB RID: 31227 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(SqlConstant sqlConstant)
		{
			return SqlExpressionToMAstVisitor.Opaque(sqlConstant);
		}

		// Token: 0x060079FC RID: 31228 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(SqlDefault sqlDefault)
		{
			return SqlExpressionToMAstVisitor.Opaque(sqlDefault);
		}

		// Token: 0x060079FD RID: 31229 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(UnaryScalarOperation unaryScalarOperation)
		{
			return SqlExpressionToMAstVisitor.Opaque(unaryScalarOperation);
		}

		// Token: 0x060079FE RID: 31230 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(VariableReference variableReference)
		{
			return SqlExpressionToMAstVisitor.Opaque(variableReference);
		}

		// Token: 0x060079FF RID: 31231 RVA: 0x001A5484 File Offset: 0x001A3684
		private static Value Visit(SqlQueryExpression sqlQueryExpression)
		{
			if (sqlQueryExpression == null)
			{
				return Value.Null;
			}
			if (sqlQueryExpression is BinaryQueryOperation)
			{
				return SqlExpressionToMAstVisitor.Visit((BinaryQueryOperation)sqlQueryExpression);
			}
			if (sqlQueryExpression is QuerySpecification)
			{
				return SqlExpressionToMAstVisitor.Visit((QuerySpecification)sqlQueryExpression);
			}
			if (sqlQueryExpression is VerbatimSqlQueryExpression)
			{
				return SqlExpressionToMAstVisitor.Visit((VerbatimSqlQueryExpression)sqlQueryExpression);
			}
			return SqlExpressionToMAstVisitor.Opaque(sqlQueryExpression);
		}

		// Token: 0x06007A00 RID: 31232 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(BinaryQueryOperation binaryQueryOperation)
		{
			return SqlExpressionToMAstVisitor.Opaque(binaryQueryOperation);
		}

		// Token: 0x06007A01 RID: 31233 RVA: 0x001A54DC File Offset: 0x001A36DC
		private static Value Visit(QuerySpecification querySpecification)
		{
			if (querySpecification == null)
			{
				return Value.Null;
			}
			if (querySpecification.GetType() == typeof(QuerySpecification))
			{
				return SqlExpressionToMAstVisitor.VisitQuerySpecification(querySpecification);
			}
			return SqlExpressionToMAstVisitor.Opaque(querySpecification);
		}

		// Token: 0x06007A02 RID: 31234 RVA: 0x001A550C File Offset: 0x001A370C
		private static RecordValue VisitQuerySpecification(QuerySpecification querySpecification)
		{
			return RecordValue.New(Keys.New(new string[] { "Kind", "Select", "From", "Where", "GroupBy", "Having", "OrderBy", "Distinct" }), new Value[]
			{
				TextValue.New("Select"),
				SqlExpressionToMAstVisitor.Visit<SelectItem>(querySpecification.SelectItems),
				SqlExpressionToMAstVisitor.Visit<FromItem>(querySpecification.FromItems),
				SqlExpressionToMAstVisitor.Visit(querySpecification.WhereClause),
				SqlExpressionToMAstVisitor.VisitGroupByClause(querySpecification.GroupByClause),
				SqlExpressionToMAstVisitor.Visit(querySpecification.HavingClause),
				SqlExpressionToMAstVisitor.VisitOrderByClause(querySpecification.OrderByClause),
				LogicalValue.New(querySpecification.RepeatedRowOption == RepeatedRowOption.Distinct)
			});
		}

		// Token: 0x06007A03 RID: 31235 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(VerbatimSqlQueryExpression verbatimSqlQueryExpression)
		{
			return SqlExpressionToMAstVisitor.Opaque(verbatimSqlQueryExpression);
		}

		// Token: 0x06007A04 RID: 31236 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value Visit(WhenItem whenItem)
		{
			return SqlExpressionToMAstVisitor.Opaque(whenItem);
		}

		// Token: 0x06007A05 RID: 31237 RVA: 0x001A4DDC File Offset: 0x001A2FDC
		private static Value VisitSqlStatement(SqlStatement sqlStatement)
		{
			return SqlExpressionToMAstVisitor.Opaque(sqlStatement);
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x001A55E4 File Offset: 0x001A37E4
		private static Value VisitTableReference(TableReference tableReference)
		{
			if (tableReference == null)
			{
				return Value.Null;
			}
			return RecordValue.New(Keys.New("Catalog", "Schema", "Name"), new Value[]
			{
				SqlExpressionToMAstVisitor.Visit(tableReference.Catalog),
				SqlExpressionToMAstVisitor.Visit(tableReference.Schema),
				SqlExpressionToMAstVisitor.Visit(tableReference.Name)
			});
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x001A5644 File Offset: 0x001A3844
		private static Value Visit(ConstantSqlString? constantString)
		{
			if (constantString == null)
			{
				return Value.Null;
			}
			return TextValue.New(constantString.Value.String);
		}

		// Token: 0x06007A08 RID: 31240 RVA: 0x001A5674 File Offset: 0x001A3874
		private static Value Visit<T>(IList<T> list) where T : IScriptable
		{
			if (list == null)
			{
				return Value.Null;
			}
			return ListValue.New(list.Select((T item) => SqlExpressionToMAstVisitor.VisitOrderByItem(item)).ToArray<Value>());
		}

		// Token: 0x06007A09 RID: 31241 RVA: 0x001A56AE File Offset: 0x001A38AE
		private static Value Opaque(IScriptable scriptable)
		{
			if (scriptable == null)
			{
				return Value.Null;
			}
			return RecordValue.New(SqlExpressionToMAstVisitor.OpaqueKeys, new Value[]
			{
				TextValue.New("Opaque"),
				new OpaqueProxyFunctionValue<IScriptable>(scriptable)
			});
		}

		// Token: 0x04004276 RID: 17014
		private static readonly Keys OpaqueKeys = Keys.New("Kind", "Value");
	}
}
