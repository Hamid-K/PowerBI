using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000010 RID: 16
	public sealed class AutodiscoverService : ExchangeServiceBase
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003352 File Offset: 0x00002352
		private bool DefaultAutodiscoverRedirectionUrlValidationCallback(string redirectionUrl)
		{
			throw new AutodiscoverLocalException(string.Format(Strings.AutodiscoverRedirectBlocked, redirectionUrl));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000336C File Offset: 0x0000236C
		private TSettings GetLegacyUserSettingsAtUrl<TSettings>(string emailAddress, Uri url) where TSettings : ConfigurationSettingsBase, new()
		{
			base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Trying to call Autodiscover for {0} on {1}.", emailAddress, url));
			TSettings tsettings = new TSettings();
			IEwsHttpWebRequest ewsHttpWebRequest = this.PrepareHttpWebRequestForUrl(url);
			base.TraceHttpRequestHeaders(TraceFlags.AutodiscoverRequestHttpHeaders, ewsHttpWebRequest);
			using (Stream requestStream = ewsHttpWebRequest.GetRequestStream())
			{
				if (base.IsTraceEnabledFor(TraceFlags.AutodiscoverRequest))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (StreamWriter streamWriter = new StreamWriter(memoryStream))
						{
							this.WriteLegacyAutodiscoverRequest(emailAddress, tsettings, streamWriter);
							streamWriter.Flush();
							base.TraceXml(TraceFlags.AutodiscoverRequest, memoryStream);
							EwsUtilities.CopyStream(memoryStream, requestStream);
						}
						goto IL_00B4;
					}
				}
				using (StreamWriter streamWriter2 = new StreamWriter(requestStream))
				{
					this.WriteLegacyAutodiscoverRequest(emailAddress, tsettings, streamWriter2);
				}
				IL_00B4:;
			}
			TSettings tsettings2;
			using (IEwsHttpWebResponse response = ewsHttpWebRequest.GetResponse())
			{
				Uri uri;
				if (this.TryGetRedirectionResponse(response, out uri))
				{
					tsettings.MakeRedirectionResponse(uri);
					tsettings2 = tsettings;
				}
				else
				{
					using (Stream responseStream = response.GetResponseStream())
					{
						if (base.IsTraceEnabledFor(TraceFlags.AutodiscoverResponse))
						{
							using (MemoryStream memoryStream2 = new MemoryStream())
							{
								EwsUtilities.CopyStream(responseStream, memoryStream2);
								memoryStream2.Position = 0L;
								this.TraceResponse(response, memoryStream2);
								EwsXmlReader ewsXmlReader = new EwsXmlReader(memoryStream2);
								ewsXmlReader.Read(17);
								tsettings.LoadFromXml(ewsXmlReader);
								goto IL_0172;
							}
						}
						EwsXmlReader ewsXmlReader2 = new EwsXmlReader(responseStream);
						ewsXmlReader2.Read(17);
						tsettings.LoadFromXml(ewsXmlReader2);
						IL_0172:;
					}
					tsettings2 = tsettings;
				}
			}
			return tsettings2;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003564 File Offset: 0x00002564
		private void WriteLegacyAutodiscoverRequest(string emailAddress, ConfigurationSettingsBase settings, StreamWriter writer)
		{
			writer.Write(string.Format("<Autodiscover xmlns=\"{0}\">", "http://schemas.microsoft.com/exchange/autodiscover/outlook/requestschema/2006"));
			writer.Write("<Request>");
			writer.Write(string.Format("<EMailAddress>{0}</EMailAddress>", emailAddress));
			writer.Write(string.Format("<AcceptableResponseSchema>{0}</AcceptableResponseSchema>", settings.GetNamespace()));
			writer.Write("</Request>");
			writer.Write("</Autodiscover>");
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000035D0 File Offset: 0x000025D0
		private Uri GetRedirectUrl(string domainName)
		{
			string text = string.Format("http://{0}/autodiscover/autodiscover.xml", "autodiscover." + domainName);
			base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Trying to get Autodiscover redirection URL from {0}.", text));
			IEwsHttpWebRequest ewsHttpWebRequest = base.HttpWebRequestFactory.CreateRequest(new Uri(text));
			ewsHttpWebRequest.Method = "GET";
			ewsHttpWebRequest.AllowAutoRedirect = false;
			ewsHttpWebRequest.PreAuthenticate = false;
			IEwsHttpWebResponse ewsHttpWebResponse = null;
			try
			{
				ewsHttpWebResponse = ewsHttpWebRequest.GetResponse();
			}
			catch (WebException ex)
			{
				base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Request error: {0}", ex.Message));
				if (ex.Response != null)
				{
					ewsHttpWebResponse = base.HttpWebRequestFactory.CreateExceptionResponse(ex);
				}
			}
			catch (IOException ex2)
			{
				base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("I/O error: {0}", ex2.Message));
			}
			if (ewsHttpWebResponse != null)
			{
				using (ewsHttpWebResponse)
				{
					Uri uri;
					if (this.TryGetRedirectionResponse(ewsHttpWebResponse, out uri))
					{
						return uri;
					}
				}
			}
			base.TraceMessage(TraceFlags.AutodiscoverConfiguration, "No Autodiscover redirection URL was returned.");
			return null;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000036EC File Offset: 0x000026EC
		private bool TryGetRedirectionResponse(IEwsHttpWebResponse response, out Uri redirectUrl)
		{
			redirectUrl = null;
			if (AutodiscoverRequest.IsRedirectionResponse(response))
			{
				string text = response.Headers[23];
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						redirectUrl = new Uri(response.ResponseUri, text);
						Match match = AutodiscoverService.LegacyPathRegex.Match(redirectUrl.AbsolutePath);
						if (redirectUrl.Scheme == Uri.UriSchemeHttps && match.Success)
						{
							base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Redirection URL found: '{0}'", redirectUrl));
							return true;
						}
					}
					catch (UriFormatException)
					{
						base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Invalid redirection URL was returned: '{0}'", text));
						return false;
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000037A0 File Offset: 0x000027A0
		internal TSettings GetLegacyUserSettings<TSettings>(string emailAddress) where TSettings : ConfigurationSettingsBase, new()
		{
			if (this.Url != null)
			{
				Match match = AutodiscoverService.LegacyPathRegex.Match(this.Url.AbsolutePath);
				if (match.Success)
				{
					return this.GetLegacyUserSettingsAtUrl<TSettings>(emailAddress, this.Url);
				}
				Uri uri = new Uri(this.Url, "/autodiscover/autodiscover.xml");
				return this.GetLegacyUserSettingsAtUrl<TSettings>(emailAddress, uri);
			}
			else
			{
				if (!string.IsNullOrEmpty(this.Domain))
				{
					Uri uri2 = new Uri(string.Format("https://{0}/autodiscover/autodiscover.xml", this.Domain));
					return this.GetLegacyUserSettingsAtUrl<TSettings>(emailAddress, uri2);
				}
				int num = 1;
				List<string> list = new List<string>();
				return this.InternalGetLegacyUserSettings<TSettings>(emailAddress, list, ref num);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003844 File Offset: 0x00002844
		private TSettings InternalGetLegacyUserSettings<TSettings>(string emailAddress, List<string> redirectionEmailAddresses, ref int currentHop) where TSettings : ConfigurationSettingsBase, new()
		{
			string text = EwsUtilities.DomainFromEmailAddress(emailAddress);
			int num;
			List<Uri> autodiscoverServiceUrls = this.GetAutodiscoverServiceUrls(text, out num);
			if (autodiscoverServiceUrls.Count == 0)
			{
				throw new ServiceValidationException(Strings.AutodiscoverServiceRequestRequiresDomainOrUrl);
			}
			this.isExternal = new bool?(true);
			int num2 = 0;
			Exception ex = null;
			TSettings tsettings = default(TSettings);
			do
			{
				Uri uri = autodiscoverServiceUrls[num2];
				bool flag = num2 < num;
				try
				{
					tsettings = this.GetLegacyUserSettingsAtUrl<TSettings>(emailAddress, uri);
					switch (tsettings.ResponseType)
					{
					case AutodiscoverResponseType.Error:
						if (!flag)
						{
							throw new AutodiscoverRemoteException(Strings.AutodiscoverError, tsettings.Error);
						}
						base.TraceMessage(TraceFlags.AutodiscoverConfiguration, "Error returned by Autodiscover service found via SCP, treating as inconclusive.");
						ex = new AutodiscoverRemoteException(Strings.AutodiscoverError, tsettings.Error);
						num2++;
						break;
					case AutodiscoverResponseType.RedirectUrl:
						if (currentHop >= 10)
						{
							throw new AutodiscoverLocalException(Strings.MaximumRedirectionHopsExceeded);
						}
						currentHop++;
						base.TraceMessage(TraceFlags.AutodiscoverResponse, string.Format("Autodiscover service returned redirection URL '{0}'.", tsettings.RedirectTarget));
						autodiscoverServiceUrls[num2] = new Uri(tsettings.RedirectTarget);
						break;
					case AutodiscoverResponseType.RedirectAddress:
						if (currentHop < 10)
						{
							currentHop++;
							base.TraceMessage(TraceFlags.AutodiscoverResponse, string.Format("Autodiscover service returned redirection email address '{0}'.", tsettings.RedirectTarget));
							this.DisableScpLookupIfDuplicateRedirection(tsettings.RedirectTarget, redirectionEmailAddresses);
							return this.InternalGetLegacyUserSettings<TSettings>(tsettings.RedirectTarget, redirectionEmailAddresses, ref currentHop);
						}
						throw new AutodiscoverLocalException(Strings.MaximumRedirectionHopsExceeded);
					case AutodiscoverResponseType.Success:
						if (flag)
						{
							this.IsExternal = new bool?(false);
						}
						this.Url = uri;
						return tsettings;
					default:
						EwsUtilities.Assert(false, "Autodiscover.GetConfigurationSettings", "An unexpected error has occured. This code path should never be reached.");
						break;
					}
				}
				catch (WebException ex2)
				{
					if (ex2.Response != null)
					{
						IEwsHttpWebResponse ewsHttpWebResponse = base.HttpWebRequestFactory.CreateExceptionResponse(ex2);
						Uri uri2;
						if (this.TryGetRedirectionResponse(ewsHttpWebResponse, out uri2))
						{
							base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Host returned a redirection to url {0}", uri2));
							currentHop++;
							autodiscoverServiceUrls[num2] = uri2;
						}
						else
						{
							this.ProcessHttpErrorResponse(ewsHttpWebResponse, ex2);
							base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("{0} failed: {1} ({2})", this.url, ex2.GetType().Name, ex2.Message));
							num2++;
						}
					}
					else
					{
						base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("{0} failed: {1} ({2})", this.url, ex2.GetType().Name, ex2.Message));
						num2++;
					}
				}
				catch (XmlException ex3)
				{
					base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("{0} failed: XML parsing error: {1}", this.url, ex3.Message));
					num2++;
				}
				catch (IOException ex4)
				{
					base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("{0} failed: I/O error: {1}", this.url, ex4.Message));
					num2++;
				}
			}
			while (num2 < autodiscoverServiceUrls.Count);
			Uri uri3 = this.GetRedirectUrl(text);
			if (uri3 != null && this.TryLastChanceHostRedirection<TSettings>(emailAddress, uri3, out tsettings))
			{
				return tsettings;
			}
			uri3 = this.GetRedirectionUrlFromDnsSrvRecord(text);
			if (uri3 != null && this.TryLastChanceHostRedirection<TSettings>(emailAddress, uri3, out tsettings))
			{
				return tsettings;
			}
			if (ex != null)
			{
				throw ex;
			}
			throw new AutodiscoverLocalException(Strings.AutodiscoverCouldNotBeLocated);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003BE8 File Offset: 0x00002BE8
		internal Uri GetRedirectionUrlFromDnsSrvRecord(string domainName)
		{
			base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Trying to get Autodiscover host from DNS SRV record for {0}.", domainName));
			string text = this.dnsClient.FindAutodiscoverHostFromSrv(domainName);
			if (!string.IsNullOrEmpty(text))
			{
				base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Autodiscover host {0} was returned.", text));
				return new Uri(string.Format("https://{0}/autodiscover/autodiscover.xml", text));
			}
			base.TraceMessage(TraceFlags.AutodiscoverConfiguration, "No matching Autodiscover DNS SRV records were found.");
			return null;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003C54 File Offset: 0x00002C54
		private bool TryLastChanceHostRedirection<TSettings>(string emailAddress, Uri redirectionUrl, out TSettings settings) where TSettings : ConfigurationSettingsBase, new()
		{
			settings = default(TSettings);
			List<string> list = new List<string>();
			if (this.CallRedirectionUrlValidationCallback(redirectionUrl.ToString()))
			{
				for (int i = 0; i < 10; i++)
				{
					try
					{
						settings = this.GetLegacyUserSettingsAtUrl<TSettings>(emailAddress, redirectionUrl);
						switch (settings.ResponseType)
						{
						case AutodiscoverResponseType.Error:
							throw new AutodiscoverRemoteException(Strings.AutodiscoverError, settings.Error);
						case AutodiscoverResponseType.RedirectUrl:
							try
							{
								redirectionUrl = new Uri(settings.RedirectTarget);
								goto IL_0124;
							}
							catch (UriFormatException)
							{
								base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Service returned invalid redirection URL {0}", settings.RedirectTarget));
								return false;
							}
							break;
							IL_0124:
							goto IL_01DE;
						case AutodiscoverResponseType.RedirectAddress:
							this.DisableScpLookupIfDuplicateRedirection(settings.RedirectTarget, list);
							settings = this.InternalGetLegacyUserSettings<TSettings>(emailAddress, list, ref i);
							return true;
						case AutodiscoverResponseType.Success:
							return true;
						}
						string text = string.Format("Autodiscover call at {0} failed with error {1}, target {2}", redirectionUrl, settings.ResponseType, settings.RedirectTarget);
						base.TraceMessage(TraceFlags.AutodiscoverConfiguration, text);
						return false;
					}
					catch (WebException ex)
					{
						if (ex.Response != null)
						{
							IEwsHttpWebResponse ewsHttpWebResponse = base.HttpWebRequestFactory.CreateExceptionResponse(ex);
							if (this.TryGetRedirectionResponse(ewsHttpWebResponse, out redirectionUrl))
							{
								base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Host returned a redirection to url {0}", redirectionUrl));
								goto IL_01DE;
							}
							this.ProcessHttpErrorResponse(ewsHttpWebResponse, ex);
						}
						base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("{0} failed: {1} ({2})", this.url, ex.GetType().Name, ex.Message));
						return false;
					}
					catch (XmlException ex2)
					{
						base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("{0} failed: XML parsing error: {1}", redirectionUrl, ex2.Message));
						return false;
					}
					catch (IOException ex3)
					{
						base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("{0} failed: I/O error: {1}", redirectionUrl, ex3.Message));
						return false;
					}
					IL_01DE:;
				}
			}
			return false;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003EB4 File Offset: 0x00002EB4
		private void DisableScpLookupIfDuplicateRedirection(string emailAddress, List<string> redirectionEmailAddresses)
		{
			emailAddress = emailAddress.ToLowerInvariant();
			if (redirectionEmailAddresses.Contains(emailAddress))
			{
				this.EnableScpLookup = false;
				return;
			}
			redirectionEmailAddresses.Add(emailAddress);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003ED8 File Offset: 0x00002ED8
		internal GetUserSettingsResponse InternalGetLegacyUserSettings(string emailAddress, List<UserSettingName> requestedSettings)
		{
			if (base.Credentials != null && base.Credentials is WSSecurityBasedCredentials)
			{
				throw new AutodiscoverLocalException(Strings.WLIDCredentialsCannotBeUsedWithLegacyAutodiscover);
			}
			OutlookConfigurationSettings legacyUserSettings = this.GetLegacyUserSettings<OutlookConfigurationSettings>(emailAddress);
			return legacyUserSettings.ConvertSettings(emailAddress, requestedSettings);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003F1C File Offset: 0x00002F1C
		internal GetUserSettingsResponse InternalGetSoapUserSettings(string smtpAddress, List<UserSettingName> requestedSettings)
		{
			List<string> list = new List<string>();
			list.Add(smtpAddress);
			List<string> list2 = new List<string>();
			list2.Add(smtpAddress.ToLowerInvariant());
			for (int i = 0; i < 10; i++)
			{
				GetUserSettingsResponse getUserSettingsResponse = this.GetUserSettings(list, requestedSettings)[0];
				switch (getUserSettingsResponse.ErrorCode)
				{
				case AutodiscoverErrorCode.NoError:
					return getUserSettingsResponse;
				case AutodiscoverErrorCode.RedirectAddress:
					base.TraceMessage(TraceFlags.AutodiscoverResponse, string.Format("Autodiscover service returned redirection email address '{0}'.", getUserSettingsResponse.RedirectTarget));
					list.Clear();
					list.Add(getUserSettingsResponse.RedirectTarget.ToLowerInvariant());
					this.Url = null;
					this.Domain = null;
					this.DisableScpLookupIfDuplicateRedirection(getUserSettingsResponse.RedirectTarget, list2);
					break;
				case AutodiscoverErrorCode.RedirectUrl:
					base.TraceMessage(TraceFlags.AutodiscoverResponse, string.Format("Autodiscover service returned redirection URL '{0}'.", getUserSettingsResponse.RedirectTarget));
					this.Url = base.Credentials.AdjustUrl(new Uri(getUserSettingsResponse.RedirectTarget));
					break;
				default:
					return getUserSettingsResponse;
				}
			}
			throw new AutodiscoverLocalException(Strings.AutodiscoverCouldNotBeLocated);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000403C File Offset: 0x0000303C
		internal GetUserSettingsResponseCollection GetUserSettings(List<string> smtpAddresses, List<UserSettingName> settings)
		{
			EwsUtilities.ValidateParam(smtpAddresses, "smtpAddresses");
			EwsUtilities.ValidateParam(settings, "settings");
			return this.GetSettings<GetUserSettingsResponseCollection, UserSettingName>(smtpAddresses, settings, default(ExchangeVersion?), new AutodiscoverService.GetSettingsMethod<GetUserSettingsResponseCollection, UserSettingName>(this.InternalGetUserSettings), () => EwsUtilities.DomainFromEmailAddress(smtpAddresses[0]));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000040A0 File Offset: 0x000030A0
		private TGetSettingsResponseCollection GetSettings<TGetSettingsResponseCollection, TSettingName>(List<string> identities, List<TSettingName> settings, ExchangeVersion? requestedVersion, AutodiscoverService.GetSettingsMethod<TGetSettingsResponseCollection, TSettingName> getSettingsMethod, Func<string> getDomainMethod)
		{
			if (base.RequestedServerVersion < ExchangeVersion.Exchange2010)
			{
				throw new ServiceVersionException(string.Format(Strings.AutodiscoverServiceIncompatibleWithRequestVersion, ExchangeVersion.Exchange2010));
			}
			if (this.Url != null)
			{
				Uri uri = this.Url;
				TGetSettingsResponseCollection tgetSettingsResponseCollection = getSettingsMethod(identities, settings, requestedVersion, ref uri);
				this.Url = uri;
				return tgetSettingsResponseCollection;
			}
			if (!string.IsNullOrEmpty(this.Domain))
			{
				Uri autodiscoverEndpointUrl = this.GetAutodiscoverEndpointUrl(this.Domain);
				TGetSettingsResponseCollection tgetSettingsResponseCollection = getSettingsMethod(identities, settings, requestedVersion, ref autodiscoverEndpointUrl);
				this.Url = autodiscoverEndpointUrl;
				return tgetSettingsResponseCollection;
			}
			this.IsExternal = new bool?(true);
			string text = getDomainMethod.Invoke();
			int num;
			List<string> autodiscoverServiceHosts = this.GetAutodiscoverServiceHosts(text, out num);
			if (autodiscoverServiceHosts.Count == 0)
			{
				throw new ServiceValidationException(Strings.AutodiscoverServiceRequestRequiresDomainOrUrl);
			}
			Uri uri2;
			for (int i = 0; i < autodiscoverServiceHosts.Count; i++)
			{
				string text2 = autodiscoverServiceHosts[i];
				bool flag = i < num;
				if (this.TryGetAutodiscoverEndpointUrl(text2, out uri2))
				{
					try
					{
						TGetSettingsResponseCollection tgetSettingsResponseCollection = getSettingsMethod(identities, settings, requestedVersion, ref uri2);
						this.Url = uri2;
						if (flag)
						{
							this.IsExternal = new bool?(false);
						}
						return tgetSettingsResponseCollection;
					}
					catch (AutodiscoverResponseException)
					{
					}
					catch (ServiceRequestException)
					{
					}
				}
			}
			uri2 = this.GetRedirectUrl(text);
			if (uri2 != null && this.CallRedirectionUrlValidationCallback(uri2.ToString()) && this.TryGetAutodiscoverEndpointUrl(uri2.Host, out uri2))
			{
				TGetSettingsResponseCollection tgetSettingsResponseCollection = getSettingsMethod(identities, settings, requestedVersion, ref uri2);
				this.Url = uri2;
				return tgetSettingsResponseCollection;
			}
			uri2 = this.GetRedirectionUrlFromDnsSrvRecord(text);
			if (uri2 != null && this.CallRedirectionUrlValidationCallback(uri2.ToString()) && this.TryGetAutodiscoverEndpointUrl(uri2.Host, out uri2))
			{
				TGetSettingsResponseCollection tgetSettingsResponseCollection = getSettingsMethod(identities, settings, requestedVersion, ref uri2);
				this.Url = uri2;
				return tgetSettingsResponseCollection;
			}
			throw new AutodiscoverLocalException(Strings.AutodiscoverCouldNotBeLocated);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004288 File Offset: 0x00003288
		private GetUserSettingsResponseCollection InternalGetUserSettings(List<string> smtpAddresses, List<UserSettingName> settings, ExchangeVersion? requestedVersion, ref Uri autodiscoverUrl)
		{
			for (int i = 0; i < 10; i++)
			{
				GetUserSettingsResponseCollection getUserSettingsResponseCollection = new GetUserSettingsRequest(this, autodiscoverUrl)
				{
					SmtpAddresses = smtpAddresses,
					Settings = settings
				}.Execute();
				if (getUserSettingsResponseCollection.ErrorCode != AutodiscoverErrorCode.RedirectUrl || !(getUserSettingsResponseCollection.RedirectionUrl != null))
				{
					return getUserSettingsResponseCollection;
				}
				base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Request to {0} returned redirection to {1}", autodiscoverUrl.ToString(), getUserSettingsResponseCollection.RedirectionUrl));
				autodiscoverUrl = getUserSettingsResponseCollection.RedirectionUrl;
			}
			base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Maximum number of redirection hops {0} exceeded", 10));
			throw new AutodiscoverLocalException(Strings.MaximumRedirectionHopsExceeded);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004348 File Offset: 0x00003348
		internal GetDomainSettingsResponseCollection GetDomainSettings(List<string> domains, List<DomainSettingName> settings, ExchangeVersion? requestedVersion)
		{
			EwsUtilities.ValidateParam(domains, "domains");
			EwsUtilities.ValidateParam(settings, "settings");
			return this.GetSettings<GetDomainSettingsResponseCollection, DomainSettingName>(domains, settings, requestedVersion, new AutodiscoverService.GetSettingsMethod<GetDomainSettingsResponseCollection, DomainSettingName>(this.InternalGetDomainSettings), () => domains[0]);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000043A4 File Offset: 0x000033A4
		private GetDomainSettingsResponseCollection InternalGetDomainSettings(List<string> domains, List<DomainSettingName> settings, ExchangeVersion? requestedVersion, ref Uri autodiscoverUrl)
		{
			for (int i = 0; i < 10; i++)
			{
				GetDomainSettingsResponseCollection getDomainSettingsResponseCollection = new GetDomainSettingsRequest(this, autodiscoverUrl)
				{
					Domains = domains,
					Settings = settings,
					RequestedVersion = requestedVersion
				}.Execute();
				if (getDomainSettingsResponseCollection.ErrorCode != AutodiscoverErrorCode.RedirectUrl || !(getDomainSettingsResponseCollection.RedirectionUrl != null))
				{
					return getDomainSettingsResponseCollection;
				}
				autodiscoverUrl = getDomainSettingsResponseCollection.RedirectionUrl;
			}
			base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Maximum number of redirection hops {0} exceeded", 10));
			throw new AutodiscoverLocalException(Strings.MaximumRedirectionHopsExceeded);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004434 File Offset: 0x00003434
		private Uri GetAutodiscoverEndpointUrl(string host)
		{
			Uri uri;
			if (this.TryGetAutodiscoverEndpointUrl(host, out uri))
			{
				return uri;
			}
			throw new AutodiscoverLocalException(Strings.NoSoapOrWsSecurityEndpointAvailable);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004460 File Offset: 0x00003460
		private bool TryGetAutodiscoverEndpointUrl(string host, out Uri url)
		{
			url = null;
			AutodiscoverEndpoints autodiscoverEndpoints;
			if (!this.TryGetEnabledEndpointsForHost(ref host, out autodiscoverEndpoints))
			{
				base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("No Autodiscover endpoints are available for host {0}", host));
				return false;
			}
			url = new Uri(string.Format("https://{0}/autodiscover/autodiscover.svc", host));
			if ((autodiscoverEndpoints & AutodiscoverEndpoints.Soap) != AutodiscoverEndpoints.Soap && (autodiscoverEndpoints & AutodiscoverEndpoints.WsSecurity) != AutodiscoverEndpoints.WsSecurity && (autodiscoverEndpoints & AutodiscoverEndpoints.WSSecuritySymmetricKey) != AutodiscoverEndpoints.WSSecuritySymmetricKey && (autodiscoverEndpoints & AutodiscoverEndpoints.WSSecurityX509Cert) != AutodiscoverEndpoints.WSSecurityX509Cert && (autodiscoverEndpoints & AutodiscoverEndpoints.OAuth) != AutodiscoverEndpoints.OAuth)
			{
				base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("No Autodiscover endpoints are available  for host {0}", host));
				return false;
			}
			if (base.Credentials is WindowsLiveCredentials)
			{
				if ((autodiscoverEndpoints & AutodiscoverEndpoints.WsSecurity) != AutodiscoverEndpoints.WsSecurity)
				{
					base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("No Autodiscover WS-Security endpoint is available for host {0}", host));
					return false;
				}
				url = new Uri(string.Format("https://{0}/autodiscover/autodiscover.svc/wssecurity", host));
			}
			else if (base.Credentials is PartnerTokenCredentials)
			{
				if ((autodiscoverEndpoints & AutodiscoverEndpoints.WSSecuritySymmetricKey) != AutodiscoverEndpoints.WSSecuritySymmetricKey)
				{
					base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("No Autodiscover WS-Security/SymmetricKey endpoint is available for host {0}", host));
					return false;
				}
				url = new Uri(string.Format("https://{0}/autodiscover/autodiscover.svc/wssecurity/symmetrickey", host));
			}
			else if (base.Credentials is X509CertificateCredentials)
			{
				if ((autodiscoverEndpoints & AutodiscoverEndpoints.WSSecurityX509Cert) != AutodiscoverEndpoints.WSSecurityX509Cert)
				{
					base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("No Autodiscover WS-Security/X509Cert endpoint is available for host {0}", host));
					return false;
				}
				url = new Uri(string.Format("https://{0}/autodiscover/autodiscover.svc/wssecurity/x509cert", host));
			}
			else if (base.Credentials is OAuthCredentials)
			{
				url = new Uri(string.Format("https://{0}/autodiscover/autodiscover.svc", host));
			}
			return true;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000045BC File Offset: 0x000035BC
		private ICollection<string> DefaultGetScpUrlsForDomain(string domainName)
		{
			DirectoryHelper directoryHelper = new DirectoryHelper(this);
			return directoryHelper.GetAutodiscoverScpUrlsForDomain(domainName);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000045D8 File Offset: 0x000035D8
		internal List<Uri> GetAutodiscoverServiceUrls(string domainName, out int scpHostCount)
		{
			List<Uri> list = new List<Uri>();
			if (this.enableScpLookup)
			{
				Func<string, ICollection<string>> func = this.GetScpUrlsForDomainCallback ?? new Func<string, ICollection<string>>(this.DefaultGetScpUrlsForDomain);
				ICollection<string> collection = func.Invoke(domainName);
				foreach (string text in collection)
				{
					list.Add(new Uri(text));
				}
			}
			scpHostCount = list.Count;
			list.Add(new Uri(string.Format("https://{0}/autodiscover/autodiscover.xml", domainName)));
			list.Add(new Uri(string.Format("https://{0}/autodiscover/autodiscover.xml", "autodiscover." + domainName)));
			return list;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004698 File Offset: 0x00003698
		internal List<string> GetAutodiscoverServiceHosts(string domainName, out int scpHostCount)
		{
			List<string> list = new List<string>();
			foreach (Uri uri in this.GetAutodiscoverServiceUrls(domainName, out scpHostCount))
			{
				list.Add(uri.Host);
			}
			return list;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000046FC File Offset: 0x000036FC
		private bool TryGetEnabledEndpointsForHost(ref string host, out AutodiscoverEndpoints endpoints)
		{
			base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Determining which endpoints are enabled for host {0}", host));
			int i = 0;
			while (i < 10)
			{
				Uri uri = new Uri(string.Format("https://{0}/autodiscover/autodiscover.xml", host));
				endpoints = AutodiscoverEndpoints.None;
				IEwsHttpWebRequest ewsHttpWebRequest = base.HttpWebRequestFactory.CreateRequest(uri);
				ewsHttpWebRequest.Method = "GET";
				ewsHttpWebRequest.AllowAutoRedirect = false;
				ewsHttpWebRequest.PreAuthenticate = false;
				ewsHttpWebRequest.UseDefaultCredentials = false;
				IEwsHttpWebResponse ewsHttpWebResponse = null;
				try
				{
					ewsHttpWebResponse = ewsHttpWebRequest.GetResponse();
				}
				catch (WebException ex)
				{
					base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Request error: {0}", ex.Message));
					if (ex.Response != null)
					{
						ewsHttpWebResponse = base.HttpWebRequestFactory.CreateExceptionResponse(ex);
					}
				}
				catch (IOException ex2)
				{
					base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("I/O error: {0}", ex2.Message));
				}
				if (ewsHttpWebResponse != null)
				{
					using (ewsHttpWebResponse)
					{
						Uri uri2;
						if (this.TryGetRedirectionResponse(ewsHttpWebResponse, out uri2))
						{
							base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Host returned redirection to host '{0}'", uri2.Host));
							host = uri2.Host;
							goto IL_012A;
						}
						endpoints = this.GetEndpointsFromHttpWebResponse(ewsHttpWebResponse);
						base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Host returned enabled endpoint flags: {0}", endpoints));
						return true;
					}
					return false;
					IL_012A:
					i++;
					continue;
				}
				return false;
			}
			base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Maximum number of redirection hops {0} exceeded", 10));
			throw new AutodiscoverLocalException(Strings.MaximumRedirectionHopsExceeded);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004894 File Offset: 0x00003894
		private AutodiscoverEndpoints GetEndpointsFromHttpWebResponse(IEwsHttpWebResponse response)
		{
			AutodiscoverEndpoints autodiscoverEndpoints = AutodiscoverEndpoints.Legacy;
			if (!string.IsNullOrEmpty(response.Headers["X-SOAP-Enabled"]))
			{
				autodiscoverEndpoints |= AutodiscoverEndpoints.Soap;
			}
			if (!string.IsNullOrEmpty(response.Headers["X-WSSecurity-Enabled"]))
			{
				autodiscoverEndpoints |= AutodiscoverEndpoints.WsSecurity;
			}
			if (!string.IsNullOrEmpty(response.Headers["X-WSSecurity-SymmetricKey-Enabled"]))
			{
				autodiscoverEndpoints |= AutodiscoverEndpoints.WSSecuritySymmetricKey;
			}
			if (!string.IsNullOrEmpty(response.Headers["X-WSSecurity-X509Cert-Enabled"]))
			{
				autodiscoverEndpoints |= AutodiscoverEndpoints.WSSecurityX509Cert;
			}
			if (!string.IsNullOrEmpty(response.Headers["X-OAuth-Enabled"]))
			{
				autodiscoverEndpoints |= AutodiscoverEndpoints.OAuth;
			}
			return autodiscoverEndpoints;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004930 File Offset: 0x00003930
		internal void TraceResponse(IEwsHttpWebResponse response, MemoryStream memoryStream)
		{
			base.ProcessHttpResponseHeaders(TraceFlags.AutodiscoverResponseHttpHeaders, response);
			if (base.TraceEnabled)
			{
				if (!string.IsNullOrEmpty(response.ContentType) && (response.ContentType.StartsWith("text/", 5) || response.ContentType.StartsWith("application/soap", 5)))
				{
					base.TraceXml(TraceFlags.AutodiscoverResponse, memoryStream);
					return;
				}
				base.TraceMessage(TraceFlags.AutodiscoverResponse, "Non-textual response");
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000499B File Offset: 0x0000399B
		internal IEwsHttpWebRequest PrepareHttpWebRequestForUrl(Uri url)
		{
			return base.PrepareHttpWebRequestForUrl(url, false, false);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000049A8 File Offset: 0x000039A8
		private bool CallRedirectionUrlValidationCallback(string redirectionUrl)
		{
			AutodiscoverRedirectionUrlValidationCallback autodiscoverRedirectionUrlValidationCallback = ((this.RedirectionUrlValidationCallback == null) ? new AutodiscoverRedirectionUrlValidationCallback(this.DefaultAutodiscoverRedirectionUrlValidationCallback) : this.RedirectionUrlValidationCallback);
			return autodiscoverRedirectionUrlValidationCallback(redirectionUrl);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000049D9 File Offset: 0x000039D9
		internal override void ProcessHttpErrorResponse(IEwsHttpWebResponse httpWebResponse, WebException webException)
		{
			base.InternalProcessHttpErrorResponse(httpWebResponse, webException, TraceFlags.AutodiscoverResponseHttpHeaders, TraceFlags.AutodiscoverResponse);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000049E9 File Offset: 0x000039E9
		public AutodiscoverService()
			: this(ExchangeVersion.Exchange2010)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000049F2 File Offset: 0x000039F2
		public AutodiscoverService(ExchangeVersion requestedServerVersion)
			: this(null, null, requestedServerVersion)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000049FD File Offset: 0x000039FD
		public AutodiscoverService(string domain)
			: this(null, domain)
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004A07 File Offset: 0x00003A07
		public AutodiscoverService(string domain, ExchangeVersion requestedServerVersion)
			: this(null, domain, requestedServerVersion)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004A12 File Offset: 0x00003A12
		public AutodiscoverService(Uri url)
			: this(url, url.Host)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004A21 File Offset: 0x00003A21
		public AutodiscoverService(Uri url, ExchangeVersion requestedServerVersion)
			: this(url, url.Host, requestedServerVersion)
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004A31 File Offset: 0x00003A31
		internal AutodiscoverService(Uri url, string domain)
		{
			this.isExternal = new bool?(true);
			this.enableScpLookup = true;
			base..ctor();
			EwsUtilities.ValidateDomainNameAllowNull(domain, "domain");
			this.url = url;
			this.domain = domain;
			this.dnsClient = new AutodiscoverDnsClient(this);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004A74 File Offset: 0x00003A74
		internal AutodiscoverService(Uri url, string domain, ExchangeVersion requestedServerVersion)
		{
			this.isExternal = new bool?(true);
			this.enableScpLookup = true;
			base..ctor(requestedServerVersion);
			EwsUtilities.ValidateDomainNameAllowNull(domain, "domain");
			this.url = url;
			this.domain = domain;
			this.dnsClient = new AutodiscoverDnsClient(this);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004AC0 File Offset: 0x00003AC0
		internal AutodiscoverService(ExchangeServiceBase service, ExchangeVersion requestedServerVersion)
		{
			this.isExternal = new bool?(true);
			this.enableScpLookup = true;
			base..ctor(service, requestedServerVersion);
			this.dnsClient = new AutodiscoverDnsClient(this);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004AE9 File Offset: 0x00003AE9
		internal AutodiscoverService(ExchangeServiceBase service)
			: this(service, service.RequestedServerVersion)
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004AF8 File Offset: 0x00003AF8
		public GetUserSettingsResponse GetUserSettings(string userSmtpAddress, params UserSettingName[] userSettingNames)
		{
			List<UserSettingName> list = new List<UserSettingName>(userSettingNames);
			if (string.IsNullOrEmpty(userSmtpAddress))
			{
				throw new ServiceValidationException(Strings.InvalidAutodiscoverSmtpAddress);
			}
			if (list.Count == 0)
			{
				throw new ServiceValidationException(Strings.InvalidAutodiscoverSettingsCount);
			}
			if (base.RequestedServerVersion < ExchangeVersion.Exchange2010)
			{
				return this.InternalGetLegacyUserSettings(userSmtpAddress, list);
			}
			return this.InternalGetSoapUserSettings(userSmtpAddress, list);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004B58 File Offset: 0x00003B58
		public GetUserSettingsResponseCollection GetUsersSettings(IEnumerable<string> userSmtpAddresses, params UserSettingName[] userSettingNames)
		{
			if (base.RequestedServerVersion < ExchangeVersion.Exchange2010)
			{
				throw new ServiceVersionException(string.Format(Strings.AutodiscoverServiceIncompatibleWithRequestVersion, ExchangeVersion.Exchange2010));
			}
			List<string> list = new List<string>(userSmtpAddresses);
			List<UserSettingName> list2 = new List<UserSettingName>(userSettingNames);
			return this.GetUserSettings(list, list2);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004BA0 File Offset: 0x00003BA0
		public GetDomainSettingsResponse GetDomainSettings(string domain, ExchangeVersion? requestedVersion, params DomainSettingName[] domainSettingNames)
		{
			List<string> list = new List<string>(1);
			list.Add(domain);
			List<DomainSettingName> list2 = new List<DomainSettingName>(domainSettingNames);
			return this.GetDomainSettings(list, list2, requestedVersion)[0];
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004BD4 File Offset: 0x00003BD4
		public GetDomainSettingsResponseCollection GetDomainSettings(IEnumerable<string> domains, ExchangeVersion? requestedVersion, params DomainSettingName[] domainSettingNames)
		{
			List<DomainSettingName> list = new List<DomainSettingName>(domainSettingNames);
			return this.GetDomainSettings(new List<string>(domains), list, requestedVersion);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004BF8 File Offset: 0x00003BF8
		public bool TryGetPartnerAccess(string targetTenantDomain, out ExchangeCredentials partnerAccessCredentials, out Uri targetTenantAutodiscoverUrl)
		{
			EwsUtilities.ValidateNonBlankStringParam(targetTenantDomain, "targetTenantDomain");
			if (this.Url == null)
			{
				throw new ServiceValidationException(Strings.PartnerTokenRequestRequiresUrl);
			}
			if (base.RequestedServerVersion < ExchangeVersion.Exchange2010_SP1)
			{
				throw new ServiceVersionException(string.Format(Strings.PartnerTokenIncompatibleWithRequestVersion, ExchangeVersion.Exchange2010_SP1));
			}
			partnerAccessCredentials = null;
			targetTenantAutodiscoverUrl = null;
			string text = targetTenantDomain;
			if (!text.Contains("@"))
			{
				text = "SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}@" + targetTenantDomain;
			}
			GetUserSettingsRequest getUserSettingsRequest = new GetUserSettingsRequest(this, this.Url, true);
			getUserSettingsRequest.SmtpAddresses = new List<string>(new string[] { text });
			getUserSettingsRequest.Settings = new List<UserSettingName>(new UserSettingName[] { UserSettingName.ExternalEwsUrl });
			GetUserSettingsResponseCollection getUserSettingsResponseCollection = null;
			try
			{
				getUserSettingsResponseCollection = getUserSettingsRequest.Execute();
			}
			catch (ServiceRequestException)
			{
				return false;
			}
			catch (ServiceRemoteException)
			{
				return false;
			}
			if (string.IsNullOrEmpty(getUserSettingsRequest.PartnerToken) || string.IsNullOrEmpty(getUserSettingsRequest.PartnerTokenReference))
			{
				return false;
			}
			if (getUserSettingsResponseCollection.ErrorCode == AutodiscoverErrorCode.NoError)
			{
				GetUserSettingsResponse getUserSettingsResponse = getUserSettingsResponseCollection.Responses[0];
				if (getUserSettingsResponse.ErrorCode == AutodiscoverErrorCode.NoError)
				{
					targetTenantAutodiscoverUrl = this.Url;
				}
				else
				{
					if (getUserSettingsResponse.ErrorCode != AutodiscoverErrorCode.RedirectUrl)
					{
						return false;
					}
					targetTenantAutodiscoverUrl = new Uri(getUserSettingsResponse.RedirectTarget);
				}
				partnerAccessCredentials = new PartnerTokenCredentials(getUserSettingsRequest.PartnerToken, getUserSettingsRequest.PartnerTokenReference);
				targetTenantAutodiscoverUrl = partnerAccessCredentials.AdjustUrl(targetTenantAutodiscoverUrl);
				return true;
			}
			return false;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004D70 File Offset: 0x00003D70
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004D78 File Offset: 0x00003D78
		public string Domain
		{
			get
			{
				return this.domain;
			}
			set
			{
				EwsUtilities.ValidateDomainNameAllowNull(value, "Domain");
				if (value != null)
				{
					this.url = null;
				}
				this.domain = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004D96 File Offset: 0x00003D96
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004D9E File Offset: 0x00003D9E
		public Uri Url
		{
			get
			{
				return this.url;
			}
			set
			{
				if (value != null)
				{
					this.domain = value.Host;
				}
				this.url = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004DBC File Offset: 0x00003DBC
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00004DC4 File Offset: 0x00003DC4
		public bool? IsExternal
		{
			get
			{
				return this.isExternal;
			}
			internal set
			{
				this.isExternal = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004DCD File Offset: 0x00003DCD
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004DD5 File Offset: 0x00003DD5
		public AutodiscoverRedirectionUrlValidationCallback RedirectionUrlValidationCallback
		{
			get
			{
				return this.redirectionUrlValidationCallback;
			}
			set
			{
				this.redirectionUrlValidationCallback = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004DDE File Offset: 0x00003DDE
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00004DE6 File Offset: 0x00003DE6
		internal IPAddress DnsServerAddress
		{
			get
			{
				return this.dnsServerAddress;
			}
			set
			{
				this.dnsServerAddress = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004DEF File Offset: 0x00003DEF
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00004DF7 File Offset: 0x00003DF7
		public bool EnableScpLookup
		{
			get
			{
				return this.enableScpLookup;
			}
			set
			{
				this.enableScpLookup = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004E00 File Offset: 0x00003E00
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004E08 File Offset: 0x00003E08
		public Func<string, ICollection<string>> GetScpUrlsForDomainCallback { get; set; }

		// Token: 0x04000037 RID: 55
		private const string AutodiscoverLegacyPath = "/autodiscover/autodiscover.xml";

		// Token: 0x04000038 RID: 56
		private const string AutodiscoverLegacyUrl = "{0}://{1}/autodiscover/autodiscover.xml";

		// Token: 0x04000039 RID: 57
		private const string AutodiscoverLegacyHttpsUrl = "https://{0}/autodiscover/autodiscover.xml";

		// Token: 0x0400003A RID: 58
		private const string AutodiscoverLegacyHttpUrl = "http://{0}/autodiscover/autodiscover.xml";

		// Token: 0x0400003B RID: 59
		private const string AutodiscoverSoapHttpsUrl = "https://{0}/autodiscover/autodiscover.svc";

		// Token: 0x0400003C RID: 60
		private const string AutodiscoverSoapWsSecurityHttpsUrl = "https://{0}/autodiscover/autodiscover.svc/wssecurity";

		// Token: 0x0400003D RID: 61
		private const string AutodiscoverSoapWsSecuritySymmetricKeyHttpsUrl = "https://{0}/autodiscover/autodiscover.svc/wssecurity/symmetrickey";

		// Token: 0x0400003E RID: 62
		private const string AutodiscoverSoapWsSecurityX509CertHttpsUrl = "https://{0}/autodiscover/autodiscover.svc/wssecurity/x509cert";

		// Token: 0x0400003F RID: 63
		private const string AutodiscoverRequestNamespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/requestschema/2006";

		// Token: 0x04000040 RID: 64
		internal const int AutodiscoverMaxRedirections = 10;

		// Token: 0x04000041 RID: 65
		private const string AutodiscoverSoapEnabledHeaderName = "X-SOAP-Enabled";

		// Token: 0x04000042 RID: 66
		private const string AutodiscoverWsSecurityEnabledHeaderName = "X-WSSecurity-Enabled";

		// Token: 0x04000043 RID: 67
		private const string AutodiscoverWsSecuritySymmetricKeyEnabledHeaderName = "X-WSSecurity-SymmetricKey-Enabled";

		// Token: 0x04000044 RID: 68
		private const string AutodiscoverWsSecurityX509CertEnabledHeaderName = "X-WSSecurity-X509Cert-Enabled";

		// Token: 0x04000045 RID: 69
		private const string AutodiscoverOAuthEnabledHeaderName = "X-OAuth-Enabled";

		// Token: 0x04000046 RID: 70
		private const ExchangeVersion MinimumRequestVersionForAutoDiscoverSoapService = ExchangeVersion.Exchange2010;

		// Token: 0x04000047 RID: 71
		private static readonly Regex LegacyPathRegex = new Regex("/autodiscover/([^/]+/)*autodiscover.xml", 9);

		// Token: 0x04000048 RID: 72
		private string domain;

		// Token: 0x04000049 RID: 73
		private bool? isExternal;

		// Token: 0x0400004A RID: 74
		private Uri url;

		// Token: 0x0400004B RID: 75
		private AutodiscoverRedirectionUrlValidationCallback redirectionUrlValidationCallback;

		// Token: 0x0400004C RID: 76
		private AutodiscoverDnsClient dnsClient;

		// Token: 0x0400004D RID: 77
		private IPAddress dnsServerAddress;

		// Token: 0x0400004E RID: 78
		private bool enableScpLookup;

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x060000C3 RID: 195
		private delegate TGetSettingsResponseCollection GetSettingsMethod<TGetSettingsResponseCollection, TSettingName>(List<string> smtpAddresses, List<TSettingName> settings, ExchangeVersion? requestedVersion, ref Uri autodiscoverUrl);
	}
}
