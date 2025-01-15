using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000101 RID: 257
	public abstract class ReadOnlyAstVisitor
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x00004DAC File Offset: 0x00002FAC
		public ReadOnlyAstVisitor()
			: this(1000)
		{
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00004DB9 File Offset: 0x00002FB9
		public ReadOnlyAstVisitor(int maxDepth)
		{
			this.maxDepth = maxDepth;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00004DC8 File Offset: 0x00002FC8
		protected void Visit(object node)
		{
			IExpression expression = node as IExpression;
			if (expression != null)
			{
				this.VisitExpression(expression);
				return;
			}
			IDocument document = node as IDocument;
			if (document != null)
			{
				this.VisitDocument(document);
				return;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00004DFE File Offset: 0x00002FFE
		protected virtual void VisitBinary(IBinaryExpression binary)
		{
			this.VisitExpression(binary.Left);
			this.VisitExpression(binary.Right);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitConstant(IConstantExpression2 constant)
		{
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00004E18 File Offset: 0x00003018
		protected virtual void VisitDocument(IDocument document)
		{
			DocumentKind kind = document.Kind;
			if (kind == DocumentKind.Section)
			{
				this.VisitModuleDocument((ISectionDocument)document);
				return;
			}
			if (kind != DocumentKind.Expression)
			{
				throw new InvalidOperationException();
			}
			this.VisitExpressionDocument((IExpressionDocument)document);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00004E54 File Offset: 0x00003054
		protected virtual void VisitExpressionDocument(IExpressionDocument document)
		{
			this.VisitExpression(document.Expression);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00004E62 File Offset: 0x00003062
		protected virtual void VisitList(IListExpression list)
		{
			this.VisitListElements(list.Members);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00004E70 File Offset: 0x00003070
		protected virtual void VisitRangeList(IRangeListExpression rangeList)
		{
			this.VisitListElements(rangeList.Members);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00004E7E File Offset: 0x0000307E
		protected virtual void VisitRangeExpression(IRangeExpression range)
		{
			this.VisitExpression(range.Lower);
			this.VisitExpression(range.Upper);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitExports(IExportsExpression expression)
		{
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00004E98 File Offset: 0x00003098
		protected void VisitOptionalExpression(IExpression expression)
		{
			if (expression == null)
			{
				return;
			}
			this.VisitExpression(expression);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00004EA8 File Offset: 0x000030A8
		protected virtual void VisitExpression(IExpression expression)
		{
			if (this.currentDepth > this.maxDepth)
			{
				throw this.NewDepthLimitExceededException();
			}
			this.currentDepth++;
			try
			{
				switch (expression.Kind)
				{
				case ExpressionKind.Binary:
					this.VisitBinary((IBinaryExpression)expression);
					break;
				case ExpressionKind.Constant:
					this.VisitConstant((IConstantExpression2)expression);
					break;
				case ExpressionKind.ElementAccess:
					this.VisitElementAccess((IElementAccessExpression)expression);
					break;
				case ExpressionKind.Exports:
					this.VisitExports((IExportsExpression)expression);
					break;
				case ExpressionKind.FieldAccess:
					this.VisitFieldAccess((IFieldAccessExpression)expression);
					break;
				case ExpressionKind.Function:
					this.VisitFunction((IFunctionExpression)expression);
					break;
				case ExpressionKind.Identifier:
					this.VisitIdentifier((IIdentifierExpression)expression);
					break;
				case ExpressionKind.If:
					this.VisitIf((IIfExpression)expression);
					break;
				case ExpressionKind.Invocation:
					this.VisitInvocation((IInvocationExpression)expression);
					break;
				case ExpressionKind.Let:
					this.VisitLet((ILetExpression)expression);
					break;
				case ExpressionKind.List:
					this.VisitList((IListExpression)expression);
					break;
				case ExpressionKind.MultiFieldRecordProjection:
					this.VisitMultiFieldRecordProjection((IMultiFieldRecordProjectionExpression)expression);
					break;
				case ExpressionKind.NotImplemented:
					this.VisitNotImplemented((INotImplementedExpression)expression);
					break;
				case ExpressionKind.Parentheses:
					this.VisitParentheses((IParenthesesExpression)expression);
					break;
				case ExpressionKind.RangeList:
					this.VisitRangeList((IRangeListExpression)expression);
					break;
				case ExpressionKind.Record:
					this.VisitRecord((IRecordExpression)expression);
					break;
				case ExpressionKind.SectionIdentifier:
					this.VisitSectionIdentifier((ISectionIdentifierExpression)expression);
					break;
				case ExpressionKind.Throw:
					this.VisitThrow((IThrowExpression)expression);
					break;
				case ExpressionKind.TryCatch:
					this.VisitTryCatch((ITryCatchExpression)expression);
					break;
				case ExpressionKind.Unary:
					this.VisitUnary((IUnaryExpression)expression);
					break;
				case ExpressionKind.Verbatim:
					this.VisitVerbatim((IVerbatimExpression)expression);
					break;
				case ExpressionKind.ImplicitIdentifier:
					this.VisitImplicitIdentifier((IImplicitIdentifierExpression)expression);
					break;
				case ExpressionKind.Type:
					this.VisitType((ITypeExpression)expression);
					break;
				case ExpressionKind.RecordType:
					this.VisitRecordType((IRecordTypeExpression)expression);
					break;
				case ExpressionKind.ListType:
					this.VisitListType((IListTypeExpression)expression);
					break;
				case ExpressionKind.TableType:
					this.VisitTableType((ITableTypeExpression)expression);
					break;
				case ExpressionKind.NullableType:
					this.VisitNullableType((INullableTypeExpression)expression);
					break;
				case ExpressionKind.FunctionType:
					this.VisitFunctionType((IFunctionTypeExpression)expression);
					break;
				default:
					throw new InvalidOperationException();
				}
			}
			finally
			{
				this.currentDepth--;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00005150 File Offset: 0x00003350
		protected virtual void VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			this.VisitExpression(fieldAccess.Expression);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000515E File Offset: 0x0000335E
		protected virtual void VisitFunction(IFunctionExpression function)
		{
			this.VisitSignature(function.FunctionType);
			this.VisitExpression(function.Expression);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitIdentifier(IIdentifierExpression identifier)
		{
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitSectionIdentifier(ISectionIdentifierExpression sectionIdentifier)
		{
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00005178 File Offset: 0x00003378
		protected virtual void VisitIf(IIfExpression @if)
		{
			this.VisitExpression(@if.Condition);
			this.VisitExpression(@if.TrueCase);
			this.VisitExpression(@if.FalseCase);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitImplicitIdentifier(IImplicitIdentifierExpression implicitIdentifier)
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000519E File Offset: 0x0000339E
		protected virtual void VisitInitializer(VariableInitializer member)
		{
			this.VisitExpression(member.Value);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000051AD File Offset: 0x000033AD
		protected virtual void VisitElementAccess(IElementAccessExpression elementAccess)
		{
			this.VisitExpression(elementAccess.Collection);
			this.VisitExpression(elementAccess.Key);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000051C7 File Offset: 0x000033C7
		protected virtual void VisitInvocation(IInvocationExpression invocation)
		{
			this.VisitExpression(invocation.Function);
			this.VisitListElements(invocation.Arguments);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000051E1 File Offset: 0x000033E1
		protected virtual void VisitLet(ILetExpression let)
		{
			this.VisitListElements(let.Variables);
			this.VisitExpression(let.Expression);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000051FB File Offset: 0x000033FB
		protected virtual void VisitModule(ISection module)
		{
			this.VisitListElements(module.Members);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00005209 File Offset: 0x00003409
		protected virtual void VisitModuleDocument(ISectionDocument document)
		{
			this.VisitModule(document.Section);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00005217 File Offset: 0x00003417
		protected virtual void VisitModuleMember(ISectionMember moduleMember)
		{
			this.VisitOptionalExpression(moduleMember.Value);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00005225 File Offset: 0x00003425
		protected virtual void VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
		{
			this.VisitExpression(multiFieldRecordProjection.Expression);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitNotImplemented(INotImplementedExpression notImplemented)
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00005233 File Offset: 0x00003433
		protected virtual void VisitType(ITypeExpression type)
		{
			this.VisitExpression(type.Expression);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00005241 File Offset: 0x00003441
		protected virtual void VisitListType(IListTypeExpression listType)
		{
			this.VisitExpression(listType.ItemType);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000524F File Offset: 0x0000344F
		protected virtual void VisitTableType(ITableTypeExpression tableType)
		{
			this.VisitExpression(tableType.RowType);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000525D File Offset: 0x0000345D
		protected virtual void VisitNullableType(INullableTypeExpression nullableType)
		{
			this.VisitExpression(nullableType.ItemType);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000526B File Offset: 0x0000346B
		protected virtual void VisitFieldType(IFieldType fieldType)
		{
			this.VisitOptionalExpression(fieldType.Type);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00005279 File Offset: 0x00003479
		protected virtual void VisitRecordType(IRecordTypeExpression recordType)
		{
			this.VisitListElements(recordType.Fields);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00005287 File Offset: 0x00003487
		protected virtual void VisitFunctionType(IFunctionTypeExpression functionType)
		{
			this.VisitListElements(functionType.Parameters);
			this.VisitOptionalExpression(functionType.ReturnType);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000052A1 File Offset: 0x000034A1
		protected virtual void VisitParameter(IParameter parameter)
		{
			this.VisitOptionalExpression(parameter.Type);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000052AF File Offset: 0x000034AF
		protected virtual void VisitParentheses(IParenthesesExpression parentheses)
		{
			this.VisitExpression(parentheses.Expression);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000052BD File Offset: 0x000034BD
		protected virtual void VisitRecord(IRecordExpression record)
		{
			this.VisitListElements(record.Members);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000052CB File Offset: 0x000034CB
		protected virtual void VisitSignature(IFunctionTypeExpression signature)
		{
			this.VisitFunctionType(signature);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000052D4 File Offset: 0x000034D4
		protected virtual void VisitThrow(IThrowExpression @throw)
		{
			this.VisitExpression(@throw.Expression);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000052E2 File Offset: 0x000034E2
		protected virtual void VisitTryCatch(ITryCatchExpression tryCatch)
		{
			this.VisitExpression(tryCatch.Try);
			this.VisitTryCatchExceptionCase(tryCatch.ExceptionCase);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000052FC File Offset: 0x000034FC
		protected virtual void VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
		{
			this.VisitExpression(tryCatchExceptionCase.Expression);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000530B File Offset: 0x0000350B
		protected virtual void VisitUnary(IUnaryExpression unary)
		{
			this.VisitExpression(unary.Expression);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00005319 File Offset: 0x00003519
		protected virtual void VisitVerbatim(IVerbatimExpression verbatim)
		{
			this.VisitExpression(verbatim.Text);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00005327 File Offset: 0x00003527
		protected virtual Exception NewDepthLimitExceededException()
		{
			return new DepthLimitExceededException("Depth limit exceeded");
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00005334 File Offset: 0x00003534
		protected void VisitListElements(IList<IExpression> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.VisitExpression(list[i]);
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00005360 File Offset: 0x00003560
		protected void VisitListElements(IList<ISectionMember> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.VisitModuleMember(list[i]);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000538C File Offset: 0x0000358C
		protected void VisitListElements(IList<IFieldType> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.VisitFieldType(list[i]);
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000053B8 File Offset: 0x000035B8
		protected void VisitListElements(IList<IParameter> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.VisitParameter(list[i]);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000053E4 File Offset: 0x000035E4
		protected void VisitListElements(IList<IRangeExpression> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.VisitRangeExpression(list[i]);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00005410 File Offset: 0x00003610
		protected void VisitListElements(IList<VariableInitializer> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.VisitInitializer(list[i]);
			}
		}

		// Token: 0x0400022B RID: 555
		private const int DefaultMaxDepth = 1000;

		// Token: 0x0400022C RID: 556
		private readonly int maxDepth;

		// Token: 0x0400022D RID: 557
		private int currentDepth;
	}
}
