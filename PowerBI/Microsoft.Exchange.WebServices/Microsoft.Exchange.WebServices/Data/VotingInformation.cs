using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000091 RID: 145
	public sealed class VotingInformation : ComplexProperty
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x000165BD File Offset: 0x000155BD
		internal VotingInformation()
		{
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000165D0 File Offset: 0x000155D0
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "UserOptions")
				{
					if (!reader.IsEmptyElement)
					{
						do
						{
							reader.Read();
							if (reader.IsStartElement(XmlNamespace.Types, "VotingOptionData"))
							{
								VotingOptionData votingOptionData = new VotingOptionData();
								votingOptionData.LoadFromXml(reader, reader.LocalName);
								this.userOptions.Add(votingOptionData);
							}
						}
						while (!reader.IsEndElement(XmlNamespace.Types, "UserOptions"));
					}
					return true;
				}
				if (localName == "VotingResponse")
				{
					this.votingResponse = reader.ReadElementValue<string>();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00016660 File Offset: 0x00015660
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "UserOptions"))
					{
						if (text2 == "VotingResponse")
						{
							this.votingResponse = jsonProperty.ReadAsString(text);
						}
					}
					else
					{
						object[] array = jsonProperty.ReadAsArray("UserOptions");
						if (array != null)
						{
							foreach (object obj in array)
							{
								JsonObject jsonObject = obj as JsonObject;
								VotingOptionData votingOptionData = new VotingOptionData();
								votingOptionData.LoadFromJson(jsonObject, service);
								this.userOptions.Add(votingOptionData);
							}
						}
					}
				}
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00016738 File Offset: 0x00015738
		public Collection<VotingOptionData> UserOptions
		{
			get
			{
				return this.userOptions;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00016740 File Offset: 0x00015740
		public string VotingResponse
		{
			get
			{
				return this.votingResponse;
			}
		}

		// Token: 0x04000219 RID: 537
		private Collection<VotingOptionData> userOptions = new Collection<VotingOptionData>();

		// Token: 0x0400021A RID: 538
		private string votingResponse;
	}
}
