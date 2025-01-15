using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData
{
	// Token: 0x020000C6 RID: 198
	internal sealed class PropertyAndAnnotationCollector
	{
		// Token: 0x06000788 RID: 1928 RVA: 0x000153E4 File Offset: 0x000135E4
		internal PropertyAndAnnotationCollector(bool throwOnDuplicateProperty)
		{
			this.throwOnDuplicateProperty = throwOnDuplicateProperty;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00015414 File Offset: 0x00013614
		internal void CheckForDuplicatePropertyNames(ODataProperty property)
		{
			if (!this.throwOnDuplicateProperty)
			{
				return;
			}
			string name = property.Name;
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(name, ref propertyData))
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

		// Token: 0x0600078A RID: 1930 RVA: 0x00015470 File Offset: 0x00013670
		internal void ValidatePropertyUniquenessOnNestedResourceInfoStart(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (!this.throwOnDuplicateProperty)
			{
				return;
			}
			string name = nestedResourceInfo.Name;
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (this.propertyData.TryGetValue(name, ref propertyData))
			{
				PropertyAndAnnotationCollector.CheckNestedResourceInfoDuplicateNameForExistingDuplicationRecord(name, propertyData);
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000154A4 File Offset: 0x000136A4
		internal Uri ValidatePropertyUniquenessAndGetAssociationLink(ODataNestedResourceInfo nestedResourceInfo)
		{
			string name = nestedResourceInfo.Name;
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(name, ref propertyData))
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

		// Token: 0x0600078C RID: 1932 RVA: 0x00015508 File Offset: 0x00013708
		internal ODataNestedResourceInfo ValidatePropertyOpenForAssociationLinkAndGetNestedResourceInfo(string propertyName, Uri associationLinkUrl)
		{
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, ref propertyData))
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

		// Token: 0x0600078D RID: 1933 RVA: 0x00015585 File Offset: 0x00013785
		internal void Reset()
		{
			this.propertyData.Clear();
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00015594 File Offset: 0x00013794
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

		// Token: 0x0600078F RID: 1935 RVA: 0x000155D4 File Offset: 0x000137D4
		internal void AddCustomScopeAnnotation(string annotationName, object annotationValue)
		{
			if (annotationValue == null)
			{
				return;
			}
			this.customScopeAnnotations.Add(new KeyValuePair<string, object>(annotationName, annotationValue));
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000155EC File Offset: 0x000137EC
		internal IDictionary<string, object> GetODataScopeAnnotation()
		{
			return this.odataScopeAnnotations;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000155F4 File Offset: 0x000137F4
		internal IEnumerable<KeyValuePair<string, object>> GetCustomScopeAnnotation()
		{
			return this.customScopeAnnotations;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000155FC File Offset: 0x000137FC
		internal void AddODataPropertyAnnotation(string propertyName, string annotationName, object annotationValue)
		{
			if (annotationValue == null)
			{
				return;
			}
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, ref propertyData))
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

		// Token: 0x06000793 RID: 1939 RVA: 0x00015688 File Offset: 0x00013888
		internal void AddCustomPropertyAnnotation(string propertyName, string annotationName, object annotationValue)
		{
			if (annotationValue == null)
			{
				return;
			}
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, ref propertyData))
			{
				propertyData = (this.propertyData[propertyName] = new PropertyAndAnnotationCollector.PropertyData(PropertyAndAnnotationCollector.PropertyState.AnnotationSeen));
			}
			else if (propertyData.Processed)
			{
				throw new ODataException(Strings.PropertyAnnotationAfterTheProperty(annotationName, propertyName));
			}
			propertyData.CustomAnnotations.Add(new KeyValuePair<string, object>(annotationName, annotationValue));
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000156E8 File Offset: 0x000138E8
		internal IDictionary<string, object> GetODataPropertyAnnotations(string propertyName)
		{
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, ref propertyData))
			{
				return PropertyAndAnnotationCollector.emptyDictionary;
			}
			return propertyData.ODataAnnotations;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00015714 File Offset: 0x00013914
		internal IEnumerable<KeyValuePair<string, object>> GetCustomPropertyAnnotations(string propertyName)
		{
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, ref propertyData))
			{
				return Enumerable.Empty<KeyValuePair<string, object>>();
			}
			return propertyData.CustomAnnotations;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00015740 File Offset: 0x00013940
		internal void MarkPropertyAsProcessed(string propertyName)
		{
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (!this.propertyData.TryGetValue(propertyName, ref propertyData))
			{
				propertyData = (this.propertyData[propertyName] = new PropertyAndAnnotationCollector.PropertyData(PropertyAndAnnotationCollector.PropertyState.AnnotationSeen));
			}
			if (propertyData.Processed)
			{
				throw new ODataException((ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName) && !ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName)) ? Strings.DuplicateAnnotationNotAllowed(propertyName) : Strings.DuplicatePropertyNamesNotAllowed(propertyName));
			}
			propertyData.Processed = true;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000157A4 File Offset: 0x000139A4
		internal void CheckIfPropertyOpenForAnnotations(string propertyName, string annotationName)
		{
			PropertyAndAnnotationCollector.PropertyData propertyData;
			if (this.propertyData.TryGetValue(propertyName, ref propertyData) && propertyData.Processed)
			{
				throw new ODataException(Strings.PropertyAnnotationAfterTheProperty(annotationName, propertyName));
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000157D6 File Offset: 0x000139D6
		private static void CheckNestedResourceInfoDuplicateNameForExistingDuplicationRecord(string propertyName, PropertyAndAnnotationCollector.PropertyData propertyData)
		{
			if ((propertyData.State != PropertyAndAnnotationCollector.PropertyState.NavigationProperty || !(propertyData.AssociationLinkUrl != null) || propertyData.NestedResourceInfo != null) && propertyData.State != PropertyAndAnnotationCollector.PropertyState.AnnotationSeen)
			{
				throw new ODataException(Strings.DuplicatePropertyNamesNotAllowed(propertyName));
			}
		}

		// Token: 0x0400030F RID: 783
		private static readonly IDictionary<string, object> emptyDictionary = new Dictionary<string, object>();

		// Token: 0x04000310 RID: 784
		private readonly bool throwOnDuplicateProperty;

		// Token: 0x04000311 RID: 785
		private IDictionary<string, object> odataScopeAnnotations = new Dictionary<string, object>();

		// Token: 0x04000312 RID: 786
		private IList<KeyValuePair<string, object>> customScopeAnnotations = new List<KeyValuePair<string, object>>();

		// Token: 0x04000313 RID: 787
		private IDictionary<string, PropertyAndAnnotationCollector.PropertyData> propertyData = new Dictionary<string, PropertyAndAnnotationCollector.PropertyData>();

		// Token: 0x020002A0 RID: 672
		private enum PropertyState
		{
			// Token: 0x04000BB4 RID: 2996
			AnnotationSeen,
			// Token: 0x04000BB5 RID: 2997
			SimpleProperty,
			// Token: 0x04000BB6 RID: 2998
			NavigationProperty
		}

		// Token: 0x020002A1 RID: 673
		private sealed class PropertyData
		{
			// Token: 0x0600184D RID: 6221 RVA: 0x0004875F File Offset: 0x0004695F
			internal PropertyData(PropertyAndAnnotationCollector.PropertyState propertyState)
			{
				this.State = propertyState;
				this.ODataAnnotations = new Dictionary<string, object>();
				this.CustomAnnotations = new List<KeyValuePair<string, object>>();
			}

			// Token: 0x17000575 RID: 1397
			// (get) Token: 0x0600184E RID: 6222 RVA: 0x00048784 File Offset: 0x00046984
			// (set) Token: 0x0600184F RID: 6223 RVA: 0x0004878C File Offset: 0x0004698C
			internal PropertyAndAnnotationCollector.PropertyState State { get; set; }

			// Token: 0x17000576 RID: 1398
			// (get) Token: 0x06001850 RID: 6224 RVA: 0x00048795 File Offset: 0x00046995
			// (set) Token: 0x06001851 RID: 6225 RVA: 0x0004879D File Offset: 0x0004699D
			internal ODataNestedResourceInfo NestedResourceInfo { get; set; }

			// Token: 0x17000577 RID: 1399
			// (get) Token: 0x06001852 RID: 6226 RVA: 0x000487A6 File Offset: 0x000469A6
			// (set) Token: 0x06001853 RID: 6227 RVA: 0x000487AE File Offset: 0x000469AE
			internal Uri AssociationLinkUrl { get; set; }

			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x06001854 RID: 6228 RVA: 0x000487B7 File Offset: 0x000469B7
			// (set) Token: 0x06001855 RID: 6229 RVA: 0x000487BF File Offset: 0x000469BF
			internal IDictionary<string, object> ODataAnnotations { get; private set; }

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x06001856 RID: 6230 RVA: 0x000487C8 File Offset: 0x000469C8
			// (set) Token: 0x06001857 RID: 6231 RVA: 0x000487D0 File Offset: 0x000469D0
			internal IList<KeyValuePair<string, object>> CustomAnnotations { get; private set; }

			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x06001858 RID: 6232 RVA: 0x000487D9 File Offset: 0x000469D9
			// (set) Token: 0x06001859 RID: 6233 RVA: 0x000487E1 File Offset: 0x000469E1
			internal bool Processed { get; set; }
		}
	}
}
