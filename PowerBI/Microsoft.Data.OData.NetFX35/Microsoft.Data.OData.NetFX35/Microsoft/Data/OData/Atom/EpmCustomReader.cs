using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001F7 RID: 503
	internal sealed class EpmCustomReader : EpmReader
	{
		// Token: 0x06000E8D RID: 3725 RVA: 0x00034737 File Offset: 0x00032937
		private EpmCustomReader(IODataAtomReaderEntryState entryState, ODataAtomInputContext inputContext)
			: base(entryState, inputContext)
		{
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00034744 File Offset: 0x00032944
		internal static void ReadEntryEpm(IODataAtomReaderEntryState entryState, ODataAtomInputContext inputContext)
		{
			EpmCustomReader epmCustomReader = new EpmCustomReader(entryState, inputContext);
			epmCustomReader.ReadEntryEpm();
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00034760 File Offset: 0x00032960
		private void ReadEntryEpm()
		{
			EpmCustomReaderValueCache epmCustomReaderValueCache = base.EntryState.EpmCustomReaderValueCache;
			foreach (KeyValuePair<EntityPropertyMappingInfo, string> keyValuePair in epmCustomReaderValueCache.CustomEpmValues)
			{
				base.SetEntryEpmValue(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}
