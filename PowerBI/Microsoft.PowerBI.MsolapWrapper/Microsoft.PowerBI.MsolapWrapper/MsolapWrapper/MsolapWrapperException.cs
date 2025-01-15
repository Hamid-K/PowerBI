using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace MsolapWrapper
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	internal class MsolapWrapperException : Exception, ISerializable
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00001D4C File Offset: 0x0000114C
		public WrapperErrorCodes ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00001D68 File Offset: 0x00001168
		public string ErrorMessage
		{
			get
			{
				return this._errorMessage;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00001D84 File Offset: 0x00001184
		public DataErrorInfo DataErrorInformation
		{
			get
			{
				return this._dataErrorInfo;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00001DA0 File Offset: 0x000011A0
		public WrapperErrorSource ErrorSource
		{
			get
			{
				return this._errorSource;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009C9C File Offset: 0x0000909C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorCode", this._errorCode);
			info.AddValue("ErrorMessage", this._errorMessage);
			info.AddValue("DataErrorInfo", this._dataErrorInfo);
			info.AddValue("ErrorSource", this._errorSource);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009BFC File Offset: 0x00008FFC
		protected MsolapWrapperException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._errorCode = (WrapperErrorCodes)info.GetValue("ErrorCode", typeof(WrapperErrorCodes));
			this._errorMessage = (string)info.GetValue("ErrorMessage", typeof(string));
			this._dataErrorInfo = (DataErrorInfo)info.GetValue("DataErrorInfo", typeof(DataErrorInfo));
			this._errorSource = (WrapperErrorSource)info.GetValue("ErrorSource", typeof(WrapperErrorSource));
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00009BC0 File Offset: 0x00008FC0
		internal MsolapWrapperException(WrapperErrorCodes errorCode, string errorMessage, DataErrorInfo dataErrorInfo, WrapperErrorSource errorSource, Exception innerException)
			: base(errorMessage, innerException)
		{
			this._errorCode = errorCode;
			this._errorMessage = errorMessage;
			this._dataErrorInfo = dataErrorInfo;
			this._errorSource = errorSource;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A098 File Offset: 0x00009498
		internal MsolapWrapperException(WrapperErrorCodes errorCode, string errorMessage, DataErrorInfo dataErrorInfo, WrapperErrorSource errorSource)
			: this(errorCode, errorMessage, dataErrorInfo, errorSource, null)
		{
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A074 File Offset: 0x00009474
		internal MsolapWrapperException(WrapperErrorCodes errorCode, string errorMessage, WrapperErrorSource errorSource, Exception innerException)
			: this(errorCode, errorMessage, new DataErrorInfo(), errorSource, innerException)
		{
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A4E8 File Offset: 0x000098E8
		internal MsolapWrapperException(WrapperErrorCodes errorCode, string errorMessage, WrapperErrorSource errorSource, WrapperErrorSourceOrigin errorSourceOrigin)
			: this(errorCode, errorMessage, new DataErrorInfo(), errorSource)
		{
			this._dataErrorInfo.TypeOrigin = errorSourceOrigin;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000A4C4 File Offset: 0x000098C4
		internal MsolapWrapperException(WrapperErrorCodes errorCode, string errorMessage, WrapperErrorSource errorSource)
			: this(errorCode, errorMessage, new DataErrorInfo(), errorSource)
		{
		}

		// Token: 0x040000FA RID: 250
		private const string ErrorCodeSlotName = "ErrorCode";

		// Token: 0x040000FB RID: 251
		private const string ErrorMessageSlotName = "ErrorMessage";

		// Token: 0x040000FC RID: 252
		private const string DataErrorInfoSlotName = "DataErrorInfo";

		// Token: 0x040000FD RID: 253
		private const string ErrorSourceSlotName = "ErrorSource";

		// Token: 0x040000FE RID: 254
		private WrapperErrorCodes _errorCode;

		// Token: 0x040000FF RID: 255
		private string _errorMessage;

		// Token: 0x04000100 RID: 256
		private DataErrorInfo _dataErrorInfo;

		// Token: 0x04000101 RID: 257
		private WrapperErrorSource _errorSource;

		// Token: 0x04000102 RID: 258
		internal static string ErrorCulture = "en-us";
	}
}
