using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.UriParser.Parsers;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Metadata
{
	// Token: 0x020001E9 RID: 489
	public class ODataUriResolver
	{
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x000401EE File Offset: 0x0003E3EE
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x000401F6 File Offset: 0x0003E3F6
		public virtual bool EnableCaseInsensitive { get; set; }

		// Token: 0x060011D1 RID: 4561 RVA: 0x000401FF File Offset: 0x0003E3FF
		public virtual void PromoteBinaryOperandTypes(BinaryOperatorKind binaryOperatorKind, ref SingleValueNode leftNode, ref SingleValueNode rightNode, out IEdmTypeReference typeReference)
		{
			typeReference = null;
			BinaryOperatorBinder.PromoteOperandTypes(binaryOperatorKind, ref leftNode, ref rightNode);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004022C File Offset: 0x0003E42C
		public virtual IEdmNavigationSource ResolveNavigationSource(IEdmModel model, string identifier)
		{
			if (this.EnableCaseInsensitive)
			{
				IEdmEntityContainer entityContainer = model.EntityContainer;
				if (entityContainer == null)
				{
					return null;
				}
				List<IEdmNavigationSource> list = Enumerable.ToList<IEdmNavigationSource>(Enumerable.Where<IEdmNavigationSource>(Enumerable.OfType<IEdmNavigationSource>(entityContainer.Elements), (IEdmNavigationSource source) => string.Equals(identifier, source.Name, 5)));
				if (list.Count == 1)
				{
					return Enumerable.Single<IEdmNavigationSource>(list);
				}
				if (list.Count > 1)
				{
					throw new ODataException(Strings.UriParserMetadata_MultipleMatchingNavigationSourcesFound(identifier));
				}
			}
			return model.FindDeclaredNavigationSource(identifier);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000402D8 File Offset: 0x0003E4D8
		public virtual IEdmProperty ResolveProperty(IEdmStructuredType type, string propertyName)
		{
			if (this.EnableCaseInsensitive)
			{
				List<IEdmProperty> list = Enumerable.ToList<IEdmProperty>(Enumerable.Where<IEdmProperty>(type.Properties(), (IEdmProperty _) => string.Equals(propertyName, _.Name, 5)));
				if (list.Count == 1)
				{
					return Enumerable.Single<IEdmProperty>(list);
				}
				if (list.Count > 1)
				{
					throw new ODataException(Strings.UriParserMetadata_MultipleMatchingPropertiesFound(propertyName, type.FullTypeName()));
				}
			}
			return type.FindProperty(propertyName);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00040378 File Offset: 0x0003E578
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "IEdmModel.FindType is allowed here and all other places should call this method to get to the type.")]
		public virtual IEdmSchemaType ResolveType(IEdmModel model, string typeName)
		{
			if (this.EnableCaseInsensitive)
			{
				List<IEdmSchemaType> list = Enumerable.ToList<IEdmSchemaType>(Enumerable.Where<IEdmSchemaType>(Enumerable.OfType<IEdmSchemaType>(model.SchemaElements), (IEdmSchemaType _) => string.Equals(typeName, _.FullName(), 5)));
				if (list.Count == 1)
				{
					return Enumerable.Single<IEdmSchemaType>(list);
				}
				if (list.Count > 1)
				{
					throw new ODataException(Strings.UriParserMetadata_MultipleMatchingTypesFound(typeName));
				}
			}
			return model.FindType(typeName);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004043C File Offset: 0x0003E63C
		public virtual IEnumerable<IEdmOperation> ResolveBoundOperations(IEdmModel model, string identifier, IEdmType bindingType)
		{
			if (this.EnableCaseInsensitive)
			{
				return Enumerable.Where<IEdmOperation>(Enumerable.OfType<IEdmOperation>(model.SchemaElements), (IEdmOperation operation) => string.Equals(identifier, operation.FullName(), 5) && operation.IsBound && Enumerable.Any<IEdmOperationParameter>(operation.Parameters) && operation.HasEquivalentBindingType(bindingType));
			}
			return model.FindBoundOperations(identifier, bindingType);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000404C4 File Offset: 0x0003E6C4
		public virtual IEnumerable<IEdmOperation> ResolveUnboundOperations(IEdmModel model, string identifier)
		{
			if (this.EnableCaseInsensitive)
			{
				return Enumerable.Where<IEdmOperation>(Enumerable.OfType<IEdmOperation>(model.SchemaElements), (IEdmOperation operation) => string.Equals(identifier, operation.FullName(), 5) && !operation.IsBound);
			}
			return model.FindOperations(identifier);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00040534 File Offset: 0x0003E734
		public virtual IEnumerable<IEdmOperationImport> ResolveOperationImports(IEdmModel model, string identifier)
		{
			if (!this.EnableCaseInsensitive)
			{
				return model.FindDeclaredOperationImports(identifier);
			}
			IEdmEntityContainer entityContainer = model.EntityContainer;
			if (entityContainer == null)
			{
				return null;
			}
			return Enumerable.Where<IEdmOperationImport>(Enumerable.OfType<IEdmOperationImport>(entityContainer.Elements), (IEdmOperationImport source) => string.Equals(identifier, source.Name, 5));
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00040594 File Offset: 0x0003E794
		public virtual IDictionary<IEdmOperationParameter, SingleValueNode> ResolveOperationParameters(IEdmOperation operation, IDictionary<string, SingleValueNode> input)
		{
			Dictionary<IEdmOperationParameter, SingleValueNode> dictionary = new Dictionary<IEdmOperationParameter, SingleValueNode>(EqualityComparer<IEdmOperationParameter>.Default);
			foreach (KeyValuePair<string, SingleValueNode> keyValuePair in input)
			{
				IEdmOperationParameter edmOperationParameter;
				if (this.EnableCaseInsensitive)
				{
					edmOperationParameter = ODataUriResolver.ResolveOpearationParameterNameCaseInsensitive(operation, keyValuePair.Key);
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

		// Token: 0x060011D9 RID: 4569 RVA: 0x00040634 File Offset: 0x0003E834
		public virtual IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IList<string> positionalValues, Func<IEdmTypeReference, string, object> convertFunc)
		{
			List<IEdmStructuralProperty> list = Enumerable.ToList<IEdmStructuralProperty>(type.Key());
			List<KeyValuePair<string, object>> list2 = new List<KeyValuePair<string, object>>(positionalValues.Count);
			for (int i = 0; i < list.Count; i++)
			{
				string text = positionalValues[i];
				IEdmProperty edmProperty = list[i];
				object obj = convertFunc.Invoke(edmProperty.Type, text);
				if (obj == null)
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
				list2.Add(new KeyValuePair<string, object>(edmProperty.Name, obj));
			}
			return list2;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000406C8 File Offset: 0x0003E8C8
		public virtual IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IDictionary<string, string> namedValues, Func<IEdmTypeReference, string, object> convertFunc)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
			List<IEdmStructuralProperty> list = Enumerable.ToList<IEdmStructuralProperty>(type.Key());
			using (List<IEdmStructuralProperty>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IEdmStructuralProperty property = enumerator.Current;
					string text;
					if (this.EnableCaseInsensitive)
					{
						List<string> list2 = Enumerable.ToList<string>(Enumerable.Where<string>(namedValues.Keys, (string key) => string.Equals(property.Name, key, 5)));
						if (list2.Count > 1)
						{
							throw new ODataException(Strings.UriParserMetadata_MultipleMatchingKeysFound(property.Name));
						}
						if (list2.Count == 0)
						{
							throw ExceptionUtil.CreateSyntaxError();
						}
						text = namedValues[Enumerable.Single<string>(list2)];
					}
					else if (!namedValues.TryGetValue(property.Name, ref text))
					{
						throw ExceptionUtil.CreateSyntaxError();
					}
					object obj = convertFunc.Invoke(property.Type, text);
					if (obj == null)
					{
						throw ExceptionUtil.CreateSyntaxError();
					}
					dictionary[property.Name] = obj;
				}
			}
			return dictionary;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00040814 File Offset: 0x0003EA14
		internal static IEdmOperationParameter ResolveOpearationParameterNameCaseInsensitive(IEdmOperation operation, string identifier)
		{
			List<IEdmOperationParameter> list = Enumerable.ToList<IEdmOperationParameter>(Enumerable.Where<IEdmOperationParameter>(operation.Parameters, (IEdmOperationParameter parameter) => string.Equals(identifier, parameter.Name, 5)));
			if (list.Count > 1)
			{
				throw new ODataException(Strings.UriParserMetadata_MultipleMatchingParametersFound(identifier));
			}
			if (list.Count == 1)
			{
				return Enumerable.Single<IEdmOperationParameter>(list);
			}
			return null;
		}

		// Token: 0x040007B3 RID: 1971
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		internal static readonly ODataUriResolver Default = new ODataUriResolver();
	}
}
