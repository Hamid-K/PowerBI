using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200080A RID: 2058
	public class GlobalExceptionHandler : IExceptionHandler
	{
		// Token: 0x060040FE RID: 16638 RVA: 0x000DCB98 File Offset: 0x000DAD98
		public virtual bool HandleException(Exception ex, DdmReader reader, DdmWriter writer)
		{
			if (ex is DrdaException)
			{
				DrdaException ex2 = (DrdaException)ex;
				if (Logger.maxTracingLevel >= 1)
				{
					Logger.Error(0, string.Format("GlobalExceptionHandler::HandleException DrdaException errorcode={0}; errorCP={1}; contextCP={2} ", ex2.ErrorCode, ex2.ErrorCodePoint, ex2.ContextCodePoint), Array.Empty<object>());
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Verbose(0, "DDMReader position = " + reader.Position.ToString(), Array.Empty<object>());
				}
				try
				{
					byte[] bytes = reader.GetBytes(reader.Position - 2, 10);
					if (Logger.maxTracingLevel >= 4)
					{
						Logger.Verbose(0, "DDMReader buffer from {0} to {1}: {2}", new object[]
						{
							reader.Position - 2,
							reader.Position + 10,
							BitUtils.ConvertToHexString(bytes)
						});
					}
				}
				catch (Exception)
				{
				}
			}
			if (Logger.maxTracingLevel >= 1)
			{
				Logger.Error(0, string.Format("GlobalExceptionHandler::HandleException Message: {0}", ex.Message), Array.Empty<object>());
			}
			if (Logger.maxTracingLevel >= 1)
			{
				Logger.Error(0, string.Format("Stacktrace:{0}", ex.StackTrace), Array.Empty<object>());
			}
			return true;
		}
	}
}
