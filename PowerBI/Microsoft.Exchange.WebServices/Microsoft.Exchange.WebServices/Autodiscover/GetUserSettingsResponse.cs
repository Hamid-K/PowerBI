using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x0200001B RID: 27
	public sealed class GetUserSettingsResponse : AutodiscoverResponse
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00006993 File Offset: 0x00005993
		public GetUserSettingsResponse()
		{
			this.SmtpAddress = string.Empty;
			this.Settings = new Dictionary<UserSettingName, object>();
			this.UserSettingErrors = new Collection<UserSettingError>();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000069BC File Offset: 0x000059BC
		public bool TryGetSettingValue<T>(UserSettingName setting, out T value)
		{
			object obj;
			if (this.Settings.TryGetValue(setting, ref obj))
			{
				value = (T)((object)obj);
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000069EF File Offset: 0x000059EF
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000069F7 File Offset: 0x000059F7
		public string SmtpAddress { get; internal set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00006A00 File Offset: 0x00005A00
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00006A08 File Offset: 0x00005A08
		public string RedirectTarget { get; internal set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006A11 File Offset: 0x00005A11
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00006A19 File Offset: 0x00005A19
		public IDictionary<UserSettingName, object> Settings { get; internal set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006A22 File Offset: 0x00005A22
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00006A2A File Offset: 0x00005A2A
		public Collection<UserSettingError> UserSettingErrors { get; internal set; }

		// Token: 0x06000133 RID: 307 RVA: 0x00006A34 File Offset: 0x00005A34
		internal override void LoadFromXml(EwsXmlReader reader, string endElementName)
		{
			do
			{
				reader.Read();
				if (reader.NodeType == 1)
				{
					string localName;
					if ((localName = reader.LocalName) != null)
					{
						if (localName == "RedirectTarget")
						{
							this.RedirectTarget = reader.ReadElementValue();
							goto IL_006A;
						}
						if (localName == "UserSettingErrors")
						{
							this.LoadUserSettingErrorsFromXml(reader);
							goto IL_006A;
						}
						if (localName == "UserSettings")
						{
							this.LoadUserSettingsFromXml(reader);
							goto IL_006A;
						}
					}
					base.LoadFromXml(reader, endElementName);
				}
				IL_006A:;
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, endElementName));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006AB8 File Offset: 0x00005AB8
		internal void LoadUserSettingsFromXml(EwsXmlReader reader)
		{
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1 && reader.LocalName == "UserSetting")
					{
						string text = reader.ReadAttributeValue(XmlNamespace.XmlSchemaInstance, "type");
						string text2;
						if ((text2 = text) != null && (text2 == "StringSetting" || text2 == "WebClientUrlCollectionSetting" || text2 == "AlternateMailboxCollectionSetting" || text2 == "ProtocolConnectionCollectionSetting" || text2 == "DocumentSharingLocationCollectionSetting"))
						{
							this.ReadSettingFromXml(reader);
						}
						else
						{
							EwsUtilities.Assert(false, "GetUserSettingsResponse.LoadUserSettingsFromXml", string.Format("Invalid setting class '{0}' returned", text));
						}
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Autodiscover, "UserSettings"));
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006B78 File Offset: 0x00005B78
		private void ReadSettingFromXml(EwsXmlReader reader)
		{
			string text = null;
			object obj = null;
			do
			{
				reader.Read();
				string localName;
				if (reader.NodeType == 1 && (localName = reader.LocalName) != null)
				{
					if (!(localName == "Name"))
					{
						if (!(localName == "Value"))
						{
							if (!(localName == "WebClientUrls"))
							{
								if (!(localName == "ProtocolConnections"))
								{
									if (!(localName == "AlternateMailboxes"))
									{
										if (localName == "DocumentSharingLocations")
										{
											obj = DocumentSharingLocationCollection.LoadFromXml(reader);
										}
									}
									else
									{
										obj = AlternateMailboxCollection.LoadFromXml(reader);
									}
								}
								else
								{
									obj = ProtocolConnectionCollection.LoadFromXml(reader);
								}
							}
							else
							{
								obj = WebClientUrlCollection.LoadFromXml(reader);
							}
						}
						else
						{
							obj = reader.ReadElementValue();
						}
					}
					else
					{
						text = reader.ReadElementValue<string>();
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "UserSetting"));
			try
			{
				UserSettingName userSettingName = EwsUtilities.Parse<UserSettingName>(text);
				this.Settings.Add(userSettingName, obj);
			}
			catch (ArgumentException)
			{
				EwsUtilities.Assert(false, "GetUserSettingsResponse.ReadSettingFromXml", "Unexpected or empty name element in user setting");
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006C78 File Offset: 0x00005C78
		private void LoadUserSettingErrorsFromXml(EwsXmlReader reader)
		{
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1 && reader.LocalName == "UserSettingError")
					{
						UserSettingError userSettingError = new UserSettingError();
						userSettingError.LoadFromXml(reader);
						this.UserSettingErrors.Add(userSettingError);
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Autodiscover, "UserSettingErrors"));
			}
		}
	}
}
