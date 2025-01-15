using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200023A RID: 570
	internal sealed class ODataJsonLightCollectionSerializer : ODataJsonLightValueSerializer
	{
		// Token: 0x060018AA RID: 6314 RVA: 0x00046B87 File Offset: 0x00044D87
		internal ODataJsonLightCollectionSerializer(ODataJsonLightOutputContext jsonLightOutputContext, bool writingTopLevelCollection)
			: base(jsonLightOutputContext, true)
		{
			this.writingTopLevelCollection = writingTopLevelCollection;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00046B98 File Offset: 0x00044D98
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

		// Token: 0x060018AC RID: 6316 RVA: 0x00046C7D File Offset: 0x00044E7D
		internal void WriteCollectionEnd()
		{
			base.JsonWriter.EndArrayScope();
			if (this.writingTopLevelCollection)
			{
				base.JsonWriter.EndObjectScope();
			}
		}

		// Token: 0x04000B1C RID: 2844
		private readonly bool writingTopLevelCollection;
	}
}
