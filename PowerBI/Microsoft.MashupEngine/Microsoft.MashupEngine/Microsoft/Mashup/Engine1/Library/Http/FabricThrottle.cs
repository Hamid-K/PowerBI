using System;
using System.IO;
using System.Net;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A4F RID: 2639
	internal static class FabricThrottle
	{
		// Token: 0x060049B8 RID: 18872 RVA: 0x000F5CC4 File Offset: 0x000F3EC4
		public static bool IsThrottledByFabric(Exception e, IEngineHost engineHost, IResource resource, out Exception throttlingException)
		{
			throttlingException = null;
			WebException ex = e as WebException;
			MashupHttpWebResponse mashupHttpWebResponse = ((ex != null) ? ex.Response : null) as MashupHttpWebResponse;
			if (mashupHttpWebResponse == null || resource == null)
			{
				return false;
			}
			bool flag = false;
			int statusCode = (int)mashupHttpWebResponse.StatusCode;
			if (statusCode == 429 && RetryPolicy.GetRetryAfter(mashupHttpWebResponse) == null)
			{
				mashupHttpWebResponse.Buffer();
				try
				{
					using (Stream decompressedResponseStream = mashupHttpWebResponse.GetDecompressedResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(decompressedResponseStream))
						{
							using (JsonTokenizer jsonTokenizer = new JsonTokenizer(streamReader, true, true, null))
							{
								Value value = jsonTokenizer.ReadValue();
								flag = value.IsRecord && FabricThrottle.CheckForFabricThrottling(value.AsRecord);
							}
						}
					}
				}
				catch (Exception ex2) when (SafeExceptions.IsSafeException(ex2))
				{
				}
			}
			if ((statusCode == 429 || statusCode == 503) && mashupHttpWebResponse.Headers["x-ms-error-code"] == "RejectedDueToResourceConstraints")
			{
				flag = true;
			}
			if (flag)
			{
				throttlingException = DataSourceException.NewCapacityExceededException<Message0>(engineHost, resource, Strings.FabricCapacityExceeded, null);
				return true;
			}
			return false;
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x000F5E1C File Offset: 0x000F401C
		private static bool CheckForFabricThrottling(RecordValue recordValue)
		{
			bool flag = FabricThrottle.CheckForCapacityLimitExceeded(recordValue);
			Value value;
			if ((!flag & recordValue.TryGetValue("error", out value)) && value.IsRecord)
			{
				flag = FabricThrottle.CheckForCapacityLimitExceeded(value.AsRecord);
			}
			return flag;
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x000F5E5C File Offset: 0x000F405C
		private static bool CheckForCapacityLimitExceeded(RecordValue recordValue)
		{
			Value value;
			return recordValue.TryGetValue("code", out value) && value.IsText && value.AsText.String == "CapacityLimitExceeded";
		}
	}
}
