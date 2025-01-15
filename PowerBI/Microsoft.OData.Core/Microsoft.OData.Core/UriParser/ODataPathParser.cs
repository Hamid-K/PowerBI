using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.Edm.Vocabularies.Community.V1;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015A RID: 346
	internal sealed class ODataPathParser
	{
		// Token: 0x06001196 RID: 4502 RVA: 0x000324C8 File Offset: 0x000306C8
		internal ODataPathParser(ODataUriParserConfiguration configuration)
		{
			this.configuration = configuration;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000324F0 File Offset: 0x000306F0
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
				if (segmentText[segmentText.Length - 1] != ')')
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

		// Token: 0x06001198 RID: 4504 RVA: 0x00032564 File Offset: 0x00030764
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
					IEdmNavigationSource edmNavigationSource = this.parsedSegments.Last<ODataPathSegment>().TranslateWith<IEdmNavigationSource>(new DetermineNavigationSourceTranslator());
					if (edmNavigationSource != null)
					{
						this.lastNavigationSource = edmNavigationSource;
					}
				}
			}
			catch (ODataUnrecognizedPathException ex)
			{
				ex.ParsedSegments = this.parsedSegments;
				ex.CurrentSegment = text2;
				ex.UnparsedSegments = this.segmentQueue.ToList<string>();
				throw;
			}
			List<ODataPathSegment> list = this.CreateValidatedPathSegments();
			this.parsedSegments.Clear();
			return list;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0003264C File Offset: 0x0003084C
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
			if (FunctionOverloadResolver.ResolveOperationImportFromList(identifier, collection.Select((FunctionParameterToken k) => k.ParameterName).ToList<string>(), configuration.Model, out matchingFunctionImport, configuration.Resolver))
			{
				IEdmOperation operation = matchingFunctionImport.Operation;
				boundParameters = FunctionCallBinder.BindSegmentParameters(configuration, operation, collection);
				return true;
			}
			boundParameters = null;
			return false;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0003272C File Offset: 0x0003092C
		private static bool TryBindingParametersAndMatchingOperation(string identifier, string parenthesisExpression, IEdmType bindingType, ODataUriParserConfiguration configuration, out ICollection<OperationSegmentParameter> boundParameters, out IEdmOperation matchingOperation)
		{
			if (identifier != null && identifier.IndexOf(".", StringComparison.Ordinal) == -1 && configuration.Resolver.GetType() == typeof(ODataUriResolver))
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
			if (FunctionOverloadResolver.ResolveOperationFromList(identifier, collection.Select((FunctionParameterToken k) => k.ParameterName).ToList<string>(), bindingType, configuration.Model, out matchingOperation, configuration.Resolver))
			{
				boundParameters = FunctionCallBinder.BindSegmentParameters(configuration, matchingOperation, collection);
				return true;
			}
			boundParameters = null;
			return false;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00032834 File Offset: 0x00030A34
		private static void CheckSingleResult(bool isSingleResult, string identifier)
		{
			if (!isSingleResult)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_CannotQueryCollections(identifier));
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00032845 File Offset: 0x00030A45
		private bool TryGetNextSegmentText(out string segmentText)
		{
			return this.TryGetNextSegmentText(false, out segmentText);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00032850 File Offset: 0x00030A50
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

		// Token: 0x0600119E RID: 4510 RVA: 0x000328D0 File Offset: 0x00030AD0
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

		// Token: 0x0600119F RID: 4511 RVA: 0x00032941 File Offset: 0x00030B41
		private KeySegment FindPreviousKeySegment()
		{
			return (KeySegment)this.parsedSegments.LastOrDefault((ODataPathSegment s) => s is KeySegment);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00032974 File Offset: 0x00030B74
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

		// Token: 0x060011A1 RID: 4513 RVA: 0x00032AC8 File Offset: 0x00030CC8
		private bool TryCreateCountSegment(string identifier, string parenthesisExpression)
		{
			if (!this.IdentifierIs("$count", identifier))
			{
				return false;
			}
			if (parenthesisExpression != null)
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

		// Token: 0x060011A2 RID: 4514 RVA: 0x00032B44 File Offset: 0x00030D44
		private FilterClause GenerateFilterClause(IEdmNavigationSource navigationSource, IEdmType targetEdmType, string filter)
		{
			ODataPathInfo odataPathInfo = new ODataPathInfo(targetEdmType, navigationSource);
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(this.configuration.Settings.FilterLimit, this.configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			QueryToken queryToken = uriQueryExpressionParser.ParseFilter(filter);
			BindingState bindingState = new BindingState(this.configuration, odataPathInfo.Segments.ToList<ODataPathSegment>())
			{
				ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(odataPathInfo.TargetEdmType.ToTypeReference(), odataPathInfo.TargetNavigationSource)
			};
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			FilterBinder filterBinder = new FilterBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), bindingState);
			return filterBinder.BindFilter(queryToken);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00032BEC File Offset: 0x00030DEC
		private bool TryCreateFilterSegment(string segmentText)
		{
			if (!segmentText.StartsWith("$filter", this.configuration.EnableCaseInsensitiveUriFunctionIdentifier ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
			{
				return false;
			}
			int length = "$filter".Length;
			if (segmentText.Length <= length + 2 || segmentText[length] != '(' || segmentText[segmentText.Length - 1] != ')')
			{
				throw new ODataException(Strings.RequestUriProcessor_FilterPathSegmentSyntaxError);
			}
			if (this.lastNavigationSource == null)
			{
				throw new ODataException(Strings.RequestUriProcessor_NoNavigationSourceFound("$filter"));
			}
			if (this.lastNavigationSource is IEdmSingleton || this.parsedSegments.Last<ODataPathSegment>() is KeySegment)
			{
				throw new ODataException(Strings.RequestUriProcessor_CannotApplyFilterOnSingleEntities(this.lastNavigationSource.Name));
			}
			string text = segmentText.Substring(length + 1, segmentText.Length - "$filter".Length - 2);
			TypeSegment typeSegment = this.parsedSegments.Last<ODataPathSegment>() as TypeSegment;
			IEdmNavigationSource edmNavigationSource = this.lastNavigationSource;
			IEdmType edmType;
			if (typeSegment != null)
			{
				edmType = typeSegment.TargetEdmType;
			}
			else
			{
				IEdmType edmType2 = this.lastNavigationSource.EntityType();
				edmType = edmType2;
			}
			FilterClause filterClause = this.GenerateFilterClause(edmNavigationSource, edmType, text);
			FilterSegment filterSegment = new FilterSegment(filterClause.Expression, filterClause.RangeVariable, this.lastNavigationSource);
			this.parsedSegments.Add(filterSegment);
			return true;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00032D24 File Offset: 0x00030F24
		private bool TryCreateEachSegment(string identifier, string parenthesisExpression)
		{
			if (!this.IdentifierIs("$each", identifier))
			{
				return false;
			}
			if (parenthesisExpression != null)
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
			ODataPathSegment odataPathSegment = this.parsedSegments.Last<ODataPathSegment>();
			if (this.lastNavigationSource == null)
			{
				throw new ODataException(Strings.RequestUriProcessor_NoNavigationSourceFound("$each"));
			}
			if (this.lastNavigationSource is IEdmSingleton || odataPathSegment is KeySegment)
			{
				throw new ODataException(Strings.RequestUriProcessor_CannotApplyEachOnSingleEntities(this.lastNavigationSource.Name));
			}
			EachSegment eachSegment = new EachSegment(this.lastNavigationSource, odataPathSegment.TargetEdmType.AsElementType());
			this.parsedSegments.Add(eachSegment);
			return true;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00032DC0 File Offset: 0x00030FC0
		private bool TryCreateEntityReferenceSegment(string identifier, string parenthesisExpression)
		{
			if (!this.IdentifierIs("$ref", identifier))
			{
				return false;
			}
			if (parenthesisExpression != null)
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
			int num = this.parsedSegments.Count - 1;
			while (num > 0 && this.parsedSegments[num] is KeySegment)
			{
				num--;
			}
			NavigationPropertySegment navigationPropertySegment = this.parsedSegments[num] as NavigationPropertySegment;
			if (navigationPropertySegment != null)
			{
				if (navigationPropertySegment.TargetKind != RequestTargetKind.Resource)
				{
					throw ExceptionUtil.CreateBadRequestError(Strings.PathParser_EntityReferenceNotSupported(navigationPropertySegment.Identifier));
				}
				IEdmPathExpression edmPathExpression;
				IEdmNavigationSource edmNavigationSource = this.parsedSegments[num - 1].TargetEdmNavigationSource.FindNavigationTarget(navigationPropertySegment.NavigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), this.parsedSegments, out edmPathExpression);
				if (edmNavigationSource == null)
				{
					throw ExceptionUtil.CreateResourceNotFoundError(navigationPropertySegment.NavigationProperty.Name);
				}
				NavigationPropertyLinkSegment navigationPropertyLinkSegment = new NavigationPropertyLinkSegment(navigationPropertySegment.NavigationProperty, edmNavigationSource);
				this.parsedSegments[num] = navigationPropertyLinkSegment;
			}
			else
			{
				ODataPathSegment odataPathSegment = this.parsedSegments.Last<ODataPathSegment>();
				if (odataPathSegment.TargetKind != RequestTargetKind.Resource)
				{
					throw ExceptionUtil.CreateBadRequestError(Strings.PathParser_EntityReferenceNotSupported(odataPathSegment.Identifier));
				}
				ReferenceSegment referenceSegment = new ReferenceSegment(this.lastNavigationSource);
				referenceSegment.SingleResult = odataPathSegment.SingleResult;
				this.parsedSegments.Add(referenceSegment);
			}
			string text;
			if (this.TryGetNextSegmentText(out text))
			{
				throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_MustBeLeafSegment("$ref"));
			}
			return true;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00032F18 File Offset: 0x00031118
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

		// Token: 0x060011A7 RID: 4519 RVA: 0x00032F7C File Offset: 0x0003117C
		private bool TryCreateValueSegment(string identifier, string parenthesisExpression)
		{
			if (!this.IdentifierIs("$value", identifier))
			{
				return false;
			}
			if (parenthesisExpression != null)
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

		// Token: 0x060011A8 RID: 4520 RVA: 0x00033060 File Offset: 0x00031260
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

		// Token: 0x060011A9 RID: 4521 RVA: 0x000330F4 File Offset: 0x000312F4
		private void CreateNamedStreamSegment(ODataPathSegment previous, IEdmProperty streamProperty)
		{
			ODataPathSegment odataPathSegment = new PropertySegment((IEdmStructuralProperty)streamProperty);
			odataPathSegment.TargetKind = RequestTargetKind.MediaResource;
			odataPathSegment.SingleResult = true;
			odataPathSegment.TargetEdmType = previous.TargetEdmType;
			this.parsedSegments.Add(odataPathSegment);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00033134 File Offset: 0x00031334
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
				if (this.IdentifierIs("$filter", text))
				{
					throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_FilterOnRoot);
				}
				if (this.IdentifierIs("$each", text))
				{
					throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_EachOnRoot);
				}
				if (this.IdentifierIs("$ref", text))
				{
					throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_RefOnRoot);
				}
				if (this.configuration.BatchReferenceCallback != null && ODataPathParser.ContentIdRegex.IsMatch(text))
				{
					if (text2 != null)
					{
						throw ExceptionUtil.CreateSyntaxError();
					}
					BatchReferenceSegment batchReferenceSegment = this.configuration.BatchReferenceCallback(text);
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

		// Token: 0x060011AB RID: 4523 RVA: 0x00033264 File Offset: 0x00031464
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

		// Token: 0x060011AC RID: 4524 RVA: 0x000332DC File Offset: 0x000314DC
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

		// Token: 0x060011AD RID: 4525 RVA: 0x00033348 File Offset: 0x00031548
		private void TryBindKeySegmentIfNoResolvedParametersAndParathesisValueExsts(string parenthesisExpression, IEdmTypeReference returnType, ICollection<OperationSegmentParameter> resolvedParameters, ODataPathSegment segment)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = returnType as IEdmCollectionTypeReference;
			if (edmCollectionTypeReference != null && edmCollectionTypeReference.ElementType().IsEntity() && resolvedParameters == null && parenthesisExpression != null && this.TryBindKeyFromParentheses(parenthesisExpression))
			{
				ODataPathParser.ThrowIfMustBeLeafSegment(segment);
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00033384 File Offset: 0x00031584
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IEdmModel", Justification = "The spelling is correct.")]
		private bool TryCreateSegmentForOperation(ODataPathSegment previousSegment, string identifier, string parenthesisExpression)
		{
			IEdmType edmType = null;
			if (previousSegment != null)
			{
				edmType = ((previousSegment is EachSegment) ? previousSegment.TargetEdmType : previousSegment.EdmType);
			}
			if (!string.IsNullOrEmpty(identifier) && identifier[0] == ':' && edmType != null)
			{
				identifier = ODataPathParser.ResolveEscapeFunction(identifier, edmType, this.configuration.Model, out parenthesisExpression);
			}
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
			this.CheckOperationTypeCastSegmentRestriction(edmOperation);
			ODataPathSegment odataPathSegment = new OperationSegment(edmOperation, collection, edmEntitySetBase)
			{
				Identifier = identifier
			};
			this.parsedSegments.Add(odataPathSegment);
			this.TryBindKeySegmentIfNoResolvedParametersAndParathesisValueExsts(parenthesisExpression, returnType, collection, odataPathSegment);
			return true;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0003348C File Offset: 0x0003168C
		private void CreateNextSegment(string text)
		{
			string text2;
			string text3;
			ODataPathParser.ExtractSegmentIdentifierAndParenthesisExpression(text, out text2, out text3);
			if (this.TryCreateValueSegment(text2, text3))
			{
				return;
			}
			ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 1];
			if (odataPathSegment.TargetKind == RequestTargetKind.Primitive)
			{
				throw ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_ValueSegmentAfterScalarPropertySegment(odataPathSegment.Identifier, text));
			}
			if (this.TryCreateEntityReferenceSegment(text2, text3))
			{
				return;
			}
			if (this.TryCreateCountSegment(text2, text3))
			{
				return;
			}
			if (this.TryCreateFilterSegment(text))
			{
				return;
			}
			if (this.TryCreateEachSegment(text2, text3))
			{
				return;
			}
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

		// Token: 0x060011B0 RID: 4528 RVA: 0x000335B4 File Offset: 0x000317B4
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

		// Token: 0x060011B1 RID: 4529 RVA: 0x0003362C File Offset: 0x0003182C
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
			this.CheckTypeCastSegmentRestriction(previous, edmType);
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

		// Token: 0x060011B2 RID: 4530 RVA: 0x00033758 File Offset: 0x00031958
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

		// Token: 0x060011B3 RID: 4531 RVA: 0x000338B4 File Offset: 0x00031AB4
		private bool IdentifierIs(string expected, string identifier)
		{
			return string.Equals(expected, identifier, this.configuration.EnableCaseInsensitiveUriFunctionIdentifier ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000338D0 File Offset: 0x00031AD0
		private List<ODataPathSegment> CreateValidatedPathSegments()
		{
			List<ODataPathSegment> list = new List<ODataPathSegment>(this.parsedSegments.Count);
			int i = 0;
			int count = this.parsedSegments.Count;
			while (i < count)
			{
				this.CheckDollarEachSegmentRestrictions(i);
				list.Add(this.parsedSegments[i]);
				i++;
			}
			return list;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00033920 File Offset: 0x00031B20
		private void CheckDollarEachSegmentRestrictions(int index)
		{
			int num = this.parsedSegments.Count - index - 1;
			if (this.parsedSegments[index] is EachSegment && num > 0)
			{
				if (num > 1)
				{
					throw new ODataException(Strings.RequestUriProcessor_OnlySingleOperationCanFollowEachPathSegment);
				}
				if (!(this.parsedSegments[index + 1] is OperationSegment))
				{
					throw new ODataException(Strings.RequestUriProcessor_OnlySingleOperationCanFollowEachPathSegment);
				}
			}
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00033984 File Offset: 0x00031B84
		private void CheckTypeCastSegmentRestriction(ODataPathSegment previous, IEdmType targetEdmType)
		{
			IEdmType edmType = previous.TargetEdmType.AsElementType();
			if (edmType == targetEdmType)
			{
				return;
			}
			string text = targetEdmType.FullTypeName();
			SingletonSegment singletonSegment = previous as SingletonSegment;
			if (singletonSegment != null)
			{
				ODataPathParser.VerifyDerivedTypeConstraints(this.configuration.Model, singletonSegment.Singleton, text, "singleton", singletonSegment.Singleton.Name);
				return;
			}
			EntitySetSegment entitySetSegment = previous as EntitySetSegment;
			if (entitySetSegment != null)
			{
				ODataPathParser.VerifyDerivedTypeConstraints(this.configuration.Model, entitySetSegment.EntitySet, text, "entity set", entitySetSegment.EntitySet.Name);
				return;
			}
			KeySegment keySegment = previous as KeySegment;
			NavigationPropertySegment navigationPropertySegment;
			if (keySegment != null)
			{
				ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 2];
				entitySetSegment = odataPathSegment as EntitySetSegment;
				navigationPropertySegment = odataPathSegment as NavigationPropertySegment;
				if (entitySetSegment != null || navigationPropertySegment != null)
				{
					IEdmVocabularyAnnotatable edmVocabularyAnnotatable;
					string text2;
					string text3;
					if (entitySetSegment != null)
					{
						edmVocabularyAnnotatable = entitySetSegment.EntitySet;
						text2 = "entity set";
						text3 = entitySetSegment.EntitySet.Name;
					}
					else
					{
						edmVocabularyAnnotatable = navigationPropertySegment.NavigationProperty;
						text2 = "navigation property";
						text3 = navigationPropertySegment.NavigationProperty.Name;
					}
					ODataPathParser.VerifyDerivedTypeConstraints(this.configuration.Model, edmVocabularyAnnotatable, text, text2, text3);
				}
				return;
			}
			navigationPropertySegment = previous as NavigationPropertySegment;
			if (navigationPropertySegment != null)
			{
				ODataPathParser.VerifyDerivedTypeConstraints(this.configuration.Model, navigationPropertySegment.NavigationProperty, text, "navigation property", navigationPropertySegment.NavigationProperty.Name);
				return;
			}
			PropertySegment propertySegment = previous as PropertySegment;
			if (propertySegment != null)
			{
				IEdmProperty property = propertySegment.Property;
				ODataPathParser.VerifyDerivedTypeConstraints(this.configuration.Model, property, text, "property", property.Name);
				return;
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00033B10 File Offset: 0x00031D10
		private void CheckOperationTypeCastSegmentRestriction(IEdmOperation operation)
		{
			if (this.parsedSegments == null)
			{
				return;
			}
			TypeSegment typeSegment = this.parsedSegments.LastOrDefault((ODataPathSegment s) => s is TypeSegment) as TypeSegment;
			if (typeSegment == null)
			{
				return;
			}
			ODataPathSegment odataPathSegment = this.parsedSegments[this.parsedSegments.Count - 1];
			ODataPathSegment odataPathSegment2 = ((this.parsedSegments.Count >= 2) ? this.parsedSegments[this.parsedSegments.Count - 2] : null);
			if (typeSegment == odataPathSegment || (typeSegment == odataPathSegment2 && odataPathSegment is KeySegment))
			{
				if (!operation.IsBound)
				{
					return;
				}
				string text = typeSegment.TargetEdmType.FullTypeName();
				IEdmOperationParameter edmOperationParameter = operation.Parameters.First<IEdmOperationParameter>();
				IEdmType edmType = edmOperationParameter.Type.Definition;
				edmType = edmType.AsElementType();
				if (text == edmType.FullTypeName())
				{
					return;
				}
				ODataPathParser.VerifyDerivedTypeConstraints(this.configuration.Model, edmOperationParameter, text, "operation", operation.Name);
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00033C14 File Offset: 0x00031E14
		private static void VerifyDerivedTypeConstraints(IEdmModel model, IEdmVocabularyAnnotatable target, string fullTypeName, string kind, string name)
		{
			IEnumerable<string> derivedTypeConstraints = model.GetDerivedTypeConstraints(target);
			if (derivedTypeConstraints == null || derivedTypeConstraints.Any((string d) => d == fullTypeName))
			{
				return;
			}
			throw new ODataException(Strings.PathParser_TypeCastOnlyAllowedInDerivedTypeConstraint(fullTypeName, kind, name));
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00033C64 File Offset: 0x00031E64
		private static string ResolveEscapeFunction(string identifier, IEdmType bindingType, IEdmModel model, out string parenthesisExpression)
		{
			bool isComposableRequired = identifier.Length >= 2 && identifier[identifier.Length - 1] == ':';
			IEdmFunction edmFunction = model.FindBoundOperations(bindingType).OfType<IEdmFunction>().FirstOrDefault((IEdmFunction f) => f.IsComposable == isComposableRequired && ODataPathParser.IsUrlEscapeFunction(model, f));
			if (edmFunction == null)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_NoBoundEscapeFunctionSupported(bindingType.FullTypeName()));
			}
			if (edmFunction.Parameters == null || edmFunction.Parameters.Count<IEdmOperationParameter>() != 2 || !edmFunction.Parameters.ElementAt(1).Type.IsString())
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_EscapeFunctionMustHaveOneStringParameter(edmFunction.FullName()));
			}
			parenthesisExpression = edmFunction.Parameters.ElementAt(1).Name + "='" + (isComposableRequired ? identifier.Substring(1, identifier.Length - 2) : identifier.Substring(1)) + "'";
			return edmFunction.FullName();
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00033D60 File Offset: 0x00031F60
		internal static bool IsUrlEscapeFunction(IEdmModel model, IEdmFunction function)
		{
			IEdmVocabularyAnnotation edmVocabularyAnnotation = model.FindVocabularyAnnotations(function, CommunityVocabularyModel.UrlEscapeFunctionTerm).FirstOrDefault<IEdmVocabularyAnnotation>();
			if (edmVocabularyAnnotation != null)
			{
				if (edmVocabularyAnnotation.Value == null)
				{
					return true;
				}
				IEdmBooleanConstantExpression edmBooleanConstantExpression = edmVocabularyAnnotation.Value as IEdmBooleanConstantExpression;
				if (edmBooleanConstantExpression != null)
				{
					return edmBooleanConstantExpression.Value;
				}
			}
			return false;
		}

		// Token: 0x04000816 RID: 2070
		internal static readonly Regex ContentIdRegex = PlatformHelper.CreateCompiled("^\\$[0-9]+$", RegexOptions.Singleline);

		// Token: 0x04000817 RID: 2071
		private static readonly IList<string> EmptyList = new List<string>();

		// Token: 0x04000818 RID: 2072
		private readonly Queue<string> segmentQueue = new Queue<string>();

		// Token: 0x04000819 RID: 2073
		private readonly List<ODataPathSegment> parsedSegments = new List<ODataPathSegment>();

		// Token: 0x0400081A RID: 2074
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x0400081B RID: 2075
		private bool nextSegmentMustReferToMetadata;

		// Token: 0x0400081C RID: 2076
		private IEdmNavigationSource lastNavigationSource;
	}
}
