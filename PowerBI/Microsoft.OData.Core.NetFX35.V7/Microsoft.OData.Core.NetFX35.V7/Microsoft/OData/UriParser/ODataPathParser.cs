using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000117 RID: 279
	internal sealed class ODataPathParser
	{
		// Token: 0x06000CEB RID: 3307 RVA: 0x00024990 File Offset: 0x00022B90
		internal ODataPathParser(ODataUriParserConfiguration configuration)
		{
			this.configuration = configuration;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000249B8 File Offset: 0x00022BB8
		internal static void ExtractSegmentIdentifierAndParenthesisExpression(string segmentText, out string identifier, out string parenthesisExpression)
		{
			int num = segmentText.IndexOf('(');
			if (num < 0)
			{
				identifier = segmentText;
				parenthesisExpression = null;
			}
			else
			{
				if (segmentText.get_Chars(segmentText.Length - 1) != ')')
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
				identifier = segmentText.Substring(0, num);
				parenthesisExpression = segmentText.Substring(num + 1, segmentText.Length - identifier.Length - 2);
			}
			if (identifier.Length == 0)
			{
				throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_EmptySegmentInRequestUrl);
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00024A2C File Offset: 0x00022C2C
		internal IList<ODataPathSegment> ParsePath(ICollection<string> segments)
		{
			foreach (string text in segments)
			{
				this.segmentQueue.Enqueue(text);
			}
			string text2 = null;
			try
			{
				while (this.TryGetNextSegmentText(out text2))
				{
					if (this.parsedSegments.Count == 0)
					{
						this.CreateFirstSegment(text2);
					}
					else
					{
						this.CreateNextSegment(text2);
					}
				}
			}
			catch (ODataUnrecognizedPathException ex)
			{
				ex.ParsedSegments = this.parsedSegments;
				ex.CurrentSegment = text2;
				ex.UnparsedSegments = Enumerable.ToList<string>(this.segmentQueue);
				throw;
			}
			List<ODataPathSegment> list = new List<ODataPathSegment>(this.parsedSegments.Count);
			foreach (ODataPathSegment odataPathSegment in this.parsedSegments)
			{
				list.Add(odataPathSegment);
			}
			this.parsedSegments.Clear();
			return list;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00024B40 File Offset: 0x00022D40
		private static bool TryBindingParametersAndMatchingOperationImport(string identifier, string parenthesisExpression, ODataUriParserConfiguration configuration, out ICollection<OperationSegmentParameter> boundParameters, out IEdmOperationImport matchingFunctionImport)
		{
			matchingFunctionImport = null;
			ICollection<FunctionParameterToken> collection = null;
			if (!string.IsNullOrEmpty(parenthesisExpression))
			{
				if (!FunctionParameterParser.TrySplitOperationParameters(parenthesisExpression, configuration, out collection))
				{
					IEdmOperationImport edmOperationImport = null;
					if (!FunctionOverloadResolver.ResolveOperationImportFromList(identifier, ODataPathParser.EmptyList, configuration.Model, out edmOperationImport, configuration.Resolver))
					{
						boundParameters = null;
						return false;
					}
					IEdmCollectionTypeReference edmCollectionTypeReference = edmOperationImport.Operation.ReturnType as IEdmCollectionTypeReference;
					if (edmCollectionTypeReference != null && edmCollectionTypeReference.ElementType().IsEntity())
					{
						matchingFunctionImport = edmOperationImport;
						boundParameters = null;
						return true;
					}
					throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(identifier));
				}
			}
			else
			{
				collection = new Collection<FunctionParameterToken>();
			}
			if (FunctionOverloadResolver.ResolveOperationImportFromList(identifier, Enumerable.ToList<string>(Enumerable.Select<FunctionParameterToken, string>(collection, (FunctionParameterToken k) => k.ParameterName)), configuration.Model, out matchingFunctionImport, configuration.Resolver))
			{
				IEdmOperation operation = matchingFunctionImport.Operation;
				boundParameters = FunctionCallBinder.BindSegmentParameters(configuration, operation, collection);
				return true;
			}
			boundParameters = null;
			return false;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00024C20 File Offset: 0x00022E20
		private static bool TryBindingParametersAndMatchingOperation(string identifier, string parenthesisExpression, IEdmType bindingType, ODataUriParserConfiguration configuration, out ICollection<OperationSegmentParameter> boundParameters, out IEdmOperation matchingOperation)
		{
			if (identifier != null && identifier.IndexOf(".", 4) == -1 && configuration.Resolver.GetType() == typeof(ODataUriResolver))
			{
				boundParameters = null;
				matchingOperation = null;
				return false;
			}
			matchingOperation = null;
			ICollection<FunctionParameterToken> collection;
			if (!string.IsNullOrEmpty(parenthesisExpression))
			{
				if (!FunctionParameterParser.TrySplitOperationParameters(parenthesisExpression, configuration, out collection))
				{
					IEdmOperation edmOperation = null;
					if (!FunctionOverloadResolver.ResolveOperationFromList(identifier, new List<string>(), bindingType, configuration.Model, out edmOperation, configuration.Resolver))
					{
						boundParameters = null;
						return false;
					}
					IEdmCollectionTypeReference edmCollectionTypeReference = edmOperation.ReturnType as IEdmCollectionTypeReference;
					if (edmCollectionTypeReference != null && edmCollectionTypeReference.ElementType().IsEntity())
					{
						matchingOperation = edmOperation;
						boundParameters = null;
						return true;
					}
					throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(identifier));
				}
			}
			else
			{
				collection = new Collection<FunctionParameterToken>();
			}
			if (FunctionOverloadResolver.ResolveOperationFromList(identifier, Enumerable.ToList<string>(Enumerable.Select<FunctionParameterToken, string>(collection, (FunctionParameterToken k) => k.ParameterName)), bindingType, configuration.Model, out matchingOperation, configuration.Resolver))
			{
				boundParameters = FunctionCallBinder.BindSegmentParameters(configuration, matchingOperation, collection);
				return true;
			}
			boundParameters = null;
			return false;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00024D28 File Offset: 0x00022F28
		private static void CheckSingleResult(bool isSingleResult, string identifier)
		{
			if (!isSingleResult)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_CannotQueryCollections(identifier));
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00024D39 File Offset: 0x00022F39
		private bool TryGetNextSegmentText(out string segmentText)
		{
			return this.TryGetNextSegmentText(false, out segmentText);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00024D44 File Offset: 0x00022F44
		private bool TryGetNextSegmentText(bool previousSegmentWasEscapeMarker, out string segmentText)
		{
			if (this.segmentQueue.Count == 0)
			{
				segmentText = null;
				return false;
			}
			segmentText = this.segmentQueue.Dequeue();
			if (segmentText == "$")
			{
				this.nextSegmentMustReferToMetadata = true;
				return this.TryGetNextSegmentText(true, out segmentText);
			}
			if (!previousSegmentWasEscapeMarker)
			{
				this.nextSegmentMustReferToMetadata = false;
			}
			if (this.parsedSegments.Count > 0)
			{
				ODataPathParser.ThrowIfMustBeLeafSegment(this.parsedSegments[this.parsedSegments.Count - 1]);
			}
			return true;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00024DC4 File Offset: 0x00022FC4
		private bool TryHandleAsKeySegment(string segmentText)
		{
			ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 1];
			KeySegment keySegment = this.FindPreviousKeySegment();
			KeySegment keySegment2;
			if (!this.nextSegmentMustReferToMetadata && SegmentKeyHandler.TryHandleSegmentAsKey(segmentText, odataPathSegment, keySegment, this.configuration.UrlKeyDelimiter, this.configuration.Resolver, out keySegment2, this.configuration.EnableUriTemplateParsing))
			{
				this.parsedSegments.Add(keySegment2);
				return true;
			}
			return false;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00024E35 File Offset: 0x00023035
		private KeySegment FindPreviousKeySegment()
		{
			return (KeySegment)Enumerable.LastOrDefault<ODataPathSegment>(this.parsedSegments, (ODataPathSegment s) => s is KeySegment);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00024E68 File Offset: 0x00023068
		private static void ThrowIfMustBeLeafSegment(ODataPathSegment previous)
		{
			OperationImportSegment operationImportSegment = previous as OperationImportSegment;
			if (operationImportSegment != null)
			{
				foreach (IEdmOperationImport edmOperationImport in operationImportSegment.OperationImports)
				{
					if (edmOperationImport.IsActionImport() || (edmOperationImport.IsFunctionImport() && !((IEdmFunctionImport)edmOperationImport).Function.IsComposable))
					{
						throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_MustBeLeafSegment(previous.Identifier));
					}
				}
			}
			OperationSegment operationSegment = previous as OperationSegment;
			if (operationSegment != null)
			{
				foreach (IEdmOperation edmOperation in operationSegment.Operations)
				{
					if (edmOperation.IsAction() || (edmOperation.IsFunction() && !((IEdmFunction)edmOperation).IsComposable))
					{
						throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_MustBeLeafSegment(previous.Identifier));
					}
				}
			}
			if (previous.TargetKind == RequestTargetKind.Batch || previous.TargetKind == RequestTargetKind.Metadata || previous.TargetKind == RequestTargetKind.PrimitiveValue || previous.TargetKind == RequestTargetKind.DynamicValue || previous.TargetKind == RequestTargetKind.EnumValue || previous.TargetKind == RequestTargetKind.MediaResource || previous.TargetKind == RequestTargetKind.VoidOperation || previous.TargetKind == RequestTargetKind.Nothing)
			{
				throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_MustBeLeafSegment(previous.Identifier));
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00024FBC File Offset: 0x000231BC
		private bool TryCreateCountSegment(string segmentText)
		{
			string text;
			string text2;
			ODataPathParser.ExtractSegmentIdentifierAndParenthesisExpression(segmentText, out text, out text2);
			if (!this.IdentifierIs("$count", text))
			{
				return false;
			}
			if (text2 != null)
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
			ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 1];
			if ((odataPathSegment.TargetKind != RequestTargetKind.Resource || odataPathSegment.SingleResult) && odataPathSegment.TargetKind != RequestTargetKind.Collection)
			{
				throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_CountNotSupported(odataPathSegment.Identifier));
			}
			this.parsedSegments.Add(CountSegment.Instance);
			return true;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00025044 File Offset: 0x00023244
		private bool TryCreateEntityReferenceSegment(string text)
		{
			string text2;
			string text3;
			ODataPathParser.ExtractSegmentIdentifierAndParenthesisExpression(text, out text2, out text3);
			if (!this.IdentifierIs("$ref", text2))
			{
				return false;
			}
			if (text3 != null)
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
			Stack<KeySegment> stack = new Stack<KeySegment>();
			for (;;)
			{
				KeySegment keySegment = this.parsedSegments[this.parsedSegments.Count - 1] as KeySegment;
				if (keySegment == null)
				{
					break;
				}
				stack.Push(keySegment);
				this.parsedSegments.Remove(keySegment);
			}
			ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 1];
			NavigationPropertySegment navigationPropertySegment = odataPathSegment as NavigationPropertySegment;
			if (navigationPropertySegment == null || navigationPropertySegment.TargetKind != RequestTargetKind.Resource)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.PathParser_EntityReferenceNotSupported(odataPathSegment.Identifier));
			}
			this.parsedSegments.Remove(odataPathSegment);
			IEdmPathExpression edmPathExpression;
			IEdmNavigationSource edmNavigationSource = this.parsedSegments[this.parsedSegments.Count - 1].TargetEdmNavigationSource.FindNavigationTarget(navigationPropertySegment.NavigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), this.parsedSegments, out edmPathExpression);
			if (edmNavigationSource == null)
			{
				throw ExceptionUtil.CreateResourceNotFoundError(navigationPropertySegment.NavigationProperty.Name);
			}
			NavigationPropertyLinkSegment navigationPropertyLinkSegment = new NavigationPropertyLinkSegment(navigationPropertySegment.NavigationProperty, edmNavigationSource);
			this.parsedSegments.Add(navigationPropertyLinkSegment);
			while (stack.Count > 0)
			{
				this.parsedSegments.Add(stack.Pop());
			}
			string text4;
			if (this.TryGetNextSegmentText(out text4))
			{
				throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_MustBeLeafSegment("$ref"));
			}
			return true;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000251AC File Offset: 0x000233AC
		private bool TryBindKeyFromParentheses(string parenthesesSection)
		{
			if (parenthesesSection == null)
			{
				return false;
			}
			ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 1];
			KeySegment keySegment = this.FindPreviousKeySegment();
			ODataPathSegment odataPathSegment2;
			if (!SegmentKeyHandler.TryCreateKeySegmentFromParentheses(odataPathSegment, keySegment, parenthesesSection, this.configuration.Resolver, out odataPathSegment2, this.configuration.EnableUriTemplateParsing))
			{
				return false;
			}
			this.parsedSegments.Add(odataPathSegment2);
			return true;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00025210 File Offset: 0x00023410
		private bool TryCreateValueSegment(string text)
		{
			string text2;
			string text3;
			ODataPathParser.ExtractSegmentIdentifierAndParenthesisExpression(text, out text2, out text3);
			if (!this.IdentifierIs("$value", text2))
			{
				return false;
			}
			if (text3 != null)
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
			ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 1];
			ODataPathSegment odataPathSegment2 = new ValueSegment(odataPathSegment.EdmType);
			if (odataPathSegment.TargetKind == RequestTargetKind.Primitive || odataPathSegment.TargetKind == RequestTargetKind.Enum)
			{
				odataPathSegment2.CopyValuesFrom(odataPathSegment);
			}
			else
			{
				odataPathSegment2.TargetEdmType = odataPathSegment.TargetEdmType;
			}
			odataPathSegment2.Identifier = "$value";
			odataPathSegment2.SingleResult = true;
			ODataPathParser.CheckSingleResult(odataPathSegment.SingleResult, odataPathSegment.Identifier);
			if (odataPathSegment.TargetKind == RequestTargetKind.Primitive)
			{
				odataPathSegment2.TargetKind = RequestTargetKind.PrimitiveValue;
			}
			else if (odataPathSegment.TargetKind == RequestTargetKind.Enum)
			{
				odataPathSegment2.TargetKind = RequestTargetKind.EnumValue;
			}
			else if (odataPathSegment.TargetKind == RequestTargetKind.Dynamic)
			{
				odataPathSegment2.TargetKind = RequestTargetKind.DynamicValue;
			}
			else
			{
				odataPathSegment2.TargetKind = RequestTargetKind.MediaResource;
			}
			this.parsedSegments.Add(odataPathSegment2);
			return true;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000252FC File Offset: 0x000234FC
		private void CreateDynamicPathSegment(ODataPathSegment previous, string identifier, string parenthesisExpression)
		{
			if (this.configuration.ParseDynamicPathSegmentFunc != null)
			{
				ICollection<ODataPathSegment> collection = this.configuration.ParseDynamicPathSegmentFunc(previous, identifier, parenthesisExpression);
				this.parsedSegments.AddRange(collection);
				return;
			}
			if (previous == null)
			{
				throw ExceptionUtil.CreateResourceNotFoundError(identifier);
			}
			ODataPathParser.CheckSingleResult(previous.SingleResult, previous.Identifier);
			if (previous.TargetEdmType != null && !previous.TargetEdmType.IsOpen())
			{
				throw ExceptionUtil.CreateResourceNotFoundError(identifier);
			}
			if (parenthesisExpression != null)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.OpenNavigationPropertiesNotSupportedOnOpenTypes(identifier));
			}
			ODataPathSegment odataPathSegment = new DynamicPathSegment(identifier);
			this.parsedSegments.Add(odataPathSegment);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00025390 File Offset: 0x00023590
		private void CreateNamedStreamSegment(ODataPathSegment previous, IEdmProperty streamProperty)
		{
			ODataPathSegment odataPathSegment = new PropertySegment((IEdmStructuralProperty)streamProperty);
			odataPathSegment.TargetKind = RequestTargetKind.MediaResource;
			odataPathSegment.SingleResult = true;
			odataPathSegment.TargetEdmType = previous.TargetEdmType;
			this.parsedSegments.Add(odataPathSegment);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000253D0 File Offset: 0x000235D0
		private void CreateFirstSegment(string segmentText)
		{
			string text;
			string text2;
			ODataPathParser.ExtractSegmentIdentifierAndParenthesisExpression(segmentText, out text, out text2);
			if (this.IdentifierIs("$metadata", text))
			{
				if (text2 != null)
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
				this.parsedSegments.Add(MetadataSegment.Instance);
				return;
			}
			else if (this.IdentifierIs("$batch", text))
			{
				if (text2 != null)
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
				this.parsedSegments.Add(BatchSegment.Instance);
				return;
			}
			else
			{
				if (this.IdentifierIs("$count", text))
				{
					throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_CountOnRoot);
				}
				if (this.configuration.BatchReferenceCallback != null && ODataPathParser.ContentIdRegex.IsMatch(text))
				{
					if (text2 != null)
					{
						throw ExceptionUtil.CreateSyntaxError();
					}
					BatchReferenceSegment batchReferenceSegment = this.configuration.BatchReferenceCallback.Invoke(text);
					if (batchReferenceSegment != null)
					{
						this.parsedSegments.Add(batchReferenceSegment);
						return;
					}
				}
				if (this.TryCreateSegmentForNavigationSource(text, text2))
				{
					return;
				}
				if (this.TryCreateSegmentForOperationImport(text, text2))
				{
					return;
				}
				this.CreateDynamicPathSegment(null, text, text2);
				return;
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000254B4 File Offset: 0x000236B4
		private bool TryCreateSegmentForNavigationSource(string identifier, string parenthesisExpression)
		{
			ODataPathSegment odataPathSegment = null;
			IEdmNavigationSource edmNavigationSource = this.configuration.Resolver.ResolveNavigationSource(this.configuration.Model, identifier);
			IEdmEntitySet edmEntitySet;
			IEdmSingleton edmSingleton;
			if ((edmEntitySet = edmNavigationSource as IEdmEntitySet) != null)
			{
				odataPathSegment = new EntitySetSegment(edmEntitySet)
				{
					Identifier = identifier
				};
			}
			else if ((edmSingleton = edmNavigationSource as IEdmSingleton) != null)
			{
				odataPathSegment = new SingletonSegment(edmSingleton)
				{
					Identifier = identifier
				};
			}
			if (odataPathSegment != null)
			{
				this.parsedSegments.Add(odataPathSegment);
				this.TryBindKeyFromParentheses(parenthesisExpression);
				return true;
			}
			return false;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002552C File Offset: 0x0002372C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IEdmModel", Justification = "The spelling is correct.")]
		private bool TryCreateSegmentForOperationImport(string identifier, string parenthesisExpression)
		{
			ICollection<OperationSegmentParameter> collection;
			IEdmOperationImport edmOperationImport;
			if (!ODataPathParser.TryBindingParametersAndMatchingOperationImport(identifier, parenthesisExpression, this.configuration, out collection, out edmOperationImport))
			{
				return false;
			}
			IEdmTypeReference returnType = edmOperationImport.Operation.ReturnType;
			IEdmEntitySetBase edmEntitySetBase = null;
			if (returnType != null)
			{
				edmEntitySetBase = edmOperationImport.GetTargetEntitySet(null, this.configuration.Model);
			}
			ODataPathSegment odataPathSegment = new OperationImportSegment(edmOperationImport, edmEntitySetBase, collection);
			this.parsedSegments.Add(odataPathSegment);
			this.TryBindKeySegmentIfNoResolvedParametersAndParathesisValueExsts(parenthesisExpression, returnType, collection, odataPathSegment);
			return true;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00025598 File Offset: 0x00023798
		private void TryBindKeySegmentIfNoResolvedParametersAndParathesisValueExsts(string parenthesisExpression, IEdmTypeReference returnType, ICollection<OperationSegmentParameter> resolvedParameters, ODataPathSegment segment)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = returnType as IEdmCollectionTypeReference;
			if (edmCollectionTypeReference != null && edmCollectionTypeReference.ElementType().IsEntity() && resolvedParameters == null && parenthesisExpression != null && this.TryBindKeyFromParentheses(parenthesisExpression))
			{
				ODataPathParser.ThrowIfMustBeLeafSegment(segment);
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000255D4 File Offset: 0x000237D4
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IEdmModel", Justification = "The spelling is correct.")]
		private bool TryCreateSegmentForOperation(ODataPathSegment previousSegment, string identifier, string parenthesisExpression)
		{
			IEdmType edmType = ((previousSegment == null) ? null : previousSegment.EdmType);
			ICollection<OperationSegmentParameter> collection;
			IEdmOperation edmOperation;
			if (!ODataPathParser.TryBindingParametersAndMatchingOperation(identifier, parenthesisExpression, edmType, this.configuration, out collection, out edmOperation))
			{
				return false;
			}
			if (!UriEdmHelpers.IsBindingTypeValid(edmType))
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_OperationSegmentBoundToANonEntityType);
			}
			if (previousSegment != null && edmType == null)
			{
				throw new ODataException(Strings.FunctionCallBinder_CallingFunctionOnOpenProperty(identifier));
			}
			IEdmTypeReference returnType = edmOperation.ReturnType;
			IEdmEntitySetBase edmEntitySetBase = null;
			if (returnType != null)
			{
				IEdmNavigationSource edmNavigationSource = ((previousSegment == null) ? null : previousSegment.TargetEdmNavigationSource);
				edmEntitySetBase = edmOperation.GetTargetEntitySet(edmNavigationSource, this.configuration.Model);
			}
			if (previousSegment is BatchReferenceSegment)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_BatchedActionOnEntityCreatedInSameChangeset(identifier));
			}
			ODataPathSegment odataPathSegment = new OperationSegment(edmOperation, collection, edmEntitySetBase)
			{
				Identifier = identifier
			};
			this.parsedSegments.Add(odataPathSegment);
			this.TryBindKeySegmentIfNoResolvedParametersAndParathesisValueExsts(parenthesisExpression, returnType, collection, odataPathSegment);
			return true;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00025698 File Offset: 0x00023898
		private void CreateNextSegment(string text)
		{
			if (this.TryCreateValueSegment(text))
			{
				return;
			}
			ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 1];
			if (odataPathSegment.TargetKind == RequestTargetKind.Primitive)
			{
				throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_ValueSegmentAfterScalarPropertySegment(odataPathSegment.Identifier, text));
			}
			if (this.TryCreateEntityReferenceSegment(text))
			{
				return;
			}
			if (this.TryCreateCountSegment(text))
			{
				return;
			}
			string text2;
			string text3;
			ODataPathParser.ExtractSegmentIdentifierAndParenthesisExpression(text, out text2, out text3);
			IEdmProperty edmProperty;
			if (odataPathSegment.SingleResult && odataPathSegment.TargetEdmType != null && this.TryBindProperty(text2, out edmProperty))
			{
				ODataPathParser.CheckSingleResult(odataPathSegment.SingleResult, odataPathSegment.Identifier);
				this.CreatePropertySegment(odataPathSegment, edmProperty, text3);
				return;
			}
			if (text.IndexOf('.') >= 0 && this.TryCreateTypeNameSegment(odataPathSegment, text2, text3))
			{
				return;
			}
			if (this.TryCreateSegmentForOperation(odataPathSegment, text2, text3))
			{
				return;
			}
			if (this.configuration.UrlKeyDelimiter.EnableKeyAsSegment && this.TryHandleAsKeySegment(text))
			{
				return;
			}
			if (this.configuration.EnableUriTemplateParsing && UriTemplateParser.IsValidTemplateLiteral(text))
			{
				this.parsedSegments.Add(new PathTemplateSegment(text));
				return;
			}
			this.CreateDynamicPathSegment(odataPathSegment, text2, text3);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x000257A8 File Offset: 0x000239A8
		private bool TryBindProperty(string identifier, out IEdmProperty projectedProperty)
		{
			ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 1];
			projectedProperty = null;
			IEdmStructuredType edmStructuredType = odataPathSegment.TargetEdmType as IEdmStructuredType;
			if (edmStructuredType == null)
			{
				IEdmCollectionType edmCollectionType = odataPathSegment.TargetEdmType as IEdmCollectionType;
				if (edmCollectionType != null)
				{
					edmStructuredType = edmCollectionType.ElementType.Definition as IEdmStructuredType;
				}
			}
			if (edmStructuredType == null)
			{
				return false;
			}
			projectedProperty = this.configuration.Resolver.ResolveProperty(edmStructuredType, identifier);
			return projectedProperty != null;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00025820 File Offset: 0x00023A20
		private bool TryCreateTypeNameSegment(ODataPathSegment previous, string identifier, string parenthesisExpression)
		{
			IEdmType edmType;
			if (previous.TargetEdmType == null || (edmType = UriEdmHelpers.FindTypeFromModel(this.configuration.Model, identifier, this.configuration.Resolver)) == null)
			{
				return false;
			}
			IEdmType edmType2 = previous.TargetEdmType;
			if (edmType2.TypeKind == EdmTypeKind.Collection)
			{
				edmType2 = ((IEdmCollectionType)edmType2).ElementType.Definition;
			}
			if (!edmType.IsOrInheritsFrom(edmType2) && !edmType2.IsOrInheritsFrom(edmType))
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_InvalidTypeIdentifier_UnrelatedType(edmType.FullTypeName(), edmType2.FullTypeName()));
			}
			IEdmType edmType3 = edmType;
			if (previous.EdmType.TypeKind == EdmTypeKind.Collection)
			{
				IEdmEntityType edmEntityType = edmType3 as IEdmEntityType;
				if (edmEntityType != null)
				{
					edmType3 = new EdmCollectionType(new EdmEntityTypeReference(edmEntityType, false));
				}
				else
				{
					IEdmComplexType edmComplexType = edmType3 as IEdmComplexType;
					if (edmComplexType == null)
					{
						throw new ODataException(Strings.PathParser_TypeCastOnlyAllowedAfterStructuralCollection(identifier));
					}
					edmType3 = new EdmCollectionType(new EdmComplexTypeReference(edmComplexType, false));
				}
			}
			ODataPathSegment odataPathSegment = new TypeSegment(edmType3, previous.EdmType, previous.TargetEdmNavigationSource)
			{
				Identifier = identifier,
				TargetKind = previous.TargetKind,
				SingleResult = previous.SingleResult,
				TargetEdmType = edmType
			};
			this.parsedSegments.Add(odataPathSegment);
			this.TryBindKeyFromParentheses(parenthesisExpression);
			return true;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00025944 File Offset: 0x00023B44
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IEdmModel", Justification = "The spelling is correct.")]
		private void CreatePropertySegment(ODataPathSegment previous, IEdmProperty property, string queryPortion)
		{
			if (property.Type.IsStream())
			{
				if (queryPortion != null)
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
				this.CreateNamedStreamSegment(previous, property);
				return;
			}
			else
			{
				ODataPathSegment odataPathSegment;
				if (property.PropertyKind == EdmPropertyKind.Navigation)
				{
					IEdmNavigationProperty edmNavigationProperty = (IEdmNavigationProperty)property;
					IEdmNavigationSource edmNavigationSource = null;
					if (previous.TargetEdmNavigationSource != null)
					{
						IEdmPathExpression edmPathExpression;
						edmNavigationSource = previous.TargetEdmNavigationSource.FindNavigationTarget(edmNavigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), this.parsedSegments, out edmPathExpression);
					}
					if (edmNavigationProperty.TargetMultiplicity() == EdmMultiplicity.One && edmNavigationSource is IEdmUnknownEntitySet)
					{
						throw new ODataException(Strings.RequestUriProcessor_TargetEntitySetNotFound(property.Name));
					}
					odataPathSegment = new NavigationPropertySegment(edmNavigationProperty, edmNavigationSource);
				}
				else
				{
					odataPathSegment = new PropertySegment((IEdmStructuralProperty)property);
					switch (property.Type.TypeKind())
					{
					case EdmTypeKind.Complex:
						odataPathSegment.TargetKind = RequestTargetKind.Resource;
						odataPathSegment.TargetEdmNavigationSource = previous.TargetEdmNavigationSource;
						goto IL_010E;
					case EdmTypeKind.Collection:
						if (property.Type.IsStructuredCollectionType())
						{
							odataPathSegment.TargetKind = RequestTargetKind.Resource;
							odataPathSegment.TargetEdmNavigationSource = previous.TargetEdmNavigationSource;
						}
						odataPathSegment.TargetKind = RequestTargetKind.Collection;
						goto IL_010E;
					case EdmTypeKind.Enum:
						odataPathSegment.TargetKind = RequestTargetKind.Enum;
						goto IL_010E;
					}
					odataPathSegment.TargetKind = RequestTargetKind.Primitive;
				}
				IL_010E:
				this.parsedSegments.Add(odataPathSegment);
				if (queryPortion != null && (!property.Type.IsCollection() || !property.Type.AsCollection().ElementType().IsEntity()))
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
				this.TryBindKeyFromParentheses(queryPortion);
				return;
			}
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00025AA0 File Offset: 0x00023CA0
		private bool IdentifierIs(string expected, string identifier)
		{
			return string.Equals(expected, identifier, this.configuration.EnableCaseInsensitiveUriFunctionIdentifier ? 5 : 4);
		}

		// Token: 0x04000700 RID: 1792
		internal static readonly Regex ContentIdRegex = PlatformHelper.CreateCompiled("^\\$[0-9]+$", 16);

		// Token: 0x04000701 RID: 1793
		private static readonly IList<string> EmptyList = new List<string>();

		// Token: 0x04000702 RID: 1794
		private readonly Queue<string> segmentQueue = new Queue<string>();

		// Token: 0x04000703 RID: 1795
		private readonly List<ODataPathSegment> parsedSegments = new List<ODataPathSegment>();

		// Token: 0x04000704 RID: 1796
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x04000705 RID: 1797
		private bool nextSegmentMustReferToMetadata;
	}
}
