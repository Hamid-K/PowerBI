using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000105 RID: 261
	internal abstract class ODataEntryMetadataContext : IODataEntryMetadataContext
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x00017D7E File Offset: 0x00015F7E
		protected ODataEntryMetadataContext(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext)
		{
			this.entry = entry;
			this.typeContext = typeContext;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00017D94 File Offset: 0x00015F94
		public ODataEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00017D9C File Offset: 0x00015F9C
		public IODataFeedAndEntryTypeContext TypeContext
		{
			get
			{
				return this.typeContext;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006E9 RID: 1769
		public abstract string ActualEntityTypeName { get; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006EA RID: 1770
		public abstract ICollection<KeyValuePair<string, object>> KeyProperties { get; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006EB RID: 1771
		public abstract IEnumerable<KeyValuePair<string, object>> ETagProperties { get; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006EC RID: 1772
		public abstract IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties { get; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006ED RID: 1773
		public abstract IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties { get; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006EE RID: 1774
		public abstract IEnumerable<IEdmFunctionImport> SelectedAlwaysBindableOperations { get; }

		// Token: 0x060006EF RID: 1775 RVA: 0x00017DA4 File Offset: 0x00015FA4
		internal static ODataEntryMetadataContext Create(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, IODataMetadataContext metadataContext, SelectedPropertiesNode selectedProperties)
		{
			if (serializationInfo != null)
			{
				return new ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel(entry, typeContext, serializationInfo);
			}
			return new ODataEntryMetadataContext.ODataEntryMetadataContextWithModel(entry, typeContext, actualEntityType, metadataContext, selectedProperties);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00017DDC File Offset: 0x00015FDC
		private static object GetPrimitivePropertyClrValue(ODataEntry entry, string propertyName, string entityTypeName, bool isKeyProperty)
		{
			ODataProperty odataProperty = ((entry.NonComputedProperties == null) ? null : Enumerable.SingleOrDefault<ODataProperty>(entry.NonComputedProperties, (ODataProperty p) => p.Name == propertyName));
			if (odataProperty == null)
			{
				throw new ODataException(Strings.EdmValueUtils_PropertyDoesntExist(entry.TypeName, propertyName));
			}
			return ODataEntryMetadataContext.GetPrimitivePropertyClrValue(entityTypeName, odataProperty, isKeyProperty);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00017E3C File Offset: 0x0001603C
		private static object GetPrimitivePropertyClrValue(string entityTypeName, ODataProperty property, bool isKeyProperty)
		{
			object value = property.Value;
			if (value == null && isKeyProperty)
			{
				throw new ODataException(Strings.ODataEntryMetadataContext_NullKeyValue(property.Name, entityTypeName));
			}
			if (value is ODataValue)
			{
				throw new ODataException(Strings.ODataEntryMetadataContext_KeyOrETagValuesMustBePrimitiveValues(property.Name, entityTypeName));
			}
			return value;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00017E83 File Offset: 0x00016083
		private static void ValidateEntityTypeHasKeyProperties(KeyValuePair<string, object>[] keyProperties, string actualEntityTypeName)
		{
			if (keyProperties == null || keyProperties.Length == 0)
			{
				throw new ODataException(Strings.ODataEntryMetadataContext_EntityTypeWithNoKeyProperties(actualEntityTypeName));
			}
		}

		// Token: 0x040002A7 RID: 679
		private static readonly KeyValuePair<string, object>[] EmptyProperties = new KeyValuePair<string, object>[0];

		// Token: 0x040002A8 RID: 680
		private readonly ODataEntry entry;

		// Token: 0x040002A9 RID: 681
		private readonly IODataFeedAndEntryTypeContext typeContext;

		// Token: 0x040002AA RID: 682
		private KeyValuePair<string, object>[] keyProperties;

		// Token: 0x040002AB RID: 683
		private IEnumerable<KeyValuePair<string, object>> etagProperties;

		// Token: 0x040002AC RID: 684
		private IEnumerable<IEdmNavigationProperty> selectedNavigationProperties;

		// Token: 0x040002AD RID: 685
		private IDictionary<string, IEdmStructuralProperty> selectedStreamProperties;

		// Token: 0x040002AE RID: 686
		private IEnumerable<IEdmFunctionImport> selectedAlwaysBindableOperations;

		// Token: 0x02000106 RID: 262
		private sealed class ODataEntryMetadataContextWithoutModel : ODataEntryMetadataContext
		{
			// Token: 0x060006F4 RID: 1780 RVA: 0x00017EA6 File Offset: 0x000160A6
			internal ODataEntryMetadataContextWithoutModel(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo)
				: base(entry, typeContext)
			{
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00017EB7 File Offset: 0x000160B7
			public override ICollection<KeyValuePair<string, object>> KeyProperties
			{
				get
				{
					if (this.keyProperties == null)
					{
						this.keyProperties = ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.GetPropertiesBySerializationInfoPropertyKind(this.entry, ODataPropertyKind.Key, this.ActualEntityTypeName);
						ODataEntryMetadataContext.ValidateEntityTypeHasKeyProperties(this.keyProperties, this.ActualEntityTypeName);
					}
					return this.keyProperties;
				}
			}

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00017EF0 File Offset: 0x000160F0
			public override IEnumerable<KeyValuePair<string, object>> ETagProperties
			{
				get
				{
					IEnumerable<KeyValuePair<string, object>> enumerable;
					if ((enumerable = this.etagProperties) == null)
					{
						enumerable = (this.etagProperties = ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.GetPropertiesBySerializationInfoPropertyKind(this.entry, ODataPropertyKind.ETag, this.ActualEntityTypeName));
					}
					return enumerable;
				}
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00017F22 File Offset: 0x00016122
			public override string ActualEntityTypeName
			{
				get
				{
					if (string.IsNullOrEmpty(base.Entry.TypeName))
					{
						throw new ODataException(Strings.ODataFeedAndEntryTypeContext_ODataEntryTypeNameMissing);
					}
					return base.Entry.TypeName;
				}
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00017F4C File Offset: 0x0001614C
			public override IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties
			{
				get
				{
					return ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.EmptyNavigationProperties;
				}
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00017F53 File Offset: 0x00016153
			public override IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties
			{
				get
				{
					return ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.EmptyStreamProperties;
				}
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x060006FA RID: 1786 RVA: 0x00017F5A File Offset: 0x0001615A
			public override IEnumerable<IEdmFunctionImport> SelectedAlwaysBindableOperations
			{
				get
				{
					return ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.EmptyOperations;
				}
			}

			// Token: 0x060006FB RID: 1787 RVA: 0x00017FAC File Offset: 0x000161AC
			private static KeyValuePair<string, object>[] GetPropertiesBySerializationInfoPropertyKind(ODataEntry entry, ODataPropertyKind propertyKind, string actualEntityTypeName)
			{
				KeyValuePair<string, object>[] array = ODataEntryMetadataContext.EmptyProperties;
				if (entry.NonComputedProperties != null)
				{
					array = Enumerable.ToArray<KeyValuePair<string, object>>(Enumerable.Select<ODataProperty, KeyValuePair<string, object>>(Enumerable.Where<ODataProperty>(entry.NonComputedProperties, (ODataProperty p) => p.SerializationInfo != null && p.SerializationInfo.PropertyKind == propertyKind), (ODataProperty p) => new KeyValuePair<string, object>(p.Name, ODataEntryMetadataContext.GetPrimitivePropertyClrValue(actualEntityTypeName, p, propertyKind == ODataPropertyKind.Key))));
				}
				return array;
			}

			// Token: 0x040002AF RID: 687
			private static readonly IEdmNavigationProperty[] EmptyNavigationProperties = new IEdmNavigationProperty[0];

			// Token: 0x040002B0 RID: 688
			private static readonly Dictionary<string, IEdmStructuralProperty> EmptyStreamProperties = new Dictionary<string, IEdmStructuralProperty>(StringComparer.Ordinal);

			// Token: 0x040002B1 RID: 689
			private static readonly IEdmFunctionImport[] EmptyOperations = new IEdmFunctionImport[0];

			// Token: 0x040002B2 RID: 690
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;
		}

		// Token: 0x02000107 RID: 263
		private sealed class ODataEntryMetadataContextWithModel : ODataEntryMetadataContext
		{
			// Token: 0x060006FD RID: 1789 RVA: 0x0001803F File Offset: 0x0001623F
			internal ODataEntryMetadataContextWithModel(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, IEdmEntityType actualEntityType, IODataMetadataContext metadataContext, SelectedPropertiesNode selectedProperties)
				: base(entry, typeContext)
			{
				this.actualEntityType = actualEntityType;
				this.metadataContext = metadataContext;
				this.selectedProperties = selectedProperties;
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x060006FE RID: 1790 RVA: 0x00018088 File Offset: 0x00016288
			public override ICollection<KeyValuePair<string, object>> KeyProperties
			{
				get
				{
					if (this.keyProperties == null)
					{
						IEnumerable<IEdmStructuralProperty> enumerable = this.actualEntityType.Key();
						if (enumerable != null)
						{
							this.keyProperties = Enumerable.ToArray<KeyValuePair<string, object>>(Enumerable.Select<IEdmStructuralProperty, KeyValuePair<string, object>>(enumerable, (IEdmStructuralProperty p) => new KeyValuePair<string, object>(p.Name, ODataEntryMetadataContext.GetPrimitivePropertyClrValue(this.entry, p.Name, this.ActualEntityTypeName, true))));
						}
						ODataEntryMetadataContext.ValidateEntityTypeHasKeyProperties(this.keyProperties, this.ActualEntityTypeName);
					}
					return this.keyProperties;
				}
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x060006FF RID: 1791 RVA: 0x00018118 File Offset: 0x00016318
			public override IEnumerable<KeyValuePair<string, object>> ETagProperties
			{
				get
				{
					if (this.etagProperties == null)
					{
						IEnumerable<IEdmStructuralProperty> enumerable = this.actualEntityType.StructuralProperties();
						IEnumerable<KeyValuePair<string, object>> enumerable2;
						if (enumerable == null)
						{
							enumerable2 = ODataEntryMetadataContext.EmptyProperties;
						}
						else
						{
							enumerable2 = Enumerable.ToArray<KeyValuePair<string, object>>(Enumerable.Select<IEdmStructuralProperty, KeyValuePair<string, object>>(Enumerable.Where<IEdmStructuralProperty>(enumerable, (IEdmStructuralProperty p) => p.ConcurrencyMode == EdmConcurrencyMode.Fixed), (IEdmStructuralProperty p) => new KeyValuePair<string, object>(p.Name, ODataEntryMetadataContext.GetPrimitivePropertyClrValue(this.entry, p.Name, this.ActualEntityTypeName, false))));
						}
						this.etagProperties = enumerable2;
					}
					return this.etagProperties;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001818F File Offset: 0x0001638F
			public override string ActualEntityTypeName
			{
				get
				{
					return this.actualEntityType.FullName();
				}
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001819C File Offset: 0x0001639C
			public override IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties
			{
				get
				{
					IEnumerable<IEdmNavigationProperty> enumerable;
					if ((enumerable = this.selectedNavigationProperties) == null)
					{
						enumerable = (this.selectedNavigationProperties = this.selectedProperties.GetSelectedNavigationProperties(this.actualEntityType));
					}
					return enumerable;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06000702 RID: 1794 RVA: 0x000181D0 File Offset: 0x000163D0
			public override IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties
			{
				get
				{
					IDictionary<string, IEdmStructuralProperty> dictionary;
					if ((dictionary = this.selectedStreamProperties) == null)
					{
						dictionary = (this.selectedStreamProperties = this.selectedProperties.GetSelectedStreamProperties(this.actualEntityType));
					}
					return dictionary;
				}
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000703 RID: 1795 RVA: 0x00018230 File Offset: 0x00016430
			public override IEnumerable<IEdmFunctionImport> SelectedAlwaysBindableOperations
			{
				get
				{
					if (this.selectedAlwaysBindableOperations == null)
					{
						bool mustBeContainerQualified = this.metadataContext.OperationsBoundToEntityTypeMustBeContainerQualified(this.actualEntityType);
						this.selectedAlwaysBindableOperations = Enumerable.ToArray<IEdmFunctionImport>(Enumerable.Where<IEdmFunctionImport>(this.metadataContext.GetAlwaysBindableOperationsForType(this.actualEntityType), (IEdmFunctionImport operation) => this.selectedProperties.IsOperationSelected(this.actualEntityType, operation, mustBeContainerQualified)));
					}
					return this.selectedAlwaysBindableOperations;
				}
			}

			// Token: 0x040002B3 RID: 691
			private readonly IEdmEntityType actualEntityType;

			// Token: 0x040002B4 RID: 692
			private readonly IODataMetadataContext metadataContext;

			// Token: 0x040002B5 RID: 693
			private readonly SelectedPropertiesNode selectedProperties;
		}
	}
}
