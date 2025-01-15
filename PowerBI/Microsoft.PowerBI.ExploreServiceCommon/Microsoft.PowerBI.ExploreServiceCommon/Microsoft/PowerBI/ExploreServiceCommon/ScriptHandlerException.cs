using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ExploreServiceCommon
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	public class ScriptHandlerException : Exception
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00004215 File Offset: 0x00002415
		public ScriptHandlerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000421F File Offset: 0x0000241F
		public ScriptHandlerException(string message, Exception innerException, string errorCode = null)
			: base(message, innerException)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004230 File Offset: 0x00002430
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00004238 File Offset: 0x00002438
		public string ErrorCode { get; private set; }
	}
}
