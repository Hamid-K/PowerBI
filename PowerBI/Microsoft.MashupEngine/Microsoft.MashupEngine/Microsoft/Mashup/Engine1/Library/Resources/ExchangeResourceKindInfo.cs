using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Exchange;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x0200051E RID: 1310
	internal class ExchangeResourceKindInfo : ResourceKindInfo
	{
		// Token: 0x06002A3A RID: 10810 RVA: 0x0007E72C File Offset: 0x0007C92C
		public ExchangeResourceKindInfo()
		{
			string text = "Exchange";
			string text2 = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			AuthenticationInfo[] array = new AuthenticationInfo[2];
			array[0] = new ExchangeAuthenticationInfo();
			int num = 1;
			AadAuthenticationInfo aadAuthenticationInfo = new AadAuthenticationInfo();
			aadAuthenticationInfo.ClientApplicationType = OAuthClientApplicationType.Required;
			aadAuthenticationInfo.ProviderFactory = new OAuthFactory((OAuthServices services, string url) => AadOAuthProvider.CreateResourceForUrl(services, ExchangeResourceKindInfo.GetExchangeEwsEndpoint(services, url), ".default", null), (OAuthServices services, OAuthClientApplication app, string url) => new AadOAuthProvider(services, app, ExchangeResourceKindInfo.GetExchangeEwsEndpoint(services, url), null, ".default"));
			array[num] = aadAuthenticationInfo;
			base..ctor(text, text2, flag, flag2, flag3, flag4, flag5, flag6, array, null, new QueryPermissionChallengeType[] { QueryPermissionChallengeType.EvaluateExchangeRedirectUnpermitted }, null, new DataSourceLocationFactory[] { EwsDataSourceLocation.Factory });
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x0007E7CC File Offset: 0x0007C9CC
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			MailAddress mailAddress = null;
			if (resourcePath != "Exchange")
			{
				try
				{
					mailAddress = new MailAddress(resourcePath);
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
					errorMessage = Strings.InvalidEmailAddress;
					resource = null;
					return false;
				}
			}
			string text;
			if (mailAddress == null)
			{
				text = "Exchange";
			}
			else
			{
				text = mailAddress.User + "@" + mailAddress.Host.ToLowerInvariant();
			}
			errorMessage = null;
			resource = new Resource(base.Kind, text, resourcePath);
			return true;
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x0007E85C File Offset: 0x0007CA5C
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			MailAddress mailAddress;
			try
			{
				mailAddress = new MailAddress(resourcePath);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				hostName = null;
				return false;
			}
			hostName = mailAddress.Host;
			return true;
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x0007E8A0 File Offset: 0x0007CAA0
		private static string GetExchangeEwsEndpoint(OAuthServices services, string email)
		{
			string text = null;
			string exchangeEwsEndpoint = ExchangeHelper.GetExchangeEwsEndpoint(new Func<Uri, WebRequest>(services.CreateRequest), email, "https://outlook.office365.com/EWS/Exchange.asmx", out text);
			if (text != null)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["Email"] = email;
				dictionary["Error"] = text;
				services.WriteTrace("ExchangeResourceKindInfo/GetExchangeEwsEndpoint", TraceEventType.Warning, dictionary, true);
			}
			return exchangeEwsEndpoint;
		}
	}
}
