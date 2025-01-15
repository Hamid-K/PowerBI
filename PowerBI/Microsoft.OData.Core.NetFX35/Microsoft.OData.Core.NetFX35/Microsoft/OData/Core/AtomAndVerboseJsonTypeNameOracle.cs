using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x0200000C RID: 12
	internal sealed class AtomAndVerboseJsonTypeNameOracle : TypeNameOracle
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000028A8 File Offset: 0x00000AA8
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This method will eventually become an override of a method in the base class, but more refactoring work needs to happen first.")]
		internal string GetEntryTypeNameForWriting(ODataEntry entry)
		{
			SerializationTypeNameAnnotation annotation = entry.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null)
			{
				return annotation.TypeName;
			}
			return entry.TypeName;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028CC File Offset: 0x00000ACC
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This method will eventually become an override of a method in the base class, but more refactoring work needs to happen first.")]
		internal string GetValueTypeNameForWriting(object value, IEdmTypeReference typeReferenceFromValue, SerializationTypeNameAnnotation typeNameAnnotation, CollectionWithoutExpectedTypeValidator collectionValidator, out string collectionItemTypeName)
		{
			collectionItemTypeName = null;
			string text = TypeNameOracle.GetTypeNameFromValue(value);
			if (text == null && typeReferenceFromValue != null)
			{
				text = typeReferenceFromValue.FullName();
			}
			if (text != null)
			{
				if (collectionValidator != null && string.CompareOrdinal(collectionValidator.ItemTypeNameFromCollection, text) == 0)
				{
					text = null;
				}
				if (text != null && value is ODataCollectionValue)
				{
					collectionItemTypeName = ValidationUtils.ValidateCollectionTypeName(text);
				}
			}
			if (typeNameAnnotation != null)
			{
				text = typeNameAnnotation.TypeName;
			}
			return text;
		}
	}
}
