using System;
using System.Globalization;
using System.Net.Http.Headers;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000170 RID: 368
	public class MsalServiceException : MsalException
	{
		// Token: 0x0600122A RID: 4650 RVA: 0x0003E1FF File Offset: 0x0003C3FF
		public MsalServiceException(string errorCode, string errorMessage)
			: base(errorCode, errorMessage)
		{
			if (string.IsNullOrWhiteSpace(errorMessage))
			{
				throw new ArgumentNullException("errorMessage");
			}
			this.UpdateIsRetryable();
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0003E222 File Offset: 0x0003C422
		public MsalServiceException(string errorCode, string errorMessage, int statusCode)
			: this(errorCode, errorMessage)
		{
			this.StatusCode = statusCode;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0003E233 File Offset: 0x0003C433
		public MsalServiceException(string errorCode, string errorMessage, Exception innerException)
			: base(errorCode, errorMessage, innerException)
		{
			this.UpdateIsRetryable();
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0003E244 File Offset: 0x0003C444
		public MsalServiceException(string errorCode, string errorMessage, int statusCode, Exception innerException)
			: base(errorCode, errorMessage, innerException)
		{
			this.StatusCode = statusCode;
			this.UpdateIsRetryable();
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0003E25D File Offset: 0x0003C45D
		public MsalServiceException(string errorCode, string errorMessage, int statusCode, string claims, Exception innerException)
			: this(errorCode, errorMessage, statusCode, innerException)
		{
			this.Claims = claims;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0003E272 File Offset: 0x0003C472
		// (set) Token: 0x06001230 RID: 4656 RVA: 0x0003E27A File Offset: 0x0003C47A
		public int StatusCode
		{
			get
			{
				return this._statusCode;
			}
			internal set
			{
				this._statusCode = value;
				this.UpdateIsRetryable();
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0003E289 File Offset: 0x0003C489
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x0003E291 File Offset: 0x0003C491
		public string Claims { get; internal set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0003E29A File Offset: 0x0003C49A
		// (set) Token: 0x06001234 RID: 4660 RVA: 0x0003E2A2 File Offset: 0x0003C4A2
		public string ResponseBody
		{
			get
			{
				return this._responseBody;
			}
			set
			{
				this._responseBody = value;
				this.UpdateIsRetryable();
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0003E2B1 File Offset: 0x0003C4B1
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x0003E2B9 File Offset: 0x0003C4B9
		public HttpResponseHeaders Headers
		{
			get
			{
				return this._headers;
			}
			set
			{
				this._headers = value;
				this.UpdateIsRetryable();
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0003E2C8 File Offset: 0x0003C4C8
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x0003E2D0 File Offset: 0x0003C4D0
		internal string SubError { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0003E2D9 File Offset: 0x0003C4D9
		// (set) Token: 0x0600123A RID: 4666 RVA: 0x0003E2E1 File Offset: 0x0003C4E1
		internal string[] ErrorCodes { get; set; }

		// Token: 0x0600123B RID: 4667 RVA: 0x0003E2EC File Offset: 0x0003C4EC
		protected virtual void UpdateIsRetryable()
		{
			base.IsRetryable = (this.StatusCode >= 500 && this.StatusCode < 600) || this.StatusCode == 429 || this.StatusCode == 408 || string.Equals(base.ErrorCode, "request_timeout", StringComparison.OrdinalIgnoreCase) || string.Equals(base.ErrorCode, "temporarily_unavailable", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0003E35A File Offset: 0x0003C55A
		public override string ToString()
		{
			return base.ToString() + string.Format(CultureInfo.InvariantCulture, "\n\tStatusCode: {0} \n\tResponseBody: {1} \n\tHeaders: {2}", this.StatusCode, this.ResponseBody, this.Headers);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0003E390 File Offset: 0x0003C590
		internal override void PopulateJson(JObject jObject)
		{
			base.PopulateJson(jObject);
			jObject["claims"] = this.Claims;
			jObject["response_body"] = this.ResponseBody;
			jObject["correlation_id"] = base.CorrelationId;
			jObject["sub_error"] = this.SubError;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0003E3FC File Offset: 0x0003C5FC
		internal override void PopulateObjectFromJson(JObject jObject)
		{
			base.PopulateObjectFromJson(jObject);
			this.Claims = JsonHelper.GetExistingOrEmptyString(jObject, "claims");
			this.ResponseBody = JsonHelper.GetExistingOrEmptyString(jObject, "response_body");
			base.CorrelationId = JsonHelper.GetExistingOrEmptyString(jObject, "correlation_id");
			this.SubError = JsonHelper.GetExistingOrEmptyString(jObject, "sub_error");
		}

		// Token: 0x040006B8 RID: 1720
		private const string ClaimsKey = "claims";

		// Token: 0x040006B9 RID: 1721
		private const string ResponseBodyKey = "response_body";

		// Token: 0x040006BA RID: 1722
		private const string CorrelationIdKey = "correlation_id";

		// Token: 0x040006BB RID: 1723
		private const string SubErrorKey = "sub_error";

		// Token: 0x040006BC RID: 1724
		private int _statusCode;

		// Token: 0x040006BD RID: 1725
		private string _responseBody;

		// Token: 0x040006BE RID: 1726
		private HttpResponseHeaders _headers;
	}
}
