using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Wrangling.Logging
{
	// Token: 0x0200017B RID: 379
	public class MultiLogger : ILogger
	{
		// Token: 0x06000856 RID: 2134 RVA: 0x00019844 File Offset: 0x00017A44
		public MultiLogger(params ILogger[] loggers)
			: this(loggers)
		{
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001984D File Offset: 0x00017A4D
		public MultiLogger(IEnumerable<ILogger> loggers)
		{
			this._loggers = loggers.ToArray<ILogger>();
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00019864 File Offset: 0x00017A64
		public void Error(string area, string message, string userData = null)
		{
			this._loggers.ForEach(delegate(ILogger l)
			{
				l.Error(area, message, userData);
			});
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000198A4 File Offset: 0x00017AA4
		public void Warn(string area, string message, string userData = null)
		{
			this._loggers.ForEach(delegate(ILogger l)
			{
				l.Warn(area, message, userData);
			});
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000198E4 File Offset: 0x00017AE4
		public void Info(string area, string message, string userData = null)
		{
			this._loggers.ForEach(delegate(ILogger l)
			{
				l.Info(area, message, userData);
			});
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00019924 File Offset: 0x00017B24
		public void Debug(string area, string message, string userData = null)
		{
			this._loggers.ForEach(delegate(ILogger l)
			{
				l.Debug(area, message, userData);
			});
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00019964 File Offset: 0x00017B64
		public void TrackException(Exception exception)
		{
			this._loggers.ForEach(delegate(ILogger l)
			{
				l.TrackException(exception);
			});
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00019998 File Offset: 0x00017B98
		public void TrackEvent(string eventName, IReadOnlyCollection<KeyValuePair<string, double>> metrics = null, IReadOnlyCollection<KeyValuePair<string, string>> properties = null, IReadOnlyCollection<KeyValuePair<string, string>> userDataProperties = null)
		{
			this._loggers.ForEach(delegate(ILogger l)
			{
				l.TrackEvent(eventName, metrics, properties, userDataProperties);
			});
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000199E0 File Offset: 0x00017BE0
		public void Flush(bool wait)
		{
			this._loggers.ForEach(delegate(ILogger l)
			{
				l.Flush(wait);
			});
		}

		// Token: 0x0400041B RID: 1051
		private ILogger[] _loggers;
	}
}
