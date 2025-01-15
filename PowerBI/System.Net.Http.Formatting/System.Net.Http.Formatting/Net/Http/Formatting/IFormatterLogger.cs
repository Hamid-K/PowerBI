using System;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200003A RID: 58
	public interface IFormatterLogger
	{
		// Token: 0x0600023B RID: 571
		void LogError(string errorPath, string errorMessage);

		// Token: 0x0600023C RID: 572
		void LogError(string errorPath, Exception exception);
	}
}
