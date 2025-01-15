using System;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F0C RID: 3852
	[Serializable]
	internal class AnalysisServicesException : Exception
	{
		// Token: 0x0600660A RID: 26122 RVA: 0x00005F3B File Offset: 0x0000413B
		public AnalysisServicesException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
