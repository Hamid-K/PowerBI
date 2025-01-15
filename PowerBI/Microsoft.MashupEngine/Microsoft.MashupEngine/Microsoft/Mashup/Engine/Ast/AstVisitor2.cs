using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B6D RID: 7021
	public abstract class AstVisitor2
	{
		// Token: 0x0600AF9A RID: 44954 RVA: 0x0023EF84 File Offset: 0x0023D184
		protected object Visit(object node)
		{
			IExpression expression = node as IExpression;
			if (expression != null)
			{
				return this.VisitExpression(expression);
			}
			IDocument document = node as IDocument;
			if (document != null)
			{
				return this.VisitDocument(document);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600AF9B RID: 44955 RVA: 0x0023EFBC File Offset: 0x0023D1BC
		protected virtual IExpression VisitBinary(IBinaryExpression binary)
		{
			IExpression expression = this.VisitExpression(binary.Left);
			IExpression expression2 = this.VisitExpression(binary.Right);
			return this.CreateBinary(binary, expression, expression2);
		}

		// Token: 0x0600AF9C RID: 44956 RVA: 0x0023EFEC File Offset: 0x0023D1EC
		protected IBinaryExpression CreateBinary(IBinaryExpression binary, IExpression left, IExpression right)
		{
			if (left != binary.Left || right != binary.Right)
			{
				binary = BinaryExpressionSyntaxNode.New(binary.Operator, left, right, binary.Range);
			}
			return binary;
		}

		// Token: 0x0600AF9D RID: 44957 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual IExpression VisitConstant(IConstantExpression2 constant)
		{
			return constant;
		}

		// Token: 0x0600AF9E RID: 44958 RVA: 0x0023F018 File Offset: 0x0023D218
		protected virtual IDocument VisitDocument(IDocument document)
		{
			DocumentKind kind = document.Kind;
			if (kind == DocumentKind.Section)
			{
				return this.VisitModuleDocument((ISectionDocument)document);
			}
			if (kind != DocumentKind.Expression)
			{
				throw new InvalidOperationException();
			}
			return this.VisitExpressionDocument((IExpressionDocument)document);
		}

		// Token: 0x0600AF9F RID: 44959 RVA: 0x0023F054 File Offset: 0x0023D254
		protected virtual IDocument VisitExpressionDocument(IExpressionDocument document)
		{
			IExpression expression = this.VisitExpression(document.Expression);
			if (expression != document.Expression)
			{
				document = new ExpressionDocumentSyntaxNode(document.Host, document.Tokens, expression, document.Range);
			}
			return document;
		}

		// Token: 0x0600AFA0 RID: 44960 RVA: 0x0023F094 File Offset: 0x0023D294
		protected virtual IExpression VisitList(IListExpression list)
		{
			IList<IExpression> list2 = this.VisitListElements(list.Members);
			return this.CreateList(list, list2);
		}

		// Token: 0x0600AFA1 RID: 44961 RVA: 0x0023F0B6 File Offset: 0x0023D2B6
		protected IListExpression CreateList(IListExpression list, IList<IExpression> members)
		{
			if (!AstVisitor2.SameContents(members, list.Members))
			{
				list = new ListExpressionSyntaxNode(members, list.Range);
			}
			return list;
		}

		// Token: 0x0600AFA2 RID: 44962 RVA: 0x0023F0D8 File Offset: 0x0023D2D8
		protected virtual IExpression VisitRangeList(IRangeListExpression rangeList)
		{
			IList<IRangeExpression> list = this.VisitListElements(rangeList.Members);
			if (list != rangeList.Members)
			{
				rangeList = new RangeListExpressionSyntaxNode(list, rangeList.Range);
			}
			return rangeList;
		}

		// Token: 0x0600AFA3 RID: 44963 RVA: 0x0023F10C File Offset: 0x0023D30C
		protected virtual IRangeExpression VisitRangeExpression(IRangeExpression range)
		{
			if (range.Lower != range.Upper)
			{
				IExpression expression = this.VisitExpression(range.Lower);
				IExpression expression2 = this.VisitExpression(range.Upper);
				if (range.Lower != expression || range.Upper != expression2)
				{
					return new BinaryRangeExpressionSyntaxNode(expression, expression2, range.Range);
				}
			}
			else
			{
				IExpression expression3 = this.VisitExpression(range.Lower);
				if (range.Lower != expression3)
				{
					return new UnaryRangeExpressionSyntaxNode(expression3, range.Range);
				}
			}
			return range;
		}

		// Token: 0x0600AFA4 RID: 44964 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual IExpression VisitExports(IExportsExpression expression)
		{
			return expression;
		}

		// Token: 0x0600AFA5 RID: 44965 RVA: 0x0023F185 File Offset: 0x0023D385
		protected IExpression VisitOptionalExpression(IExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			return this.VisitExpression(expression);
		}

		// Token: 0x0600AFA6 RID: 44966 RVA: 0x0023F194 File Offset: 0x0023D394
		protected virtual IExpression VisitExpression(IExpression expression)
		{
			IExpression expression2;
			try
			{
				this.IncrementDepth();
				switch (expression.Kind)
				{
				case ExpressionKind.Binary:
					expression2 = this.VisitBinary((IBinaryExpression)expression);
					break;
				case ExpressionKind.Constant:
					expression2 = this.VisitConstant((IConstantExpression2)expression);
					break;
				case ExpressionKind.ElementAccess:
					expression2 = this.VisitElementAccess((IElementAccessExpression)expression);
					break;
				case ExpressionKind.Exports:
					expression2 = this.VisitExports((IExportsExpression)expression);
					break;
				case ExpressionKind.FieldAccess:
					expression2 = this.VisitFieldAccess((IFieldAccessExpression)expression);
					break;
				case ExpressionKind.Function:
					expression2 = this.VisitFunction((IFunctionExpression)expression);
					break;
				case ExpressionKind.Identifier:
					expression2 = this.VisitIdentifier((IIdentifierExpression)expression);
					break;
				case ExpressionKind.If:
					expression2 = this.VisitIf((IIfExpression)expression);
					break;
				case ExpressionKind.Invocation:
					expression2 = this.VisitInvocation((IInvocationExpression)expression);
					break;
				case ExpressionKind.Let:
					expression2 = this.VisitLet((ILetExpression)expression);
					break;
				case ExpressionKind.List:
					expression2 = this.VisitList((IListExpression)expression);
					break;
				case ExpressionKind.MultiFieldRecordProjection:
					expression2 = this.VisitMultiFieldRecordProjection((IMultiFieldRecordProjectionExpression)expression);
					break;
				case ExpressionKind.NotImplemented:
					expression2 = this.VisitNotImplemented((INotImplementedExpression)expression);
					break;
				case ExpressionKind.Parentheses:
					expression2 = this.VisitParentheses((IParenthesesExpression)expression);
					break;
				case ExpressionKind.RangeList:
					expression2 = this.VisitRangeList((IRangeListExpression)expression);
					break;
				case ExpressionKind.Record:
					expression2 = this.VisitRecord((IRecordExpression)expression);
					break;
				case ExpressionKind.SectionIdentifier:
					expression2 = this.VisitSectionIdentifier((ISectionIdentifierExpression)expression);
					break;
				case ExpressionKind.Throw:
					expression2 = this.VisitThrow((IThrowExpression)expression);
					break;
				case ExpressionKind.TryCatch:
					expression2 = this.VisitTryCatch((ITryCatchExpression)expression);
					break;
				case ExpressionKind.Unary:
					expression2 = this.VisitUnary((IUnaryExpression)expression);
					break;
				case ExpressionKind.Verbatim:
					expression2 = this.VisitVerbatim((IVerbatimExpression)expression);
					break;
				case ExpressionKind.ImplicitIdentifier:
					expression2 = this.VisitImplicitIdentifier((IImplicitIdentifierExpression)expression);
					break;
				case ExpressionKind.Type:
					expression2 = this.VisitType((ITypeExpression)expression);
					break;
				case ExpressionKind.RecordType:
					expression2 = this.VisitRecordType((IRecordTypeExpression)expression);
					break;
				case ExpressionKind.ListType:
					expression2 = this.VisitListType((IListTypeExpression)expression);
					break;
				case ExpressionKind.TableType:
					expression2 = this.VisitTableType((ITableTypeExpression)expression);
					break;
				case ExpressionKind.NullableType:
					expression2 = this.VisitNullableType((INullableTypeExpression)expression);
					break;
				case ExpressionKind.FunctionType:
					expression2 = this.VisitFunctionType((IFunctionTypeExpression)expression);
					break;
				default:
					throw new InvalidOperationException();
				}
			}
			finally
			{
				this.DecrementDepth();
			}
			return expression2;
		}

		// Token: 0x0600AFA7 RID: 44967 RVA: 0x0023F434 File Offset: 0x0023D634
		protected void IncrementDepth()
		{
			if (this.currentDepth > 1050)
			{
				throw new DepthLimitExceededException(Strings.DocumentReader_ParseDepth);
			}
			this.currentDepth++;
		}

		// Token: 0x0600AFA8 RID: 44968 RVA: 0x0023F45C File Offset: 0x0023D65C
		protected void DecrementDepth()
		{
			this.currentDepth--;
		}

		// Token: 0x0600AFA9 RID: 44969 RVA: 0x0023F46C File Offset: 0x0023D66C
		protected virtual IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			IExpression expression = this.VisitExpression(fieldAccess.Expression);
			return this.CreateFieldAccess(fieldAccess, expression);
		}

		// Token: 0x0600AFAA RID: 44970 RVA: 0x0023F48E File Offset: 0x0023D68E
		protected IFieldAccessExpression CreateFieldAccess(IFieldAccessExpression fieldAccess, IExpression expression)
		{
			if (expression != fieldAccess.Expression)
			{
				if (fieldAccess.IsOptional)
				{
					fieldAccess = new OptionalFieldAccessExpressionSyntaxNode(expression, fieldAccess.MemberName, fieldAccess.Range);
				}
				else
				{
					fieldAccess = new RequiredFieldAccessExpressionSyntaxNode(expression, fieldAccess.MemberName, fieldAccess.Range);
				}
			}
			return fieldAccess;
		}

		// Token: 0x0600AFAB RID: 44971 RVA: 0x0023F4CC File Offset: 0x0023D6CC
		protected virtual IExpression VisitFunction(IFunctionExpression function)
		{
			IFunctionTypeExpression functionTypeExpression = this.VisitSignature(function.FunctionType);
			IExpression expression = this.VisitExpression(function.Expression);
			return this.CreateFunction(function, functionTypeExpression, expression);
		}

		// Token: 0x0600AFAC RID: 44972 RVA: 0x0023F4FC File Offset: 0x0023D6FC
		protected IFunctionExpression CreateFunction(IFunctionExpression function, IFunctionTypeExpression signature, IExpression expression)
		{
			if (signature != function.FunctionType || expression != function.Expression)
			{
				function = new FunctionExpressionSyntaxNode(signature, expression, function.Range);
			}
			return function;
		}

		// Token: 0x0600AFAD RID: 44973 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual IExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			return identifier;
		}

		// Token: 0x0600AFAE RID: 44974 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual IExpression VisitSectionIdentifier(ISectionIdentifierExpression sectionIdentifier)
		{
			return sectionIdentifier;
		}

		// Token: 0x0600AFAF RID: 44975 RVA: 0x0023F520 File Offset: 0x0023D720
		protected virtual IExpression VisitIf(IIfExpression @if)
		{
			IExpression expression = this.VisitExpression(@if.Condition);
			IExpression expression2 = this.VisitExpression(@if.TrueCase);
			IExpression expression3 = this.VisitExpression(@if.FalseCase);
			return this.CreateIf(@if, expression, expression2, expression3);
		}

		// Token: 0x0600AFB0 RID: 44976 RVA: 0x0023F55E File Offset: 0x0023D75E
		protected IIfExpression CreateIf(IIfExpression @if, IExpression condition, IExpression trueCase, IExpression falseCase)
		{
			if (condition != @if.Condition || trueCase != @if.TrueCase || falseCase != @if.FalseCase)
			{
				@if = new IfExpressionSyntaxNode(condition, trueCase, falseCase, @if.Range);
			}
			return @if;
		}

		// Token: 0x0600AFB1 RID: 44977 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual IExpression VisitImplicitIdentifier(IImplicitIdentifierExpression implicitIdentifier)
		{
			return implicitIdentifier;
		}

		// Token: 0x0600AFB2 RID: 44978 RVA: 0x0023F58E File Offset: 0x0023D78E
		protected virtual VariableInitializer VisitInitializer(VariableInitializer member)
		{
			return new VariableInitializer(member.Name, this.VisitExpression(member.Value));
		}

		// Token: 0x0600AFB3 RID: 44979 RVA: 0x0023F5AC File Offset: 0x0023D7AC
		protected virtual IExpression VisitElementAccess(IElementAccessExpression elementAccess)
		{
			IExpression expression = this.VisitExpression(elementAccess.Collection);
			IExpression expression2 = this.VisitExpression(elementAccess.Key);
			return this.CreateElementAccess(elementAccess, expression, expression2);
		}

		// Token: 0x0600AFB4 RID: 44980 RVA: 0x0023F5DC File Offset: 0x0023D7DC
		protected IElementAccessExpression CreateElementAccess(IElementAccessExpression elementAccess, IExpression collection, IExpression key)
		{
			if (collection != elementAccess.Collection || key != elementAccess.Key)
			{
				if (elementAccess.IsOptional)
				{
					elementAccess = new OptionalElementAccessExpressionSyntaxNode(collection, key, elementAccess.Range);
				}
				else
				{
					elementAccess = new RequiredElementAccessExpressionSyntaxNode(collection, key, elementAccess.Range);
				}
			}
			return elementAccess;
		}

		// Token: 0x0600AFB5 RID: 44981 RVA: 0x0023F61C File Offset: 0x0023D81C
		protected virtual IExpression VisitInvocation(IInvocationExpression invocation)
		{
			IExpression expression = this.VisitExpression(invocation.Function);
			IList<IExpression> list = this.VisitListElements(invocation.Arguments);
			return this.CreateInvocation(invocation, expression, list);
		}

		// Token: 0x0600AFB6 RID: 44982 RVA: 0x0023F64C File Offset: 0x0023D84C
		protected IInvocationExpression CreateInvocation(IInvocationExpression invocation, IExpression function, params IExpression[] arguments)
		{
			return this.CreateInvocation(invocation, function, arguments);
		}

		// Token: 0x0600AFB7 RID: 44983 RVA: 0x0023F657 File Offset: 0x0023D857
		protected IInvocationExpression CreateInvocation(IInvocationExpression invocation, IExpression function, IList<IExpression> arguments)
		{
			if (function != invocation.Function || !AstVisitor2.SameContents(arguments, invocation.Arguments))
			{
				invocation = new InvocationExpressionSyntaxNodeN(function, arguments, invocation.Range);
			}
			return invocation;
		}

		// Token: 0x0600AFB8 RID: 44984 RVA: 0x0023F680 File Offset: 0x0023D880
		protected virtual IExpression VisitLet(ILetExpression let)
		{
			IList<VariableInitializer> list = this.VisitListElements(let.Variables);
			IExpression expression = this.VisitExpression(let.Expression);
			if (!AstVisitor2.SameContents(list, let.Variables) || expression != let.Expression)
			{
				let = new LetExpressionSyntaxNode(list, expression, let.Range);
			}
			return let;
		}

		// Token: 0x0600AFB9 RID: 44985 RVA: 0x0023F6D0 File Offset: 0x0023D8D0
		protected virtual ISection VisitModule(ISection module)
		{
			IList<ISectionMember> list = this.VisitListElements(module.Members);
			if (list != module.Members)
			{
				module = new ModuleSyntaxNode(module.Attribute, module.SectionName, list, module.Range);
			}
			return module;
		}

		// Token: 0x0600AFBA RID: 44986 RVA: 0x0023F710 File Offset: 0x0023D910
		protected virtual IDocument VisitModuleDocument(ISectionDocument document)
		{
			ISection section = this.VisitModule(document.Section);
			if (section != document.Section)
			{
				document = new ModuleDocumentSyntaxNode(document.Host, document.Tokens, section, document.Range);
			}
			return document;
		}

		// Token: 0x0600AFBB RID: 44987 RVA: 0x0023F750 File Offset: 0x0023D950
		protected virtual ISectionMember VisitModuleMember(ISectionMember moduleMember)
		{
			IExpression expression = this.VisitOptionalExpression(moduleMember.Value);
			if (expression != moduleMember.Value)
			{
				moduleMember = new ModuleMemberSyntaxNode(moduleMember.Attribute, moduleMember.Export, moduleMember.Name, expression, moduleMember.Range);
			}
			return moduleMember;
		}

		// Token: 0x0600AFBC RID: 44988 RVA: 0x0023F794 File Offset: 0x0023D994
		protected virtual IExpression VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
		{
			IExpression expression = this.VisitExpression(multiFieldRecordProjection.Expression);
			return this.CreateMultiFieldRecordProjection(multiFieldRecordProjection, expression);
		}

		// Token: 0x0600AFBD RID: 44989 RVA: 0x0023F7B6 File Offset: 0x0023D9B6
		protected IMultiFieldRecordProjectionExpression CreateMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection, IExpression expression)
		{
			if (expression != multiFieldRecordProjection.Expression)
			{
				if (multiFieldRecordProjection.IsOptional)
				{
					multiFieldRecordProjection = new OptionalMultiFieldRecordProjectionExpressionSyntaxNode(expression, multiFieldRecordProjection.MemberNames, multiFieldRecordProjection.Range);
				}
				else
				{
					multiFieldRecordProjection = new RequiredMultiFieldRecordProjectionExpressionSyntaxNode(expression, multiFieldRecordProjection.MemberNames, multiFieldRecordProjection.Range);
				}
			}
			return multiFieldRecordProjection;
		}

		// Token: 0x0600AFBE RID: 44990 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual IExpression VisitNotImplemented(INotImplementedExpression notImplemented)
		{
			return notImplemented;
		}

		// Token: 0x0600AFBF RID: 44991 RVA: 0x0023F7F4 File Offset: 0x0023D9F4
		protected virtual IExpression VisitType(ITypeExpression type)
		{
			IExpression expression = this.VisitExpression(type.Expression);
			if (expression != type.Expression)
			{
				type = new TypeExpressionSyntaxNode(expression, type.Range);
			}
			return type;
		}

		// Token: 0x0600AFC0 RID: 44992 RVA: 0x0023F828 File Offset: 0x0023DA28
		protected virtual IExpression VisitListType(IListTypeExpression listType)
		{
			IExpression expression = this.VisitExpression(listType.ItemType);
			if (expression != listType.ItemType)
			{
				listType = new ListTypeSyntaxNode(expression, listType.Range);
			}
			return listType;
		}

		// Token: 0x0600AFC1 RID: 44993 RVA: 0x0023F85C File Offset: 0x0023DA5C
		protected virtual IExpression VisitTableType(ITableTypeExpression tableType)
		{
			IExpression expression = this.VisitExpression(tableType.RowType);
			if (expression != tableType.RowType)
			{
				tableType = new TableTypeSyntaxNode(expression, tableType.Range);
			}
			return tableType;
		}

		// Token: 0x0600AFC2 RID: 44994 RVA: 0x0023F890 File Offset: 0x0023DA90
		protected virtual IExpression VisitNullableType(INullableTypeExpression nullableType)
		{
			IExpression expression = this.VisitExpression(nullableType.ItemType);
			if (expression != nullableType.ItemType)
			{
				nullableType = new NullableTypeSyntaxNode(expression, nullableType.Range);
			}
			return nullableType;
		}

		// Token: 0x0600AFC3 RID: 44995 RVA: 0x0023F8C4 File Offset: 0x0023DAC4
		protected virtual IFieldType VisitFieldType(IFieldType fieldType)
		{
			IExpression expression = this.VisitOptionalExpression(fieldType.Type);
			if (expression != fieldType.Type)
			{
				fieldType = new FieldTypeSyntaxNode(fieldType.Name, expression, fieldType.Optional);
			}
			return fieldType;
		}

		// Token: 0x0600AFC4 RID: 44996 RVA: 0x0023F8FC File Offset: 0x0023DAFC
		protected virtual IExpression VisitRecordType(IRecordTypeExpression recordType)
		{
			IList<IFieldType> list = this.VisitListElements(recordType.Fields);
			if (list != recordType.Fields)
			{
				recordType = new RecordTypeSyntaxNode(list, recordType.Wildcard, recordType.Range);
			}
			return recordType;
		}

		// Token: 0x0600AFC5 RID: 44997 RVA: 0x0023F934 File Offset: 0x0023DB34
		protected virtual IExpression VisitFunctionType(IFunctionTypeExpression functionType)
		{
			IList<IParameter> list = this.VisitListElements(functionType.Parameters);
			IExpression expression = this.VisitOptionalExpression(functionType.ReturnType);
			if (expression != functionType.ReturnType || list != functionType.Parameters)
			{
				functionType = new FunctionTypeSyntaxNode(expression, list, functionType.Min, functionType.Range);
			}
			return functionType;
		}

		// Token: 0x0600AFC6 RID: 44998 RVA: 0x0023F984 File Offset: 0x0023DB84
		protected virtual IParameter VisitParameter(IParameter parameter)
		{
			IExpression expression = this.VisitOptionalExpression(parameter.Type);
			if (expression != parameter.Type)
			{
				parameter = new ParameterSyntaxNode(parameter.Identifier, expression);
			}
			return parameter;
		}

		// Token: 0x0600AFC7 RID: 44999 RVA: 0x0023F9B8 File Offset: 0x0023DBB8
		protected virtual IExpression VisitParentheses(IParenthesesExpression parentheses)
		{
			IExpression expression = this.VisitExpression(parentheses.Expression);
			if (expression != parentheses.Expression)
			{
				parentheses = new ParenthesesExpressionSyntaxNode(expression, parentheses.Range);
			}
			return parentheses;
		}

		// Token: 0x0600AFC8 RID: 45000 RVA: 0x0023F9EC File Offset: 0x0023DBEC
		protected virtual IExpression VisitRecord(IRecordExpression record)
		{
			IList<VariableInitializer> list = this.VisitListElements(record.Members);
			return this.CreateRecord(record, list);
		}

		// Token: 0x0600AFC9 RID: 45001 RVA: 0x0023FA0E File Offset: 0x0023DC0E
		protected IRecordExpression CreateRecord(IRecordExpression record, IList<VariableInitializer> members)
		{
			if (!AstVisitor2.SameContents(members, record.Members))
			{
				record = new RecordExpressionSyntaxNode(record.Identifier, members, record.Range);
			}
			return record;
		}

		// Token: 0x0600AFCA RID: 45002 RVA: 0x0023FA33 File Offset: 0x0023DC33
		protected virtual IFunctionTypeExpression VisitSignature(IFunctionTypeExpression signature)
		{
			return (IFunctionTypeExpression)this.VisitFunctionType(signature);
		}

		// Token: 0x0600AFCB RID: 45003 RVA: 0x0023FA44 File Offset: 0x0023DC44
		protected virtual IExpression VisitThrow(IThrowExpression @throw)
		{
			IExpression expression = this.VisitExpression(@throw.Expression);
			if (expression != @throw.Expression)
			{
				@throw = new ThrowExpressionSyntaxNode(expression, @throw.Range);
			}
			return @throw;
		}

		// Token: 0x0600AFCC RID: 45004 RVA: 0x0023FA78 File Offset: 0x0023DC78
		protected virtual IExpression VisitTryCatch(ITryCatchExpression tryCatch)
		{
			IExpression expression = this.VisitExpression(tryCatch.Try);
			TryCatchExceptionCase tryCatchExceptionCase = this.VisitTryCatchExceptionCase(tryCatch.ExceptionCase);
			return this.CreateTryCatch(tryCatch, expression, tryCatchExceptionCase);
		}

		// Token: 0x0600AFCD RID: 45005 RVA: 0x0023FAA8 File Offset: 0x0023DCA8
		protected ITryCatchExpression CreateTryCatch(ITryCatchExpression tryCatch, IExpression @try, TryCatchExceptionCase exceptionCase)
		{
			if (@try != tryCatch.Try || exceptionCase.Variable != tryCatch.ExceptionCase.Variable || exceptionCase.Expression != tryCatch.ExceptionCase.Expression)
			{
				tryCatch = new TryCatchExpressionSyntaxNode(@try, exceptionCase, tryCatch.Range);
			}
			return tryCatch;
		}

		// Token: 0x0600AFCE RID: 45006 RVA: 0x0023FAFC File Offset: 0x0023DCFC
		protected virtual TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
		{
			IExpression expression = this.VisitExpression(tryCatchExceptionCase.Expression);
			return this.CreateTryCatchExceptionCase(tryCatchExceptionCase, expression);
		}

		// Token: 0x0600AFCF RID: 45007 RVA: 0x0023FB1F File Offset: 0x0023DD1F
		protected TryCatchExceptionCase CreateTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase, IExpression expression)
		{
			if (expression != tryCatchExceptionCase.Expression)
			{
				tryCatchExceptionCase = new TryCatchExceptionCase(tryCatchExceptionCase.Variable, expression);
			}
			return tryCatchExceptionCase;
		}

		// Token: 0x0600AFD0 RID: 45008 RVA: 0x0023FB3C File Offset: 0x0023DD3C
		protected virtual IExpression VisitUnary(IUnaryExpression unary)
		{
			IExpression expression = this.VisitExpression(unary.Expression);
			return this.CreateUnary(unary, expression);
		}

		// Token: 0x0600AFD1 RID: 45009 RVA: 0x0023FB5E File Offset: 0x0023DD5E
		protected IUnaryExpression CreateUnary(IUnaryExpression unary, IExpression expression)
		{
			if (expression != unary.Expression)
			{
				unary = UnaryExpressionSyntaxNode.New(unary.Operator, expression, unary.Range);
			}
			return unary;
		}

		// Token: 0x0600AFD2 RID: 45010 RVA: 0x0023FB80 File Offset: 0x0023DD80
		protected virtual IExpression VisitVerbatim(IVerbatimExpression verbatim)
		{
			IExpression expression = this.VisitExpression(verbatim.Text);
			if (expression != verbatim.Text)
			{
				verbatim = new VerbatimExpressionSyntaxNode((IConstantExpression2)expression, verbatim.Range);
			}
			return verbatim;
		}

		// Token: 0x0600AFD3 RID: 45011 RVA: 0x0023FBB8 File Offset: 0x0023DDB8
		protected IList<IExpression> VisitListElements(IList<IExpression> list)
		{
			IExpression[] array = list as IExpression[];
			if (array != null)
			{
				return this.VisitListElements(array);
			}
			IExpression[] array2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				IExpression expression = this.VisitExpression(list[i]);
				if (array2 != null || expression != list[i])
				{
					if (array2 == null)
					{
						array2 = new IExpression[list.Count];
						for (int j = 0; j < i; j++)
						{
							array2[j] = list[j];
						}
					}
					array2[i] = expression;
				}
			}
			if (array2 != null)
			{
				list = array2;
			}
			return list;
		}

		// Token: 0x0600AFD4 RID: 45012 RVA: 0x0023FC3C File Offset: 0x0023DE3C
		protected IExpression[] VisitListElements(IExpression[] list)
		{
			IExpression[] array = null;
			for (int i = 0; i < list.Length; i++)
			{
				IExpression expression = this.VisitExpression(list[i]);
				if (array != null || expression != list[i])
				{
					if (array == null)
					{
						array = new IExpression[list.Length];
						for (int j = 0; j < i; j++)
						{
							array[j] = list[j];
						}
					}
					array[i] = expression;
				}
			}
			if (array != null)
			{
				list = array;
			}
			return list;
		}

		// Token: 0x0600AFD5 RID: 45013 RVA: 0x0023FC98 File Offset: 0x0023DE98
		protected IList<ISectionMember> VisitListElements(IList<ISectionMember> list)
		{
			ISectionMember[] array = list as ISectionMember[];
			if (array != null)
			{
				return this.VisitListElements(array);
			}
			ISectionMember[] array2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				ISectionMember sectionMember = this.VisitModuleMember(list[i]);
				if (array2 != null || sectionMember != list[i])
				{
					if (array2 == null)
					{
						array2 = new ISectionMember[list.Count];
						for (int j = 0; j < i; j++)
						{
							array2[j] = list[j];
						}
					}
					array2[i] = sectionMember;
				}
			}
			if (array2 != null)
			{
				list = array2;
			}
			return list;
		}

		// Token: 0x0600AFD6 RID: 45014 RVA: 0x0023FD1C File Offset: 0x0023DF1C
		protected ISectionMember[] VisitListElements(ISectionMember[] list)
		{
			ISectionMember[] array = null;
			for (int i = 0; i < list.Length; i++)
			{
				ISectionMember sectionMember = this.VisitModuleMember(list[i]);
				if (array != null || sectionMember != list[i])
				{
					if (array == null)
					{
						array = new ISectionMember[list.Length];
						for (int j = 0; j < i; j++)
						{
							array[j] = list[j];
						}
					}
					array[i] = sectionMember;
				}
			}
			if (array != null)
			{
				list = array;
			}
			return list;
		}

		// Token: 0x0600AFD7 RID: 45015 RVA: 0x0023FD78 File Offset: 0x0023DF78
		protected IList<IFieldType> VisitListElements(IList<IFieldType> list)
		{
			IFieldType[] array = list as IFieldType[];
			if (array != null)
			{
				return this.VisitListElements(array);
			}
			IFieldType[] array2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				IFieldType fieldType = this.VisitFieldType(list[i]);
				if (array2 != null || fieldType != list[i])
				{
					if (array2 == null)
					{
						array2 = new IFieldType[list.Count];
						for (int j = 0; j < i; j++)
						{
							array2[j] = list[j];
						}
					}
					array2[i] = fieldType;
				}
			}
			if (array2 != null)
			{
				list = array2;
			}
			return list;
		}

		// Token: 0x0600AFD8 RID: 45016 RVA: 0x0023FDFC File Offset: 0x0023DFFC
		protected IFieldType[] VisitListElements(IFieldType[] list)
		{
			IFieldType[] array = null;
			for (int i = 0; i < list.Length; i++)
			{
				IFieldType fieldType = this.VisitFieldType(list[i]);
				if (array != null || fieldType != list[i])
				{
					if (array == null)
					{
						array = new IFieldType[list.Length];
						for (int j = 0; j < i; j++)
						{
							array[j] = list[j];
						}
					}
					array[i] = fieldType;
				}
			}
			if (array != null)
			{
				list = array;
			}
			return list;
		}

		// Token: 0x0600AFD9 RID: 45017 RVA: 0x0023FE58 File Offset: 0x0023E058
		protected IList<IParameter> VisitListElements(IList<IParameter> list)
		{
			IParameter[] array = list as IParameter[];
			if (array != null)
			{
				return this.VisitListElements(array);
			}
			IParameter[] array2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				IParameter parameter = this.VisitParameter(list[i]);
				if (array2 != null || parameter != list[i])
				{
					if (array2 == null)
					{
						array2 = new IParameter[list.Count];
						for (int j = 0; j < i; j++)
						{
							array2[j] = list[j];
						}
					}
					array2[i] = parameter;
				}
			}
			if (array2 != null)
			{
				list = array2;
			}
			return list;
		}

		// Token: 0x0600AFDA RID: 45018 RVA: 0x0023FEDC File Offset: 0x0023E0DC
		protected IParameter[] VisitListElements(IParameter[] list)
		{
			IParameter[] array = null;
			for (int i = 0; i < list.Length; i++)
			{
				IParameter parameter = this.VisitParameter(list[i]);
				if (array != null || parameter != list[i])
				{
					if (array == null)
					{
						array = new IParameter[list.Length];
						for (int j = 0; j < i; j++)
						{
							array[j] = list[j];
						}
					}
					array[i] = parameter;
				}
			}
			if (array != null)
			{
				list = array;
			}
			return list;
		}

		// Token: 0x0600AFDB RID: 45019 RVA: 0x0023FF38 File Offset: 0x0023E138
		protected IList<IRangeExpression> VisitListElements(IList<IRangeExpression> list)
		{
			IRangeExpression[] array = list as IRangeExpression[];
			if (array != null)
			{
				return this.VisitListElements(array);
			}
			IRangeExpression[] array2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				IRangeExpression rangeExpression = this.VisitRangeExpression(list[i]);
				if (array2 != null || rangeExpression != list[i])
				{
					if (array2 == null)
					{
						array2 = new IRangeExpression[list.Count];
						for (int j = 0; j < i; j++)
						{
							array2[j] = list[j];
						}
					}
					array2[i] = rangeExpression;
				}
			}
			if (array2 != null)
			{
				list = array2;
			}
			return list;
		}

		// Token: 0x0600AFDC RID: 45020 RVA: 0x0023FFBC File Offset: 0x0023E1BC
		protected IRangeExpression[] VisitListElements(IRangeExpression[] list)
		{
			IRangeExpression[] array = null;
			for (int i = 0; i < list.Length; i++)
			{
				IRangeExpression rangeExpression = this.VisitRangeExpression(list[i]);
				if (array != null || rangeExpression != list[i])
				{
					if (array == null)
					{
						array = new IRangeExpression[list.Length];
						for (int j = 0; j < i; j++)
						{
							array[j] = list[j];
						}
					}
					array[i] = rangeExpression;
				}
			}
			if (array != null)
			{
				list = array;
			}
			return list;
		}

		// Token: 0x0600AFDD RID: 45021 RVA: 0x00240018 File Offset: 0x0023E218
		protected IList<VariableInitializer> VisitListElements(IList<VariableInitializer> list)
		{
			VariableInitializer[] array = list as VariableInitializer[];
			if (array != null)
			{
				return this.VisitListElements(array);
			}
			VariableInitializer[] array2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				VariableInitializer variableInitializer = this.VisitInitializer(list[i]);
				if (array2 != null || variableInitializer.Value != list[i].Value)
				{
					if (array2 == null)
					{
						array2 = new VariableInitializer[list.Count];
						for (int j = 0; j < i; j++)
						{
							array2[j] = list[j];
						}
					}
					array2[i] = variableInitializer;
				}
			}
			if (array2 != null)
			{
				list = array2;
			}
			return list;
		}

		// Token: 0x0600AFDE RID: 45022 RVA: 0x002400B4 File Offset: 0x0023E2B4
		protected VariableInitializer[] VisitListElements(VariableInitializer[] list)
		{
			VariableInitializer[] array = null;
			for (int i = 0; i < list.Length; i++)
			{
				VariableInitializer variableInitializer = this.VisitInitializer(list[i]);
				if (array != null || variableInitializer.Value != list[i].Value || variableInitializer.Name != list[i].Name)
				{
					if (array == null)
					{
						array = new VariableInitializer[list.Length];
						for (int j = 0; j < i; j++)
						{
							array[j] = list[j];
						}
					}
					array[i] = variableInitializer;
				}
			}
			if (array != null)
			{
				list = array;
			}
			return list;
		}

		// Token: 0x0600AFDF RID: 45023 RVA: 0x00240148 File Offset: 0x0023E348
		private static bool SameContents(IList<IExpression> list1, IList<IExpression> list2)
		{
			if (list1 == list2)
			{
				return true;
			}
			IExpression[] array = list1 as IExpression[];
			IExpression[] array2 = list2 as IExpression[];
			if (array != null && array2 != null)
			{
				return AstVisitor2.SameContents(array, array2);
			}
			bool flag = list1.Count == list2.Count;
			int num = 0;
			while (flag && num < list1.Count)
			{
				flag = list1[num] == list2[num];
				num++;
			}
			return flag;
		}

		// Token: 0x0600AFE0 RID: 45024 RVA: 0x002401AC File Offset: 0x0023E3AC
		private static bool SameContents(IExpression[] list1, IExpression[] list2)
		{
			bool flag = list1.Length == list2.Length;
			int num = 0;
			while (flag && num < list1.Length)
			{
				flag = list1[num] == list2[num];
				num++;
			}
			return flag;
		}

		// Token: 0x0600AFE1 RID: 45025 RVA: 0x002401E0 File Offset: 0x0023E3E0
		private static bool SameContents(IList<VariableInitializer> list1, IList<VariableInitializer> list2)
		{
			if (list1 == list2)
			{
				return true;
			}
			VariableInitializer[] array = list1 as VariableInitializer[];
			VariableInitializer[] array2 = list2 as VariableInitializer[];
			if (array != null && array2 != null)
			{
				return AstVisitor2.SameContents(array, array2);
			}
			bool flag = list1.Count == list2.Count;
			int num = 0;
			while (flag && num < list1.Count)
			{
				flag = list1[num].Name == list2[num].Name && list1[num].Value == list2[num].Value;
				num++;
			}
			return flag;
		}

		// Token: 0x0600AFE2 RID: 45026 RVA: 0x00240280 File Offset: 0x0023E480
		private static bool SameContents(VariableInitializer[] list1, VariableInitializer[] list2)
		{
			bool flag = list1.Length == list2.Length;
			int num = 0;
			while (flag && num < list1.Length)
			{
				flag = list1[num].Name == list2[num].Name && list1[num].Value == list2[num].Value;
				num++;
			}
			return flag;
		}

		// Token: 0x04005A87 RID: 23175
		public const int MaxExpressionDepth = 1050;

		// Token: 0x04005A88 RID: 23176
		private int currentDepth;
	}
}
