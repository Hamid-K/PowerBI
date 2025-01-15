using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000207 RID: 519
	internal sealed class BaseHashTableKeyEnumerator : BaseHashTableEnumerator
	{
		// Token: 0x060010F2 RID: 4338 RVA: 0x00037F61 File Offset: 0x00036161
		internal BaseHashTableKeyEnumerator(BaseHashTable parentHashTable)
			: base(parentHashTable)
		{
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00037FBB File Offset: 0x000361BB
		protected override object GetCurrent(IBaseDataNode baseDataNode)
		{
			return baseDataNode.Key;
		}
	}
}
