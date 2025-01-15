using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	public class PowerBIExploreException : Exception
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0000588C File Offset: 0x00003A8C
		public PowerBIExploreException(string errorCode, string message, Exception innerException, ErrorSource errorSource, IReadOnlyDictionary<string, string> errorDetails, ServiceErrorStatusCode statusCode = ServiceErrorStatusCode.GeneralError)
			: base(message, innerException)
		{
			this.ErrorCode = errorCode;
			this.ErrorSource = errorSource;
			this.ErrorStatusCode = statusCode;
			this.ErrorDetails = errorDetails;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000058B5 File Offset: 0x00003AB5
		public PowerBIExploreException(string errorCode, string message, Exception innerException, ErrorSource errorSource, ServiceErrorStatusCode statusCode = ServiceErrorStatusCode.GeneralError)
			: this(errorCode, message, innerException, errorSource, null, statusCode)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000058C5 File Offset: 0x00003AC5
		public PowerBIExploreException(string errorCode, string message, ErrorSource errorSource, ServiceErrorStatusCode statusCode = ServiceErrorStatusCode.GeneralError)
			: this(errorCode, message, null, errorSource, null, statusCode)
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000058D4 File Offset: 0x00003AD4
		public PowerBIExploreException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ErrorCode = (string)info.GetValue("ErrorCode", typeof(string));
			this.ErrorSource = (ErrorSource)info.GetValue("ErrorSource", typeof(ErrorSource));
			this.ErrorStatusCode = (ServiceErrorStatusCode)info.GetValue("ErrorStatusCode", typeof(ServiceErrorStatusCode));
			this.ErrorDetails = (IReadOnlyDictionary<string, string>)info.GetValue("ErrorDetails", typeof(IReadOnlyDictionary<string, string>));
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00005969 File Offset: 0x00003B69
		public string ErrorCode { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00005971 File Offset: 0x00003B71
		public ErrorSource ErrorSource { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00005979 File Offset: 0x00003B79
		public ServiceErrorStatusCode ErrorStatusCode { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00005981 File Offset: 0x00003B81
		public IReadOnlyDictionary<string, string> ErrorDetails { get; }

		// Token: 0x060001F7 RID: 503 RVA: 0x0000598C File Offset: 0x00003B8C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorCode", this.ErrorCode);
			info.AddValue("ErrorSource", this.ErrorSource);
			info.AddValue("ErrorStatusCode", this.ErrorStatusCode);
			info.AddValue("ErrorDetails", this.ErrorDetails);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000059F0 File Offset: 0x00003BF0
		public virtual string ToTraceString()
		{
			string fullName = base.GetType().FullName;
			string text = "Type={0}, Message={1}, ErrorCode={2}, ErrorSource={3}, ErrorStatusCode={4}, InnerErrorDetails=[{5}]";
			string text2 = PowerBIExploreException.FormatInnerErrorDetails(this);
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { fullName, this.Message, this.ErrorCode, this.ErrorSource, this.ErrorStatusCode, text2 });
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005A5D File Offset: 0x00003C5D
		internal static string FormatInnerErrorDetails(Exception ex)
		{
			if (ex.InnerException == null)
			{
				return string.Empty;
			}
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Message={1}", ex.InnerException.GetType(), ex.InnerException.Message);
		}

		// Token: 0x04000130 RID: 304
		private const string ErrorCodeSlotName = "ErrorCode";

		// Token: 0x04000131 RID: 305
		private const string ErrorDetailsSlotName = "ErrorDetails";

		// Token: 0x04000132 RID: 306
		private const string ErrorSourceSlotName = "ErrorSource";

		// Token: 0x04000133 RID: 307
		private const string ErrorStatusCodeSlotName = "ErrorStatusCode";
	}
}
