using System;
using System.Diagnostics;
using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BC4 RID: 3012
	internal class EngineAutodiscoverService
	{
		// Token: 0x06005226 RID: 21030 RVA: 0x00115110 File Offset: 0x00113310
		public EngineAutodiscoverService(IEngineHost host, IResource resource, IResourceCredential credential, ExchangeCredentialAdornment settings, string credentialHash, string mailbox)
		{
			this.host = host;
			this.resource = resource;
			this.credential = credential;
			this.settings = settings;
			this.credentialHash = credentialHash;
			this.mailbox = mailbox;
			this.credentialService = this.host.QueryService<ICredentialService>();
			this.ewsEndpoint = this.GetEwsEndpoint();
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x00115170 File Offset: 0x00113370
		public ExchangeUserSettings GetUserSettings()
		{
			if (this.settings.EwsUrl != null && this.settings.EwsSupportedSchema != null)
			{
				return new ExchangeUserSettings(this.settings.EwsUrl, (ExchangeVersion)Enum.Parse(typeof(ExchangeVersion), this.settings.EwsSupportedSchema));
			}
			OAuthCredential oauthCredential = this.credential as OAuthCredential;
			if (oauthCredential != null)
			{
				this.credential = oauthCredential.RefreshTokenAsNeeded(this.host, this.resource, false);
				return new ExchangeUserSettings(this.ewsEndpoint, ExchangeVersion.Exchange2013);
			}
			ExchangeUserSettings userSettingsThroughAutoDiscover = this.GetUserSettingsThroughAutoDiscover();
			this.credentialService.UpdateExchangeCredential(this.resource, new ResourceCredentialCollection(this.resource, new IResourceCredential[]
			{
				this.credential,
				new ExchangeCredentialAdornment(userSettingsThroughAutoDiscover.EwsUrl, userSettingsThroughAutoDiscover.ExchangeVersion.ToString(), this.settings.EmailAddress)
			}));
			return userSettingsThroughAutoDiscover;
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x0011525C File Offset: 0x0011345C
		private ExchangeUserSettings GetUserSettingsThroughAutoDiscover()
		{
			AutodiscoverService autodiscoverService = new AutodiscoverService();
			ExchangeHelper.InitializeExchangeService(this.credential, autodiscoverService);
			autodiscoverService.RedirectionUrlValidationCallback = new AutodiscoverRedirectionUrlValidationCallback(this.ValidateHttpsUrl);
			ExchangeTracingService.EnableTracing(autodiscoverService, this.host, SourceLevels.Information, this.resource);
			for (int i = 0; i < 10; i++)
			{
				autodiscoverService.EnableScpLookup = i == 0;
				GetUserSettingsResponse getUserSettingsResponse = this.TraceAndGetUserSettings(autodiscoverService);
				if (getUserSettingsResponse != null)
				{
					if (getUserSettingsResponse.ErrorCode == AutodiscoverErrorCode.NoError)
					{
						bool? isExternal = autodiscoverService.IsExternal;
						bool flag = true;
						bool flag2 = ((isExternal.GetValueOrDefault() == flag) & (isExternal != null)) || (autodiscoverService.EnableScpLookup && autodiscoverService.Domain == "outlook.office365.com");
						return this.TraceAndGetUserSettingsFromResponse(getUserSettingsResponse, flag2);
					}
					if (getUserSettingsResponse.ErrorCode != AutodiscoverErrorCode.RedirectUrl)
					{
						throw ExchangeExceptions.NewAutodiscoverServiceFailedException(this.host, getUserSettingsResponse.ErrorMessage, this.resource);
					}
					autodiscoverService.Url = new Uri(getUserSettingsResponse.RedirectTarget);
				}
			}
			throw ExchangeExceptions.NewAutodiscoverServiceFailedException(this.host, null, this.resource);
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x00115364 File Offset: 0x00113564
		private string GetEwsEndpoint()
		{
			string text2;
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Exchange/GetEwsEndpoint", TraceEventType.Information, this.resource))
			{
				string text = null;
				string exchangeEwsEndpoint = ExchangeHelper.GetExchangeEwsEndpoint(this.resource.Path, "https://outlook.office365.com/EWS/Exchange.asmx", out text);
				if (text != null)
				{
					hostTrace.Add("Error", text, false);
				}
				text2 = exchangeEwsEndpoint;
			}
			return text2;
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x001153D0 File Offset: 0x001135D0
		private ExchangeVersion GetServerVersionFromUserSetting(GetUserSettingsResponse response)
		{
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Exchange/GetServerVersionFromUserSettings", TraceEventType.Information, this.resource))
			{
				string text;
				if (response.TryGetSettingValue<string>(UserSettingName.EwsSupportedSchemas, out text) && text != null)
				{
					hostTrace.Add("EwsSupportedSchemas", text, false);
					string[] array = text.Split(new char[] { ',' });
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = array[i].Trim();
					}
					Array.Sort<string>(array);
					if (array.Length != 0)
					{
						for (int j = array.Length - 1; j >= 0; j--)
						{
							string text2 = array[j].Trim();
							if (Enum.IsDefined(typeof(ExchangeVersion), text2))
							{
								hostTrace.Add("SelectedEwsSchema", text2, false);
								return (ExchangeVersion)Enum.Parse(typeof(ExchangeVersion), text2);
							}
						}
					}
				}
				throw ExchangeExceptions.NewAutodiscoverServiceFailedException(this.host, Strings.Resource_AutoDiscoverService_GetServerVersion_Failed, this.resource);
			}
			ExchangeVersion exchangeVersion;
			return exchangeVersion;
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x001154E0 File Offset: 0x001136E0
		private string GetEwsUrlFromUserSetting(GetUserSettingsResponse response, bool isExternal)
		{
			string text2;
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Exchange/GetEwsUrlFromUserSettings", TraceEventType.Information, this.resource))
			{
				string text;
				if (isExternal && response.TryGetSettingValue<string>(UserSettingName.ExternalEwsUrl, out text))
				{
					hostTrace.Add("ExternalEwsUrl", text, true);
					this.ValidateHttpsUrl(text);
					text2 = text;
				}
				else
				{
					if (!response.TryGetSettingValue<string>(UserSettingName.InternalEwsUrl, out text))
					{
						throw ExchangeExceptions.NewAutodiscoverServiceFailedException(this.host, Strings.Resource_AutoDiscoverService_GetEwsUrl_Failed, this.resource);
					}
					hostTrace.Add("InternalEwsUrl", text, true);
					this.ValidateHttpsUrl(text);
					text2 = text;
				}
			}
			return text2;
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x0011558C File Offset: 0x0011378C
		private ExchangeUserSettings TraceAndGetUserSettingsFromResponse(GetUserSettingsResponse response, bool isExternal)
		{
			ExchangeUserSettings exchangeUserSettings;
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Exchange/GetUserSettingsResponse", TraceEventType.Information, this.resource))
			{
				ExchangeVersion serverVersionFromUserSetting = this.GetServerVersionFromUserSetting(response);
				hostTrace.Add("TargetExchangeVersion", serverVersionFromUserSetting.ToString(), false);
				string ewsUrlFromUserSetting = this.GetEwsUrlFromUserSetting(response, isExternal);
				hostTrace.Add("EwsServerAddress", ewsUrlFromUserSetting, true);
				exchangeUserSettings = new ExchangeUserSettings(ewsUrlFromUserSetting, serverVersionFromUserSetting);
			}
			return exchangeUserSettings;
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x00115610 File Offset: 0x00113810
		private GetUserSettingsResponse TraceAndGetUserSettings(AutodiscoverService service)
		{
			GetUserSettingsResponse getUserSettingsResponse;
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Exchange/GetUserSettings", TraceEventType.Information, this.resource))
			{
				try
				{
					hostTrace.Add("EmailAddress", this.mailbox, true);
					getUserSettingsResponse = service.GetUserSettings(this.mailbox, new UserSettingName[]
					{
						UserSettingName.EwsSupportedSchemas,
						UserSettingName.ExternalEwsUrl,
						UserSettingName.InternalEwsUrl
					});
				}
				catch (AutodiscoverRemoteException ex)
				{
					hostTrace.Add(ex, true);
					throw DataSourceException.NewDataSourceError<Message0>(this.host, Strings.Resource_AutoDiscoverService_Failed, this.resource, "Message", TextValue.New(ex.Message), TypeValue.Text, null);
				}
				catch (AutodiscoverLocalException ex2)
				{
					hostTrace.Add(ex2, true);
					throw ExchangeExceptions.NewAutodiscoverServiceFailedException(this.host, ex2.Message, this.resource);
				}
				catch (UriFormatException ex3)
				{
					hostTrace.Add(ex3, true);
					throw ExchangeExceptions.NewAutodiscoverServiceFailedException(this.host, ex3.Message, this.resource);
				}
				catch (ServiceValidationException ex4)
				{
					hostTrace.Add(ex4, true);
					throw ExchangeExceptions.NewAutodiscoverServiceFailedException(this.host, ex4.Message, this.resource);
				}
				catch (ServiceXmlDeserializationException ex5)
				{
					hostTrace.Add(ex5, true);
					throw ExchangeExceptions.NewExchangeDeserializationException(this.host, ex5, this.resource);
				}
				catch (ServiceRequestException ex6)
				{
					hostTrace.Add(ex6, true);
					throw ExchangeExceptions.NewExchangeServiceRequestException(this.host, ex6, this.resource);
				}
				catch (ArgumentException ex7)
				{
					hostTrace.Add(ex7, true);
					if (!service.EnableScpLookup)
					{
						throw DataSourceException.NewDataSourceError<Message0>(this.host, Strings.Resource_AutoDiscoverService_Failed, this.resource, "Message", TextValue.New(ex7.Message), TypeValue.Text, null);
					}
					getUserSettingsResponse = null;
				}
			}
			return getUserSettingsResponse;
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x0011585C File Offset: 0x00113A5C
		private bool ValidateHttpsUrl(string redirectionUrl)
		{
			if (EngineAutodiscoverService.AlwaysAllowRedirect)
			{
				return true;
			}
			if (EngineAutodiscoverService.IsHttps(redirectionUrl))
			{
				HostResourceQueryPermissionService.VerifyQueryPermission(this.host, this.resource, QueryPermissionChallengeType.EvaluateExchangeRedirectUnpermitted, redirectionUrl);
				return true;
			}
			throw DataSourceException.NewDataSourceError<Message0>(this.host, Strings.Resource_AutoDiscoverService_Https_Failed, this.resource, null, null);
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x001158AD File Offset: 0x00113AAD
		private static bool IsHttps(string url)
		{
			return new Uri(url).Scheme == Uri.UriSchemeHttps;
		}

		// Token: 0x04002D29 RID: 11561
		public static bool AlwaysAllowRedirect;

		// Token: 0x04002D2A RID: 11562
		private const int MaxHops = 10;

		// Token: 0x04002D2B RID: 11563
		private readonly IEngineHost host;

		// Token: 0x04002D2C RID: 11564
		private readonly IResource resource;

		// Token: 0x04002D2D RID: 11565
		private readonly ExchangeCredentialAdornment settings;

		// Token: 0x04002D2E RID: 11566
		private readonly string credentialHash;

		// Token: 0x04002D2F RID: 11567
		private readonly string ewsEndpoint;

		// Token: 0x04002D30 RID: 11568
		private readonly string mailbox;

		// Token: 0x04002D31 RID: 11569
		private readonly ICredentialService credentialService;

		// Token: 0x04002D32 RID: 11570
		private IResourceCredential credential;
	}
}
