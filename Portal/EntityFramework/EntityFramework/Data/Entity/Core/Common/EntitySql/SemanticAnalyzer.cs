using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000669 RID: 1641
	internal sealed class SemanticAnalyzer
	{
		// Token: 0x06004E31 RID: 20017 RVA: 0x00118B39 File Offset: 0x00116D39
		internal SemanticAnalyzer(SemanticResolver sr)
		{
			this._sr = sr;
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x00118B48 File Offset: 0x00116D48
		internal ParseResult AnalyzeCommand(Node astExpr)
		{
			Command command = SemanticAnalyzer.ValidateQueryCommandAst(astExpr);
			SemanticAnalyzer.ConvertAndRegisterNamespaceImports(command.NamespaceImportList, command.ErrCtx, this._sr);
			return SemanticAnalyzer.ConvertStatement(command.Statement, this._sr);
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x00118B84 File Offset: 0x00116D84
		internal DbLambda AnalyzeQueryCommand(Node astExpr)
		{
			Command command = SemanticAnalyzer.ValidateQueryCommandAst(astExpr);
			SemanticAnalyzer.ConvertAndRegisterNamespaceImports(command.NamespaceImportList, command.ErrCtx, this._sr);
			List<FunctionDefinition> list;
			return DbExpressionBuilder.Lambda(SemanticAnalyzer.ConvertQueryStatementToDbExpression(command.Statement, this._sr, out list), this._sr.Variables.Values);
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x00118BD8 File Offset: 0x00116DD8
		private static Command ValidateQueryCommandAst(Node astExpr)
		{
			Command command = astExpr as Command;
			if (command == null)
			{
				throw new ArgumentException(Strings.UnknownAstCommandExpression);
			}
			if (!(command.Statement is QueryStatement))
			{
				throw new ArgumentException(Strings.UnknownAstExpressionType);
			}
			return command;
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x00118C14 File Offset: 0x00116E14
		private static void ConvertAndRegisterNamespaceImports(NodeList<NamespaceImport> nsImportList, ErrorContext cmdErrCtx, SemanticResolver sr)
		{
			List<Tuple<string, MetadataNamespace, ErrorContext>> list = new List<Tuple<string, MetadataNamespace, ErrorContext>>();
			List<Tuple<MetadataNamespace, ErrorContext>> list2 = new List<Tuple<MetadataNamespace, ErrorContext>>();
			if (nsImportList != null)
			{
				foreach (NamespaceImport namespaceImport in ((IEnumerable<NamespaceImport>)nsImportList))
				{
					string[] array = null;
					Identifier identifier = namespaceImport.NamespaceName as Identifier;
					if (identifier != null)
					{
						array = new string[] { identifier.Name };
					}
					DotExpr dotExpr = namespaceImport.NamespaceName as DotExpr;
					if (dotExpr != null)
					{
						dotExpr.IsMultipartIdentifier(out array);
					}
					if (array == null)
					{
						ErrorContext errCtx = namespaceImport.NamespaceName.ErrCtx;
						string invalidMetadataMemberName = Strings.InvalidMetadataMemberName;
						throw EntitySqlException.Create(errCtx, invalidMetadataMemberName, null);
					}
					string text = ((namespaceImport.Alias != null) ? namespaceImport.Alias.Name : null);
					MetadataMember metadataMember = sr.ResolveMetadataMemberName(array, namespaceImport.NamespaceName.ErrCtx);
					if (metadataMember.MetadataMemberClass != MetadataMemberClass.Namespace)
					{
						ErrorContext errCtx2 = namespaceImport.NamespaceName.ErrCtx;
						string text2 = Strings.InvalidMetadataMemberClassResolution(metadataMember.Name, metadataMember.MetadataMemberClassName, MetadataNamespace.NamespaceClassName);
						throw EntitySqlException.Create(errCtx2, text2, null);
					}
					MetadataNamespace metadataNamespace = (MetadataNamespace)metadataMember;
					if (text != null)
					{
						list.Add(Tuple.Create<string, MetadataNamespace, ErrorContext>(text, metadataNamespace, namespaceImport.ErrCtx));
					}
					else
					{
						list2.Add(Tuple.Create<MetadataNamespace, ErrorContext>(metadataNamespace, namespaceImport.ErrCtx));
					}
				}
			}
			sr.TypeResolver.AddNamespaceImport(new MetadataNamespace("Edm"), (nsImportList != null) ? nsImportList.ErrCtx : cmdErrCtx);
			foreach (Tuple<string, MetadataNamespace, ErrorContext> tuple in list)
			{
				sr.TypeResolver.AddAliasedNamespaceImport(tuple.Item1, tuple.Item2, tuple.Item3);
			}
			foreach (Tuple<MetadataNamespace, ErrorContext> tuple2 in list2)
			{
				sr.TypeResolver.AddNamespaceImport(tuple2.Item1, tuple2.Item2);
			}
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x00118E5C File Offset: 0x0011705C
		private static ParseResult ConvertStatement(Statement astStatement, SemanticResolver sr)
		{
			if (astStatement is QueryStatement)
			{
				SemanticAnalyzer.StatementConverter statementConverter = new SemanticAnalyzer.StatementConverter(SemanticAnalyzer.ConvertQueryStatementToDbCommandTree);
				return statementConverter(astStatement, sr);
			}
			throw new ArgumentException(Strings.UnknownAstExpressionType);
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x00118E94 File Offset: 0x00117094
		private static ParseResult ConvertQueryStatementToDbCommandTree(Statement astStatement, SemanticResolver sr)
		{
			List<FunctionDefinition> list;
			DbExpression dbExpression = SemanticAnalyzer.ConvertQueryStatementToDbExpression(astStatement, sr, out list);
			return new ParseResult(DbQueryCommandTree.FromValidExpression(sr.TypeResolver.Perspective.MetadataWorkspace, sr.TypeResolver.Perspective.TargetDataspace, dbExpression, true, false), list);
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x00118EDC File Offset: 0x001170DC
		private static DbExpression ConvertQueryStatementToDbExpression(Statement astStatement, SemanticResolver sr, out List<FunctionDefinition> functionDefs)
		{
			QueryStatement queryStatement = astStatement as QueryStatement;
			if (queryStatement == null)
			{
				throw new ArgumentException(Strings.UnknownAstExpressionType);
			}
			functionDefs = SemanticAnalyzer.ConvertInlineFunctionDefinitions(queryStatement.FunctionDefList, sr);
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(queryStatement.Expr, sr);
			if (dbExpression == null)
			{
				ErrorContext errCtx = queryStatement.Expr.ErrCtx;
				string resultingExpressionTypeCannotBeNull = Strings.ResultingExpressionTypeCannotBeNull;
				throw EntitySqlException.Create(errCtx, resultingExpressionTypeCannotBeNull, null);
			}
			if (dbExpression is DbScanExpression)
			{
				DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(sr.GenerateInternalName("extent"));
				dbExpression = dbExpressionBinding.Project(dbExpressionBinding.Variable);
			}
			if (sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.NormalMode)
			{
				SemanticAnalyzer.ValidateQueryResultType(dbExpression.ResultType, queryStatement.Expr.ErrCtx);
			}
			return dbExpression;
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x00118F80 File Offset: 0x00117180
		private static void ValidateQueryResultType(TypeUsage resultType, ErrorContext errCtx)
		{
			if (Helper.IsCollectionType(resultType.EdmType))
			{
				SemanticAnalyzer.ValidateQueryResultType(((CollectionType)resultType.EdmType).TypeUsage, errCtx);
				return;
			}
			if (Helper.IsRowType(resultType.EdmType))
			{
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = ((RowType)resultType.EdmType).Properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EdmProperty edmProperty = enumerator.Current;
						SemanticAnalyzer.ValidateQueryResultType(edmProperty.TypeUsage, errCtx);
					}
					return;
				}
			}
			if (Helper.IsAssociationType(resultType.EdmType))
			{
				string text = Strings.InvalidQueryResultType(resultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x00119038 File Offset: 0x00117238
		private static List<FunctionDefinition> ConvertInlineFunctionDefinitions(NodeList<FunctionDefinition> functionDefList, SemanticResolver sr)
		{
			List<FunctionDefinition> list = new List<FunctionDefinition>();
			if (functionDefList != null)
			{
				List<InlineFunctionInfo> list2 = new List<InlineFunctionInfo>();
				foreach (FunctionDefinition functionDefinition in ((IEnumerable<FunctionDefinition>)functionDefList))
				{
					string name = functionDefinition.Name;
					List<DbVariableReferenceExpression> list3 = SemanticAnalyzer.ConvertInlineFunctionParameterDefs(functionDefinition.Parameters, sr);
					InlineFunctionInfo inlineFunctionInfo = new SemanticAnalyzer.InlineFunctionInfoImpl(functionDefinition, list3);
					list2.Add(inlineFunctionInfo);
					sr.TypeResolver.DeclareInlineFunction(name, inlineFunctionInfo);
				}
				foreach (InlineFunctionInfo inlineFunctionInfo2 in list2)
				{
					list.Add(new FunctionDefinition(inlineFunctionInfo2.FunctionDefAst.Name, inlineFunctionInfo2.GetLambda(sr), inlineFunctionInfo2.FunctionDefAst.StartPosition, inlineFunctionInfo2.FunctionDefAst.EndPosition));
				}
			}
			return list;
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x00119130 File Offset: 0x00117330
		private static List<DbVariableReferenceExpression> ConvertInlineFunctionParameterDefs(NodeList<PropDefinition> parameterDefs, SemanticResolver sr)
		{
			List<DbVariableReferenceExpression> list = new List<DbVariableReferenceExpression>();
			if (parameterDefs != null)
			{
				foreach (PropDefinition propDefinition in ((IEnumerable<PropDefinition>)parameterDefs))
				{
					string name = propDefinition.Name.Name;
					if (list.Exists((DbVariableReferenceExpression arg) => sr.NameComparer.Compare(arg.VariableName, name) == 0))
					{
						ErrorContext errCtx = propDefinition.ErrCtx;
						string text = Strings.MultipleDefinitionsOfParameter(name);
						throw EntitySqlException.Create(errCtx, text, null);
					}
					DbVariableReferenceExpression dbVariableReferenceExpression = new DbVariableReferenceExpression(SemanticAnalyzer.ConvertTypeDefinition(propDefinition.Type, sr), name);
					list.Add(dbVariableReferenceExpression);
				}
			}
			return list;
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x00119214 File Offset: 0x00117414
		private static DbLambda ConvertInlineFunctionDefinition(InlineFunctionInfo functionInfo, SemanticResolver sr)
		{
			sr.EnterScope();
			functionInfo.Parameters.Each((DbVariableReferenceExpression p) => sr.CurrentScope.Add(p.VariableName, new FreeVariableScopeEntry(p)));
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(functionInfo.FunctionDefAst.Body, sr);
			sr.LeaveScope();
			return DbExpressionBuilder.Lambda(dbExpression, functionInfo.Parameters);
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x0011927C File Offset: 0x0011747C
		private static ExpressionResolution Convert(Node astExpr, SemanticResolver sr)
		{
			SemanticAnalyzer.AstExprConverter astExprConverter = SemanticAnalyzer._astExprConverters[astExpr.GetType()];
			if (astExprConverter == null)
			{
				throw new EntitySqlException(Strings.UnknownAstExpressionType);
			}
			return astExprConverter(astExpr, sr);
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x001192A4 File Offset: 0x001174A4
		private static DbExpression ConvertValueExpression(Node astExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astExpr, sr);
			if (dbExpression == null)
			{
				ErrorContext errCtx = astExpr.ErrCtx;
				string expressionCannotBeNull = Strings.ExpressionCannotBeNull;
				throw EntitySqlException.Create(errCtx, expressionCannotBeNull, null);
			}
			return dbExpression;
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x001192D0 File Offset: 0x001174D0
		private static DbExpression ConvertValueExpressionAllowUntypedNulls(Node astExpr, SemanticResolver sr)
		{
			ExpressionResolution expressionResolution = SemanticAnalyzer.Convert(astExpr, sr);
			if (expressionResolution.ExpressionClass == ExpressionResolutionClass.Value)
			{
				return ((ValueExpression)expressionResolution).Value;
			}
			if (expressionResolution.ExpressionClass == ExpressionResolutionClass.MetadataMember)
			{
				MetadataMember metadataMember = (MetadataMember)expressionResolution;
				if (metadataMember.MetadataMemberClass == MetadataMemberClass.EnumMember)
				{
					MetadataEnumMember metadataEnumMember = (MetadataEnumMember)metadataMember;
					return metadataEnumMember.EnumType.Constant(metadataEnumMember.EnumMember.Value);
				}
			}
			string text = Strings.InvalidExpressionResolutionClass(expressionResolution.ExpressionClassName, ValueExpression.ValueClassName);
			Identifier identifier = astExpr as Identifier;
			if (identifier != null)
			{
				text = Strings.CouldNotResolveIdentifier(identifier.Name);
			}
			DotExpr dotExpr = astExpr as DotExpr;
			string[] array;
			if (dotExpr != null && dotExpr.IsMultipartIdentifier(out array))
			{
				text = Strings.CouldNotResolveIdentifier(TypeResolver.GetFullName(array));
			}
			throw EntitySqlException.Create(astExpr.ErrCtx, text, null);
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x0011938C File Offset: 0x0011758C
		private static Pair<DbExpression, DbExpression> ConvertValueExpressionsWithUntypedNulls(Node leftAst, Node rightAst, ErrorContext errCtx, Func<string> formatMessage, SemanticResolver sr)
		{
			DbExpression dbExpression = ((leftAst != null) ? SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(leftAst, sr) : null);
			DbExpression dbExpression2 = ((rightAst != null) ? SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(rightAst, sr) : null);
			if (dbExpression == null)
			{
				if (dbExpression2 == null)
				{
					string text = formatMessage();
					throw EntitySqlException.Create(errCtx, text, null);
				}
				dbExpression = dbExpression2.ResultType.Null();
			}
			else if (dbExpression2 == null)
			{
				dbExpression2 = dbExpression.ResultType.Null();
			}
			return new Pair<DbExpression, DbExpression>(dbExpression, dbExpression2);
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x001193F4 File Offset: 0x001175F4
		private static ExpressionResolution ConvertLiteral(Node expr, SemanticResolver sr)
		{
			Literal literal = (Literal)expr;
			if (literal.IsNullLiteral)
			{
				return new ValueExpression(null);
			}
			return new ValueExpression(SemanticAnalyzer.GetLiteralTypeUsage(literal).Constant(literal.Value));
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x00119430 File Offset: 0x00117630
		private static TypeUsage GetLiteralTypeUsage(Literal literal)
		{
			PrimitiveType primitiveType = null;
			if (!ClrProviderManifest.Instance.TryGetPrimitiveType(literal.Type, out primitiveType))
			{
				ErrorContext errCtx = literal.ErrCtx;
				string text = Strings.LiteralTypeNotFoundInMetadata(literal.OriginalValue);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			return TypeHelpers.GetLiteralTypeUsage(primitiveType.PrimitiveTypeKind, literal.IsUnicodeString);
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x0011947E File Offset: 0x0011767E
		private static ExpressionResolution ConvertIdentifier(Node expr, SemanticResolver sr)
		{
			return SemanticAnalyzer.ConvertIdentifier((Identifier)expr, false, sr);
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x0011948D File Offset: 0x0011768D
		private static ExpressionResolution ConvertIdentifier(Identifier identifier, bool leftHandSideOfMemberAccess, SemanticResolver sr)
		{
			return sr.ResolveSimpleName(identifier.Name, leftHandSideOfMemberAccess, identifier.ErrCtx);
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x001194A4 File Offset: 0x001176A4
		private static ExpressionResolution ConvertDotExpr(Node expr, SemanticResolver sr)
		{
			DotExpr dotExpr = (DotExpr)expr;
			ValueExpression valueExpression;
			if (sr.TryResolveDotExprAsGroupKeyAlternativeName(dotExpr, out valueExpression))
			{
				return valueExpression;
			}
			Identifier identifier = dotExpr.Left as Identifier;
			ExpressionResolution expressionResolution;
			if (identifier != null)
			{
				expressionResolution = SemanticAnalyzer.ConvertIdentifier(identifier, true, sr);
			}
			else
			{
				expressionResolution = SemanticAnalyzer.Convert(dotExpr.Left, sr);
			}
			switch (expressionResolution.ExpressionClass)
			{
			case ExpressionResolutionClass.Value:
				return sr.ResolvePropertyAccess(((ValueExpression)expressionResolution).Value, dotExpr.Identifier.Name, dotExpr.Identifier.ErrCtx);
			case ExpressionResolutionClass.EntityContainer:
				return sr.ResolveEntityContainerMemberAccess(((EntityContainerExpression)expressionResolution).EntityContainer, dotExpr.Identifier.Name, dotExpr.Identifier.ErrCtx);
			case ExpressionResolutionClass.MetadataMember:
				return sr.ResolveMetadataMemberAccess((MetadataMember)expressionResolution, dotExpr.Identifier.Name, dotExpr.Identifier.ErrCtx);
			default:
			{
				ErrorContext errCtx = dotExpr.Left.ErrCtx;
				string text = Strings.UnknownExpressionResolutionClass(expressionResolution.ExpressionClass);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			}
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x001195A1 File Offset: 0x001177A1
		private static ExpressionResolution ConvertParenExpr(Node astExpr, SemanticResolver sr)
		{
			return new ValueExpression(SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(((ParenExpr)astExpr).Expr, sr));
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x001195BC File Offset: 0x001177BC
		private static ExpressionResolution ConvertGroupPartitionExpr(Node astExpr, SemanticResolver sr)
		{
			GroupPartitionExpr groupPartitionExpr = (GroupPartitionExpr)astExpr;
			DbExpression dbExpression = null;
			if (!SemanticAnalyzer.TryConvertAsResolvedGroupAggregate(groupPartitionExpr, sr, out dbExpression))
			{
				if (!sr.IsInAnyGroupScope())
				{
					ErrorContext errCtx = astExpr.ErrCtx;
					string groupPartitionOutOfContext = Strings.GroupPartitionOutOfContext;
					throw EntitySqlException.Create(errCtx, groupPartitionOutOfContext, null);
				}
				GroupPartitionInfo groupPartitionInfo;
				DbExpression dbExpression2;
				using (sr.EnterGroupPartition(groupPartitionExpr, groupPartitionExpr.ErrCtx, out groupPartitionInfo))
				{
					dbExpression2 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(groupPartitionExpr.ArgExpr, sr);
				}
				if (dbExpression2 == null)
				{
					ErrorContext errCtx2 = groupPartitionExpr.ArgExpr.ErrCtx;
					string resultingExpressionTypeCannotBeNull = Strings.ResultingExpressionTypeCannotBeNull;
					throw EntitySqlException.Create(errCtx2, resultingExpressionTypeCannotBeNull, null);
				}
				DbExpression dbExpression3 = groupPartitionInfo.EvaluatingScopeRegion.GroupAggregateBinding.Project(dbExpression2);
				if (groupPartitionExpr.DistinctKind == DistinctKind.Distinct)
				{
					SemanticAnalyzer.ValidateDistinctProjection(dbExpression3.ResultType, groupPartitionExpr.ArgExpr.ErrCtx, null);
					dbExpression3 = dbExpression3.Distinct();
				}
				groupPartitionInfo.AttachToAstNode(sr.GenerateInternalName("groupPartition"), dbExpression3);
				groupPartitionInfo.EvaluatingScopeRegion.GroupAggregateInfos.Add(groupPartitionInfo);
				dbExpression = groupPartitionInfo.AggregateStubExpression;
			}
			return new ValueExpression(dbExpression);
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x001196C8 File Offset: 0x001178C8
		private static ExpressionResolution ConvertMethodExpr(Node expr, SemanticResolver sr)
		{
			return SemanticAnalyzer.ConvertMethodExpr((MethodExpr)expr, true, sr);
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x001196D8 File Offset: 0x001178D8
		private static ExpressionResolution ConvertMethodExpr(MethodExpr methodExpr, bool includeInlineFunctions, SemanticResolver sr)
		{
			ExpressionResolution expressionResolution;
			using (sr.TypeResolver.EnterFunctionNameResolution(includeInlineFunctions))
			{
				Identifier identifier = methodExpr.Expr as Identifier;
				if (identifier != null)
				{
					expressionResolution = sr.ResolveSimpleFunctionName(identifier.Name, identifier.ErrCtx);
				}
				else
				{
					DotExpr dotExpr = methodExpr.Expr as DotExpr;
					using (SemanticAnalyzer.ConvertMethodExpr_TryEnterIgnoreEntityContainerNameResolution(dotExpr, sr))
					{
						using (SemanticAnalyzer.ConvertMethodExpr_TryEnterV1ViewGenBackwardCompatibilityResolution(dotExpr, sr))
						{
							expressionResolution = SemanticAnalyzer.Convert(methodExpr.Expr, sr);
						}
					}
				}
			}
			if (expressionResolution.ExpressionClass != ExpressionResolutionClass.MetadataMember)
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string methodInvocationNotSupported = Strings.MethodInvocationNotSupported;
				throw EntitySqlException.Create(errCtx, methodInvocationNotSupported, null);
			}
			MetadataMember metadataMember = (MetadataMember)expressionResolution;
			if (metadataMember.MetadataMemberClass == MetadataMemberClass.InlineFunctionGroup)
			{
				methodExpr.ErrCtx.ErrorContextInfo = Strings.CtxFunction(metadataMember.Name);
				methodExpr.ErrCtx.UseContextInfoAsResourceIdentifier = false;
				ValueExpression valueExpression;
				if (SemanticAnalyzer.TryConvertInlineFunctionCall((InlineFunctionGroup)metadataMember, methodExpr, sr, out valueExpression))
				{
					return valueExpression;
				}
				return SemanticAnalyzer.ConvertMethodExpr(methodExpr, false, sr);
			}
			else
			{
				MetadataMemberClass metadataMemberClass = metadataMember.MetadataMemberClass;
				if (metadataMemberClass == MetadataMemberClass.Type)
				{
					methodExpr.ErrCtx.ErrorContextInfo = Strings.CtxTypeCtor(metadataMember.Name);
					methodExpr.ErrCtx.UseContextInfoAsResourceIdentifier = false;
					return SemanticAnalyzer.ConvertTypeConstructorCall((MetadataType)metadataMember, methodExpr, sr);
				}
				if (metadataMemberClass != MetadataMemberClass.FunctionGroup)
				{
					ErrorContext errCtx2 = methodExpr.Expr.ErrCtx;
					string text = Strings.CannotResolveNameToTypeOrFunction(metadataMember.Name);
					throw EntitySqlException.Create(errCtx2, text, null);
				}
				methodExpr.ErrCtx.ErrorContextInfo = Strings.CtxFunction(metadataMember.Name);
				methodExpr.ErrCtx.UseContextInfoAsResourceIdentifier = false;
				return SemanticAnalyzer.ConvertModelFunctionCall((MetadataFunctionGroup)metadataMember, methodExpr, sr);
			}
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x0011989C File Offset: 0x00117A9C
		private static IDisposable ConvertMethodExpr_TryEnterIgnoreEntityContainerNameResolution(DotExpr leftExpr, SemanticResolver sr)
		{
			if (leftExpr == null || !(leftExpr.Left is Identifier))
			{
				return null;
			}
			return sr.EnterIgnoreEntityContainerNameResolution();
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x001198B8 File Offset: 0x00117AB8
		private static IDisposable ConvertMethodExpr_TryEnterV1ViewGenBackwardCompatibilityResolution(DotExpr leftExpr, SemanticResolver sr)
		{
			if (leftExpr != null && leftExpr.Left is Identifier && (sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.RestrictedViewGenerationMode || sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.UserViewGenerationMode) && (sr.TypeResolver.Perspective.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace) as StorageMappingItemCollection).MappingVersion < 2.0)
			{
				return sr.TypeResolver.EnterBackwardCompatibilityResolution();
			}
			return null;
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x0011992C File Offset: 0x00117B2C
		private static bool TryConvertInlineFunctionCall(InlineFunctionGroup inlineFunctionGroup, MethodExpr methodExpr, SemanticResolver sr, out ValueExpression inlineFunctionCall)
		{
			inlineFunctionCall = null;
			if (methodExpr.DistinctKind != DistinctKind.None)
			{
				return false;
			}
			List<TypeUsage> list2;
			List<DbExpression> list = SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out list2);
			bool flag = false;
			InlineFunctionInfo inlineFunctionInfo = SemanticResolver.ResolveFunctionOverloads<InlineFunctionInfo, DbVariableReferenceExpression>(inlineFunctionGroup.FunctionMetadata, list2, (InlineFunctionInfo lambdaOverload) => lambdaOverload.Parameters, (DbVariableReferenceExpression varRef) => varRef.ResultType, (DbVariableReferenceExpression varRef) => ParameterMode.In, false, out flag);
			if (flag)
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string ambiguousFunctionArguments = Strings.AmbiguousFunctionArguments;
				throw EntitySqlException.Create(errCtx, ambiguousFunctionArguments, null);
			}
			if (inlineFunctionInfo == null)
			{
				return false;
			}
			SemanticAnalyzer.ConvertUntypedNullsInArguments<DbVariableReferenceExpression>(list, inlineFunctionInfo.Parameters, (DbVariableReferenceExpression formal) => formal.ResultType);
			inlineFunctionCall = new ValueExpression(inlineFunctionInfo.GetLambda(sr).Invoke(list));
			return true;
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x00119A24 File Offset: 0x00117C24
		private static ValueExpression ConvertTypeConstructorCall(MetadataType metadataType, MethodExpr methodExpr, SemanticResolver sr)
		{
			if (!TypeSemantics.IsComplexType(metadataType.TypeUsage) && !TypeSemantics.IsEntityType(metadataType.TypeUsage) && !TypeSemantics.IsRelationshipType(metadataType.TypeUsage))
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string text = Strings.InvalidCtorUseOnType(metadataType.TypeUsage.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			if (metadataType.TypeUsage.EdmType.Abstract)
			{
				ErrorContext errCtx2 = methodExpr.ErrCtx;
				string text2 = Strings.CannotInstantiateAbstractType(metadataType.TypeUsage.EdmType.FullName);
				throw EntitySqlException.Create(errCtx2, text2, null);
			}
			if (methodExpr.DistinctKind != DistinctKind.None)
			{
				ErrorContext errCtx3 = methodExpr.ErrCtx;
				string invalidDistinctArgumentInCtor = Strings.InvalidDistinctArgumentInCtor;
				throw EntitySqlException.Create(errCtx3, invalidDistinctArgumentInCtor, null);
			}
			List<DbRelatedEntityRef> list = null;
			if (methodExpr.HasRelationships)
			{
				if (sr.ParserOptions.ParserCompilationMode != ParserOptions.CompilationMode.RestrictedViewGenerationMode && sr.ParserOptions.ParserCompilationMode != ParserOptions.CompilationMode.UserViewGenerationMode)
				{
					ErrorContext errCtx4 = methodExpr.Relationships.ErrCtx;
					string invalidModeForWithRelationshipClause = Strings.InvalidModeForWithRelationshipClause;
					throw EntitySqlException.Create(errCtx4, invalidModeForWithRelationshipClause, null);
				}
				EntityType entityType = metadataType.TypeUsage.EdmType as EntityType;
				if (entityType == null)
				{
					ErrorContext errCtx5 = methodExpr.Relationships.ErrCtx;
					string invalidTypeForWithRelationshipClause = Strings.InvalidTypeForWithRelationshipClause;
					throw EntitySqlException.Create(errCtx5, invalidTypeForWithRelationshipClause, null);
				}
				HashSet<string> hashSet = new HashSet<string>();
				list = new List<DbRelatedEntityRef>(methodExpr.Relationships.Count);
				for (int i = 0; i < methodExpr.Relationships.Count; i++)
				{
					RelshipNavigationExpr relshipNavigationExpr = methodExpr.Relationships[i];
					DbRelatedEntityRef dbRelatedEntityRef = SemanticAnalyzer.ConvertRelatedEntityRef(relshipNavigationExpr, entityType, sr);
					string text3 = string.Join(":", new string[]
					{
						dbRelatedEntityRef.TargetEnd.DeclaringType.Identity,
						dbRelatedEntityRef.TargetEnd.Identity
					});
					if (hashSet.Contains(text3))
					{
						ErrorContext errCtx6 = relshipNavigationExpr.ErrCtx;
						string text4 = Strings.RelationshipTargetMustBeUnique(text3);
						throw EntitySqlException.Create(errCtx6, text4, null);
					}
					hashSet.Add(text3);
					list.Add(dbRelatedEntityRef);
				}
			}
			List<TypeUsage> list2;
			return new ValueExpression(SemanticAnalyzer.CreateConstructorCallExpression(methodExpr, metadataType.TypeUsage, SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out list2), list, sr));
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x00119C24 File Offset: 0x00117E24
		private static ValueExpression ConvertModelFunctionCall(MetadataFunctionGroup metadataFunctionGroup, MethodExpr methodExpr, SemanticResolver sr)
		{
			if (metadataFunctionGroup.FunctionMetadata.Any((EdmFunction f) => !f.IsComposableAttribute))
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string text = Strings.CannotCallNoncomposableFunction(metadataFunctionGroup.Name);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			if (TypeSemantics.IsAggregateFunction(metadataFunctionGroup.FunctionMetadata[0]) && sr.IsInAnyGroupScope())
			{
				return new ValueExpression(SemanticAnalyzer.ConvertAggregateFunctionInGroupScope(methodExpr, metadataFunctionGroup, sr));
			}
			return new ValueExpression(SemanticAnalyzer.CreateModelFunctionCallExpression(methodExpr, metadataFunctionGroup, sr));
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x00119CB0 File Offset: 0x00117EB0
		private static DbExpression ConvertAggregateFunctionInGroupScope(MethodExpr methodExpr, MetadataFunctionGroup metadataFunctionGroup, SemanticResolver sr)
		{
			DbExpression dbExpression = null;
			if (SemanticAnalyzer.TryConvertAsResolvedGroupAggregate(methodExpr, sr, out dbExpression))
			{
				return dbExpression;
			}
			ScopeRegion scopeRegion = ((sr.CurrentGroupAggregateInfo != null) ? sr.CurrentGroupAggregateInfo.InnermostReferencedScopeRegion : null);
			List<TypeUsage> list;
			if (SemanticAnalyzer.TryConvertAsCollectionFunction(methodExpr, metadataFunctionGroup, sr, out list, out dbExpression))
			{
				return dbExpression;
			}
			if (sr.CurrentGroupAggregateInfo != null)
			{
				sr.CurrentGroupAggregateInfo.InnermostReferencedScopeRegion = scopeRegion;
			}
			if (SemanticAnalyzer.TryConvertAsFunctionAggregate(methodExpr, metadataFunctionGroup, list, sr, out dbExpression))
			{
				return dbExpression;
			}
			ErrorContext errCtx = methodExpr.ErrCtx;
			string text = Strings.FailedToResolveAggregateFunction(metadataFunctionGroup.Name);
			throw EntitySqlException.Create(errCtx, text, null);
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x00119D30 File Offset: 0x00117F30
		private static bool TryConvertAsResolvedGroupAggregate(GroupAggregateExpr groupAggregateExpr, SemanticResolver sr, out DbExpression converted)
		{
			converted = null;
			if (groupAggregateExpr.AggregateInfo == null)
			{
				return false;
			}
			groupAggregateExpr.AggregateInfo.SetContainingAggregate(sr.CurrentGroupAggregateInfo);
			if (!sr.TryResolveInternalAggregateName(groupAggregateExpr.AggregateInfo.AggregateName, groupAggregateExpr.AggregateInfo.ErrCtx, out converted))
			{
				converted = groupAggregateExpr.AggregateInfo.AggregateStubExpression;
			}
			return true;
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x00119D88 File Offset: 0x00117F88
		private static bool TryConvertAsCollectionFunction(MethodExpr methodExpr, MetadataFunctionGroup metadataFunctionGroup, SemanticResolver sr, out List<TypeUsage> argTypes, out DbExpression converted)
		{
			List<DbExpression> list = SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out argTypes);
			bool flag = false;
			EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, argTypes, false, out flag);
			if (flag)
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string ambiguousFunctionArguments = Strings.AmbiguousFunctionArguments;
				throw EntitySqlException.Create(errCtx, ambiguousFunctionArguments, null);
			}
			if (edmFunction != null)
			{
				SemanticAnalyzer.ConvertUntypedNullsInArguments<FunctionParameter>(list, edmFunction.Parameters, (FunctionParameter parameter) => parameter.TypeUsage);
				converted = edmFunction.Invoke(list);
				return true;
			}
			converted = null;
			return false;
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x00119E0C File Offset: 0x0011800C
		private static bool TryConvertAsFunctionAggregate(MethodExpr methodExpr, MetadataFunctionGroup metadataFunctionGroup, List<TypeUsage> argTypes, SemanticResolver sr, out DbExpression converted)
		{
			converted = null;
			bool flag = false;
			EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, argTypes, true, out flag);
			if (flag)
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string ambiguousFunctionArguments = Strings.AmbiguousFunctionArguments;
				throw EntitySqlException.Create(errCtx, ambiguousFunctionArguments, null);
			}
			if (edmFunction == null)
			{
				CqlErrorHelper.ReportFunctionOverloadError(methodExpr, metadataFunctionGroup.FunctionMetadata[0], argTypes);
			}
			FunctionAggregateInfo functionAggregateInfo;
			List<DbExpression> list;
			using (sr.EnterFunctionAggregate(methodExpr, methodExpr.ErrCtx, out functionAggregateInfo))
			{
				List<TypeUsage> list2;
				list = SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out list2);
			}
			SemanticAnalyzer.ConvertUntypedNullsInArguments<FunctionParameter>(list, edmFunction.Parameters, (FunctionParameter parameter) => TypeHelpers.GetElementTypeUsage(parameter.TypeUsage));
			DbFunctionAggregate dbFunctionAggregate;
			if (methodExpr.DistinctKind == DistinctKind.Distinct)
			{
				dbFunctionAggregate = edmFunction.AggregateDistinct(list);
			}
			else
			{
				dbFunctionAggregate = edmFunction.Aggregate(list);
			}
			functionAggregateInfo.AttachToAstNode(sr.GenerateInternalName("groupAgg" + edmFunction.Name), dbFunctionAggregate);
			functionAggregateInfo.EvaluatingScopeRegion.GroupAggregateInfos.Add(functionAggregateInfo);
			converted = functionAggregateInfo.AggregateStubExpression;
			return true;
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x00119F20 File Offset: 0x00118120
		private static DbExpression CreateConstructorCallExpression(MethodExpr methodExpr, TypeUsage type, List<DbExpression> args, List<DbRelatedEntityRef> relshipExprList, SemanticResolver sr)
		{
			int num = 0;
			int count = args.Count;
			StructuralType structuralType = (StructuralType)type.EdmType;
			foreach (object obj in TypeHelpers.GetAllStructuralMembers(structuralType))
			{
				EdmMember edmMember = (EdmMember)obj;
				TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(edmMember);
				if (count <= num)
				{
					ErrorContext errCtx = methodExpr.ErrCtx;
					string text = Strings.NumberOfTypeCtorIsLessThenFormalSpec(edmMember.Name);
					throw EntitySqlException.Create(errCtx, text, null);
				}
				if (args[num] == null)
				{
					EdmProperty edmProperty = edmMember as EdmProperty;
					if (edmProperty != null && !edmProperty.Nullable)
					{
						ErrorContext errCtx2 = methodExpr.Args[num].ErrCtx;
						string text2 = Strings.InvalidNullLiteralForNonNullableMember(edmMember.Name, structuralType.FullName);
						throw EntitySqlException.Create(errCtx2, text2, null);
					}
					args[num] = modelTypeUsage.Null();
				}
				bool flag = TypeSemantics.IsPromotableTo(args[num].ResultType, modelTypeUsage);
				if (ParserOptions.CompilationMode.RestrictedViewGenerationMode == sr.ParserOptions.ParserCompilationMode || ParserOptions.CompilationMode.UserViewGenerationMode == sr.ParserOptions.ParserCompilationMode)
				{
					if (!flag && !TypeSemantics.IsPromotableTo(modelTypeUsage, args[num].ResultType))
					{
						ErrorContext errCtx3 = methodExpr.Args[num].ErrCtx;
						string text3 = Strings.InvalidCtorArgumentType(args[num].ResultType.EdmType.FullName, edmMember.Name, modelTypeUsage.EdmType.FullName);
						throw EntitySqlException.Create(errCtx3, text3, null);
					}
					if (Helper.IsPrimitiveType(modelTypeUsage.EdmType) && !TypeSemantics.IsSubTypeOf(args[num].ResultType, modelTypeUsage))
					{
						args[num] = args[num].CastTo(modelTypeUsage);
					}
				}
				else if (!flag)
				{
					ErrorContext errCtx4 = methodExpr.Args[num].ErrCtx;
					string text4 = Strings.InvalidCtorArgumentType(args[num].ResultType.EdmType.FullName, edmMember.Name, modelTypeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx4, text4, null);
				}
				num++;
			}
			if (num != count)
			{
				ErrorContext errCtx5 = methodExpr.ErrCtx;
				string text5 = Strings.NumberOfTypeCtorIsMoreThenFormalSpec(structuralType.FullName);
				throw EntitySqlException.Create(errCtx5, text5, null);
			}
			DbExpression dbExpression;
			if (relshipExprList != null && relshipExprList.Count > 0)
			{
				dbExpression = DbExpressionBuilder.CreateNewEntityWithRelationshipsExpression((EntityType)type.EdmType, args, relshipExprList);
			}
			else
			{
				dbExpression = TypeHelpers.GetReadOnlyType(type).New(args);
			}
			return dbExpression;
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x0011A19C File Offset: 0x0011839C
		private static DbFunctionExpression CreateModelFunctionCallExpression(MethodExpr methodExpr, MetadataFunctionGroup metadataFunctionGroup, SemanticResolver sr)
		{
			bool flag = false;
			if (methodExpr.DistinctKind != DistinctKind.None)
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string invalidDistinctArgumentInNonAggFunction = Strings.InvalidDistinctArgumentInNonAggFunction;
				throw EntitySqlException.Create(errCtx, invalidDistinctArgumentInNonAggFunction, null);
			}
			List<TypeUsage> list2;
			List<DbExpression> list = SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out list2);
			EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, list2, false, out flag);
			if (flag)
			{
				ErrorContext errCtx2 = methodExpr.ErrCtx;
				string ambiguousFunctionArguments = Strings.AmbiguousFunctionArguments;
				throw EntitySqlException.Create(errCtx2, ambiguousFunctionArguments, null);
			}
			if (edmFunction == null)
			{
				CqlErrorHelper.ReportFunctionOverloadError(methodExpr, metadataFunctionGroup.FunctionMetadata[0], list2);
			}
			SemanticAnalyzer.ConvertUntypedNullsInArguments<FunctionParameter>(list, edmFunction.Parameters, (FunctionParameter parameter) => parameter.TypeUsage);
			return edmFunction.Invoke(list);
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x0011A24C File Offset: 0x0011844C
		private static List<DbExpression> ConvertFunctionArguments(NodeList<Node> astExprList, SemanticResolver sr, out List<TypeUsage> argTypes)
		{
			List<DbExpression> list = new List<DbExpression>();
			if (astExprList != null)
			{
				for (int i = 0; i < astExprList.Count; i++)
				{
					list.Add(SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astExprList[i], sr));
				}
			}
			argTypes = list.Select(delegate(DbExpression a)
			{
				if (a == null)
				{
					return null;
				}
				return a.ResultType;
			}).ToList<TypeUsage>();
			return list;
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x0011A2B4 File Offset: 0x001184B4
		private static void ConvertUntypedNullsInArguments<TParameterMetadata>(List<DbExpression> args, IList<TParameterMetadata> parametersMetadata, Func<TParameterMetadata, TypeUsage> getParameterTypeUsage)
		{
			for (int i = 0; i < args.Count; i++)
			{
				if (args[i] == null)
				{
					args[i] = getParameterTypeUsage(parametersMetadata[i]).Null();
				}
			}
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x0011A2F4 File Offset: 0x001184F4
		private static ExpressionResolution ConvertParameter(Node expr, SemanticResolver sr)
		{
			QueryParameter queryParameter = (QueryParameter)expr;
			DbParameterReferenceExpression dbParameterReferenceExpression;
			if (sr.Parameters == null || !sr.Parameters.TryGetValue(queryParameter.Name, out dbParameterReferenceExpression))
			{
				ErrorContext errCtx = queryParameter.ErrCtx;
				string text = Strings.ParameterWasNotDefined(queryParameter.Name);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			return new ValueExpression(dbParameterReferenceExpression);
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x0011A348 File Offset: 0x00118548
		private static DbRelatedEntityRef ConvertRelatedEntityRef(RelshipNavigationExpr relshipExpr, EntityType driverEntityType, SemanticResolver sr)
		{
			EdmType edmType = SemanticAnalyzer.ConvertTypeName(relshipExpr.TypeName, sr).EdmType;
			RelationshipType relationshipType = edmType as RelationshipType;
			if (relationshipType == null)
			{
				ErrorContext errCtx = relshipExpr.TypeName.ErrCtx;
				string text = Strings.RelationshipTypeExpected(edmType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(relshipExpr.RefExpr, sr);
			RefType refType = dbExpression.ResultType.EdmType as RefType;
			if (refType == null)
			{
				ErrorContext errCtx2 = relshipExpr.RefExpr.ErrCtx;
				string relatedEndExprTypeMustBeReference = Strings.RelatedEndExprTypeMustBeReference;
				throw EntitySqlException.Create(errCtx2, relatedEndExprTypeMustBeReference, null);
			}
			RelationshipEndMember toEnd;
			if (relshipExpr.ToEndIdentifier != null)
			{
				toEnd = (RelationshipEndMember)relationshipType.Members.FirstOrDefault((EdmMember m) => m.Name.Equals(relshipExpr.ToEndIdentifier.Name, StringComparison.OrdinalIgnoreCase));
				if (toEnd == null)
				{
					ErrorContext errCtx3 = relshipExpr.ToEndIdentifier.ErrCtx;
					string text2 = Strings.InvalidRelationshipMember(relshipExpr.ToEndIdentifier.Name, relationshipType.FullName);
					throw EntitySqlException.Create(errCtx3, text2, null);
				}
				if (toEnd.RelationshipMultiplicity != RelationshipMultiplicity.One && toEnd.RelationshipMultiplicity != RelationshipMultiplicity.ZeroOrOne)
				{
					ErrorContext errCtx4 = relshipExpr.ToEndIdentifier.ErrCtx;
					string text3 = Strings.InvalidWithRelationshipTargetEndMultiplicity(toEnd.Name, toEnd.RelationshipMultiplicity.ToString());
					throw EntitySqlException.Create(errCtx4, text3, null);
				}
				if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(refType, toEnd.TypeUsage.EdmType))
				{
					ErrorContext errCtx5 = relshipExpr.RefExpr.ErrCtx;
					string text4 = Strings.RelatedEndExprTypeMustBePromotoableToToEnd(refType.FullName, toEnd.TypeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx5, text4, null);
				}
			}
			else
			{
				RelationshipEndMember[] array = (from m in relationshipType.Members
					select (RelationshipEndMember)m into e
					where TypeSemantics.IsStructurallyEqualOrPromotableTo(refType, e.TypeUsage.EdmType) && (e.RelationshipMultiplicity == RelationshipMultiplicity.One || e.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
					select e).ToArray<RelationshipEndMember>();
				int num = array.Length;
				if (num == 0)
				{
					ErrorContext errCtx6 = relshipExpr.ErrCtx;
					string text5 = Strings.InvalidImplicitRelationshipToEnd(relationshipType.FullName);
					throw EntitySqlException.Create(errCtx6, text5, null);
				}
				if (num != 1)
				{
					ErrorContext errCtx7 = relshipExpr.ErrCtx;
					string relationshipToEndIsAmbiguos = Strings.RelationshipToEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx7, relationshipToEndIsAmbiguos, null);
				}
				toEnd = array[0];
			}
			RelationshipEndMember relationshipEndMember;
			if (relshipExpr.FromEndIdentifier != null)
			{
				relationshipEndMember = (RelationshipEndMember)relationshipType.Members.FirstOrDefault((EdmMember m) => m.Name.Equals(relshipExpr.FromEndIdentifier.Name, StringComparison.OrdinalIgnoreCase));
				if (relationshipEndMember == null)
				{
					ErrorContext errCtx8 = relshipExpr.FromEndIdentifier.ErrCtx;
					string text6 = Strings.InvalidRelationshipMember(relshipExpr.FromEndIdentifier.Name, relationshipType.FullName);
					throw EntitySqlException.Create(errCtx8, text6, null);
				}
				if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(driverEntityType.GetReferenceType(), relationshipEndMember.TypeUsage.EdmType))
				{
					ErrorContext errCtx9 = relshipExpr.FromEndIdentifier.ErrCtx;
					string text7 = Strings.SourceTypeMustBePromotoableToFromEndRelationType(driverEntityType.FullName, relationshipEndMember.TypeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx9, text7, null);
				}
				if (relationshipEndMember.EdmEquals(toEnd))
				{
					ErrorContext errCtx10 = relshipExpr.ErrCtx;
					string relationshipFromEndIsAmbiguos = Strings.RelationshipFromEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx10, relationshipFromEndIsAmbiguos, null);
				}
			}
			else
			{
				RelationshipEndMember[] array2 = (from m in relationshipType.Members
					select (RelationshipEndMember)m into e
					where TypeSemantics.IsStructurallyEqualOrPromotableTo(driverEntityType.GetReferenceType(), e.TypeUsage.EdmType) && !e.EdmEquals(toEnd)
					select e).ToArray<RelationshipEndMember>();
				int num = array2.Length;
				if (num == 0)
				{
					ErrorContext errCtx11 = relshipExpr.ErrCtx;
					string text8 = Strings.InvalidImplicitRelationshipFromEnd(relationshipType.FullName);
					throw EntitySqlException.Create(errCtx11, text8, null);
				}
				if (num != 1)
				{
					ErrorContext errCtx12 = relshipExpr.ErrCtx;
					string relationshipFromEndIsAmbiguos2 = Strings.RelationshipFromEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx12, relationshipFromEndIsAmbiguos2, null);
				}
				relationshipEndMember = array2[0];
			}
			return DbExpressionBuilder.CreateRelatedEntityRef(relationshipEndMember, toEnd, dbExpression);
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x0011A75C File Offset: 0x0011895C
		private static ExpressionResolution ConvertRelshipNavigationExpr(Node astExpr, SemanticResolver sr)
		{
			RelshipNavigationExpr relshipExpr = (RelshipNavigationExpr)astExpr;
			EdmType edmType = SemanticAnalyzer.ConvertTypeName(relshipExpr.TypeName, sr).EdmType;
			RelationshipType relationshipType = edmType as RelationshipType;
			if (relationshipType == null)
			{
				ErrorContext errCtx = relshipExpr.TypeName.ErrCtx;
				string text = Strings.RelationshipTypeExpected(edmType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(relshipExpr.RefExpr, sr);
			RefType sourceRefType = dbExpression.ResultType.EdmType as RefType;
			if (sourceRefType == null)
			{
				if (!(dbExpression.ResultType.EdmType is EntityType))
				{
					ErrorContext errCtx2 = relshipExpr.RefExpr.ErrCtx;
					string relatedEndExprTypeMustBeReference = Strings.RelatedEndExprTypeMustBeReference;
					throw EntitySqlException.Create(errCtx2, relatedEndExprTypeMustBeReference, null);
				}
				dbExpression = dbExpression.GetEntityRef();
				sourceRefType = (RefType)dbExpression.ResultType.EdmType;
			}
			RelationshipEndMember toEnd;
			if (relshipExpr.ToEndIdentifier != null)
			{
				toEnd = (RelationshipEndMember)relationshipType.Members.FirstOrDefault((EdmMember m) => m.Name.Equals(relshipExpr.ToEndIdentifier.Name, StringComparison.OrdinalIgnoreCase));
				if (toEnd == null)
				{
					ErrorContext errCtx3 = relshipExpr.ToEndIdentifier.ErrCtx;
					string text2 = Strings.InvalidRelationshipMember(relshipExpr.ToEndIdentifier.Name, relationshipType.FullName);
					throw EntitySqlException.Create(errCtx3, text2, null);
				}
			}
			else
			{
				toEnd = null;
			}
			RelationshipEndMember fromEnd;
			if (relshipExpr.FromEndIdentifier != null)
			{
				fromEnd = (RelationshipEndMember)relationshipType.Members.FirstOrDefault((EdmMember m) => m.Name.Equals(relshipExpr.FromEndIdentifier.Name, StringComparison.OrdinalIgnoreCase));
				if (fromEnd == null)
				{
					ErrorContext errCtx4 = relshipExpr.FromEndIdentifier.ErrCtx;
					string text3 = Strings.InvalidRelationshipMember(relshipExpr.FromEndIdentifier.Name, relationshipType.FullName);
					throw EntitySqlException.Create(errCtx4, text3, null);
				}
				if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(sourceRefType, fromEnd.TypeUsage.EdmType))
				{
					ErrorContext errCtx5 = relshipExpr.FromEndIdentifier.ErrCtx;
					string text4 = Strings.SourceTypeMustBePromotoableToFromEndRelationType(sourceRefType.FullName, fromEnd.TypeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx5, text4, null);
				}
				if (toEnd != null && fromEnd.EdmEquals(toEnd))
				{
					ErrorContext errCtx6 = relshipExpr.ErrCtx;
					string relationshipFromEndIsAmbiguos = Strings.RelationshipFromEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx6, relationshipFromEndIsAmbiguos, null);
				}
			}
			else
			{
				RelationshipEndMember[] array = (from m in relationshipType.Members
					select (RelationshipEndMember)m into e
					where TypeSemantics.IsStructurallyEqualOrPromotableTo(sourceRefType, e.TypeUsage.EdmType) && (toEnd == null || !e.EdmEquals(toEnd))
					select e).ToArray<RelationshipEndMember>();
				int num = array.Length;
				if (num == 0)
				{
					ErrorContext errCtx7 = relshipExpr.ErrCtx;
					string text5 = Strings.InvalidImplicitRelationshipFromEnd(relationshipType.FullName);
					throw EntitySqlException.Create(errCtx7, text5, null);
				}
				if (num != 1)
				{
					ErrorContext errCtx8 = relshipExpr.ErrCtx;
					string relationshipFromEndIsAmbiguos2 = Strings.RelationshipFromEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx8, relationshipFromEndIsAmbiguos2, null);
				}
				fromEnd = array[0];
			}
			if (toEnd == null)
			{
				RelationshipEndMember[] array2 = (from m in relationshipType.Members
					select (RelationshipEndMember)m into e
					where !e.EdmEquals(fromEnd)
					select e).ToArray<RelationshipEndMember>();
				int num = array2.Length;
				if (num == 0)
				{
					ErrorContext errCtx9 = relshipExpr.ErrCtx;
					string text6 = Strings.InvalidImplicitRelationshipToEnd(relationshipType.FullName);
					throw EntitySqlException.Create(errCtx9, text6, null);
				}
				if (num != 1)
				{
					ErrorContext errCtx10 = relshipExpr.ErrCtx;
					string relationshipToEndIsAmbiguos = Strings.RelationshipToEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx10, relationshipToEndIsAmbiguos, null);
				}
				toEnd = array2[0];
			}
			return new ValueExpression(dbExpression.Navigate(fromEnd, toEnd));
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x0011AB14 File Offset: 0x00118D14
		private static ExpressionResolution ConvertRefExpr(Node astExpr, SemanticResolver sr)
		{
			RefExpr refExpr = (RefExpr)astExpr;
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(refExpr.ArgExpr, sr);
			if (!TypeSemantics.IsEntityType(dbExpression.ResultType))
			{
				ErrorContext errCtx = refExpr.ArgExpr.ErrCtx;
				string text = Strings.RefArgIsNotOfEntityType(dbExpression.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			dbExpression = dbExpression.GetEntityRef();
			return new ValueExpression(dbExpression);
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x0011AB78 File Offset: 0x00118D78
		private static ExpressionResolution ConvertDeRefExpr(Node astExpr, SemanticResolver sr)
		{
			DerefExpr derefExpr = (DerefExpr)astExpr;
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(derefExpr.ArgExpr, sr);
			if (!TypeSemantics.IsReferenceType(dbExpression.ResultType))
			{
				ErrorContext errCtx = derefExpr.ArgExpr.ErrCtx;
				string text = Strings.DeRefArgIsNotOfRefType(dbExpression.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			dbExpression = dbExpression.Deref();
			return new ValueExpression(dbExpression);
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x0011ABE0 File Offset: 0x00118DE0
		private static ExpressionResolution ConvertCreateRefExpr(Node astExpr, SemanticResolver sr)
		{
			CreateRefExpr createRefExpr = (CreateRefExpr)astExpr;
			DbScanExpression dbScanExpression = SemanticAnalyzer.ConvertValueExpression(createRefExpr.EntitySet, sr) as DbScanExpression;
			if (dbScanExpression == null)
			{
				ErrorContext errCtx = createRefExpr.EntitySet.ErrCtx;
				string exprIsNotValidEntitySetForCreateRef = Strings.ExprIsNotValidEntitySetForCreateRef;
				throw EntitySqlException.Create(errCtx, exprIsNotValidEntitySetForCreateRef, null);
			}
			EntitySet entitySet = dbScanExpression.Target as EntitySet;
			if (entitySet == null)
			{
				ErrorContext errCtx2 = createRefExpr.EntitySet.ErrCtx;
				string exprIsNotValidEntitySetForCreateRef2 = Strings.ExprIsNotValidEntitySetForCreateRef;
				throw EntitySqlException.Create(errCtx2, exprIsNotValidEntitySetForCreateRef2, null);
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(createRefExpr.Keys, sr);
			RowType rowType = dbExpression.ResultType.EdmType as RowType;
			if (rowType == null)
			{
				ErrorContext errCtx3 = createRefExpr.Keys.ErrCtx;
				string invalidCreateRefKeyType = Strings.InvalidCreateRefKeyType;
				throw EntitySqlException.Create(errCtx3, invalidCreateRefKeyType, null);
			}
			RowType rowType2 = TypeHelpers.CreateKeyRowType(entitySet.ElementType);
			if (rowType2.Members.Count != rowType.Members.Count)
			{
				ErrorContext errCtx4 = createRefExpr.Keys.ErrCtx;
				string imcompatibleCreateRefKeyType = Strings.ImcompatibleCreateRefKeyType;
				throw EntitySqlException.Create(errCtx4, imcompatibleCreateRefKeyType, null);
			}
			if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(dbExpression.ResultType, TypeUsage.Create(rowType2)))
			{
				ErrorContext errCtx5 = createRefExpr.Keys.ErrCtx;
				string imcompatibleCreateRefKeyElementType = Strings.ImcompatibleCreateRefKeyElementType;
				throw EntitySqlException.Create(errCtx5, imcompatibleCreateRefKeyElementType, null);
			}
			DbExpression dbExpression2;
			if (createRefExpr.TypeIdentifier != null)
			{
				TypeUsage typeUsage = SemanticAnalyzer.ConvertTypeName(createRefExpr.TypeIdentifier, sr);
				if (!TypeSemantics.IsEntityType(typeUsage))
				{
					ErrorContext errCtx6 = createRefExpr.TypeIdentifier.ErrCtx;
					string text = Strings.CreateRefTypeIdentifierMustSpecifyAnEntityType(typeUsage.EdmType.FullName, typeUsage.EdmType.BuiltInTypeKind.ToString());
					throw EntitySqlException.Create(errCtx6, text, null);
				}
				if (!TypeSemantics.IsValidPolymorphicCast(entitySet.ElementType, typeUsage.EdmType))
				{
					ErrorContext errCtx7 = createRefExpr.TypeIdentifier.ErrCtx;
					string text2 = Strings.CreateRefTypeIdentifierMustBeASubOrSuperType(entitySet.ElementType.FullName, typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx7, text2, null);
				}
				dbExpression2 = entitySet.RefFromKey(dbExpression, (EntityType)typeUsage.EdmType);
			}
			else
			{
				dbExpression2 = entitySet.RefFromKey(dbExpression);
			}
			return new ValueExpression(dbExpression2);
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x0011ADCC File Offset: 0x00118FCC
		private static ExpressionResolution ConvertKeyExpr(Node astExpr, SemanticResolver sr)
		{
			KeyExpr keyExpr = (KeyExpr)astExpr;
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(keyExpr.ArgExpr, sr);
			if (TypeSemantics.IsEntityType(dbExpression.ResultType))
			{
				dbExpression = dbExpression.GetEntityRef();
			}
			else if (!TypeSemantics.IsReferenceType(dbExpression.ResultType))
			{
				ErrorContext errCtx = keyExpr.ArgExpr.ErrCtx;
				string text = Strings.InvalidKeyArgument(dbExpression.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			dbExpression = dbExpression.GetRefKey();
			return new ValueExpression(dbExpression);
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x0011AE48 File Offset: 0x00119048
		private static ExpressionResolution ConvertBuiltIn(Node astExpr, SemanticResolver sr)
		{
			BuiltInExpr builtInExpr = (BuiltInExpr)astExpr;
			SemanticAnalyzer.BuiltInExprConverter builtInExprConverter = SemanticAnalyzer._builtInExprConverter[builtInExpr.Kind];
			if (builtInExprConverter == null)
			{
				throw new EntitySqlException(Strings.UnknownBuiltInAstExpressionType);
			}
			return new ValueExpression(builtInExprConverter(builtInExpr, sr));
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x0011AE88 File Offset: 0x00119088
		private static Pair<DbExpression, DbExpression> ConvertArithmeticArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullArithmetic, sr);
			if (!TypeSemantics.IsNumericType(pair.Left.ResultType))
			{
				ErrorContext errCtx = astBuiltInExpr.Arg1.ErrCtx;
				string expressionMustBeNumericType = Strings.ExpressionMustBeNumericType;
				throw EntitySqlException.Create(errCtx, expressionMustBeNumericType, null);
			}
			if (pair.Right != null)
			{
				if (!TypeSemantics.IsNumericType(pair.Right.ResultType))
				{
					ErrorContext errCtx2 = astBuiltInExpr.Arg2.ErrCtx;
					string expressionMustBeNumericType2 = Strings.ExpressionMustBeNumericType;
					throw EntitySqlException.Create(errCtx2, expressionMustBeNumericType2, null);
				}
				if (TypeHelpers.GetCommonTypeUsage(pair.Left.ResultType, pair.Right.ResultType) == null)
				{
					ErrorContext errCtx3 = astBuiltInExpr.ErrCtx;
					string text = Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
					throw EntitySqlException.Create(errCtx3, text, null);
				}
			}
			return pair;
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x0011AF8C File Offset: 0x0011918C
		private static Pair<DbExpression, DbExpression> ConvertPlusOperands(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullArithmetic, sr);
			if (!TypeSemantics.IsNumericType(pair.Left.ResultType) && !TypeSemantics.IsPrimitiveType(pair.Left.ResultType, PrimitiveTypeKind.String))
			{
				ErrorContext errCtx = astBuiltInExpr.Arg1.ErrCtx;
				string plusLeftExpressionInvalidType = Strings.PlusLeftExpressionInvalidType;
				throw EntitySqlException.Create(errCtx, plusLeftExpressionInvalidType, null);
			}
			if (!TypeSemantics.IsNumericType(pair.Right.ResultType) && !TypeSemantics.IsPrimitiveType(pair.Right.ResultType, PrimitiveTypeKind.String))
			{
				ErrorContext errCtx2 = astBuiltInExpr.Arg2.ErrCtx;
				string plusRightExpressionInvalidType = Strings.PlusRightExpressionInvalidType;
				throw EntitySqlException.Create(errCtx2, plusRightExpressionInvalidType, null);
			}
			if (TypeHelpers.GetCommonTypeUsage(pair.Left.ResultType, pair.Right.ResultType) == null)
			{
				ErrorContext errCtx3 = astBuiltInExpr.ErrCtx;
				string text = Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx3, text, null);
			}
			return pair;
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x0011B0AC File Offset: 0x001192AC
		private static Pair<DbExpression, DbExpression> ConvertLogicalArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astBuiltInExpr.Arg1, sr);
			if (dbExpression == null)
			{
				dbExpression = TypeResolver.BooleanType.Null();
			}
			DbExpression dbExpression2 = null;
			if (astBuiltInExpr.Arg2 != null)
			{
				dbExpression2 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astBuiltInExpr.Arg2, sr);
				if (dbExpression2 == null)
				{
					dbExpression2 = TypeResolver.BooleanType.Null();
				}
			}
			if (!SemanticAnalyzer.IsBooleanType(dbExpression.ResultType))
			{
				ErrorContext errCtx = astBuiltInExpr.Arg1.ErrCtx;
				string expressionTypeMustBeBoolean = Strings.ExpressionTypeMustBeBoolean;
				throw EntitySqlException.Create(errCtx, expressionTypeMustBeBoolean, null);
			}
			if (dbExpression2 != null && !SemanticAnalyzer.IsBooleanType(dbExpression2.ResultType))
			{
				ErrorContext errCtx2 = astBuiltInExpr.Arg2.ErrCtx;
				string expressionTypeMustBeBoolean2 = Strings.ExpressionTypeMustBeBoolean;
				throw EntitySqlException.Create(errCtx2, expressionTypeMustBeBoolean2, null);
			}
			return new Pair<DbExpression, DbExpression>(dbExpression, dbExpression2);
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x0011B150 File Offset: 0x00119350
		private static Pair<DbExpression, DbExpression> ConvertEqualCompArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullComparison, sr);
			if (!TypeSemantics.IsEqualComparableTo(pair.Left.ResultType, pair.Right.ResultType))
			{
				ErrorContext errCtx = astBuiltInExpr.ErrCtx;
				string text = Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			return pair;
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x0011B1F4 File Offset: 0x001193F4
		private static Pair<DbExpression, DbExpression> ConvertOrderCompArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullComparison, sr);
			if (!TypeSemantics.IsOrderComparableTo(pair.Left.ResultType, pair.Right.ResultType))
			{
				ErrorContext errCtx = astBuiltInExpr.ErrCtx;
				string text = Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			return pair;
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x0011B298 File Offset: 0x00119498
		private static Pair<DbExpression, DbExpression> ConvertSetArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(astBuiltInExpr.Arg1, sr);
			DbExpression dbExpression2 = null;
			if (astBuiltInExpr.Arg2 != null)
			{
				if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
				{
					ErrorContext errCtx = astBuiltInExpr.Arg1.ErrCtx;
					string leftSetExpressionArgsMustBeCollection = Strings.LeftSetExpressionArgsMustBeCollection;
					throw EntitySqlException.Create(errCtx, leftSetExpressionArgsMustBeCollection, null);
				}
				dbExpression2 = SemanticAnalyzer.ConvertValueExpression(astBuiltInExpr.Arg2, sr);
				if (!TypeSemantics.IsCollectionType(dbExpression2.ResultType))
				{
					ErrorContext errCtx2 = astBuiltInExpr.Arg2.ErrCtx;
					string rightSetExpressionArgsMustBeCollection = Strings.RightSetExpressionArgsMustBeCollection;
					throw EntitySqlException.Create(errCtx2, rightSetExpressionArgsMustBeCollection, null);
				}
				TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(dbExpression.ResultType);
				TypeUsage elementTypeUsage2 = TypeHelpers.GetElementTypeUsage(dbExpression2.ResultType);
				TypeUsage typeUsage;
				if (!TypeSemantics.TryGetCommonType(elementTypeUsage, elementTypeUsage2, out typeUsage))
				{
					CqlErrorHelper.ReportIncompatibleCommonType(astBuiltInExpr.ErrCtx, elementTypeUsage, elementTypeUsage2);
				}
				if (astBuiltInExpr.Kind != BuiltInKind.UnionAll)
				{
					if (!TypeHelpers.IsSetComparableOpType(TypeHelpers.GetElementTypeUsage(dbExpression.ResultType)))
					{
						ErrorContext errCtx3 = astBuiltInExpr.Arg1.ErrCtx;
						string text = Strings.PlaceholderSetArgTypeIsNotEqualComparable(Strings.LocalizedLeft, astBuiltInExpr.Kind.ToString().ToUpperInvariant(), TypeHelpers.GetElementTypeUsage(dbExpression.ResultType).EdmType.FullName);
						throw EntitySqlException.Create(errCtx3, text, null);
					}
					if (!TypeHelpers.IsSetComparableOpType(TypeHelpers.GetElementTypeUsage(dbExpression2.ResultType)))
					{
						ErrorContext errCtx4 = astBuiltInExpr.Arg2.ErrCtx;
						string text2 = Strings.PlaceholderSetArgTypeIsNotEqualComparable(Strings.LocalizedRight, astBuiltInExpr.Kind.ToString().ToUpperInvariant(), TypeHelpers.GetElementTypeUsage(dbExpression2.ResultType).EdmType.FullName);
						throw EntitySqlException.Create(errCtx4, text2, null);
					}
				}
				else
				{
					if (Helper.IsAssociationType(elementTypeUsage.EdmType))
					{
						ErrorContext errCtx5 = astBuiltInExpr.Arg1.ErrCtx;
						string text3 = Strings.InvalidAssociationTypeForUnion(elementTypeUsage.EdmType.FullName);
						throw EntitySqlException.Create(errCtx5, text3, null);
					}
					if (Helper.IsAssociationType(elementTypeUsage2.EdmType))
					{
						ErrorContext errCtx6 = astBuiltInExpr.Arg2.ErrCtx;
						string text4 = Strings.InvalidAssociationTypeForUnion(elementTypeUsage2.EdmType.FullName);
						throw EntitySqlException.Create(errCtx6, text4, null);
					}
				}
			}
			else
			{
				if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
				{
					ErrorContext errCtx7 = astBuiltInExpr.Arg1.ErrCtx;
					string text5 = Strings.InvalidUnarySetOpArgument(astBuiltInExpr.Name);
					throw EntitySqlException.Create(errCtx7, text5, null);
				}
				if (astBuiltInExpr.Kind == BuiltInKind.Distinct && !TypeHelpers.IsValidDistinctOpType(TypeHelpers.GetElementTypeUsage(dbExpression.ResultType)))
				{
					ErrorContext errCtx8 = astBuiltInExpr.Arg1.ErrCtx;
					string expressionTypeMustBeEqualComparable = Strings.ExpressionTypeMustBeEqualComparable;
					throw EntitySqlException.Create(errCtx8, expressionTypeMustBeEqualComparable, null);
				}
			}
			return new Pair<DbExpression, DbExpression>(dbExpression, dbExpression2);
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x0011B4F0 File Offset: 0x001196F0
		private static Pair<DbExpression, DbExpression> ConvertInExprArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(astBuiltInExpr.Arg2, sr);
			if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
			{
				ErrorContext errCtx = astBuiltInExpr.Arg2.ErrCtx;
				string rightSetExpressionArgsMustBeCollection = Strings.RightSetExpressionArgsMustBeCollection;
				throw EntitySqlException.Create(errCtx, rightSetExpressionArgsMustBeCollection, null);
			}
			DbExpression dbExpression2 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astBuiltInExpr.Arg1, sr);
			if (dbExpression2 == null)
			{
				TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(dbExpression.ResultType);
				SemanticAnalyzer.ValidateTypeForNullExpression(elementTypeUsage, astBuiltInExpr.Arg1.ErrCtx);
				dbExpression2 = elementTypeUsage.Null();
			}
			if (TypeSemantics.IsCollectionType(dbExpression2.ResultType))
			{
				ErrorContext errCtx2 = astBuiltInExpr.Arg1.ErrCtx;
				string expressionTypeMustNotBeCollection = Strings.ExpressionTypeMustNotBeCollection;
				throw EntitySqlException.Create(errCtx2, expressionTypeMustNotBeCollection, null);
			}
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(dbExpression2.ResultType, TypeHelpers.GetElementTypeUsage(dbExpression.ResultType));
			if (commonTypeUsage == null || !TypeHelpers.IsValidInOpType(commonTypeUsage))
			{
				ErrorContext errCtx3 = astBuiltInExpr.ErrCtx;
				string text = Strings.InvalidInExprArgs(dbExpression2.ResultType.EdmType.FullName, dbExpression.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx3, text, null);
			}
			return new Pair<DbExpression, DbExpression>(dbExpression2, dbExpression);
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x0011B5EC File Offset: 0x001197EC
		private static void ValidateTypeForNullExpression(TypeUsage type, ErrorContext errCtx)
		{
			if (TypeSemantics.IsCollectionType(type))
			{
				string nullLiteralCannotBePromotedToCollectionOfNulls = Strings.NullLiteralCannotBePromotedToCollectionOfNulls;
				throw EntitySqlException.Create(errCtx, nullLiteralCannotBePromotedToCollectionOfNulls, null);
			}
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x0011B610 File Offset: 0x00119810
		private static TypeUsage ConvertTypeName(Node typeName, SemanticResolver sr)
		{
			string[] array = null;
			NodeList<Node> nodeList = null;
			MethodExpr methodExpr = typeName as MethodExpr;
			if (methodExpr != null)
			{
				typeName = methodExpr.Expr;
				typeName.ErrCtx.ErrorContextInfo = methodExpr.ErrCtx.ErrorContextInfo;
				typeName.ErrCtx.UseContextInfoAsResourceIdentifier = methodExpr.ErrCtx.UseContextInfoAsResourceIdentifier;
				nodeList = methodExpr.Args;
			}
			Identifier identifier = typeName as Identifier;
			if (identifier != null)
			{
				array = new string[] { identifier.Name };
			}
			DotExpr dotExpr = typeName as DotExpr;
			if (dotExpr != null)
			{
				dotExpr.IsMultipartIdentifier(out array);
			}
			if (array == null)
			{
				ErrorContext errCtx = typeName.ErrCtx;
				string invalidMetadataMemberName = Strings.InvalidMetadataMemberName;
				throw EntitySqlException.Create(errCtx, invalidMetadataMemberName, null);
			}
			MetadataMember metadataMember = sr.ResolveMetadataMemberName(array, typeName.ErrCtx);
			MetadataMemberClass metadataMemberClass = metadataMember.MetadataMemberClass;
			if (metadataMemberClass == MetadataMemberClass.Type)
			{
				TypeUsage typeUsage = ((MetadataType)metadataMember).TypeUsage;
				if (nodeList != null)
				{
					typeUsage = SemanticAnalyzer.ConvertTypeSpecArgs(typeUsage, nodeList, typeName.ErrCtx);
				}
				return typeUsage;
			}
			if (metadataMemberClass != MetadataMemberClass.Namespace)
			{
				ErrorContext errCtx2 = typeName.ErrCtx;
				string text = Strings.InvalidMetadataMemberClassResolution(metadataMember.Name, metadataMember.MetadataMemberClassName, MetadataType.TypeClassName);
				throw EntitySqlException.Create(errCtx2, text, null);
			}
			ErrorContext errCtx3 = typeName.ErrCtx;
			string text2 = Strings.TypeNameNotFound(metadataMember.Name);
			throw EntitySqlException.Create(errCtx3, text2, null);
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x0011B73C File Offset: 0x0011993C
		private static TypeUsage ConvertTypeSpecArgs(TypeUsage parameterizedType, NodeList<Node> typeSpecArgs, ErrorContext errCtx)
		{
			foreach (Node node in ((IEnumerable<Node>)typeSpecArgs))
			{
				if (!(node is Literal))
				{
					ErrorContext errCtx2 = node.ErrCtx;
					string typeArgumentMustBeLiteral = Strings.TypeArgumentMustBeLiteral;
					throw EntitySqlException.Create(errCtx2, typeArgumentMustBeLiteral, null);
				}
			}
			PrimitiveType primitiveType = parameterizedType.EdmType as PrimitiveType;
			if (primitiveType == null || primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Decimal)
			{
				string text = Strings.TypeDoesNotSupportSpec(primitiveType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			if (typeSpecArgs.Count > 2)
			{
				string text2 = Strings.TypeArgumentCountMismatch(primitiveType.FullName, 2);
				throw EntitySqlException.Create(errCtx, text2, null);
			}
			byte b;
			SemanticAnalyzer.ConvertTypeFacetValue(primitiveType, (Literal)typeSpecArgs[0], "Precision", out b);
			byte b2 = 0;
			if (typeSpecArgs.Count == 2)
			{
				SemanticAnalyzer.ConvertTypeFacetValue(primitiveType, (Literal)typeSpecArgs[1], "Scale", out b2);
			}
			if (b < b2)
			{
				ErrorContext errCtx3 = typeSpecArgs[0].ErrCtx;
				string text3 = Strings.PrecisionMustBeGreaterThanScale(b, b2);
				throw EntitySqlException.Create(errCtx3, text3, null);
			}
			return TypeUsage.CreateDecimalTypeUsage(primitiveType, b, b2);
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x0011B864 File Offset: 0x00119A64
		private static void ConvertTypeFacetValue(PrimitiveType type, Literal value, string facetName, out byte byteValue)
		{
			FacetDescription facet = Helper.GetFacet(type.ProviderManifest.GetFacetDescriptions(type), facetName);
			if (facet == null)
			{
				ErrorContext errCtx = value.ErrCtx;
				string text = Strings.TypeDoesNotSupportFacet(type.FullName, facetName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			if (!value.IsNumber || !byte.TryParse(value.OriginalValue, out byteValue))
			{
				ErrorContext errCtx2 = value.ErrCtx;
				string typeArgumentIsNotValid = Strings.TypeArgumentIsNotValid;
				throw EntitySqlException.Create(errCtx2, typeArgumentIsNotValid, null);
			}
			if (facet.MaxValue != null && (int)byteValue > facet.MaxValue.Value)
			{
				ErrorContext errCtx3 = value.ErrCtx;
				string text2 = Strings.TypeArgumentExceedsMax(facetName);
				throw EntitySqlException.Create(errCtx3, text2, null);
			}
			if (facet.MinValue != null && (int)byteValue < facet.MinValue.Value)
			{
				ErrorContext errCtx4 = value.ErrCtx;
				string text3 = Strings.TypeArgumentBelowMin(facetName);
				throw EntitySqlException.Create(errCtx4, text3, null);
			}
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x0011B940 File Offset: 0x00119B40
		private static TypeUsage ConvertTypeDefinition(Node typeDefinitionExpr, SemanticResolver sr)
		{
			CollectionTypeDefinition collectionTypeDefinition = typeDefinitionExpr as CollectionTypeDefinition;
			RefTypeDefinition refTypeDefinition = typeDefinitionExpr as RefTypeDefinition;
			RowTypeDefinition rowTypeDefinition = typeDefinitionExpr as RowTypeDefinition;
			TypeUsage typeUsage;
			if (collectionTypeDefinition != null)
			{
				typeUsage = TypeHelpers.CreateCollectionTypeUsage(SemanticAnalyzer.ConvertTypeDefinition(collectionTypeDefinition.ElementTypeDef, sr));
			}
			else if (refTypeDefinition != null)
			{
				TypeUsage typeUsage2 = SemanticAnalyzer.ConvertTypeName(refTypeDefinition.RefTypeIdentifier, sr);
				if (!TypeSemantics.IsEntityType(typeUsage2))
				{
					ErrorContext errCtx = refTypeDefinition.RefTypeIdentifier.ErrCtx;
					string text = Strings.RefTypeIdentifierMustSpecifyAnEntityType(typeUsage2.EdmType.FullName, typeUsage2.EdmType.BuiltInTypeKind.ToString());
					throw EntitySqlException.Create(errCtx, text, null);
				}
				typeUsage = TypeHelpers.CreateReferenceTypeUsage((EntityType)typeUsage2.EdmType);
			}
			else if (rowTypeDefinition != null)
			{
				typeUsage = TypeHelpers.CreateRowTypeUsage(rowTypeDefinition.Properties.Select((PropDefinition p) => new KeyValuePair<string, TypeUsage>(p.Name.Name, SemanticAnalyzer.ConvertTypeDefinition(p.Type, sr))));
			}
			else
			{
				typeUsage = SemanticAnalyzer.ConvertTypeName(typeDefinitionExpr, sr);
			}
			return typeUsage;
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x0011BA38 File Offset: 0x00119C38
		private static ExpressionResolution ConvertRowConstructor(Node expr, SemanticResolver sr)
		{
			RowConstructorExpr rowConstructorExpr = (RowConstructorExpr)expr;
			Dictionary<string, TypeUsage> dictionary = new Dictionary<string, TypeUsage>(sr.NameComparer);
			List<DbExpression> list = new List<DbExpression>(rowConstructorExpr.AliasedExprList.Count);
			for (int i = 0; i < rowConstructorExpr.AliasedExprList.Count; i++)
			{
				AliasedExpr aliasedExpr = rowConstructorExpr.AliasedExprList[i];
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(aliasedExpr.Expr, sr);
				if (dbExpression == null)
				{
					ErrorContext errCtx = aliasedExpr.Expr.ErrCtx;
					string rowCtorElementCannotBeNull = Strings.RowCtorElementCannotBeNull;
					throw EntitySqlException.Create(errCtx, rowCtorElementCannotBeNull, null);
				}
				string text = sr.InferAliasName(aliasedExpr, dbExpression);
				if (dictionary.ContainsKey(text))
				{
					if (aliasedExpr.Alias != null)
					{
						CqlErrorHelper.ReportAliasAlreadyUsedError(text, aliasedExpr.Alias.ErrCtx, Strings.InRowCtor);
					}
					else
					{
						text = sr.GenerateInternalName("autoRowCol");
					}
				}
				dictionary.Add(text, dbExpression.ResultType);
				list.Add(dbExpression);
			}
			return new ValueExpression(TypeHelpers.CreateRowTypeUsage(dictionary).New(list));
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x0011BB30 File Offset: 0x00119D30
		private static ExpressionResolution ConvertMultisetConstructor(Node expr, SemanticResolver sr)
		{
			MultisetConstructorExpr multisetConstructorExpr = (MultisetConstructorExpr)expr;
			if (multisetConstructorExpr.ExprList == null)
			{
				ErrorContext errCtx = expr.ErrCtx;
				string cannotCreateEmptyMultiset = Strings.CannotCreateEmptyMultiset;
				throw EntitySqlException.Create(errCtx, cannotCreateEmptyMultiset, null);
			}
			DbExpression[] array = multisetConstructorExpr.ExprList.Select((Node e) => SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(e, sr)).ToArray<DbExpression>();
			TypeUsage[] array2 = (from e in array
				where e != null
				select e.ResultType).ToArray<TypeUsage>();
			if (array2.Length == 0)
			{
				ErrorContext errCtx2 = expr.ErrCtx;
				string cannotCreateMultisetofNulls = Strings.CannotCreateMultisetofNulls;
				throw EntitySqlException.Create(errCtx2, cannotCreateMultisetofNulls, null);
			}
			TypeUsage typeUsage = TypeHelpers.GetCommonTypeUsage(array2);
			if (typeUsage == null)
			{
				ErrorContext errCtx3 = expr.ErrCtx;
				string multisetElemsAreNotTypeCompatible = Strings.MultisetElemsAreNotTypeCompatible;
				throw EntitySqlException.Create(errCtx3, multisetElemsAreNotTypeCompatible, null);
			}
			typeUsage = TypeHelpers.GetReadOnlyType(typeUsage);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					SemanticAnalyzer.ValidateTypeForNullExpression(typeUsage, multisetConstructorExpr.ExprList[i].ErrCtx);
					array[i] = typeUsage.Null();
				}
			}
			return new ValueExpression(TypeHelpers.CreateCollectionTypeUsage(typeUsage).New(array));
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x0011BC68 File Offset: 0x00119E68
		private static ExpressionResolution ConvertCaseExpr(Node expr, SemanticResolver sr)
		{
			CaseExpr caseExpr = (CaseExpr)expr;
			List<DbExpression> list = new List<DbExpression>(caseExpr.WhenThenExprList.Count);
			List<DbExpression> list2 = new List<DbExpression>(caseExpr.WhenThenExprList.Count);
			for (int i = 0; i < caseExpr.WhenThenExprList.Count; i++)
			{
				WhenThenExpr whenThenExpr = caseExpr.WhenThenExprList[i];
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(whenThenExpr.WhenExpr, sr);
				if (!SemanticAnalyzer.IsBooleanType(dbExpression.ResultType))
				{
					ErrorContext errCtx = whenThenExpr.WhenExpr.ErrCtx;
					string expressionTypeMustBeBoolean = Strings.ExpressionTypeMustBeBoolean;
					throw EntitySqlException.Create(errCtx, expressionTypeMustBeBoolean, null);
				}
				list.Add(dbExpression);
				DbExpression dbExpression2 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(whenThenExpr.ThenExpr, sr);
				list2.Add(dbExpression2);
			}
			DbExpression dbExpression3 = ((caseExpr.ElseExpr != null) ? SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(caseExpr.ElseExpr, sr) : null);
			List<TypeUsage> list3 = (from e in list2
				where e != null
				select e.ResultType).ToList<TypeUsage>();
			if (dbExpression3 != null)
			{
				list3.Add(dbExpression3.ResultType);
			}
			if (list3.Count == 0)
			{
				ErrorContext errCtx2 = caseExpr.ElseExpr.ErrCtx;
				string invalidCaseWhenThenNullType = Strings.InvalidCaseWhenThenNullType;
				throw EntitySqlException.Create(errCtx2, invalidCaseWhenThenNullType, null);
			}
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(list3);
			if (commonTypeUsage == null)
			{
				ErrorContext errCtx3 = caseExpr.WhenThenExprList[0].ThenExpr.ErrCtx;
				string invalidCaseResultTypes = Strings.InvalidCaseResultTypes;
				throw EntitySqlException.Create(errCtx3, invalidCaseResultTypes, null);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				if (list2[j] == null)
				{
					SemanticAnalyzer.ValidateTypeForNullExpression(commonTypeUsage, caseExpr.WhenThenExprList[j].ThenExpr.ErrCtx);
					list2[j] = commonTypeUsage.Null();
				}
			}
			if (dbExpression3 == null)
			{
				if (caseExpr.ElseExpr == null && TypeSemantics.IsCollectionType(commonTypeUsage))
				{
					dbExpression3 = commonTypeUsage.NewEmptyCollection();
				}
				else
				{
					SemanticAnalyzer.ValidateTypeForNullExpression(commonTypeUsage, (caseExpr.ElseExpr ?? caseExpr).ErrCtx);
					dbExpression3 = commonTypeUsage.Null();
				}
			}
			return new ValueExpression(DbExpressionBuilder.Case(list, list2, dbExpression3));
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x0011BE84 File Offset: 0x0011A084
		private static ExpressionResolution ConvertQueryExpr(Node expr, SemanticResolver sr)
		{
			QueryExpr queryExpr = (QueryExpr)expr;
			DbExpression dbExpression = null;
			bool flag = ParserOptions.CompilationMode.RestrictedViewGenerationMode == sr.ParserOptions.ParserCompilationMode;
			if (queryExpr.HavingClause != null && queryExpr.GroupByClause == null)
			{
				ErrorContext errCtx = queryExpr.ErrCtx;
				string havingRequiresGroupClause = Strings.HavingRequiresGroupClause;
				throw EntitySqlException.Create(errCtx, havingRequiresGroupClause, null);
			}
			if (queryExpr.SelectClause.TopExpr != null)
			{
				if (queryExpr.OrderByClause != null && queryExpr.OrderByClause.LimitSubClause != null)
				{
					ErrorContext errCtx2 = queryExpr.SelectClause.TopExpr.ErrCtx;
					string topAndLimitCannotCoexist = Strings.TopAndLimitCannotCoexist;
					throw EntitySqlException.Create(errCtx2, topAndLimitCannotCoexist, null);
				}
				if (queryExpr.OrderByClause != null && queryExpr.OrderByClause.SkipSubClause != null)
				{
					ErrorContext errCtx3 = queryExpr.SelectClause.TopExpr.ErrCtx;
					string topAndSkipCannotCoexist = Strings.TopAndSkipCannotCoexist;
					throw EntitySqlException.Create(errCtx3, topAndSkipCannotCoexist, null);
				}
			}
			using (sr.EnterScopeRegion())
			{
				DbExpressionBinding dbExpressionBinding = SemanticAnalyzer.ProcessFromClause(queryExpr.FromClause, sr);
				dbExpressionBinding = SemanticAnalyzer.ProcessWhereClause(dbExpressionBinding, queryExpr.WhereClause, sr);
				bool flag2 = false;
				if (!flag)
				{
					dbExpressionBinding = SemanticAnalyzer.ProcessGroupByClause(dbExpressionBinding, queryExpr, sr);
					dbExpressionBinding = SemanticAnalyzer.ProcessHavingClause(dbExpressionBinding, queryExpr.HavingClause, sr);
					dbExpressionBinding = SemanticAnalyzer.ProcessOrderByClause(dbExpressionBinding, queryExpr, out flag2, sr);
				}
				dbExpression = SemanticAnalyzer.ProcessSelectClause(dbExpressionBinding, queryExpr, flag2, sr);
			}
			return new ValueExpression(dbExpression);
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x0011BFC8 File Offset: 0x0011A1C8
		private static DbExpression ProcessSelectClause(DbExpressionBinding source, QueryExpr queryExpr, bool queryProjectionProcessed, SemanticResolver sr)
		{
			SelectClause selectClause = queryExpr.SelectClause;
			DbExpression dbExpression;
			if (queryProjectionProcessed)
			{
				dbExpression = source.Expression;
			}
			else
			{
				List<KeyValuePair<string, DbExpression>> list = SemanticAnalyzer.ConvertSelectClauseItems(queryExpr, sr);
				dbExpression = SemanticAnalyzer.CreateProjectExpression(source, selectClause, list);
			}
			if (selectClause.TopExpr != null || (queryExpr.OrderByClause != null && queryExpr.OrderByClause.LimitSubClause != null))
			{
				Node node;
				string text;
				if (selectClause.TopExpr != null)
				{
					node = selectClause.TopExpr;
					text = "TOP";
				}
				else
				{
					node = queryExpr.OrderByClause.LimitSubClause;
					text = "LIMIT";
				}
				DbExpression dbExpression2 = SemanticAnalyzer.ConvertValueExpression(node, sr);
				SemanticAnalyzer.ValidateExpressionIsCommandParamOrNonNegativeIntegerConstant(dbExpression2, node.ErrCtx, text);
				dbExpression = dbExpression.Limit(dbExpression2);
			}
			return dbExpression;
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x0011C064 File Offset: 0x0011A264
		private static List<KeyValuePair<string, DbExpression>> ConvertSelectClauseItems(QueryExpr queryExpr, SemanticResolver sr)
		{
			SelectClause selectClause = queryExpr.SelectClause;
			if (selectClause.SelectKind == SelectKind.Value)
			{
				if (selectClause.Items.Count != 1)
				{
					ErrorContext errCtx = selectClause.ErrCtx;
					string invalidSelectValueList = Strings.InvalidSelectValueList;
					throw EntitySqlException.Create(errCtx, invalidSelectValueList, null);
				}
				if (selectClause.Items[0].Alias != null && queryExpr.OrderByClause == null)
				{
					ErrorContext errCtx2 = selectClause.Items[0].ErrCtx;
					string invalidSelectValueAliasedExpression = Strings.InvalidSelectValueAliasedExpression;
					throw EntitySqlException.Create(errCtx2, invalidSelectValueAliasedExpression, null);
				}
			}
			HashSet<string> hashSet = new HashSet<string>(sr.NameComparer);
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>(selectClause.Items.Count);
			for (int i = 0; i < selectClause.Items.Count; i++)
			{
				AliasedExpr aliasedExpr = selectClause.Items[i];
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(aliasedExpr.Expr, sr);
				string text = sr.InferAliasName(aliasedExpr, dbExpression);
				if (hashSet.Contains(text))
				{
					if (aliasedExpr.Alias != null)
					{
						CqlErrorHelper.ReportAliasAlreadyUsedError(text, aliasedExpr.Alias.ErrCtx, Strings.InSelectProjectionList);
					}
					else
					{
						text = sr.GenerateInternalName("autoProject");
					}
				}
				hashSet.Add(text);
				list.Add(new KeyValuePair<string, DbExpression>(text, dbExpression));
			}
			return list;
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x0011C198 File Offset: 0x0011A398
		private static DbExpression CreateProjectExpression(DbExpressionBinding source, SelectClause selectClause, List<KeyValuePair<string, DbExpression>> projectionItems)
		{
			DbExpression dbExpression;
			if (selectClause.SelectKind == SelectKind.Value)
			{
				dbExpression = source.Project(projectionItems[0].Value);
			}
			else
			{
				dbExpression = source.Project(DbExpressionBuilder.NewRow(projectionItems));
			}
			if (selectClause.DistinctKind == DistinctKind.Distinct)
			{
				SemanticAnalyzer.ValidateDistinctProjection(dbExpression.ResultType, selectClause);
				dbExpression = dbExpression.Distinct();
			}
			return dbExpression;
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x0011C1F0 File Offset: 0x0011A3F0
		private static void ValidateDistinctProjection(TypeUsage projectExpressionResultType, SelectClause selectClause)
		{
			ErrorContext errCtx = selectClause.Items[0].Expr.ErrCtx;
			List<ErrorContext> list;
			if (selectClause.SelectKind != SelectKind.Row)
			{
				list = null;
			}
			else
			{
				list = new List<ErrorContext>(selectClause.Items.Select((AliasedExpr item) => item.Expr.ErrCtx));
			}
			SemanticAnalyzer.ValidateDistinctProjection(projectExpressionResultType, errCtx, list);
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x0011C254 File Offset: 0x0011A454
		private static void ValidateDistinctProjection(TypeUsage projectExpressionResultType, ErrorContext defaultErrCtx, List<ErrorContext> projectionItemErrCtxs)
		{
			TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(projectExpressionResultType);
			if (!TypeHelpers.IsValidDistinctOpType(elementTypeUsage))
			{
				ErrorContext errorContext = defaultErrCtx;
				if (projectionItemErrCtxs != null && TypeSemantics.IsRowType(elementTypeUsage))
				{
					RowType rowType = elementTypeUsage.EdmType as RowType;
					for (int i = 0; i < rowType.Members.Count; i++)
					{
						if (!TypeHelpers.IsValidDistinctOpType(rowType.Members[i].TypeUsage))
						{
							errorContext = projectionItemErrCtxs[i];
							break;
						}
					}
				}
				string selectDistinctMustBeEqualComparable = Strings.SelectDistinctMustBeEqualComparable;
				throw EntitySqlException.Create(errorContext, selectDistinctMustBeEqualComparable, null);
			}
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x0011C2D8 File Offset: 0x0011A4D8
		private static void ValidateExpressionIsCommandParamOrNonNegativeIntegerConstant(DbExpression expr, ErrorContext errCtx, string exprName)
		{
			if (expr.ExpressionKind != DbExpressionKind.Constant && expr.ExpressionKind != DbExpressionKind.ParameterReference)
			{
				string text = Strings.PlaceholderExpressionMustBeConstant(exprName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			if (!TypeSemantics.IsPromotableTo(expr.ResultType, TypeResolver.Int64Type))
			{
				string text2 = Strings.PlaceholderExpressionMustBeCompatibleWithEdm64(exprName, expr.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, text2, null);
			}
			DbConstantExpression dbConstantExpression = expr as DbConstantExpression;
			if (dbConstantExpression != null && global::System.Convert.ToInt64(dbConstantExpression.Value, CultureInfo.InvariantCulture) < 0L)
			{
				string text3 = Strings.PlaceholderExpressionMustBeGreaterThanOrEqualToZero(exprName);
				throw EntitySqlException.Create(errCtx, text3, null);
			}
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x0011C368 File Offset: 0x0011A568
		private static DbExpressionBinding ProcessFromClause(FromClause fromClause, SemanticResolver sr)
		{
			DbExpressionBinding fromBinding = null;
			List<SourceScopeEntry> list = new List<SourceScopeEntry>();
			Func<SourceScopeEntry, SourceScopeEntry> <>9__0;
			for (int i = 0; i < fromClause.FromClauseItems.Count; i++)
			{
				List<SourceScopeEntry> list2;
				DbExpressionBinding dbExpressionBinding = SemanticAnalyzer.ProcessFromClauseItem(fromClause.FromClauseItems[i], sr, out list2);
				list.AddRange(list2);
				if (fromBinding == null)
				{
					fromBinding = dbExpressionBinding;
				}
				else
				{
					fromBinding = fromBinding.CrossApply(dbExpressionBinding).BindAs(sr.GenerateInternalName("lcapply"));
					IEnumerable<SourceScopeEntry> enumerable = list;
					Func<SourceScopeEntry, SourceScopeEntry> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (SourceScopeEntry scopeEntry) => scopeEntry.AddParentVar(fromBinding.Variable));
					}
					enumerable.Each(func);
				}
			}
			return fromBinding;
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x0011C420 File Offset: 0x0011A620
		private static DbExpressionBinding ProcessFromClauseItem(FromClauseItem fromClauseItem, SemanticResolver sr, out List<SourceScopeEntry> scopeEntries)
		{
			FromClauseItemKind fromClauseItemKind = fromClauseItem.FromClauseItemKind;
			DbExpressionBinding dbExpressionBinding;
			if (fromClauseItemKind != FromClauseItemKind.AliasedFromClause)
			{
				if (fromClauseItemKind != FromClauseItemKind.JoinFromClause)
				{
					dbExpressionBinding = SemanticAnalyzer.ProcessApplyClauseItem((ApplyClauseItem)fromClauseItem.FromExpr, sr, out scopeEntries);
				}
				else
				{
					dbExpressionBinding = SemanticAnalyzer.ProcessJoinClauseItem((JoinClauseItem)fromClauseItem.FromExpr, sr, out scopeEntries);
				}
			}
			else
			{
				dbExpressionBinding = SemanticAnalyzer.ProcessAliasedFromClauseItem((AliasedExpr)fromClauseItem.FromExpr, sr, out scopeEntries);
			}
			return dbExpressionBinding;
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x0011C480 File Offset: 0x0011A680
		private static DbExpressionBinding ProcessAliasedFromClauseItem(AliasedExpr aliasedExpr, SemanticResolver sr, out List<SourceScopeEntry> scopeEntries)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(aliasedExpr.Expr, sr);
			if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
			{
				ErrorContext errCtx = aliasedExpr.Expr.ErrCtx;
				string expressionMustBeCollection = Strings.ExpressionMustBeCollection;
				throw EntitySqlException.Create(errCtx, expressionMustBeCollection, null);
			}
			string text = sr.InferAliasName(aliasedExpr, dbExpression);
			if (sr.CurrentScope.Contains(text))
			{
				if (aliasedExpr.Alias != null)
				{
					CqlErrorHelper.ReportAliasAlreadyUsedError(text, aliasedExpr.Alias.ErrCtx, Strings.InFromClause);
				}
				else
				{
					text = sr.GenerateInternalName("autoFrom");
				}
			}
			DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(text);
			SourceScopeEntry sourceScopeEntry = new SourceScopeEntry(dbExpressionBinding.Variable);
			sr.CurrentScope.Add(dbExpressionBinding.Variable.VariableName, sourceScopeEntry);
			scopeEntries = new List<SourceScopeEntry>();
			scopeEntries.Add(sourceScopeEntry);
			return dbExpressionBinding;
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x0011C544 File Offset: 0x0011A744
		private static DbExpressionBinding ProcessJoinClauseItem(JoinClauseItem joinClause, SemanticResolver sr, out List<SourceScopeEntry> scopeEntries)
		{
			DbExpressionBinding joinBinding = null;
			if (joinClause.OnExpr == null)
			{
				if (JoinKind.Inner == joinClause.JoinKind)
				{
					ErrorContext errCtx = joinClause.ErrCtx;
					string innerJoinMustHaveOnPredicate = Strings.InnerJoinMustHaveOnPredicate;
					throw EntitySqlException.Create(errCtx, innerJoinMustHaveOnPredicate, null);
				}
			}
			else if (joinClause.JoinKind == JoinKind.Cross)
			{
				ErrorContext errCtx2 = joinClause.OnExpr.ErrCtx;
				string invalidPredicateForCrossJoin = Strings.InvalidPredicateForCrossJoin;
				throw EntitySqlException.Create(errCtx2, invalidPredicateForCrossJoin, null);
			}
			List<SourceScopeEntry> list;
			DbExpressionBinding dbExpressionBinding = SemanticAnalyzer.ProcessFromClauseItem(joinClause.LeftExpr, sr, out list);
			list.Each((SourceScopeEntry scopeEntry) => scopeEntry.IsJoinClauseLeftExpr = true);
			List<SourceScopeEntry> list2;
			DbExpressionBinding dbExpressionBinding2 = SemanticAnalyzer.ProcessFromClauseItem(joinClause.RightExpr, sr, out list2);
			list.Each((SourceScopeEntry scopeEntry) => scopeEntry.IsJoinClauseLeftExpr = false);
			if (joinClause.JoinKind == JoinKind.RightOuter)
			{
				joinClause.JoinKind = JoinKind.LeftOuter;
				DbExpressionBinding dbExpressionBinding3 = dbExpressionBinding;
				dbExpressionBinding = dbExpressionBinding2;
				dbExpressionBinding2 = dbExpressionBinding3;
			}
			DbExpressionKind dbExpressionKind = SemanticAnalyzer.MapJoinKind(joinClause.JoinKind);
			DbExpression dbExpression = null;
			if (joinClause.OnExpr == null)
			{
				if (DbExpressionKind.CrossJoin != dbExpressionKind)
				{
					dbExpression = DbExpressionBuilder.True;
				}
			}
			else
			{
				dbExpression = SemanticAnalyzer.ConvertValueExpression(joinClause.OnExpr, sr);
			}
			joinBinding = DbExpressionBuilder.CreateJoinExpressionByKind(dbExpressionKind, dbExpression, dbExpressionBinding, dbExpressionBinding2).BindAs(sr.GenerateInternalName("join"));
			scopeEntries = list;
			scopeEntries.AddRange(list2);
			scopeEntries.Each((SourceScopeEntry scopeEntry) => scopeEntry.AddParentVar(joinBinding.Variable));
			return joinBinding;
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x0011C6A1 File Offset: 0x0011A8A1
		private static DbExpressionKind MapJoinKind(JoinKind joinKind)
		{
			return SemanticAnalyzer._joinMap[(int)joinKind];
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x0011C6AC File Offset: 0x0011A8AC
		private static DbExpressionBinding ProcessApplyClauseItem(ApplyClauseItem applyClause, SemanticResolver sr, out List<SourceScopeEntry> scopeEntries)
		{
			DbExpressionBinding applyBinding = null;
			List<SourceScopeEntry> list;
			DbExpressionBinding dbExpressionBinding = SemanticAnalyzer.ProcessFromClauseItem(applyClause.LeftExpr, sr, out list);
			List<SourceScopeEntry> list2;
			DbExpressionBinding dbExpressionBinding2 = SemanticAnalyzer.ProcessFromClauseItem(applyClause.RightExpr, sr, out list2);
			applyBinding = DbExpressionBuilder.CreateApplyExpressionByKind(SemanticAnalyzer.MapApplyKind(applyClause.ApplyKind), dbExpressionBinding, dbExpressionBinding2).BindAs(sr.GenerateInternalName("apply"));
			scopeEntries = list;
			scopeEntries.AddRange(list2);
			scopeEntries.Each((SourceScopeEntry scopeEntry) => scopeEntry.AddParentVar(applyBinding.Variable));
			return applyBinding;
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x0011C732 File Offset: 0x0011A932
		private static DbExpressionKind MapApplyKind(ApplyKind applyKind)
		{
			return SemanticAnalyzer._applyMap[(int)applyKind];
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x0011C73B File Offset: 0x0011A93B
		private static DbExpressionBinding ProcessWhereClause(DbExpressionBinding source, Node whereClause, SemanticResolver sr)
		{
			if (whereClause == null)
			{
				return source;
			}
			return SemanticAnalyzer.ProcessWhereHavingClausePredicate(source, whereClause, whereClause.ErrCtx, "where", sr);
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x0011C755 File Offset: 0x0011A955
		private static DbExpressionBinding ProcessHavingClause(DbExpressionBinding source, HavingClause havingClause, SemanticResolver sr)
		{
			if (havingClause == null)
			{
				return source;
			}
			return SemanticAnalyzer.ProcessWhereHavingClausePredicate(source, havingClause.HavingPredicate, havingClause.ErrCtx, "having", sr);
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x0011C774 File Offset: 0x0011A974
		private static DbExpressionBinding ProcessWhereHavingClausePredicate(DbExpressionBinding source, Node predicate, ErrorContext errCtx, string bindingNameTemplate, SemanticResolver sr)
		{
			DbExpressionBinding whereBinding = null;
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(predicate, sr);
			if (!SemanticAnalyzer.IsBooleanType(dbExpression.ResultType))
			{
				string expressionTypeMustBeBoolean = Strings.ExpressionTypeMustBeBoolean;
				throw EntitySqlException.Create(errCtx, expressionTypeMustBeBoolean, null);
			}
			whereBinding = source.Filter(dbExpression).BindAs(sr.GenerateInternalName(bindingNameTemplate));
			sr.CurrentScopeRegion.ApplyToScopeEntries(delegate(ScopeEntry scopeEntry)
			{
				if (scopeEntry.EntryKind == ScopeEntryKind.SourceVar)
				{
					((SourceScopeEntry)scopeEntry).ReplaceParentVar(whereBinding.Variable);
				}
			});
			return whereBinding;
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x0011C7EC File Offset: 0x0011A9EC
		private static DbExpressionBinding ProcessGroupByClause(DbExpressionBinding source, QueryExpr queryExpr, SemanticResolver sr)
		{
			GroupByClause groupByClause = queryExpr.GroupByClause;
			int num = ((groupByClause != null) ? groupByClause.GroupItems.Count : 0);
			bool flag = num == 0;
			if (flag && !queryExpr.HasMethodCall)
			{
				return source;
			}
			DbGroupExpressionBinding groupInputBinding = source.Expression.GroupBindAs(sr.GenerateInternalName("geb"), sr.GenerateInternalName("group"));
			DbGroupAggregate groupAggregate = groupInputBinding.GroupAggregate;
			DbVariableReferenceExpression dbVariableReferenceExpression = groupAggregate.ResultType.Variable(sr.GenerateInternalName("groupAggregate"));
			DbExpressionBinding groupAggregateBinding = dbVariableReferenceExpression.BindAs(sr.GenerateInternalName("groupPartitionItem"));
			sr.CurrentScopeRegion.EnterGroupOperation(groupAggregateBinding);
			sr.CurrentScopeRegion.ApplyToScopeEntries(delegate(ScopeEntry scopeEntry)
			{
				((SourceScopeEntry)scopeEntry).AdjustToGroupVar(groupInputBinding.Variable, groupInputBinding.GroupVariable, groupAggregateBinding.Variable);
			});
			HashSet<string> hashSet = new HashSet<string>(sr.NameComparer);
			List<SemanticAnalyzer.GroupKeyInfo> list = new List<SemanticAnalyzer.GroupKeyInfo>(num);
			if (!flag)
			{
				for (int i = 0; i < num; i++)
				{
					AliasedExpr aliasedExpr = groupByClause.GroupItems[i];
					sr.CurrentScopeRegion.WasResolutionCorrelated = false;
					GroupKeyAggregateInfo groupKeyAggregateInfo;
					DbExpression dbExpression;
					using (sr.EnterGroupKeyDefinition(GroupAggregateKind.GroupKey, aliasedExpr.ErrCtx, out groupKeyAggregateInfo))
					{
						dbExpression = SemanticAnalyzer.ConvertValueExpression(aliasedExpr.Expr, sr);
					}
					if (!sr.CurrentScopeRegion.WasResolutionCorrelated)
					{
						ErrorContext errCtx = aliasedExpr.Expr.ErrCtx;
						string text = Strings.KeyMustBeCorrelated("GROUP BY");
						throw EntitySqlException.Create(errCtx, text, null);
					}
					if (!TypeHelpers.IsValidGroupKeyType(dbExpression.ResultType))
					{
						ErrorContext errCtx2 = aliasedExpr.Expr.ErrCtx;
						string groupingKeysMustBeEqualComparable = Strings.GroupingKeysMustBeEqualComparable;
						throw EntitySqlException.Create(errCtx2, groupingKeysMustBeEqualComparable, null);
					}
					GroupKeyAggregateInfo groupKeyAggregateInfo2;
					DbExpression dbExpression2;
					using (sr.EnterGroupKeyDefinition(GroupAggregateKind.Function, aliasedExpr.ErrCtx, out groupKeyAggregateInfo2))
					{
						dbExpression2 = SemanticAnalyzer.ConvertValueExpression(aliasedExpr.Expr, sr);
					}
					GroupKeyAggregateInfo groupKeyAggregateInfo3;
					DbExpression dbExpression3;
					using (sr.EnterGroupKeyDefinition(GroupAggregateKind.Partition, aliasedExpr.ErrCtx, out groupKeyAggregateInfo3))
					{
						dbExpression3 = SemanticAnalyzer.ConvertValueExpression(aliasedExpr.Expr, sr);
					}
					string text2 = sr.InferAliasName(aliasedExpr, dbExpression);
					if (hashSet.Contains(text2))
					{
						if (aliasedExpr.Alias != null)
						{
							CqlErrorHelper.ReportAliasAlreadyUsedError(text2, aliasedExpr.Alias.ErrCtx, Strings.InGroupClause);
						}
						else
						{
							text2 = sr.GenerateInternalName("autoGroup");
						}
					}
					hashSet.Add(text2);
					SemanticAnalyzer.GroupKeyInfo groupKeyInfo = new SemanticAnalyzer.GroupKeyInfo(text2, dbExpression, dbExpression2, dbExpression3);
					list.Add(groupKeyInfo);
					if (aliasedExpr.Alias == null)
					{
						DotExpr dotExpr = aliasedExpr.Expr as DotExpr;
						string[] array;
						if (dotExpr != null && dotExpr.IsMultipartIdentifier(out array))
						{
							groupKeyInfo.AlternativeName = array;
							string fullName = TypeResolver.GetFullName(array);
							if (hashSet.Contains(fullName))
							{
								CqlErrorHelper.ReportAliasAlreadyUsedError(fullName, dotExpr.ErrCtx, Strings.InGroupClause);
							}
							hashSet.Add(fullName);
						}
					}
				}
			}
			int currentScopeIndex = sr.CurrentScopeIndex;
			sr.EnterScope();
			foreach (SemanticAnalyzer.GroupKeyInfo groupKeyInfo2 in list)
			{
				sr.CurrentScope.Add(groupKeyInfo2.Name, new GroupKeyDefinitionScopeEntry(groupKeyInfo2.VarBasedKeyExpr, groupKeyInfo2.GroupVarBasedKeyExpr, groupKeyInfo2.GroupAggBasedKeyExpr, null));
				if (groupKeyInfo2.AlternativeName != null)
				{
					string fullName2 = TypeResolver.GetFullName(groupKeyInfo2.AlternativeName);
					sr.CurrentScope.Add(fullName2, new GroupKeyDefinitionScopeEntry(groupKeyInfo2.VarBasedKeyExpr, groupKeyInfo2.GroupVarBasedKeyExpr, groupKeyInfo2.GroupAggBasedKeyExpr, groupKeyInfo2.AlternativeName));
				}
			}
			if (queryExpr.HavingClause != null && queryExpr.HavingClause.HasMethodCall)
			{
				SemanticAnalyzer.ConvertValueExpression(queryExpr.HavingClause.HavingPredicate, sr);
			}
			Dictionary<string, DbExpression> dictionary = null;
			if (queryExpr.OrderByClause != null || queryExpr.SelectClause.HasMethodCall)
			{
				dictionary = new Dictionary<string, DbExpression>(queryExpr.SelectClause.Items.Count, sr.NameComparer);
				for (int j = 0; j < queryExpr.SelectClause.Items.Count; j++)
				{
					AliasedExpr aliasedExpr2 = queryExpr.SelectClause.Items[j];
					DbExpression dbExpression4 = SemanticAnalyzer.ConvertValueExpression(aliasedExpr2.Expr, sr);
					dbExpression4 = ((dbExpression4.ExpressionKind == DbExpressionKind.Null) ? dbExpression4 : dbExpression4.ResultType.Null());
					string text3 = sr.InferAliasName(aliasedExpr2, dbExpression4);
					if (dictionary.ContainsKey(text3))
					{
						if (aliasedExpr2.Alias != null)
						{
							CqlErrorHelper.ReportAliasAlreadyUsedError(text3, aliasedExpr2.Alias.ErrCtx, Strings.InSelectProjectionList);
						}
						else
						{
							text3 = sr.GenerateInternalName("autoProject");
						}
					}
					dictionary.Add(text3, dbExpression4);
				}
			}
			if (queryExpr.OrderByClause != null && queryExpr.OrderByClause.HasMethodCall)
			{
				sr.EnterScope();
				foreach (KeyValuePair<string, DbExpression> keyValuePair in dictionary)
				{
					sr.CurrentScope.Add(keyValuePair.Key, new ProjectionItemDefinitionScopeEntry(keyValuePair.Value));
				}
				for (int k = 0; k < queryExpr.OrderByClause.OrderByClauseItem.Count; k++)
				{
					OrderByClauseItem orderByClauseItem = queryExpr.OrderByClause.OrderByClauseItem[k];
					sr.CurrentScopeRegion.WasResolutionCorrelated = false;
					SemanticAnalyzer.ConvertValueExpression(orderByClauseItem.OrderExpr, sr);
					if (!sr.CurrentScopeRegion.WasResolutionCorrelated)
					{
						ErrorContext errCtx3 = orderByClauseItem.ErrCtx;
						string text4 = Strings.KeyMustBeCorrelated("ORDER BY");
						throw EntitySqlException.Create(errCtx3, text4, null);
					}
				}
				sr.LeaveScope();
			}
			if (flag && sr.CurrentScopeRegion.GroupAggregateInfos.Count == 0)
			{
				sr.RollbackToScope(currentScopeIndex);
				sr.CurrentScopeRegion.ApplyToScopeEntries(delegate(ScopeEntry scopeEntry)
				{
					((SourceScopeEntry)scopeEntry).RollbackAdjustmentToGroupVar(source.Variable);
				});
				sr.CurrentScopeRegion.RollbackGroupOperation();
				return source;
			}
			List<KeyValuePair<string, DbAggregate>> list2 = new List<KeyValuePair<string, DbAggregate>>(sr.CurrentScopeRegion.GroupAggregateInfos.Count);
			bool flag2 = false;
			foreach (GroupAggregateInfo groupAggregateInfo3 in sr.CurrentScopeRegion.GroupAggregateInfos)
			{
				GroupAggregateKind aggregateKind = groupAggregateInfo3.AggregateKind;
				if (aggregateKind != GroupAggregateKind.Function)
				{
					if (aggregateKind == GroupAggregateKind.Partition)
					{
						flag2 = true;
					}
				}
				else
				{
					list2.Add(new KeyValuePair<string, DbAggregate>(groupAggregateInfo3.AggregateName, ((FunctionAggregateInfo)groupAggregateInfo3).AggregateDefinition));
				}
			}
			if (flag2)
			{
				list2.Add(new KeyValuePair<string, DbAggregate>(dbVariableReferenceExpression.VariableName, groupAggregate));
			}
			DbGroupByExpression dbGroupByExpression = groupInputBinding.GroupBy(list.Select((SemanticAnalyzer.GroupKeyInfo keyInfo) => new KeyValuePair<string, DbExpression>(keyInfo.Name, keyInfo.VarBasedKeyExpr)), list2);
			DbExpressionBinding groupBinding = dbGroupByExpression.BindAs(sr.GenerateInternalName("group"));
			if (flag2)
			{
				List<KeyValuePair<string, DbExpression>> list3 = SemanticAnalyzer.ProcessGroupPartitionDefinitions(sr.CurrentScopeRegion.GroupAggregateInfos, dbVariableReferenceExpression, groupBinding);
				if (list3 != null)
				{
					list3.AddRange(list.Select((SemanticAnalyzer.GroupKeyInfo keyInfo) => new KeyValuePair<string, DbExpression>(keyInfo.Name, groupBinding.Variable.Property(keyInfo.Name))));
					list3.AddRange(from groupAggregateInfo in sr.CurrentScopeRegion.GroupAggregateInfos
						where groupAggregateInfo.AggregateKind == GroupAggregateKind.Function
						select new KeyValuePair<string, DbExpression>(groupAggregateInfo.AggregateName, groupBinding.Variable.Property(groupAggregateInfo.AggregateName)));
					DbExpression dbExpression5 = DbExpressionBuilder.NewRow(list3);
					groupBinding = groupBinding.Project(dbExpression5).BindAs(sr.GenerateInternalName("groupPartitionDefs"));
				}
			}
			sr.RollbackToScope(currentScopeIndex);
			sr.CurrentScopeRegion.ApplyToScopeEntries((ScopeEntry scopeEntry) => new InvalidGroupInputRefScopeEntry());
			sr.EnterScope();
			foreach (SemanticAnalyzer.GroupKeyInfo groupKeyInfo3 in list)
			{
				sr.CurrentScope.Add(groupKeyInfo3.VarRef.VariableName, new SourceScopeEntry(groupKeyInfo3.VarRef).AddParentVar(groupBinding.Variable));
				if (groupKeyInfo3.AlternativeName != null)
				{
					string fullName3 = TypeResolver.GetFullName(groupKeyInfo3.AlternativeName);
					sr.CurrentScope.Add(fullName3, new SourceScopeEntry(groupKeyInfo3.VarRef, groupKeyInfo3.AlternativeName).AddParentVar(groupBinding.Variable));
				}
			}
			foreach (GroupAggregateInfo groupAggregateInfo2 in sr.CurrentScopeRegion.GroupAggregateInfos)
			{
				DbVariableReferenceExpression dbVariableReferenceExpression2 = groupAggregateInfo2.AggregateStubExpression.ResultType.Variable(groupAggregateInfo2.AggregateName);
				if (!sr.CurrentScope.Contains(dbVariableReferenceExpression2.VariableName))
				{
					sr.CurrentScope.Add(dbVariableReferenceExpression2.VariableName, new SourceScopeEntry(dbVariableReferenceExpression2).AddParentVar(groupBinding.Variable));
					sr.CurrentScopeRegion.RegisterGroupAggregateName(dbVariableReferenceExpression2.VariableName);
				}
				groupAggregateInfo2.AggregateStubExpression = null;
			}
			return groupBinding;
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x0011D15C File Offset: 0x0011B35C
		private static List<KeyValuePair<string, DbExpression>> ProcessGroupPartitionDefinitions(List<GroupAggregateInfo> groupAggregateInfos, DbVariableReferenceExpression groupAggregateVarRef, DbExpressionBinding groupBinding)
		{
			ReadOnlyCollection<DbVariableReferenceExpression> readOnlyCollection = new ReadOnlyCollection<DbVariableReferenceExpression>(new DbVariableReferenceExpression[] { groupAggregateVarRef });
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
			bool flag = false;
			foreach (GroupAggregateInfo groupAggregateInfo in groupAggregateInfos)
			{
				if (groupAggregateInfo.AggregateKind == GroupAggregateKind.Partition)
				{
					GroupPartitionInfo groupPartitionInfo = (GroupPartitionInfo)groupAggregateInfo;
					DbExpression aggregateDefinition = groupPartitionInfo.AggregateDefinition;
					if (SemanticAnalyzer.IsTrivialInputProjection(groupAggregateVarRef, aggregateDefinition))
					{
						groupAggregateInfo.AggregateName = groupAggregateVarRef.VariableName;
						flag = true;
					}
					else
					{
						DbLambda dbLambda = new DbLambda(readOnlyCollection, groupPartitionInfo.AggregateDefinition);
						list.Add(new KeyValuePair<string, DbExpression>(groupAggregateInfo.AggregateName, dbLambda.Invoke(new DbExpression[] { groupBinding.Variable.Property(groupAggregateVarRef.VariableName) })));
					}
				}
			}
			if (flag)
			{
				if (list.Count > 0)
				{
					list.Add(new KeyValuePair<string, DbExpression>(groupAggregateVarRef.VariableName, groupBinding.Variable.Property(groupAggregateVarRef.VariableName)));
				}
				else
				{
					list = null;
				}
			}
			return list;
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x0011D270 File Offset: 0x0011B470
		private static bool IsTrivialInputProjection(DbVariableReferenceExpression lambdaVariable, DbExpression lambdaBody)
		{
			if (lambdaBody.ExpressionKind != DbExpressionKind.Project)
			{
				return false;
			}
			DbProjectExpression dbProjectExpression = (DbProjectExpression)lambdaBody;
			if (dbProjectExpression.Input.Expression != lambdaVariable)
			{
				return false;
			}
			if (dbProjectExpression.Projection.ExpressionKind == DbExpressionKind.VariableReference)
			{
				return (DbVariableReferenceExpression)dbProjectExpression.Projection == dbProjectExpression.Input.Variable;
			}
			if (dbProjectExpression.Projection.ExpressionKind != DbExpressionKind.NewInstance || !TypeSemantics.IsRowType(dbProjectExpression.Projection.ResultType))
			{
				return false;
			}
			if (!TypeSemantics.IsEqual(dbProjectExpression.Projection.ResultType, dbProjectExpression.Input.Variable.ResultType))
			{
				return false;
			}
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(dbProjectExpression.Input.Variable.ResultType);
			DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)dbProjectExpression.Projection;
			for (int i = 0; i < dbNewInstanceExpression.Arguments.Count; i++)
			{
				if (dbNewInstanceExpression.Arguments[i].ExpressionKind != DbExpressionKind.Property)
				{
					return false;
				}
				DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)dbNewInstanceExpression.Arguments[i];
				if (dbPropertyExpression.Instance != dbProjectExpression.Input.Variable || dbPropertyExpression.Property != allStructuralMembers[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x0011D3A0 File Offset: 0x0011B5A0
		private static DbExpressionBinding ProcessOrderByClause(DbExpressionBinding source, QueryExpr queryExpr, out bool queryProjectionProcessed, SemanticResolver sr)
		{
			queryProjectionProcessed = false;
			if (queryExpr.OrderByClause == null)
			{
				return source;
			}
			DbExpressionBinding sortBinding = null;
			OrderByClause orderByClause = queryExpr.OrderByClause;
			SelectClause selectClause = queryExpr.SelectClause;
			DbExpression dbExpression = null;
			if (orderByClause.SkipSubClause != null)
			{
				dbExpression = SemanticAnalyzer.ConvertValueExpression(orderByClause.SkipSubClause, sr);
				SemanticAnalyzer.ValidateExpressionIsCommandParamOrNonNegativeIntegerConstant(dbExpression, orderByClause.SkipSubClause.ErrCtx, "SKIP");
			}
			List<KeyValuePair<string, DbExpression>> list = SemanticAnalyzer.ConvertSelectClauseItems(queryExpr, sr);
			if (selectClause.DistinctKind == DistinctKind.Distinct)
			{
				sr.CurrentScopeRegion.RollbackAllScopes();
			}
			int currentScopeIndex = sr.CurrentScopeIndex;
			sr.EnterScope();
			list.Each((KeyValuePair<string, DbExpression> projectionItem) => sr.CurrentScope.Add(projectionItem.Key, new ProjectionItemDefinitionScopeEntry(projectionItem.Value)));
			if (selectClause.DistinctKind == DistinctKind.Distinct)
			{
				source = SemanticAnalyzer.CreateProjectExpression(source, selectClause, list).BindAs(sr.GenerateInternalName("distinct"));
				if (selectClause.SelectKind == SelectKind.Value)
				{
					sr.CurrentScope.Replace(list[0].Key, new SourceScopeEntry(source.Variable));
				}
				else
				{
					foreach (KeyValuePair<string, DbExpression> keyValuePair in list)
					{
						DbVariableReferenceExpression dbVariableReferenceExpression = keyValuePair.Value.ResultType.Variable(keyValuePair.Key);
						sr.CurrentScope.Replace(dbVariableReferenceExpression.VariableName, new SourceScopeEntry(dbVariableReferenceExpression).AddParentVar(source.Variable));
					}
				}
				queryProjectionProcessed = true;
			}
			List<DbSortClause> list2 = new List<DbSortClause>(orderByClause.OrderByClauseItem.Count);
			for (int i = 0; i < orderByClause.OrderByClauseItem.Count; i++)
			{
				OrderByClauseItem orderByClauseItem = orderByClause.OrderByClauseItem[i];
				sr.CurrentScopeRegion.WasResolutionCorrelated = false;
				DbExpression dbExpression2 = SemanticAnalyzer.ConvertValueExpression(orderByClauseItem.OrderExpr, sr);
				if (!sr.CurrentScopeRegion.WasResolutionCorrelated)
				{
					ErrorContext errCtx = orderByClauseItem.ErrCtx;
					string text = Strings.KeyMustBeCorrelated("ORDER BY");
					throw EntitySqlException.Create(errCtx, text, null);
				}
				if (!TypeHelpers.IsValidSortOpKeyType(dbExpression2.ResultType))
				{
					ErrorContext errCtx2 = orderByClauseItem.OrderExpr.ErrCtx;
					string orderByKeyIsNotOrderComparable = Strings.OrderByKeyIsNotOrderComparable;
					throw EntitySqlException.Create(errCtx2, orderByKeyIsNotOrderComparable, null);
				}
				bool flag = orderByClauseItem.OrderKind == OrderKind.None || orderByClauseItem.OrderKind == OrderKind.Asc;
				string text2 = null;
				if (orderByClauseItem.Collation != null)
				{
					if (!SemanticAnalyzer.IsStringType(dbExpression2.ResultType))
					{
						ErrorContext errCtx3 = orderByClauseItem.OrderExpr.ErrCtx;
						string text3 = Strings.InvalidKeyTypeForCollation(dbExpression2.ResultType.EdmType.FullName);
						throw EntitySqlException.Create(errCtx3, text3, null);
					}
					text2 = orderByClauseItem.Collation.Name;
				}
				if (string.IsNullOrEmpty(text2))
				{
					list2.Add(flag ? dbExpression2.ToSortClause() : dbExpression2.ToSortClauseDescending());
				}
				else
				{
					list2.Add(flag ? dbExpression2.ToSortClause(text2) : dbExpression2.ToSortClauseDescending(text2));
				}
			}
			sr.RollbackToScope(currentScopeIndex);
			DbExpression dbExpression3;
			if (dbExpression != null)
			{
				dbExpression3 = source.Skip(list2, dbExpression);
			}
			else
			{
				dbExpression3 = source.Sort(list2);
			}
			sortBinding = dbExpression3.BindAs(sr.GenerateInternalName("sort"));
			if (!queryProjectionProcessed)
			{
				sr.CurrentScopeRegion.ApplyToScopeEntries(delegate(ScopeEntry scopeEntry)
				{
					if (scopeEntry.EntryKind == ScopeEntryKind.SourceVar)
					{
						((SourceScopeEntry)scopeEntry).ReplaceParentVar(sortBinding.Variable);
					}
				});
			}
			return sortBinding;
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x0011D724 File Offset: 0x0011B924
		private static DbExpression ConvertSimpleInExpression(DbExpression left, DbExpression right)
		{
			DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)right;
			if (dbNewInstanceExpression.Arguments.Count == 0)
			{
				return DbExpressionBuilder.False;
			}
			return Helpers.BuildBalancedTreeInPlace<DbExpression>(new List<DbExpression>(dbNewInstanceExpression.Arguments.Select((DbExpression arg) => left.Equal(arg))), (DbExpression prev, DbExpression next) => prev.Or(next));
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x0011D798 File Offset: 0x0011B998
		private static bool IsStringType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String);
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x0011D7A2 File Offset: 0x0011B9A2
		private static bool IsBooleanType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Boolean);
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x0011D7AB File Offset: 0x0011B9AB
		private static bool IsSubOrSuperType(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.IsStructurallyEqual(type1, type2) || type1.IsSubtypeOf(type2) || type2.IsSubtypeOf(type1);
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x0011D7C8 File Offset: 0x0011B9C8
		private static Dictionary<Type, SemanticAnalyzer.AstExprConverter> CreateAstExprConverters()
		{
			return new Dictionary<Type, SemanticAnalyzer.AstExprConverter>(17)
			{
				{
					typeof(Literal),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertLiteral)
				},
				{
					typeof(QueryParameter),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertParameter)
				},
				{
					typeof(Identifier),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertIdentifier)
				},
				{
					typeof(DotExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertDotExpr)
				},
				{
					typeof(BuiltInExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertBuiltIn)
				},
				{
					typeof(QueryExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertQueryExpr)
				},
				{
					typeof(ParenExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertParenExpr)
				},
				{
					typeof(RowConstructorExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertRowConstructor)
				},
				{
					typeof(MultisetConstructorExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertMultisetConstructor)
				},
				{
					typeof(CaseExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertCaseExpr)
				},
				{
					typeof(RelshipNavigationExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertRelshipNavigationExpr)
				},
				{
					typeof(RefExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertRefExpr)
				},
				{
					typeof(DerefExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertDeRefExpr)
				},
				{
					typeof(MethodExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertMethodExpr)
				},
				{
					typeof(CreateRefExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertCreateRefExpr)
				},
				{
					typeof(KeyExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertKeyExpr)
				},
				{
					typeof(GroupPartitionExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertGroupPartitionExpr)
				}
			};
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x0011D9B8 File Offset: 0x0011BBB8
		private static Dictionary<BuiltInKind, SemanticAnalyzer.BuiltInExprConverter> CreateBuiltInExprConverter()
		{
			Dictionary<BuiltInKind, SemanticAnalyzer.BuiltInExprConverter> dictionary = new Dictionary<BuiltInKind, SemanticAnalyzer.BuiltInExprConverter>(4);
			dictionary.Add(BuiltInKind.Plus, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertPlusOperands(bltInExpr, sr);
				if (TypeSemantics.IsNumericType(pair.Left.ResultType))
				{
					return pair.Left.Plus(pair.Right);
				}
				MetadataFunctionGroup metadataFunctionGroup;
				if (!sr.TypeResolver.TryGetFunctionFromMetadata("Edm", "Concat", out metadataFunctionGroup))
				{
					ErrorContext errCtx = bltInExpr.ErrCtx;
					string concatBuiltinNotSupported = Strings.ConcatBuiltinNotSupported;
					throw EntitySqlException.Create(errCtx, concatBuiltinNotSupported, null);
				}
				List<TypeUsage> list = new List<TypeUsage>(2);
				list.Add(pair.Left.ResultType);
				list.Add(pair.Right.ResultType);
				bool flag = false;
				EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, list, false, out flag);
				if (edmFunction == null || flag)
				{
					ErrorContext errCtx2 = bltInExpr.ErrCtx;
					string concatBuiltinNotSupported2 = Strings.ConcatBuiltinNotSupported;
					throw EntitySqlException.Create(errCtx2, concatBuiltinNotSupported2, null);
				}
				return edmFunction.Invoke(new DbExpression[] { pair.Left, pair.Right });
			});
			dictionary.Add(BuiltInKind.Minus, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair2 = SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr);
				return pair2.Left.Minus(pair2.Right);
			});
			dictionary.Add(BuiltInKind.Multiply, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair3 = SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr);
				return pair3.Left.Multiply(pair3.Right);
			});
			dictionary.Add(BuiltInKind.Divide, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair4 = SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr);
				return pair4.Left.Divide(pair4.Right);
			});
			dictionary.Add(BuiltInKind.Modulus, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair5 = SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr);
				return pair5.Left.Modulo(pair5.Right);
			});
			dictionary.Add(BuiltInKind.UnaryMinus, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression left = SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr).Left;
				if (TypeSemantics.IsUnsignedNumericType(left.ResultType))
				{
					TypeUsage typeUsage = null;
					if (!TypeHelpers.TryGetClosestPromotableType(left.ResultType, out typeUsage))
					{
						throw new EntitySqlException(Strings.InvalidUnsignedTypeForUnaryMinusOperation(left.ResultType.EdmType.FullName));
					}
				}
				return left.UnaryMinus();
			});
			dictionary.Add(BuiltInKind.UnaryPlus, (BuiltInExpr bltInExpr, SemanticResolver sr) => SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr).Left);
			dictionary.Add(BuiltInKind.And, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair6 = SemanticAnalyzer.ConvertLogicalArgs(bltInExpr, sr);
				return pair6.Left.And(pair6.Right);
			});
			dictionary.Add(BuiltInKind.Or, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair7 = SemanticAnalyzer.ConvertLogicalArgs(bltInExpr, sr);
				return pair7.Left.Or(pair7.Right);
			});
			dictionary.Add(BuiltInKind.Not, (BuiltInExpr bltInExpr, SemanticResolver sr) => SemanticAnalyzer.ConvertLogicalArgs(bltInExpr, sr).Left.Not());
			dictionary.Add(BuiltInKind.Equal, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair8 = SemanticAnalyzer.ConvertEqualCompArgs(bltInExpr, sr);
				return pair8.Left.Equal(pair8.Right);
			});
			dictionary.Add(BuiltInKind.NotEqual, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair9 = SemanticAnalyzer.ConvertEqualCompArgs(bltInExpr, sr);
				return pair9.Left.Equal(pair9.Right).Not();
			});
			dictionary.Add(BuiltInKind.GreaterEqual, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair10 = SemanticAnalyzer.ConvertOrderCompArgs(bltInExpr, sr);
				return pair10.Left.GreaterThanOrEqual(pair10.Right);
			});
			dictionary.Add(BuiltInKind.GreaterThan, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair11 = SemanticAnalyzer.ConvertOrderCompArgs(bltInExpr, sr);
				return pair11.Left.GreaterThan(pair11.Right);
			});
			dictionary.Add(BuiltInKind.LessEqual, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair12 = SemanticAnalyzer.ConvertOrderCompArgs(bltInExpr, sr);
				return pair12.Left.LessThanOrEqual(pair12.Right);
			});
			dictionary.Add(BuiltInKind.LessThan, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair13 = SemanticAnalyzer.ConvertOrderCompArgs(bltInExpr, sr);
				return pair13.Left.LessThan(pair13.Right);
			});
			dictionary.Add(BuiltInKind.Union, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair14 = SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr);
				return pair14.Left.UnionAll(pair14.Right).Distinct();
			});
			dictionary.Add(BuiltInKind.UnionAll, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair15 = SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr);
				return pair15.Left.UnionAll(pair15.Right);
			});
			dictionary.Add(BuiltInKind.Intersect, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair16 = SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr);
				return pair16.Left.Intersect(pair16.Right);
			});
			dictionary.Add(BuiltInKind.Overlaps, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair17 = SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr);
				return pair17.Left.Intersect(pair17.Right).IsEmpty().Not();
			});
			dictionary.Add(BuiltInKind.AnyElement, (BuiltInExpr bltInExpr, SemanticResolver sr) => SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr).Left.Element());
			dictionary.Add(BuiltInKind.Element, delegate
			{
				throw new NotSupportedException(Strings.ElementOperatorIsNotSupported);
			});
			dictionary.Add(BuiltInKind.Except, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair18 = SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr);
				return pair18.Left.Except(pair18.Right);
			});
			dictionary.Add(BuiltInKind.Exists, (BuiltInExpr bltInExpr, SemanticResolver sr) => SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr).Left.IsEmpty().Not());
			dictionary.Add(BuiltInKind.Flatten, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(bltInExpr.Arg1, sr);
				if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
				{
					ErrorContext errCtx3 = bltInExpr.Arg1.ErrCtx;
					string invalidFlattenArgument = Strings.InvalidFlattenArgument;
					throw EntitySqlException.Create(errCtx3, invalidFlattenArgument, null);
				}
				if (!TypeSemantics.IsCollectionType(TypeHelpers.GetElementTypeUsage(dbExpression.ResultType)))
				{
					ErrorContext errCtx4 = bltInExpr.Arg1.ErrCtx;
					string invalidFlattenArgument2 = Strings.InvalidFlattenArgument;
					throw EntitySqlException.Create(errCtx4, invalidFlattenArgument2, null);
				}
				DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(sr.GenerateInternalName("l_flatten"));
				DbExpressionBinding dbExpressionBinding2 = dbExpressionBinding.Variable.BindAs(sr.GenerateInternalName("r_flatten"));
				DbExpressionBinding dbExpressionBinding3 = dbExpressionBinding.CrossApply(dbExpressionBinding2).BindAs(sr.GenerateInternalName("flatten"));
				return dbExpressionBinding3.Project(dbExpressionBinding3.Variable.Property(dbExpressionBinding2.VariableName));
			});
			dictionary.Add(BuiltInKind.In, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair19 = SemanticAnalyzer.ConvertInExprArgs(bltInExpr, sr);
				if (pair19.Right.ExpressionKind == DbExpressionKind.NewInstance)
				{
					return SemanticAnalyzer.ConvertSimpleInExpression(pair19.Left, pair19.Right);
				}
				DbExpressionBinding dbExpressionBinding4 = pair19.Right.BindAs(sr.GenerateInternalName("in-filter"));
				DbExpression left2 = pair19.Left;
				DbExpression variable = dbExpressionBinding4.Variable;
				DbExpression dbExpression2 = dbExpressionBinding4.Filter(left2.Equal(variable)).IsEmpty().Not();
				return DbExpressionBuilder.Case(new List<DbExpression>(1) { left2.IsNull() }, new List<DbExpression>(1) { TypeResolver.BooleanType.Null() }, DbExpressionBuilder.False).Or(dbExpression2);
			});
			dictionary.Add(BuiltInKind.NotIn, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair20 = SemanticAnalyzer.ConvertInExprArgs(bltInExpr, sr);
				if (pair20.Right.ExpressionKind == DbExpressionKind.NewInstance)
				{
					return SemanticAnalyzer.ConvertSimpleInExpression(pair20.Left, pair20.Right).Not();
				}
				DbExpressionBinding dbExpressionBinding5 = pair20.Right.BindAs(sr.GenerateInternalName("in-filter"));
				DbExpression left3 = pair20.Left;
				DbExpression variable2 = dbExpressionBinding5.Variable;
				DbExpression dbExpression3 = dbExpressionBinding5.Filter(left3.Equal(variable2)).IsEmpty();
				return DbExpressionBuilder.Case(new List<DbExpression>(1) { left3.IsNull() }, new List<DbExpression>(1) { TypeResolver.BooleanType.Null() }, DbExpressionBuilder.True).And(dbExpression3);
			});
			dictionary.Add(BuiltInKind.Distinct, (BuiltInExpr bltInExpr, SemanticResolver sr) => SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr).Left.Distinct());
			dictionary.Add(BuiltInKind.IsNull, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression4 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
				if (dbExpression4 != null && !TypeHelpers.IsValidIsNullOpType(dbExpression4.ResultType))
				{
					ErrorContext errCtx5 = bltInExpr.Arg1.ErrCtx;
					string isNullInvalidType = Strings.IsNullInvalidType;
					throw EntitySqlException.Create(errCtx5, isNullInvalidType, null);
				}
				if (dbExpression4 == null)
				{
					return DbExpressionBuilder.True;
				}
				return dbExpression4.IsNull();
			});
			dictionary.Add(BuiltInKind.IsNotNull, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression5 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
				if (dbExpression5 != null && !TypeHelpers.IsValidIsNullOpType(dbExpression5.ResultType))
				{
					ErrorContext errCtx6 = bltInExpr.Arg1.ErrCtx;
					string isNullInvalidType2 = Strings.IsNullInvalidType;
					throw EntitySqlException.Create(errCtx6, isNullInvalidType2, null);
				}
				if (dbExpression5 == null)
				{
					return DbExpressionBuilder.False;
				}
				return dbExpression5.IsNull().Not();
			});
			dictionary.Add(BuiltInKind.IsOf, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression6 = SemanticAnalyzer.ConvertValueExpression(bltInExpr.Arg1, sr);
				TypeUsage typeUsage2 = SemanticAnalyzer.ConvertTypeName(bltInExpr.Arg2, sr);
				bool flag2 = (bool)((Literal)bltInExpr.Arg3).Value;
				bool flag3 = (bool)((Literal)bltInExpr.Arg4).Value;
				bool flag4 = sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.RestrictedViewGenerationMode;
				if (!flag4 && !TypeSemantics.IsEntityType(dbExpression6.ResultType))
				{
					ErrorContext errCtx7 = bltInExpr.Arg1.ErrCtx;
					string text = Strings.ExpressionTypeMustBeEntityType(Strings.CtxIsOf, dbExpression6.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression6.ResultType.EdmType.FullName);
					throw EntitySqlException.Create(errCtx7, text, null);
				}
				if (flag4 && !TypeSemantics.IsNominalType(dbExpression6.ResultType))
				{
					ErrorContext errCtx8 = bltInExpr.Arg1.ErrCtx;
					string text2 = Strings.ExpressionTypeMustBeNominalType(Strings.CtxIsOf, dbExpression6.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression6.ResultType.EdmType.FullName);
					throw EntitySqlException.Create(errCtx8, text2, null);
				}
				if (!flag4 && !TypeSemantics.IsEntityType(typeUsage2))
				{
					ErrorContext errCtx9 = bltInExpr.Arg2.ErrCtx;
					string text3 = Strings.TypeMustBeEntityType(Strings.CtxIsOf, typeUsage2.EdmType.BuiltInTypeKind.ToString(), typeUsage2.EdmType.FullName);
					throw EntitySqlException.Create(errCtx9, text3, null);
				}
				if (flag4 && !TypeSemantics.IsNominalType(typeUsage2))
				{
					ErrorContext errCtx10 = bltInExpr.Arg2.ErrCtx;
					string text4 = Strings.TypeMustBeNominalType(Strings.CtxIsOf, typeUsage2.EdmType.BuiltInTypeKind.ToString(), typeUsage2.EdmType.FullName);
					throw EntitySqlException.Create(errCtx10, text4, null);
				}
				if (!TypeSemantics.IsPolymorphicType(dbExpression6.ResultType))
				{
					ErrorContext errCtx11 = bltInExpr.Arg1.ErrCtx;
					string typeMustBeInheritableType = Strings.TypeMustBeInheritableType;
					throw EntitySqlException.Create(errCtx11, typeMustBeInheritableType, null);
				}
				if (!TypeSemantics.IsPolymorphicType(typeUsage2))
				{
					ErrorContext errCtx12 = bltInExpr.Arg2.ErrCtx;
					string typeMustBeInheritableType2 = Strings.TypeMustBeInheritableType;
					throw EntitySqlException.Create(errCtx12, typeMustBeInheritableType2, null);
				}
				if (!SemanticAnalyzer.IsSubOrSuperType(dbExpression6.ResultType, typeUsage2))
				{
					ErrorContext errCtx13 = bltInExpr.ErrCtx;
					string text5 = Strings.NotASuperOrSubType(dbExpression6.ResultType.EdmType.FullName, typeUsage2.EdmType.FullName);
					throw EntitySqlException.Create(errCtx13, text5, null);
				}
				typeUsage2 = TypeHelpers.GetReadOnlyType(typeUsage2);
				DbExpression dbExpression7;
				if (flag2)
				{
					dbExpression7 = dbExpression6.IsOfOnly(typeUsage2);
				}
				else
				{
					dbExpression7 = dbExpression6.IsOf(typeUsage2);
				}
				if (flag3)
				{
					dbExpression7 = dbExpression7.Not();
				}
				return dbExpression7;
			});
			dictionary.Add(BuiltInKind.Treat, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression8 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
				TypeUsage typeUsage3 = SemanticAnalyzer.ConvertTypeName(bltInExpr.Arg2, sr);
				bool flag5 = sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.RestrictedViewGenerationMode;
				if (!flag5 && !TypeSemantics.IsEntityType(typeUsage3))
				{
					ErrorContext errCtx14 = bltInExpr.Arg2.ErrCtx;
					string text6 = Strings.TypeMustBeEntityType(Strings.CtxTreat, typeUsage3.EdmType.BuiltInTypeKind.ToString(), typeUsage3.EdmType.FullName);
					throw EntitySqlException.Create(errCtx14, text6, null);
				}
				if (flag5 && !TypeSemantics.IsNominalType(typeUsage3))
				{
					ErrorContext errCtx15 = bltInExpr.Arg2.ErrCtx;
					string text7 = Strings.TypeMustBeNominalType(Strings.CtxTreat, typeUsage3.EdmType.BuiltInTypeKind.ToString(), typeUsage3.EdmType.FullName);
					throw EntitySqlException.Create(errCtx15, text7, null);
				}
				if (dbExpression8 == null)
				{
					dbExpression8 = typeUsage3.Null();
				}
				else
				{
					if (!flag5 && !TypeSemantics.IsEntityType(dbExpression8.ResultType))
					{
						ErrorContext errCtx16 = bltInExpr.Arg1.ErrCtx;
						string text8 = Strings.ExpressionTypeMustBeEntityType(Strings.CtxTreat, dbExpression8.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression8.ResultType.EdmType.FullName);
						throw EntitySqlException.Create(errCtx16, text8, null);
					}
					if (flag5 && !TypeSemantics.IsNominalType(dbExpression8.ResultType))
					{
						ErrorContext errCtx17 = bltInExpr.Arg1.ErrCtx;
						string text9 = Strings.ExpressionTypeMustBeNominalType(Strings.CtxTreat, dbExpression8.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression8.ResultType.EdmType.FullName);
						throw EntitySqlException.Create(errCtx17, text9, null);
					}
				}
				if (!TypeSemantics.IsPolymorphicType(dbExpression8.ResultType))
				{
					ErrorContext errCtx18 = bltInExpr.Arg1.ErrCtx;
					string typeMustBeInheritableType3 = Strings.TypeMustBeInheritableType;
					throw EntitySqlException.Create(errCtx18, typeMustBeInheritableType3, null);
				}
				if (!TypeSemantics.IsPolymorphicType(typeUsage3))
				{
					ErrorContext errCtx19 = bltInExpr.Arg2.ErrCtx;
					string typeMustBeInheritableType4 = Strings.TypeMustBeInheritableType;
					throw EntitySqlException.Create(errCtx19, typeMustBeInheritableType4, null);
				}
				if (!SemanticAnalyzer.IsSubOrSuperType(dbExpression8.ResultType, typeUsage3))
				{
					ErrorContext errCtx20 = bltInExpr.Arg1.ErrCtx;
					string text10 = Strings.NotASuperOrSubType(dbExpression8.ResultType.EdmType.FullName, typeUsage3.EdmType.FullName);
					throw EntitySqlException.Create(errCtx20, text10, null);
				}
				return dbExpression8.TreatAs(TypeHelpers.GetReadOnlyType(typeUsage3));
			});
			dictionary.Add(BuiltInKind.Cast, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression9 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
				TypeUsage typeUsage4 = SemanticAnalyzer.ConvertTypeName(bltInExpr.Arg2, sr);
				if (!TypeSemantics.IsScalarType(typeUsage4))
				{
					ErrorContext errCtx21 = bltInExpr.Arg2.ErrCtx;
					string invalidCastType = Strings.InvalidCastType;
					throw EntitySqlException.Create(errCtx21, invalidCastType, null);
				}
				if (dbExpression9 == null)
				{
					return typeUsage4.Null();
				}
				if (!TypeSemantics.IsScalarType(dbExpression9.ResultType))
				{
					ErrorContext errCtx22 = bltInExpr.Arg1.ErrCtx;
					string invalidCastExpressionType = Strings.InvalidCastExpressionType;
					throw EntitySqlException.Create(errCtx22, invalidCastExpressionType, null);
				}
				if (!TypeSemantics.IsCastAllowed(dbExpression9.ResultType, typeUsage4))
				{
					ErrorContext errCtx23 = bltInExpr.Arg1.ErrCtx;
					string text11 = Strings.InvalidCast(dbExpression9.ResultType.EdmType.FullName, typeUsage4.EdmType.FullName);
					throw EntitySqlException.Create(errCtx23, text11, null);
				}
				return dbExpression9.CastTo(TypeHelpers.GetReadOnlyType(typeUsage4));
			});
			dictionary.Add(BuiltInKind.OfType, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression10 = SemanticAnalyzer.ConvertValueExpression(bltInExpr.Arg1, sr);
				TypeUsage typeUsage5 = SemanticAnalyzer.ConvertTypeName(bltInExpr.Arg2, sr);
				bool flag6 = (bool)((Literal)bltInExpr.Arg3).Value;
				bool flag7 = sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.RestrictedViewGenerationMode;
				if (!TypeSemantics.IsCollectionType(dbExpression10.ResultType))
				{
					ErrorContext errCtx24 = bltInExpr.Arg1.ErrCtx;
					string expressionMustBeCollection = Strings.ExpressionMustBeCollection;
					throw EntitySqlException.Create(errCtx24, expressionMustBeCollection, null);
				}
				TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(dbExpression10.ResultType);
				if (!flag7 && !TypeSemantics.IsEntityType(elementTypeUsage))
				{
					ErrorContext errCtx25 = bltInExpr.Arg1.ErrCtx;
					string text12 = Strings.OfTypeExpressionElementTypeMustBeEntityType(elementTypeUsage.EdmType.BuiltInTypeKind.ToString(), elementTypeUsage);
					throw EntitySqlException.Create(errCtx25, text12, null);
				}
				if (flag7 && !TypeSemantics.IsNominalType(elementTypeUsage))
				{
					ErrorContext errCtx26 = bltInExpr.Arg1.ErrCtx;
					string text13 = Strings.OfTypeExpressionElementTypeMustBeNominalType(elementTypeUsage.EdmType.BuiltInTypeKind.ToString(), elementTypeUsage);
					throw EntitySqlException.Create(errCtx26, text13, null);
				}
				if (!flag7 && !TypeSemantics.IsEntityType(typeUsage5))
				{
					ErrorContext errCtx27 = bltInExpr.Arg2.ErrCtx;
					string text14 = Strings.TypeMustBeEntityType(Strings.CtxOfType, typeUsage5.EdmType.BuiltInTypeKind.ToString(), typeUsage5.EdmType.FullName);
					throw EntitySqlException.Create(errCtx27, text14, null);
				}
				if (flag7 && !TypeSemantics.IsNominalType(typeUsage5))
				{
					ErrorContext errCtx28 = bltInExpr.Arg2.ErrCtx;
					string text15 = Strings.TypeMustBeNominalType(Strings.CtxOfType, typeUsage5.EdmType.BuiltInTypeKind.ToString(), typeUsage5.EdmType.FullName);
					throw EntitySqlException.Create(errCtx28, text15, null);
				}
				if (flag6 && typeUsage5.EdmType.Abstract)
				{
					ErrorContext errCtx29 = bltInExpr.Arg2.ErrCtx;
					string text16 = Strings.OfTypeOnlyTypeArgumentCannotBeAbstract(typeUsage5.EdmType.FullName);
					throw EntitySqlException.Create(errCtx29, text16, null);
				}
				if (!SemanticAnalyzer.IsSubOrSuperType(elementTypeUsage, typeUsage5))
				{
					ErrorContext errCtx30 = bltInExpr.Arg1.ErrCtx;
					string text17 = Strings.NotASuperOrSubType(elementTypeUsage.EdmType.FullName, typeUsage5.EdmType.FullName);
					throw EntitySqlException.Create(errCtx30, text17, null);
				}
				DbExpression dbExpression11;
				if (flag6)
				{
					dbExpression11 = dbExpression10.OfTypeOnly(TypeHelpers.GetReadOnlyType(typeUsage5));
				}
				else
				{
					dbExpression11 = dbExpression10.OfType(TypeHelpers.GetReadOnlyType(typeUsage5));
				}
				return dbExpression11;
			});
			dictionary.Add(BuiltInKind.Like, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression12 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
				if (dbExpression12 == null)
				{
					dbExpression12 = TypeResolver.StringType.Null();
				}
				else if (!SemanticAnalyzer.IsStringType(dbExpression12.ResultType))
				{
					ErrorContext errCtx31 = bltInExpr.Arg1.ErrCtx;
					string likeArgMustBeStringType = Strings.LikeArgMustBeStringType;
					throw EntitySqlException.Create(errCtx31, likeArgMustBeStringType, null);
				}
				DbExpression dbExpression13 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg2, sr);
				if (dbExpression13 == null)
				{
					dbExpression13 = TypeResolver.StringType.Null();
				}
				else if (!SemanticAnalyzer.IsStringType(dbExpression13.ResultType))
				{
					ErrorContext errCtx32 = bltInExpr.Arg2.ErrCtx;
					string likeArgMustBeStringType2 = Strings.LikeArgMustBeStringType;
					throw EntitySqlException.Create(errCtx32, likeArgMustBeStringType2, null);
				}
				DbExpression dbExpression15;
				if (3 == bltInExpr.ArgCount)
				{
					DbExpression dbExpression14 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg3, sr);
					if (dbExpression14 == null)
					{
						dbExpression14 = TypeResolver.StringType.Null();
					}
					else if (!SemanticAnalyzer.IsStringType(dbExpression14.ResultType))
					{
						ErrorContext errCtx33 = bltInExpr.Arg3.ErrCtx;
						string likeArgMustBeStringType3 = Strings.LikeArgMustBeStringType;
						throw EntitySqlException.Create(errCtx33, likeArgMustBeStringType3, null);
					}
					dbExpression15 = dbExpression12.Like(dbExpression13, dbExpression14);
				}
				else
				{
					dbExpression15 = dbExpression12.Like(dbExpression13);
				}
				return dbExpression15;
			});
			dictionary.Add(BuiltInKind.Between, new SemanticAnalyzer.BuiltInExprConverter(SemanticAnalyzer.ConvertBetweenExpr));
			dictionary.Add(BuiltInKind.NotBetween, (BuiltInExpr bltInExpr, SemanticResolver sr) => SemanticAnalyzer.ConvertBetweenExpr(bltInExpr, sr).Not());
			return dictionary;
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x0011DF54 File Offset: 0x0011C154
		private static DbExpression ConvertBetweenExpr(BuiltInExpr bltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(bltInExpr.Arg2, bltInExpr.Arg3, bltInExpr.Arg1.ErrCtx, () => Strings.BetweenLimitsCannotBeUntypedNulls, sr);
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(pair.Left.ResultType, pair.Right.ResultType);
			if (commonTypeUsage == null)
			{
				ErrorContext errCtx = bltInExpr.Arg1.ErrCtx;
				string text = Strings.BetweenLimitsTypesAreNotCompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			if (!TypeSemantics.IsOrderComparableTo(pair.Left.ResultType, pair.Right.ResultType))
			{
				ErrorContext errCtx2 = bltInExpr.Arg1.ErrCtx;
				string text2 = Strings.BetweenLimitsTypesAreNotOrderComparable(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx2, text2, null);
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
			if (dbExpression == null)
			{
				dbExpression = commonTypeUsage.Null();
			}
			if (!TypeSemantics.IsOrderComparableTo(dbExpression.ResultType, commonTypeUsage))
			{
				ErrorContext errCtx3 = bltInExpr.Arg1.ErrCtx;
				string text3 = Strings.BetweenValueIsNotOrderComparable(dbExpression.ResultType.EdmType.FullName, commonTypeUsage.EdmType.FullName);
				throw EntitySqlException.Create(errCtx3, text3, null);
			}
			return dbExpression.GreaterThanOrEqual(pair.Left).And(dbExpression.LessThanOrEqual(pair.Right));
		}

		// Token: 0x04001C67 RID: 7271
		private readonly SemanticResolver _sr;

		// Token: 0x04001C68 RID: 7272
		private static readonly DbExpressionKind[] _joinMap = new DbExpressionKind[]
		{
			DbExpressionKind.CrossJoin,
			DbExpressionKind.InnerJoin,
			DbExpressionKind.LeftOuterJoin,
			DbExpressionKind.FullOuterJoin
		};

		// Token: 0x04001C69 RID: 7273
		private static readonly DbExpressionKind[] _applyMap = new DbExpressionKind[]
		{
			DbExpressionKind.CrossApply,
			DbExpressionKind.OuterApply
		};

		// Token: 0x04001C6A RID: 7274
		private static readonly Dictionary<Type, SemanticAnalyzer.AstExprConverter> _astExprConverters = SemanticAnalyzer.CreateAstExprConverters();

		// Token: 0x04001C6B RID: 7275
		private static readonly Dictionary<BuiltInKind, SemanticAnalyzer.BuiltInExprConverter> _builtInExprConverter = SemanticAnalyzer.CreateBuiltInExprConverter();

		// Token: 0x02000C75 RID: 3189
		// (Invoke) Token: 0x06006B3F RID: 27455
		private delegate ParseResult StatementConverter(Statement astExpr, SemanticResolver sr);

		// Token: 0x02000C76 RID: 3190
		private sealed class InlineFunctionInfoImpl : InlineFunctionInfo
		{
			// Token: 0x06006B42 RID: 27458 RVA: 0x0016ECE6 File Offset: 0x0016CEE6
			internal InlineFunctionInfoImpl(FunctionDefinition functionDef, List<DbVariableReferenceExpression> parameters)
				: base(functionDef, parameters)
			{
			}

			// Token: 0x06006B43 RID: 27459 RVA: 0x0016ECF0 File Offset: 0x0016CEF0
			internal override DbLambda GetLambda(SemanticResolver sr)
			{
				if (this._convertedDefinition == null)
				{
					if (this._convertingDefinition)
					{
						ErrorContext errCtx = this.FunctionDefAst.ErrCtx;
						string text = Strings.Cqt_UDF_FunctionDefinitionWithCircularReference(this.FunctionDefAst.Name);
						throw EntitySqlException.Create(errCtx, text, null);
					}
					SemanticResolver semanticResolver = sr.CloneForInlineFunctionConversion();
					this._convertingDefinition = true;
					this._convertedDefinition = SemanticAnalyzer.ConvertInlineFunctionDefinition(this, semanticResolver);
					this._convertingDefinition = false;
				}
				return this._convertedDefinition;
			}

			// Token: 0x0400312D RID: 12589
			private DbLambda _convertedDefinition;

			// Token: 0x0400312E RID: 12590
			private bool _convertingDefinition;
		}

		// Token: 0x02000C77 RID: 3191
		private sealed class GroupKeyInfo
		{
			// Token: 0x06006B44 RID: 27460 RVA: 0x0016ED59 File Offset: 0x0016CF59
			internal GroupKeyInfo(string name, DbExpression varBasedKeyExpr, DbExpression groupVarBasedKeyExpr, DbExpression groupAggBasedKeyExpr)
			{
				this.Name = name;
				this.VarRef = varBasedKeyExpr.ResultType.Variable(name);
				this.VarBasedKeyExpr = varBasedKeyExpr;
				this.GroupVarBasedKeyExpr = groupVarBasedKeyExpr;
				this.GroupAggBasedKeyExpr = groupAggBasedKeyExpr;
			}

			// Token: 0x1700118C RID: 4492
			// (get) Token: 0x06006B45 RID: 27461 RVA: 0x0016ED90 File Offset: 0x0016CF90
			// (set) Token: 0x06006B46 RID: 27462 RVA: 0x0016ED98 File Offset: 0x0016CF98
			internal string[] AlternativeName
			{
				get
				{
					return this._alternativeName;
				}
				set
				{
					this._alternativeName = value;
				}
			}

			// Token: 0x0400312F RID: 12591
			internal readonly string Name;

			// Token: 0x04003130 RID: 12592
			private string[] _alternativeName;

			// Token: 0x04003131 RID: 12593
			internal readonly DbVariableReferenceExpression VarRef;

			// Token: 0x04003132 RID: 12594
			internal readonly DbExpression VarBasedKeyExpr;

			// Token: 0x04003133 RID: 12595
			internal readonly DbExpression GroupVarBasedKeyExpr;

			// Token: 0x04003134 RID: 12596
			internal readonly DbExpression GroupAggBasedKeyExpr;
		}

		// Token: 0x02000C78 RID: 3192
		// (Invoke) Token: 0x06006B48 RID: 27464
		private delegate ExpressionResolution AstExprConverter(Node astExpr, SemanticResolver sr);

		// Token: 0x02000C79 RID: 3193
		// (Invoke) Token: 0x06006B4C RID: 27468
		private delegate DbExpression BuiltInExprConverter(BuiltInExpr astBltInExpr, SemanticResolver sr);
	}
}
