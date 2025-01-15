using System;
using System.Collections.Generic;
using System.Linq;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000217 RID: 535
	internal abstract class EpmWriter
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x00039F43 File Offset: 0x00038143
		protected EpmWriter(ODataAtomOutputContext atomOutputContext)
		{
			this.atomOutputContext = atomOutputContext;
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00039F52 File Offset: 0x00038152
		protected ODataVersion Version
		{
			get
			{
				return this.atomOutputContext.Version;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00039F5F File Offset: 0x0003815F
		protected ODataWriterBehavior WriterBehavior
		{
			get
			{
				return this.atomOutputContext.MessageWriterSettings.WriterBehavior;
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00039F71 File Offset: 0x00038171
		protected object ReadEntryPropertyValue(EntityPropertyMappingInfo epmInfo, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType)
		{
			return this.ReadPropertyValue(epmInfo, epmValueCache.EntryProperties, 0, entityType, epmValueCache);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00039F83 File Offset: 0x00038183
		private object ReadComplexPropertyValue(EntityPropertyMappingInfo epmInfo, ODataComplexValue complexValue, EpmValueCache epmValueCache, int sourceSegmentIndex, IEdmComplexTypeReference complexType)
		{
			return this.ReadPropertyValue(epmInfo, EpmValueCache.GetComplexValueProperties(epmValueCache, complexValue, false), sourceSegmentIndex, complexType, epmValueCache);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00039FB4 File Offset: 0x000381B4
		private object ReadPropertyValue(EntityPropertyMappingInfo epmInfo, IEnumerable<ODataProperty> cachedProperties, int sourceSegmentIndex, IEdmStructuredTypeReference structuredTypeReference, EpmValueCache epmValueCache)
		{
			EpmSourcePathSegment epmSourcePathSegment = epmInfo.PropertyValuePath[sourceSegmentIndex];
			string propertyName = epmSourcePathSegment.PropertyName;
			bool flag = epmInfo.PropertyValuePath.Length == sourceSegmentIndex + 1;
			IEdmStructuredType edmStructuredType = structuredTypeReference.StructuredDefinition();
			IEdmProperty edmProperty = WriterValidationUtils.ValidatePropertyDefined(propertyName, edmStructuredType, this.atomOutputContext.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
			IEdmTypeReference edmTypeReference = null;
			if (edmProperty != null)
			{
				edmTypeReference = edmProperty.Type;
				if (flag)
				{
					if (!edmTypeReference.IsODataPrimitiveTypeKind() && !edmTypeReference.IsNonEntityCollectionType())
					{
						throw new ODataException(Strings.EpmSourceTree_EndsWithNonPrimitiveType(propertyName));
					}
				}
				else if (edmTypeReference.TypeKind() != EdmTypeKind.Complex)
				{
					throw new ODataException(Strings.EpmSourceTree_TraversalOfNonComplexType(propertyName));
				}
			}
			ODataProperty odataProperty = ((cachedProperties == null) ? null : Enumerable.FirstOrDefault<ODataProperty>(cachedProperties, (ODataProperty p) => p.Name == propertyName));
			if (odataProperty == null)
			{
				throw new ODataException(Strings.EpmSourceTree_MissingPropertyOnInstance(propertyName, structuredTypeReference.ODataFullName()));
			}
			object value = odataProperty.Value;
			ODataComplexValue odataComplexValue = value as ODataComplexValue;
			if (flag)
			{
				if (value == null)
				{
					WriterValidationUtils.ValidateNullPropertyValue(edmTypeReference, propertyName, this.WriterBehavior, this.atomOutputContext.Model);
				}
				else
				{
					if (odataComplexValue != null)
					{
						throw new ODataException(Strings.EpmSourceTree_EndsWithNonPrimitiveType(propertyName));
					}
					ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
					if (odataCollectionValue != null)
					{
						TypeNameOracle.ResolveAndValidateTypeNameForValue(this.atomOutputContext.Model, edmTypeReference, odataCollectionValue, edmProperty == null);
					}
					else
					{
						if (value is ODataStreamReferenceValue)
						{
							throw new ODataException(Strings.ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry(propertyName));
						}
						if (value is ISpatial)
						{
							throw new ODataException(Strings.EpmSourceTree_OpenPropertySpatialTypeCannotBeMapped(propertyName, epmInfo.DefiningType.FullName()));
						}
						if (edmTypeReference != null)
						{
							ValidationUtils.ValidateIsExpectedPrimitiveType(value, edmTypeReference);
						}
					}
				}
				return value;
			}
			if (odataComplexValue != null)
			{
				IEdmComplexTypeReference edmComplexTypeReference = TypeNameOracle.ResolveAndValidateTypeNameForValue(this.atomOutputContext.Model, (edmProperty == null) ? null : edmProperty.Type, odataComplexValue, edmProperty == null).AsComplexOrNull();
				return this.ReadComplexPropertyValue(epmInfo, odataComplexValue, epmValueCache, sourceSegmentIndex + 1, edmComplexTypeReference);
			}
			if (value != null)
			{
				throw new ODataException(Strings.EpmSourceTree_TraversalOfNonComplexType(propertyName));
			}
			return null;
		}

		// Token: 0x04000606 RID: 1542
		private readonly ODataAtomOutputContext atomOutputContext;
	}
}
