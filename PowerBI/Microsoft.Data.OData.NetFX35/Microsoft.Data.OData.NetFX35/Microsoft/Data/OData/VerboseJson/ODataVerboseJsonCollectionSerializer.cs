using System;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C5 RID: 453
	internal sealed class ODataVerboseJsonCollectionSerializer : ODataVerboseJsonPropertyAndValueSerializer
	{
		// Token: 0x06000D51 RID: 3409 RVA: 0x0002F72D File Offset: 0x0002D92D
		internal ODataVerboseJsonCollectionSerializer(ODataVerboseJsonOutputContext verboseJsonOutputContext)
			: base(verboseJsonOutputContext)
		{
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002F736 File Offset: 0x0002D936
		internal void WriteCollectionStart()
		{
			if (base.WritingResponse && base.Version >= ODataVersion.V2)
			{
				base.JsonWriter.StartObjectScope();
				base.JsonWriter.WriteDataArrayName();
			}
			base.JsonWriter.StartArrayScope();
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0002F76A File Offset: 0x0002D96A
		internal void WriteCollectionEnd()
		{
			base.JsonWriter.EndArrayScope();
			if (base.WritingResponse && base.Version >= ODataVersion.V2)
			{
				base.JsonWriter.EndObjectScope();
			}
		}
	}
}
