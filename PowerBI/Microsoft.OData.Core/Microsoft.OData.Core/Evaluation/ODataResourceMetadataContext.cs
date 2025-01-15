using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000264 RID: 612
	internal abstract class ODataResourceMetadataContext : IODataResourceMetadataContext
	{
		// Token: 0x06001BAF RID: 7087 RVA: 0x00055337 File Offset: 0x00053537
		protected ODataResourceMetadataContext(ODataResourceBase resource, IODataResourceTypeContext typeContext)
		{
			this.resource = resource;
			this.typeContext = typeContext;
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0005534D File Offset: 0x0005354D
		public ODataResourceBase Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x00055355 File Offset: 0x00053555
		public IODataResourceTypeContext TypeContext
		{
			get
			{
				return this.typeContext;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001BB2 RID: 7090
		public abstract string ActualResourceTypeName { get; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001BB3 RID: 7091
		public abstract ICollection<KeyValuePair<string, object>> KeyProperties { get; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001BB4 RID: 7092
		public abstract IEnumerable<KeyValuePair<string, object>> ETagProperties { get; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001BB5 RID: 7093
		public abstract IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties { get; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001BB6 RID: 7094
		public abstract IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties { get; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001BB7 RID: 7095
		public abstract IEnumerable<IEdmOperation> SelectedBindableOperations { get; }

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0005535D File Offset: 0x0005355D
		internal static ODataResourceMetadataContext Create(ODataResourceBase resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo, IEdmStructuredType actualResourceType, IODataMetadataContext metadataContext, SelectedPropertiesNode selectedProperties, ODataMetadataSelector metadataSelector)
		{
			if (serializationInfo != null)
			{
				return new ODataResourceMetadataContext.ODataResourceMetadataContextWithoutModel(resource, typeContext, serializationInfo);
			}
			return new ODataResourceMetadataContext.ODataResourceMetadataContextWithModel(resource, typeContext, actualResourceType, metadataContext, selectedProperties, metadataSelector);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0005537C File Offset: 0x0005357C
		internal static KeyValuePair<string, object>[] GetKeyProperties(ODataResourceBase resource, ODataResourceSerializationInfo serializationInfo, IEdmEntityType actualEntityType)
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
					array = enumerable.Select((IEdmStructuralProperty p) => new KeyValuePair<string, object>(p.Name, ODataResourceMetadataContext.GetPrimitiveOrEnumPropertyValue(resource, p.Name, actualEntityTypeName, false))).ToArray<KeyValuePair<string, object>>();
				}
			}
			ODataResourceMetadataContext.ValidateEntityTypeHasKeyProperties(array, actualEntityTypeName);
			return array;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00055420 File Offset: 0x00053620
		private static object GetPrimitiveOrEnumPropertyValue(ODataResourceBase resource, string propertyName, string entityTypeName, bool isKeyProperty)
		{
			ODataProperty odataProperty = ((resource.NonComputedProperties == null) ? null : resource.NonComputedProperties.SingleOrDefault((ODataProperty p) => p.Name == propertyName));
			if (odataProperty == null)
			{
				throw new ODataException(Strings.EdmValueUtils_PropertyDoesntExist(entityTypeName, propertyName));
			}
			return ODataResourceMetadataContext.GetPrimitiveOrEnumPropertyValue(entityTypeName, odataProperty, isKeyProperty);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0005547C File Offset: 0x0005367C
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

		// Token: 0x06001BBC RID: 7100 RVA: 0x000554CD File Offset: 0x000536CD
		private static void ValidateEntityTypeHasKeyProperties(KeyValuePair<string, object>[] keyProperties, string actualEntityTypeName)
		{
			if (keyProperties == null || keyProperties.Length == 0)
			{
				throw new ODataException(Strings.ODataResourceMetadataContext_EntityTypeWithNoKeyProperties(actualEntityTypeName));
			}
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x000554E4 File Offset: 0x000536E4
		private static KeyValuePair<string, object>[] GetPropertiesBySerializationInfoPropertyKind(ODataResourceBase resource, ODataPropertyKind propertyKind, string actualEntityTypeName)
		{
			KeyValuePair<string, object>[] array = ODataResourceMetadataContext.EmptyProperties;
			if (resource.NonComputedProperties != null)
			{
				array = (from p in resource.NonComputedProperties
					where p.SerializationInfo != null && p.SerializationInfo.PropertyKind == propertyKind
					select new KeyValuePair<string, object>(p.Name, ODataResourceMetadataContext.GetPrimitiveOrEnumPropertyValue(actualEntityTypeName, p, propertyKind == ODataPropertyKind.Key))).ToArray<KeyValuePair<string, object>>();
			}
			return array;
		}

		// Token: 0x04000B8B RID: 2955
		private static readonly KeyValuePair<string, object>[] EmptyProperties = new KeyValuePair<string, object>[0];

		// Token: 0x04000B8C RID: 2956
		private readonly ODataResourceBase resource;

		// Token: 0x04000B8D RID: 2957
		private readonly IODataResourceTypeContext typeContext;

		// Token: 0x04000B8E RID: 2958
		private KeyValuePair<string, object>[] keyProperties;

		// Token: 0x04000B8F RID: 2959
		private IEnumerable<KeyValuePair<string, object>> etagProperties;

		// Token: 0x04000B90 RID: 2960
		private IEnumerable<IEdmNavigationProperty> selectedNavigationProperties;

		// Token: 0x04000B91 RID: 2961
		private IDictionary<string, IEdmStructuralProperty> selectedStreamProperties;

		// Token: 0x04000B92 RID: 2962
		private IEnumerable<IEdmOperation> selectedBindableOperations;

		// Token: 0x02000456 RID: 1110
		private sealed class ODataResourceMetadataContextWithoutModel : ODataResourceMetadataContext
		{
			// Token: 0x06002213 RID: 8723 RVA: 0x0005EA2F File Offset: 0x0005CC2F
			internal ODataResourceMetadataContextWithoutModel(ODataResourceBase resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo)
				: base(resource, typeContext)
			{
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x17000677 RID: 1655
			// (get) Token: 0x06002214 RID: 8724 RVA: 0x0005EA40 File Offset: 0x0005CC40
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

			// Token: 0x17000678 RID: 1656
			// (get) Token: 0x06002215 RID: 8725 RVA: 0x0005EA7C File Offset: 0x0005CC7C
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

			// Token: 0x17000679 RID: 1657
			// (get) Token: 0x06002216 RID: 8726 RVA: 0x0005EAAE File Offset: 0x0005CCAE
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

			// Token: 0x1700067A RID: 1658
			// (get) Token: 0x06002217 RID: 8727 RVA: 0x0005EAD8 File Offset: 0x0005CCD8
			public override IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties
			{
				get
				{
					return ODataResourceMetadataContext.ODataResourceMetadataContextWithoutModel.EmptyNavigationProperties;
				}
			}

			// Token: 0x1700067B RID: 1659
			// (get) Token: 0x06002218 RID: 8728 RVA: 0x0005EADF File Offset: 0x0005CCDF
			public override IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties
			{
				get
				{
					return ODataResourceMetadataContext.ODataResourceMetadataContextWithoutModel.EmptyStreamProperties;
				}
			}

			// Token: 0x1700067C RID: 1660
			// (get) Token: 0x06002219 RID: 8729 RVA: 0x0005EAE6 File Offset: 0x0005CCE6
			public override IEnumerable<IEdmOperation> SelectedBindableOperations
			{
				get
				{
					return ODataResourceMetadataContext.ODataResourceMetadataContextWithoutModel.EmptyOperations;
				}
			}

			// Token: 0x04001087 RID: 4231
			private static readonly IEdmNavigationProperty[] EmptyNavigationProperties = new IEdmNavigationProperty[0];

			// Token: 0x04001088 RID: 4232
			private static readonly Dictionary<string, IEdmStructuralProperty> EmptyStreamProperties = new Dictionary<string, IEdmStructuralProperty>(StringComparer.Ordinal);

			// Token: 0x04001089 RID: 4233
			private static readonly IEdmOperation[] EmptyOperations = new IEdmOperation[0];

			// Token: 0x0400108A RID: 4234
			private readonly ODataResourceSerializationInfo serializationInfo;
		}

		// Token: 0x02000457 RID: 1111
		private sealed class ODataResourceMetadataContextWithModel : ODataResourceMetadataContext
		{
			// Token: 0x0600221B RID: 8731 RVA: 0x0005EB14 File Offset: 0x0005CD14
			internal ODataResourceMetadataContextWithModel(ODataResourceBase resource, IODataResourceTypeContext typeContext, IEdmStructuredType actualResourceType, IODataMetadataContext metadataContext, SelectedPropertiesNode selectedProperties, ODataMetadataSelector metadataSelector)
				: base(resource, typeContext)
			{
				this.actualResourceType = actualResourceType;
				this.metadataContext = metadataContext;
				this.selectedProperties = selectedProperties;
				this.metadataSelector = metadataSelector;
			}

			// Token: 0x1700067D RID: 1661
			// (get) Token: 0x0600221C RID: 8732 RVA: 0x0005EB40 File Offset: 0x0005CD40
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
								this.keyProperties = enumerable.Select((IEdmStructuralProperty p) => new KeyValuePair<string, object>(p.Name, ODataResourceMetadataContext.GetPrimitiveOrEnumPropertyValue(this.resource, p.Name, this.ActualResourceTypeName, true))).ToArray<KeyValuePair<string, object>>();
							}
							ODataResourceMetadataContext.ValidateEntityTypeHasKeyProperties(this.keyProperties, this.ActualResourceTypeName);
						}
						else
						{
							this.keyProperties = Enumerable.Empty<KeyValuePair<string, object>>().ToArray<KeyValuePair<string, object>>();
						}
					}
					return this.keyProperties;
				}
			}

			// Token: 0x1700067E RID: 1662
			// (get) Token: 0x0600221D RID: 8733 RVA: 0x0005EBB4 File Offset: 0x0005CDB4
			public override IEnumerable<KeyValuePair<string, object>> ETagProperties
			{
				get
				{
					if (this.etagProperties == null)
					{
						IEnumerable<IEdmStructuralProperty> enumerable = this.ComputeETagPropertiesFromAnnotation();
						this.etagProperties = (enumerable.Any<IEdmStructuralProperty>() ? enumerable.Select((IEdmStructuralProperty p) => new KeyValuePair<string, object>(p.Name, ODataResourceMetadataContext.GetPrimitiveOrEnumPropertyValue(this.resource, p.Name, this.ActualResourceTypeName, false))).ToArray<KeyValuePair<string, object>>() : ODataResourceMetadataContext.EmptyProperties);
					}
					return this.etagProperties;
				}
			}

			// Token: 0x1700067F RID: 1663
			// (get) Token: 0x0600221E RID: 8734 RVA: 0x0005EC02 File Offset: 0x0005CE02
			public override string ActualResourceTypeName
			{
				get
				{
					return this.actualResourceType.FullTypeName();
				}
			}

			// Token: 0x17000680 RID: 1664
			// (get) Token: 0x0600221F RID: 8735 RVA: 0x0005EC10 File Offset: 0x0005CE10
			public override IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties
			{
				get
				{
					if (this.selectedNavigationProperties == null)
					{
						this.selectedNavigationProperties = this.selectedProperties.GetSelectedNavigationProperties(this.actualResourceType);
						if (this.metadataSelector != null)
						{
							this.selectedNavigationProperties = this.metadataSelector.SelectNavigationProperties(this.actualResourceType, this.selectedNavigationProperties);
						}
					}
					return this.selectedNavigationProperties;
				}
			}

			// Token: 0x17000681 RID: 1665
			// (get) Token: 0x06002220 RID: 8736 RVA: 0x0005EC68 File Offset: 0x0005CE68
			public override IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties
			{
				get
				{
					if (this.selectedStreamProperties == null)
					{
						this.selectedStreamProperties = this.selectedProperties.GetSelectedStreamProperties(this.actualResourceType as IEdmEntityType);
						if (this.metadataSelector != null)
						{
							this.selectedStreamProperties = this.metadataSelector.SelectStreamProperties(this.actualResourceType, this.selectedStreamProperties.Values).ToDictionary((IEdmStructuralProperty v) => v.Name);
						}
					}
					return this.selectedStreamProperties;
				}
			}

			// Token: 0x17000682 RID: 1666
			// (get) Token: 0x06002221 RID: 8737 RVA: 0x0005ECF0 File Offset: 0x0005CEF0
			public override IEnumerable<IEdmOperation> SelectedBindableOperations
			{
				get
				{
					if (this.selectedBindableOperations == null)
					{
						bool mustBeContainerQualified = this.metadataContext.OperationsBoundToStructuredTypeMustBeContainerQualified(this.actualResourceType);
						this.selectedBindableOperations = from operation in this.metadataContext.GetBindableOperationsForType(this.actualResourceType)
							where this.selectedProperties.IsOperationSelected(this.actualResourceType, operation, mustBeContainerQualified)
							select operation;
						if (this.metadataSelector != null)
						{
							this.selectedBindableOperations = this.metadataSelector.SelectBindableOperations(this.actualResourceType, this.selectedBindableOperations);
						}
					}
					return this.selectedBindableOperations;
				}
			}

			// Token: 0x06002222 RID: 8738 RVA: 0x0005ED7C File Offset: 0x0005CF7C
			private IEnumerable<IEdmStructuralProperty> ComputeETagPropertiesFromAnnotation()
			{
				IEdmModel model = this.metadataContext.Model;
				IEdmEntitySet edmEntitySet = model.FindDeclaredEntitySet(this.typeContext.NavigationSourceName);
				if (edmEntitySet != null)
				{
					IEdmVocabularyAnnotation edmVocabularyAnnotation = model.FindDeclaredVocabularyAnnotations(edmEntitySet).SingleOrDefault((IEdmVocabularyAnnotation t) => t.Term.FullName().Equals("Org.OData.Core.V1.OptimisticConcurrency", StringComparison.Ordinal));
					if (edmVocabularyAnnotation != null)
					{
						IEdmExpression value = edmVocabularyAnnotation.Value;
						if (value is IEdmCollectionExpression)
						{
							IEnumerable<IEdmExpression> enumerable = (value as IEdmCollectionExpression).Elements.Where((IEdmExpression p) => p is IEdmPathExpression);
							using (IEnumerator<IEdmExpression> enumerator = enumerable.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									IEdmPathExpression pathExpression = (IEdmPathExpression)enumerator.Current;
									IEdmStructuralProperty edmStructuralProperty = this.actualResourceType.StructuralProperties().FirstOrDefault((IEdmStructuralProperty p) => p.Name == pathExpression.PathSegments.LastOrDefault<string>());
									if (edmStructuralProperty == null)
									{
										throw new ODataException(Strings.EdmValueUtils_PropertyDoesntExist(this.ActualResourceTypeName, pathExpression.PathSegments.LastOrDefault<string>()));
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

			// Token: 0x0400108B RID: 4235
			private readonly IEdmStructuredType actualResourceType;

			// Token: 0x0400108C RID: 4236
			private readonly IODataMetadataContext metadataContext;

			// Token: 0x0400108D RID: 4237
			private readonly SelectedPropertiesNode selectedProperties;

			// Token: 0x0400108E RID: 4238
			private readonly ODataMetadataSelector metadataSelector;
		}
	}
}
