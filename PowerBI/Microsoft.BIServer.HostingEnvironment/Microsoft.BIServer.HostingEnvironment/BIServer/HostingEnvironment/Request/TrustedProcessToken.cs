using System;
using System.Security.Principal;
using System.Threading;
using Microsoft.BIServer.HostingEnvironment.Exceptions;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;

namespace Microsoft.BIServer.HostingEnvironment.Request
{
	// Token: 0x0200002B RID: 43
	public static class TrustedProcessToken
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00004D56 File Offset: 0x00002F56
		public static DateTime NowTime()
		{
			return DateTime.UtcNow;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004D60 File Offset: 0x00002F60
		public static long NowFileTime()
		{
			return TrustedProcessToken.NowTime().ToFileTime();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004D7A File Offset: 0x00002F7A
		public static DateTime FromFileTime(long fileTime)
		{
			return DateTime.FromFileTimeUtc(fileTime);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004D82 File Offset: 0x00002F82
		public static string CreateToken()
		{
			return TrustedProcessToken.CreateToken(Thread.CurrentPrincipal, TrustedProcessToken.NowFileTime());
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004D93 File Offset: 0x00002F93
		public static string CreateToken(IPrincipal principal)
		{
			return TrustedProcessToken.CreateToken(principal, TrustedProcessToken.NowFileTime());
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public static string CreateToken(IPrincipal principal, long fileTime)
		{
			string text = string.Format("{0}|{1}", principal.Identity.Name, fileTime);
			return HostingState.Current.CatalogCrypto.EncryptToString(text);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004DD9 File Offset: 0x00002FD9
		public static void ValididateTokenOrException(string token)
		{
			TrustedProcessToken.ValidateTokenOrException(token, Thread.CurrentPrincipal);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public static void ValidateTokenOrException(string token, IPrincipal userPrincipal)
		{
			DateTime dateTime = TrustedProcessToken.NowTime();
			if (string.IsNullOrEmpty(token))
			{
				throw new InvalidTrustedProcessTokenException("null token", Array.Empty<object>());
			}
			string text;
			try
			{
				text = HostingState.Current.CatalogCrypto.DecryptToString(token);
			}
			catch (Exception ex)
			{
				Logger.Debug(ex, "Invalid TrustedProcessToken", Array.Empty<object>());
				throw new InvalidTrustedProcessTokenException(ex, "failed decryption", Array.Empty<object>());
			}
			string[] array = text.Split(new char[] { '|' });
			if (array.Length < 2)
			{
				throw new InvalidTrustedProcessTokenException("wrong number of parameters [{0}]", new object[] { text });
			}
			string text2 = array[0];
			string name = userPrincipal.Identity.Name;
			if (string.IsNullOrEmpty(text2) || text2 != name)
			{
				throw new InvalidTrustedProcessTokenException("wrong user [{0}], expecting[{1}] in token {2}", new object[] { text2, name, text });
			}
			long num;
			if (!long.TryParse(array[1], out num))
			{
				throw new InvalidTrustedProcessTokenException("Bad timestamp [{0}]", new object[] { text });
			}
			DateTime dateTime2 = TrustedProcessToken.FromFileTime(num).Add(TrustedProcessToken.RequestTimeout);
			if (dateTime2 < dateTime)
			{
				TimeSpan timeSpan = dateTime.Subtract(dateTime2);
				throw new TrustedProcessTokenExpiredException("Expired by {0}", new object[] { timeSpan });
			}
		}

		// Token: 0x0400008F RID: 143
		public static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds((double)StaticConfig.Current.GetIntOrDefault("TrustedProcessTimeoutSeconds", 10));
	}
}
