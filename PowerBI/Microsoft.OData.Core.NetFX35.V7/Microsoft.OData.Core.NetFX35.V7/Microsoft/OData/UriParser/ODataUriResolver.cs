using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A8 RID: 424
	public class ODataUriResolver
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0002FB34 File Offset: 0x0002DD34
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x0002FB3C File Offset: 0x0002DD3C
		public virtual bool EnableCaseInsensitive { get; set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x0002FB45 File Offset: 0x0002DD45
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x0002FB4D File Offset: 0x0002DD4D
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

		// Token: 0x0600110B RID: 4363 RVA: 0x0002FB56 File Offset: 0x0002DD56
		public virtual void PromoteBinaryOperandTypes(BinaryOperatorKind binaryOperatorKind, ref SingleValueNode leftNode, ref SingleValueNode rightNode, out IEdmTypeReference typeReference)
		{
			typeReference = null;
			BinaryOperatorBinder.PromoteOperandTypes(binaryOperatorKind, ref leftNode, ref rightNode, this.typeFacetsPromotionRules);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0002FB6C File Offset: 0x0002DD6C
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

		// Token: 0x0600110D RID: 4365 RVA: 0x0002FBF4 File Offset: 0x0002DDF4
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

		// Token: 0x0600110E RID: 4366 RVA: 0x0002FC70 File Offset: 0x0002DE70
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

		// Token: 0x0600110F RID: 4367 RVA: 0x0002FCEC File Offset: 0x0002DEEC
		public virtual IEnumerable<IEdmOperation> ResolveBoundOperations(IEdmModel model, string identifier, IEdmType bindingType)
		{
			if (this.EnableCaseInsensitive)
			{
				return Enumerable.Where<IEdmOperation>(Enumerable.OfType<IEdmOperation>(model.SchemaElements), (IEdmOperation operation) => string.Equals(identifier, operation.FullName(), 5) && operation.IsBound && Enumerable.Any<IEdmOperationParameter>(operation.Parameters) && operation.HasEquivalentBindingType(bindingType));
			}
			return model.FindBoundOperations(identifier, bindingType);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0002FD44 File Offset: 0x0002DF44
		public virtual IEnumerable<IEdmOperation> ResolveUnboundOperations(IEdmModel model, string identifier)
		{
			if (this.EnableCaseInsensitive)
			{
				return Enumerable.Where<IEdmOperation>(Enumerable.OfType<IEdmOperation>(model.SchemaElements), (IEdmOperation operation) => string.Equals(identifier, operation.FullName(), 5) && !operation.IsBound);
			}
			return model.FindOperations(identifier);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0002FD90 File Offset: 0x0002DF90
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

		// Token: 0x06001112 RID: 4370 RVA: 0x0002FDE8 File Offset: 0x0002DFE8
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

		// Token: 0x06001113 RID: 4371 RVA: 0x0002FE88 File Offset: 0x0002E088
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

		// Token: 0x06001114 RID: 4372 RVA: 0x0002FF00 File Offset: 0x0002E100
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

		// Token: 0x06001115 RID: 4373 RVA: 0x00030024 File Offset: 0x0002E224
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

		// Token: 0x06001116 RID: 4374 RVA: 0x00030086 File Offset: 0x0002E286
		internal static ODataUriResolver GetUriResolver(IServiceProvider container)
		{
			if (container == null)
			{
				return ODataUriResolver.Default;
			}
			return container.GetRequiredService<ODataUriResolver>();
		}

		// Token: 0x040008BF RID: 2239
		private static readonly ODataUriResolver Default = new ODataUriResolver();

		// Token: 0x040008C0 RID: 2240
		private TypeFacetsPromotionRules typeFacetsPromotionRules = new TypeFacetsPromotionRules();
	}
}
