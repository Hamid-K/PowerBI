using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000147 RID: 327
	public class ODataUriResolver
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0002F330 File Offset: 0x0002D530
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x0002F338 File Offset: 0x0002D538
		public virtual bool EnableCaseInsensitive { get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0002F341 File Offset: 0x0002D541
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x0002F349 File Offset: 0x0002D549
		public virtual bool EnableNoDollarQueryOptions { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0002F352 File Offset: 0x0002D552
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x0002F35A File Offset: 0x0002D55A
		public TypeFacetsPromotionRules TypeFacetsPromotionRules
		{
			get
			{
				return this.typeFacetsPromotionRules;
			}
			set
			{
				this.typeFacetsPromotionRules = value;
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0002F363 File Offset: 0x0002D563
		public virtual void PromoteBinaryOperandTypes(BinaryOperatorKind binaryOperatorKind, ref SingleValueNode leftNode, ref SingleValueNode rightNode, out IEdmTypeReference typeReference)
		{
			typeReference = null;
			BinaryOperatorBinder.PromoteOperandTypes(binaryOperatorKind, ref leftNode, ref rightNode, this.typeFacetsPromotionRules);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0002F378 File Offset: 0x0002D578
		public virtual IEdmNavigationSource ResolveNavigationSource(IEdmModel model, string identifier)
		{
			IEdmNavigationSource edmNavigationSource = model.FindDeclaredNavigationSource(identifier);
			if (edmNavigationSource != null || !this.EnableCaseInsensitive)
			{
				return edmNavigationSource;
			}
			IEdmEntityContainer entityContainer = model.EntityContainer;
			if (entityContainer == null)
			{
				return null;
			}
			List<IEdmNavigationSource> list = (from source in entityContainer.Elements.OfType<IEdmNavigationSource>()
				where string.Equals(identifier, source.Name, StringComparison.OrdinalIgnoreCase)
				select source).ToList<IEdmNavigationSource>();
			if (list.Count > 1)
			{
				throw new ODataException(Strings.UriParserMetadata_MultipleMatchingNavigationSourcesFound(identifier));
			}
			return list.SingleOrDefault<IEdmNavigationSource>();
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0002F3FC File Offset: 0x0002D5FC
		public virtual IEdmProperty ResolveProperty(IEdmStructuredType type, string propertyName)
		{
			IEdmProperty edmProperty = type.FindProperty(propertyName);
			if (edmProperty != null || !this.EnableCaseInsensitive)
			{
				return edmProperty;
			}
			List<IEdmProperty> list = (from _ in type.Properties()
				where string.Equals(propertyName, _.Name, StringComparison.OrdinalIgnoreCase)
				select _).ToList<IEdmProperty>();
			if (list.Count > 1)
			{
				throw new ODataException(Strings.UriParserMetadata_MultipleMatchingPropertiesFound(propertyName, type.FullTypeName()));
			}
			return list.SingleOrDefault<IEdmProperty>();
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0002F474 File Offset: 0x0002D674
		public virtual IEdmTerm ResolveTerm(IEdmModel model, string termName)
		{
			IEdmTerm edmTerm = model.FindTerm(termName);
			if (edmTerm != null || !this.EnableCaseInsensitive)
			{
				return edmTerm;
			}
			IList<IEdmTerm> list = ODataUriResolver.FindAcrossModels<IEdmTerm>(model, termName, true);
			if (list.Count > 1)
			{
				throw new ODataException(Strings.UriParserMetadata_MultipleMatchingTypesFound(termName));
			}
			return list.SingleOrDefault<IEdmTerm>();
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0002F4BC File Offset: 0x0002D6BC
		public virtual IEdmSchemaType ResolveType(IEdmModel model, string typeName)
		{
			IEdmSchemaType edmSchemaType = model.FindType(typeName);
			if (edmSchemaType != null || !this.EnableCaseInsensitive)
			{
				return edmSchemaType;
			}
			IList<IEdmSchemaType> list = ODataUriResolver.FindAcrossModels<IEdmSchemaType>(model, typeName, true);
			if (list.Count > 1)
			{
				throw new ODataException(Strings.UriParserMetadata_MultipleMatchingTypesFound(typeName));
			}
			return list.SingleOrDefault<IEdmSchemaType>();
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0002F504 File Offset: 0x0002D704
		public virtual IEnumerable<IEdmOperation> ResolveBoundOperations(IEdmModel model, string identifier, IEdmType bindingType)
		{
			IEnumerable<IEdmOperation> enumerable = model.FindBoundOperations(identifier, bindingType);
			if (enumerable.Any<IEdmOperation>() || !this.EnableCaseInsensitive)
			{
				return enumerable;
			}
			IList<IEdmOperation> list = ODataUriResolver.FindAcrossModels<IEdmOperation>(model, identifier, true);
			if (list != null && list.Count<IEdmOperation>() > 0)
			{
				IList<IEdmOperation> list2 = new List<IEdmOperation>();
				for (int i = 0; i < list.Count<IEdmOperation>(); i++)
				{
					if (list[i].HasEquivalentBindingType(bindingType))
					{
						list2.Add(list[i]);
					}
				}
				return list2;
			}
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0002F57C File Offset: 0x0002D77C
		public virtual IEnumerable<IEdmOperation> ResolveUnboundOperations(IEdmModel model, string identifier)
		{
			IEnumerable<IEdmOperation> enumerable = model.FindOperations(identifier);
			if (enumerable.Any<IEdmOperation>() || !this.EnableCaseInsensitive)
			{
				return enumerable;
			}
			IList<IEdmOperation> list = ODataUriResolver.FindAcrossModels<IEdmOperation>(model, identifier, true);
			if (list != null && list.Count<IEdmOperation>() > 0)
			{
				IList<IEdmOperation> list2 = new List<IEdmOperation>();
				for (int i = 0; i < list.Count<IEdmOperation>(); i++)
				{
					if (!list[i].IsBound)
					{
						list2.Add(list[i]);
					}
				}
				return list2;
			}
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0002F5F4 File Offset: 0x0002D7F4
		public virtual IEnumerable<IEdmOperationImport> ResolveOperationImports(IEdmModel model, string identifier)
		{
			IEnumerable<IEdmOperationImport> enumerable = model.FindDeclaredOperationImports(identifier);
			if (enumerable.Any<IEdmOperationImport>() || !this.EnableCaseInsensitive)
			{
				return enumerable;
			}
			IEdmEntityContainer entityContainer = model.EntityContainer;
			if (entityContainer == null)
			{
				return null;
			}
			return from source in entityContainer.Elements.OfType<IEdmOperationImport>()
				where string.Equals(identifier, source.Name, StringComparison.OrdinalIgnoreCase)
				select source;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0002F658 File Offset: 0x0002D858
		public virtual IDictionary<IEdmOperationParameter, SingleValueNode> ResolveOperationParameters(IEdmOperation operation, IDictionary<string, SingleValueNode> input)
		{
			Dictionary<IEdmOperationParameter, SingleValueNode> dictionary = new Dictionary<IEdmOperationParameter, SingleValueNode>(EqualityComparer<IEdmOperationParameter>.Default);
			foreach (KeyValuePair<string, SingleValueNode> keyValuePair in input)
			{
				IEdmOperationParameter edmOperationParameter;
				if (this.EnableCaseInsensitive)
				{
					edmOperationParameter = ODataUriResolver.ResolveOperationParameterNameCaseInsensitive(operation, keyValuePair.Key);
				}
				else
				{
					edmOperationParameter = operation.FindParameter(keyValuePair.Key);
				}
				if (edmOperationParameter == null)
				{
					throw new ODataException(Strings.ODataParameterWriterCore_ParameterNameNotFoundInOperation(keyValuePair.Key, operation.Name));
				}
				dictionary.Add(edmOperationParameter, keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0002F6F8 File Offset: 0x0002D8F8
		public virtual IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IList<string> positionalValues, Func<IEdmTypeReference, string, object> convertFunc)
		{
			List<IEdmStructuralProperty> list = type.Key().ToList<IEdmStructuralProperty>();
			if (list.Count != positionalValues.Count)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.BadRequest_KeyCountMismatch(type.FullName()));
			}
			List<KeyValuePair<string, object>> list2 = new List<KeyValuePair<string, object>>(positionalValues.Count);
			for (int i = 0; i < list.Count; i++)
			{
				string text = positionalValues[i];
				IEdmProperty edmProperty = list[i];
				object obj = convertFunc(edmProperty.Type, text);
				if (obj == null)
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
				list2.Add(new KeyValuePair<string, object>(edmProperty.Name, obj));
			}
			return list2;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0002F790 File Offset: 0x0002D990
		public virtual IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IDictionary<string, string> namedValues, Func<IEdmTypeReference, string, object> convertFunc)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
			List<IEdmStructuralProperty> list = type.Key().ToList<IEdmStructuralProperty>();
			if (list.Count != namedValues.Count)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.BadRequest_KeyCountMismatch(type.FullName()));
			}
			using (List<IEdmStructuralProperty>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IEdmStructuralProperty property = enumerator.Current;
					string text;
					if (!namedValues.TryGetValue(property.Name, out text))
					{
						if (!this.EnableCaseInsensitive)
						{
							throw ExceptionUtil.CreateSyntaxError();
						}
						List<string> list2 = namedValues.Keys.Where((string key) => string.Equals(property.Name, key, StringComparison.OrdinalIgnoreCase)).ToList<string>();
						if (list2.Count > 1)
						{
							throw new ODataException(Strings.UriParserMetadata_MultipleMatchingKeysFound(property.Name));
						}
						if (list2.Count == 0)
						{
							throw ExceptionUtil.CreateSyntaxError();
						}
						text = namedValues[list2.Single<string>()];
					}
					object obj = convertFunc(property.Type, text);
					if (obj == null)
					{
						throw ExceptionUtil.CreateSyntaxError();
					}
					dictionary[property.Name] = obj;
				}
			}
			return dictionary;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0002F8D4 File Offset: 0x0002DAD4
		internal static IEdmOperationParameter ResolveOperationParameterNameCaseInsensitive(IEdmOperation operation, string identifier)
		{
			List<IEdmOperationParameter> list = operation.Parameters.Where((IEdmOperationParameter parameter) => string.Equals(identifier, parameter.Name, StringComparison.Ordinal)).ToList<IEdmOperationParameter>();
			if (list.Count == 0)
			{
				list = operation.Parameters.Where((IEdmOperationParameter parameter) => string.Equals(identifier, parameter.Name, StringComparison.OrdinalIgnoreCase)).ToList<IEdmOperationParameter>();
			}
			if (list.Count > 1)
			{
				throw new ODataException(Strings.UriParserMetadata_MultipleMatchingParametersFound(identifier));
			}
			if (list.Count == 1)
			{
				return list.Single<IEdmOperationParameter>();
			}
			return null;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0002F95B File Offset: 0x0002DB5B
		internal static ODataUriResolver GetUriResolver(IServiceProvider container)
		{
			if (container == null)
			{
				return ODataUriResolver.Default;
			}
			return container.GetRequiredService<ODataUriResolver>();
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0002F96C File Offset: 0x0002DB6C
		private static List<T> FindAcrossModels<T>(IEdmModel model, string qualifiedName, bool caseInsensitive) where T : IEdmSchemaElement
		{
			List<T> list = ODataUriResolver.FindSchemaElements<T>(model, qualifiedName, caseInsensitive).ToList<T>();
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				list.AddRange(ODataUriResolver.FindSchemaElements<T>(edmModel, qualifiedName, caseInsensitive));
			}
			return list;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0002F9D0 File Offset: 0x0002DBD0
		private static IEnumerable<T> FindSchemaElements<T>(IEdmModel model, string qualifiedName, bool caseInsensitive) where T : IEdmSchemaElement
		{
			return from e in model.SchemaElements.OfType<T>()
				where string.Equals(qualifiedName, e.FullName(), caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)
				select e;
		}

		// Token: 0x040007D6 RID: 2006
		private static readonly ODataUriResolver Default = new ODataUriResolver();

		// Token: 0x040007D7 RID: 2007
		private TypeFacetsPromotionRules typeFacetsPromotionRules = new TypeFacetsPromotionRules();
	}
}
