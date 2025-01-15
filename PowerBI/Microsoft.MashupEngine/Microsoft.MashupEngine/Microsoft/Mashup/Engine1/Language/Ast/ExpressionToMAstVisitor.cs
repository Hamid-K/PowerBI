using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018A8 RID: 6312
	internal class ExpressionToMAstVisitor
	{
		// Token: 0x0600A095 RID: 41109 RVA: 0x00213EE4 File Offset: 0x002120E4
		public static RecordValue ToMAst(IExpression expression)
		{
			return new ExpressionToMAstVisitor().VisitExpression(expression);
		}

		// Token: 0x0600A096 RID: 41110 RVA: 0x00213EF4 File Offset: 0x002120F4
		protected virtual RecordValue VisitExpression(IExpression expression)
		{
			switch (expression.Kind)
			{
			case ExpressionKind.Binary:
				return this.VisitBinary((IBinaryExpression)expression);
			case ExpressionKind.Constant:
				return this.VisitConstant((IConstantExpression)expression);
			case ExpressionKind.ElementAccess:
				return this.VisitElementAccess((IElementAccessExpression)expression);
			case ExpressionKind.Exports:
				return this.VisitExports((IExportsExpression)expression);
			case ExpressionKind.FieldAccess:
				return this.VisitFieldAccess((IFieldAccessExpression)expression);
			case ExpressionKind.Function:
				return this.VisitFunction((IFunctionExpression)expression);
			case ExpressionKind.Identifier:
				return this.VisitIdentifier((IIdentifierExpression)expression);
			case ExpressionKind.If:
				return this.VisitIf((IIfExpression)expression);
			case ExpressionKind.Invocation:
				return this.VisitInvocation((IInvocationExpression)expression);
			case ExpressionKind.Let:
				return this.VisitLet((ILetExpression)expression);
			case ExpressionKind.List:
				return this.VisitList((IListExpression)expression);
			case ExpressionKind.MultiFieldRecordProjection:
				return this.VisitMultiFieldRecordProjection((IMultiFieldRecordProjectionExpression)expression);
			case ExpressionKind.NotImplemented:
				return this.VisitNotImplemented((INotImplementedExpression)expression);
			case ExpressionKind.Parentheses:
				return this.VisitParentheses((IParenthesesExpression)expression);
			case ExpressionKind.RangeList:
				return this.VisitRangeList((IRangeListExpression)expression);
			case ExpressionKind.Record:
				return this.VisitRecord((IRecordExpression)expression);
			case ExpressionKind.SectionIdentifier:
				return this.VisitSectionIdentifier((ISectionIdentifierExpression)expression);
			case ExpressionKind.Throw:
				return this.VisitThrow((IThrowExpression)expression);
			case ExpressionKind.TryCatch:
				return this.VisitTryCatch((ITryCatchExpression)expression);
			case ExpressionKind.Unary:
				return this.VisitUnary((IUnaryExpression)expression);
			case ExpressionKind.Verbatim:
				return this.VisitVerbatim((IVerbatimExpression)expression);
			case ExpressionKind.ImplicitIdentifier:
				return this.VisitImplicitIdentifier((IImplicitIdentifierExpression)expression);
			case ExpressionKind.Type:
				return this.VisitType((ITypeExpression)expression);
			case ExpressionKind.RecordType:
				return this.VisitRecordType((IRecordTypeExpression)expression);
			case ExpressionKind.ListType:
				return this.VisitListType((IListTypeExpression)expression);
			case ExpressionKind.TableType:
				return this.VisitTableType((ITableTypeExpression)expression);
			case ExpressionKind.NullableType:
				return this.VisitNullableType((INullableTypeExpression)expression);
			case ExpressionKind.FunctionType:
				return this.VisitFunctionType((IFunctionTypeExpression)expression);
			default:
				throw new NotSupportedException(Strings.UnsupportedExpressionKind(expression.Kind));
			}
		}

		// Token: 0x0600A097 RID: 41111 RVA: 0x0021410C File Offset: 0x0021230C
		protected virtual RecordValue VisitBinary(IBinaryExpression binary)
		{
			return RecordValue.New(MAst.BinaryKeys, new Value[]
			{
				MAst.BinaryKind,
				TextValue.New(binary.Operator.ToString()),
				this.VisitExpression(binary.Left),
				this.VisitExpression(binary.Right)
			});
		}

		// Token: 0x0600A098 RID: 41112 RVA: 0x0021416B File Offset: 0x0021236B
		protected virtual RecordValue VisitConstant(IConstantExpression constant)
		{
			return RecordValue.New(MAst.ConstantKeys, new Value[]
			{
				MAst.ConstantKind,
				constant.Value
			});
		}

		// Token: 0x0600A099 RID: 41113 RVA: 0x00214190 File Offset: 0x00212390
		protected virtual RecordValue VisitElementAccess(IElementAccessExpression elementAccess)
		{
			return RecordValue.New(MAst.ElementAccessKeys, new Value[]
			{
				MAst.ElementAccessKind,
				this.VisitExpression(elementAccess.Collection),
				this.VisitExpression(elementAccess.Key),
				LogicalValue.New(elementAccess.IsOptional)
			});
		}

		// Token: 0x0600A09A RID: 41114 RVA: 0x002141E1 File Offset: 0x002123E1
		protected virtual RecordValue VisitExports(IExportsExpression exports)
		{
			return RecordValue.New(MAst.ExportsKeys, new Value[]
			{
				MAst.ExportsKind,
				this.GetIdentifier(exports.Name)
			});
		}

		// Token: 0x0600A09B RID: 41115 RVA: 0x0021420C File Offset: 0x0021240C
		protected virtual RecordValue VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			return RecordValue.New(MAst.FieldAccessKeys, new Value[]
			{
				MAst.FieldAccessKind,
				LogicalValue.New(fieldAccess.IsOptional),
				TextValue.New(fieldAccess.MemberName),
				this.VisitExpression(fieldAccess.Expression)
			});
		}

		// Token: 0x0600A09C RID: 41116 RVA: 0x00214261 File Offset: 0x00212461
		protected virtual RecordValue VisitFunction(IFunctionExpression function)
		{
			return RecordValue.New(MAst.FunctionKeys, new Value[]
			{
				MAst.FunctionKind,
				this.VisitExpression(function.FunctionType),
				this.VisitExpression(function.Expression)
			});
		}

		// Token: 0x0600A09D RID: 41117 RVA: 0x0021429C File Offset: 0x0021249C
		protected virtual RecordValue VisitFunctionType(IFunctionTypeExpression functionType)
		{
			Value[] array = new Value[functionType.Parameters.Count];
			for (int i = 0; i < array.Length; i++)
			{
				Value valueOrNull = this.GetValueOrNull(functionType.Parameters[i].Type);
				array[i] = RecordValue.New(MAst.ParameterKeys, new Value[]
				{
					this.GetIdentifier(functionType.Parameters[i].Identifier),
					valueOrNull
				});
			}
			Value valueOrNull2 = this.GetValueOrNull(functionType.ReturnType);
			return RecordValue.New(MAst.FunctionTypeKeys, new Value[]
			{
				MAst.FunctionTypeKind,
				valueOrNull2,
				ListValue.New(array),
				NumberValue.New(functionType.Min)
			});
		}

		// Token: 0x0600A09E RID: 41118 RVA: 0x00214351 File Offset: 0x00212551
		protected virtual RecordValue VisitIdentifier(IIdentifierExpression identifier)
		{
			return RecordValue.New(MAst.IdentifierKeys, new Value[]
			{
				MAst.IdentifierKind,
				TextValue.New(identifier.Name),
				LogicalValue.New(identifier.IsInclusive)
			});
		}

		// Token: 0x0600A09F RID: 41119 RVA: 0x0021438C File Offset: 0x0021258C
		protected virtual RecordValue VisitIf(IIfExpression @if)
		{
			return RecordValue.New(MAst.IfKeys, new Value[]
			{
				MAst.IfKind,
				this.VisitExpression(@if.Condition),
				this.VisitExpression(@if.TrueCase),
				this.VisitExpression(@if.FalseCase)
			});
		}

		// Token: 0x0600A0A0 RID: 41120 RVA: 0x002143DE File Offset: 0x002125DE
		protected virtual RecordValue VisitImplicitIdentifier(IImplicitIdentifierExpression implicitIdentifier)
		{
			return RecordValue.New(MAst.IdentifierKeys, new Value[]
			{
				MAst.ImplicitIdentifierKind,
				TextValue.New(implicitIdentifier.Name),
				LogicalValue.New(implicitIdentifier.IsInclusive)
			});
		}

		// Token: 0x0600A0A1 RID: 41121 RVA: 0x00214419 File Offset: 0x00212619
		protected virtual RecordValue VisitInvocation(IInvocationExpression invocation)
		{
			return RecordValue.New(MAst.InvocationKeys, new Value[]
			{
				MAst.InvocationKind,
				this.VisitExpression(invocation.Function),
				this.GetExpressionListValue(invocation.Arguments)
			});
		}

		// Token: 0x0600A0A2 RID: 41122 RVA: 0x00214451 File Offset: 0x00212651
		protected virtual RecordValue VisitLet(ILetExpression let)
		{
			return RecordValue.New(MAst.LetKeys, new Value[]
			{
				MAst.LetKind,
				this.GetVariableInitializerValue(let.Variables),
				this.VisitExpression(let.Expression)
			});
		}

		// Token: 0x0600A0A3 RID: 41123 RVA: 0x00214489 File Offset: 0x00212689
		protected virtual RecordValue VisitList(IListExpression list)
		{
			return RecordValue.New(MAst.ListKeys, new Value[]
			{
				MAst.ListKind,
				this.GetExpressionListValue(list.Members)
			});
		}

		// Token: 0x0600A0A4 RID: 41124 RVA: 0x002144B2 File Offset: 0x002126B2
		protected virtual RecordValue VisitListType(IListTypeExpression listType)
		{
			return RecordValue.New(MAst.ListTypeKeys, new Value[]
			{
				MAst.ListTypeKind,
				this.VisitExpression(listType.ItemType)
			});
		}

		// Token: 0x0600A0A5 RID: 41125 RVA: 0x002144DC File Offset: 0x002126DC
		protected virtual RecordValue VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
		{
			return RecordValue.New(MAst.MultiFieldRecordProjectionKeys, new Value[]
			{
				MAst.MultiFieldRecordProjectionKind,
				this.VisitExpression(multiFieldRecordProjection.Expression),
				this.GetIdentifiersValue(multiFieldRecordProjection.MemberNames),
				LogicalValue.New(multiFieldRecordProjection.IsOptional)
			});
		}

		// Token: 0x0600A0A6 RID: 41126 RVA: 0x0021452D File Offset: 0x0021272D
		protected virtual RecordValue VisitNotImplemented(INotImplementedExpression notImplemented)
		{
			return RecordValue.New(MAst.NotImplementedKeys, new Value[] { MAst.NotImplementedKind });
		}

		// Token: 0x0600A0A7 RID: 41127 RVA: 0x00214547 File Offset: 0x00212747
		protected virtual RecordValue VisitNullableType(INullableTypeExpression nullableType)
		{
			return RecordValue.New(MAst.NullableTypeKeys, new Value[]
			{
				MAst.NullableTypeKind,
				this.VisitExpression(nullableType.ItemType)
			});
		}

		// Token: 0x0600A0A8 RID: 41128 RVA: 0x00214570 File Offset: 0x00212770
		protected virtual RecordValue VisitParentheses(IParenthesesExpression parentheses)
		{
			return RecordValue.New(MAst.ParenthesesKeys, new Value[]
			{
				MAst.ParenthesesKind,
				this.VisitExpression(parentheses.Expression)
			});
		}

		// Token: 0x0600A0A9 RID: 41129 RVA: 0x00214599 File Offset: 0x00212799
		protected virtual RecordValue VisitRangeList(IRangeListExpression rangeList)
		{
			return RecordValue.New(MAst.RangeListKeys, new Value[]
			{
				MAst.RangeListKind,
				this.GetRangeExpressionValue(rangeList.Members)
			});
		}

		// Token: 0x0600A0AA RID: 41130 RVA: 0x002145C2 File Offset: 0x002127C2
		protected virtual RecordValue VisitRecord(IRecordExpression record)
		{
			return RecordValue.New(MAst.RecordKeys, new Value[]
			{
				MAst.RecordKind,
				this.GetIdentifier(record.Identifier),
				this.GetVariableInitializerValue(record.Members)
			});
		}

		// Token: 0x0600A0AB RID: 41131 RVA: 0x002145FA File Offset: 0x002127FA
		protected virtual RecordValue VisitRecordType(IRecordTypeExpression recordType)
		{
			return RecordValue.New(MAst.RecordTypeKeys, new Value[]
			{
				MAst.RecordTypeKind,
				this.GetFieldTypeValue(recordType.Fields),
				LogicalValue.New(recordType.Wildcard)
			});
		}

		// Token: 0x0600A0AC RID: 41132 RVA: 0x00214631 File Offset: 0x00212831
		protected virtual RecordValue VisitSectionIdentifier(ISectionIdentifierExpression sectionIdentifier)
		{
			return RecordValue.New(MAst.SectionIdentifierKeys, new Value[]
			{
				MAst.SectionIdentifierKind,
				this.GetIdentifier(sectionIdentifier.Section),
				this.GetIdentifier(sectionIdentifier.Name)
			});
		}

		// Token: 0x0600A0AD RID: 41133 RVA: 0x00214669 File Offset: 0x00212869
		protected virtual RecordValue VisitTableType(ITableTypeExpression tableType)
		{
			return RecordValue.New(MAst.TableTypeKeys, new Value[]
			{
				MAst.TableTypeKind,
				this.VisitExpression(tableType.RowType)
			});
		}

		// Token: 0x0600A0AE RID: 41134 RVA: 0x00214692 File Offset: 0x00212892
		protected virtual RecordValue VisitThrow(IThrowExpression @throw)
		{
			return RecordValue.New(MAst.ThrowKeys, new Value[]
			{
				MAst.ThrowKind,
				this.VisitExpression(@throw.Expression)
			});
		}

		// Token: 0x0600A0AF RID: 41135 RVA: 0x002146BB File Offset: 0x002128BB
		protected virtual RecordValue VisitTryCatch(ITryCatchExpression tryCatch)
		{
			return RecordValue.New(MAst.TryCatchKeys, new Value[]
			{
				MAst.TryCatchKind,
				this.VisitExpression(tryCatch.Try),
				this.GetExceptionCaseValue(tryCatch.ExceptionCase)
			});
		}

		// Token: 0x0600A0B0 RID: 41136 RVA: 0x002146F3 File Offset: 0x002128F3
		protected virtual RecordValue VisitType(ITypeExpression type)
		{
			return RecordValue.New(MAst.TypeKeys, new Value[]
			{
				MAst.TypeKind,
				this.VisitExpression(type.Expression)
			});
		}

		// Token: 0x0600A0B1 RID: 41137 RVA: 0x0021471C File Offset: 0x0021291C
		protected virtual RecordValue VisitUnary(IUnaryExpression unary)
		{
			return RecordValue.New(MAst.UnaryKeys, new Value[]
			{
				MAst.UnaryKind,
				TextValue.New(unary.Operator.ToString()),
				this.VisitExpression(unary.Expression)
			});
		}

		// Token: 0x0600A0B2 RID: 41138 RVA: 0x0021476C File Offset: 0x0021296C
		protected virtual RecordValue VisitVerbatim(IVerbatimExpression verbatim)
		{
			return RecordValue.New(MAst.VerbatimKeys, new Value[]
			{
				MAst.VerbatimKind,
				this.VisitExpression(verbatim.Text)
			});
		}

		// Token: 0x0600A0B3 RID: 41139 RVA: 0x00214795 File Offset: 0x00212995
		protected virtual Value GetIdentifier(Identifier identifier)
		{
			if (identifier == null)
			{
				return Value.Null;
			}
			return TextValue.New(identifier.Name);
		}

		// Token: 0x0600A0B4 RID: 41140 RVA: 0x002147B1 File Offset: 0x002129B1
		protected virtual Value GetValueOrNull(IExpression expression)
		{
			if (expression != null)
			{
				return this.VisitExpression(expression);
			}
			return Value.Null;
		}

		// Token: 0x0600A0B5 RID: 41141 RVA: 0x002147C4 File Offset: 0x002129C4
		protected virtual ListValue GetVariableInitializerValue(IList<VariableInitializer> variables)
		{
			RecordValue[] array = new RecordValue[variables.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = RecordValue.New(MAst.VariableKeys, new Value[]
				{
					TextValue.New(variables[i].Name.Name),
					this.VisitExpression(variables[i].Value)
				});
			}
			Value[] array2 = array;
			return ListValue.New(array2);
		}

		// Token: 0x0600A0B6 RID: 41142 RVA: 0x0021483C File Offset: 0x00212A3C
		protected virtual ListValue GetExpressionListValue(IList<IExpression> expressions)
		{
			RecordValue[] array = new RecordValue[expressions.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.VisitExpression(expressions[i]);
			}
			Value[] array2 = array;
			return ListValue.New(array2);
		}

		// Token: 0x0600A0B7 RID: 41143 RVA: 0x0021487C File Offset: 0x00212A7C
		protected virtual ListValue GetRangeExpressionValue(IList<IRangeExpression> rangeExpressions)
		{
			RecordValue[] array = new RecordValue[rangeExpressions.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = RecordValue.New(MAst.RangeExpressionKeys, new Value[]
				{
					this.VisitExpression(rangeExpressions[i].Lower),
					this.VisitExpression(rangeExpressions[i].Upper)
				});
			}
			Value[] array2 = array;
			return ListValue.New(array2);
		}

		// Token: 0x0600A0B8 RID: 41144 RVA: 0x002148E8 File Offset: 0x00212AE8
		protected virtual ListValue GetIdentifiersValue(IList<Identifier> identifiers)
		{
			Value[] array = new Value[identifiers.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GetIdentifier(identifiers[i]);
			}
			return ListValue.New(array);
		}

		// Token: 0x0600A0B9 RID: 41145 RVA: 0x00214928 File Offset: 0x00212B28
		protected virtual ListValue GetFieldTypeValue(IList<IFieldType> fieldTypes)
		{
			RecordValue[] array = new RecordValue[fieldTypes.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = RecordValue.New(MAst.FieldTypeKeys, new Value[]
				{
					this.GetIdentifier(fieldTypes[i].Name),
					this.VisitExpression(fieldTypes[i].Type),
					LogicalValue.New(fieldTypes[i].Optional)
				});
			}
			Value[] array2 = array;
			return ListValue.New(array2);
		}

		// Token: 0x0600A0BA RID: 41146 RVA: 0x002149A8 File Offset: 0x00212BA8
		protected virtual RecordValue GetExceptionCaseValue(TryCatchExceptionCase exceptionCase)
		{
			return RecordValue.New(MAst.ExceptionCaseKeys, new Value[]
			{
				this.GetIdentifier(exceptionCase.Variable),
				this.VisitExpression(exceptionCase.Expression)
			});
		}
	}
}
