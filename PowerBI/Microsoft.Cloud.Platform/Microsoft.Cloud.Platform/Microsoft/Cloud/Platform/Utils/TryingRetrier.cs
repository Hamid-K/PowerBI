using System;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200027C RID: 636
	public class TryingRetrier : RetrierBase
	{
		// Token: 0x060010FE RID: 4350 RVA: 0x0003A8B3 File Offset: 0x00038AB3
		public TryingRetrier(ITryingRetriableCommand command, TimeSpan intervalToWaitBetweenRetries, int maxRetries)
			: base(command.GetType().Name, (int)intervalToWaitBetweenRetries.TotalMilliseconds, maxRetries, false)
		{
			this.m_command = command;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0003A8D7 File Offset: 0x00038AD7
		protected override IAsyncResult BeginExecuteCommand(RetrierContext retrierContext, AsyncCallback callback, object userState)
		{
			return this.m_command.BeginExecute(retrierContext, callback, userState);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0003A8E7 File Offset: 0x00038AE7
		protected override void EndExecuteCommand(IAsyncResult ar)
		{
			this.m_command.EndExecute(ar);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0003A8F5 File Offset: 0x00038AF5
		protected override Task<bool> IsRetryRequired(Exception commandException)
		{
			return Task.FromResult<bool>(this.m_command.IsRetryRequired());
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0003A907 File Offset: 0x00038B07
		protected override void OnRetryDepleted(Exception commandException)
		{
			throw new RetrierInvocationException("Maximum number of retries reached for command {0}.".FormatWithInvariantCulture(new object[] { this.m_command }));
		}

		// Token: 0x0400063E RID: 1598
		private readonly ITryingRetriableCommand m_command;
	}
}
