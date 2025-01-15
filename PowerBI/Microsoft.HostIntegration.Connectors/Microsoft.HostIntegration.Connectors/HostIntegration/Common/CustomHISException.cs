using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020004F4 RID: 1268
	[Serializable]
	public class CustomHISException : ApplicationException
	{
		// Token: 0x06002B0B RID: 11019 RVA: 0x000942C9 File Offset: 0x000924C9
		public CustomHISException(string message, int result)
			: base(message)
		{
			base.HResult = result;
			this._compatibleErrorCode = result;
			this._messageId = message.Substring(0, 11);
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000942F0 File Offset: 0x000924F0
		public CustomHISException(string message, int compatibleErrorCode, bool makeCompatible)
			: base(makeCompatible ? string.Format("({0}) {1}", compatibleErrorCode, message) : message)
		{
			this._compatibleErrorCode = compatibleErrorCode;
			this._messageId = message.Substring(0, 11);
			if (makeCompatible)
			{
				base.HResult = (int)((ulong)(-2147221504) + (ulong)((long)compatibleErrorCode));
				return;
			}
			base.HResult = CustomHISException.E_FAIL;
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x00094350 File Offset: 0x00092550
		public CustomHISException(string message, int compatibleErrorCode, bool makeCompatible, Exception inner)
			: base(makeCompatible ? string.Format("({0}) {1}", compatibleErrorCode, message) : message, inner)
		{
			this._compatibleErrorCode = compatibleErrorCode;
			this._messageId = message.Substring(0, 11);
			if (makeCompatible)
			{
				base.HResult = (int)((ulong)(-2147221504) + (ulong)((long)compatibleErrorCode));
				return;
			}
			base.HResult = CustomHISException.E_FAIL;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000943B1 File Offset: 0x000925B1
		public CustomHISException()
		{
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000943B9 File Offset: 0x000925B9
		public CustomHISException(string message)
			: base(message)
		{
			this._messageId = message.Substring(0, 11);
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000943D1 File Offset: 0x000925D1
		public CustomHISException(string message, Exception inner)
			: base(message, inner)
		{
			this._messageId = message.Substring(0, 11);
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000943EA File Offset: 0x000925EA
		protected CustomHISException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x000943F4 File Offset: 0x000925F4
		// (set) Token: 0x06002B13 RID: 11027 RVA: 0x000036A9 File Offset: 0x000018A9
		public int UserErrorCode
		{
			get
			{
				return this._compatibleErrorCode;
			}
			set
			{
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x000943FC File Offset: 0x000925FC
		// (set) Token: 0x06002B15 RID: 11029 RVA: 0x000036A9 File Offset: 0x000018A9
		public string TIExceptionMsgId
		{
			get
			{
				return this._messageId;
			}
			set
			{
			}
		}

		// Token: 0x04001A1F RID: 6687
		private static int E_FAIL = -2147467259;

		// Token: 0x04001A20 RID: 6688
		private int _compatibleErrorCode;

		// Token: 0x04001A21 RID: 6689
		private string _messageId;
	}
}
