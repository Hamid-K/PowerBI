using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.Logging
{
	// Token: 0x02000175 RID: 373
	public interface ILogger
	{
		// Token: 0x0600084D RID: 2125
		void Error(string area, string message, string userData = null);

		// Token: 0x0600084E RID: 2126
		void Warn(string area, string message, string userData = null);

		// Token: 0x0600084F RID: 2127
		void Info(string area, string message, string userData = null);

		// Token: 0x06000850 RID: 2128
		void Debug(string area, string message, string userData = null);

		// Token: 0x06000851 RID: 2129
		void TrackException(Exception exception);

		// Token: 0x06000852 RID: 2130
		void TrackEvent(string eventName, IReadOnlyCollection<KeyValuePair<string, double>> metrics = null, IReadOnlyCollection<KeyValuePair<string, string>> properties = null, IReadOnlyCollection<KeyValuePair<string, string>> userDataProperties = null);

		// Token: 0x06000853 RID: 2131
		void Flush(bool wait);
	}
}
