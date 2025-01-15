using System;
using System.Collections.Generic;
using Microsoft.OData.Core.JsonLight;

namespace Microsoft.OData.Core
{
	// Token: 0x02000071 RID: 113
	internal sealed class DuplicatePropertyNamesChecker
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x00011511 File Offset: 0x0000F711
		public DuplicatePropertyNamesChecker(bool allowDuplicateProperties, bool isResponse, bool disabled = false)
		{
			this.allowDuplicateProperties = allowDuplicateProperties;
			this.isResponse = isResponse;
			this.disabled = disabled;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00011530 File Offset: 0x0000F730
		internal void CheckForDuplicatePropertyNames(ODataProperty property)
		{
			if (this.disabled)
			{
				return;
			}
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
			if (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.Prohibited || duplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.Prohibited || (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && duplicationRecord.AssociationLinkName != null) || !this.allowDuplicateProperties)
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(name));
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000115B4 File Offset: 0x0000F7B4
		internal void CheckForDuplicatePropertyNamesOnNavigationLinkStart(ODataNavigationLink navigationLink)
		{
			if (this.disabled)
			{
				return;
			}
			string name = navigationLink.Name;
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (this.propertyNameCache != null && this.propertyNameCache.TryGetValue(name, ref duplicationRecord))
			{
				this.CheckNavigationLinkDuplicateNameForExistingDuplicationRecord(name, duplicationRecord);
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000115F4 File Offset: 0x0000F7F4
		internal Uri CheckForDuplicatePropertyNames(ODataNavigationLink navigationLink, bool isExpanded, bool? isCollection)
		{
			if (this.disabled)
			{
				return null;
			}
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
			if (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen || (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && duplicationRecord.AssociationLinkName != null && duplicationRecord.NavigationLink == null))
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
			return duplicationRecord.AssociationLinkUrl;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000116EC File Offset: 0x0000F8EC
		internal ODataNavigationLink CheckForDuplicateAssociationLinkNames(string associationLinkName, Uri associationLinkUrl)
		{
			if (this.disabled)
			{
				return null;
			}
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(associationLinkName, out duplicationRecord))
			{
				this.propertyNameCache.Add(associationLinkName, new DuplicatePropertyNamesChecker.DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty)
				{
					AssociationLinkName = associationLinkName,
					AssociationLinkUrl = associationLinkUrl
				});
				return null;
			}
			if (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen || (duplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && duplicationRecord.AssociationLinkName == null))
			{
				duplicationRecord.DuplicationKind = DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty;
				duplicationRecord.AssociationLinkName = associationLinkName;
				duplicationRecord.AssociationLinkUrl = associationLinkUrl;
				return duplicationRecord.NavigationLink;
			}
			throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(associationLinkName));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00011774 File Offset: 0x0000F974
		internal void Clear()
		{
			if (this.propertyNameCache != null)
			{
				this.propertyNameCache.Clear();
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001178C File Offset: 0x0000F98C
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

		// Token: 0x060004A4 RID: 1188 RVA: 0x000117F4 File Offset: 0x0000F9F4
		internal void AddCustomPropertyAnnotation(string propertyName, string annotationName, object annotationValue = null)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecordToAddPropertyAnnotation = this.GetDuplicationRecordToAddPropertyAnnotation(propertyName, annotationName);
			Dictionary<string, object> dictionary = duplicationRecordToAddPropertyAnnotation.PropertyCustomAnnotations;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
				duplicationRecordToAddPropertyAnnotation.PropertyCustomAnnotations = dictionary;
			}
			else if (dictionary.ContainsKey(annotationName))
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicateAnnotationForPropertyNotAllowed(annotationName, propertyName));
			}
			dictionary.Add(annotationName, annotationValue);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00011848 File Offset: 0x0000FA48
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

		// Token: 0x060004A6 RID: 1190 RVA: 0x00011870 File Offset: 0x0000FA70
		internal Dictionary<string, object> GetCustomPropertyAnnotations(string propertyName)
		{
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(propertyName, out duplicationRecord))
			{
				return null;
			}
			DuplicatePropertyNamesChecker.ThrowIfPropertyIsProcessed(propertyName, duplicationRecord);
			return duplicationRecord.PropertyCustomAnnotations;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00011898 File Offset: 0x0000FA98
		internal void MarkPropertyAsProcessed(string propertyName)
		{
			if (this.disabled)
			{
				return;
			}
			DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord;
			if (!this.TryGetDuplicationRecord(propertyName, out duplicationRecord))
			{
				duplicationRecord = new DuplicatePropertyNamesChecker.DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind.PropertyAnnotationSeen);
				this.propertyNameCache.Add(propertyName, duplicationRecord);
			}
			DuplicatePropertyNamesChecker.ThrowIfPropertyIsProcessed(propertyName, duplicationRecord);
			duplicationRecord.PropertyODataAnnotations = DuplicatePropertyNamesChecker.propertyAnnotationsProcessedToken;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000118DF File Offset: 0x0000FADF
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

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001191C File Offset: 0x0000FB1C
		private static DuplicatePropertyNamesChecker.DuplicationKind GetDuplicationKind(ODataProperty property)
		{
			object value = property.Value;
			if (value == null || (!(value is ODataStreamReferenceValue) && !(value is ODataCollectionValue)))
			{
				return DuplicatePropertyNamesChecker.DuplicationKind.PotentiallyAllowed;
			}
			return DuplicatePropertyNamesChecker.DuplicationKind.Prohibited;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00011948 File Offset: 0x0000FB48
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

		// Token: 0x060004AB RID: 1195 RVA: 0x00011981 File Offset: 0x0000FB81
		private static void ApplyNavigationLinkToDuplicationRecord(DuplicatePropertyNamesChecker.DuplicationRecord duplicationRecord, ODataNavigationLink navigationLink, bool isExpanded, bool? isCollection)
		{
			duplicationRecord.DuplicationKind = DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty;
			duplicationRecord.NavigationLink = navigationLink;
			duplicationRecord.NavigationPropertyIsCollection = DuplicatePropertyNamesChecker.GetIsCollectionEffectiveValue(isExpanded, isCollection);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001199E File Offset: 0x0000FB9E
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

		// Token: 0x060004AD RID: 1197 RVA: 0x000119CC File Offset: 0x0000FBCC
		private void CheckNavigationLinkDuplicateNameForExistingDuplicationRecord(string propertyName, DuplicatePropertyNamesChecker.DuplicationRecord existingDuplicationRecord)
		{
			if (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && existingDuplicationRecord.AssociationLinkUrl != null && existingDuplicationRecord.NavigationLink == null)
			{
				return;
			}
			if (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.Prohibited || (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.PotentiallyAllowed && !this.allowDuplicateProperties) || (existingDuplicationRecord.DuplicationKind == DuplicatePropertyNamesChecker.DuplicationKind.NavigationProperty && this.isResponse && !this.allowDuplicateProperties))
			{
				throw new ODataException(Strings.DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(propertyName));
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00011A38 File Offset: 0x0000FC38
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

		// Token: 0x0400020F RID: 527
		private static readonly Dictionary<string, object> propertyAnnotationsProcessedToken = new Dictionary<string, object>(0, StringComparer.Ordinal);

		// Token: 0x04000210 RID: 528
		private readonly bool allowDuplicateProperties;

		// Token: 0x04000211 RID: 529
		private readonly bool isResponse;

		// Token: 0x04000212 RID: 530
		private readonly bool disabled;

		// Token: 0x04000213 RID: 531
		private Dictionary<string, DuplicatePropertyNamesChecker.DuplicationRecord> propertyNameCache;

		// Token: 0x02000072 RID: 114
		private enum DuplicationKind
		{
			// Token: 0x04000215 RID: 533
			PropertyAnnotationSeen,
			// Token: 0x04000216 RID: 534
			Prohibited,
			// Token: 0x04000217 RID: 535
			PotentiallyAllowed,
			// Token: 0x04000218 RID: 536
			NavigationProperty
		}

		// Token: 0x02000073 RID: 115
		private sealed class DuplicationRecord
		{
			// Token: 0x060004B0 RID: 1200 RVA: 0x00011A96 File Offset: 0x0000FC96
			public DuplicationRecord(DuplicatePropertyNamesChecker.DuplicationKind duplicationKind)
			{
				this.DuplicationKind = duplicationKind;
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00011AA5 File Offset: 0x0000FCA5
			// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00011AAD File Offset: 0x0000FCAD
			public DuplicatePropertyNamesChecker.DuplicationKind DuplicationKind { get; set; }

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00011AB6 File Offset: 0x0000FCB6
			// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00011ABE File Offset: 0x0000FCBE
			public ODataNavigationLink NavigationLink { get; set; }

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00011AC7 File Offset: 0x0000FCC7
			// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00011ACF File Offset: 0x0000FCCF
			public string AssociationLinkName { get; set; }

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00011AD8 File Offset: 0x0000FCD8
			// (set) Token: 0x060004B8 RID: 1208 RVA: 0x00011AE0 File Offset: 0x0000FCE0
			public Uri AssociationLinkUrl { get; set; }

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00011AE9 File Offset: 0x0000FCE9
			// (set) Token: 0x060004BA RID: 1210 RVA: 0x00011AF1 File Offset: 0x0000FCF1
			public bool? NavigationPropertyIsCollection { get; set; }

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060004BB RID: 1211 RVA: 0x00011AFA File Offset: 0x0000FCFA
			// (set) Token: 0x060004BC RID: 1212 RVA: 0x00011B02 File Offset: 0x0000FD02
			public Dictionary<string, object> PropertyODataAnnotations { get; set; }

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x060004BD RID: 1213 RVA: 0x00011B0B File Offset: 0x0000FD0B
			// (set) Token: 0x060004BE RID: 1214 RVA: 0x00011B13 File Offset: 0x0000FD13
			public Dictionary<string, object> PropertyCustomAnnotations { get; set; }
		}
	}
}
