using System;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.AnalysisServices.MsoId
{
	// Token: 0x0200011D RID: 285
	[Serializable]
	internal sealed class MsoIdAuthenticationException : Exception, ISerializable
	{
		// Token: 0x0600102B RID: 4139 RVA: 0x00038657 File Offset: 0x00036857
		internal MsoIdAuthenticationException(string message)
			: base(message)
		{
			this.ErrorType = MsoIdAuthenticationError.Unknown;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00038667 File Offset: 0x00036867
		internal MsoIdAuthenticationException(MsoIdAuthenticationError errorType)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00038676 File Offset: 0x00036876
		internal MsoIdAuthenticationException(MsoIdAuthenticationError errorType, int hResult)
		{
			this.ErrorType = errorType;
			base.Data.Add("HResult", hResult);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0003869B File Offset: 0x0003689B
		internal MsoIdAuthenticationException(MsoIdAuthenticationError errorType, Exception innerException)
			: base(innerException.Message, innerException)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000386B1 File Offset: 0x000368B1
		internal MsoIdAuthenticationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ErrorType = (MsoIdAuthenticationError)info.GetInt32("ErrorType");
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x000386CC File Offset: 0x000368CC
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x000386D4 File Offset: 0x000368D4
		public MsoIdAuthenticationError ErrorType { get; private set; }

		// Token: 0x06001032 RID: 4146 RVA: 0x000386DD File Offset: 0x000368DD
		public int GetHResult()
		{
			if (this.Data.Contains("HResult"))
			{
				return (int)this.Data["HResult"];
			}
			return 0;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00038708 File Offset: 0x00036908
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorType", this.ErrorType);
		}

		// Token: 0x04000A1D RID: 2589
		private const string SerializationInfoElementName_ErrorType = "ErrorType";

		// Token: 0x020001CD RID: 461
		private static class DataKeys
		{
			// Token: 0x0400114B RID: 4427
			public const string HResult = "HResult";
		}
	}
}
