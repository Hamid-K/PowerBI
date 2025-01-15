using System;
using NLog.Internal;

namespace NLog
{
	// Token: 0x0200001E RID: 30
	public sealed class NullLogger : Logger
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x00009088 File Offset: 0x00007288
		public NullLogger(LogFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			TargetWithFilterChain[] array = new TargetWithFilterChain[LogLevel.MaxLevel.Ordinal + 1];
			base.Initialize(string.Empty, new LoggerConfiguration(array, false), factory);
		}
	}
}
