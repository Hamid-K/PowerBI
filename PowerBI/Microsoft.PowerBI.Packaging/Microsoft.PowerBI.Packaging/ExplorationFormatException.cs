using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class ExplorationFormatException : Exception
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002B93 File Offset: 0x00000D93
		public ExplorationFormatException(string message, ExplorationFormatErrorCode errorCode, ExplorationErrorSource errorSource)
			: base(message)
		{
			this.ErrorCode = errorCode;
			this.ErrorSource = errorSource;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002BAA File Offset: 0x00000DAA
		public ExplorationFormatException(string message, Exception innerException, ExplorationFormatErrorCode errorCode, ExplorationErrorSource errorSource)
			: base(message, innerException)
		{
			this.ErrorCode = errorCode;
			this.ErrorSource = errorSource;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public ExplorationFormatErrorCode ErrorCode { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002BCB File Offset: 0x00000DCB
		public ExplorationErrorSource ErrorSource { get; }

		// Token: 0x06000068 RID: 104 RVA: 0x00002BD3 File Offset: 0x00000DD3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
