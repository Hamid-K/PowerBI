using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000229 RID: 553
	internal abstract class ODataResourceMetadataContext : IODataResourceMetadataContext
	{
		// Token: 0x06001685 RID: 5765 RVA: 0x0004537F File Offset: 0x0004357F
		protected ODataResourceMetadataContext(ODataResource resource, IODataResourceTypeContext typeContext)
		{
			this.resource = resource;
			this.typeContext = typeContext;
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00045395 File Offset: 0x00043595
		public ODataResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0004539D File Offset: 0x0004359D
		public IODataResourceTypeContext TypeContext
		{
			get
			{
				return this.typeContext;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001688 RID: 5768
		public abstract string ActualResourceTypeName { get; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001689 RID: 5769
		public abstract ICollection<KeyValuePair<string, object>> KeyProperties { get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600168A RID: 5770
		public abstract IEnumerable<KeyValuePair<string, object>> ETagProperties { get; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600168B RID: 5771
		public abstract IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties { get; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600168C RID: 5772
		public abstract IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties { get; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600168D RID: 5773
		public abstract IEnumerable<IEdmOperation> SelectedBindableOperations { get; }

		// Token: 0x0600168E RID: 5774 RVA: 0x000453A5 File Offset: 0x000435A5
		internal static ODataResourceMetadataContext Create(ODataResource resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo, IEdmStructuredType actualResourceType, IODataMetadataContext metadataContext, SelectedPropertiesNode selectedProperties)
		{
			if (serializationInfo != null)
			{
				return new ODataResourceMetadataContext.ODataResourceMetadataContextWithoutModel(resource, typeContext, serializationInfo);
			}
			return new ODataResourceMetadataContext.ODataResourceMetadataContextWithModel(resource, typeContext, actualResourceType, metadataContext, selectedProperties);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000453C0 File Offset: 0x000435C0
		internal static KeyValuePair<string, object>[] GetKeyProperties(ODataResource resource, ODataResourceSerializationInfo serializationInfo, IEdmEntityType actualEntityType)
		{
			KeyValuePair<string, object>[] array = null;
			string actualEntityTypeName = null;
			if (serializationInfo != null)
			{
				if (string.IsNullOrEmpty(resource.TypeName))
				{
					throw new ODataException(Strings.ODataResourceTypeContext_ODataResourceTypeNameMissing);
				}
				actualEntityTypeName = resource.TypeName;
				array = ODataResourceMetadataContext.GetPropertiesBySerializationInfoPropertyKind(resource, ODataPropertyKind.Key, actualEntityTypeName);
			}
			else
			{
				actualEntityTypeName = actualEntityType.FullName();
				IEnumerable<IEdmStructuralProperty> enumerable = actualEntityType.Key();
				if (enumerable != null)
				{
					array = Enumerable.ToArray<KeyValuePair<string, object>>(Enumerable.Select<IEdmStructuralProperty, KeyValuePair<string, object>>(enumerable, (IEdmStructuralProperty p) => new KeyValuePair<string, object>(p.Name, ODataResourceMetadataContext.GetPrimitiveOrEnumPropertyValue(resource, p.Name, actualEntityTypeName, false))));
				}
			}
			ODataResourceMetadataContext.ValidateEntityTypeHasKeyProperties(array, actualEntityTypeName);
			return array;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00045464 File Offset: 0x00043664
		private static object GetPrimitiveOrEnumPropertyValue(ODataResource resource, string propertyName, string entityTypeName, bool isKeyProperty)
		{
			ODataProperty odataProperty = ((resource.NonComputedProperties == null) ? null : Enumerable.SingleOrDefault<ODataProperty>(resource.NonComputedProperties, (ODataProperty p) => p.Name == propertyName));
			if (odataProperty == null)
			{
				throw new ODataException(Strings.EdmValueUtils_PropertyDoesntExist(entityTypeName, propertyName));
			}
			return ODataResourceMetadataContext.GetPrimitiveOrEnumPropertyValue(entityTypeName, odataProperty, isKeyProperty);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000454C0 File Offset: 0x000436C0
		private static object GetPrimitiveOrEnumPropertyValue(string entityTypeName, ODataProperty property, bool isKeyProperty)
		{
			object value = property.Value;
			if (value == null && isKeyProperty)
			{
				throw new ODataException(Strings.ODataResourceMetadataContext_NullKeyValue(property.Name, entityTypeName));
			}
			if (value is ODataValue && !(value is ODataEnumValue))
			{
				throw new ODataException(Strings.ODataResourceMetadataContext_KeyOrETagValuesMustBePrimitiveValues(property.Name, entityTypeName));
			}
			return value;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00045511 File Offset: 0x00043711
		private static void ValidateEntityTypeHasKeyProperties(KeyValuePair<string, object>[] keyProperties, string actualEntityTypeName)
		{
			if (keyProperties == null || keyProperties.Length == 0)
			{
				throw new ODataException(Strings.ODataResourceMetadataContext_EntityTypeWithNoKeyProperties(actualEntityTypeName));
			}
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00045528 File Offset: 0x00043728
		private static KeyValuePair<string, object>[] GetPropertiesBySerializationInfoPropertyKind(ODataResource resource, ODataPropertyKind propertyKind, string actualEntityTypeName)
		{
			KeyValuePair<string, object>[] array = ODataResourceMetadataContext.EmptyProperties;
			if (resource.NonComputedProperties != null)
			{
				array = Enumerable.ToArray<KeyValuePair<string, object>>(Enumerable.Select<ODataProperty, KeyValuePair<string, object>>(Enumerable.Where<ODataProperty>(resource.NonComputedProperties, (ODataProperty p) => p.SerializationInfo != null && p.SerializationInfo.PropertyKind == propertyKind), (ODataProperty p) => new KeyValuePair<string, object>(p.Name, ODataResourceMetadataContext.GetPrimitiveOrEnumPropertyValue(actualEntityTypeName, p, propertyKind == ODataPropertyKind.Key))));
			}
			return array;
		}

		// Token: 0x04000A60 RID: 2656
		private static readonly KeyValuePair<string, object>[] EmptyProperties = new KeyValuePair<string, object>[0];

		// Token: 0x04000A61 RID: 2657
		private readonly ODataResource resource;

		// Token: 0x04000A62 RID: 2658
		private readonly IODataResourceTypeContext typeContext;

		// Token: 0x04000A63 RID: 2659
		private KeyValuePair<string, object>[] keyProperties;

		// Token: 0x04000A64 RID: 2660
		private IEnumerable<KeyValuePair<string, object>> etagProperties;

		// Token: 0x04000A65 RID: 2661
		private IEnumerable<IEdmNavigationProperty> selectedNavigationProperties;

		// Token: 0x04000A66 RID: 2662
		private IDictionary<string, IEdmStructuralProperty> selectedStreamProperties;

		// Token: 0x04000A67 RID: 2663
		private IEnumerable<IEdmOperation> selectedBindableOperations;

		// Token: 0x0200036F RID: 879
		private sealed class ODataResourceMetadataContextWithoutModel : ODataResourceMetadataContext
		{
			// Token: 0x06001B6A RID: 7018 RVA: 0x0004D2AD File Offset: 0x0004B4AD
			internal ODataResourceMetadataContextWithoutModel(ODataResource resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo)
				: base(resource, typeContext)
			{
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0004D2BE File Offset: 0x0004B4BE
			public override ICollection<KeyValuePair<string, object>> KeyProperties
			{
				get
				{
					if (this.keyProperties == null)
					{
						this.keyProperties = ODataResourceMetadataContext.GetPropertiesBySerializationInfoPropertyKind(this.resource, ODataPropertyKind.Key, this.ActualResourceTypeName);
						ODataResourceMetadataContext.ValidateEntityTypeHasKeyProperties(this.keyProperties, this.ActualResourceTypeName);
					}
					return this.keyProperties;
				}
			}

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0004D2F8 File Offset: 0x0004B4F8
			public override IEnumerable<KeyValuePair<string, object>> ETagProperties
			{
				get
				{
					IEnumerable<KeyValuePair<string, object>> enumerable;
					if ((enumerable = this.etagProperties) == null)
					{
						enumerable = (this.etagProperties = ODataResourceMetadataContext.GetPropertiesBySerializationInfoPropertyKind(this.resource, ODataPropertyKind.ETag, this.ActualResourceTypeName));
					}
					return enumerable;
				}
			}

			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0004D32A File Offset: 0x0004B52A
			public override string ActualResourceTypeName
			{
				get
				{
					if (string.IsNullOrEmpty(base.Resource.TypeName))
					{
						throw new ODataException(Strings.ODataResourceTypeContext_ODataResourceTypeNameMissing);
					}
					return base.Resource.TypeName;
				}
			}

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0004D354 File Offset: 0x0004B554
			public override IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties
			{
				get
				{
					return ODataResourceMetadataContext.ODataResourceMetadataContextWithoutModel.EmptyNavigationProperties;
				}
			}

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0004D35B File Offset: 0x0004B55B
			public override IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties
			{
				get
				{
					return ODataResourceMetadataContext.ODataResourceMetadataContextWithoutModel.EmptyStreamProperties;
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0004D362 File Offset: 0x0004B562
			public override IEnumerable<IEdmOperation> SelectedBindableOperations
			{
				get
				{
					return ODataResourceMetadataContext.ODataResourceMetadataContextWithoutModel.EmptyOperations;
				}
			}

			// Token: 0x04000DC9 RID: 3529
			private static readonly IEdmNavigationProperty[] EmptyNavigationProperties = new IEdmNavigationProperty[0];

			// Token: 0x04000DCA RID: 3530
			private static readonly Dictionary<string, IEdmStructuralProperty> EmptyStreamProperties = new Dictionary<string, IEdmStructuralProperty>(StringComparer.Ordinal);

			// Token: 0x04000DCB RID: 3531
			private static readonly IEdmOperation[] EmptyOperations = new IEdmOperation[0];

			// Token: 0x04000DCC RID: 3532
			private readonly ODataResourceSerializationInfo serializationInfo;
		}

		// Token: 0x02000370 RID: 880
		private sealed class ODataResourceMetadataContextWithModel : ODataResourceMetadataContext
		{
			// Token: 0x06001B72 RID: 7026 RVA: 0x0004D390 File Offset: 0x0004B590
			internal ODataResourceMetadataContextWithModel(ODataResource resource, IODataResourceTypeContext typeContext, IEdmStructuredType actualResourceType, IODataMetadataContext metadataContext, SelectedPropertiesNode selectedProperties)
				: base(resource, typeContext)
			{
				this.actualResourceType = actualResourceType;
				this.metadataContext = metadataContext;
				this.selectedProperties = selectedProperties;
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0004D3B4 File Offset: 0x0004B5B4
			public override ICollection<KeyValuePair<string, object>> KeyProperties
			{
				get
				{
					if (this.keyProperties == null)
					{
						IEdmEntityType edmEntityType = this.actualResourceType as IEdmEntityType;
						if (edmEntityType != null)
						{
							IEnumerable<IEdmStructuralProperty> enumerable = edmEntityType.Key();
							if (enumerable != null)
							{
								this.keyProperties = Enumerable.ToArray<KeyValuePair<string, object>>(Enumerable.Select<IEdmStructuralProperty, KeyValuePair<string, object>>(enumerable, (IEdmStructuralProperty p) => new KeyValuePair<string, object>(p.Name, ODataResourceMetadataContext.GetPrimitiveOrEnumPropertyValue(this.resource, p.Name, this.ActualResourceTypeName, true))));
							}
							ODataResourceMetadataContext.ValidateEntityTypeHasKeyProperties(this.keyProperties, this.ActualResourceTypeName);
						}
						else
						{
							this.keyProperties = Enumerable.ToArray<KeyValuePair<string, object>>(Enumerable.Empty<KeyValuePair<string, object>>());
						}
					}
					return this.keyProperties;
				}
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x06001B74 RID: 7028 RVA: 0x0004D428 File Offset: 0x0004B628
			public override IEnumerable<KeyValuePair<string, object>> ETagProperties
			{
				get
				{
					if (this.etagProperties == null)
					{
						IEnumerable<IEdmStructuralProperty> enumerable = this.ComputeETagPropertiesFromAnnotation();
						this.etagProperties = (Enumerable.Any<IEdmStructuralProperty>(enumerable) ? Enumerable.ToArray<KeyValuePair<string, object>>(Enumerable.Select<IEdmStructuralProperty, KeyValuePair<string, object>>(enumerable, (IEdmStructuralProperty p) => new KeyValuePair<string, object>(p.Name, ODataResourceMetadataContext.GetPrimitiveOrEnumPropertyValue(this.resource, p.Name, this.ActualResourceTypeName, false)))) : ODataResourceMetadataContext.EmptyProperties);
					}
					return this.etagProperties;
				}
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0004D476 File Offset: 0x0004B676
			public override string ActualResourceTypeName
			{
				get
				{
					return this.actualResourceType.FullTypeName();
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x06001B76 RID: 7030 RVA: 0x0004D484 File Offset: 0x0004B684
			public override IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties
			{
				get
				{
					IEnumerable<IEdmNavigationProperty> enumerable;
					if ((enumerable = this.selectedNavigationProperties) == null)
					{
						enumerable = (this.selectedNavigationProperties = this.selectedProperties.GetSelectedNavigationProperties(this.actualResourceType));
					}
					return enumerable;
				}
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0004D4B8 File Offset: 0x0004B6B8
			public override IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties
			{
				get
				{
					IDictionary<string, IEdmStructuralProperty> dictionary;
					if ((dictionary = this.selectedStreamProperties) == null)
					{
						dictionary = (this.selectedStreamProperties = this.selectedProperties.GetSelectedStreamProperties(this.actualResourceType as IEdmEntityType));
					}
					return dictionary;
				}
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0004D4F0 File Offset: 0x0004B6F0
			public override IEnumerable<IEdmOperation> SelectedBindableOperations
			{
				get
				{
					if (this.selectedBindableOperations == null)
					{
						bool mustBeContainerQualified = this.metadataContext.OperationsBoundToStructuredTypeMustBeContainerQualified(this.actualResourceType);
						this.selectedBindableOperations = Enumerable.ToArray<IEdmOperation>(Enumerable.Where<IEdmOperation>(this.metadataContext.GetBindableOperationsForType(this.actualResourceType), (IEdmOperation operation) => this.selectedProperties.IsOperationSelected(this.actualResourceType, operation, mustBeContainerQualified)));
					}
					return this.selectedBindableOperations;
				}
			}

			// Token: 0x06001B79 RID: 7033 RVA: 0x0004D55C File Offset: 0x0004B75C
			private IEnumerable<IEdmStructuralProperty> ComputeETagPropertiesFromAnnotation()
			{
				IEdmModel model = this.metadataContext.Model;
				IEdmEntitySet edmEntitySet = model.FindDeclaredEntitySet(this.typeContext.NavigationSourceName);
				if (edmEntitySet != null)
				{
					IEdmVocabularyAnnotation edmVocabularyAnnotation = Enumerable.SingleOrDefault<IEdmVocabularyAnnotation>(model.FindDeclaredVocabularyAnnotations(edmEntitySet), (IEdmVocabularyAnnotation t) => t.Term.FullName().Equals("Org.OData.Core.V1.OptimisticConcurrency", 4));
					if (edmVocabularyAnnotation != null)
					{
						IEdmExpression value = edmVocabularyAnnotation.Value;
						if (value is IEdmCollectionExpression)
						{
							IEnumerable<IEdmExpression> enumerable = Enumerable.Where<IEdmExpression>((value as IEdmCollectionExpression).Elements, (IEdmExpression p) => p is IEdmPathExpression);
							using (IEnumerator<IEdmExpression> enumerator = enumerable.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									IEdmPathExpression pathExpression = (IEdmPathExpression)enumerator.Current;
									IEdmStructuralProperty edmStructuralProperty = Enumerable.FirstOrDefault<IEdmStructuralProperty>(this.actualResourceType.StructuralProperties(), (IEdmStructuralProperty p) => p.Name == Enumerable.LastOrDefault<string>(pathExpression.PathSegments));
									if (edmStructuralProperty == null)
									{
										throw new ODataException(Strings.EdmValueUtils_PropertyDoesntExist(this.ActualResourceTypeName, Enumerable.LastOrDefault<string>(pathExpression.PathSegments)));
									}
									yield return edmStructuralProperty;
								}
							}
							IEnumerator<IEdmExpression> enumerator = null;
						}
					}
				}
				yield break;
				yield break;
			}

			// Token: 0x04000DCD RID: 3533
			private readonly IEdmStructuredType actualResourceType;

			// Token: 0x04000DCE RID: 3534
			private readonly IODataMetadataContext metadataContext;

			// Token: 0x04000DCF RID: 3535
			private readonly SelectedPropertiesNode selectedProperties;
		}
	}
}
