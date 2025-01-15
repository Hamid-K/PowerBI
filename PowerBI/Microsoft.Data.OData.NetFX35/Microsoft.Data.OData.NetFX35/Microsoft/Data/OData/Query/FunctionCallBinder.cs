using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200002B RID: 43
	internal sealed class FunctionCallBinder
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00005478 File Offset: 0x00003678
		internal FunctionCallBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005488 File Offset: 0x00003688
		internal static void TypePromoteArguments(FunctionSignature signature, List<QueryNode> argumentNodes)
		{
			for (int i = 0; i < argumentNodes.Count; i++)
			{
				SingleValueNode singleValueNode = (SingleValueNode)argumentNodes[i];
				IEdmTypeReference edmTypeReference = signature.ArgumentTypes[i];
				argumentNodes[i] = MetadataBindingUtils.ConvertToTypeIfNeeded(singleValueNode, edmTypeReference);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000054CC File Offset: 0x000036CC
		internal static IEdmTypeReference[] EnsureArgumentsAreSingleValue(string functionName, List<QueryNode> argumentNodes)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(functionName, "functionCallToken");
			ExceptionUtils.CheckArgumentNotNull<List<QueryNode>>(argumentNodes, "argumentNodes");
			IEdmTypeReference[] array = new IEdmTypeReference[argumentNodes.Count];
			for (int i = 0; i < argumentNodes.Count; i++)
			{
				SingleValueNode singleValueNode = argumentNodes[i] as SingleValueNode;
				if (singleValueNode == null)
				{
					throw new ODataException(Strings.MetadataBinder_FunctionArgumentNotSingleValue(functionName));
				}
				array[i] = singleValueNode.TypeReference;
			}
			return array;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005558 File Offset: 0x00003758
		internal static FunctionSignatureWithReturnType MatchSignatureToBuiltInFunction(string functionName, IEdmTypeReference[] argumentTypes, FunctionSignatureWithReturnType[] signatures)
		{
			int argumentCount = argumentTypes.Length;
			FunctionSignatureWithReturnType functionSignatureWithReturnType;
			if (Enumerable.All<IEdmTypeReference>(argumentTypes, (IEdmTypeReference a) => a == null) && argumentCount > 0)
			{
				functionSignatureWithReturnType = Enumerable.FirstOrDefault<FunctionSignatureWithReturnType>(signatures, (FunctionSignatureWithReturnType candidateFunction) => Enumerable.Count<IEdmTypeReference>(candidateFunction.ArgumentTypes) == argumentCount);
				if (functionSignatureWithReturnType == null)
				{
					throw new ODataException(Strings.FunctionCallBinder_CannotFindASuitableOverload(functionName, Enumerable.Count<IEdmTypeReference>(argumentTypes)));
				}
				functionSignatureWithReturnType = new FunctionSignatureWithReturnType(null, functionSignatureWithReturnType.ArgumentTypes);
			}
			else
			{
				functionSignatureWithReturnType = TypePromotionUtils.FindBestFunctionSignature(signatures, argumentTypes);
				if (functionSignatureWithReturnType == null)
				{
					throw new ODataException(Strings.MetadataBinder_NoApplicableFunctionFound(functionName, BuiltInFunctions.BuildFunctionSignatureListDescription(functionName, signatures)));
				}
			}
			return functionSignatureWithReturnType;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005604 File Offset: 0x00003804
		internal static FunctionSignatureWithReturnType[] GetBuiltInFunctionSignatures(string functionName)
		{
			FunctionSignatureWithReturnType[] array;
			if (!BuiltInFunctions.TryGetBuiltInFunction(functionName, out array))
			{
				throw new ODataException(Strings.MetadataBinder_UnknownFunction(functionName));
			}
			return array;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005638 File Offset: 0x00003838
		internal QueryNode BindFunctionCall(FunctionCallToken functionCallToken, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<FunctionCallToken>(functionCallToken, "functionCallToken");
			ExceptionUtils.CheckArgumentNotNull<string>(functionCallToken.Name, "functionCallToken.Name");
			QueryNode queryNode;
			if (functionCallToken.Source != null)
			{
				queryNode = this.bindMethod(functionCallToken.Source);
			}
			else
			{
				queryNode = NodeFactory.CreateRangeVariableReferenceNode(state.ImplicitRangeVariable);
			}
			QueryNode queryNode2;
			if (this.TryBindIdentifier(functionCallToken.Name, functionCallToken.Arguments, queryNode, state, out queryNode2))
			{
				return queryNode2;
			}
			if (this.TryBindIdentifier(functionCallToken.Name, functionCallToken.Arguments, null, state, out queryNode2))
			{
				return queryNode2;
			}
			List<QueryNode> list = new List<QueryNode>(Enumerable.Select<FunctionParameterToken, QueryNode>(functionCallToken.Arguments, (FunctionParameterToken ar) => this.bindMethod(ar)));
			return FunctionCallBinder.BindAsBuiltInFunction(functionCallToken, state, list);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000056E3 File Offset: 0x000038E3
		internal bool TryBindEndPathAsFunctionCall(EndPathToken endPathToken, QueryNode parent, BindingState state, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(endPathToken.Identifier, null, parent, state, out boundFunction);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000056F6 File Offset: 0x000038F6
		internal bool TryBindInnerPathAsFunctionCall(InnerPathToken innerPathToken, QueryNode parent, BindingState state, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(innerPathToken.Identifier, null, parent, state, out boundFunction);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005709 File Offset: 0x00003909
		internal bool TryBindDottedIdentifierAsFunctionCall(DottedIdentifierToken dottedIdentifierToken, SingleValueNode parent, BindingState state, out QueryNode boundFunction)
		{
			return this.TryBindIdentifier(dottedIdentifierToken.Identifier, null, parent, state, out boundFunction);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000571C File Offset: 0x0000391C
		private static QueryNode BindAsBuiltInFunction(FunctionCallToken functionCallToken, BindingState state, List<QueryNode> argumentNodes)
		{
			if (functionCallToken.Source != null)
			{
				throw new ODataException(Strings.FunctionCallBinder_BuiltInFunctionMustHaveHaveNullParent(functionCallToken.Name));
			}
			if (FunctionCallBinder.IsUnboundFunction(functionCallToken.Name))
			{
				return FunctionCallBinder.CreateUnboundFunctionNode(functionCallToken, argumentNodes, state);
			}
			FunctionSignatureWithReturnType[] builtInFunctionSignatures = FunctionCallBinder.GetBuiltInFunctionSignatures(functionCallToken.Name);
			IEdmTypeReference[] array = FunctionCallBinder.EnsureArgumentsAreSingleValue(functionCallToken.Name, argumentNodes);
			FunctionSignatureWithReturnType functionSignatureWithReturnType = FunctionCallBinder.MatchSignatureToBuiltInFunction(functionCallToken.Name, array, builtInFunctionSignatures);
			if (functionSignatureWithReturnType.ReturnType != null)
			{
				FunctionCallBinder.TypePromoteArguments(functionSignatureWithReturnType, argumentNodes);
			}
			IEdmTypeReference returnType = functionSignatureWithReturnType.ReturnType;
			return new SingleValueFunctionCallNode(functionCallToken.Name, new ReadOnlyCollection<QueryNode>(argumentNodes), returnType);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000057C0 File Offset: 0x000039C0
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
			List<FunctionParameterToken> list = ((arguments == null) ? new List<FunctionParameterToken>() : Enumerable.ToList<FunctionParameterToken>(arguments));
			IEdmFunctionImport edmFunctionImport;
			if (!FunctionOverloadResolver.ResolveFunctionsFromList(identifier, Enumerable.ToList<string>(Enumerable.Select<FunctionParameterToken, string>(list, (FunctionParameterToken ar) => ar.ParameterName)), edmType, state.Model, out edmFunctionImport))
			{
				return false;
			}
			if (singleValueNode != null && singleValueNode.TypeReference == null)
			{
				throw new ODataException(Strings.FunctionCallBinder_CallingFunctionOnOpenProperty(identifier));
			}
			if (edmFunctionImport.IsSideEffecting)
			{
				return false;
			}
			ICollection<FunctionParameterToken> collection;
			if (!FunctionParameterParser.TryParseFunctionParameters(list, state.Configuration, edmFunctionImport, out collection))
			{
				return false;
			}
			IEnumerable<QueryNode> enumerable = Enumerable.Select<FunctionParameterToken, QueryNode>(collection, (FunctionParameterToken p) => this.bindMethod(p));
			IEdmTypeReference returnType = edmFunctionImport.ReturnType;
			IEdmEntitySet edmEntitySet = null;
			SingleEntityNode singleEntityNode = parent as SingleEntityNode;
			if (singleEntityNode != null)
			{
				edmEntitySet = edmFunctionImport.GetTargetEntitySet(singleEntityNode.EntitySet, state.Model);
			}
			if (returnType.IsEntity())
			{
				boundFunction = new SingleEntityFunctionCallNode(identifier, new IEdmFunctionImport[] { edmFunctionImport }, enumerable, (IEdmEntityTypeReference)returnType.Definition.ToTypeReference(), edmEntitySet, parent);
			}
			else if (returnType.IsEntityCollection())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = (IEdmCollectionTypeReference)returnType;
				boundFunction = new EntityCollectionFunctionCallNode(identifier, new IEdmFunctionImport[] { edmFunctionImport }, enumerable, edmCollectionTypeReference, edmEntitySet, parent);
			}
			else if (returnType.IsCollection())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference2 = (IEdmCollectionTypeReference)returnType;
				boundFunction = new CollectionFunctionCallNode(identifier, new IEdmFunctionImport[] { edmFunctionImport }, enumerable, edmCollectionTypeReference2, parent);
			}
			else
			{
				boundFunction = new SingleValueFunctionCallNode(identifier, new IEdmFunctionImport[] { edmFunctionImport }, enumerable, returnType, parent);
			}
			return true;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005998 File Offset: 0x00003B98
		private static bool IsUnboundFunction(string functionName)
		{
			return Enumerable.Contains<string>(FunctionCallBinder.UnboundFunctionNames, functionName);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000059A8 File Offset: 0x00003BA8
		private static SingleValueNode CreateUnboundFunctionNode(FunctionCallToken functionCallToken, List<QueryNode> args, BindingState state)
		{
			IEdmTypeReference edmTypeReference = null;
			string name;
			if ((name = functionCallToken.Name) != null)
			{
				if (!(name == "isof"))
				{
					if (name == "cast")
					{
						edmTypeReference = FunctionCallBinder.ValidateAndBuildCastArgs(state, ref args);
						if (edmTypeReference.IsEntity())
						{
							IEdmEntityTypeReference edmEntityTypeReference = edmTypeReference.AsEntity();
							SingleEntityNode singleEntityNode = Enumerable.ElementAt<QueryNode>(args, 0) as SingleEntityNode;
							if (singleEntityNode != null)
							{
								return new SingleEntityFunctionCallNode(functionCallToken.Name, args, edmEntityTypeReference, singleEntityNode.EntitySet);
							}
						}
					}
				}
				else
				{
					edmTypeReference = FunctionCallBinder.ValidateAndBuildIsOfArgs(state, ref args);
				}
			}
			return new SingleValueFunctionCallNode(functionCallToken.Name, args, edmTypeReference);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005A31 File Offset: 0x00003C31
		private static IEdmTypeReference ValidateAndBuildCastArgs(BindingState state, ref List<QueryNode> args)
		{
			return FunctionCallBinder.ValidateIsOfOrCast(state, true, ref args);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005A3B File Offset: 0x00003C3B
		private static IEdmTypeReference ValidateAndBuildIsOfArgs(BindingState state, ref List<QueryNode> args)
		{
			return FunctionCallBinder.ValidateIsOfOrCast(state, false, ref args);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005A48 File Offset: 0x00003C48
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
				edmTypeReference = FunctionCallBinder.TryGetTypeReference(state.Model, constantNode.Value as string);
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
			if (isCast)
			{
				return edmTypeReference;
			}
			return EdmCoreModel.Instance.GetBoolean(true);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005B38 File Offset: 0x00003D38
		private static IEdmTypeReference TryGetTypeReference(IEdmModel model, string fullTypeName)
		{
			IEdmTypeReference edmTypeReference = UriEdmHelpers.FindTypeFromModel(model, fullTypeName).ToTypeReference();
			if (edmTypeReference == null)
			{
				return UriEdmHelpers.FindCollectionTypeFromModel(model, fullTypeName);
			}
			return edmTypeReference;
		}

		// Token: 0x04000059 RID: 89
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x0400005A RID: 90
		private static readonly string[] UnboundFunctionNames = new string[] { "cast", "isof" };
	}
}
