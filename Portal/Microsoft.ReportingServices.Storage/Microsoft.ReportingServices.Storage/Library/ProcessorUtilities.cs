using System;
using Microsoft.ReportingServices.HostingInterfaces;
using Microsoft.ReportingServices.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200003C RID: 60
	internal static class ProcessorUtilities
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00009CA8 File Offset: 0x00007EA8
		internal static int ProcessorsInUse
		{
			get
			{
				if (ProcessorUtilities.m_numberOfProcessors == 0)
				{
					IRsUnmanagedCallback rsUnmanagedCallback = null;
					IServiceProvider serviceProvider = AppDomain.CurrentDomain.DomainManager as IServiceProvider;
					if (serviceProvider != null)
					{
						rsUnmanagedCallback = (IRsUnmanagedCallback)serviceProvider.GetService(typeof(IRsUnmanagedCallback));
					}
					if (rsUnmanagedCallback == null)
					{
						ProcessorUtilities.m_numberOfProcessors = Environment.ProcessorCount;
					}
					else
					{
						ProcessorUtilities.m_numberOfProcessors = (int)Math.Min(rsUnmanagedCallback.GetConcurrencyLimit(), 2147483647L);
					}
				}
				return ProcessorUtilities.m_numberOfProcessors;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00009D14 File Offset: 0x00007F14
		internal static double ProcessCPUPercentage
		{
			get
			{
				double num = 0.0;
				TimeSpan timeSpan = new TimeSpan(ProcessUtilities.GetProcessTimes());
				DateTime now = DateTime.Now;
				if (ProcessorUtilities.m_lastReadTime != TimeSpan.Zero)
				{
					TimeSpan timeSpan2 = now - ProcessorUtilities.m_lastReadDate;
					num = (double)(timeSpan.Ticks - ProcessorUtilities.m_lastReadTime.Ticks) / (double)timeSpan2.Ticks;
					num /= (double)ProcessorUtilities.ProcessorsInUse;
				}
				ProcessorUtilities.m_lastReadDate = now;
				ProcessorUtilities.m_lastReadTime = timeSpan;
				return num;
			}
		}

		// Token: 0x04000183 RID: 387
		private static int m_numberOfProcessors = 0;

		// Token: 0x04000184 RID: 388
		private static DateTime m_lastReadDate = DateTime.MinValue;

		// Token: 0x04000185 RID: 389
		private static TimeSpan m_lastReadTime = TimeSpan.Zero;
	}
}
