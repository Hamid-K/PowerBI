using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000D3 RID: 211
	internal class CompositeExceptionLogger : IExceptionLogger
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x0000E106 File Offset: 0x0000C306
		public CompositeExceptionLogger(params IExceptionLogger[] loggers)
			: this(loggers)
		{
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000E10F File Offset: 0x0000C30F
		public CompositeExceptionLogger(IEnumerable<IExceptionLogger> loggers)
		{
			if (loggers == null)
			{
				throw new ArgumentNullException("loggers");
			}
			this._loggers = loggers.ToArray<IExceptionLogger>();
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000E131 File Offset: 0x0000C331
		public IEnumerable<IExceptionLogger> Loggers
		{
			get
			{
				return this._loggers;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000E13C File Offset: 0x0000C33C
		public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
		{
			List<Task> list = new List<Task>();
			foreach (IExceptionLogger exceptionLogger in this._loggers)
			{
				if (exceptionLogger == null)
				{
					throw new InvalidOperationException(Error.Format(SRResources.TypeInstanceMustNotBeNull, new object[] { typeof(IExceptionLogger).Name }));
				}
				Task task = exceptionLogger.LogAsync(context, cancellationToken);
				list.Add(task);
			}
			return Task.WhenAll(list);
		}

		// Token: 0x0400013D RID: 317
		private readonly IExceptionLogger[] _loggers;
	}
}
