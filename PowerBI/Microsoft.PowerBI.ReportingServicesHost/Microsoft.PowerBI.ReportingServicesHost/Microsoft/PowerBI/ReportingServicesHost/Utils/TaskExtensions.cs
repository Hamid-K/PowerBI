using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.ReportingServicesHost.Utils
{
	// Token: 0x02000067 RID: 103
	internal static class TaskExtensions
	{
		// Token: 0x06000243 RID: 579 RVA: 0x00006500 File Offset: 0x00004700
		internal static void WaitAndUnwrapException(this Task task)
		{
			try
			{
				task.Wait();
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException != null)
				{
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				}
				throw;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006540 File Offset: 0x00004740
		internal static T WaitAndUnwrapException<T>(this Task<T> task)
		{
			T result;
			try
			{
				task.Wait();
				result = task.Result;
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException != null)
				{
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				}
				throw;
			}
			return result;
		}
	}
}
