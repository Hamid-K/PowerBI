using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000126 RID: 294
	internal sealed class FunctionCallBinder : BinderBase
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x000275F3 File Offset: 0x000257F3
		internal FunctionCallBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
			: base(bindMethod, state)
		{
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00028058 File Offset: 0x00026258
		internal static void TypePromoteArguments(FunctionSignatureWithReturnType signature, List<QueryNode> argumentNodes)
		{
			for (int i = 0; i < argumentNodes.Count; i++)
			{
				SingleValueNode singleValueNode = (SingleValueNode)argumentNodes[i];
				IEdmTypeReference edmTypeReference = signature.ArgumentTypes[i];
				argumentNodes[i] = MetadataBindingUtils.ConvertToTypeIfNeeded(singleValueNode, edmTypeReference);
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0002809C File Offset: 0x0002629C
		internal static SingleValueNode[] ValidateArgumentsAreSingleValue(string functionName, List<QueryNode> argumentNodes)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(functionName, "functionCallToken");
			ExceptionUtils.CheckArgumentNotNull<List<QueryNode>>(argumentNodes, "argumentNodes");
			SingleValueNode[] array = new SingleValueNode[argumentNodes.Count];
			for (int i = 0; i < argumentNodes.Count; i++)
			{
				SingleValueNode singleValueNode = argumentNodes[i] as SingleValueNode;
				if (singleValueNode == null)
				{
					throw new ODataException(Strings.MetadataBinder_FunctionArgumentNotSingleValue(functionName));
				}
				array[i] = singleValueNode;
			}
			return array;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00028100 File Offset: 0x00026300
		internal static KeyValuePair<string, FunctionSignatureWithReturnType> MatchSignatureToUriFunction(string functionCallToken, SingleValueNode[] argumentNodes, IList<KeyValuePair<string, FunctionSignatureWithReturnType>> nameSignatures)
		{
			IEdmTypeReference[] array = argumentNodes.Select((SingleValueNode s) => s.TypeReference).ToArray<IEdmTypeReference>();
			int argumentCount = array.Length;
			KeyValuePair<string, FunctionSignatureWithReturnType> keyValuePair2;
			if (array.All((IEdmTypeReference a) => a == null) && argumentCount > 0)
			{
				KeyValuePair<string, FunctionSignatureWithReturnType> keyValuePair = nameSignatures.FirstOrDefault((KeyValuePair<string, FunctionSignatureWithReturnType> pair) => pair.Value.ArgumentTypes.Count<IEdmTypeReference>() == argumentCount);
				if (keyValuePair.Equals(TypePromotionUtils.NotFoundKeyValuePair))
				{
					throw new ODataException(Strings.FunctionCallBinder_CannotFindASuitableOverload(functionCallToken, array.Count<IEdmTypeReference>()));
				}
				keyValuePair2 = new KeyValuePair<string, FunctionSignatureWithReturnType>(keyValuePair.Key, new FunctionSignatureWithReturnType(null, keyValuePair.Value.ArgumentTypes));
			}
			else
			{
				keyValuePair2 = TypePromotionUtils.FindBestFunctionSignature(nameSignatures, argumentNodes, functionCallToken);
				if (keyValuePair2.Equals(TypePromotionUtils.NotFoundKeyValuePair))
				{
					throw new ODataException(Strings.MetadataBinder_NoApplicableFunctionFound(functionCallToken, UriFunctionsHelper.BuildFunctionSignatureListDescription(functionCallToken, nameSignatures.Select((KeyValuePair<string, FunctionSignatureWithReturnType> sig) => sig.Value))));
				}
			}
			return keyValuePair2;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00028238 File Offset: 0x00026438
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "need to use lower characters for built-in functions.")]
		internal static IList<KeyValuePair<string, FunctionSignatureWithReturnType>> GetUriFunctionSignatures(string functionCallToken, bool enableCaseInsensitive = false)
		{
			IList<KeyValuePair<string, FunctionSignatureWithReturnType>> list = null;
			FunctionSignatureWithReturnType[] array = null;
			IList<KeyValuePair<string, FunctionSignatureWithReturnType>> list2 = null;
			bool flag = CustomUriFunctions.TryGetCustomFunction(functionCallToken, out list, enableCaseInsensitive);
			string nameKey = (enableCaseInsensitive ? functionCallToken.ToLowerInvariant() : functionCallToken);
			bool flag2 = BuiltInUriFunctions.TryGetBuiltInFunction(nameKey, out array);
			if (flag2)
			{
				list2 = array.Select((FunctionSignatureWithReturnType sig) => new KeyValuePair<string, FunctionSignatureWithReturnType>(nameKey, sig)).ToList<KeyValuePair<string, FunctionSignatureWithReturnType>>();
			}
			if (!flag && !flag2)
			{
				throw new ODataException(Strings.MetadataBinder_UnknownFunction(functionCallToken));
			}
			if (!flag)
			{
				return list2;
			}
			if (!flag2)
			{
				return list;
			}
			return list2.Concat(list).ToArray<KeyValuePair<string, FunctionSignatureWithReturnType>>();
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000282C5 File Offset: 0x000264C5
		internal static FunctionSignatureWithReturnType[] ExtractSignatures(IList<KeyValuePair<string, FunctionSignatureWithReturnType>> nameSignatures)
		{
			return nameSignatures.Select((KeyValuePair<string, FunctionSignatureWithReturnType> nameSig) => nameSig.Value).ToArray<FunctionSignatureWithReturnType>();
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000282F4 File Offset: 0x000264F4
		internal QueryNode BindFunctionCall(FunctionCallToken functionCallToken)
		{
			ExceptionUtils.CheckArgumentNotNull<FunctionCallToken>(functionCallToken, "functionCallToken");
			ExceptionUtils.CheckArgumentNotNull<string>(functionCallToken.Name, "functionCallToken.Name");
			QueryNode queryNode = null;
			if (this.state.ImplicitRangeVariable != null)
			{
				if (functionCallToken.Source != null)
				{
					queryNode = this.bindMethod(functionCallToken.Source);
				}
				else
				{
					queryNode = NodeFactory.CreateRangeVariableReferenceNode(this.state.ImplicitRangeVariable);
				}
			}
			QueryNode queryNode2;
			if (this.TryBindIdentifier(functionCallToken.Name, functionCallToken.Arguments, queryNode, this.state, out queryNode2))
			{
				return queryNode2;
			}
			if (this.TryBindIdentifier(functionCallToken.Name, functionCallToken.Arguments, null, this.state, out queryNode2))
			{
				return queryNode2;
			}
			List<QueryNode> list = new List<QueryNode>(functionCallToken.Arguments.Select((FunctionParameterToken ar) => this.bindMethod(ar)));
			return this.BindAsUriFunction(functionCallToken, list);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x000283BD File Offset: 0x000265BD
		internal bool TryBindEndPathAsFunctionCall(EndPathToken endPathToken, QueryNode parent, BindingState state, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(endPathToken.Identifier, null, parent, state, out boundFunction);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000283D0 File Offset: 0x000265D0
		internal bool TryBindInnerPathAsFunctionCall(InnerPathToken innerPathToken, QueryNode parent, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(innerPathToken.Identifier, null, parent, this.state, out boundFunction);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x000283D0 File Offset: 0x000265D0
		internal bool TryBindDottedIdentifierAsFunctionCall(DottedIdentifierToken dottedIdentifierToken, SingleValueNode parent, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(dottedIdentifierToken.Identifier, null, parent, this.state, out boundFunction);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000283E8 File Offset: 0x000265E8
		private QueryNode BindAsUriFunction(FunctionCallToken functionCallToken, List<QueryNode> argumentNodes)
		{
			if (functionCallToken.Source != null)
			{
				throw new ODataException(Strings.FunctionCallBinder_UriFunctionMustHaveHaveNullParent(functionCallToken.Name));
			}
			string text = this.IsUnboundFunction(functionCallToken.Name);
			if (text != null)
			{
				return this.CreateUnboundFunctionNode(text, argumentNodes);
			}
			IList<KeyValuePair<string, FunctionSignatureWithReturnType>> uriFunctionSignatures = FunctionCallBinder.GetUriFunctionSignatures(functionCallToken.Name, this.state.Configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			SingleValueNode[] array = FunctionCallBinder.ValidateArgumentsAreSingleValue(functionCallToken.Name, argumentNodes);
			KeyValuePair<string, FunctionSignatureWithReturnType> keyValuePair = FunctionCallBinder.MatchSignatureToUriFunction(functionCallToken.Name, array, uriFunctionSignatures);
			string key = keyValuePair.Key;
			FunctionSignatureWithReturnType value = keyValuePair.Value;
			if (value.ReturnType != null)
			{
				FunctionCallBinder.TypePromoteArguments(value, argumentNodes);
			}
			if (value.ReturnType != null && value.ReturnType.IsStructured())
			{
				return new SingleResourceFunctionCallNode(key, new ReadOnlyCollection<QueryNode>(argumentNodes), value.ReturnType.AsStructured(), null);
			}
			return new SingleValueFunctionCallNode(key, new ReadOnlyCollection<QueryNode>(argumentNodes), value.ReturnType);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000284C8 File Offset: 0x000266C8
		private bool TryBindIdentifier(string identifier, IEnumerable<FunctionParameterToken> arguments, QueryNode parent, BindingState state, out QueryNode boundFunction)
		{
			boundFunction = null;
			IEdmType edmType = null;
			SingleValueNode singleValueNode = parent as SingleValueNode;
			if (singleValueNode != null)
			{
				if (singleValueNode.TypeReference != null)
				{
					edmType = singleValueNode.TypeReference.Definition;
				}
			}
			else
			{
				CollectionNode collectionNode = parent as CollectionNode;
				if (collectionNode != null)
				{
					edmType = collectionNode.CollectionType.Definition;
				}
			}
			if (!UriEdmHelpers.IsBindingTypeValid(edmType))
			{
				return false;
			}
			if (identifier.IndexOf(".", StringComparison.Ordinal) == -1 && base.Resolver.GetType() == typeof(ODataUriResolver))
			{
				return false;
			}
			List<FunctionParameterToken> list = ((arguments == null) ? new List<FunctionParameterToken>() : arguments.ToList<FunctionParameterToken>());
			IEdmOperation edmOperation;
			if (!FunctionOverloadResolver.ResolveOperationFromList(identifier, list.Select((FunctionParameterToken ar) => ar.ParameterName).ToList<string>(), edmType, state.Model, out edmOperation, base.Resolver))
			{
				return false;
			}
			if (singleValueNode != null && singleValueNode.TypeReference == null)
			{
				throw new ODataException(Strings.FunctionCallBinder_CallingFunctionOnOpenProperty(identifier));
			}
			if (edmOperation.IsAction())
			{
				return false;
			}
			IEdmFunction edmFunction = (IEdmFunction)edmOperation;
			ICollection<FunctionParameterToken> collection = FunctionCallBinder.HandleComplexOrCollectionParameterValueIfExists(state.Configuration.Model, edmFunction, list, state.Configuration.Resolver.EnableCaseInsensitive, false);
			IEnumerable<QueryNode> enumerable = collection.Select((FunctionParameterToken p) => this.bindMethod(p));
			enumerable = enumerable.ToList<QueryNode>();
			IEdmTypeReference returnType = edmFunction.ReturnType;
			IEdmEntitySetBase edmEntitySetBase = null;
			SingleResourceNode singleResourceNode = parent as SingleResourceNode;
			if (singleResourceNode != null)
			{
				edmEntitySetBase = edmFunction.GetTargetEntitySet(singleResourceNode.NavigationSource, state.Model);
			}
			string text = edmFunction.FullName();
			if (returnType.IsEntity())
			{
				boundFunction = new SingleResourceFunctionCallNode(text, new IEdmFunction[] { edmFunction }, enumerable, (IEdmEntityTypeReference)returnType.Definition.ToTypeReference(), edmEntitySetBase, parent);
			}
			else if (returnType.IsStructuredCollection())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = (IEdmCollectionTypeReference)returnType;
				boundFunction = new CollectionResourceFunctionCallNode(text, new IEdmFunction[] { edmFunction }, enumerable, edmCollectionTypeReference, edmEntitySetBase, parent);
			}
			else if (returnType.IsCollection())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference2 = (IEdmCollectionTypeReference)returnType;
				boundFunction = new CollectionFunctionCallNode(text, new IEdmFunction[] { edmFunction }, enumerable, edmCollectionTypeReference2, parent);
			}
			else
			{
				boundFunction = new SingleValueFunctionCallNode(text, new IEdmFunction[] { edmFunction }, enumerable, returnType, parent);
			}
			return true;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000286F4 File Offset: 0x000268F4
		internal static List<OperationSegmentParameter> BindSegmentParameters(ODataUriParserConfiguration configuration, IEdmOperation functionOrOpertion, ICollection<FunctionParameterToken> segmentParameterTokens)
		{
			ICollection<FunctionParameterToken> collection = FunctionCallBinder.HandleComplexOrCollectionParameterValueIfExists(configuration.Model, functionOrOpertion, segmentParameterTokens, configuration.Resolver.EnableCaseInsensitive, configuration.EnableUriTemplateParsing);
			BindingState bindingState = new BindingState(configuration);
			bindingState.ImplicitRangeVariable = null;
			bindingState.RangeVariables.Clear();
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			List<OperationSegmentParameter> list = new List<OperationSegmentParameter>();
			IDictionary<string, SingleValueNode> dictionary = new Dictionary<string, SingleValueNode>(StringComparer.Ordinal);
			foreach (FunctionParameterToken functionParameterToken in collection)
			{
				if (functionParameterToken.ValueToken is EndPathToken)
				{
					throw new ODataException(Strings.MetadataBinder_ParameterNotInScope(string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[]
					{
						functionParameterToken.ParameterName,
						(functionParameterToken.ValueToken as EndPathToken).Identifier
					})));
				}
				SingleValueNode singleValueNode = (SingleValueNode)metadataBinder.Bind(functionParameterToken.ValueToken);
				if (!dictionary.ContainsKey(functionParameterToken.ParameterName))
				{
					dictionary.Add(functionParameterToken.ParameterName, singleValueNode);
				}
			}
			IDictionary<IEdmOperationParameter, SingleValueNode> dictionary2 = configuration.Resolver.ResolveOperationParameters(functionOrOpertion, dictionary);
			foreach (KeyValuePair<IEdmOperationParameter, SingleValueNode> keyValuePair in dictionary2)
			{
				SingleValueNode singleValueNode2 = keyValuePair.Value;
				IEdmTypeReference edmTypeReference = singleValueNode2.GetEdmTypeReference();
				if (edmTypeReference != null && !FunctionCallBinder.TryRewriteIntegralConstantNode(ref singleValueNode2, keyValuePair.Key.Type))
				{
					singleValueNode2 = MetadataBindingUtils.ConvertToTypeIfNeeded(singleValueNode2, keyValuePair.Key.Type);
				}
				OperationSegmentParameter operationSegmentParameter = new OperationSegmentParameter(keyValuePair.Key.Name, singleValueNode2);
				list.Add(operationSegmentParameter);
			}
			return list;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x000288C0 File Offset: 0x00026AC0
		private static bool TryRewriteIntegralConstantNode(ref SingleValueNode boundNode, IEdmTypeReference targetType)
		{
			if (targetType == null || (!targetType.IsByte() && !targetType.IsSByte() && !targetType.IsInt16()))
			{
				return false;
			}
			ConstantNode constantNode = boundNode as ConstantNode;
			if (constantNode == null)
			{
				return false;
			}
			IEdmTypeReference typeReference = constantNode.TypeReference;
			if (typeReference == null || !typeReference.IsInt32())
			{
				return false;
			}
			int num = (int)constantNode.Value;
			object obj = null;
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = targetType.PrimitiveKind();
			if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.Byte)
			{
				if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.Int16)
				{
					if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.SByte)
					{
						if (num >= -128 && num <= 127)
						{
							obj = (sbyte)num;
						}
					}
				}
				else if (num >= -32768 && num <= 32767)
				{
					obj = (short)num;
				}
			}
			else if (num >= 0 && num <= 255)
			{
				obj = (byte)num;
			}
			if (obj == null)
			{
				return false;
			}
			boundNode = new ConstantNode(obj, constantNode.LiteralText, targetType);
			return true;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0002898C File Offset: 0x00026B8C
		private static ICollection<FunctionParameterToken> HandleComplexOrCollectionParameterValueIfExists(IEdmModel model, IEdmOperation operation, ICollection<FunctionParameterToken> parameterTokens, bool enableCaseInsensitive, bool enableUriTemplateParsing = false)
		{
			ICollection<FunctionParameterToken> collection = new Collection<FunctionParameterToken>();
			foreach (FunctionParameterToken functionParameterToken in parameterTokens)
			{
				IEdmOperationParameter edmOperationParameter = operation.FindParameter(functionParameterToken.ParameterName);
				FunctionParameterToken functionParameterToken2;
				if (enableCaseInsensitive && edmOperationParameter == null)
				{
					edmOperationParameter = ODataUriResolver.ResolveOperationParameterNameCaseInsensitive(operation, functionParameterToken.ParameterName);
					functionParameterToken2 = new FunctionParameterToken(edmOperationParameter.Name, functionParameterToken.ValueToken);
				}
				else
				{
					functionParameterToken2 = functionParameterToken;
				}
				FunctionParameterAliasToken functionParameterAliasToken = functionParameterToken2.ValueToken as FunctionParameterAliasToken;
				if (functionParameterAliasToken != null)
				{
					functionParameterAliasToken.ExpectedParameterType = edmOperationParameter.Type;
				}
				LiteralToken literalToken = functionParameterToken2.ValueToken as LiteralToken;
				string text;
				if (literalToken != null && (text = literalToken.Value as string) != null && !string.IsNullOrEmpty(literalToken.OriginalText))
				{
					ExpressionLexer expressionLexer = new ExpressionLexer(literalToken.OriginalText, true, false, true);
					if (expressionLexer.CurrentToken.Kind == ExpressionTokenKind.BracketedExpression || expressionLexer.CurrentToken.Kind == ExpressionTokenKind.BracedExpression)
					{
						UriTemplateExpression uriTemplateExpression;
						object obj;
						if (enableUriTemplateParsing && UriTemplateParser.TryParseLiteral(expressionLexer.CurrentToken.Text, edmOperationParameter.Type, out uriTemplateExpression))
						{
							obj = uriTemplateExpression;
						}
						else
						{
							if (edmOperationParameter.Type.IsStructured() || edmOperationParameter.Type.IsStructuredCollectionType())
							{
								collection.Add(functionParameterToken2);
								continue;
							}
							obj = ODataUriUtils.ConvertFromUriLiteral(text, ODataVersion.V4, model, edmOperationParameter.Type);
						}
						LiteralToken literalToken2 = new LiteralToken(obj, literalToken.OriginalText);
						FunctionParameterToken functionParameterToken3 = new FunctionParameterToken(functionParameterToken2.ParameterName, literalToken2);
						collection.Add(functionParameterToken3);
						continue;
					}
				}
				collection.Add(functionParameterToken2);
			}
			return collection;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00028B40 File Offset: 0x00026D40
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "need to use lower characters for unbound functions.")]
		private string IsUnboundFunction(string functionName)
		{
			functionName = (this.state.Configuration.EnableCaseInsensitiveUriFunctionIdentifier ? functionName.ToLowerInvariant() : functionName);
			return FunctionCallBinder.UnboundFunctionNames.FirstOrDefault((string name) => name.Equals(functionName, StringComparison.Ordinal));
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00028B9C File Offset: 0x00026D9C
		private SingleValueNode CreateUnboundFunctionNode(string functionCallTokenName, List<QueryNode> args)
		{
			IEdmTypeReference edmTypeReference = null;
			if (!(functionCallTokenName == "isof"))
			{
				if (functionCallTokenName == "cast")
				{
					edmTypeReference = FunctionCallBinder.ValidateAndBuildCastArgs(this.state, ref args);
					if (edmTypeReference.IsStructured())
					{
						SingleResourceNode singleResourceNode = args.ElementAt(0) as SingleResourceNode;
						return new SingleResourceFunctionCallNode(functionCallTokenName, args, edmTypeReference.AsStructured(), (singleResourceNode != null) ? singleResourceNode.NavigationSource : null);
					}
				}
			}
			else
			{
				edmTypeReference = FunctionCallBinder.ValidateAndBuildIsOfArgs(this.state, ref args);
			}
			return new SingleValueFunctionCallNode(functionCallTokenName, args, edmTypeReference);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00028C1C File Offset: 0x00026E1C
		private static IEdmTypeReference ValidateAndBuildCastArgs(BindingState state, ref List<QueryNode> args)
		{
			return FunctionCallBinder.ValidateIsOfOrCast(state, true, ref args);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00028C26 File Offset: 0x00026E26
		private static IEdmTypeReference ValidateAndBuildIsOfArgs(BindingState state, ref List<QueryNode> args)
		{
			return FunctionCallBinder.ValidateIsOfOrCast(state, false, ref args);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00028C30 File Offset: 0x00026E30
		private static IEdmTypeReference ValidateIsOfOrCast(BindingState state, bool isCast, ref List<QueryNode> args)
		{
			if (args.Count != 1 && args.Count != 2)
			{
				throw new ODataErrorException(Strings.MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands(args.Count));
			}
			ConstantNode constantNode = args.Last<QueryNode>() as ConstantNode;
			IEdmTypeReference edmTypeReference = null;
			if (constantNode != null)
			{
				edmTypeReference = FunctionCallBinder.TryGetTypeReference(state.Model, constantNode.Value as string, state.Configuration.Resolver);
			}
			if (edmTypeReference == null)
			{
				throw new ODataException(Strings.MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument);
			}
			if (edmTypeReference.IsCollection())
			{
				throw new ODataException(Strings.MetadataBinder_CastOrIsOfCollectionsNotSupported);
			}
			if (args.Count == 1)
			{
				args = new List<QueryNode>
				{
					new ResourceRangeVariableReferenceNode(state.ImplicitRangeVariable.Name, state.ImplicitRangeVariable as ResourceRangeVariable),
					args[0]
				};
			}
			else if (!(args[0] is SingleValueNode))
			{
				throw new ODataException(Strings.MetadataBinder_CastOrIsOfCollectionsNotSupported);
			}
			if (isCast && args.Count == 2)
			{
				if (args[0].GetEdmTypeReference() is IEdmEnumTypeReference && !string.Equals(constantNode.Value as string, "Edm.String", StringComparison.Ordinal))
				{
					throw new ODataException(Strings.CastBinder_EnumOnlyCastToOrFromString);
				}
				if (edmTypeReference is IEdmEnumTypeReference)
				{
					IEdmTypeReference edmTypeReference2 = args[0].GetEdmTypeReference();
					if (edmTypeReference2 != null)
					{
						IEdmPrimitiveTypeReference edmPrimitiveTypeReference = edmTypeReference2 as IEdmPrimitiveTypeReference;
						if (edmPrimitiveTypeReference != null)
						{
							IEdmPrimitiveType edmPrimitiveType = edmPrimitiveTypeReference.Definition as IEdmPrimitiveType;
							if (edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.String)
							{
								goto IL_0175;
							}
						}
						throw new ODataException(Strings.CastBinder_EnumOnlyCastToOrFromString);
					}
				}
			}
			IL_0175:
			if (isCast)
			{
				return edmTypeReference;
			}
			return EdmCoreModel.Instance.GetBoolean(true);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00028DC4 File Offset: 0x00026FC4
		private static IEdmTypeReference TryGetTypeReference(IEdmModel model, string fullTypeName, ODataUriResolver resolver)
		{
			IEdmTypeReference edmTypeReference = UriEdmHelpers.FindTypeFromModel(model, fullTypeName, resolver).ToTypeReference();
			if (edmTypeReference != null)
			{
				return edmTypeReference;
			}
			if (fullTypeName.StartsWith("Collection", StringComparison.Ordinal))
			{
				string[] array = fullTypeName.Split(new char[] { '(' });
				string text = array[1].Split(new char[] { ')' })[0];
				return EdmCoreModel.GetCollection(UriEdmHelpers.FindTypeFromModel(model, text, resolver).ToTypeReference());
			}
			return null;
		}

		// Token: 0x040007A5 RID: 1957
		private static readonly string[] UnboundFunctionNames = new string[] { "cast", "isof" };
	}
}
