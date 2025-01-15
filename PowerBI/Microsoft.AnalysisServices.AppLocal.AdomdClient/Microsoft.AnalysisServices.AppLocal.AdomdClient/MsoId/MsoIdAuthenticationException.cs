using System;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	internal sealed class MsoIdAuthenticationException : Exception, ISerializable
	{
		// Token: 0x06000F9D RID: 3997 RVA: 0x00035D53 File Offset: 0x00033F53
		internal MsoIdAuthenticationException(string message)
			: base(message)
		{
			this.ErrorType = MsoIdAuthenticationError.Unknown;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00035D63 File Offset: 0x00033F63
		internal MsoIdAuthenticationException(MsoIdAuthenticationError errorType)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00035D72 File Offset: 0x00033F72
		internal MsoIdAuthenticationException(MsoIdAuthenticationError errorType, int hResult)
		{
			this.ErrorType = errorType;
			base.Data.Add("HResult", hResult);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00035D97 File Offset: 0x00033F97
		internal MsoIdAuthenticationException(MsoIdAuthenticationError errorType, Exception innerException)
			: base(innerException.Message, innerException)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00035DAD File Offset: 0x00033FAD
		internal MsoIdAuthenticationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ErrorType = (MsoIdAuthenticationError)info.GetInt32("ErrorType");
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00035DC8 File Offset: 0x00033FC8
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x00035DD0 File Offset: 0x00033FD0
		public MsoIdAuthenticationError ErrorType { get; private set; }

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00035DD9 File Offset: 0x00033FD9
		public int GetHResult()
		{
			if (this.Data.Contains("HResult"))
			{
				return (int)this.Data["HResult"];
			}
			return 0;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00035E04 File Offset: 0x00034004
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorType", this.ErrorType);
		}

		// Token: 0x04000A64 RID: 2660
		private const string SerializationInfoElementName_ErrorType = "ErrorType";

		// Token: 0x020001F0 RID: 496
		private static class DataKeys
		{
			// Token: 0x04000E90 RID: 3728
			public const string HResult = "HResult";
		}
	}
}
