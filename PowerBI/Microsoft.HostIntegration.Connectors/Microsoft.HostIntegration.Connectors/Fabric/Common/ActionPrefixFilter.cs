using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003C9 RID: 969
	internal class ActionPrefixFilter : MessageFilter
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x00069378 File Offset: 0x00067578
		public ActionPrefixFilter(params string[] actionPrefixes)
		{
			this.m_actionPrefixes = actionPrefixes;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00069387 File Offset: 0x00067587
		public string[] GetActionPrefix()
		{
			return this.m_actionPrefixes;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0006938F File Offset: 0x0006758F
		public override bool Match(MessageBuffer buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.Match(buffer.CreateMessage());
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000693AC File Offset: 0x000675AC
		public override bool Match(Message message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			string action = message.Headers.Action;
			for (int i = 0; i < this.m_actionPrefixes.Length; i++)
			{
				if (action.StartsWith(this.m_actionPrefixes[i], StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400159C RID: 5532
		private string[] m_actionPrefixes;
	}
}
