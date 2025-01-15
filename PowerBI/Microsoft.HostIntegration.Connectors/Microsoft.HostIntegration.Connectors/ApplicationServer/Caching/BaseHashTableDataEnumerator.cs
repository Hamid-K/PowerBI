using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000205 RID: 517
	internal sealed class BaseHashTableDataEnumerator : BaseHashTableEnumerator
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x00037F61 File Offset: 0x00036161
		internal BaseHashTableDataEnumerator(BaseHashTable parentHashTable)
			: base(parentHashTable)
		{
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00037F6A File Offset: 0x0003616A
		protected override object GetCurrent(IBaseDataNode baseDataNode)
		{
			return baseDataNode.Data;
		}
	}
}
