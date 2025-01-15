using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000045 RID: 69
	internal sealed class OpenConnectionSequence : Sequencer
	{
		// Token: 0x060001AE RID: 430 RVA: 0x000060C5 File Offset: 0x000042C5
		public OpenConnectionSequence(DatabaseMonitoringContext monitoringContext, DatabaseCommand command)
		{
			this.m_monitoringContext = monitoringContext;
			this.m_command = command;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000060DB File Offset: 0x000042DB
		protected override IEnumerable<IFlowStep> Run()
		{
			Exception ex;
			for (;;)
			{
				bool retry = false;
				this.m_monitoringContext.NotifyConnectionBegin();
				ex = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					this.m_command.Open();
					this.m_monitoringContext.NotifyConnectionComplete();
				});
				if (ex != null)
				{
					retry = this.ShouldRetry(ex);
					if (!retry)
					{
						break;
					}
					this.m_monitoringContext.NotifyConnectionRetry(ex);
					int num = Math.Min(this.m_command.Specification.RetryProfile.ConnectionRetryProfile.IntervalInMilliseconds * this.m_retries, 60000) + Randomizer.GetI32(1, 32);
					yield return base.RunAsyncStep<int>("Sleep {0}ms between retries (OpenConnectionSequence)".FormatWithInvariantCulture(new object[] { num }), new Sequencer.AsyncBeginFunction<int>(AsyncTimer.BeginSleep), new Sequencer.AsyncEndFunction(AsyncTimer.EndSleep), num);
				}
				if (!retry)
				{
					goto Block_2;
				}
			}
			throw this.m_monitoringContext.NotifySqlError(ex);
			Block_2:
			yield break;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000060EC File Offset: 0x000042EC
		private bool ShouldRetry(Exception e)
		{
			bool flag = false;
			if (e is InvalidOperationException)
			{
				flag = true;
			}
			else
			{
				SqlException sqlEx = e as SqlException;
				if (sqlEx != null)
				{
					if (sqlEx.Number == 17189 && sqlEx.Class == 16)
					{
						flag = true;
					}
					if (sqlEx.Number == -2 && sqlEx.Class == 11 && !string.IsNullOrEmpty(this.m_command.Specification.ConnectionProperties.FailoverPartner) && this.m_monitoringContext.ElapsedSinceLastNotification < (long)(this.m_command.Connection.ConnectionTimeout * 1000 / 2))
					{
						flag = true;
						SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(this.m_command.Connection.ConnectionString);
						sqlConnectionStringBuilder.ConnectTimeout *= 2;
						this.m_command.Connection.ConnectionString = sqlConnectionStringBuilder.ToString();
					}
					if (OpenConnectionSequence.s_errorsToRetryOn.Length == 0 || OpenConnectionSequence.s_errorsToRetryOn.Any((int n) => n == sqlEx.Number))
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				int retries = this.m_retries;
				this.m_retries = retries + 1;
				return retries < this.m_command.Specification.RetryProfile.ConnectionRetryProfile.RetryCount;
			}
			return false;
		}

		// Token: 0x040000BF RID: 191
		private readonly DatabaseCommand m_command;

		// Token: 0x040000C0 RID: 192
		private readonly DatabaseMonitoringContext m_monitoringContext;

		// Token: 0x040000C1 RID: 193
		private int m_retries;

		// Token: 0x040000C2 RID: 194
		private static readonly int[] s_errorsToRetryOn = new int[0];
	}
}
