using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Data.OData;
using Microsoft.Data.OData.Query;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008BC RID: 2236
	internal class ODataCompiler
	{
		// Token: 0x06003FD7 RID: 16343 RVA: 0x000D3FC4 File Offset: 0x000D21C4
		private ODataCompiler(QueryDescriptorQueryToken queryToken, IEdmTypeReference dataSourceTypeRef, ODataVersion odataVersion, List<IEdmFunctionParameter> functionParams)
		{
			if (dataSourceTypeRef == null)
			{
				throw new ArgumentNullException("dataSourceTypeRef");
			}
			this.queryToken = queryToken;
			this.dataSourceTypeRef = dataSourceTypeRef;
			this.odataVersion = odataVersion;
			if (functionParams != null)
			{
				this.functionParams = new Dictionary<string, IEdmTypeReference>();
				this.functionParamsIndex = new Dictionary<int, string>();
				int num = 0;
				foreach (IEdmFunctionParameter edmFunctionParameter in functionParams)
				{
					this.functionParams.Add(edmFunctionParameter.Name, edmFunctionParameter.Type);
					this.functionParamsIndex.Add(num++, edmFunctionParameter.Name);
				}
			}
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x000D4080 File Offset: 0x000D2280
		public static Value Decompile(QueryDescriptorQueryToken queryToken, string feedName, ODataEnvironment environment, Value feedValue, IEngineHost host)
		{
			if (queryToken == null)
			{
				throw new ArgumentNullException("query");
			}
			if (feedName == null)
			{
				throw new ArgumentNullException("feedName");
			}
			if (environment == null)
			{
				throw new ArgumentNullException("environment");
			}
			if (feedValue == null)
			{
				throw new ArgumentNullException("feedValue");
			}
			if (queryToken.Expand != null)
			{
				return null;
			}
			if (!ODataCompiler.VerifySegmentsHaveNoLookup(queryToken.Path))
			{
				return null;
			}
			List<IEdmFunctionParameter> list;
			IEdmTypeReference edmTypeReference = ODataCompiler.FindResultEdmType(environment.EdmModel, feedName, out list);
			if (edmTypeReference == null)
			{
				return null;
			}
			return new ODataCompiler(queryToken, edmTypeReference, ODataVersion.V3, list).Decompile(new ODataCompiler.CodeBuilder(feedValue), environment, host);
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x000D410C File Offset: 0x000D230C
		private Value Decompile(ODataCompiler.CodeBuilder codeBuilder, ODataEnvironment environment, IEngineHost host)
		{
			try
			{
				bool flag = false;
				Stack<SegmentQueryToken> pathStartingFromRoot = ODataCompiler.GetPathStartingFromRoot(this.queryToken.Path, out flag);
				if (flag)
				{
					return null;
				}
				IEdmTypeReference edmTypeReference = this.dataSourceTypeRef;
				List<QueryOptionQueryToken> list = this.ProcessQueryOptions(this.queryToken.QueryOptions);
				IExpression expression = this.ToSegmentExpression(pathStartingFromRoot, ref edmTypeReference, list, codeBuilder);
				if (expression != null && this.queryToken.Filter != null)
				{
					expression = this.ToSelectFunctionInvocation(expression, this.queryToken.Filter, edmTypeReference, codeBuilder);
				}
				if (expression != null && this.queryToken.OrderByTokens != null && this.queryToken.OrderByTokens.Count<OrderByQueryToken>() > 0)
				{
					expression = this.ToSortFunctionInvocation(expression, this.queryToken.OrderByTokens, edmTypeReference, codeBuilder);
				}
				if (expression != null && this.queryToken.Select != null)
				{
					expression = ODataCompiler.ToTransformFunctionInvocation(expression, this.queryToken.Select, edmTypeReference, codeBuilder);
				}
				if (expression != null && this.queryToken.Skip != null)
				{
					codeBuilder.Wrap("Table.Skip", this.queryToken.Skip.Value.ToString(CultureInfo.CurrentCulture));
				}
				if (expression != null && this.queryToken.Top != null)
				{
					codeBuilder.Wrap("Table.FirstN", this.queryToken.Top.Value.ToString(CultureInfo.CurrentCulture));
				}
				if (this.queryToken.Expand != null || this.queryToken.Format != null || this.queryToken.InlineCount != null)
				{
					expression = null;
				}
				if (expression != null && environment.IsExpressionSupported(expression, host))
				{
					return LanguageLibrary.Evaluate(codeBuilder.ToString(), RecordValue.New(Keys.New("OData.Feed.Internal"), new Value[] { codeBuilder.FeedValue }), new Module[] { Modules.All });
				}
				return null;
			}
			catch (ValueException)
			{
			}
			catch (NotSupportedException)
			{
			}
			catch (ODataException)
			{
			}
			return null;
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x000D4354 File Offset: 0x000D2554
		private static IEdmTypeReference FindResultEdmType(IEdmModel model, string name, out List<IEdmFunctionParameter> functionParams)
		{
			foreach (IEdmEntityContainer edmEntityContainer in model.EntityContainers())
			{
				foreach (IEdmEntityContainerElement edmEntityContainerElement in edmEntityContainer.Elements)
				{
					if (edmEntityContainerElement.ContainerElementKind == EdmContainerElementKind.EntitySet)
					{
						IEdmEntitySet edmEntitySet = (IEdmEntitySet)edmEntityContainerElement;
						if (edmEntityContainerElement.Name == name || edmEntitySet.ElementType.Name == name)
						{
							functionParams = null;
							return EdmCoreModel.GetCollection(new EdmEntityTypeReference(edmEntitySet.ElementType, true));
						}
					}
					else if (edmEntityContainerElement.ContainerElementKind == EdmContainerElementKind.FunctionImport)
					{
						IEdmFunctionImport edmFunctionImport = (IEdmFunctionImport)edmEntityContainerElement;
						if (edmFunctionImport.Name == name && edmFunctionImport.ReturnType != null)
						{
							functionParams = new List<IEdmFunctionParameter>(edmFunctionImport.Parameters);
							return edmFunctionImport.ReturnType;
						}
					}
				}
			}
			functionParams = null;
			return null;
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x000D446C File Offset: 0x000D266C
		private static Stack<SegmentQueryToken> GetPathStartingFromRoot(QueryToken path, out bool hasCount)
		{
			Stack<SegmentQueryToken> stack = new Stack<SegmentQueryToken>();
			hasCount = false;
			while (path != null)
			{
				if (path.Kind == QueryTokenKind.KeywordSegment)
				{
					KeywordSegmentQueryToken keywordSegmentQueryToken = (KeywordSegmentQueryToken)path;
					if (keywordSegmentQueryToken.Keyword != KeywordKind.Count)
					{
						throw new NotSupportedException();
					}
					if (keywordSegmentQueryToken.Parent == null)
					{
						throw new NotSupportedException();
					}
					path = keywordSegmentQueryToken.Parent;
					hasCount = true;
				}
				else
				{
					if (path.Kind != QueryTokenKind.Segment)
					{
						throw new NotSupportedException();
					}
					SegmentQueryToken segmentQueryToken = (SegmentQueryToken)path;
					stack.Push(segmentQueryToken);
					path = segmentQueryToken.Parent;
				}
			}
			return stack;
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x000D44EA File Offset: 0x000D26EA
		private IEdmTypeReference GetSegmentChildrenType(IEdmTypeReference segmentTypeRef, string parentName, SegmentQueryToken childSegment)
		{
			if (ODataCompiler.IsCollectionType(segmentTypeRef))
			{
				throw new NotSupportedException();
			}
			IEdmProperty edmProperty = ODataCompiler.StripCollectionType(segmentTypeRef).AsStructured().FindProperty(childSegment.Name);
			if (edmProperty == null)
			{
				throw new NotSupportedException();
			}
			return edmProperty.Type;
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x000D451E File Offset: 0x000D271E
		private static IEdmStructuredTypeReference GetStructuredType(IEdmTypeReference edmTypeRef)
		{
			edmTypeRef = ODataCompiler.StripCollectionType(edmTypeRef);
			return edmTypeRef as IEdmStructuredTypeReference;
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x000D452E File Offset: 0x000D272E
		private static bool IsCollectionType(IEdmTypeReference edmTypeRef)
		{
			return edmTypeRef != null && edmTypeRef.Definition != null && edmTypeRef.Definition.TypeKind == EdmTypeKind.Collection;
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x000D454C File Offset: 0x000D274C
		private List<QueryOptionQueryToken> ProcessQueryOptions(IEnumerable<QueryOptionQueryToken> queryOptions)
		{
			List<QueryOptionQueryToken> list = ((this.functionParams != null) ? new List<QueryOptionQueryToken>(this.functionParams.Count) : null);
			if (this.functionParams != null)
			{
				foreach (QueryOptionQueryToken queryOptionQueryToken in queryOptions)
				{
					if (!string.IsNullOrEmpty(queryOptionQueryToken.Name))
					{
						if (queryOptionQueryToken.Name[0] == '$' || !this.functionParams.ContainsKey(queryOptionQueryToken.Name))
						{
							throw new NotSupportedException();
						}
						list.Add(queryOptionQueryToken);
					}
				}
			}
			return list;
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000D45F0 File Offset: 0x000D27F0
		private static IEdmTypeReference StripCollectionType(IEdmTypeReference edmTypeRef)
		{
			if (edmTypeRef != null && edmTypeRef.Definition != null && edmTypeRef.Definition.TypeKind == EdmTypeKind.Collection)
			{
				edmTypeRef = edmTypeRef.AsCollection().ElementType();
			}
			return edmTypeRef;
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000D461C File Offset: 0x000D281C
		private IExpression ToBinary(BinaryOperatorQueryToken binaryOperation, ODataCompiler.CodeBuilder codeBuilder)
		{
			ODataCompiler.CodeBuilder codeBuilder2 = codeBuilder.CreateChild();
			IExpression expression = this.ToExpression(binaryOperation.Left, codeBuilder2);
			ODataCompiler.CodeBuilder codeBuilder3 = codeBuilder.CreateChild();
			IExpression expression2 = this.ToExpression(binaryOperation.Right, codeBuilder3);
			string text;
			IExpression expression3;
			switch (binaryOperation.OperatorKind)
			{
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Or:
				text = "({0}) or ({1})";
				expression3 = new OrBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.And:
				text = "({0}) and ({1})";
				expression3 = new AndBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Equal:
				text = "({0}) = ({1})";
				expression3 = new EqualsBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.NotEqual:
				text = "({0}) <> ({1})";
				expression3 = new NotEqualsBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.GreaterThan:
				text = "({0}) > ({1})";
				expression3 = new GreaterThanBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.GreaterThanOrEqual:
				text = "({0}) >= ({1})";
				expression3 = new GreaterThanOrEqualsBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.LessThan:
				text = "({0}) < ({1})";
				expression3 = new LessThanBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.LessThanOrEqual:
				text = "({0}) <= ({1})";
				expression3 = new LessThanOrEqualsBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Add:
				text = "({0}) + ({1})";
				expression3 = new AddBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Subtract:
				text = "({0}) - ({1})";
				expression3 = new SubtractBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Multiply:
				text = "({0}) * ({1})";
				expression3 = new MultiplyBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Divide:
				text = "({0}) / ({1})";
				expression3 = new DivideBinaryExpressionSyntaxNode(expression, expression2);
				break;
			case Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Modulo:
				text = "Number.Mod({0}, {1})";
				expression3 = ODataCompiler.ToFunctionInvocation(ODataCompiler.ToConstantExpression(Library.Number.Mod), new IExpression[] { expression, expression2 });
				break;
			default:
				throw new NotSupportedException();
			}
			codeBuilder.Append(text, new object[]
			{
				codeBuilder2.ToString(),
				codeBuilder3.ToString()
			});
			return expression3;
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x000D47E0 File Offset: 0x000D29E0
		private IExpression ToConstant(string text, ODataCompiler.CodeBuilder codeBuilder)
		{
			text = ((text != null) ? text.Replace("%27", "'") : text);
			object obj = ODataUriUtils.ConvertFromUriLiteral(text, this.odataVersion);
			return this.ToValueExpression(obj, codeBuilder);
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x000D481A File Offset: 0x000D2A1A
		private static IConstantExpression ToConstant(LiteralQueryToken literal, ODataCompiler.CodeBuilder codeBuilder)
		{
			return ODataCompiler.ToConstant(ODataTypeServices.MarshalFromClr(literal.Value), codeBuilder);
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x000D482D File Offset: 0x000D2A2D
		private static IConstantExpression ToConstant(Value constant, ODataCompiler.CodeBuilder codeBuilder)
		{
			codeBuilder.Append(constant.ToSource(), Array.Empty<object>());
			return new ConstantExpressionSyntaxNode(constant);
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x000D4846 File Offset: 0x000D2A46
		private static IConstantExpression ToConstantExpression(Value constant)
		{
			return new ConstantExpressionSyntaxNode(constant);
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x000D484E File Offset: 0x000D2A4E
		private static IFunctionExpression ToEachExpression(IExpression expression)
		{
			return new FunctionExpressionSyntaxNode(ODataCompiler.ImplicitFunctionType, expression, new TokenRange(ODataCompiler.ImplicitFunctionType, expression));
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000D4866 File Offset: 0x000D2A66
		private IExpression ToExpression(QueryToken expression, ODataCompiler.CodeBuilder codeBuilder)
		{
			return this.ToExpression(expression, null, codeBuilder);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000D4874 File Offset: 0x000D2A74
		private IExpression ToExpression(QueryToken expression, IEnumerable<QueryOptionQueryToken> paramTokens, ODataCompiler.CodeBuilder codeBuilder)
		{
			switch (expression.Kind)
			{
			case QueryTokenKind.BinaryOperator:
				return this.ToBinary((BinaryOperatorQueryToken)expression, codeBuilder);
			case QueryTokenKind.UnaryOperator:
				return this.ToUnary((UnaryOperatorQueryToken)expression, codeBuilder);
			case QueryTokenKind.Literal:
				return ODataCompiler.ToConstant((LiteralQueryToken)expression, codeBuilder);
			case QueryTokenKind.FunctionCall:
				return this.ToFunctionInvocation((FunctionCallQueryToken)expression, codeBuilder);
			case QueryTokenKind.PropertyAccess:
				return this.ToFieldAccess((PropertyAccessQueryToken)expression, codeBuilder);
			case QueryTokenKind.QueryOption:
				return this.ToConstant(((QueryOptionQueryToken)expression).Value, codeBuilder);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000D490C File Offset: 0x000D2B0C
		private IFieldAccessExpression ToFieldAccess(PropertyAccessQueryToken propertyAccess, ODataCompiler.CodeBuilder codeBuilder)
		{
			IExpression expression;
			if (propertyAccess.Parent == null)
			{
				expression = ODataCompiler.ImplicitIdentifierExpression;
				codeBuilder.Append("_[{0}]", new object[] { EdmNameEncoder.Decode(propertyAccess.Name) });
			}
			else
			{
				expression = this.ToExpression(propertyAccess.Parent, codeBuilder);
				codeBuilder.Append("[{0}]", new object[] { EdmNameEncoder.Decode(propertyAccess.Name) });
			}
			return new RequiredFieldAccessExpressionSyntaxNode(expression, propertyAccess.Name);
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x000D4988 File Offset: 0x000D2B88
		private IInvocationExpression ToFunctionInvocation(FunctionCallQueryToken functionCall, ODataCompiler.CodeBuilder codeBuilder)
		{
			IExpression[] array = null;
			string text;
			IConstantExpression constantExpression;
			if (!ODataCompiler.TryGetBuiltInFunction(functionCall.Name, out text, out constantExpression))
			{
				throw new NotSupportedException();
			}
			codeBuilder.Append(text, Array.Empty<object>());
			List<QueryToken> list = new List<QueryToken>(functionCall.Arguments);
			int num = list.Count;
			if (num > 0)
			{
				array = new IExpression[num];
				if (functionCall.Name == "substringof")
				{
					QueryToken queryToken = list[0];
					list[0] = list[1];
					list[1] = queryToken;
				}
				codeBuilder.Append("(", Array.Empty<object>());
				bool flag = functionCall.Name == "concat";
				if (flag)
				{
					codeBuilder.Append("{", Array.Empty<object>());
				}
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ToExpression(list[i], codeBuilder);
					if (i < num - 1)
					{
						codeBuilder.Append(", ", Array.Empty<object>());
					}
				}
				if (flag)
				{
					array = new IExpression[] { ODataCompiler.ToList(array) };
					num = 1;
					codeBuilder.Append("}", Array.Empty<object>());
				}
				codeBuilder.Append(")", Array.Empty<object>());
			}
			else
			{
				codeBuilder.Append("()", Array.Empty<object>());
			}
			switch (num)
			{
			case 0:
				return new InvocationExpressionSyntaxNode0(constantExpression);
			case 1:
				return new InvocationExpressionSyntaxNode1(constantExpression, array[0]);
			case 2:
				return new InvocationExpressionSyntaxNode2(constantExpression, array[0], array[1]);
			default:
				return new InvocationExpressionSyntaxNodeN(constantExpression, array);
			}
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x000D4B08 File Offset: 0x000D2D08
		private IInvocationExpression ToFunctionInvocation(IExpression functionExpression, SegmentQueryToken segment, List<Microsoft.Data.Experimental.OData.Query.NamedValue> namedValueArgs, List<QueryOptionQueryToken> queryOptionArgs, ODataCompiler.CodeBuilder codeBuilder)
		{
			IExpression[] array = null;
			if (this.functionParams.Count > 0)
			{
				HashSet<string> hashSet = new HashSet<string>();
				Dictionary<string, QueryToken> dictionary = new Dictionary<string, QueryToken>(this.functionParams.Count);
				foreach (KeyValuePair<string, IEdmTypeReference> keyValuePair in this.functionParams)
				{
					dictionary.Add(keyValuePair.Key, null);
					if (keyValuePair.Value.IsNullable)
					{
						hashSet.Add(keyValuePair.Key);
					}
				}
				if (queryOptionArgs.Count > 0)
				{
					List<string> list = new List<string>();
					foreach (QueryOptionQueryToken queryOptionQueryToken in queryOptionArgs)
					{
						QueryToken queryToken;
						if (dictionary.TryGetValue(queryOptionQueryToken.Name, out queryToken))
						{
							if (queryToken != null)
							{
								throw new NotSupportedException();
							}
							dictionary[queryOptionQueryToken.Name] = queryOptionQueryToken;
						}
						else
						{
							list.Add(queryOptionQueryToken.Name);
						}
					}
					if (list.Count > 0)
					{
						throw new NotSupportedException();
					}
				}
				else if (namedValueArgs.Count > 0)
				{
					foreach (Microsoft.Data.Experimental.OData.Query.NamedValue namedValue in new List<Microsoft.Data.Experimental.OData.Query.NamedValue>(namedValueArgs))
					{
						QueryToken queryToken2;
						if (dictionary.TryGetValue(namedValue.Name, out queryToken2))
						{
							namedValueArgs.Remove(namedValue);
							if (queryToken2 != null)
							{
								throw new NotSupportedException();
							}
							dictionary[namedValue.Name] = namedValue.Value;
						}
					}
				}
				int count = dictionary.Count;
				codeBuilder.Append("(", Array.Empty<object>());
				array = new IExpression[count];
				for (int i = 0; i < count; i++)
				{
					string text = this.functionParamsIndex[i];
					QueryToken queryToken3 = dictionary[text];
					if (queryToken3 != null)
					{
						array[i] = this.ToExpression(queryToken3, codeBuilder);
					}
					else
					{
						if (!hashSet.Contains(text))
						{
							throw new NotSupportedException();
						}
						array[i] = ConstantExpressionSyntaxNode.Null;
						codeBuilder.Append("null", Array.Empty<object>());
					}
					if (i < count - 1)
					{
						codeBuilder.Append(", ", Array.Empty<object>());
					}
				}
				codeBuilder.Append(")", Array.Empty<object>());
			}
			else
			{
				if (queryOptionArgs != null && queryOptionArgs.Count > 0)
				{
					List<string> list2 = new List<string>();
					foreach (QueryOptionQueryToken queryOptionQueryToken2 in queryOptionArgs)
					{
						list2.Add(queryOptionQueryToken2.Name);
					}
					if (list2.Count > 0)
					{
						throw new NotSupportedException();
					}
				}
				codeBuilder.Append("()", Array.Empty<object>());
			}
			return ODataCompiler.ToFunctionInvocation(functionExpression, array);
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000D4DF8 File Offset: 0x000D2FF8
		private static IInvocationExpression ToFunctionInvocation(Value function, params IExpression[] arguments)
		{
			return ODataCompiler.ToFunctionInvocation(ODataCompiler.ToConstantExpression(function), arguments);
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000D4E06 File Offset: 0x000D3006
		private static IInvocationExpression ToFunctionInvocation(IExpression function, params IExpression[] arguments)
		{
			if (arguments == null || arguments.Length == 0)
			{
				return new InvocationExpressionSyntaxNode0(function);
			}
			if (arguments.Length == 1)
			{
				return new InvocationExpressionSyntaxNode1(function, arguments[0]);
			}
			if (arguments.Length == 2)
			{
				return new InvocationExpressionSyntaxNode2(function, arguments[0], arguments[1]);
			}
			return new InvocationExpressionSyntaxNodeN(function, arguments);
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000D4E40 File Offset: 0x000D3040
		private static IListExpression ToList(params IExpression[] elements)
		{
			return new ListExpressionSyntaxNode(elements);
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000D4E48 File Offset: 0x000D3048
		private static IExpression ToListTransformFunctionInvocation(IExpression collection, IEnumerable<ODataCompiler.PropertyAccess> propertyAccesses, ODataCompiler.CodeBuilder codeBuilder)
		{
			ODataCompiler.CodeBuilder codeBuilder2 = codeBuilder.CreateChild();
			IExpression expression = ODataCompiler.ToListTransformExpression(propertyAccesses, codeBuilder2);
			if (expression == null)
			{
				return collection;
			}
			codeBuilder.Wrap("Table.SelectColumns", codeBuilder2.ToString());
			return ODataCompiler.ToFunctionInvocation(Library.ListRuntime.Transform, new IExpression[]
			{
				collection,
				ODataCompiler.ToEachExpression(expression)
			});
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000D4E98 File Offset: 0x000D3098
		private static IExpression ToListTransformExpression(IEnumerable<ODataCompiler.PropertyAccess> propertyAccesses, ODataCompiler.CodeBuilder codeBuilder)
		{
			List<VariableInitializer> list = new List<VariableInitializer>();
			bool flag = false;
			codeBuilder.Append("{", Array.Empty<object>());
			bool flag2 = true;
			foreach (ODataCompiler.PropertyAccess propertyAccess in propertyAccesses)
			{
				if (!flag2)
				{
					codeBuilder.Append(", ", Array.Empty<object>());
				}
				else
				{
					flag2 = false;
				}
				VariableInitializer? variableInitializer = ODataCompiler.ToVariableInitializer(propertyAccess, codeBuilder);
				if (variableInitializer != null)
				{
					list.Add(variableInitializer.Value);
				}
				else if (propertyAccess.IsStar)
				{
					throw new NotSupportedException();
				}
			}
			codeBuilder.Append("}", Array.Empty<object>());
			if (list.Count == 0)
			{
				return null;
			}
			IExpression expression = new RecordExpressionSyntaxNode(list);
			if (flag)
			{
				return new ConcatenateBinaryExpressionSyntaxNode(ODataCompiler.ImplicitIdentifierExpression, expression);
			}
			return expression;
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x000D4F74 File Offset: 0x000D3174
		private IExpression ToSegmentExpression(Stack<SegmentQueryToken> resourcePath, ref IEdmTypeReference segmentTypeRef, List<QueryOptionQueryToken> paramTokens, ODataCompiler.CodeBuilder codeBuilder)
		{
			SegmentQueryToken segmentQueryToken = resourcePath.Pop();
			codeBuilder.AppendFeed();
			IExpression expression = ODataCompiler.ToConstantExpression(codeBuilder.FeedValue);
			List<Microsoft.Data.Experimental.OData.Query.NamedValue> list = new List<Microsoft.Data.Experimental.OData.Query.NamedValue>(segmentQueryToken.NamedValues ?? new Microsoft.Data.Experimental.OData.Query.NamedValue[0]);
			if (this.functionParams != null)
			{
				expression = this.ToFunctionInvocation(expression, segmentQueryToken, list, paramTokens, codeBuilder);
			}
			while (resourcePath.Count > 0)
			{
				SegmentQueryToken segmentQueryToken2 = resourcePath.Pop();
				segmentTypeRef = this.GetSegmentChildrenType(segmentTypeRef, segmentQueryToken.Name, segmentQueryToken2);
				segmentQueryToken = segmentQueryToken2;
				codeBuilder.Append("[{0}]", new object[] { segmentQueryToken.Name });
				expression = new RequiredFieldAccessExpressionSyntaxNode(expression, segmentQueryToken.Name);
				list.Clear();
				list.AddRange(segmentQueryToken.NamedValues ?? new Microsoft.Data.Experimental.OData.Query.NamedValue[0]);
			}
			return expression;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000D5038 File Offset: 0x000D3238
		private IInvocationExpression ToSelectFunctionInvocation(IExpression collection, QueryToken filter, IEdmTypeReference collectionType, ODataCompiler.CodeBuilder codeBuilder)
		{
			if (!collectionType.IsCollection())
			{
				throw new NotSupportedException();
			}
			ODataCompiler.CodeBuilder codeBuilder2 = codeBuilder.CreateChild();
			IExpression expression = this.ToExpression(filter, codeBuilder2);
			codeBuilder.Wrap("Table.SelectRows", "each " + codeBuilder2.ToString());
			return ODataCompiler.ToFunctionInvocation(TableModule.Table.SelectRows, new IExpression[]
			{
				collection,
				ODataCompiler.ToEachExpression(expression)
			});
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000D50A0 File Offset: 0x000D32A0
		private IListExpression ToSortCriteria(IEnumerable<OrderByQueryToken> orderByClauses, ODataCompiler.CodeBuilder codeBuilder)
		{
			codeBuilder.Append("{", Array.Empty<object>());
			List<IRecordExpression> list = new List<IRecordExpression>();
			bool flag = true;
			foreach (OrderByQueryToken orderByQueryToken in orderByClauses)
			{
				if (!flag)
				{
					codeBuilder.Append(", ", Array.Empty<object>());
				}
				else
				{
					flag = false;
				}
				list.Add(this.ToSortCriterion(orderByQueryToken, codeBuilder));
			}
			codeBuilder.Append("}", Array.Empty<object>());
			IExpression[] array = list.ToArray();
			return ODataCompiler.ToList(array);
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000D5140 File Offset: 0x000D3340
		private IRecordExpression ToSortCriterion(OrderByQueryToken orderByClause, ODataCompiler.CodeBuilder codeBuilder)
		{
			codeBuilder.Append("{ each ", Array.Empty<object>());
			IFunctionExpression functionExpression = ODataCompiler.ToEachExpression(this.ToExpression(orderByClause.Expression, codeBuilder));
			Value value;
			if (orderByClause.Direction == Microsoft.Data.Experimental.OData.Query.OrderByDirection.Descending)
			{
				codeBuilder.Append(", Order.Descending }", Array.Empty<object>());
				value = LogicalValue.False;
			}
			else
			{
				codeBuilder.Append(", Order.Ascending }", Array.Empty<object>());
				value = LogicalValue.True;
			}
			return new RecordExpressionSyntaxNode(new VariableInitializer[]
			{
				new VariableInitializer("KeySelector", functionExpression),
				new VariableInitializer("Ascending", ODataCompiler.ToConstantExpression(value))
			});
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x000D51E8 File Offset: 0x000D33E8
		private IInvocationExpression ToSortFunctionInvocation(IExpression collection, IEnumerable<OrderByQueryToken> orderByClauses, IEdmTypeReference collectionType, ODataCompiler.CodeBuilder codeBuilder)
		{
			if (!collectionType.IsCollection())
			{
				throw new NotSupportedException();
			}
			if (!(ODataCompiler.StripCollectionType(collectionType) is IEdmStructuredTypeReference))
			{
				throw new NotSupportedException();
			}
			ODataCompiler.CodeBuilder codeBuilder2 = codeBuilder.CreateChild();
			IExpression expression = this.ToSortCriteria(orderByClauses, codeBuilder2);
			codeBuilder.Wrap("Table.Sort", codeBuilder2.ToString());
			return ODataCompiler.ToFunctionInvocation(TableModule.Table.Sort, new IExpression[] { collection, expression });
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x000D5254 File Offset: 0x000D3454
		private static IEnumerable<ODataCompiler.PropertyAccess> ToPropertyAccesses(SelectQueryToken select, IEdmStructuredTypeReference structuredTypeRef)
		{
			Dictionary<string, ODataCompiler.PropertyAccess> dictionary = new Dictionary<string, ODataCompiler.PropertyAccess>();
			foreach (QueryToken queryToken in select.Properties)
			{
				QueryTokenKind kind = queryToken.Kind;
				if (kind != QueryTokenKind.PropertyAccess && kind != QueryTokenKind.Star)
				{
					throw new NotSupportedException();
				}
				ODataCompiler.PropertyAccess propertyAccess = ODataCompiler.PropertyAccess.Create(queryToken, structuredTypeRef);
				ODataCompiler.PropertyAccess propertyAccess2;
				if (dictionary.TryGetValue(propertyAccess.Name, out propertyAccess2))
				{
					propertyAccess2.Merge(propertyAccess);
				}
				else
				{
					dictionary.Add(propertyAccess.Name, propertyAccess);
				}
			}
			return dictionary.Values;
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x000D52F0 File Offset: 0x000D34F0
		private static IExpression ToTransformFunctionInvocation(IExpression collection, SelectQueryToken select, IEdmTypeReference typeRef, ODataCompiler.CodeBuilder codeBuilder)
		{
			IEdmStructuredTypeReference edmStructuredTypeReference = ODataCompiler.StripCollectionType(typeRef) as IEdmStructuredTypeReference;
			if (edmStructuredTypeReference == null)
			{
				throw new NotSupportedException();
			}
			IEnumerable<ODataCompiler.PropertyAccess> enumerable = ODataCompiler.ToPropertyAccesses(select, edmStructuredTypeReference);
			return ODataCompiler.ToTransformFunctionInvocation(collection, typeRef, enumerable, codeBuilder);
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x000D5323 File Offset: 0x000D3523
		private static IExpression ToTransformFunctionInvocation(IExpression collection, IEdmTypeReference typeRef, IEnumerable<ODataCompiler.PropertyAccess> propertyAccesses, ODataCompiler.CodeBuilder codeBuilder)
		{
			if (typeRef.IsCollection())
			{
				return ODataCompiler.ToListTransformFunctionInvocation(collection, propertyAccesses, codeBuilder);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x000D533C File Offset: 0x000D353C
		private IExpression ToValueExpression(object obj, ODataCompiler.CodeBuilder codeBuilder)
		{
			ODataCollectionValue odataCollectionValue = obj as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return this.ToValueExpressionFromList(odataCollectionValue, codeBuilder);
			}
			ODataComplexValue odataComplexValue = obj as ODataComplexValue;
			if (odataComplexValue != null)
			{
				return this.ToValueExpressionFromRecord(odataComplexValue, codeBuilder);
			}
			ODataProperty odataProperty = obj as ODataProperty;
			if (odataProperty != null)
			{
				return this.ToValueExpression(odataProperty.Value, codeBuilder);
			}
			return ODataCompiler.ToConstant(ODataTypeServices.MarshalFromClr(obj), codeBuilder);
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000D5394 File Offset: 0x000D3594
		private IListExpression ToValueExpressionFromList(ODataCollectionValue collectionValue, ODataCompiler.CodeBuilder codeBuilder)
		{
			codeBuilder.Append("{", Array.Empty<object>());
			bool flag = true;
			List<IExpression> list = new List<IExpression>();
			foreach (object obj in collectionValue.Items)
			{
				if (!flag)
				{
					codeBuilder.Append(", ", Array.Empty<object>());
				}
				else
				{
					flag = false;
				}
				list.Add(this.ToValueExpression(obj, codeBuilder));
			}
			codeBuilder.Append("}", Array.Empty<object>());
			return new ListExpressionSyntaxNode(list);
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x000D5438 File Offset: 0x000D3638
		private IRecordExpression ToValueExpressionFromRecord(ODataComplexValue complexValue, ODataCompiler.CodeBuilder codeBuilder)
		{
			codeBuilder.Append("[ ", Array.Empty<object>());
			bool flag = true;
			List<VariableInitializer> list = new List<VariableInitializer>();
			foreach (ODataProperty odataProperty in complexValue.Properties)
			{
				if (!flag)
				{
					codeBuilder.Append(", ", Array.Empty<object>());
				}
				else
				{
					flag = false;
				}
				codeBuilder.Append(odataProperty.Name + " = ", Array.Empty<object>());
				list.Add(new VariableInitializer(odataProperty.Name, this.ToValueExpression(odataProperty.Value, codeBuilder)));
			}
			codeBuilder.Append(" ]", Array.Empty<object>());
			return new RecordExpressionSyntaxNode(list);
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x000D5504 File Offset: 0x000D3704
		private static VariableInitializer? ToVariableInitializer(ODataCompiler.PropertyAccess pa, ODataCompiler.CodeBuilder codeBuilder)
		{
			if (pa.IsStar)
			{
				return null;
			}
			string name = pa.Name;
			codeBuilder.Append("\"{0}\"", new object[] { name });
			IExpression expression = new RequiredFieldAccessExpressionSyntaxNode(ODataCompiler.ImplicitIdentifierExpression, name);
			if (pa.Children.Count != 0)
			{
				throw new NotSupportedException();
			}
			return new VariableInitializer?(new VariableInitializer(name, expression));
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x000D5574 File Offset: 0x000D3774
		private IUnaryExpression ToUnary(UnaryOperatorQueryToken unaryOperation, ODataCompiler.CodeBuilder codeBuilder)
		{
			ODataCompiler.CodeBuilder codeBuilder2 = codeBuilder.CreateChild();
			IExpression expression = this.ToExpression(unaryOperation.Operand, codeBuilder2);
			Microsoft.Data.Experimental.OData.Query.UnaryOperatorKind operatorKind = unaryOperation.OperatorKind;
			if (operatorKind == Microsoft.Data.Experimental.OData.Query.UnaryOperatorKind.Negate)
			{
				codeBuilder.Append("-({0})", new object[] { codeBuilder2.ToString() });
				return new NegativeUnaryExpressionSyntaxNode(expression);
			}
			if (operatorKind != Microsoft.Data.Experimental.OData.Query.UnaryOperatorKind.Not)
			{
				throw new NotSupportedException();
			}
			codeBuilder.Append("not({0})", new object[] { codeBuilder2.ToString() });
			return new NotUnaryExpressionSyntaxNode(expression);
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x000D55F0 File Offset: 0x000D37F0
		private static bool TryGetBuiltInFunction(string odataName, out string functionName, out IConstantExpression function)
		{
			function = null;
			Value value;
			if (ODataCompiler.BuiltInFunctionNames.TryGetValue(odataName, out functionName) && LibraryHelper.StandardLibrary.TryGetValue(functionName, out value))
			{
				function = ODataCompiler.ToConstantExpression(value);
				return true;
			}
			return false;
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x000D562C File Offset: 0x000D382C
		private static bool VerifySegmentsHaveNoLookup(QueryToken path)
		{
			if (path == null)
			{
				return true;
			}
			if (path.Kind == QueryTokenKind.Segment)
			{
				SegmentQueryToken segmentQueryToken = (SegmentQueryToken)path;
				return (segmentQueryToken.NamedValues == null || segmentQueryToken.NamedValues.Count<Microsoft.Data.Experimental.OData.Query.NamedValue>() <= 0) && ODataCompiler.VerifySegmentsHaveNoLookup(segmentQueryToken.Parent);
			}
			return false;
		}

		// Token: 0x04002180 RID: 8576
		private static readonly Dictionary<string, string> BuiltInFunctionNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{ "length", "Text.Length" },
			{ "endswith", "Text.EndsWith" },
			{ "startswith", "Text.StartsWith" },
			{ "indexof", "Text.PositionOf" },
			{ "replace", "Text.Replace" },
			{ "substring", "Text.Range" },
			{ "substringof", "Text.Contains" },
			{ "tolower", "Text.Lower" },
			{ "toupper", "Text.Upper" },
			{ "trim", "Text.Trim" },
			{ "concat", "Text.Combine" },
			{ "day", "Date.Day" },
			{ "month", "Date.Month" },
			{ "year", "Date.Year" },
			{ "hour", "Time.Hour" },
			{ "minute", "Time.Minute" },
			{ "second", "Time.Second" },
			{ "round", "Number.Round" },
			{ "floor", "Number.RoundDown" },
			{ "ceiling", "Number.RoundUp" }
		};

		// Token: 0x04002181 RID: 8577
		private static readonly IFunctionTypeExpression ImplicitFunctionType = new FunctionTypeSyntaxNode(null, new IParameter[]
		{
			new ParameterSyntaxNode(Identifier.Underscore, null)
		}, 1);

		// Token: 0x04002182 RID: 8578
		private static readonly IIdentifierExpression ImplicitIdentifierExpression = new ExclusiveIdentifierExpressionSyntaxNode("_");

		// Token: 0x04002183 RID: 8579
		private readonly QueryDescriptorQueryToken queryToken;

		// Token: 0x04002184 RID: 8580
		private readonly IEdmTypeReference dataSourceTypeRef;

		// Token: 0x04002185 RID: 8581
		private readonly ODataVersion odataVersion;

		// Token: 0x04002186 RID: 8582
		private readonly Dictionary<string, IEdmTypeReference> functionParams;

		// Token: 0x04002187 RID: 8583
		private readonly Dictionary<int, string> functionParamsIndex;

		// Token: 0x020008BD RID: 2237
		private sealed class CodeBuilder
		{
			// Token: 0x06004001 RID: 16385 RVA: 0x000D5804 File Offset: 0x000D3A04
			public CodeBuilder(Value feedValue)
			{
				this.feedValue = feedValue;
			}

			// Token: 0x170014A9 RID: 5289
			// (get) Token: 0x06004002 RID: 16386 RVA: 0x000D5830 File Offset: 0x000D3A30
			public Value FeedValue
			{
				get
				{
					return this.feedValue;
				}
			}

			// Token: 0x06004003 RID: 16387 RVA: 0x000D5838 File Offset: 0x000D3A38
			public void Append(string format, params object[] args)
			{
				if (args.Length == 0)
				{
					this.builder.Append(format);
					return;
				}
				this.builder.AppendFormat(CultureInfo.InvariantCulture, format, args);
			}

			// Token: 0x06004004 RID: 16388 RVA: 0x000D5860 File Offset: 0x000D3A60
			public void AppendFeed()
			{
				this.currentLetVariableIndex++;
				this.currentLetVariableName = "Feed" + this.currentLetVariableIndex.ToString();
				this.Append("let {0} = {1}", new object[] { this.currentLetVariableName, "OData.Feed.Internal" });
			}

			// Token: 0x06004005 RID: 16389 RVA: 0x000D58B8 File Offset: 0x000D3AB8
			public ODataCompiler.CodeBuilder CreateChild()
			{
				return new ODataCompiler.CodeBuilder(this.feedValue);
			}

			// Token: 0x06004006 RID: 16390 RVA: 0x000D58C8 File Offset: 0x000D3AC8
			private static string GetFunctionVariableName(string functionName)
			{
				StringBuilder stringBuilder = new StringBuilder(functionName);
				for (int i = 0; i < stringBuilder.Length; i++)
				{
					if (!char.IsLetterOrDigit(stringBuilder[i]))
					{
						stringBuilder[i] = '_';
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06004007 RID: 16391 RVA: 0x000D590A File Offset: 0x000D3B0A
			public override string ToString()
			{
				return this.builder.ToString() + ((this.currentLetVariableIndex >= 0) ? (" in " + this.currentLetVariableName) : string.Empty);
			}

			// Token: 0x06004008 RID: 16392 RVA: 0x000D593C File Offset: 0x000D3B3C
			public void Wrap(string functionName, string functionArgs)
			{
				this.currentLetVariableIndex++;
				string text = this.currentLetVariableName;
				this.currentLetVariableName = ODataCompiler.CodeBuilder.GetFunctionVariableName(functionName) + this.currentLetVariableIndex.ToString();
				this.Append(", {0} = {1}({2}, {3})", new object[] { this.currentLetVariableName, functionName, text, functionArgs });
			}

			// Token: 0x04002188 RID: 8584
			public const string ODataFeedInternalIdentifier = "OData.Feed.Internal";

			// Token: 0x04002189 RID: 8585
			private const string FunctionCallFormat = ", {0} = {1}({2}, {3})";

			// Token: 0x0400218A RID: 8586
			private readonly StringBuilder builder = new StringBuilder();

			// Token: 0x0400218B RID: 8587
			private readonly Value feedValue;

			// Token: 0x0400218C RID: 8588
			private int currentLetVariableIndex = -1;

			// Token: 0x0400218D RID: 8589
			private string currentLetVariableName = string.Empty;
		}

		// Token: 0x020008BE RID: 2238
		private sealed class PropertyAccess
		{
			// Token: 0x06004009 RID: 16393 RVA: 0x000D59A0 File Offset: 0x000D3BA0
			private PropertyAccess(string name, bool isStar)
			{
				this.name = name;
				this.isStar = isStar;
			}

			// Token: 0x0600400A RID: 16394 RVA: 0x000D59C1 File Offset: 0x000D3BC1
			public PropertyAccess(string name, IEdmTypeReference type)
			{
				this.name = name;
				this.isStar = false;
				this.type = type;
			}

			// Token: 0x170014AA RID: 5290
			// (get) Token: 0x0600400B RID: 16395 RVA: 0x000D59E9 File Offset: 0x000D3BE9
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170014AB RID: 5291
			// (get) Token: 0x0600400C RID: 16396 RVA: 0x000D59F1 File Offset: 0x000D3BF1
			public bool IsStar
			{
				get
				{
					return this.isStar;
				}
			}

			// Token: 0x170014AC RID: 5292
			// (get) Token: 0x0600400D RID: 16397 RVA: 0x000D59F9 File Offset: 0x000D3BF9
			public IDictionary<string, ODataCompiler.PropertyAccess> Children
			{
				get
				{
					return this.children;
				}
			}

			// Token: 0x170014AD RID: 5293
			// (get) Token: 0x0600400E RID: 16398 RVA: 0x000D5A01 File Offset: 0x000D3C01
			public IEdmTypeReference Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x0600400F RID: 16399 RVA: 0x000D5A0C File Offset: 0x000D3C0C
			public static ODataCompiler.PropertyAccess Create(QueryToken token, IEdmTypeReference tokenType)
			{
				ODataCompiler.PropertyAccess propertyAccess = null;
				return ODataCompiler.PropertyAccess.Create(token, tokenType, out propertyAccess);
			}

			// Token: 0x06004010 RID: 16400 RVA: 0x000D5A24 File Offset: 0x000D3C24
			private static ODataCompiler.PropertyAccess Create(QueryToken token, IEdmTypeReference parentTypeRef, out ODataCompiler.PropertyAccess tokenPropAccess)
			{
				ODataCompiler.PropertyAccess propertyAccess;
				QueryToken queryToken;
				if (token.Kind == QueryTokenKind.Star)
				{
					propertyAccess = new ODataCompiler.PropertyAccess("*", true);
					queryToken = ((StarQueryToken)token).Parent;
				}
				else
				{
					if (token.Kind != QueryTokenKind.PropertyAccess)
					{
						throw new NotSupportedException();
					}
					PropertyAccessQueryToken propertyAccessQueryToken = (PropertyAccessQueryToken)token;
					propertyAccess = new ODataCompiler.PropertyAccess(propertyAccessQueryToken.Name, false);
					queryToken = propertyAccessQueryToken.Parent;
				}
				if (queryToken == null)
				{
					if (!propertyAccess.IsStar)
					{
						propertyAccess.SetTypeFromParentType(parentTypeRef);
					}
					tokenPropAccess = propertyAccess;
					return propertyAccess;
				}
				if (queryToken.Kind == QueryTokenKind.Star)
				{
					throw new NotSupportedException();
				}
				ODataCompiler.PropertyAccess propertyAccess2 = null;
				ODataCompiler.PropertyAccess propertyAccess3 = ODataCompiler.PropertyAccess.Create(queryToken, parentTypeRef, out propertyAccess2);
				if (propertyAccess.Name == "*")
				{
					propertyAccess2.Children.Clear();
				}
				else
				{
					propertyAccess.SetTypeFromParentType(propertyAccess2.Type);
					propertyAccess2.Children.Add(propertyAccess.Name, propertyAccess);
				}
				tokenPropAccess = propertyAccess;
				return propertyAccess3;
			}

			// Token: 0x06004011 RID: 16401 RVA: 0x000D5AF8 File Offset: 0x000D3CF8
			public void Merge(ODataCompiler.PropertyAccess propAccess)
			{
				if (this.Children.Count == 0)
				{
					return;
				}
				if (propAccess.Children.Count == 0)
				{
					this.Children.Clear();
					return;
				}
				foreach (ODataCompiler.PropertyAccess propertyAccess in propAccess.Children.Values)
				{
					ODataCompiler.PropertyAccess propertyAccess2;
					if (this.Children.TryGetValue(propertyAccess.Name, out propertyAccess2))
					{
						propertyAccess2.Merge(propertyAccess);
					}
					else
					{
						this.Children.Add(propertyAccess.Name, propertyAccess);
					}
				}
			}

			// Token: 0x06004012 RID: 16402 RVA: 0x000D5B9C File Offset: 0x000D3D9C
			private void SetTypeFromParentType(IEdmTypeReference parentTypeRef)
			{
				IEdmStructuredTypeReference structuredType = ODataCompiler.GetStructuredType(parentTypeRef);
				this.type = null;
				if (structuredType == null)
				{
					return;
				}
				IEdmProperty edmProperty = structuredType.FindProperty(this.Name);
				if (edmProperty != null)
				{
					this.type = edmProperty.Type;
					return;
				}
				throw new NotSupportedException();
			}

			// Token: 0x0400218E RID: 8590
			private readonly bool isStar;

			// Token: 0x0400218F RID: 8591
			private readonly string name;

			// Token: 0x04002190 RID: 8592
			private IEdmTypeReference type;

			// Token: 0x04002191 RID: 8593
			private readonly Dictionary<string, ODataCompiler.PropertyAccess> children = new Dictionary<string, ODataCompiler.PropertyAccess>();
		}
	}
}
