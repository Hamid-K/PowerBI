using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003F4 RID: 1012
	internal class OperationContextState
	{
		// Token: 0x06002384 RID: 9092 RVA: 0x0006CF97 File Offset: 0x0006B197
		public OperationContextState(object callbackState, params object[] contextState)
		{
			this.m_callbackState = callbackState;
			this.m_contextState = contextState;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x0006CFAD File Offset: 0x0006B1AD
		public object CallbackState
		{
			get
			{
				return this.m_callbackState;
			}
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0006CFB5 File Offset: 0x0006B1B5
		public object[] GetContextState()
		{
			return this.m_contextState;
		}

		// Token: 0x04001615 RID: 5653
		private object m_callbackState;

		// Token: 0x04001616 RID: 5654
		private object[] m_contextState;
	}
}
