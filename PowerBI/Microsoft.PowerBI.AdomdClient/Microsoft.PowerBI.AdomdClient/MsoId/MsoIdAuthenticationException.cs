using System;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	internal sealed class MsoIdAuthenticationException : Exception, ISerializable
	{
		// Token: 0x06000F90 RID: 3984 RVA: 0x00035A23 File Offset: 0x00033C23
		internal MsoIdAuthenticationException(string message)
			: base(message)
		{
			this.ErrorType = MsoIdAuthenticationError.Unknown;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00035A33 File Offset: 0x00033C33
		internal MsoIdAuthenticationException(MsoIdAuthenticationError errorType)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00035A42 File Offset: 0x00033C42
		internal MsoIdAuthenticationException(MsoIdAuthenticationError errorType, int hResult)
		{
			this.ErrorType = errorType;
			base.Data.Add("HResult", hResult);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00035A67 File Offset: 0x00033C67
		internal MsoIdAuthenticationException(MsoIdAuthenticationError errorType, Exception innerException)
			: base(innerException.Message, innerException)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00035A7D File Offset: 0x00033C7D
		internal MsoIdAuthenticationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ErrorType = (MsoIdAuthenticationError)info.GetInt32("ErrorType");
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00035A98 File Offset: 0x00033C98
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x00035AA0 File Offset: 0x00033CA0
		public MsoIdAuthenticationError ErrorType { get; private set; }

		// Token: 0x06000F97 RID: 3991 RVA: 0x00035AA9 File Offset: 0x00033CA9
		public int GetHResult()
		{
			if (this.Data.Contains("HResult"))
			{
				return (int)this.Data["HResult"];
			}
			return 0;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00035AD4 File Offset: 0x00033CD4
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorType", this.ErrorType);
		}

		// Token: 0x04000A57 RID: 2647
		private const string SerializationInfoElementName_ErrorType = "ErrorType";

		// Token: 0x020001F0 RID: 496
		private static class DataKeys
		{
			// Token: 0x04000E7F RID: 3711
			public const string HResult = "HResult";
		}
	}
}
