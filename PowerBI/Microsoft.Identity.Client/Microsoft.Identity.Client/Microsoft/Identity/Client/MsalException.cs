using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.PlatformsCommon.Factories;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200016E RID: 366
	public class MsalException : Exception
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0003DC91 File Offset: 0x0003BE91
		// (set) Token: 0x06001214 RID: 4628 RVA: 0x0003DC99 File Offset: 0x0003BE99
		public bool IsRetryable { get; set; }

		// Token: 0x06001215 RID: 4629 RVA: 0x0003DCA2 File Offset: 0x0003BEA2
		public MsalException()
			: base("Unknown error")
		{
			this.ErrorCode = "unknown_error";
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0003DCC5 File Offset: 0x0003BEC5
		public MsalException(string errorCode)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0003DCDF File Offset: 0x0003BEDF
		public MsalException(string errorCode, string errorMessage)
			: base(errorMessage)
		{
			if (string.IsNullOrWhiteSpace(this.Message))
			{
				throw new ArgumentNullException("errorMessage");
			}
			this.ErrorCode = errorCode;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0003DD12 File Offset: 0x0003BF12
		public MsalException(string errorCode, string errorMessage, Exception innerException)
			: base(errorMessage, innerException)
		{
			if (string.IsNullOrWhiteSpace(this.Message))
			{
				throw new ArgumentNullException("errorMessage");
			}
			this.ErrorCode = errorCode;
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0003DD46 File Offset: 0x0003BF46
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x0003DD4E File Offset: 0x0003BF4E
		public string ErrorCode
		{
			get
			{
				return this._errorCode;
			}
			private set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					this._errorCode = value;
					return;
				}
				throw new ArgumentNullException("ErrorCode");
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0003DD6C File Offset: 0x0003BF6C
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x0003DD74 File Offset: 0x0003BF74
		public string CorrelationId { get; set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0003DD7D File Offset: 0x0003BF7D
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x0003DD85 File Offset: 0x0003BF85
		public IReadOnlyDictionary<string, string> AdditionalExceptionData { get; set; } = CollectionHelpers.GetEmptyDictionary<string, string>();

		// Token: 0x0600121F RID: 4639 RVA: 0x0003DD90 File Offset: 0x0003BF90
		public override string ToString()
		{
			string productName = PlatformProxyFactory.CreatePlatformProxy(null).GetProductName();
			string msalVersion = MsalIdHelper.GetMsalVersion();
			string text = ((base.InnerException == null) ? string.Empty : string.Format("\nInner Exception: {0}", base.InnerException));
			return string.Concat(new string[]
			{
				productName,
				".",
				msalVersion,
				".",
				base.GetType().Name,
				":\r\n\tErrorCode: ",
				this.ErrorCode,
				"\r\n",
				base.ToString(),
				text
			});
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0003DE28 File Offset: 0x0003C028
		internal virtual void PopulateJson(JObject jObject)
		{
			jObject["type"] = base.GetType().Name;
			jObject["error_code"] = this.ErrorCode;
			jObject["error_description"] = this.Message;
			JObject jobject = new JObject();
			string text;
			if (this.AdditionalExceptionData.TryGetValue("BrokerErrorContext", out text))
			{
				jobject["broker_error_context"] = text;
			}
			string text2;
			if (this.AdditionalExceptionData.TryGetValue("BrokerErrorTag", out text2))
			{
				jobject["broker_error_tag"] = text2;
			}
			string text3;
			if (this.AdditionalExceptionData.TryGetValue("BrokerErrorStatus", out text3))
			{
				jobject["broker_error_status"] = text3;
			}
			string text4;
			if (this.AdditionalExceptionData.TryGetValue("BrokerErrorCode", out text4))
			{
				jobject["broker_error_code"] = text4;
			}
			string text5;
			if (this.AdditionalExceptionData.TryGetValue("BrokerTelemetry", out text5))
			{
				jobject["broker_telemetry"] = text5;
			}
			string text6;
			if (this.AdditionalExceptionData.TryGetValue("ManagedIdentitySource", out text6))
			{
				jobject["managed_identity_source"] = text6;
			}
			jObject["additional_exception_data"] = jobject;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0003DF70 File Offset: 0x0003C170
		internal virtual void PopulateObjectFromJson(JObject jObject)
		{
			IDictionary<string, string> dictionary = JsonHelper.ExtractInnerJsonAsDictionary(jObject, "additional_exception_data");
			string text;
			if (dictionary.TryGetValue("broker_error_context", out text))
			{
				dictionary["BrokerErrorContext"] = text;
				dictionary.Remove("broker_error_context");
			}
			string text2;
			if (dictionary.TryGetValue("broker_error_tag", out text2))
			{
				dictionary["BrokerErrorTag"] = text2;
				dictionary.Remove("broker_error_tag");
			}
			string text3;
			if (dictionary.TryGetValue("broker_error_status", out text3))
			{
				dictionary["BrokerErrorStatus"] = text3;
				dictionary.Remove("broker_error_status");
			}
			string text4;
			if (dictionary.TryGetValue("broker_error_code", out text4))
			{
				dictionary["BrokerErrorCode"] = text4;
				dictionary.Remove("broker_error_code");
			}
			string text5;
			if (dictionary.TryGetValue("broker_telemetry", out text5))
			{
				dictionary["BrokerTelemetry"] = text5;
				dictionary.Remove("broker_telemetry");
			}
			string text6;
			if (dictionary.TryGetValue("managed_identity_source", out text6))
			{
				dictionary["ManagedIdentitySource"] = text6;
				dictionary.Remove("managed_identity_source");
			}
			this.AdditionalExceptionData = (IReadOnlyDictionary<string, string>)dictionary;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0003E084 File Offset: 0x0003C284
		public string ToJsonString()
		{
			JObject jobject = new JObject();
			this.PopulateJson(jobject);
			return jobject.ToString();
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0003E0A4 File Offset: 0x0003C2A4
		public static MsalException FromJsonString(string json)
		{
			JObject jobject = JsonHelper.ParseIntoJsonObject(json);
			string value = JsonHelper.GetValue<string>(jobject["type"]);
			string existingOrEmptyString = JsonHelper.GetExistingOrEmptyString(jobject, "error_code");
			string existingOrEmptyString2 = JsonHelper.GetExistingOrEmptyString(jobject, "error_description");
			MsalException ex;
			if (!(value == "MsalException"))
			{
				if (!(value == "MsalClientException"))
				{
					if (!(value == "MsalServiceException"))
					{
						if (!(value == "MsalUiRequiredException"))
						{
							throw new MsalClientException("json_parse_failed", "Attempted to deserialize an MsalException but the type was unknown. ");
						}
						ex = new MsalUiRequiredException(existingOrEmptyString, existingOrEmptyString2);
					}
					else
					{
						ex = new MsalServiceException(existingOrEmptyString, existingOrEmptyString2);
					}
				}
				else
				{
					ex = new MsalClientException(existingOrEmptyString, existingOrEmptyString2);
				}
			}
			else
			{
				ex = new MsalException(existingOrEmptyString, existingOrEmptyString2);
			}
			MsalException ex2 = ex;
			ex2.PopulateObjectFromJson(jobject);
			return ex2;
		}

		// Token: 0x040006AD RID: 1709
		public const string BrokerErrorContext = "BrokerErrorContext";

		// Token: 0x040006AE RID: 1710
		public const string BrokerErrorTag = "BrokerErrorTag";

		// Token: 0x040006AF RID: 1711
		public const string BrokerErrorStatus = "BrokerErrorStatus";

		// Token: 0x040006B0 RID: 1712
		public const string BrokerErrorCode = "BrokerErrorCode";

		// Token: 0x040006B1 RID: 1713
		public const string BrokerTelemetry = "BrokerTelemetry";

		// Token: 0x040006B2 RID: 1714
		public const string ManagedIdentitySource = "ManagedIdentitySource";

		// Token: 0x040006B3 RID: 1715
		private string _errorCode;

		// Token: 0x02000402 RID: 1026
		private class ExceptionSerializationKey
		{
			// Token: 0x040011D6 RID: 4566
			internal const string ExceptionTypeKey = "type";

			// Token: 0x040011D7 RID: 4567
			internal const string ErrorCodeKey = "error_code";

			// Token: 0x040011D8 RID: 4568
			internal const string ErrorDescriptionKey = "error_description";

			// Token: 0x040011D9 RID: 4569
			internal const string AdditionalExceptionData = "additional_exception_data";

			// Token: 0x040011DA RID: 4570
			internal const string BrokerErrorContext = "broker_error_context";

			// Token: 0x040011DB RID: 4571
			internal const string BrokerErrorTag = "broker_error_tag";

			// Token: 0x040011DC RID: 4572
			internal const string BrokerErrorStatus = "broker_error_status";

			// Token: 0x040011DD RID: 4573
			internal const string BrokerErrorCode = "broker_error_code";

			// Token: 0x040011DE RID: 4574
			internal const string BrokerTelemetry = "broker_telemetry";

			// Token: 0x040011DF RID: 4575
			internal const string ManagedIdentitySource = "managed_identity_source";
		}
	}
}
