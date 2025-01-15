using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x02000209 RID: 521
	internal sealed class ODataPathParser
	{
		// Token: 0x060012C1 RID: 4801 RVA: 0x00044360 File Offset: 0x00042560
		internal ODataPathParser(ODataUriParserConfiguration configuration)
		{
			this.configuration = configuration;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00044388 File Offset: 0x00042588
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

		// Token: 0x060012C3 RID: 4803 RVA: 0x000443FC File Offset: 0x000425FC
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

		// Token: 0x060012C4 RID: 4804 RVA: 0x00044518 File Offset: 0x00042718
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

		// Token: 0x060012C5 RID: 4805 RVA: 0x000445FC File Offset: 0x000427FC
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

		// Token: 0x060012C6 RID: 4806 RVA: 0x00044704 File Offset: 0x00042904
		private static RequestTargetKind TargetKindFromType(IEdmType type)
		{
			switch (type.TypeKind)
			{
			case EdmTypeKind.Entity:
				return RequestTargetKind.Resource;
			case EdmTypeKind.Complex:
				return RequestTargetKind.ComplexObject;
			case EdmTypeKind.Collection:
				if (type.IsEntityOrEntityCollectionType())
				{
					return RequestTargetKind.Resource;
				}
				return RequestTargetKind.Collection;
			case EdmTypeKind.Enum:
				return RequestTargetKind.Enum;
			case EdmTypeKind.TypeDefinition:
				return RequestTargetKind.Primitive;
			}
			return RequestTargetKind.Primitive;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00044750 File Offset: 0x00042950
		private static void CheckSingleResult(bool isSingleResult, string identifier)
		{
			if (!isSingleResult)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_CannotQueryCollections(identifier));
			}
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00044764 File Offset: 0x00042964
		private static void DetermineEntitySetForSegment(string identifier, IEdmTypeReference returnType, ODataPathSegment segment, IEdmEntitySetBase targetset, IEdmOperation singleOperation)
		{
			if (returnType != null)
			{
				segment.TargetEdmNavigationSource = targetset;
				segment.TargetEdmType = returnType.Definition;
				segment.TargetKind = ODataPathParser.TargetKindFromType(segment.TargetEdmType);
				segment.SingleResult = !singleOperation.ReturnType.IsCollection();
				return;
			}
			segment.TargetEdmNavigationSource = null;
			segment.TargetEdmType = null;
			segment.TargetKind = RequestTargetKind.VoidOperation;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x000447C4 File Offset: 0x000429C4
		private bool TryGetNextSegmentText(out string segmentText)
		{
			return this.TryGetNextSegmentText(false, out segmentText);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000447D0 File Offset: 0x000429D0
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
				this.ThrowIfMustBeLeafSegment(this.parsedSegments[this.parsedSegments.Count - 1]);
			}
			return true;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00044854 File Offset: 0x00042A54
		private bool TryHandleAsKeySegment(string segmentText)
		{
			ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 1];
			KeySegment keySegment = this.FindPreviousKeySegment();
			KeySegment keySegment2;
			if (!this.nextSegmentMustReferToMetadata && SegmentKeyHandler.TryHandleSegmentAsKey(segmentText, odataPathSegment, keySegment, this.configuration.UrlConventions.UrlConvention, out keySegment2, this.configuration.EnableUriTemplateParsing, this.configuration.Resolver))
			{
				this.parsedSegments.Add(keySegment2);
				return true;
			}
			return false;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x000448D5 File Offset: 0x00042AD5
		private KeySegment FindPreviousKeySegment()
		{
			return (KeySegment)Enumerable.LastOrDefault<ODataPathSegment>(this.parsedSegments, (ODataPathSegment s) => s is KeySegment);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00044904 File Offset: 0x00042B04
		private void ThrowIfMustBeLeafSegment(ODataPathSegment previous)
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
			if (previous.TargetKind == RequestTargetKind.Batch || previous.TargetKind == RequestTargetKind.Metadata || previous.TargetKind == RequestTargetKind.PrimitiveValue || previous.TargetKind == RequestTargetKind.OpenPropertyValue || previous.TargetKind == RequestTargetKind.EnumValue || previous.TargetKind == RequestTargetKind.MediaResource || previous.TargetKind == RequestTargetKind.VoidOperation || previous.TargetKind == RequestTargetKind.Nothing)
			{
				throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_MustBeLeafSegment(previous.Identifier));
			}
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00044A5C File Offset: 0x00042C5C
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

		// Token: 0x060012CF RID: 4815 RVA: 0x00044AE4 File Offset: 0x00042CE4
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
			IEdmNavigationSource edmNavigationSource = this.parsedSegments[this.parsedSegments.Count - 1].TargetEdmNavigationSource.FindNavigationTarget(navigationPropertySegment.NavigationProperty);
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

		// Token: 0x060012D0 RID: 4816 RVA: 0x00044C38 File Offset: 0x00042E38
		private bool TryBindKeyFromParentheses(string parenthesesSection)
		{
			if (parenthesesSection == null)
			{
				return false;
			}
			ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 1];
			KeySegment keySegment = this.FindPreviousKeySegment();
			ODataPathSegment odataPathSegment2;
			if (!SegmentKeyHandler.TryCreateKeySegmentFromParentheses(odataPathSegment, keySegment, parenthesesSection, out odataPathSegment2, this.configuration.EnableUriTemplateParsing, this.configuration.Resolver))
			{
				return false;
			}
			this.parsedSegments.Add(odataPathSegment2);
			return true;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00044C9C File Offset: 0x00042E9C
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
			else if (odataPathSegment.TargetKind == RequestTargetKind.OpenProperty)
			{
				odataPathSegment2.TargetKind = RequestTargetKind.OpenPropertyValue;
			}
			else
			{
				odataPathSegment2.TargetKind = RequestTargetKind.MediaResource;
			}
			this.parsedSegments.Add(odataPathSegment2);
			return true;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00044D88 File Offset: 0x00042F88
		private void CreateOpenPropertySegment(ODataPathSegment previous, string identifier, string parenthesisExpression)
		{
			ODataPathSegment odataPathSegment = new OpenPropertySegment(identifier);
			if (previous.TargetEdmType != null && !previous.TargetEdmType.IsOpenType())
			{
				throw ExceptionUtil.CreateResourceNotFoundError(odataPathSegment.Identifier);
			}
			if (parenthesisExpression != null)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.OpenNavigationPropertiesNotSupportedOnOpenTypes(odataPathSegment.Identifier));
			}
			this.parsedSegments.Add(odataPathSegment);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00044DE0 File Offset: 0x00042FE0
		private void CreateNamedStreamSegment(ODataPathSegment previous, IEdmProperty streamProperty)
		{
			ODataPathSegment odataPathSegment = new PropertySegment((IEdmStructuralProperty)streamProperty);
			odataPathSegment.TargetKind = RequestTargetKind.MediaResource;
			odataPathSegment.SingleResult = true;
			odataPathSegment.TargetEdmType = previous.TargetEdmType;
			this.parsedSegments.Add(odataPathSegment);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00044E20 File Offset: 0x00043020
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Uri parsing does not go through the same resolvers/settings that payload reading/writing does.")]
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
				throw ExceptionUtil.CreateResourceNotFoundError(text);
			}
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00044F04 File Offset: 0x00043104
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

		// Token: 0x060012D6 RID: 4822 RVA: 0x00044F88 File Offset: 0x00043188
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Uri parsing does not go through the same resolvers/settings that payload reading/writing does.")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IEdmModel", Justification = "The spelling is correct.")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:Do not use string.format", Justification = "Will be removed when string freeze is over.")]
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
			ODataPathSegment odataPathSegment = new OperationImportSegment(new IEdmOperationImport[] { edmOperationImport }, edmEntitySetBase, collection);
			ODataPathParser.DetermineEntitySetForSegment(identifier, returnType, odataPathSegment, edmEntitySetBase, edmOperationImport.Operation);
			this.parsedSegments.Add(odataPathSegment);
			this.TryBindKeySegmentIfNoResolvedParametersAndParathesisValueExsts(parenthesisExpression, returnType, collection, odataPathSegment);
			return true;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00045010 File Offset: 0x00043210
		private void TryBindKeySegmentIfNoResolvedParametersAndParathesisValueExsts(string parenthesisExpression, IEdmTypeReference returnType, ICollection<OperationSegmentParameter> resolvedParameters, ODataPathSegment segment)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = returnType as IEdmCollectionTypeReference;
			if (edmCollectionTypeReference != null && edmCollectionTypeReference.ElementType().IsEntity() && resolvedParameters == null && parenthesisExpression != null && this.TryBindKeyFromParentheses(parenthesisExpression))
			{
				this.ThrowIfMustBeLeafSegment(segment);
			}
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0004504C File Offset: 0x0004324C
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Uri parsing does not go through the same resolvers/settings that payload reading/writing does.")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IEdmModel", Justification = "The spelling is correct.")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:Do not use string.format", Justification = "Will be removed when string freeze is over.")]
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
			ODataPathSegment odataPathSegment = new OperationSegment(new IEdmOperation[] { edmOperation }, collection, edmEntitySetBase)
			{
				Identifier = identifier
			};
			ODataPathParser.DetermineEntitySetForSegment(identifier, returnType, odataPathSegment, edmEntitySetBase, edmOperation);
			this.parsedSegments.Add(odataPathSegment);
			this.TryBindKeySegmentIfNoResolvedParametersAndParathesisValueExsts(parenthesisExpression, returnType, collection, odataPathSegment);
			return true;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00045130 File Offset: 0x00043330
		private void CreateNextSegment(string text)
		{
			if (!this.configuration.UrlConventions.UrlConvention.ODataSimplified && this.TryHandleAsKeySegment(text))
			{
				return;
			}
			if (this.configuration.EnableUriTemplateParsing && UriTemplateParser.IsValidTemplateLiteral(text))
			{
				this.parsedSegments.Add(new PathTemplateSegment(text));
				return;
			}
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
			if (this.TryCreateTypeNameSegment(odataPathSegment, text2, text3))
			{
				return;
			}
			if (this.TryCreateSegmentForOperation(odataPathSegment, text2, text3))
			{
				return;
			}
			if (this.configuration.UrlConventions.UrlConvention.ODataSimplified && this.TryHandleAsKeySegment(text))
			{
				return;
			}
			ODataPathParser.CheckSingleResult(odataPathSegment.SingleResult, odataPathSegment.Identifier);
			this.CreateOpenPropertySegment(odataPathSegment, text2, text3);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0004526C File Offset: 0x0004346C
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

		// Token: 0x060012DB RID: 4827 RVA: 0x000452E8 File Offset: 0x000434E8
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Uri parsing does not go through the same resolvers/settings that payload reading/writing does.")]
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
			ODataPathSegment odataPathSegment = new TypeSegment(edmType3, previous.TargetEdmNavigationSource)
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

		// Token: 0x060012DC RID: 4828 RVA: 0x0004540C File Offset: 0x0004360C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IEdmModel", Justification = "The spelling is correct.")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:Do not use string.format", Justification = "Will be removed when string freeze is over.")]
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
					IEdmNavigationSource edmNavigationSource = previous.TargetEdmNavigationSource.FindNavigationTarget(edmNavigationProperty);
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
						odataPathSegment.TargetKind = RequestTargetKind.ComplexObject;
						goto IL_00BF;
					case EdmTypeKind.Collection:
						odataPathSegment.TargetKind = RequestTargetKind.Collection;
						goto IL_00BF;
					case EdmTypeKind.Enum:
						odataPathSegment.TargetKind = RequestTargetKind.Enum;
						goto IL_00BF;
					}
					odataPathSegment.TargetKind = RequestTargetKind.Primitive;
				}
				IL_00BF:
				this.parsedSegments.Add(odataPathSegment);
				if (queryPortion != null && (!property.Type.IsCollection() || !property.Type.AsCollection().ElementType().IsEntity()))
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
				this.TryBindKeyFromParentheses(queryPortion);
				return;
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00045519 File Offset: 0x00043719
		private bool IdentifierIs(string expected, string identifier)
		{
			return string.Equals(expected, identifier, this.configuration.EnableCaseInsensitiveUriFunctionIdentifier ? 5 : 4);
		}

		// Token: 0x04000810 RID: 2064
		internal static readonly Regex ContentIdRegex = PlatformHelper.CreateCompiled("^\\$[0-9]+$", 16);

		// Token: 0x04000811 RID: 2065
		private static readonly IList<string> EmptyList = new List<string>();

		// Token: 0x04000812 RID: 2066
		private readonly Queue<string> segmentQueue = new Queue<string>();

		// Token: 0x04000813 RID: 2067
		private readonly List<ODataPathSegment> parsedSegments = new List<ODataPathSegment>();

		// Token: 0x04000814 RID: 2068
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x04000815 RID: 2069
		private bool nextSegmentMustReferToMetadata;
	}
}
