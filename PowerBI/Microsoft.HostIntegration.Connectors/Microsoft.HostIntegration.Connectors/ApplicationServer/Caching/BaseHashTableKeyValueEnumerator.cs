using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000208 RID: 520
	internal sealed class BaseHashTableKeyValueEnumerator : BaseHashTableEnumerator
	{
		// Token: 0x060010F4 RID: 4340 RVA: 0x00037F61 File Offset: 0x00036161
		internal BaseHashTableKeyValueEnumerator(BaseHashTable parentHashTable)
			: base(parentHashTable)
		{
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00028FA6 File Offset: 0x000271A6
		protected override object GetCurrent(IBaseDataNode baseDataNode)
		{
			return baseDataNode;
		}
	}
}
