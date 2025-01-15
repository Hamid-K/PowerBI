using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001F6 RID: 502
	internal abstract class EpmReader
	{
		// Token: 0x06000E85 RID: 3717 RVA: 0x00034430 File Offset: 0x00032630
		protected EpmReader(IODataAtomReaderEntryState entryState, ODataAtomInputContext inputContext)
		{
			this.entryState = entryState;
			this.atomInputContext = inputContext;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00034446 File Offset: 0x00032646
		protected IODataAtomReaderEntryState EntryState
		{
			get
			{
				return this.entryState;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0003444E File Offset: 0x0003264E
		protected ODataVersion Version
		{
			get
			{
				return this.atomInputContext.Version;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0003445B File Offset: 0x0003265B
		protected ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.atomInputContext.MessageReaderSettings;
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00034468 File Offset: 0x00032668
		protected void SetEntryEpmValue(EntityPropertyMappingInfo epmInfo, object propertyValue)
		{
			this.SetEpmValue(this.entryState.Entry.Properties.ToReadOnlyEnumerable("Properties"), this.entryState.EntityType.ToTypeReference(), epmInfo, propertyValue);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003449C File Offset: 0x0003269C
		protected void SetEpmValue(ReadOnlyEnumerable<ODataProperty> targetList, IEdmTypeReference targetTypeReference, EntityPropertyMappingInfo epmInfo, object propertyValue)
		{
			this.SetEpmValueForSegment(epmInfo, 0, targetTypeReference.AsStructuredOrNull(), targetList, propertyValue);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x000344D0 File Offset: 0x000326D0
		private void SetEpmValueForSegment(EntityPropertyMappingInfo epmInfo, int propertyValuePathIndex, IEdmStructuredTypeReference segmentStructuralTypeReference, ReadOnlyEnumerable<ODataProperty> existingProperties, object propertyValue)
		{
			string propertyName = epmInfo.PropertyValuePath[propertyValuePathIndex].PropertyName;
			if (epmInfo.Attribute.KeepInContent)
			{
				return;
			}
			ODataProperty odataProperty = Enumerable.FirstOrDefault<ODataProperty>(existingProperties, (ODataProperty p) => string.CompareOrdinal(p.Name, propertyName) == 0);
			ODataComplexValue odataComplexValue = null;
			if (odataProperty != null)
			{
				odataComplexValue = odataProperty.Value as ODataComplexValue;
				if (odataComplexValue == null)
				{
					return;
				}
			}
			IEdmProperty edmProperty = segmentStructuralTypeReference.FindProperty(propertyName);
			if (edmProperty == null && propertyValuePathIndex != epmInfo.PropertyValuePath.Length - 1)
			{
				throw new ODataException(Strings.EpmReader_OpenComplexOrCollectionEpmProperty(epmInfo.Attribute.SourcePath));
			}
			IEdmTypeReference edmTypeReference;
			if (edmProperty == null || (this.MessageReaderSettings.DisablePrimitiveTypeConversion && edmProperty.Type.IsODataPrimitiveTypeKind()))
			{
				edmTypeReference = EdmCoreModel.Instance.GetString(true);
			}
			else
			{
				edmTypeReference = edmProperty.Type;
			}
			switch (edmTypeReference.TypeKind())
			{
			case EdmTypeKind.Primitive:
			{
				if (edmTypeReference.IsStream())
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmReader_SetEpmValueForSegment_StreamProperty));
				}
				object obj;
				if (propertyValue == null)
				{
					ReaderValidationUtils.ValidateNullValue(this.atomInputContext.Model, edmTypeReference, this.atomInputContext.MessageReaderSettings, true, this.atomInputContext.Version, propertyName);
					obj = null;
				}
				else
				{
					obj = AtomValueUtils.ConvertStringToPrimitive((string)propertyValue, edmTypeReference.AsPrimitive());
				}
				this.AddEpmPropertyValue(existingProperties, propertyName, obj, segmentStructuralTypeReference.IsODataEntityTypeKind());
				return;
			}
			case EdmTypeKind.Complex:
			{
				if (odataComplexValue == null)
				{
					odataComplexValue = new ODataComplexValue
					{
						TypeName = edmTypeReference.ODataFullName(),
						Properties = new ReadOnlyEnumerable<ODataProperty>()
					};
					this.AddEpmPropertyValue(existingProperties, propertyName, odataComplexValue, segmentStructuralTypeReference.IsODataEntityTypeKind());
				}
				IEdmComplexTypeReference edmComplexTypeReference = edmTypeReference.AsComplex();
				this.SetEpmValueForSegment(epmInfo, propertyValuePathIndex + 1, edmComplexTypeReference, odataComplexValue.Properties.ToReadOnlyEnumerable("Properties"), propertyValue);
				return;
			}
			case EdmTypeKind.Collection:
			{
				ODataCollectionValue odataCollectionValue = new ODataCollectionValue
				{
					TypeName = edmTypeReference.ODataFullName(),
					Items = new ReadOnlyEnumerable((List<object>)propertyValue)
				};
				this.AddEpmPropertyValue(existingProperties, propertyName, odataCollectionValue, segmentStructuralTypeReference.IsODataEntityTypeKind());
				return;
			}
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmReader_SetEpmValueForSegment_TypeKind));
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000346F8 File Offset: 0x000328F8
		private void AddEpmPropertyValue(ReadOnlyEnumerable<ODataProperty> properties, string propertyName, object propertyValue, bool checkDuplicateEntryPropertyNames)
		{
			ODataProperty odataProperty = new ODataProperty
			{
				Name = propertyName,
				Value = propertyValue
			};
			if (checkDuplicateEntryPropertyNames)
			{
				this.entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
			}
			properties.AddToSourceList(odataProperty);
		}

		// Token: 0x04000570 RID: 1392
		private readonly ODataAtomInputContext atomInputContext;

		// Token: 0x04000571 RID: 1393
		private readonly IODataAtomReaderEntryState entryState;
	}
}
