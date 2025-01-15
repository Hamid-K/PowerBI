using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000026 RID: 38
	[EditorBrowsable(1)]
	internal sealed class OutlookUser
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00008F35 File Offset: 0x00007F35
		internal OutlookUser()
		{
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008F40 File Offset: 0x00007F40
		internal void LoadFromXml(EwsXmlReader reader)
		{
			do
			{
				reader.Read();
				if (reader.NodeType == 1)
				{
					string localName;
					if ((localName = reader.LocalName) != null)
					{
						if (localName == "DisplayName")
						{
							this.displayName = reader.ReadElementValue();
							goto IL_008D;
						}
						if (localName == "LegacyDN")
						{
							this.legacyDN = reader.ReadElementValue();
							goto IL_008D;
						}
						if (localName == "DeploymentId")
						{
							this.deploymentId = reader.ReadElementValue();
							goto IL_008D;
						}
						if (localName == "AutoDiscoverSMTPAddress")
						{
							this.autodiscoverAMTPAddress = reader.ReadElementValue();
							goto IL_008D;
						}
					}
					reader.SkipCurrentElement();
				}
				IL_008D:;
			}
			while (!reader.IsEndElement(XmlNamespace.NotSpecified, "User"));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00009008 File Offset: 0x00008008
		internal void ConvertToUserSettings(List<UserSettingName> requestedSettings, GetUserSettingsResponse response)
		{
			IEnumerable<KeyValuePair<UserSettingName, Func<OutlookUser, string>>> enumerable = Enumerable.Where<KeyValuePair<UserSettingName, Func<OutlookUser, string>>>(OutlookUser.converterDictionary.Member, (KeyValuePair<UserSettingName, Func<OutlookUser, string>> converter) => requestedSettings.Contains(converter.Key));
			foreach (KeyValuePair<UserSettingName, Func<OutlookUser, string>> keyValuePair in enumerable)
			{
				string text = keyValuePair.Value.Invoke(this);
				if (!string.IsNullOrEmpty(text))
				{
					response.Settings[keyValuePair.Key] = text;
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000090A0 File Offset: 0x000080A0
		internal static IEnumerable<UserSettingName> AvailableUserSettings
		{
			get
			{
				return OutlookUser.converterDictionary.Member.Keys;
			}
		}

		// Token: 0x04000106 RID: 262
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookUser, string>>> converterDictionary = new LazyMember<Dictionary<UserSettingName, Func<OutlookUser, string>>>(delegate
		{
			Dictionary<UserSettingName, Func<OutlookUser, string>> dictionary = new Dictionary<UserSettingName, Func<OutlookUser, string>>();
			dictionary.Add(UserSettingName.UserDisplayName, (OutlookUser u) => u.displayName);
			dictionary.Add(UserSettingName.UserDN, (OutlookUser u) => u.legacyDN);
			dictionary.Add(UserSettingName.UserDeploymentId, (OutlookUser u) => u.deploymentId);
			dictionary.Add(UserSettingName.AutoDiscoverSMTPAddress, (OutlookUser u) => u.autodiscoverAMTPAddress);
			return dictionary;
		});

		// Token: 0x04000107 RID: 263
		private string displayName;

		// Token: 0x04000108 RID: 264
		private string legacyDN;

		// Token: 0x04000109 RID: 265
		private string deploymentId;

		// Token: 0x0400010A RID: 266
		private string autodiscoverAMTPAddress;
	}
}
