using System;
using System.Threading.Tasks;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x02000068 RID: 104
	internal static class QueryExecutionUtils
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00007168 File Offset: 0x00005368
		internal static void ExecuteInTryCatch(ITracer tracer, Action action, bool shouldNotThrowNonStopping, string errorMessage)
		{
			try
			{
				action();
			}
			catch (Exception ex) when (!ErrorUtils.IsStoppingException(ex))
			{
				if (!shouldNotThrowNonStopping)
				{
					throw;
				}
				tracer.TraceSanitizedError(ex, errorMessage);
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000071B8 File Offset: 0x000053B8
		internal static async Task ExecuteInTryCatch(ITracer tracer, Func<Task> action, bool shouldNotThrowNonStopping, string errorMessage)
		{
			try
			{
				await action();
			}
			catch (Exception ex) when (!ErrorUtils.IsStoppingException(ex))
			{
				if (!shouldNotThrowNonStopping)
				{
					throw;
				}
				tracer.TraceSanitizedError(ex, errorMessage);
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00007214 File Offset: 0x00005414
		internal static void HandleKnownProviderErrors(DataExtensionException extensionException, DataSet dataSet, ITracer tracer)
		{
			bool flag = false;
			DataExtensionEngineException ex;
			try
			{
				flag = QueryExtensionErrorExtractor.TryExtract(extensionException, dataSet.QuerySourceMap, out ex);
			}
			catch (Exception ex2) when (!ErrorUtils.IsStoppingException(ex2))
			{
				tracer.TraceSanitizedError(extensionException, "Unexpected exception while checking for exceptions due to query extensions in DataShaping Processing.");
				throw;
			}
			if (flag)
			{
				throw ex;
			}
		}
	}
}
