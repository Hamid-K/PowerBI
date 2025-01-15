using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000024 RID: 36
	internal sealed class OutlookConfigurationSettings : ConfigurationSettingsBase
	{
		// Token: 0x06000176 RID: 374 RVA: 0x000074B3 File Offset: 0x000064B3
		public OutlookConfigurationSettings()
		{
			this.user = new OutlookUser();
			this.account = new OutlookAccount();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000074D1 File Offset: 0x000064D1
		internal static bool IsAvailableUserSetting(UserSettingName setting)
		{
			return OutlookConfigurationSettings.allOutlookProviderSettings.Member.Contains(setting);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000074E3 File Offset: 0x000064E3
		internal override string GetNamespace()
		{
			return "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a";
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000074EC File Offset: 0x000064EC
		internal override void MakeRedirectionResponse(Uri redirectUrl)
		{
			this.account = new OutlookAccount
			{
				RedirectTarget = redirectUrl.ToString(),
				ResponseType = AutodiscoverResponseType.RedirectUrl
			};
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000751C File Offset: 0x0000651C
		internal override bool TryReadCurrentXmlElement(EwsXmlReader reader)
		{
			if (!base.TryReadCurrentXmlElement(reader))
			{
				string localName;
				if ((localName = reader.LocalName) != null)
				{
					if (localName == "User")
					{
						this.user.LoadFromXml(reader);
						return true;
					}
					if (localName == "Account")
					{
						this.account.LoadFromXml(reader);
						return true;
					}
				}
				reader.SkipCurrentElement();
				return false;
			}
			return true;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007580 File Offset: 0x00006580
		internal override GetUserSettingsResponse ConvertSettings(string smtpAddress, List<UserSettingName> requestedSettings)
		{
			GetUserSettingsResponse getUserSettingsResponse = new GetUserSettingsResponse();
			getUserSettingsResponse.SmtpAddress = smtpAddress;
			if (base.Error != null)
			{
				getUserSettingsResponse.ErrorCode = AutodiscoverErrorCode.InternalServerError;
				getUserSettingsResponse.ErrorMessage = base.Error.Message;
			}
			else
			{
				switch (this.ResponseType)
				{
				case AutodiscoverResponseType.Error:
					getUserSettingsResponse.ErrorCode = AutodiscoverErrorCode.InternalServerError;
					getUserSettingsResponse.ErrorMessage = Strings.InvalidAutodiscoverServiceResponse;
					break;
				case AutodiscoverResponseType.RedirectUrl:
					getUserSettingsResponse.ErrorCode = AutodiscoverErrorCode.RedirectUrl;
					getUserSettingsResponse.ErrorMessage = string.Empty;
					getUserSettingsResponse.RedirectTarget = this.RedirectTarget;
					break;
				case AutodiscoverResponseType.RedirectAddress:
					getUserSettingsResponse.ErrorCode = AutodiscoverErrorCode.RedirectAddress;
					getUserSettingsResponse.ErrorMessage = string.Empty;
					getUserSettingsResponse.RedirectTarget = this.RedirectTarget;
					break;
				case AutodiscoverResponseType.Success:
					getUserSettingsResponse.ErrorCode = AutodiscoverErrorCode.NoError;
					getUserSettingsResponse.ErrorMessage = string.Empty;
					this.user.ConvertToUserSettings(requestedSettings, getUserSettingsResponse);
					this.account.ConvertToUserSettings(requestedSettings, getUserSettingsResponse);
					this.ReportUnsupportedSettings(requestedSettings, getUserSettingsResponse);
					break;
				default:
					EwsUtilities.Assert(false, "OutlookConfigurationSettings.ConvertSettings", "An unexpected error has occured. This code path should never be reached.");
					break;
				}
			}
			return getUserSettingsResponse;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007690 File Offset: 0x00006690
		private void ReportUnsupportedSettings(List<UserSettingName> requestedSettings, GetUserSettingsResponse response)
		{
			IEnumerable<UserSettingName> enumerable = Enumerable.Where<UserSettingName>(requestedSettings, (UserSettingName setting) => !OutlookConfigurationSettings.IsAvailableUserSetting(setting));
			foreach (UserSettingName userSettingName in enumerable)
			{
				UserSettingError userSettingError = new UserSettingError
				{
					ErrorCode = AutodiscoverErrorCode.InvalidSetting,
					SettingName = userSettingName.ToString(),
					ErrorMessage = string.Format(Strings.AutodiscoverInvalidSettingForOutlookProvider, userSettingName.ToString())
				};
				response.UserSettingErrors.Add(userSettingError);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00007748 File Offset: 0x00006748
		internal override AutodiscoverResponseType ResponseType
		{
			get
			{
				if (this.account != null)
				{
					return this.account.ResponseType;
				}
				return AutodiscoverResponseType.Error;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000775F File Offset: 0x0000675F
		internal override string RedirectTarget
		{
			get
			{
				return this.account.RedirectTarget;
			}
		}

		// Token: 0x04000088 RID: 136
		private static LazyMember<List<UserSettingName>> allOutlookProviderSettings = new LazyMember<List<UserSettingName>>(delegate
		{
			List<UserSettingName> list = new List<UserSettingName>();
			list.AddRange(OutlookUser.AvailableUserSettings);
			list.AddRange(OutlookProtocol.AvailableUserSettings);
			list.Add(UserSettingName.AlternateMailboxes);
			return list;
		});

		// Token: 0x04000089 RID: 137
		private OutlookUser user;

		// Token: 0x0400008A RID: 138
		private OutlookAccount account;
	}
}
