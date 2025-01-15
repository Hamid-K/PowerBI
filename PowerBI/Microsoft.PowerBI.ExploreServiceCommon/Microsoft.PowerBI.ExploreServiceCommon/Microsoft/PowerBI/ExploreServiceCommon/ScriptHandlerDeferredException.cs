using System;

namespace Microsoft.PowerBI.ExploreServiceCommon
{
	// Token: 0x0200001E RID: 30
	public class ScriptHandlerDeferredException : Exception
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000041EE File Offset: 0x000023EE
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000041F6 File Offset: 0x000023F6
		public ScriptHandlerDeferredException.ExceptionType Type { get; private set; }

		// Token: 0x060000F6 RID: 246 RVA: 0x000041FF File Offset: 0x000023FF
		public ScriptHandlerDeferredException(ScriptHandlerDeferredException.ExceptionType type, Exception innerException)
			: base(innerException.Message, innerException)
		{
			this.Type = type;
		}

		// Token: 0x0200002C RID: 44
		public enum ExceptionType
		{
			// Token: 0x040000F3 RID: 243
			failedToMaterilaizeInputData
		}
	}
}
