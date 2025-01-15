using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData
{
	// Token: 0x0200022E RID: 558
	internal sealed class DuplicatePropertyNamesChecker
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x0003F420 File Offset: 0x0003D620
		public DuplicatePropertyNamesChecker(bool allowDuplicateProperties, bool isResponse)
		{
			this.allowDuplicateProperties = allowDuplicateProperties;
			this.isResponse = isResponse;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0003F441 File Offset: 0x0003D641
		public DuplicatePropertyNamesChecker.PropertyAnnotationCollector AnnotationCollector
		{
			get
			{
				return this.annotationCollector;
			}
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0003F44C File Offset: 0x0003D64C
		internal void CheckForDuplicatePropertyNames(ODataProperty property)
		{
			string name = property.Name;
			DuplicatePropertyNamesChecker.DuplicationKind duplicationKind = DuplicatePropertyNamesChecker.GetDuplicationKind(property);
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(name, out duplicationRecord))
			{
				this.propertyNameCache.Add(name, new DuplicatePropertyNamesChecker.DuplicationRecord(duplicationKind));
				return;
			}
			if (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen)
			{
				duplicationRecord.DuplicationKind = duplicationKind;
				return;
			}
			if (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.Prohibited || duplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.Prohibited || (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && duplicationRecord.AssociationLink != null) || !this.allowDuplicateProperties)
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(name));
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003F4C8 File Offset: 0x0003D6C8
		internal void CheckForDuplicatePropertyNamesOnNavigationLinkStart(ODataNavigationLink navigationLink)
		{
			string name = navigationLink.Name;
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (this.propertyNameCache != null && this.propertyNameCache.TryGetValue(name, ref duplicationRecord))
			{
				this.CheckNavigationLinkDuplicateNameForExistingDuplicationRecord(name, duplicationRecord);
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003F4FC File Offset: 0x0003D6FC
		internal ODataAssociationLink CheckForDuplicatePropertyNames(ODataNavigationLink navigationLink, bool isExpanded, bool? isCollection)
		{
			string name = navigationLink.Name;
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(name, out duplicationRecord))
			{
				DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord2 = new DuplicatePropertyNamesChecker.DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty);
				DuplicatePropertyNamesChecker.ApplyNavigationLinkToDuplicationRecord(duplicationRecord2, navigationLink, isExpanded, isCollection);
				this.propertyNameCache.Add(name, duplicationRecord2);
				return null;
			}
			this.CheckNavigationLinkDuplicateNameForExistingDuplicationRecord(name, duplicationRecord);
			if (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen || (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && duplicationRecord.AssociationLink != null && duplicationRecord.NavigationLink == null))
			{
				DuplicatePropertyNamesChecker.ApplyNavigationLinkToDuplicationRecord(duplicationRecord, navigationLink, isExpanded, isCollection);
			}
			else if (this.allowDuplicateProperties)
			{
				duplicationRecord.DuplicationKind = DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty;
				DuplicatePropertyNamesChecker.ApplyNavigationLinkToDuplicationRecord(duplicationRecord, navigationLink, isExpanded, isCollection);
			}
			else
			{
				bool? isCollectionEffectiveValue = DuplicatePropertyNamesChecker.GetIsCollectionEffectiveValue(isExpanded, isCollection);
				if (isCollectionEffectiveValue == false || duplicationRecord.NavigationPropertyIsCollection == false)
				{
					throw new ODataException(Strings.DuplicatePropertyNamesChecker_MultipleLinksForSingleton(name));
				}
				if (isCollectionEffectiveValue != null)
				{
					duplicationRecord.NavigationPropertyIsCollection = isCollectionEffectiveValue;
				}
			}
			return duplicationRecord.AssociationLink;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0003F5E8 File Offset: 0x0003D7E8
		internal ODataNavigationLink CheckForDuplicateAssociationLinkNames(ODataAssociationLink associationLink)
		{
			string name = associationLink.Name;
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(name, out duplicationRecord))
			{
				this.propertyNameCache.Add(name, new DuplicatePropertyNamesChecker.DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty)
				{
					AssociationLink = associationLink
				});
				return null;
			}
			if (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen || (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && duplicationRecord.AssociationLink == null))
			{
				duplicationRecord.DuplicationKind = DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty;
				duplicationRecord.AssociationLink = associationLink;
				return duplicationRecord.NavigationLink;
			}
			throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(name));
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0003F65F File Offset: 0x0003D85F
		internal void Clear()
		{
			if (this.propertyNameCache != null)
			{
				this.propertyNameCache.Clear();
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0003F674 File Offset: 0x0003D874
		internal void AddODataPropertyAnnotation(string propertyName, string annotationName, object annotationValue)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecordToAddPropertyAnnotation = this.GetDuplicationRecordToAddPropertyAnnotation(propertyName, annotationName);
			Dictionary<string, object> dictionary = duplicationRecordToAddPropertyAnnotation.PropertyODataAnnotations;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
				duplicationRecordToAddPropertyAnnotation.PropertyODataAnnotations = dictionary;
			}
			else if (dictionary.ContainsKey(annotationName))
			{
				if (ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName))
				{
					throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicateAnnotationForInstanceAnnotationNotAllowed(annotationName, propertyName));
				}
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicateAnnotationForPropertyNotAllowed(annotationName, propertyName));
			}
			dictionary.Add(annotationName, annotationValue);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0003F6DC File Offset: 0x0003D8DC
		internal void AddCustomPropertyAnnotation(string propertyName, string annotationName)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecordToAddPropertyAnnotation = this.GetDuplicationRecordToAddPropertyAnnotation(propertyName, annotationName);
			HashSet<string> hashSet = duplicationRecordToAddPropertyAnnotation.PropertyCustomAnnotations;
			if (hashSet == null)
			{
				hashSet = new HashSet<string>(StringComparer.Ordinal);
				duplicationRecordToAddPropertyAnnotation.PropertyCustomAnnotations = hashSet;
			}
			else if (hashSet.Contains(annotationName))
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicateAnnotationForPropertyNotAllowed(annotationName, propertyName));
			}
			hashSet.Add(annotationName);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0003F730 File Offset: 0x0003D930
		internal Dictionary<string, object> GetODataPropertyAnnotations(string propertyName)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(propertyName, out duplicationRecord))
			{
				return null;
			}
			DuplicatePropertyNamesChecker.ThrowIfPropertyIsProcessed(propertyName, duplicationRecord);
			return duplicationRecord.PropertyODataAnnotations;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0003F758 File Offset: 0x0003D958
		internal void MarkPropertyAsProcessed(string propertyName)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(propertyName, out duplicationRecord))
			{
				duplicationRecord = new DuplicatePropertyNamesChecker.DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen);
				this.propertyNameCache.Add(propertyName, duplicationRecord);
			}
			DuplicatePropertyNamesChecker.ThrowIfPropertyIsProcessed(propertyName, duplicationRecord);
			duplicationRecord.PropertyODataAnnotations = DuplicatePropertyNamesChecker.propertyAnnotationsProcessedToken;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0003F7A0 File Offset: 0x0003D9A0
		internal IEnumerable<string> GetAllUnprocessedProperties()
		{
			if (this.propertyNameCache == null)
			{
				return Enumerable.Empty<string>();
			}
			return Enumerable.Select<KeyValuePair<string, DuplicatePropertyNamesChecker.DuplicationRecord>, string>(Enumerable.Where<KeyValuePair<string, DuplicatePropertyNamesChecker.DuplicationRecord>>(this.propertyNameCache, new Func<KeyValuePair<string, DuplicatePropertyNamesChecker.DuplicationRecord>, bool>(DuplicatePropertyNamesChecker.IsPropertyUnprocessed)), (KeyValuePair<string, DuplicatePropertyNamesChecker.DuplicationRecord> property) => property.Key);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
		private static void ThrowIfPropertyIsProcessed(string propertyName, DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord)
		{
			if (!object.ReferenceEquals(duplicationRecord.PropertyODataAnnotations, DuplicatePropertyNamesChecker.propertyAnnotationsProcessedToken))
			{
				return;
			}
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName) && !ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName))
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicateAnnotationNotAllowed(propertyName));
			}
			throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(propertyName));
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0003F830 File Offset: 0x0003DA30
		private static bool IsPropertyUnprocessed(KeyValuePair<string, DuplicatePropertyNamesChecker.DuplicationRecord> property)
		{
			return !string.IsNullOrEmpty(property.Key) && !object.ReferenceEquals(property.Value.PropertyODataAnnotations, DuplicatePropertyNamesChecker.propertyAnnotationsProcessedToken);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0003F85C File Offset: 0x0003DA5C
		private static DuplicatePropertyNamesChecker.DuplicationKind GetDuplicationKind(ODataProperty property)
		{
			object value = property.Value;
			if (value == null || (!(value is ODataStreamReferenceValue) && !(value is ODataCollectionValue)))
			{
				return DuplicatePropertyNamesChecker.DuplicationKind.PotentiallyAllowed;
			}
			return DuplicatePropertyNamesChecker.DuplicationKind.Prohibited;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0003F888 File Offset: 0x0003DA88
		private static bool? GetIsCollectionEffectiveValue(bool isExpanded, bool? isCollection)
		{
			if (isExpanded)
			{
				return isCollection;
			}
			if (!(isCollection == true))
			{
				return default(bool?);
			}
			return new bool?(true);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0003F8C1 File Offset: 0x0003DAC1
		private static void ApplyNavigationLinkToDuplicationRecord(DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord, ODataNavigationLink navigationLink, bool isExpanded, bool? isCollection)
		{
			duplicationRecord.DuplicationKind = DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty;
			duplicationRecord.NavigationLink = navigationLink;
			duplicationRecord.NavigationPropertyIsCollection = DuplicatePropertyNamesChecker.GetIsCollectionEffectiveValue(isExpanded, isCollection);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0003F8DE File Offset: 0x0003DADE
		private bool TryGetDuplicationRecord(string propertyName, out DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord)
		{
			if (this.propertyNameCache == null)
			{
				this.propertyNameCache = new Dictionary<string, DuplicatePropertyNamesChecker.DuplicationRecord>(StringComparer.Ordinal);
				duplicationRecord = null;
				return false;
			}
			return this.propertyNameCache.TryGetValue(propertyName, ref duplicationRecord);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0003F90C File Offset: 0x0003DB0C
		private void CheckNavigationLinkDuplicateNameForExistingDuplicationRecord(string propertyName, DuplicatePropertyNamesChecker.DuplicationRecord existingDuplicationRecord)
		{
			if (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && existingDuplicationRecord.AssociationLink != null && existingDuplicationRecord.NavigationLink == null)
			{
				return;
			}
			if (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.Prohibited || (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.PotentiallyAllowed && !this.allowDuplicateProperties) || (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && this.isResponse && !this.allowDuplicateProperties))
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(propertyName));
			}
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0003F974 File Offset: 0x0003DB74
		private DuplicatePropertyNamesChecker.DuplicationRecord GetDuplicationRecordToAddPropertyAnnotation(string propertyName, string annotationName)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(propertyName, out duplicationRecord))
			{
				duplicationRecord = new DuplicatePropertyNamesChecker.DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen);
				this.propertyNameCache.Add(propertyName, duplicationRecord);
			}
			if (object.ReferenceEquals(duplicationRecord.PropertyODataAnnotations, DuplicatePropertyNamesChecker.propertyAnnotationsProcessedToken))
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_PropertyAnnotationAfterTheProperty(annotationName, propertyName));
			}
			return duplicationRecord;
		}

		// Token: 0x0400067A RID: 1658
		private static readonly Dictionary<string, object> propertyAnnotationsProcessedToken = new Dictionary<string, object>(0, StringComparer.Ordinal);

		// Token: 0x0400067B RID: 1659
		private readonly bool allowDuplicateProperties;

		// Token: 0x0400067C RID: 1660
		private readonly bool isResponse;

		// Token: 0x0400067D RID: 1661
		private Dictionary<string, DuplicatePropertyNamesChecker.DuplicationRecord> propertyNameCache;

		// Token: 0x0400067E RID: 1662
		private DuplicatePropertyNamesChecker.PropertyAnnotationCollector annotationCollector = new DuplicatePropertyNamesChecker.PropertyAnnotationCollector();

		// Token: 0x0200022F RID: 559
		private enum DuplicationKind
		{
			// Token: 0x04000681 RID: 1665
			PropertyAnnotationSeen,
			// Token: 0x04000682 RID: 1666
			Prohibited,
			// Token: 0x04000683 RID: 1667
			PotentiallyAllowed,
			// Token: 0x04000684 RID: 1668
			NavigationProperty
		}

		// Token: 0x02000230 RID: 560
		internal sealed class PropertyAnnotationCollector
		{
			// Token: 0x170003D7 RID: 983
			// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0003F9D2 File Offset: 0x0003DBD2
			// (set) Token: 0x060010F4 RID: 4340 RVA: 0x0003F9DA File Offset: 0x0003DBDA
			public bool ShouldCollectAnnotation
			{
				get
				{
					return this.shouldCollectAnnotation;
				}
				set
				{
					this.shouldCollectAnnotation = value;
				}
			}

			// Token: 0x060010F5 RID: 4341 RVA: 0x0003F9E3 File Offset: 0x0003DBE3
			public void TryPeekAndCollectAnnotationRawJson(BufferingJsonReader jsonReader, string propertyName, string annotationName)
			{
				if (this.shouldCollectAnnotation)
				{
					this.PeekAndCollectAnnotationRawJson(jsonReader, propertyName, annotationName);
				}
			}

			// Token: 0x060010F6 RID: 4342 RVA: 0x0003F9F6 File Offset: 0x0003DBF6
			public void TryAddPropertyAnnotationRawJson(string propertyName, string annotationName, string rawJson)
			{
				if (this.shouldCollectAnnotation)
				{
					this.AddPropertyAnnotationRawJson(propertyName, annotationName, rawJson);
				}
			}

			// Token: 0x060010F7 RID: 4343 RVA: 0x0003FA0C File Offset: 0x0003DC0C
			public ODataJsonLightRawAnnotationSet GetPropertyRawAnnotationSet(string propertyName)
			{
				Dictionary<string, string> dictionary = null;
				if (!this.propertyAnnotations.TryGetValue(propertyName, ref dictionary))
				{
					return null;
				}
				return new ODataJsonLightRawAnnotationSet
				{
					Annotations = dictionary
				};
			}

			// Token: 0x060010F8 RID: 4344 RVA: 0x0003FA3C File Offset: 0x0003DC3C
			private void PeekAndCollectAnnotationRawJson(BufferingJsonReader jsonReader, string propertyName, string annotationName)
			{
				if (jsonReader.IsBuffering)
				{
					return;
				}
				try
				{
					jsonReader.StartBuffering();
					if (jsonReader.NodeType == JsonNodeType.Property)
					{
						jsonReader.Read();
					}
					StringBuilder stringBuilder = new StringBuilder();
					jsonReader.SkipValue(stringBuilder);
					string text = stringBuilder.ToString();
					this.AddPropertyAnnotationRawJson(propertyName, annotationName, text);
				}
				finally
				{
					jsonReader.StopBuffering();
				}
			}

			// Token: 0x060010F9 RID: 4345 RVA: 0x0003FAA0 File Offset: 0x0003DCA0
			private void AddPropertyAnnotationRawJson(string propertyName, string annotationName, string rawJson)
			{
				Dictionary<string, string> dictionary = null;
				if (!this.propertyAnnotations.TryGetValue(propertyName, ref dictionary))
				{
					dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
					this.propertyAnnotations[propertyName] = dictionary;
				}
				dictionary[annotationName] = rawJson;
			}

			// Token: 0x04000685 RID: 1669
			private Dictionary<string, Dictionary<string, string>> propertyAnnotations = new Dictionary<string, Dictionary<string, string>>(StringComparer.Ordinal);

			// Token: 0x04000686 RID: 1670
			private bool shouldCollectAnnotation;
		}

		// Token: 0x02000231 RID: 561
		private sealed class DuplicationRecord
		{
			// Token: 0x060010FB RID: 4347 RVA: 0x0003FAF7 File Offset: 0x0003DCF7
			public DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind duplicationKind)
			{
				this.DuplicationKind = duplicationKind;
			}

			// Token: 0x170003D8 RID: 984
			// (get) Token: 0x060010FC RID: 4348 RVA: 0x0003FB06 File Offset: 0x0003DD06
			// (set) Token: 0x060010FD RID: 4349 RVA: 0x0003FB0E File Offset: 0x0003DD0E
			public DuplicatePropertyNamesChecker.DuplicationKind DuplicationKind { get; set; }

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x060010FE RID: 4350 RVA: 0x0003FB17 File Offset: 0x0003DD17
			// (set) Token: 0x060010FF RID: 4351 RVA: 0x0003FB1F File Offset: 0x0003DD1F
			public ODataNavigationLink NavigationLink { get; set; }

			// Token: 0x170003DA RID: 986
			// (get) Token: 0x06001100 RID: 4352 RVA: 0x0003FB28 File Offset: 0x0003DD28
			// (set) Token: 0x06001101 RID: 4353 RVA: 0x0003FB30 File Offset: 0x0003DD30
			public ODataAssociationLink AssociationLink { get; set; }

			// Token: 0x170003DB RID: 987
			// (get) Token: 0x06001102 RID: 4354 RVA: 0x0003FB39 File Offset: 0x0003DD39
			// (set) Token: 0x06001103 RID: 4355 RVA: 0x0003FB41 File Offset: 0x0003DD41
			public bool? NavigationPropertyIsCollection { get; set; }

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x06001104 RID: 4356 RVA: 0x0003FB4A File Offset: 0x0003DD4A
			// (set) Token: 0x06001105 RID: 4357 RVA: 0x0003FB52 File Offset: 0x0003DD52
			public Dictionary<string, object> PropertyODataAnnotations { get; set; }

			// Token: 0x170003DD RID: 989
			// (get) Token: 0x06001106 RID: 4358 RVA: 0x0003FB5B File Offset: 0x0003DD5B
			// (set) Token: 0x06001107 RID: 4359 RVA: 0x0003FB63 File Offset: 0x0003DD63
			public HashSet<string> PropertyCustomAnnotations { get; set; }
		}
	}
}
