using System;
using System.Globalization;
using System.Text;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.Identity.Client.PlatformsCommon.Shared;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Kerberos
{
	// Token: 0x02000229 RID: 553
	public static class KerberosSupplementalTicketManager
	{
		// Token: 0x060016B0 RID: 5808 RVA: 0x0004B3C4 File Offset: 0x000495C4
		public static KerberosSupplementalTicket FromIdToken(string idToken)
		{
			if (string.IsNullOrEmpty(idToken) || idToken.Length < 128)
			{
				return null;
			}
			string[] array = idToken.Split(new char[] { '.' });
			if (array.Length != 3)
			{
				return null;
			}
			byte[] array2 = Base64UrlHelpers.DecodeBytes(array[1]);
			string @string = Encoding.UTF8.GetString(array2);
			if (string.IsNullOrEmpty(@string))
			{
				return null;
			}
			JToken jtoken;
			if (!JsonHelper.TryGetValue(JsonHelper.ParseIntoJsonObject(@string), "xms_as_rep", out jtoken))
			{
				return null;
			}
			return JsonHelper.DeserializeFromJson<KerberosSupplementalTicket>(JsonHelper.GetValue<string>(jtoken));
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0004B442 File Offset: 0x00049642
		public static void SaveToWindowsTicketCache(KerberosSupplementalTicket ticket)
		{
			KerberosSupplementalTicketManager.SaveToWindowsTicketCache(ticket, 0L);
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0004B44C File Offset: 0x0004964C
		public static void SaveToWindowsTicketCache(KerberosSupplementalTicket ticket, long logonId)
		{
			if (!DesktopOsHelper.IsWindows())
			{
				throw new PlatformNotSupportedException("Ticket Cache interface is not supported on this OS. It is supported on Windows only.");
			}
			if (ticket == null || string.IsNullOrEmpty(ticket.KerberosMessageBuffer))
			{
				throw new ArgumentException("Kerberos Ticket information is not valid");
			}
			using (TicketCacheWriter ticketCacheWriter = TicketCacheWriter.Connect("Kerberos"))
			{
				byte[] array = Convert.FromBase64String(ticket.KerberosMessageBuffer);
				ticketCacheWriter.ImportCredential(array, logonId);
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0004B4C4 File Offset: 0x000496C4
		public static byte[] GetKerberosTicketFromWindowsTicketCache(string servicePrincipalName)
		{
			return KerberosSupplementalTicketManager.GetKerberosTicketFromWindowsTicketCache(servicePrincipalName, 0L);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0004B4D0 File Offset: 0x000496D0
		public static byte[] GetKerberosTicketFromWindowsTicketCache(string servicePrincipalName, long logonId)
		{
			if (!DesktopOsHelper.IsWindows())
			{
				throw new PlatformNotSupportedException("Ticket Cache interface is not supported on this OS. It is supported on Windows only.");
			}
			byte[] array;
			using (TicketCacheReader ticketCacheReader = new TicketCacheReader(servicePrincipalName, logonId, "Kerberos"))
			{
				array = ticketCacheReader.RequestToken();
			}
			return array;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0004B520 File Offset: 0x00049720
		public static byte[] GetKrbCred(KerberosSupplementalTicket ticket)
		{
			if (!string.IsNullOrEmpty(ticket.KerberosMessageBuffer))
			{
				return Convert.FromBase64String(ticket.KerberosMessageBuffer);
			}
			return null;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0004B53C File Offset: 0x0004973C
		internal static string GetKerberosTicketClaim(string servicePrincipalName, KerberosTicketContainer ticketContainer)
		{
			if (string.IsNullOrEmpty(servicePrincipalName))
			{
				return string.Empty;
			}
			if (ticketContainer == KerberosTicketContainer.IdToken)
			{
				return string.Format(CultureInfo.InvariantCulture, "{{\"id_token\": {{ \"xms_as_rep\":{{\"essential\":\"false\",\"value\":\"{0}\"}} }} }}", servicePrincipalName);
			}
			return string.Format(CultureInfo.InvariantCulture, "{{\"access_token\": {{ \"xms_as_rep\":{{\"essential\":\"false\",\"value\":\"{0}\"}} }} }}", servicePrincipalName);
		}

		// Token: 0x040009B6 RID: 2486
		private const int DefaultLogonId = 0;

		// Token: 0x040009B7 RID: 2487
		private const string KerberosClaimType = "xms_as_rep";

		// Token: 0x040009B8 RID: 2488
		private const string IdTokenAsRepTemplate = "{{\"id_token\": {{ \"xms_as_rep\":{{\"essential\":\"false\",\"value\":\"{0}\"}} }} }}";

		// Token: 0x040009B9 RID: 2489
		private const string AccessTokenAsRepTemplate = "{{\"access_token\": {{ \"xms_as_rep\":{{\"essential\":\"false\",\"value\":\"{0}\"}} }} }}";
	}
}
