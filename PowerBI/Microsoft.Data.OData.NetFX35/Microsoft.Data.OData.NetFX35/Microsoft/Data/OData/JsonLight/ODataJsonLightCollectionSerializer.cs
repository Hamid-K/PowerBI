using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000184 RID: 388
	internal sealed class ODataJsonLightCollectionSerializer : ODataJsonLightValueSerializer
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x00023C9B File Offset: 0x00021E9B
		internal ODataJsonLightCollectionSerializer(ODataJsonLightOutputContext jsonLightOutputContext, bool writingTopLevelCollection)
			: base(jsonLightOutputContext)
		{
			this.writingTopLevelCollection = writingTopLevelCollection;
			this.metadataUriBuilder = jsonLightOutputContext.CreateMetadataUriBuilder();
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00023CB8 File Offset: 0x00021EB8
		internal void WriteCollectionStart(ODataCollectionStart collectionStart, IEdmTypeReference itemTypeReference)
		{
			if (this.writingTopLevelCollection)
			{
				base.JsonWriter.StartObjectScope();
				Uri uri;
				if (this.metadataUriBuilder.TryBuildCollectionMetadataUri(collectionStart.SerializationInfo, itemTypeReference, out uri))
				{
					base.WriteMetadataUriProperty(uri);
				}
				base.JsonWriter.WriteValuePropertyName();
			}
			base.JsonWriter.StartArrayScope();
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00023D0B File Offset: 0x00021F0B
		internal void WriteCollectionEnd()
		{
			base.JsonWriter.EndArrayScope();
			if (this.writingTopLevelCollection)
			{
				base.JsonWriter.EndObjectScope();
			}
		}

		// Token: 0x04000404 RID: 1028
		private readonly bool writingTopLevelCollection;

		// Token: 0x04000405 RID: 1029
		private readonly ODataJsonLightMetadataUriBuilder metadataUriBuilder;
	}
}
