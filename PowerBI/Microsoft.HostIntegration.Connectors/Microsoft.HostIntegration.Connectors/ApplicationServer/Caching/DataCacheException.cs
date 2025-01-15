using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003A3 RID: 931
	[Serializable]
	public class DataCacheException : Exception, ISerializable
	{
		// Token: 0x060020F7 RID: 8439 RVA: 0x00064D0F File Offset: 0x00062F0F
		public DataCacheException()
		{
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x00064D25 File Offset: 0x00062F25
		public DataCacheException(string message)
			: base(message)
		{
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x00064D3C File Offset: 0x00062F3C
		public DataCacheException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00064D54 File Offset: 0x00062F54
		internal DataCacheException(string source, int errorCode, string message)
			: this(source, errorCode, message, false)
		{
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00064D60 File Offset: 0x00062F60
		internal DataCacheException(string source, int errorCode, string message, bool logFlag)
			: this(source, errorCode, message, null, logFlag)
		{
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00064D6E File Offset: 0x00062F6E
		internal DataCacheException(string source, int errorCode, string message, Exception innerException)
			: this(source, errorCode, message, innerException, false)
		{
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x00064D7C File Offset: 0x00062F7C
		internal DataCacheException(string source, int errorCode, string message, Exception innerException, bool logFlag)
			: base(message, innerException)
		{
			this._errorCode = errorCode;
			if (logFlag)
			{
				DataCacheException.LogException(source, DataCacheException.FormatLogMsg(source, errorCode, -1, message, innerException));
			}
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00064DB1 File Offset: 0x00062FB1
		internal DataCacheException(string source, int errorCode, int substatus, string message)
			: this(source, errorCode, substatus, message, false)
		{
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00064DBF File Offset: 0x00062FBF
		internal DataCacheException(string source, int errorCode, int substatus, string message, bool logFlag)
			: this(source, errorCode, substatus, message, null, logFlag)
		{
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00064DCF File Offset: 0x00062FCF
		internal DataCacheException(string source, int errorCode, int substatus, string message, Exception innerException, bool logFlag)
			: base(message, innerException)
		{
			this._errorCode = errorCode;
			this._substatus = substatus;
			if (logFlag)
			{
				DataCacheException.LogException(source, DataCacheException.FormatLogMsg(source, errorCode, substatus, message, innerException));
			}
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x00064E0D File Offset: 0x0006300D
		protected DataCacheException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._errorCode = info.GetInt32("ErrorCode");
			this._substatus = info.GetInt32("Substatus");
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x00064E47 File Offset: 0x00063047
		internal bool IsRetryable()
		{
			return this._errorCode == 17 || this._errorCode == 18 || this._errorCode == 16;
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x00064E6C File Offset: 0x0006306C
		private static string FormatLogMsg(string source, int errorCode, int substatus, string message, Exception innerException)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			if (innerException != null)
			{
				text = DataCacheException.GetErrorStack(innerException);
			}
			stringBuilder.Append("ErrorCode:").Append(DataCacheException.GetErrorString(errorCode)).Append("Substatus:")
				.Append(DataCacheException.GetErrorSubstatusString(substatus))
				.Append(":ComponentName:")
				.Append(source)
				.Append(":Message:")
				.Append(message)
				.Append(":")
				.Append(DateTime.UtcNow.ToShortDateString())
				.Append(":")
				.Append(DateTime.UtcNow.ToShortTimeString())
				.Append("\n")
				.Append(text)
				.Append("\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x00064F34 File Offset: 0x00063134
		private static string GetErrorStack(Exception innerException)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			Exception ex = innerException;
			stringBuilder.Append("Error Stack: \n");
			while (ex != null)
			{
				stringBuilder.Append("\t").Append(num).Append(")")
					.Append(ex.Message)
					.Append("\n");
				ex = ex.InnerException;
				num++;
			}
			stringBuilder.Append("Stack Trace : \n").Append(innerException.StackTrace);
			return stringBuilder.ToString();
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x00064FB8 File Offset: 0x000631B8
		private static void LogException(string src, string errorLogMsg)
		{
			if (Provider.IsEnabled(TraceLevel.Error))
			{
				EventLogWriter.WriteError(src, "{0}", new object[] { errorLogMsg });
			}
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x00064FE4 File Offset: 0x000631E4
		internal void Log(string source)
		{
			if (this.StackTrace != null)
			{
				string text = DataCacheException.FormatLogMsg(source, this._errorCode, this._substatus, this.Message, base.InnerException);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Explicit Error Log\n").Append(stringBuilder).Append(this.StackTrace)
					.Append("\n");
				text = stringBuilder.ToString();
				DataCacheException.LogException(source, text);
			}
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00065054 File Offset: 0x00063254
		internal static string GetErrorString(int errorcode)
		{
			if (errorcode <= 7002)
			{
				if (errorcode <= 3007)
				{
					switch (errorcode)
					{
					case -1:
						return "UnspecifiedErrorCode";
					case 0:
					case 35:
					case 36:
						break;
					case 1:
						return "ERRCA0001";
					case 2:
						return "ERRCA0002";
					case 3:
						return "ERRCA0003";
					case 4:
						return "ERRCA0004";
					case 5:
						return "ERRCA0005";
					case 6:
						return "ERRCA0006";
					case 7:
						return "ERRCA0007";
					case 8:
						return "ERRCA0008";
					case 9:
						return "ERRCA0009";
					case 10:
						return "ERRCA0010";
					case 11:
						return "ERRCA0011";
					case 12:
						return "ERRCA0012";
					case 13:
						return "ERRCA0013";
					case 14:
						return "ERRCA0014";
					case 15:
						return "ERRCA0015";
					case 16:
						return "ERRCA0016";
					case 17:
						return "ERRCA0017";
					case 18:
						return "ERRCA0018";
					case 19:
						return "ERRCA0019";
					case 20:
						return "ERRCA0020";
					case 21:
						return "ERRCA0021";
					case 22:
						return "ERRCA0023";
					case 23:
						return "ERRCA0022";
					case 24:
						return "ERRCA0024";
					case 25:
						return "VersionValueModified";
					case 26:
						return "ERRCA0025";
					case 27:
						return "ERRCA0026";
					case 28:
						return "ERRCA0027";
					case 29:
						return "ERRCA0029";
					case 30:
						return "ERRCA0030";
					case 31:
						return "ERRCA0031";
					case 32:
						return "ERRCA0032";
					case 33:
						return "ERRCA0033";
					case 34:
						return "ERRCA0034";
					case 37:
						return "ERRCA0037";
					case 38:
						return "ERRCA0038";
					case 39:
						return "ERRCA0039";
					case 40:
						return "ERRCA0040";
					case 41:
						return "ERRCA0041";
					case 42:
						return "ERRCA0042";
					default:
						switch (errorcode)
						{
						case 2001:
							return "ERRDM0001";
						case 2002:
							return "ERRDM0002";
						case 2003:
							return "ERRDM0003";
						case 2004:
							return "ERRDM0004";
						case 2005:
							return "ERRDM0005";
						case 2006:
							return "ERRDM0006";
						case 2007:
							return "ERRDM0007";
						case 2008:
							return "ERRDM0008";
						case 2009:
							return "ERRDM0009";
						case 2010:
							return "ERRDM0010";
						case 2011:
							return "ERRDM0011";
						case 2012:
							return "ERRDM00012";
						case 2013:
							return "ERRDM0013";
						default:
							switch (errorcode)
							{
							case 3001:
								return "ERROM0001";
							case 3002:
								return "ERROM0002";
							case 3003:
								return "ERROM0003";
							case 3004:
								return "ERROM0004";
							case 3005:
								return "ERROM0005";
							case 3006:
								return "ERROM0006";
							case 3007:
								return "ERROM0007";
							}
							break;
						}
						break;
					}
				}
				else
				{
					switch (errorcode)
					{
					case 4001:
						return "ERRService0001";
					case 4002:
						return "ERRService0002";
					case 4003:
						return "ERRService0003";
					default:
						switch (errorcode)
						{
						case 6001:
							return "ERRSS0001";
						case 6002:
							return "ERRSS0002";
						case 6003:
							return "ERRSS0003";
						case 6004:
							return "ERRSS0004";
						case 6005:
							return "ERRSS0005";
						case 6006:
							return "ERRSS0006";
						default:
							switch (errorcode)
							{
							case 7001:
								return "ERRRSL0001";
							case 7002:
								return "ERRRSL0002";
							}
							break;
						}
						break;
					}
				}
			}
			else if (errorcode <= 12041)
			{
				switch (errorcode)
				{
				case 8002:
					return "ERRCMC0002";
				case 8003:
					return "ERRCMC0003";
				default:
					switch (errorcode)
					{
					case 9001:
						return "ERRCMS0001";
					case 9002:
						return "ERRCMS0002";
					case 9003:
						return "ERRCMS0003";
					case 9004:
						return "ERRCMS0004";
					case 9005:
						return "ERRCMS0005";
					case 9006:
						return "ERRCMS0006";
					case 9007:
						return "ERRCMS0007";
					case 9008:
						return "ERRCMS0008";
					case 9009:
						return "ERRCMS0009";
					case 9010:
						return "ERRCMS0010";
					case 9011:
						return "ERRCMS0011";
					case 9012:
						return "ERRCMS0012";
					case 9013:
						return "ERRCMS0013";
					default:
						switch (errorcode)
						{
						case 12001:
							return "ERRCAdmin001";
						case 12002:
							return "ERRCAdmin002";
						case 12003:
							return "ERRCAdmin003";
						case 12004:
							return "ERRCAdmin004";
						case 12005:
							return "ERRCAdmin005";
						case 12006:
							return "ERRCAdmin006";
						case 12007:
							return "ERRCAdmin007";
						case 12008:
							return "ERRCAdmin008";
						case 12009:
							return "ERRCAdmin009";
						case 12010:
							return "ERRCAdmin010";
						case 12011:
							return "ERRCAdmin011";
						case 12012:
							return "ERRCAdmin012";
						case 12013:
							return "ERRCAdmin013";
						case 12014:
							return "ERRCAdmin014";
						case 12015:
							return "ERRCAdmin015";
						case 12016:
							return "ERRCAdmin016";
						case 12017:
							return "ERRCAdmin017";
						case 12018:
							return "ERRCAdmin018";
						case 12019:
							return "ERRCAdmin019";
						case 12020:
							return "ERRCAdmin020";
						case 12021:
							return "ERRCAdmin021";
						case 12022:
							return "ERRCAdmin022";
						case 12023:
							return "ERRCAdmin023";
						case 12024:
							return "ERRCAdmin024";
						case 12025:
							return "ERRCAdmin025";
						case 12026:
							return "ERRCAdmin026";
						case 12027:
							return "ERRCAdmin027";
						case 12028:
							return "ERRCAdmin028";
						case 12029:
							return "ERRCAdmin029";
						case 12030:
							return "ERRCAdmin030";
						case 12031:
							return "ERRCAdmin031";
						case 12032:
							return "ERRCAdmin032";
						case 12033:
							return "ERRCAdmin033";
						case 12034:
							return "ERRCAdmin034";
						case 12035:
							return "ERRCAdmin035";
						case 12036:
							return "ERRCAdmin036";
						case 12037:
							return "ERRCAdmin037";
						case 12038:
							return "ERRCAdmin038";
						case 12039:
							return "ERRCAdmin039";
						case 12040:
							return "ERRCAdmin040";
						case 12041:
							return "ERRCAdmin041";
						}
						break;
					}
					break;
				}
			}
			else if (errorcode <= 17036)
			{
				switch (errorcode)
				{
				case 13001:
					return "ERRPS001";
				case 13002:
					return "ERRPS002";
				case 13003:
					return "ERRPS003";
				case 13004:
					return "ERRPS004";
				case 13005:
					return "ERRPS005";
				case 13006:
					return "ERRPS006";
				case 13007:
					return "ERRPS007";
				case 13008:
					return "ERRPS008";
				case 13009:
				case 13010:
					break;
				case 13011:
					return "ERRPS011";
				case 13012:
					return "ERRPS012";
				case 13013:
					return "ERRPS013";
				case 13014:
					return "ERRPS014";
				case 13015:
					return "ERRPS015";
				case 13016:
					return "ERRPS016";
				case 13017:
					return "ERRPS017";
				case 13018:
					return "ERRPS018";
				case 13019:
					return "ERRPS019";
				case 13020:
					return "ERRPS020";
				case 13021:
					return "ERRPS021";
				case 13022:
					return "ERRPS022";
				case 13023:
					return "ERRPS023";
				case 13024:
					return "ERRPS024";
				case 13025:
					return "ERRPS025";
				default:
					switch (errorcode)
					{
					case 17001:
						return "NetworkShareAsLocalPathError";
					case 17002:
						return "NetworkShareFolderConnectionError";
					case 17003:
						return "ClusterConfigReadError";
					case 17004:
						return "ClusterConfigConnectionError";
					case 17005:
						return "NewNetworkShareSetupError";
					case 17006:
						return "ConnectionSettingsRegistrySaveError";
					case 17007:
						return "InstallPathMissingError";
					case 17008:
						return "HostAdditionFailureError";
					case 17009:
						return "IncompleteConnectionParameters";
					case 17010:
						return "PortAlreadyInUseError";
					case 17011:
						return "ClusterAlreadyInitialized";
					case 17012:
						return "ClusterNotInitialized";
					case 17013:
						return "PermissionsError";
					case 17014:
						return "HostAdditionFailureError";
					case 17015:
						return "NonDomainBlockedAccount";
					case 17016:
						return "ServiceAccessError";
					case 17017:
						return "ServiceNotStopped";
					case 17018:
						return "HostEntryNotFound";
					case 17020:
						return "AdminAlreadyConfigured";
					case 17021:
						return "ServiceAlreadyConfigured";
					case 17022:
						return "AdminNotConfigured";
					case 17023:
						return "ServiceNotConfigured";
					case 17024:
						return "GetComputerDomainError";
					case 17025:
						return "TestConnectionFailed";
					case 17026:
						return "RegistryAccessFailed";
					case 17027:
						return "ConfigurationStateSaveError";
					case 17028:
						return "PortDuplicationError";
					case 17029:
						return "NonDomainNWService";
					case 17031:
						return "NetworkShareFilePermissionsError";
					case 17032:
						return "SqlAuthenticationNotSupported";
					case 17033:
						return "HostAlreadyPresent";
					case 17034:
						return "DomainBlockedAccount";
					case 17035:
						return "OffloadingWithXml";
					case 17036:
						return "IncompatibleExpirationParameters";
					}
					break;
				}
			}
			else
			{
				if (errorcode == 18001)
				{
					return "MemoryPoolExhausted";
				}
				if (errorcode == 20001)
				{
					return "ERRCA0035";
				}
			}
			return null;
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x000658A4 File Offset: 0x00063AA4
		internal static string GetErrorSubstatusString(int substatus)
		{
			switch (substatus)
			{
			case -1:
				return "ES0001";
			case 0:
				break;
			case 1:
				return "ES0002";
			case 2:
				return "ES0003";
			case 3:
				return "ES0004";
			case 4:
				return "ES0005";
			case 5:
				return "ES0006";
			case 6:
				return "ES0007";
			case 7:
				return "ES0008";
			case 8:
				return "ES0001";
			case 9:
				return "ES0009";
			case 10:
				return "ES0010";
			case 11:
				return "ES0011";
			case 12:
				return "ES0012";
			case 13:
				return "ES0013";
			case 14:
				return "ES0014";
			case 15:
				return "ES0015";
			case 16:
				return "ES0016";
			default:
				if (substatus == 100)
				{
					return "ES0100";
				}
				break;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0006597C File Offset: 0x00063B7C
		private string GetAdditionalInformationString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.Data.Keys)
			{
				if (this.Data[obj] != null)
				{
					stringBuilder.AppendFormat(CultureInfo.CurrentUICulture, "{0}, {1}", new object[]
					{
						obj,
						this.Data[obj]
					});
				}
				else
				{
					stringBuilder.Append(obj);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00065A24 File Offset: 0x00063C24
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorCode", this._errorCode);
			info.AddValue("Substatus", this._substatus);
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x00065A50 File Offset: 0x00063C50
		public int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x00065A58 File Offset: 0x00063C58
		public int SubStatus
		{
			get
			{
				return this._substatus;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x00065A60 File Offset: 0x00063C60
		// (set) Token: 0x0600210E RID: 8462 RVA: 0x00065A67 File Offset: 0x00063C67
		public override string HelpLink
		{
			get
			{
				return "http://go.microsoft.com/fwlink/?LinkId=164049";
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x00065A70 File Offset: 0x00063C70
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.Data != null && this.Data.Count > 0)
				{
					stringBuilder.AppendFormat(CultureInfo.CurrentUICulture, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "ErrorMessageFormatExtended"), new object[]
					{
						DataCacheException.GetErrorString(this._errorCode),
						DataCacheException.GetErrorSubstatusString(this._substatus),
						base.Message,
						this.GetAdditionalInformationString()
					});
				}
				else
				{
					stringBuilder.AppendFormat(CultureInfo.CurrentUICulture, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "ErrorMessageFormat"), new object[]
					{
						DataCacheException.GetErrorString(this._errorCode),
						DataCacheException.GetErrorSubstatusString(this._substatus),
						base.Message
					});
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x00065B3A File Offset: 0x00063D3A
		// (set) Token: 0x06002111 RID: 8465 RVA: 0x00065B42 File Offset: 0x00063D42
		public Guid TrackingId
		{
			get
			{
				return this._trackingId;
			}
			internal set
			{
				this._trackingId = value;
			}
		}

		// Token: 0x04001515 RID: 5397
		private const string _helpLink = "http://go.microsoft.com/fwlink/?LinkId=164049";

		// Token: 0x04001516 RID: 5398
		private int _errorCode = -1;

		// Token: 0x04001517 RID: 5399
		private int _substatus = -1;

		// Token: 0x04001518 RID: 5400
		private Guid _trackingId;
	}
}
