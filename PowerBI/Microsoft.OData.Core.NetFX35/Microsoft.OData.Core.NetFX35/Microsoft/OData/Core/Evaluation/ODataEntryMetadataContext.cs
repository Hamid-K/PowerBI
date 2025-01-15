using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x02000088 RID: 136
	internal abstract class ODataEntryMetadataContext : IODataEntryMetadataContext
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x00013BC0 File Offset: 0x00011DC0
		protected ODataEntryMetadataContext(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext)
		{
			this.entry = entry;
			this.typeContext = typeContext;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00013BD6 File Offset: 0x00011DD6
		public ODataEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00013BDE File Offset: 0x00011DDE
		public IODataFeedAndEntryTypeContext TypeContext
		{
			get
			{
				return this.typeContext;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000566 RID: 1382
		public abstract string ActualEntityTypeName { get; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000567 RID: 1383
		public abstract ICollection<KeyValuePair<string, object>> KeyProperties { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000568 RID: 1384
		public abstract IEnumerable<KeyValuePair<string, object>> ETagProperties { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000569 RID: 1385
		public abstract IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600056A RID: 1386
		public abstract IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600056B RID: 1387
		public abstract IEnumerable<IEdmOperation> SelectedBindableOperations { get; }

		// Token: 0x0600056C RID: 1388 RVA: 0x00013BE6 File Offset: 0x00011DE6
		internal static ODataEntryMetadataContext Create(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, IODataMetadataContext metadataContext, SelectedPropertiesNode selectedProperties)
		{
			if (serializationInfo != null)
			{
				return new ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel(entry, typeContext, serializationInfo);
			}
			return new ODataEntryMetadataContext.ODataEntryMetadataContextWithModel(entry, typeContext, actualEntityType, metadataContext, selectedProperties);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00013C30 File Offset: 0x00011E30
		internal static KeyValuePair<string, object>[] GetKeyProperties(ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType)
		{
			KeyValuePair<string, object>[] array = null;
			string actualEntityTypeName = null;
			if (serializationInfo != null)
			{
				if (string.IsNullOrEmpty(entry.TypeName))
				{
					throw new ODataException(Strings.ODataFeedAndEntryTypeContext_ODataEntryTypeNameMissing);
				}
				actualEntityTypeName = entry.TypeName;
				array = ODataEntryMetadataContext.GetPropertiesBySerializationInfoPropertyKind(entry, ODataPropertyKind.Key, actualEntityTypeName);
			}
			else
			{
				actualEntityTypeName = actualEntityType.FullName();
				IEnumerable<IEdmStructuralProperty> enumerable = actualEntityType.Key();
				if (enumerable != null)
				{
					array = Enumerable.ToArray<KeyValuePair<string, object>>(Enumerable.Select<IEdmStructuralProperty, KeyValuePair<string, object>>(enumerable, (IEdmStructuralProperty p) => new KeyValuePair<string, object>(p.Name, ODataEntryMetadataContext.GetPrimitivePropertyClrValue(entry, p.Name, actualEntityTypeName, false))));
				}
			}
			ODataEntryMetadataContext.ValidateEntityTypeHasKeyProperties(array, actualEntityTypeName);
			return array;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00013CF8 File Offset: 0x00011EF8
		private static object GetPrimitivePropertyClrValue(ODataEntry entry, string propertyName, string entityTypeName, bool isKeyProperty)
		{
			ODataProperty odataProperty = ((entry.NonComputedProperties == null) ? null : Enumerable.SingleOrDefault<ODataProperty>(entry.NonComputedProperties, (ODataProperty p) => p.Name == propertyName));
			if (odataProperty == null)
			{
				throw new ODataException(Strings.EdmValueUtils_PropertyDoesntExist(entityTypeName, propertyName));
			}
			return ODataEntryMetadataContext.GetPrimitivePropertyClrValue(entityTypeName, odataProperty, isKeyProperty);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00013D54 File Offset: 0x00011F54
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

		// Token: 0x06000570 RID: 1392 RVA: 0x00013D9B File Offset: 0x00011F9B
		private static void ValidateEntityTypeHasKeyProperties(KeyValuePair<string, object>[] keyProperties, string actualEntityTypeName)
		{
			if (keyProperties == null || keyProperties.Length == 0)
			{
				throw new ODataException(Strings.ODataEntryMetadataContext_EntityTypeWithNoKeyProperties(actualEntityTypeName));
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00013DFC File Offset: 0x00011FFC
		private static KeyValuePair<string, object>[] GetPropertiesBySerializationInfoPropertyKind(ODataEntry entry, ODataPropertyKind propertyKind, string actualEntityTypeName)
		{
			KeyValuePair<string, object>[] array = ODataEntryMetadataContext.EmptyProperties;
			if (entry.NonComputedProperties != null)
			{
				array = Enumerable.ToArray<KeyValuePair<string, object>>(Enumerable.Select<ODataProperty, KeyValuePair<string, object>>(Enumerable.Where<ODataProperty>(entry.NonComputedProperties, (ODataProperty p) => p.SerializationInfo != null && p.SerializationInfo.PropertyKind == propertyKind), (ODataProperty p) => new KeyValuePair<string, object>(p.Name, ODataEntryMetadataContext.GetPrimitivePropertyClrValue(actualEntityTypeName, p, propertyKind == ODataPropertyKind.Key))));
			}
			return array;
		}

		// Token: 0x04000243 RID: 579
		private static readonly KeyValuePair<string, object>[] EmptyProperties = new KeyValuePair<string, object>[0];

		// Token: 0x04000244 RID: 580
		private readonly ODataEntry entry;

		// Token: 0x04000245 RID: 581
		private readonly IODataFeedAndEntryTypeContext typeContext;

		// Token: 0x04000246 RID: 582
		private KeyValuePair<string, object>[] keyProperties;

		// Token: 0x04000247 RID: 583
		private IEnumerable<KeyValuePair<string, object>> etagProperties;

		// Token: 0x04000248 RID: 584
		private IEnumerable<IEdmNavigationProperty> selectedNavigationProperties;

		// Token: 0x04000249 RID: 585
		private IDictionary<string, IEdmStructuralProperty> selectedStreamProperties;

		// Token: 0x0400024A RID: 586
		private IEnumerable<IEdmOperation> selectedBindableOperations;

		// Token: 0x02000089 RID: 137
		private sealed class ODataEntryMetadataContextWithoutModel : ODataEntryMetadataContext
		{
			// Token: 0x06000573 RID: 1395 RVA: 0x00013E75 File Offset: 0x00012075
			internal ODataEntryMetadataContextWithoutModel(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo)
				: base(entry, typeContext)
			{
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000574 RID: 1396 RVA: 0x00013E86 File Offset: 0x00012086
			public override ICollection<KeyValuePair<string, object>> KeyProperties
			{
				get
				{
					if (this.keyProperties == null)
					{
						this.keyProperties = ODataEntryMetadataContext.GetPropertiesBySerializationInfoPropertyKind(this.entry, ODataPropertyKind.Key, this.ActualEntityTypeName);
						ODataEntryMetadataContext.ValidateEntityTypeHasKeyProperties(this.keyProperties, this.ActualEntityTypeName);
					}
					return this.keyProperties;
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x06000575 RID: 1397 RVA: 0x00013EC0 File Offset: 0x000120C0
			public override IEnumerable<KeyValuePair<string, object>> ETagProperties
			{
				get
				{
					IEnumerable<KeyValuePair<string, object>> enumerable;
					if ((enumerable = this.etagProperties) == null)
					{
						enumerable = (this.etagProperties = ODataEntryMetadataContext.GetPropertiesBySerializationInfoPropertyKind(this.entry, ODataPropertyKind.ETag, this.ActualEntityTypeName));
					}
					return enumerable;
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x06000576 RID: 1398 RVA: 0x00013EF2 File Offset: 0x000120F2
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

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000577 RID: 1399 RVA: 0x00013F1C File Offset: 0x0001211C
			public override IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties
			{
				get
				{
					return ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.EmptyNavigationProperties;
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000578 RID: 1400 RVA: 0x00013F23 File Offset: 0x00012123
			public override IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties
			{
				get
				{
					return ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.EmptyStreamProperties;
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000579 RID: 1401 RVA: 0x00013F2A File Offset: 0x0001212A
			public override IEnumerable<IEdmOperation> SelectedBindableOperations
			{
				get
				{
					return ODataEntryMetadataContext.ODataEntryMetadataContextWithoutModel.EmptyOperations;
				}
			}

			// Token: 0x0400024B RID: 587
			private static readonly IEdmNavigationProperty[] EmptyNavigationProperties = new IEdmNavigationProperty[0];

			// Token: 0x0400024C RID: 588
			private static readonly Dictionary<string, IEdmStructuralProperty> EmptyStreamProperties = new Dictionary<string, IEdmStructuralProperty>(StringComparer.Ordinal);

			// Token: 0x0400024D RID: 589
			private static readonly IEdmOperation[] EmptyOperations = new IEdmOperation[0];

			// Token: 0x0400024E RID: 590
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;
		}

		// Token: 0x0200008A RID: 138
		private sealed class ODataEntryMetadataContextWithModel : ODataEntryMetadataContext
		{
			// Token: 0x0600057B RID: 1403 RVA: 0x00013F58 File Offset: 0x00012158
			internal ODataEntryMetadataContextWithModel(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, IEdmEntityType actualEntityType, IODataMetadataContext metadataContext, SelectedPropertiesNode selectedProperties)
				: base(entry, typeContext)
			{
				this.actualEntityType = actualEntityType;
				this.metadataContext = metadataContext;
				this.selectedProperties = selectedProperties;
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x0600057C RID: 1404 RVA: 0x00013FA0 File Offset: 0x000121A0
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

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x0600057D RID: 1405 RVA: 0x00014054 File Offset: 0x00012254
			public override IEnumerable<KeyValuePair<string, object>> ETagProperties
			{
				get
				{
					if (this.etagProperties == null)
					{
						IEnumerable<IEdmStructuralProperty> enumerable = this.ComputeETagPropertiesFromAnnotation();
						if (Enumerable.Any<IEdmStructuralProperty>(enumerable))
						{
							this.etagProperties = Enumerable.ToArray<KeyValuePair<string, object>>(Enumerable.Select<IEdmStructuralProperty, KeyValuePair<string, object>>(enumerable, (IEdmStructuralProperty p) => new KeyValuePair<string, object>(p.Name, ODataEntryMetadataContext.GetPrimitivePropertyClrValue(this.entry, p.Name, this.ActualEntityTypeName, false))));
						}
						else
						{
							enumerable = this.actualEntityType.StructuralProperties();
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
					}
					return this.etagProperties;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x0600057E RID: 1406 RVA: 0x00014103 File Offset: 0x00012303
			public override string ActualEntityTypeName
			{
				get
				{
					return this.actualEntityType.FullName();
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x0600057F RID: 1407 RVA: 0x00014110 File Offset: 0x00012310
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

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000580 RID: 1408 RVA: 0x00014144 File Offset: 0x00012344
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

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000581 RID: 1409 RVA: 0x000141A4 File Offset: 0x000123A4
			public override IEnumerable<IEdmOperation> SelectedBindableOperations
			{
				get
				{
					if (this.selectedBindableOperations == null)
					{
						bool mustBeContainerQualified = this.metadataContext.OperationsBoundToEntityTypeMustBeContainerQualified(this.actualEntityType);
						this.selectedBindableOperations = Enumerable.ToArray<IEdmOperation>(Enumerable.Where<IEdmOperation>(this.metadataContext.GetBindableOperationsForType(this.actualEntityType), (IEdmOperation operation) => this.selectedProperties.IsOperationSelected(this.actualEntityType, operation, mustBeContainerQualified)));
					}
					return this.selectedBindableOperations;
				}
			}

			// Token: 0x06000582 RID: 1410 RVA: 0x0001458C File Offset: 0x0001278C
			private IEnumerable<IEdmStructuralProperty> ComputeETagPropertiesFromAnnotation()
			{
				IEdmModel model = this.metadataContext.Model;
				IEdmEntitySet entitySet = model.FindDeclaredEntitySet(this.typeContext.NavigationSourceName);
				if (entitySet != null)
				{
					IEdmVocabularyAnnotation annotation = Enumerable.SingleOrDefault<IEdmVocabularyAnnotation>(model.FindDeclaredVocabularyAnnotations(entitySet), (IEdmVocabularyAnnotation t) => t.Term.FullName().Equals("Org.OData.Core.V1.OptimisticConcurrencyControl", 4) || t.Term.FullName().Equals("Org.OData.Core.V1.OptimisticConcurrency", 4));
					if (annotation is IEdmValueAnnotation)
					{
						IEdmExpression collectionExpression = (annotation as IEdmValueAnnotation).Value;
						if (collectionExpression is IEdmCollectionExpression)
						{
							IEnumerable<IEdmExpression> pathExpressions = Enumerable.Where<IEdmExpression>((collectionExpression as IEdmCollectionExpression).Elements, (IEdmExpression p) => p is IEdmPathExpression);
							using (IEnumerator<IEdmExpression> enumerator = pathExpressions.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									IEdmPathExpression pathExpression = (IEdmPathExpression)enumerator.Current;
									IEdmStructuralProperty property = Enumerable.FirstOrDefault<IEdmStructuralProperty>(this.actualEntityType.StructuralProperties(), (IEdmStructuralProperty p) => p.Name == Enumerable.LastOrDefault<string>(pathExpression.Path));
									if (property == null)
									{
										throw new ODataException(Strings.EdmValueUtils_PropertyDoesntExist(this.ActualEntityTypeName, Enumerable.LastOrDefault<string>(pathExpression.Path)));
									}
									yield return property;
								}
							}
						}
					}
				}
				yield break;
			}

			// Token: 0x0400024F RID: 591
			private readonly IEdmEntityType actualEntityType;

			// Token: 0x04000250 RID: 592
			private readonly IODataMetadataContext metadataContext;

			// Token: 0x04000251 RID: 593
			private readonly SelectedPropertiesNode selectedProperties;
		}
	}
}
