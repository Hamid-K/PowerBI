using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Binders;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001CA RID: 458
	internal sealed class FunctionCallBinder : BinderBase
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x0003B8B8 File Offset: 0x00039AB8
		internal FunctionCallBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
			: base(bindMethod, state)
		{
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0003B8C4 File Offset: 0x00039AC4
		internal static void TypePromoteArguments(FunctionSignatureWithReturnType signature, List<QueryNode> argumentNodes)
		{
			for (int i = 0; i < argumentNodes.Count; i++)
			{
				SingleValueNode singleValueNode = (SingleValueNode)argumentNodes[i];
				IEdmTypeReference edmTypeReference = signature.ArgumentTypes[i];
				argumentNodes[i] = MetadataBindingUtils.ConvertToTypeIfNeeded(singleValueNode, edmTypeReference);
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0003B908 File Offset: 0x00039B08
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

		// Token: 0x0600110C RID: 4364 RVA: 0x0003B994 File Offset: 0x00039B94
		internal static FunctionSignatureWithReturnType MatchSignatureToUriFunction(string functionName, SingleValueNode[] argumentNodes, FunctionSignatureWithReturnType[] signatures)
		{
			IEdmTypeReference[] array = Enumerable.ToArray<IEdmTypeReference>(Enumerable.Select<SingleValueNode, IEdmTypeReference>(argumentNodes, (SingleValueNode s) => s.TypeReference));
			int argumentCount = array.Length;
			FunctionSignatureWithReturnType functionSignatureWithReturnType;
			if (Enumerable.All<IEdmTypeReference>(array, (IEdmTypeReference a) => a == null) && argumentCount > 0)
			{
				functionSignatureWithReturnType = Enumerable.FirstOrDefault<FunctionSignatureWithReturnType>(signatures, (FunctionSignatureWithReturnType candidateFunction) => Enumerable.Count<IEdmTypeReference>(candidateFunction.ArgumentTypes) == argumentCount);
				if (functionSignatureWithReturnType == null)
				{
					throw new ODataException(Strings.FunctionCallBinder_CannotFindASuitableOverload(functionName, Enumerable.Count<IEdmTypeReference>(array)));
				}
				functionSignatureWithReturnType = new FunctionSignatureWithReturnType(null, functionSignatureWithReturnType.ArgumentTypes);
			}
			else
			{
				functionSignatureWithReturnType = TypePromotionUtils.FindBestFunctionSignature(signatures, argumentNodes);
				if (functionSignatureWithReturnType == null)
				{
					throw new ODataException(Strings.MetadataBinder_NoApplicableFunctionFound(functionName, UriFunctionsHelper.BuildFunctionSignatureListDescription(functionName, signatures)));
				}
			}
			return functionSignatureWithReturnType;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0003BA6C File Offset: 0x00039C6C
		internal static FunctionSignatureWithReturnType[] GetUriFunctionSignatures(string functionName)
		{
			FunctionSignatureWithReturnType[] array = null;
			FunctionSignatureWithReturnType[] array2 = null;
			bool flag = CustomUriFunctions.TryGetCustomFunction(functionName, out array);
			bool flag2 = BuiltInUriFunctions.TryGetBuiltInFunction(functionName, out array2);
			if (!flag && !flag2)
			{
				throw new ODataException(Strings.MetadataBinder_UnknownFunction(functionName));
			}
			if (!flag)
			{
				return array2;
			}
			if (!flag2)
			{
				return array;
			}
			return Enumerable.ToArray<FunctionSignatureWithReturnType>(Enumerable.Concat<FunctionSignatureWithReturnType>(array2, array));
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0003BAC8 File Offset: 0x00039CC8
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
			List<QueryNode> list = new List<QueryNode>(Enumerable.Select<FunctionParameterToken, QueryNode>(functionCallToken.Arguments, (FunctionParameterToken ar) => this.bindMethod(ar)));
			return this.BindAsUriFunction(functionCallToken, list);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0003BB8F File Offset: 0x00039D8F
		internal bool TryBindEndPathAsFunctionCall(EndPathToken endPathToken, QueryNode parent, BindingState state, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(endPathToken.Identifier, null, parent, state, out boundFunction);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0003BBA2 File Offset: 0x00039DA2
		internal bool TryBindInnerPathAsFunctionCall(InnerPathToken innerPathToken, QueryNode parent, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(innerPathToken.Identifier, null, parent, this.state, out boundFunction);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0003BBB9 File Offset: 0x00039DB9
		internal bool TryBindDottedIdentifierAsFunctionCall(DottedIdentifierToken dottedIdentifierToken, SingleValueNode parent, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(dottedIdentifierToken.Identifier, null, parent, this.state, out boundFunction);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0003BBD0 File Offset: 0x00039DD0
		private QueryNode BindAsUriFunction(FunctionCallToken functionCallToken, List<QueryNode> argumentNodes)
		{
			if (functionCallToken.Source != null)
			{
				throw new ODataException(Strings.FunctionCallBinder_UriFunctionMustHaveHaveNullParent(functionCallToken.Name));
			}
			string text = (this.state.Configuration.EnableCaseInsensitiveUriFunctionIdentifier ? functionCallToken.Name.ToLowerInvariant() : functionCallToken.Name);
			if (FunctionCallBinder.IsUnboundFunction(text))
			{
				return this.CreateUnboundFunctionNode(text, argumentNodes);
			}
			FunctionSignatureWithReturnType[] uriFunctionSignatures = FunctionCallBinder.GetUriFunctionSignatures(text);
			SingleValueNode[] array = FunctionCallBinder.ValidateArgumentsAreSingleValue(text, argumentNodes);
			FunctionSignatureWithReturnType functionSignatureWithReturnType = FunctionCallBinder.MatchSignatureToUriFunction(text, array, uriFunctionSignatures);
			if (functionSignatureWithReturnType.ReturnType != null)
			{
				FunctionCallBinder.TypePromoteArguments(functionSignatureWithReturnType, argumentNodes);
			}
			IEdmTypeReference returnType = functionSignatureWithReturnType.ReturnType;
			return new SingleValueFunctionCallNode(text, new ReadOnlyCollection<QueryNode>(argumentNodes), returnType);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0003BC80 File Offset: 0x00039E80
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Uri Parser does not need to go through the ODL behavior knob.")]
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
			if (identifier.IndexOf(".", 4) == -1 && base.Resolver.GetType() == typeof(ODataUriResolver))
			{
				return false;
			}
			List<FunctionParameterToken> list = ((arguments == null) ? new List<FunctionParameterToken>() : Enumerable.ToList<FunctionParameterToken>(arguments));
			IEdmOperation edmOperation;
			if (!FunctionOverloadResolver.ResolveOperationFromList(identifier, Enumerable.ToList<string>(Enumerable.Select<FunctionParameterToken, string>(list, (FunctionParameterToken ar) => ar.ParameterName)), edmType, state.Model, out edmOperation, base.Resolver))
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
			IEnumerable<QueryNode> enumerable = Enumerable.Select<FunctionParameterToken, QueryNode>(collection, (FunctionParameterToken p) => this.bindMethod(p));
			enumerable = Enumerable.ToList<QueryNode>(enumerable);
			IEdmTypeReference returnType = edmFunction.ReturnType;
			IEdmEntitySetBase edmEntitySetBase = null;
			SingleEntityNode singleEntityNode = parent as SingleEntityNode;
			if (singleEntityNode != null)
			{
				edmEntitySetBase = edmFunction.GetTargetEntitySet(singleEntityNode.NavigationSource, state.Model);
			}
			string text = edmFunction.FullName();
			if (returnType.IsEntity())
			{
				boundFunction = new SingleEntityFunctionCallNode(text, new IEdmFunction[] { edmFunction }, enumerable, (IEdmEntityTypeReference)returnType.Definition.ToTypeReference(), edmEntitySetBase, parent);
			}
			else if (returnType.IsEntityCollection())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = (IEdmCollectionTypeReference)returnType;
				boundFunction = new EntityCollectionFunctionCallNode(text, new IEdmFunction[] { edmFunction }, enumerable, edmCollectionTypeReference, edmEntitySetBase, parent);
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

		// Token: 0x06001114 RID: 4372 RVA: 0x0003BEC0 File Offset: 0x0003A0C0
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Parameter type is needed here to check whether can be convert from source")]
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

		// Token: 0x06001115 RID: 4373 RVA: 0x0003C090 File Offset: 0x0003A290
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

		// Token: 0x06001116 RID: 4374 RVA: 0x0003C15C File Offset: 0x0003A35C
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Parameter type is needed to convert from uri literal.")]
		private static ICollection<FunctionParameterToken> HandleComplexOrCollectionParameterValueIfExists(IEdmModel model, IEdmOperation operation, ICollection<FunctionParameterToken> parameterTokens, bool enableCaseInsensitive, bool enableUriTemplateParsing = false)
		{
			ICollection<FunctionParameterToken> collection = new Collection<FunctionParameterToken>();
			foreach (FunctionParameterToken functionParameterToken in parameterTokens)
			{
				IEdmOperationParameter edmOperationParameter = operation.FindParameter(functionParameterToken.ParameterName);
				FunctionParameterToken functionParameterToken2;
				if (enableCaseInsensitive && edmOperationParameter == null)
				{
					edmOperationParameter = ODataUriResolver.ResolveOpearationParameterNameCaseInsensitive(operation, functionParameterToken.ParameterName);
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
					if (expressionLexer.CurrentToken.Kind == ExpressionTokenKind.BracketedExpression)
					{
						UriTemplateExpression uriTemplateExpression;
						object obj;
						if (enableUriTemplateParsing && UriTemplateParser.TryParseLiteral(expressionLexer.CurrentToken.Text, edmOperationParameter.Type, out uriTemplateExpression))
						{
							obj = uriTemplateExpression;
						}
						else
						{
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

		// Token: 0x06001117 RID: 4375 RVA: 0x0003C2D4 File Offset: 0x0003A4D4
		private static bool IsUnboundFunction(string functionName)
		{
			return Enumerable.Contains<string>(FunctionCallBinder.UnboundFunctionNames, functionName);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0003C2E4 File Offset: 0x0003A4E4
		private SingleValueNode CreateUnboundFunctionNode(string functionCallTokenName, List<QueryNode> args)
		{
			IEdmTypeReference edmTypeReference = null;
			if (functionCallTokenName != null)
			{
				if (!(functionCallTokenName == "isof"))
				{
					if (functionCallTokenName == "cast")
					{
						edmTypeReference = FunctionCallBinder.ValidateAndBuildCastArgs(this.state, ref args);
						if (edmTypeReference.IsEntity())
						{
							IEdmEntityTypeReference edmEntityTypeReference = edmTypeReference.AsEntity();
							SingleEntityNode singleEntityNode = Enumerable.ElementAt<QueryNode>(args, 0) as SingleEntityNode;
							if (singleEntityNode != null)
							{
								return new SingleEntityFunctionCallNode(functionCallTokenName, args, edmEntityTypeReference, singleEntityNode.NavigationSource);
							}
						}
					}
				}
				else
				{
					edmTypeReference = FunctionCallBinder.ValidateAndBuildIsOfArgs(this.state, ref args);
				}
			}
			return new SingleValueFunctionCallNode(functionCallTokenName, args, edmTypeReference);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0003C368 File Offset: 0x0003A568
		private static IEdmTypeReference ValidateAndBuildCastArgs(BindingState state, ref List<QueryNode> args)
		{
			return FunctionCallBinder.ValidateIsOfOrCast(state, true, ref args);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0003C372 File Offset: 0x0003A572
		private static IEdmTypeReference ValidateAndBuildIsOfArgs(BindingState state, ref List<QueryNode> args)
		{
			return FunctionCallBinder.ValidateIsOfOrCast(state, false, ref args);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0003C37C File Offset: 0x0003A57C
		private static IEdmTypeReference ValidateIsOfOrCast(BindingState state, bool isCast, ref List<QueryNode> args)
		{
			if (args.Count != 1 && args.Count != 2)
			{
				throw new ODataErrorException(Strings.MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands(args.Count));
			}
			ConstantNode constantNode = Enumerable.Last<QueryNode>(args) as ConstantNode;
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
				List<QueryNode> list = new List<QueryNode>();
				list.Add(new EntityRangeVariableReferenceNode(state.ImplicitRangeVariable.Name, state.ImplicitRangeVariable as EntityRangeVariable));
				list.Add(args[0]);
				args = list;
			}
			else if (!(args[0] is SingleValueNode))
			{
				throw new ODataException(Strings.MetadataBinder_CastOrIsOfCollectionsNotSupported);
			}
			if (isCast && args.Count == 2)
			{
				if (args[0].GetEdmTypeReference() is IEdmEnumTypeReference && !string.Equals(constantNode.Value as string, "Edm.String", 4))
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

		// Token: 0x0600111C RID: 4380 RVA: 0x0003C510 File Offset: 0x0003A710
		private static IEdmTypeReference TryGetTypeReference(IEdmModel model, string fullTypeName, ODataUriResolver resolver)
		{
			IEdmTypeReference edmTypeReference = UriEdmHelpers.FindTypeFromModel(model, fullTypeName, resolver).ToTypeReference();
			if (edmTypeReference != null)
			{
				return edmTypeReference;
			}
			if (fullTypeName.StartsWith("Collection", 4))
			{
				string[] array = fullTypeName.Split(new char[] { '(' });
				string text = array[1].Split(new char[] { ')' })[0];
				return EdmCoreModel.GetCollection(UriEdmHelpers.FindTypeFromModel(model, text, resolver).ToTypeReference());
			}
			return null;
		}

		// Token: 0x04000782 RID: 1922
		private static readonly string[] UnboundFunctionNames = new string[] { "cast", "isof" };
	}
}
