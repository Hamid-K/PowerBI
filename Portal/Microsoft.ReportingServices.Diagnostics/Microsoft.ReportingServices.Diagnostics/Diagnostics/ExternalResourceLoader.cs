using System;
using System.IO;
using System.Net;
using Microsoft.Win32;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200004A RID: 74
	internal static class ExternalResourceLoader
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000B788 File Offset: 0x00009988
		public static byte[] GetExternalResource(string resourceUrl, bool impersonate, string surrogateUser, string surrogatePassword, string surrogateDomain, int webTimeout, int maxResourceSizeBytes, ExternalResourceAbortHelper abortHelper, out string mimeType, out bool resourceExceededMaxSize)
		{
			byte[] array = null;
			mimeType = null;
			Uri uri = new Uri(resourceUrl);
			WebRequest webRequest;
			if (uri.IsFile)
			{
				webRequest = (FileWebRequest)WebRequest.Create(uri);
			}
			else
			{
				webRequest = (HttpWebRequest)WebRequest.Create(uri);
			}
			int num = 600000;
			if (webTimeout > 0 && webTimeout < 2147483)
			{
				webRequest.Timeout = webTimeout * 1000;
			}
			else
			{
				webRequest.Timeout = num;
			}
			if (surrogateUser != null)
			{
				webRequest.Credentials = new NetworkCredential(surrogateUser, surrogatePassword, surrogateDomain);
			}
			else if (impersonate)
			{
				webRequest.Credentials = CredentialCache.DefaultCredentials;
			}
			else
			{
				webRequest.Credentials = null;
			}
			resourceExceededMaxSize = false;
			using (WebResponse webResponse = ExternalResourceLoader.RequestExternalResource(webRequest, abortHelper))
			{
				mimeType = webResponse.ContentType;
				using (Stream responseStream = webResponse.GetResponseStream())
				{
					if (maxResourceSizeBytes == ExternalResourceLoader.MaxResourceSizeUnlimited)
					{
						array = StreamSupport.ReadToEndNotUsingLength(responseStream, 1024);
					}
					else
					{
						array = StreamSupport.ReadToEndNotUsingLength(responseStream, 1024, maxResourceSizeBytes, out resourceExceededMaxSize);
					}
				}
			}
			if (uri.IsFile && !resourceExceededMaxSize)
			{
				string text = Path.GetExtension(uri.LocalPath).ToUpperInvariant();
				if (text != null && text.StartsWith(".", StringComparison.Ordinal))
				{
					text = text.Substring(1);
				}
				string mimeTypeByRegistryLookup = ExternalResourceLoader.GetMimeTypeByRegistryLookup(text);
				if (mimeTypeByRegistryLookup != null)
				{
					mimeType = mimeTypeByRegistryLookup;
				}
			}
			return array;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000B8EC File Offset: 0x00009AEC
		public static bool IsValidResourceSize(int maxResourceBytes, byte[] contents)
		{
			return maxResourceBytes == ExternalResourceLoader.MaxResourceSizeUnlimited || contents == null || contents.Length <= maxResourceBytes;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B904 File Offset: 0x00009B04
		private static WebResponse RequestExternalResource(WebRequest request, ExternalResourceAbortHelper abortHelper)
		{
			if (abortHelper == null)
			{
				return request.GetResponse();
			}
			IAsyncResult asyncResult = request.BeginGetResponse(null, null);
			while (!asyncResult.AsyncWaitHandle.WaitOne(1000, false))
			{
				if (abortHelper.IsAborted)
				{
					request.Abort();
				}
			}
			return request.EndGetResponse(asyncResult);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B94E File Offset: 0x00009B4E
		public static string GetMimeTypeByRegistryLookup(string extension)
		{
			string mimeType = null;
			RevertImpersonationContext.Run(delegate
			{
				RegistryKey registryKey = null;
				try
				{
					registryKey = Registry.ClassesRoot.OpenSubKey("." + extension);
					if (registryKey != null)
					{
						object value = registryKey.GetValue("Content Type");
						if (value is string)
						{
							mimeType = (string)value;
						}
					}
				}
				catch (Exception)
				{
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
			});
			return mimeType;
		}

		// Token: 0x0400021D RID: 541
		internal static readonly int MaxResourceSizeUnlimited = -1;
	}
}
