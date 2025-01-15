using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Utils
{
	// Token: 0x0200003C RID: 60
	public static class RsErrorCodeMapperUtility
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000C728 File Offset: 0x0000A928
		public static Dictionary<ErrorCode, HttpStatusCode> HttpStatusCodeMap
		{
			get
			{
				Dictionary<ErrorCode, HttpStatusCode> dictionary;
				if ((dictionary = RsErrorCodeMapperUtility._httpStatusCodeMap) == null)
				{
					dictionary = (RsErrorCodeMapperUtility._httpStatusCodeMap = RsErrorCodeMapperUtility.BuildDictionary());
				}
				return dictionary;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000C73E File Offset: 0x0000A93E
		private static Dictionary<ErrorCode, HttpStatusCode> BuildDictionary()
		{
			return new Dictionary<ErrorCode, HttpStatusCode>
			{
				{
					ErrorCode.rsInvalidSubscription,
					HttpStatusCode.BadRequest
				},
				{
					ErrorCode.rsUserCannotOwnSubscription,
					HttpStatusCode.Forbidden
				},
				{
					ErrorCode.rsSubscriptionNotFound,
					HttpStatusCode.NotFound
				}
			};
		}

		// Token: 0x040000C2 RID: 194
		private static Dictionary<ErrorCode, HttpStatusCode> _httpStatusCodeMap;
	}
}
