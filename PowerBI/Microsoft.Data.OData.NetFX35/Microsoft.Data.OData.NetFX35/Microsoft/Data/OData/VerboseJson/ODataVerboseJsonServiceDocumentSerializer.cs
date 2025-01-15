using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C9 RID: 457
	internal sealed class ODataVerboseJsonServiceDocumentSerializer : ODataVerboseJsonSerializer
	{
		// Token: 0x06000D76 RID: 3446 RVA: 0x00030057 File Offset: 0x0002E257
		internal ODataVerboseJsonServiceDocumentSerializer(ODataVerboseJsonOutputContext verboseJsonOutputContext)
			: base(verboseJsonOutputContext)
		{
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0003012C File Offset: 0x0002E32C
		internal void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			IEnumerable<ODataResourceCollectionInfo> collections = defaultWorkspace.Collections;
			base.WriteTopLevelPayload(delegate
			{
				this.JsonWriter.StartObjectScope();
				this.JsonWriter.WriteName("EntitySets");
				this.JsonWriter.StartArrayScope();
				if (collections != null)
				{
					foreach (ODataResourceCollectionInfo odataResourceCollectionInfo in collections)
					{
						ValidationUtils.ValidateResourceCollectionInfo(odataResourceCollectionInfo);
						this.JsonWriter.WriteValue(UriUtilsCommon.UriToString(odataResourceCollectionInfo.Url));
					}
				}
				this.JsonWriter.EndArrayScope();
				this.JsonWriter.EndObjectScope();
			});
		}
	}
}
