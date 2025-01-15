using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.PowerBI.DataExtension.Contracts
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public sealed class DataExtensionException : Exception
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000023B8 File Offset: 0x000005B8
		public DataExtensionException(string errorCode, string message, bool containsPii, Exception innerException)
			: this(errorCode, message, 0U, null, null, 0, false, null, containsPii, ErrorSource.Unknown, null, innerException, null)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023DC File Offset: 0x000005DC
		public DataExtensionException(string errorCode, string message, uint providerErrorCode, string providerMessage, string providerGenericMessage, int hresult, bool hasUserSafeProviderMessage, string onPremErrorCode, ErrorSource errorSource)
			: this(errorCode, message, providerErrorCode, providerMessage, providerGenericMessage, hresult, hasUserSafeProviderMessage, onPremErrorCode, true, errorSource, null, null, null)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002404 File Offset: 0x00000604
		public DataExtensionException(string errorCode, string message, uint providerErrorCode, string providerMessage, string providerGenericMessage, int hresult, bool hasUserSafeProviderMessage, string onPremErrorCode, Exception innerException, ErrorSource errorSource, string errorSourceOrigin)
			: this(errorCode, message, providerErrorCode, providerMessage, providerGenericMessage, hresult, hasUserSafeProviderMessage, onPremErrorCode, true, errorSource, errorSourceOrigin, innerException, null)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000242C File Offset: 0x0000062C
		public DataExtensionException(string errorCode, string message, uint providerErrorCode, string providerMessage, string providerGenericMessage, int hresult, bool hasUserSafeProviderMessage, string onPremErrorCode, bool containsPii, ErrorSource errorSource, string errorSourceOrigin = null, Exception innerException = null, string userMessage = null)
			: base(message, innerException)
		{
			this._errorCode = errorCode;
			this._providerErrorCode = providerErrorCode;
			this._providerMessage = providerMessage;
			this._providerGenericMessage = providerGenericMessage;
			base.HResult = hresult;
			this._hasUserSafeProviderMessage = hasUserSafeProviderMessage;
			this._onPremErrorCode = onPremErrorCode;
			this._containsPii = containsPii;
			this._errorSource = errorSource;
			this._errorSourceOrigin = errorSourceOrigin;
			this._userMessage = userMessage;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002498 File Offset: 0x00000698
		public DataExtensionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._errorCode = (string)info.GetValue("ErrorCode", typeof(string));
			this._providerErrorCode = (uint)info.GetValue("ProviderErrorCode", typeof(uint));
			this._providerMessage = (string)info.GetValue("ProviderMessage", typeof(string));
			this._providerGenericMessage = (string)info.GetValue("ProviderGenericMessage", typeof(string));
			this._hasUserSafeProviderMessage = (bool)info.GetValue("HasUserSafeProviderMessage", typeof(bool));
			this._onPremErrorCode = (string)info.GetValue("OnPremErrorCode", typeof(string));
			this._containsPii = (bool)info.GetValue("ContainsPii", typeof(bool));
			this._errorSource = (ErrorSource)info.GetValue("ErrorSource", typeof(ErrorSource));
			this._errorSourceOrigin = (string)info.GetValue("ErrorSourceOrigin", typeof(string));
			this._userMessage = (string)info.GetValue("UserMessage", typeof(string));
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000025ED File Offset: 0x000007ED
		public string ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000025F5 File Offset: 0x000007F5
		public uint ProviderErrorCode
		{
			get
			{
				return this._providerErrorCode;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000025FD File Offset: 0x000007FD
		public string ProviderMessage
		{
			get
			{
				return this._providerMessage;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002605 File Offset: 0x00000805
		public string ProviderGenericMessage
		{
			get
			{
				return this._providerGenericMessage;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000260D File Offset: 0x0000080D
		public bool HasUserSafeErrorDetails
		{
			get
			{
				return this._hasUserSafeProviderMessage;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002615 File Offset: 0x00000815
		public bool HasUserSafeProviderMessage
		{
			get
			{
				return this._hasUserSafeProviderMessage;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000261D File Offset: 0x0000081D
		public string OnPremErrorCode
		{
			get
			{
				return this._onPremErrorCode;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002625 File Offset: 0x00000825
		public bool ContainsPii
		{
			get
			{
				return this._containsPii;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000262D File Offset: 0x0000082D
		public ErrorSource ErrorSource
		{
			get
			{
				return this._errorSource;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002635 File Offset: 0x00000835
		public string ErrorSourceOrigin
		{
			get
			{
				return this._errorSourceOrigin;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000263D File Offset: 0x0000083D
		public string Language
		{
			get
			{
				return "en-US";
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002644 File Offset: 0x00000844
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorCode", this.ErrorCode);
			info.AddValue("ErrorSource", this.ErrorSource);
			info.AddValue("ErrorSourceOrigin", this.ErrorSourceOrigin);
			info.AddValue("ProviderErrorCode", this._providerErrorCode);
			info.AddValue("ProviderMessage", this._providerMessage);
			info.AddValue("ProviderGenericMessage", this._providerGenericMessage);
			info.AddValue("UserMessage", this._userMessage);
			info.AddValue("HasUserSafeProviderMessage", this._hasUserSafeProviderMessage);
			info.AddValue("ContainsPii", this._containsPii);
			info.AddValue("OnPremErrorCode", this._onPremErrorCode);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002708 File Offset: 0x00000908
		public string GetErrorDetails()
		{
			return this._userMessage ?? this.ProviderMessage;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000271A File Offset: 0x0000091A
		public string ToErrorDetailsString()
		{
			return this.FormatDataExtensionErrorDetails(false);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002723 File Offset: 0x00000923
		public override string ToString()
		{
			return this.FormatDataExtensionErrorDetails(true);
		}

		// Token: 0x04000058 RID: 88
		private const string ErrorCodeSlotName = "ErrorCode";

		// Token: 0x04000059 RID: 89
		private const string ProviderErrorCodeSlotName = "ProviderErrorCode";

		// Token: 0x0400005A RID: 90
		private const string ProviderMessageSlotName = "ProviderMessage";

		// Token: 0x0400005B RID: 91
		private const string ProviderGenericMessageSlotName = "ProviderGenericMessage";

		// Token: 0x0400005C RID: 92
		private const string HasUserSafeProviderMessageSlotName = "HasUserSafeProviderMessage";

		// Token: 0x0400005D RID: 93
		private const string OnPremErrorCodeSlotName = "OnPremErrorCode";

		// Token: 0x0400005E RID: 94
		private const string ContainsPiiSlotName = "ContainsPii";

		// Token: 0x0400005F RID: 95
		private const string ErrorSourceSlotName = "ErrorSource";

		// Token: 0x04000060 RID: 96
		private const string ErrorSourceOriginSlotName = "ErrorSourceOrigin";

		// Token: 0x04000061 RID: 97
		private const string UserMessageSlotName = "UserMessage";

		// Token: 0x04000062 RID: 98
		internal const string ErrorLanguage = "en-US";

		// Token: 0x04000063 RID: 99
		private readonly string _errorCode;

		// Token: 0x04000064 RID: 100
		private readonly uint _providerErrorCode;

		// Token: 0x04000065 RID: 101
		private readonly string _providerMessage;

		// Token: 0x04000066 RID: 102
		private readonly string _providerGenericMessage;

		// Token: 0x04000067 RID: 103
		private readonly bool _hasUserSafeProviderMessage;

		// Token: 0x04000068 RID: 104
		private readonly string _onPremErrorCode;

		// Token: 0x04000069 RID: 105
		private readonly bool _containsPii;

		// Token: 0x0400006A RID: 106
		private readonly ErrorSource _errorSource;

		// Token: 0x0400006B RID: 107
		private readonly string _errorSourceOrigin;

		// Token: 0x0400006C RID: 108
		private readonly string _userMessage;
	}
}
