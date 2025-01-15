using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Shims.Json;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BE3 RID: 3043
	internal static class ExchangeHelper
	{
		// Token: 0x060052EE RID: 21230 RVA: 0x001186F4 File Offset: 0x001168F4
		public static bool IsEmailAddressValid(string emailAddress)
		{
			bool flag;
			try
			{
				new MailAddress(emailAddress);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x00118734 File Offset: 0x00116934
		public static IHostProgress GetHostProgress(IEngineHost host, string label)
		{
			return ProgressService.GetHostProgress(host, "Exchange", label);
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x00118744 File Offset: 0x00116944
		private static bool IsValidServerAddress(string serverAddress)
		{
			Uri uri;
			return Uri.TryCreate(serverAddress, UriKind.Absolute, out uri);
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x0011875C File Offset: 0x0011695C
		public static ExchangeCatalog GetExchangeCatalog(ExchangeVersion exchangeVersion, string exchangeView)
		{
			string text;
			if (!(exchangeView == "Mail"))
			{
				if (!(exchangeView == "Calendar"))
				{
					if (!(exchangeView == "People"))
					{
						if (!(exchangeView == "Tasks"))
						{
							if (!(exchangeView == "Meeting Requests"))
							{
								text = "";
							}
							else
							{
								text = "IPM.Schedule.Meeting.Request";
							}
						}
						else
						{
							text = "IPM.Task";
						}
					}
					else
					{
						text = "IPM.Contact";
					}
				}
				else
				{
					text = "IPM.Appointment";
				}
			}
			else
			{
				text = "IPM.Note";
			}
			ExchangeCatalog exchangeCatalog;
			if (ExchangeCatalogFactory.TryGetCatalog(exchangeVersion, text, out exchangeCatalog))
			{
				return exchangeCatalog;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x001187EC File Offset: 0x001169EC
		public static IResourceCredential GetCredential(IEngineHost engineHost, IResource resource, ResourceCredentialCollection credentials, out ExchangeCredentialAdornment settings)
		{
			if (credentials.Count > 0)
			{
				if (credentials.Count > 1)
				{
					settings = credentials[1] as ExchangeCredentialAdornment;
					if (settings == null)
					{
						settings = new ExchangeCredentialAdornment();
					}
					else if (!string.IsNullOrEmpty(settings.EmailAddress) && !ExchangeHelper.IsEmailAddressValid(settings.EmailAddress))
					{
						throw ExchangeExceptions.NewInvalidEmailAddressException(engineHost, settings.EmailAddress ?? string.Empty, resource);
					}
				}
				else
				{
					settings = new ExchangeCredentialAdornment();
				}
				return credentials[0];
			}
			throw DataSourceException.NewInvalidCredentialsError(engineHost, resource, null, null, null);
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x00118874 File Offset: 0x00116A74
		public static void InitializeExchangeService(IResourceCredential credential, ExchangeServiceBase service)
		{
			Action<object> testHook = ExchangeHelper.TestHook;
			if (testHook != null)
			{
				testHook(service);
			}
			if (credential is WindowsCredential)
			{
				service.UseDefaultCredentials = true;
				return;
			}
			OAuthCredential oauthCredential = credential as OAuthCredential;
			if (oauthCredential != null)
			{
				service.Credentials = new OAuthCredentials(oauthCredential.AccessTokenForResource("https://outlook.office365.com/EWS/Exchange.asmx"));
				return;
			}
			BasicAuthCredential basicAuthCredential = (BasicAuthCredential)credential;
			service.Credentials = new NetworkCredential(basicAuthCredential.Username, basicAuthCredential.Password);
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x001188E8 File Offset: 0x00116AE8
		public static bool TryGetSplitFolderPath(string folderPath, out string[] folderPathArray)
		{
			if (string.IsNullOrEmpty(folderPath))
			{
				folderPathArray = null;
				return false;
			}
			folderPathArray = folderPath.TrimStart(new char[] { "\\"[0] }).Split(new char[] { "\\"[0] });
			return true;
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x00118938 File Offset: 0x00116B38
		public static string GetAndSplitFolderPathByBOM(string folderPath)
		{
			return Encoding.Unicode.GetString(ExchangeHelper.HexStringToByteArray(BitConverter.ToString(Encoding.Unicode.GetBytes(folderPath)).Replace("FE-FF", "5C-00").Replace("-", ""))) + "\\";
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x0011898C File Offset: 0x00116B8C
		private static byte[] HexStringToByteArray(string hexString)
		{
			byte[] array = new byte[hexString.Length / 2];
			for (int i = 0; i < hexString.Length; i += 2)
			{
				array[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
			}
			return array;
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x001189CD File Offset: 0x00116BCD
		public static string GetFolderPath(List<string> input)
		{
			return string.Join("\\", input.ToArray());
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x001189E0 File Offset: 0x00116BE0
		public static Type GetEnumerableType(Type type)
		{
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					return type2.GetGenericArguments()[0];
				}
			}
			return null;
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x00118A30 File Offset: 0x00116C30
		public static string GetFolderPath(Folder folder)
		{
			string text;
			if (!folder.TryGetProperty<string>(ExchangeHelper.PR_Folder_Path, out text))
			{
				return null;
			}
			return ExchangeHelper.GetAndSplitFolderPathByBOM(text);
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x00118A54 File Offset: 0x00116C54
		public static string GetExchangeEwsEndpoint(string email, string defaultValue, out string error)
		{
			return ExchangeHelper.GetExchangeEwsEndpoint(new Func<Uri, WebRequest>(WebRequest.Create), email, defaultValue, out error);
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x00118A6C File Offset: 0x00116C6C
		public static string GetExchangeEwsEndpoint(Func<Uri, WebRequest> createRequest, string email, string defaultValue, out string error)
		{
			error = null;
			Uri uri = new Uri(string.Format(CultureInfo.CurrentCulture, "{0}/{1}?Protocol=EWS", "https://outlook.office365.com/autodiscover/autodiscover.json/v1.0", email));
			WebRequest webRequest = createRequest(uri);
			if (webRequest is HttpWebRequest)
			{
				((HttpWebRequest)webRequest).AllowAutoRedirect = true;
				((HttpWebRequest)webRequest).UserAgent = "Microsoft.Data.Mashup (https://go.microsoft.com/fwlink/?LinkID=304225)";
			}
			else if (webRequest is MashupHttpWebRequest)
			{
				((MashupHttpWebRequest)webRequest).AllowAutoRedirect = true;
				((MashupHttpWebRequest)webRequest).UserAgent = "Microsoft.Data.Mashup (https://go.microsoft.com/fwlink/?LinkID=304225)";
			}
			try
			{
				using (WebResponse response = webRequest.GetResponse())
				{
					using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
					{
						Dictionary<string, object> dictionary = Json.DeserializeObject<Dictionary<string, object>>(streamReader.ReadToEnd());
						object obj;
						object obj2;
						if (dictionary.TryGetValue("Url", out obj) && dictionary.TryGetValue("Protocol", out obj2))
						{
							string text = obj as string;
							string text2 = obj2 as string;
							if (text2 != null && text2.Equals("EWS", StringComparison.CurrentCulture) && text != null)
							{
								return text;
							}
						}
					}
				}
			}
			catch (WebException ex)
			{
				error = ex.Message;
			}
			return defaultValue;
		}

		// Token: 0x04002DC4 RID: 11716
		public const string FolderPathDelimiter = "\\";

		// Token: 0x04002DC5 RID: 11717
		public static readonly ExtendedPropertyDefinition PR_Folder_Path = new ExtendedPropertyDefinition(26293, MapiPropertyType.String);

		// Token: 0x04002DC6 RID: 11718
		public static readonly ExtendedPropertyDefinition PR_Html_Body = new ExtendedPropertyDefinition(4115, MapiPropertyType.Binary);

		// Token: 0x04002DC7 RID: 11719
		public const string Mail = "Mail";

		// Token: 0x04002DC8 RID: 11720
		public const string Calendar = "Calendar";

		// Token: 0x04002DC9 RID: 11721
		public const string People = "People";

		// Token: 0x04002DCA RID: 11722
		public const string Tasks = "Tasks";

		// Token: 0x04002DCB RID: 11723
		public const string MeetingRequests = "Meeting Requests";

		// Token: 0x04002DCC RID: 11724
		public const string Office365Domain = "outlook.office365.com";

		// Token: 0x04002DCD RID: 11725
		public const string Office365Url = "https://outlook.office365.com/EWS/Exchange.asmx";

		// Token: 0x04002DCE RID: 11726
		public const string OfficeAutoDiscoverUrl = "https://outlook.office365.com/autodiscover/autodiscover.json/v1.0";

		// Token: 0x04002DCF RID: 11727
		public static readonly string[] Views = new string[] { "Mail", "Calendar", "People", "Tasks", "Meeting Requests" };

		// Token: 0x04002DD0 RID: 11728
		internal static Action<object> TestHook;
	}
}
