using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000074 RID: 116
	public class OnlineMeetingSettings : ComplexProperty
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00012363 File Offset: 0x00011363
		public OnlineMeetingSettings()
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001236B File Offset: 0x0001136B
		internal OnlineMeetingSettings(LobbyBypass lobbyBypass, OnlineMeetingAccessLevel accessLevel, Presenters presenters)
		{
			this.lobbyBypass = lobbyBypass;
			this.accessLevel = accessLevel;
			this.presenters = presenters;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00012388 File Offset: 0x00011388
		internal OnlineMeetingSettings(OnlineMeetingSettings onlineMeetingSettings)
			: this()
		{
			EwsUtilities.ValidateParam(onlineMeetingSettings, "OnlineMeetingSettings");
			this.LobbyBypass = onlineMeetingSettings.LobbyBypass;
			this.AccessLevel = onlineMeetingSettings.AccessLevel;
			this.Presenters = onlineMeetingSettings.Presenters;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x000123BF File Offset: 0x000113BF
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x000123C7 File Offset: 0x000113C7
		public LobbyBypass LobbyBypass
		{
			get
			{
				return this.lobbyBypass;
			}
			set
			{
				this.SetFieldValue<LobbyBypass>(ref this.lobbyBypass, value);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x000123D6 File Offset: 0x000113D6
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x000123DE File Offset: 0x000113DE
		public OnlineMeetingAccessLevel AccessLevel
		{
			get
			{
				return this.accessLevel;
			}
			set
			{
				this.SetFieldValue<OnlineMeetingAccessLevel>(ref this.accessLevel, value);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x000123ED File Offset: 0x000113ED
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x000123F5 File Offset: 0x000113F5
		public Presenters Presenters
		{
			get
			{
				return this.presenters;
			}
			set
			{
				this.SetFieldValue<Presenters>(ref this.presenters, value);
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00012404 File Offset: 0x00011404
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "LobbyBypass")
				{
					this.lobbyBypass = reader.ReadElementValue<LobbyBypass>();
					return true;
				}
				if (localName == "AccessLevel")
				{
					this.accessLevel = reader.ReadElementValue<OnlineMeetingAccessLevel>();
					return true;
				}
				if (localName == "Presenters")
				{
					this.presenters = reader.ReadElementValue<Presenters>();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00012470 File Offset: 0x00011470
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "LobbyBypass", this.LobbyBypass);
			writer.WriteElementValue(XmlNamespace.Types, "AccessLevel", this.AccessLevel);
			writer.WriteElementValue(XmlNamespace.Types, "Presenters", this.Presenters);
		}

		// Token: 0x040001C5 RID: 453
		private LobbyBypass lobbyBypass;

		// Token: 0x040001C6 RID: 454
		private OnlineMeetingAccessLevel accessLevel;

		// Token: 0x040001C7 RID: 455
		private Presenters presenters;
	}
}
