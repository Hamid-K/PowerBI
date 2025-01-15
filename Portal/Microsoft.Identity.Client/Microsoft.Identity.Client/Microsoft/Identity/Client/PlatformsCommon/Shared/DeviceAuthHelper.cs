using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001EF RID: 495
	internal class DeviceAuthHelper
	{
		// Token: 0x06001539 RID: 5433 RVA: 0x00046AA4 File Offset: 0x00044CA4
		public static IDictionary<string, string> ParseChallengeData(HttpResponseHeaders responseHeaders)
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			string text = responseHeaders.GetValues("WWW-Authenticate").SingleOrDefault<string>();
			text = ((text != null) ? text.Substring("PKeyAuth".Length + 1) : null);
			if (string.IsNullOrEmpty(text))
			{
				return dictionary;
			}
			foreach (string text2 in CoreHelpers.SplitWithQuotes(text, ','))
			{
				IReadOnlyList<string> readOnlyList = CoreHelpers.SplitWithQuotes(text2, '=');
				if (readOnlyList.Count == 2)
				{
					dictionary.Add(readOnlyList[0].Trim(), readOnlyList[1].Trim().Replace("\"", ""));
				}
			}
			return dictionary;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00046B64 File Offset: 0x00044D64
		public static bool IsDeviceAuthChallenge(HttpResponseHeaders responseHeaders)
		{
			return responseHeaders != null && responseHeaders.Contains("WWW-Authenticate") && responseHeaders.GetValues("WWW-Authenticate").First<string>().StartsWith("PKeyAuth", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00046B94 File Offset: 0x00044D94
		public static string GetBypassChallengeResponse(HttpResponseHeaders responseHeaders)
		{
			IDictionary<string, string> dictionary = DeviceAuthHelper.ParseChallengeData(responseHeaders);
			return string.Format(CultureInfo.InvariantCulture, "PKeyAuth Context=\"{0}\",Version=\"{1}\"", dictionary["Context"], dictionary["Version"]);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00046BCD File Offset: 0x00044DCD
		public static string GetBypassChallengeResponse(Dictionary<string, string> response)
		{
			return string.Format(CultureInfo.InvariantCulture, "PKeyAuth Context=\"{0}\",Version=\"{1}\"", response["Context"], response["Version"]);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00046BF4 File Offset: 0x00044DF4
		public static bool CanOSPerformPKeyAuth()
		{
			bool flag;
			try
			{
				if (!DesktopOsHelper.IsWindows())
				{
					flag = false;
				}
				else
				{
					flag = !DesktopOsHelper.IsWin10OrServerEquivalent();
				}
			}
			catch (DllNotFoundException)
			{
				flag = false;
			}
			return flag;
		}
	}
}
