using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData
{
	// Token: 0x02000030 RID: 48
	internal sealed class PropertyAndAnnotationCollector
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00004ACD File Offset: 0x00002CCD
		internal PropertyAndAnnotationCollector(bool throwOnDuplicateProperty)
		{
			this.throwOnDuplicateProperty = throwOnDuplicateProperty;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00004B00 File Offset: 0x00002D00
		internal void CheckForDuplicatePropertyNames(ODataProperty property)
		{
			if (!this.throwOnDuplicateProperty)
			{
				return;
			}
			string name = property.Name;
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(name, out propertyData))
			{
				this.propertyData[name] = new PropertyAndAnnotationCollector.PropertyData(PropertyAndAnnotationCollector.PropertyState.SimpleProperty);
				return;
			}
			if (propertyData.State == PropertyAndAnnotationCollector.PropertyState.AnnotationSeen)
			{
				propertyData.State = PropertyAndAnnotationCollector.PropertyState.SimpleProperty;
				return;
			}
			throw new ODataException(Strings.DuplicatePropertyNamesNotAllowed(name));
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00004B5C File Offset: 0x00002D5C
		internal void ValidatePropertyUniquenessOnNestedResourceInfoStart(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (!this.throwOnDuplicateProperty)
			{
				return;
			}
			string name = nestedResourceInfo.Name;
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (this.propertyData.TryGetValue(name, out propertyData))
			{
				PropertyAndAnnotationCollector.CheckNestedResourceInfoDuplicateNameForExistingDuplicationRecord(name, propertyData);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00004B90 File Offset: 0x00002D90
		internal Uri ValidatePropertyUniquenessAndGetAssociationLink(ODataNestedResourceInfo nestedResourceInfo)
		{
			string name = nestedResourceInfo.Name;
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(name, out propertyData))
			{
				this.propertyData[name] = new PropertyAndAnnotationCollector.PropertyData(PropertyAndAnnotationCollector.PropertyState.NavigationProperty)
				{
					NestedResourceInfo = nestedResourceInfo
				};
				return null;
			}
			if (this.throwOnDuplicateProperty)
			{
				PropertyAndAnnotationCollector.CheckNestedResourceInfoDuplicateNameForExistingDuplicationRecord(name, propertyData);
			}
			propertyData.State = PropertyAndAnnotationCollector.PropertyState.NavigationProperty;
			propertyData.NestedResourceInfo = nestedResourceInfo;
			return propertyData.AssociationLinkUrl;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00004BF4 File Offset: 0x00002DF4
		internal ODataNestedResourceInfo ValidatePropertyOpenForAssociationLinkAndGetNestedResourceInfo(string propertyName, Uri associationLinkUrl)
		{
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, out propertyData))
			{
				this.propertyData[propertyName] = new PropertyAndAnnotationCollector.PropertyData(PropertyAndAnnotationCollector.PropertyState.NavigationProperty)
				{
					AssociationLinkUrl = associationLinkUrl
				};
				return null;
			}
			if (propertyData.State == PropertyAndAnnotationCollector.PropertyState.AnnotationSeen || (propertyData.State == PropertyAndAnnotationCollector.PropertyState.NavigationProperty && propertyData.AssociationLinkUrl == null))
			{
				propertyData.State = PropertyAndAnnotationCollector.PropertyState.NavigationProperty;
				propertyData.AssociationLinkUrl = associationLinkUrl;
				return propertyData.NestedResourceInfo;
			}
			throw new ODataException(Strings.DuplicateAnnotationNotAllowed("odata.associationLink"));
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00004C71 File Offset: 0x00002E71
		internal void Reset()
		{
			this.propertyData.Clear();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00004C80 File Offset: 0x00002E80
		internal void AddODataScopeAnnotation(string annotationName, object annotationValue)
		{
			if (annotationValue == null)
			{
				return;
			}
			try
			{
				this.odataScopeAnnotations.Add(annotationName, annotationValue);
			}
			catch (ArgumentException)
			{
				throw new ODataException(Strings.DuplicateAnnotationNotAllowed(annotationName));
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00004CC0 File Offset: 0x00002EC0
		internal void AddCustomScopeAnnotation(string annotationName, object annotationValue)
		{
			if (annotationValue == null)
			{
				return;
			}
			this.customScopeAnnotations.Add(new KeyValuePair<string, object>(annotationName, annotationValue));
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00004CD8 File Offset: 0x00002ED8
		internal IDictionary<string, object> GetODataScopeAnnotation()
		{
			return this.odataScopeAnnotations;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00004CE0 File Offset: 0x00002EE0
		internal IEnumerable<KeyValuePair<string, object>> GetCustomScopeAnnotation()
		{
			return this.customScopeAnnotations;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00004CE8 File Offset: 0x00002EE8
		internal void AddODataPropertyAnnotation(string propertyName, string annotationName, object annotationValue)
		{
			if (annotationValue == null)
			{
				return;
			}
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, out propertyData))
			{
				propertyData = (this.propertyData[propertyName] = new PropertyAndAnnotationCollector.PropertyData(PropertyAndAnnotationCollector.PropertyState.AnnotationSeen));
			}
			else if (propertyData.Processed)
			{
				throw new ODataException(Strings.PropertyAnnotationAfterTheProperty(annotationName, propertyName));
			}
			try
			{
				propertyData.ODataAnnotations.Add(annotationName, annotationValue);
			}
			catch (ArgumentException)
			{
				throw new ODataException(ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName) ? Strings.DuplicateAnnotationForInstanceAnnotationNotAllowed(annotationName, propertyName) : Strings.DuplicateAnnotationForPropertyNotAllowed(annotationName, propertyName));
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00004D74 File Offset: 0x00002F74
		internal void AddCustomPropertyAnnotation(string propertyName, string annotationName, object annotationValue)
		{
			if (annotationValue == null)
			{
				return;
			}
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, out propertyData))
			{
				propertyData = (this.propertyData[propertyName] = new PropertyAndAnnotationCollector.PropertyData(PropertyAndAnnotationCollector.PropertyState.AnnotationSeen));
			}
			else if (propertyData.Processed)
			{
				throw new ODataException(Strings.PropertyAnnotationAfterTheProperty(annotationName, propertyName));
			}
			propertyData.CustomAnnotations.Add(new KeyValuePair<string, object>(annotationName, annotationValue));
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00004DD4 File Offset: 0x00002FD4
		internal void SetDerivedTypeValidator(string propertyName, DerivedTypeValidator validator)
		{
			if (validator == null)
			{
				return;
			}
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, out propertyData))
			{
				propertyData = (this.propertyData[propertyName] = new PropertyAndAnnotationCollector.PropertyData(PropertyAndAnnotationCollector.PropertyState.AnnotationSeen));
			}
			propertyData.DerivedTypeValidator = validator;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00004E10 File Offset: 0x00003010
		internal DerivedTypeValidator GetDerivedTypeValidator(string propertyName)
		{
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, out propertyData))
			{
				return null;
			}
			return propertyData.DerivedTypeValidator;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00004E38 File Offset: 0x00003038
		internal IDictionary<string, object> GetODataPropertyAnnotations(string propertyName)
		{
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, out propertyData))
			{
				return PropertyAndAnnotationCollector.emptyDictionary;
			}
			return propertyData.ODataAnnotations;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00004E64 File Offset: 0x00003064
		internal IEnumerable<KeyValuePair<string, object>> GetCustomPropertyAnnotations(string propertyName)
		{
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, out propertyData))
			{
				return Enumerable.Empty<KeyValuePair<string, object>>();
			}
			return propertyData.CustomAnnotations;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004E90 File Offset: 0x00003090
		internal void MarkPropertyAsProcessed(string propertyName)
		{
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, out propertyData))
			{
				propertyData = (this.propertyData[propertyName] = new PropertyAndAnnotationCollector.PropertyData(PropertyAndAnnotationCollector.PropertyState.AnnotationSeen));
			}
			if (propertyData.Processed)
			{
				throw new ODataException((ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName) && !ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName)) ? Strings.DuplicateAnnotationNotAllowed(propertyName) : Strings.DuplicatePropertyNamesNotAllowed(propertyName));
			}
			propertyData.Processed = true;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00004EF4 File Offset: 0x000030F4
		internal void CheckIfPropertyOpenForAnnotations(string propertyName, string annotationName)
		{
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (this.propertyData.TryGetValue(propertyName, out propertyData) && propertyData.Processed)
			{
				throw new ODataException(Strings.PropertyAnnotationAfterTheProperty(annotationName, propertyName));
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004F26 File Offset: 0x00003126
		private static void CheckNestedResourceInfoDuplicateNameForExistingDuplicationRecord(string propertyName, PropertyAndAnnotationCollector.PropertyData propertyData)
		{
			if ((propertyData.State != PropertyAndAnnotationCollector.PropertyState.NavigationProperty || !(propertyData.AssociationLinkUrl != null) || propertyData.NestedResourceInfo != null) && propertyData.State != PropertyAndAnnotationCollector.PropertyState.AnnotationSeen)
			{
				throw new ODataException(Strings.DuplicatePropertyNamesNotAllowed(propertyName));
			}
		}

		// Token: 0x0400008D RID: 141
		private static readonly IDictionary<string, object> emptyDictionary = new Dictionary<string, object>();

		// Token: 0x0400008E RID: 142
		private readonly bool throwOnDuplicateProperty;

		// Token: 0x0400008F RID: 143
		private IDictionary<string, object> odataScopeAnnotations = new Dictionary<string, object>();

		// Token: 0x04000090 RID: 144
		private IList<KeyValuePair<string, object>> customScopeAnnotations = new List<KeyValuePair<string, object>>();

		// Token: 0x04000091 RID: 145
		private IDictionary<string, PropertyAndAnnotationCollector.PropertyData> propertyData = new Dictionary<string, PropertyAndAnnotationCollector.PropertyData>();

		// Token: 0x02000283 RID: 643
		private enum PropertyState
		{
			// Token: 0x04000BE2 RID: 3042
			AnnotationSeen,
			// Token: 0x04000BE3 RID: 3043
			SimpleProperty,
			// Token: 0x04000BE4 RID: 3044
			NavigationProperty
		}

		// Token: 0x02000284 RID: 644
		private sealed class PropertyData
		{
			// Token: 0x06001C2F RID: 7215 RVA: 0x00056097 File Offset: 0x00054297
			internal PropertyData(PropertyAndAnnotationCollector.PropertyState propertyState)
			{
				this.State = propertyState;
				this.ODataAnnotations = new Dictionary<string, object>();
				this.CustomAnnotations = new List<KeyValuePair<string, object>>();
			}

			// Token: 0x170005BF RID: 1471
			// (get) Token: 0x06001C30 RID: 7216 RVA: 0x000560BC File Offset: 0x000542BC
			// (set) Token: 0x06001C31 RID: 7217 RVA: 0x000560C4 File Offset: 0x000542C4
			internal PropertyAndAnnotationCollector.PropertyState State { get; set; }

			// Token: 0x170005C0 RID: 1472
			// (get) Token: 0x06001C32 RID: 7218 RVA: 0x000560CD File Offset: 0x000542CD
			// (set) Token: 0x06001C33 RID: 7219 RVA: 0x000560D5 File Offset: 0x000542D5
			internal ODataNestedResourceInfo NestedResourceInfo { get; set; }

			// Token: 0x170005C1 RID: 1473
			// (get) Token: 0x06001C34 RID: 7220 RVA: 0x000560DE File Offset: 0x000542DE
			// (set) Token: 0x06001C35 RID: 7221 RVA: 0x000560E6 File Offset: 0x000542E6
			internal Uri AssociationLinkUrl { get; set; }

			// Token: 0x170005C2 RID: 1474
			// (get) Token: 0x06001C36 RID: 7222 RVA: 0x000560EF File Offset: 0x000542EF
			// (set) Token: 0x06001C37 RID: 7223 RVA: 0x000560F7 File Offset: 0x000542F7
			internal IDictionary<string, object> ODataAnnotations { get; private set; }

			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x06001C38 RID: 7224 RVA: 0x00056100 File Offset: 0x00054300
			// (set) Token: 0x06001C39 RID: 7225 RVA: 0x00056108 File Offset: 0x00054308
			internal IList<KeyValuePair<string, object>> CustomAnnotations { get; private set; }

			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x06001C3A RID: 7226 RVA: 0x00056111 File Offset: 0x00054311
			// (set) Token: 0x06001C3B RID: 7227 RVA: 0x00056119 File Offset: 0x00054319
			internal bool Processed { get; set; }

			// Token: 0x170005C5 RID: 1477
			// (get) Token: 0x06001C3C RID: 7228 RVA: 0x00056122 File Offset: 0x00054322
			// (set) Token: 0x06001C3D RID: 7229 RVA: 0x0005612A File Offset: 0x0005432A
			internal DerivedTypeValidator DerivedTypeValidator { get; set; }
		}
	}
}
