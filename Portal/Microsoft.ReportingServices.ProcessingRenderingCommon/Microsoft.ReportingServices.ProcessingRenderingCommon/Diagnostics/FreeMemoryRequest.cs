using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	internal sealed class FreeMemoryRequest
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x0000F4AF File Offset: 0x0000D6AF
		public FreeMemoryRequest(FreeMemoryRequestContext requestContext)
		{
			this.m_requestContext = requestContext;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		public void Initiate()
		{
			long num = 0L;
			long kbytesToFree = this.m_requestContext.KBytesToFree;
			bool notifyAllConsumers = this.m_requestContext.NotifyAllConsumers;
			long num2 = 0L;
			if (ProcessingContext.Configuration == null)
			{
				return;
			}
			bool flag = MemoryAuditCollection.InitiateMemoryShrink(kbytesToFree, notifyAllConsumers, MemoryGroupType.Default, out num2, out num);
			this.m_requestContext.SetShrinkResults(flag, num2, num);
		}

		// Token: 0x040002DA RID: 730
		private readonly FreeMemoryRequestContext m_requestContext;
	}
}
