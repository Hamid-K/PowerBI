using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000201 RID: 513
	internal sealed class ODataJsonLightCollectionSerializer : ODataJsonLightValueSerializer
	{
		// Token: 0x060013D8 RID: 5080 RVA: 0x00038CFF File Offset: 0x00036EFF
		internal ODataJsonLightCollectionSerializer(ODataJsonLightOutputContext jsonLightOutputContext, bool writingTopLevelCollection)
			: base(jsonLightOutputContext, true)
		{
			this.writingTopLevelCollection = writingTopLevelCollection;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00038D10 File Offset: 0x00036F10
		internal void WriteCollectionStart(ODataCollectionStart collectionStart, IEdmTypeReference itemTypeReference)
		{
			if (this.writingTopLevelCollection)
			{
				base.JsonWriter.StartObjectScope();
				base.WriteContextUriProperty(ODataPayloadKind.Collection, () => ODataContextUrlInfo.Create(collectionStart.SerializationInfo, itemTypeReference), null, null);
				if (collectionStart.Count != null)
				{
					base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.count");
					base.JsonWriter.WriteValue(collectionStart.Count.Value);
				}
				if (collectionStart.NextPageLink != null)
				{
					base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.nextLink");
					base.JsonWriter.WriteValue(base.UriToString(collectionStart.NextPageLink));
				}
				base.JsonWriter.WriteValuePropertyName();
			}
			base.JsonWriter.StartArrayScope();
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00038DF5 File Offset: 0x00036FF5
		internal void WriteCollectionEnd()
		{
			base.JsonWriter.EndArrayScope();
			if (this.writingTopLevelCollection)
			{
				base.JsonWriter.EndObjectScope();
			}
		}

		// Token: 0x040009FE RID: 2558
		private readonly bool writingTopLevelCollection;
	}
}
