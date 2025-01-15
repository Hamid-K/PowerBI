using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018AD RID: 6317
	internal static class MAstToExpressionVisitor
	{
		// Token: 0x0600A0C9 RID: 41161 RVA: 0x0021507E File Offset: 0x0021327E
		public static IExpression ToExpression(RecordValue ast)
		{
			return MAstToExpressionVisitor.VisitAst(ast);
		}

		// Token: 0x0600A0CA RID: 41162 RVA: 0x00215086 File Offset: 0x00213286
		private static IExpression VisitAst(Value ast)
		{
			return MAstToExpressionVisitor.VisitAst(ast.AsRecord);
		}

		// Token: 0x0600A0CB RID: 41163 RVA: 0x00215094 File Offset: 0x00213294
		private static IExpression VisitAst(RecordValue ast)
		{
			string asString = ast["Kind"].AsString;
			if (asString != null)
			{
				switch (asString.Length)
				{
				case 2:
					if (asString == "If")
					{
						return MAstToExpressionVisitor.VisitIf(ast);
					}
					break;
				case 3:
					if (asString == "Let")
					{
						return MAstToExpressionVisitor.VisitLet(ast);
					}
					break;
				case 4:
				{
					char c = asString[0];
					if (c != 'L')
					{
						if (c == 'T')
						{
							if (asString == "Type")
							{
								return MAstToExpressionVisitor.VisitType(ast);
							}
						}
					}
					else if (asString == "List")
					{
						return MAstToExpressionVisitor.VisitList(ast);
					}
					break;
				}
				case 5:
				{
					char c = asString[0];
					if (c != 'T')
					{
						if (c == 'U')
						{
							if (asString == "Unary")
							{
								return MAstToExpressionVisitor.VisitUnary(ast);
							}
						}
					}
					else if (asString == "Throw")
					{
						return MAstToExpressionVisitor.VisitThrow(ast);
					}
					break;
				}
				case 6:
				{
					char c = asString[0];
					if (c != 'B')
					{
						if (c == 'R')
						{
							if (asString == "Record")
							{
								return MAstToExpressionVisitor.VisitRecord(ast);
							}
						}
					}
					else if (asString == "Binary")
					{
						return MAstToExpressionVisitor.VisitBinary(ast);
					}
					break;
				}
				case 7:
					if (asString == "Exports")
					{
						return MAstToExpressionVisitor.VisitExports(ast);
					}
					break;
				case 8:
				{
					char c = asString[0];
					if (c <= 'F')
					{
						if (c != 'C')
						{
							if (c == 'F')
							{
								if (asString == "Function")
								{
									return MAstToExpressionVisitor.VisitFunction(ast);
								}
							}
						}
						else if (asString == "Constant")
						{
							return MAstToExpressionVisitor.VisitConstant(ast);
						}
					}
					else if (c != 'L')
					{
						if (c != 'T')
						{
							if (c == 'V')
							{
								if (asString == "Verbatim")
								{
									return MAstToExpressionVisitor.VisitVerbatim(ast);
								}
							}
						}
						else if (asString == "TryCatch")
						{
							return MAstToExpressionVisitor.VisitTryCatch(ast);
						}
					}
					else if (asString == "ListType")
					{
						return MAstToExpressionVisitor.VisitListType(ast);
					}
					break;
				}
				case 9:
				{
					char c = asString[0];
					if (c != 'R')
					{
						if (c == 'T')
						{
							if (asString == "TableType")
							{
								return MAstToExpressionVisitor.VisitTableType(ast);
							}
						}
					}
					else if (asString == "RangeList")
					{
						return MAstToExpressionVisitor.VisitRangeList(ast);
					}
					break;
				}
				case 10:
				{
					char c = asString[1];
					if (c != 'd')
					{
						if (c != 'e')
						{
							if (c == 'n')
							{
								if (asString == "Invocation")
								{
									return MAstToExpressionVisitor.VisitInvocation(ast);
								}
							}
						}
						else if (asString == "RecordType")
						{
							return MAstToExpressionVisitor.VisitRecordType(ast);
						}
					}
					else if (asString == "Identifier")
					{
						return MAstToExpressionVisitor.VisitIdentifier(ast);
					}
					break;
				}
				case 11:
				{
					char c = asString[0];
					if (c != 'F')
					{
						if (c == 'P')
						{
							if (asString == "Parentheses")
							{
								return MAstToExpressionVisitor.VisitParentheses(ast);
							}
						}
					}
					else if (asString == "FieldAccess")
					{
						return MAstToExpressionVisitor.VisitFieldAccess(ast);
					}
					break;
				}
				case 12:
				{
					char c = asString[0];
					if (c != 'F')
					{
						if (c == 'N')
						{
							if (asString == "NullableType")
							{
								return MAstToExpressionVisitor.VisitNullableType(ast);
							}
						}
					}
					else if (asString == "FunctionType")
					{
						return MAstToExpressionVisitor.VisitFunctionType(ast);
					}
					break;
				}
				case 13:
					if (asString == "ElementAccess")
					{
						return MAstToExpressionVisitor.VisitElementAccess(ast);
					}
					break;
				case 14:
					if (asString == "NotImplemented")
					{
						return MAstToExpressionVisitor.VisitNotImplemented(ast);
					}
					break;
				case 17:
					if (asString == "SectionIdentifier")
					{
						return MAstToExpressionVisitor.VisitSectionIdentifier(ast);
					}
					break;
				case 18:
					if (asString == "ImplicitIdentifier")
					{
						return MAstToExpressionVisitor.VisitImplicitIdentifier(ast);
					}
					break;
				case 26:
					if (asString == "MultiFieldRecordProjection")
					{
						return MAstToExpressionVisitor.VisitMultiFieldRecordProjection(ast);
					}
					break;
				}
			}
			throw new NotSupportedException(Strings.UnsupportedMAStKind(ast["Kind"].AsString));
		}

		// Token: 0x0600A0CC RID: 41164 RVA: 0x00215574 File Offset: 0x00213774
		private static IExpression VisitBinary(RecordValue binary)
		{
			BinaryOperator2 binaryOperator = (BinaryOperator2)Enum.Parse(typeof(BinaryOperator2), binary["Operator"].AsString);
			IExpression expression = MAstToExpressionVisitor.VisitAst(binary["Left"]);
			IExpression expression2 = MAstToExpressionVisitor.VisitAst(binary["Right"]);
			return BinaryExpressionSyntaxNode.New(binaryOperator, expression, expression2, TokenRange.Null);
		}

		// Token: 0x0600A0CD RID: 41165 RVA: 0x002155D3 File Offset: 0x002137D3
		private static IExpression VisitConstant(RecordValue constant)
		{
			return ConstantExpressionSyntaxNode.New(constant["Value"]);
		}

		// Token: 0x0600A0CE RID: 41166 RVA: 0x002155E8 File Offset: 0x002137E8
		private static IExpression VisitElementAccess(RecordValue elementAccess)
		{
			if (elementAccess["IsOptional"].AsLogical.Equals(LogicalValue.True))
			{
				return new OptionalElementAccessExpressionSyntaxNode(MAstToExpressionVisitor.VisitAst(elementAccess["Collection"]), MAstToExpressionVisitor.VisitAst(elementAccess["Key"]), TokenRange.Null);
			}
			return new RequiredElementAccessExpressionSyntaxNode(MAstToExpressionVisitor.VisitAst(elementAccess["Collection"]), MAstToExpressionVisitor.VisitAst(elementAccess["Key"]), TokenRange.Null);
		}

		// Token: 0x0600A0CF RID: 41167 RVA: 0x00215666 File Offset: 0x00213866
		private static IExpression VisitExports(RecordValue exports)
		{
			return new ExportsExpressionSyntaxNode(MAstToExpressionVisitor.GetIdentifier(exports["Name"]), TokenRange.Null);
		}

		// Token: 0x0600A0D0 RID: 41168 RVA: 0x00215684 File Offset: 0x00213884
		private static IExpression VisitFieldAccess(RecordValue fieldAccess)
		{
			bool asBoolean = fieldAccess["IsOptional"].AsBoolean;
			IExpression expression = MAstToExpressionVisitor.VisitAst(fieldAccess["Expression"]);
			Identifier identifier = Identifier.New(fieldAccess["MemberName"].AsString);
			if (asBoolean)
			{
				return new OptionalFieldAccessExpressionSyntaxNode(expression, identifier);
			}
			return new RequiredFieldAccessExpressionSyntaxNode(expression, identifier);
		}

		// Token: 0x0600A0D1 RID: 41169 RVA: 0x002156D9 File Offset: 0x002138D9
		private static IExpression VisitFunction(RecordValue function)
		{
			return new FunctionExpressionSyntaxNode(MAstToExpressionVisitor.VisitAst(function["FunctionType"]) as IFunctionTypeExpression, MAstToExpressionVisitor.VisitAst(function["Expression"]));
		}

		// Token: 0x0600A0D2 RID: 41170 RVA: 0x00215708 File Offset: 0x00213908
		private static IExpression VisitFunctionType(RecordValue functionType)
		{
			ListValue asList = functionType["Parameters"].AsList;
			IParameter[] array = new IParameter[asList.Count];
			for (int i = 0; i < array.Length; i++)
			{
				Identifier identifier = MAstToExpressionVisitor.GetIdentifier(asList[i]["Identifier"]);
				IExpression expressionOrNull = MAstToExpressionVisitor.GetExpressionOrNull(asList[i]["Type"]);
				array[i] = new ParameterSyntaxNode(identifier, expressionOrNull);
			}
			return new FunctionTypeSyntaxNode(MAstToExpressionVisitor.GetExpressionOrNull(functionType["ReturnType"]), array, functionType["Min"].AsInteger32);
		}

		// Token: 0x0600A0D3 RID: 41171 RVA: 0x002157A0 File Offset: 0x002139A0
		private static IExpression VisitIdentifier(RecordValue identifier)
		{
			bool asBoolean = identifier["IsInclusive"].AsBoolean;
			Identifier identifier2 = Identifier.New(identifier["Name"].AsString);
			if (asBoolean)
			{
				return new InclusiveIdentifierExpressionSyntaxNode(identifier2);
			}
			if (identifier2 == Identifier.Underscore)
			{
				return new ImplicitIdentifierExpressionSyntaxNode();
			}
			return new ExclusiveIdentifierExpressionSyntaxNode(identifier2);
		}

		// Token: 0x0600A0D4 RID: 41172 RVA: 0x002157F8 File Offset: 0x002139F8
		private static IExpression VisitIf(RecordValue @if)
		{
			IExpression expression = MAstToExpressionVisitor.VisitAst(@if["Condition"]);
			IExpression expression2 = MAstToExpressionVisitor.VisitAst(@if["TrueCase"]);
			IExpression expression3 = MAstToExpressionVisitor.VisitAst(@if["FalseCase"]);
			return new IfExpressionSyntaxNode(expression, expression2, expression3, TokenRange.Null);
		}

		// Token: 0x0600A0D5 RID: 41173 RVA: 0x00215843 File Offset: 0x00213A43
		private static IExpression VisitImplicitIdentifier(RecordValue implicitIdentifier)
		{
			return new ImplicitIdentifierExpressionSyntaxNode();
		}

		// Token: 0x0600A0D6 RID: 41174 RVA: 0x0021584C File Offset: 0x00213A4C
		private static IExpression VisitInvocation(RecordValue invocation)
		{
			IExpression expression = MAstToExpressionVisitor.VisitAst(invocation["Function"]);
			ListValue asList = invocation["Arguments"].AsList;
			switch (asList.Count)
			{
			case 0:
				return new InvocationExpressionSyntaxNode0(expression);
			case 1:
				return new InvocationExpressionSyntaxNode1(expression, MAstToExpressionVisitor.VisitAst(asList[0]));
			case 2:
				return new InvocationExpressionSyntaxNode2(expression, MAstToExpressionVisitor.VisitAst(asList[0]), MAstToExpressionVisitor.VisitAst(asList[1]));
			default:
				return new InvocationExpressionSyntaxNodeN(expression, MAstToExpressionVisitor.GetIExpressionCollection(asList));
			}
		}

		// Token: 0x0600A0D7 RID: 41175 RVA: 0x002158DB File Offset: 0x00213ADB
		private static IExpression VisitLet(RecordValue let)
		{
			return new LetExpressionSyntaxNode(MAstToExpressionVisitor.GetVariableInitializerCollection(let["Variables"].AsList), MAstToExpressionVisitor.VisitAst(let["Expression"]));
		}

		// Token: 0x0600A0D8 RID: 41176 RVA: 0x00215907 File Offset: 0x00213B07
		private static IExpression VisitList(RecordValue list)
		{
			return new ListExpressionSyntaxNode(MAstToExpressionVisitor.GetIExpressionCollection(list["Members"].AsList));
		}

		// Token: 0x0600A0D9 RID: 41177 RVA: 0x00215923 File Offset: 0x00213B23
		private static IExpression VisitListType(RecordValue listType)
		{
			return new ListTypeSyntaxNode(MAstToExpressionVisitor.VisitAst(listType["ItemType"]), TokenRange.Null);
		}

		// Token: 0x0600A0DA RID: 41178 RVA: 0x00215940 File Offset: 0x00213B40
		private static IExpression VisitMultiFieldRecordProjection(RecordValue multiFieldRecordProjection)
		{
			if (multiFieldRecordProjection["IsOptional"].AsLogical.Equals(LogicalValue.True))
			{
				return new OptionalMultiFieldRecordProjectionExpressionSyntaxNode(MAstToExpressionVisitor.VisitAst(multiFieldRecordProjection["Expression"]), MAstToExpressionVisitor.GetIdentifiersCollection(multiFieldRecordProjection["MemberNames"].AsList));
			}
			return new RequiredMultiFieldRecordProjectionExpressionSyntaxNode(MAstToExpressionVisitor.VisitAst(multiFieldRecordProjection["Expression"]), MAstToExpressionVisitor.GetIdentifiersCollection(multiFieldRecordProjection["MemberNames"].AsList));
		}

		// Token: 0x0600A0DB RID: 41179 RVA: 0x002159BE File Offset: 0x00213BBE
		private static IExpression VisitNotImplemented(RecordValue notImplemented)
		{
			return new NotImplementedExpressionSyntaxNode();
		}

		// Token: 0x0600A0DC RID: 41180 RVA: 0x002159C5 File Offset: 0x00213BC5
		private static IExpression VisitNullableType(RecordValue nullableType)
		{
			return new NullableTypeSyntaxNode(MAstToExpressionVisitor.VisitAst(nullableType["ItemType"]), TokenRange.Null);
		}

		// Token: 0x0600A0DD RID: 41181 RVA: 0x002159E1 File Offset: 0x00213BE1
		private static IExpression VisitParentheses(RecordValue parentheses)
		{
			return new ParenthesesExpressionSyntaxNode(MAstToExpressionVisitor.VisitAst(parentheses["Expression"]), TokenRange.Null);
		}

		// Token: 0x0600A0DE RID: 41182 RVA: 0x002159FD File Offset: 0x00213BFD
		private static IExpression VisitRangeList(RecordValue rangeList)
		{
			return new RangeListExpressionSyntaxNode(MAstToExpressionVisitor.GetIRangeExpressionCollection(rangeList["Members"].AsList), TokenRange.Null);
		}

		// Token: 0x0600A0DF RID: 41183 RVA: 0x00215A1E File Offset: 0x00213C1E
		private static IExpression VisitRecord(RecordValue record)
		{
			return new RecordExpressionSyntaxNode(MAstToExpressionVisitor.GetIdentifier(record["Identifier"]), MAstToExpressionVisitor.GetVariableInitializerCollection(record["Members"].AsList), TokenRange.Null);
		}

		// Token: 0x0600A0E0 RID: 41184 RVA: 0x00215A4F File Offset: 0x00213C4F
		private static IExpression VisitRecordType(RecordValue recordType)
		{
			return new RecordTypeSyntaxNode(MAstToExpressionVisitor.GetFieldTypeCollection(recordType["Fields"].AsList), recordType["Wildcard"].AsBoolean, TokenRange.Null);
		}

		// Token: 0x0600A0E1 RID: 41185 RVA: 0x00215A80 File Offset: 0x00213C80
		private static IExpression VisitSectionIdentifier(RecordValue sectionIdentifier)
		{
			return new SectionIdentifierExpressionSyntaxNode(MAstToExpressionVisitor.GetIdentifier(sectionIdentifier["Section"]), MAstToExpressionVisitor.GetIdentifier(sectionIdentifier["Name"]), TokenRange.Null);
		}

		// Token: 0x0600A0E2 RID: 41186 RVA: 0x00215AAC File Offset: 0x00213CAC
		private static IExpression VisitTableType(RecordValue tableType)
		{
			return new TableTypeSyntaxNode(MAstToExpressionVisitor.VisitAst(tableType["RowType"]), TokenRange.Null);
		}

		// Token: 0x0600A0E3 RID: 41187 RVA: 0x00215AC8 File Offset: 0x00213CC8
		private static IExpression VisitThrow(RecordValue @throw)
		{
			return new ThrowExpressionSyntaxNode(MAstToExpressionVisitor.VisitAst(@throw["Expression"]));
		}

		// Token: 0x0600A0E4 RID: 41188 RVA: 0x00215ADF File Offset: 0x00213CDF
		private static IExpression VisitTryCatch(RecordValue tryCatch)
		{
			return new TryCatchExpressionSyntaxNode(MAstToExpressionVisitor.VisitAst(tryCatch["Try"]), MAstToExpressionVisitor.GetExceptionCase(tryCatch["ExceptionCase"].AsRecord), TokenRange.Null);
		}

		// Token: 0x0600A0E5 RID: 41189 RVA: 0x00215B10 File Offset: 0x00213D10
		private static IExpression VisitType(RecordValue type)
		{
			return new TypeExpressionSyntaxNode(MAstToExpressionVisitor.VisitAst(type["Expression"]), TokenRange.Null);
		}

		// Token: 0x0600A0E6 RID: 41190 RVA: 0x00215B2C File Offset: 0x00213D2C
		private static IExpression VisitUnary(RecordValue unary)
		{
			UnaryOperator2 unaryOperator = (UnaryOperator2)Enum.Parse(typeof(UnaryOperator2), unary["Operator"].AsString);
			IExpression expression = MAstToExpressionVisitor.VisitAst(unary["Expression"]);
			return UnaryExpressionSyntaxNode.New(unaryOperator, expression, TokenRange.Null);
		}

		// Token: 0x0600A0E7 RID: 41191 RVA: 0x00215B79 File Offset: 0x00213D79
		private static IExpression VisitVerbatim(RecordValue verbatim)
		{
			return new VerbatimExpressionSyntaxNode(MAstToExpressionVisitor.VisitAst(verbatim["Text"]) as IConstantExpression2, TokenRange.Null);
		}

		// Token: 0x0600A0E8 RID: 41192 RVA: 0x00215B9A File Offset: 0x00213D9A
		private static Identifier GetIdentifier(Value identifierValue)
		{
			if (!identifierValue.IsNull)
			{
				return Identifier.New(identifierValue.AsString);
			}
			return null;
		}

		// Token: 0x0600A0E9 RID: 41193 RVA: 0x00215BB1 File Offset: 0x00213DB1
		private static IExpression GetExpressionOrNull(Value value)
		{
			if (!value.IsNull)
			{
				return MAstToExpressionVisitor.VisitAst(value);
			}
			return null;
		}

		// Token: 0x0600A0EA RID: 41194 RVA: 0x00215BC4 File Offset: 0x00213DC4
		private static VariableInitializer[] GetVariableInitializerCollection(ListValue values)
		{
			VariableInitializer[] array = new VariableInitializer[values.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new VariableInitializer(Identifier.New(values[i]["Name"].AsString), MAstToExpressionVisitor.VisitAst(values[i]["Value"]));
			}
			return array;
		}

		// Token: 0x0600A0EB RID: 41195 RVA: 0x00215C2C File Offset: 0x00213E2C
		private static IRangeExpression[] GetIRangeExpressionCollection(ListValue values)
		{
			IRangeExpression[] array = new IRangeExpression[values.Count];
			for (int i = 0; i < array.Length; i++)
			{
				IExpression expression = MAstToExpressionVisitor.VisitAst(values[i]["Lower"]);
				IExpression expression2 = MAstToExpressionVisitor.VisitAst(values[i]["Upper"]);
				if (expression != expression2)
				{
					array[i] = new BinaryRangeExpressionSyntaxNode(expression, expression2, TokenRange.Null);
				}
				else
				{
					array[i] = new UnaryRangeExpressionSyntaxNode(expression, TokenRange.Null);
				}
			}
			return array;
		}

		// Token: 0x0600A0EC RID: 41196 RVA: 0x00215CA8 File Offset: 0x00213EA8
		private static IExpression[] GetIExpressionCollection(ListValue values)
		{
			IExpression[] array = new IExpression[values.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = MAstToExpressionVisitor.VisitAst(values[i]);
			}
			return array;
		}

		// Token: 0x0600A0ED RID: 41197 RVA: 0x00215CE0 File Offset: 0x00213EE0
		private static Identifier[] GetIdentifiersCollection(ListValue values)
		{
			Identifier[] array = new Identifier[values.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = MAstToExpressionVisitor.GetIdentifier(values[i]);
			}
			return array;
		}

		// Token: 0x0600A0EE RID: 41198 RVA: 0x00215D18 File Offset: 0x00213F18
		private static IFieldType[] GetFieldTypeCollection(ListValue values)
		{
			IFieldType[] array = new IFieldType[values.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new FieldTypeSyntaxNode(MAstToExpressionVisitor.GetIdentifier(values[i]["Name"]), MAstToExpressionVisitor.VisitAst(values[i]["Type"]), values[i]["Optional"].AsBoolean);
			}
			return array;
		}

		// Token: 0x0600A0EF RID: 41199 RVA: 0x00215D8A File Offset: 0x00213F8A
		private static TryCatchExceptionCase GetExceptionCase(RecordValue exceptionCaseValue)
		{
			return new TryCatchExceptionCase(MAstToExpressionVisitor.GetIdentifier(exceptionCaseValue["Variable"]), MAstToExpressionVisitor.VisitAst(exceptionCaseValue["Expression"]));
		}
	}
}
