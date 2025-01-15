using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011E3 RID: 4579
	internal class MAstToSqlExpressionVisitor
	{
		// Token: 0x060078B8 RID: 30904 RVA: 0x001A1B3C File Offset: 0x0019FD3C
		public static IScriptable VisitAst(RecordValue ast)
		{
			string asString = ast["Kind"].AsString;
			if (asString != null)
			{
				int length = asString.Length;
				switch (length)
				{
				case 4:
					if (asString == "Cast")
					{
						return MAstToSqlExpressionVisitor.VisitCast(ast);
					}
					break;
				case 5:
				case 8:
					break;
				case 6:
				{
					char c = asString[0];
					if (c != 'B')
					{
						if (c != 'O')
						{
							if (c == 'S')
							{
								if (asString == "Select")
								{
									return MAstToSqlExpressionVisitor.VisitSelect(ast);
								}
							}
						}
						else if (asString == "Opaque")
						{
							return MAstToSqlExpressionVisitor.VisitOpaque(ast);
						}
					}
					else if (asString == "Binary")
					{
						return MAstToSqlExpressionVisitor.VisitBinary(ast);
					}
					break;
				}
				case 7:
					if (asString == "Literal")
					{
						return MAstToSqlExpressionVisitor.VisitLiteral(ast);
					}
					break;
				case 9:
					if (asString == "FromTable")
					{
						return MAstToSqlExpressionVisitor.VisitFromTable(ast);
					}
					break;
				case 10:
				{
					char c = asString[0];
					if (c != 'I')
					{
						if (c == 'S')
						{
							if (asString == "SelectItem")
							{
								return MAstToSqlExpressionVisitor.VisitSelectItem(ast);
							}
						}
					}
					else if (asString == "Invocation")
					{
						return MAstToSqlExpressionVisitor.VisitInvocation(ast);
					}
					break;
				}
				default:
					if (length == 15)
					{
						if (asString == "ColumnReference")
						{
							return MAstToSqlExpressionVisitor.VisitColumnReference(ast);
						}
					}
					break;
				}
			}
			throw new NotSupportedException(Strings.UnsupportedMAStKind(ast["Kind"].AsString));
		}

		// Token: 0x060078B9 RID: 30905 RVA: 0x001A1CC8 File Offset: 0x0019FEC8
		private static T VisitAst<T>(Value ast) where T : IScriptable
		{
			if (ast.IsNull)
			{
				return default(T);
			}
			return (T)((object)MAstToSqlExpressionVisitor.VisitAst(ast.AsRecord));
		}

		// Token: 0x060078BA RID: 30906 RVA: 0x001A1CF7 File Offset: 0x0019FEF7
		private static Alias VisitAlias(Value alias)
		{
			if (alias.IsText)
			{
				return Alias.NewNativeAlias(alias.AsString);
			}
			return MAstToSqlExpressionVisitor.Extract<Alias>(alias);
		}

		// Token: 0x060078BB RID: 30907 RVA: 0x001A1D14 File Offset: 0x0019FF14
		private static BinaryScalarOperation VisitBinary(Value binaryScalarOperation)
		{
			return new BinaryScalarOperation(MAstToSqlExpressionVisitor.VisitAst<SqlExpression>(binaryScalarOperation["Left"]), (BinaryScalarOperator)Enum.Parse(typeof(BinaryScalarOperator), binaryScalarOperation["Operator"].AsString), MAstToSqlExpressionVisitor.VisitAst<SqlExpression>(binaryScalarOperation["Right"]));
		}

		// Token: 0x060078BC RID: 30908 RVA: 0x001A1D6C File Offset: 0x0019FF6C
		private static CastCall VisitCast(RecordValue castCall)
		{
			Value value = castCall["Type"];
			return new CastCall
			{
				Expression = MAstToSqlExpressionVisitor.VisitAst<SqlExpression>(castCall["Expression"]),
				Type = MAstToSqlExpressionVisitor.VisitSqlDataType(value)
			};
		}

		// Token: 0x060078BD RID: 30909 RVA: 0x001A1DAC File Offset: 0x0019FFAC
		private static ColumnReference VisitColumnReference(Value columnReference)
		{
			return new ColumnReference(MAstToSqlExpressionVisitor.VisitAlias(columnReference["Qualifier"]), MAstToSqlExpressionVisitor.VisitAlias(columnReference["Name"]));
		}

		// Token: 0x060078BE RID: 30910 RVA: 0x001A1DD3 File Offset: 0x0019FFD3
		private static SqlDataType VisitSqlDataType(Value ast)
		{
			if (ast.IsNull)
			{
				return null;
			}
			if (ast.IsText)
			{
				return new SqlDataType(TypeValue.Any, new ConstantSqlString(ast.AsString));
			}
			return new SqlDataType(ast.AsType);
		}

		// Token: 0x060078BF RID: 30911 RVA: 0x001A1E08 File Offset: 0x001A0008
		private static ConstantSqlString? VisitConstantSqlString(Value constantSqlString)
		{
			if (constantSqlString.IsNull)
			{
				return null;
			}
			return new ConstantSqlString?(new ConstantSqlString(constantSqlString.AsString));
		}

		// Token: 0x060078C0 RID: 30912 RVA: 0x001A1E38 File Offset: 0x001A0038
		private static IScriptable VisitFromTable(RecordValue fromTable)
		{
			return new FromTable
			{
				Table = MAstToSqlExpressionVisitor.VisitTableReference(fromTable["Table"]),
				RotationClause = MAstToSqlExpressionVisitor.VisitAst<RotationClause>(fromTable["Rotation"]),
				Alias = MAstToSqlExpressionVisitor.VisitAlias(fromTable["Alias"])
			};
		}

		// Token: 0x060078C1 RID: 30913 RVA: 0x001A1E8C File Offset: 0x001A008C
		private static IScriptable VisitInvocation(Value invocation)
		{
			ConstantSqlString? constantSqlString = null;
			Value value;
			if (invocation["Function"].TryGetValue("VerbatimPrefix", out value))
			{
				constantSqlString = MAstToSqlExpressionVisitor.VisitConstantSqlString(value);
			}
			ConstantSqlString constantSqlString2 = new ConstantSqlString(invocation["Function"]["Name"].AsString);
			if (invocation["Function"]["Kind"].AsString == "Function")
			{
				FunctionReferenceBase functionReferenceBase = ((constantSqlString == null) ? new BuiltInFunctionReference(constantSqlString2) : new BuiltInFunctionReference(constantSqlString.Value, constantSqlString2));
				functionReferenceBase.Parameters.AddRange(MAstToSqlExpressionVisitor.VisitArgumentList(invocation["Arguments"]));
				return functionReferenceBase;
			}
			throw new NotSupportedException();
		}

		// Token: 0x060078C2 RID: 30914 RVA: 0x001A1F4B File Offset: 0x001A014B
		private static FunctionParameterValue VisitFunctionParameterValue(Value functionParameterValue)
		{
			return new FunctionParameterValue
			{
				Value = MAstToSqlExpressionVisitor.VisitAst<SqlExpression>(functionParameterValue["Expression"]),
				ParameterType = MAstToSqlExpressionVisitor.VisitSqlDataType(functionParameterValue["Type"])
			};
		}

		// Token: 0x060078C3 RID: 30915 RVA: 0x001A1F80 File Offset: 0x001A0180
		private static IScriptable VisitLiteral(Value literal)
		{
			return new LiteralExpression(MAstToSqlExpressionVisitor.VisitConstantSqlString(literal["Value"]).Value);
		}

		// Token: 0x060078C4 RID: 30916 RVA: 0x001A1FAC File Offset: 0x001A01AC
		private static byte? VisitNullableByte(Value value)
		{
			if (value.IsNull)
			{
				return null;
			}
			return new byte?((byte)value.AsNumber.AsInteger32);
		}

		// Token: 0x060078C5 RID: 30917 RVA: 0x001A1FDC File Offset: 0x001A01DC
		private static int? VisitNullableInt(Value value)
		{
			if (value.IsNull)
			{
				return null;
			}
			return new int?(value.AsNumber.AsInteger32);
		}

		// Token: 0x060078C6 RID: 30918 RVA: 0x001A200B File Offset: 0x001A020B
		private static IScriptable VisitOpaque(RecordValue opaque)
		{
			return MAstToSqlExpressionVisitor.Extract<IScriptable>(opaque["Value"]);
		}

		// Token: 0x060078C7 RID: 30919 RVA: 0x001A2020 File Offset: 0x001A0220
		private static QuerySpecification VisitSelect(RecordValue select)
		{
			return new QuerySpecification
			{
				SelectItems = MAstToSqlExpressionVisitor.VisitList<SelectItem>(select["Select"]),
				FromItems = MAstToSqlExpressionVisitor.VisitList<FromItem>(select["From"]),
				WhereClause = MAstToSqlExpressionVisitor.VisitAst<Condition>(select["Where"]),
				GroupByClause = MAstToSqlExpressionVisitor.VisitAst<GroupByClause>(select["GroupBy"]),
				HavingClause = MAstToSqlExpressionVisitor.VisitAst<Condition>(select["Having"]),
				OrderByClause = MAstToSqlExpressionVisitor.VisitAst<OrderByClause>(select["OrderBy"]),
				RepeatedRowOption = (select["Distinct"].AsLogical.Boolean ? RepeatedRowOption.Distinct : RepeatedRowOption.All)
			};
		}

		// Token: 0x060078C8 RID: 30920 RVA: 0x001A20D7 File Offset: 0x001A02D7
		private static IScriptable VisitSelectItem(RecordValue selectItem)
		{
			return new SelectItem(MAstToSqlExpressionVisitor.VisitAst<SqlExpression>(selectItem["Expression"]), MAstToSqlExpressionVisitor.VisitAlias(selectItem["Alias"]));
		}

		// Token: 0x060078C9 RID: 30921 RVA: 0x001A2100 File Offset: 0x001A0300
		private static TableReference VisitTableReference(Value tableReference)
		{
			if (tableReference.IsNull)
			{
				return null;
			}
			return new TableReference(MAstToSqlExpressionVisitor.VisitAlias(tableReference["Schema"]), MAstToSqlExpressionVisitor.VisitAlias(tableReference["Name"]), MAstToSqlExpressionVisitor.VisitAlias(tableReference["Catalog"]));
		}

		// Token: 0x060078CA RID: 30922 RVA: 0x001A214C File Offset: 0x001A034C
		private static T[] VisitList<T>(Value value) where T : IScriptable
		{
			if (value.IsNull)
			{
				return null;
			}
			T[] array = new T[value.AsList.Count];
			int num = 0;
			foreach (IValueReference valueReference in value.AsList)
			{
				array[num++] = MAstToSqlExpressionVisitor.VisitAst<T>(valueReference.Value);
			}
			return array;
		}

		// Token: 0x060078CB RID: 30923 RVA: 0x001A21C8 File Offset: 0x001A03C8
		private static FunctionParameterValue[] VisitArgumentList(Value value)
		{
			if (value.IsNull)
			{
				return null;
			}
			FunctionParameterValue[] array = new FunctionParameterValue[value.AsList.Count];
			int num = 0;
			foreach (IValueReference valueReference in value.AsList)
			{
				array[num++] = MAstToSqlExpressionVisitor.VisitFunctionParameterValue(valueReference.Value);
			}
			return array;
		}

		// Token: 0x060078CC RID: 30924 RVA: 0x001A2240 File Offset: 0x001A0440
		private static T Extract<T>(Value value)
		{
			if (value.IsNull)
			{
				return default(T);
			}
			T t;
			if (value.IsFunction && OpaqueProxyFunctionValue<T>.TryGet(value.AsFunction, out t))
			{
				return t;
			}
			throw new NotSupportedException();
		}
	}
}
