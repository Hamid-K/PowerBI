using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000233 RID: 563
	internal sealed class ODataVerboseJsonCollectionDeserializer : ODataVerboseJsonPropertyAndValueDeserializer
	{
		// Token: 0x0600110C RID: 4364 RVA: 0x0003FD47 File Offset: 0x0003DF47
		internal ODataVerboseJsonCollectionDeserializer(ODataVerboseJsonInputContext jsonInputContext)
			: base(jsonInputContext)
		{
			this.duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0003FD5C File Offset: 0x0003DF5C
		internal ODataCollectionStart ReadCollectionStart(bool isResultsWrapperExpected)
		{
			if (isResultsWrapperExpected)
			{
				base.JsonReader.ReadStartObject();
				bool flag = false;
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = base.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal("results", text) == 0)
					{
						flag = true;
						break;
					}
					base.JsonReader.SkipValue();
				}
				if (!flag)
				{
					throw new ODataException(Strings.ODataJsonCollectionDeserializer_MissingResultsPropertyForCollection);
				}
			}
			if (base.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonCollectionDeserializer_CannotReadCollectionContentStart(base.JsonReader.NodeType));
			}
			return new ODataCollectionStart
			{
				Name = null
			};
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0003FDF8 File Offset: 0x0003DFF8
		internal object ReadCollectionItem(IEdmTypeReference expectedItemTypeReference, CollectionWithoutExpectedTypeValidator collectionValidator)
		{
			return base.ReadNonEntityValue(expectedItemTypeReference, this.duplicatePropertyNamesChecker, collectionValidator, true, null);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0003FE18 File Offset: 0x0003E018
		internal void ReadCollectionEnd(bool isResultsWrapperExpected)
		{
			base.JsonReader.ReadEndArray();
			if (!isResultsWrapperExpected)
			{
				return;
			}
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("results", text) == 0)
				{
					throw new ODataException(Strings.ODataJsonCollectionDeserializer_MultipleResultsPropertiesForCollection);
				}
				base.JsonReader.SkipValue();
			}
			base.JsonReader.ReadEndObject();
		}

		// Token: 0x0400068E RID: 1678
		private readonly DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
	}
}
