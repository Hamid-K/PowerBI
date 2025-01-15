using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000BF RID: 191
	internal sealed class ODataJsonLightCollectionSerializer : ODataJsonLightValueSerializer
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x00018AF0 File Offset: 0x00016CF0
		internal ODataJsonLightCollectionSerializer(ODataJsonLightOutputContext jsonLightOutputContext, bool writingTopLevelCollection)
			: base(jsonLightOutputContext, true)
		{
			this.writingTopLevelCollection = writingTopLevelCollection;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00018B24 File Offset: 0x00016D24
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

		// Token: 0x060006E4 RID: 1764 RVA: 0x00018C10 File Offset: 0x00016E10
		internal void WriteCollectionEnd()
		{
			base.JsonWriter.EndArrayScope();
			if (this.writingTopLevelCollection)
			{
				base.JsonWriter.EndObjectScope();
			}
		}

		// Token: 0x0400032C RID: 812
		private readonly bool writingTopLevelCollection;
	}
}
