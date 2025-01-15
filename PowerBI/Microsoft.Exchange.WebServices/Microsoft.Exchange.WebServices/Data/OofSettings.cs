using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000A8 RID: 168
	public sealed class OofSettings : ComplexProperty, ISelfValidate
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x00019870 File Offset: 0x00018870
		private void SerializeOofReply(OofReply oofReply, EwsServiceXmlWriter writer, string xmlElementName)
		{
			if (oofReply != null)
			{
				oofReply.WriteToXml(writer, xmlElementName);
				return;
			}
			OofReply.WriteEmptyReplyToXml(writer, xmlElementName);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00019890 File Offset: 0x00018890
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "OofState")
				{
					this.state = reader.ReadValue<OofState>();
					return true;
				}
				if (localName == "ExternalAudience")
				{
					this.externalAudience = reader.ReadValue<OofExternalAudience>();
					return true;
				}
				if (localName == "Duration")
				{
					this.duration = new TimeWindow();
					this.duration.LoadFromXml(reader);
					return true;
				}
				if (localName == "InternalReply")
				{
					this.internalReply = new OofReply();
					this.internalReply.LoadFromXml(reader, reader.LocalName);
					return true;
				}
				if (localName == "ExternalReply")
				{
					this.externalReply = new OofReply();
					this.externalReply.LoadFromXml(reader, reader.LocalName);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00019964 File Offset: 0x00018964
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "OofState"))
					{
						if (!(text2 == "ExternalAudience"))
						{
							if (!(text2 == "Duration"))
							{
								if (!(text2 == "InternalReply"))
								{
									if (text2 == "ExternalReply")
									{
										this.externalReply = new OofReply();
										this.externalReply.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
									}
								}
								else
								{
									this.internalReply = new OofReply();
									this.internalReply.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
								}
							}
							else
							{
								this.duration = new TimeWindow();
								this.duration.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							}
						}
						else
						{
							this.externalAudience = jsonProperty.ReadEnumValue<OofExternalAudience>(text);
						}
					}
					else
					{
						this.state = jsonProperty.ReadEnumValue<OofState>(text);
					}
				}
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00019A80 File Offset: 0x00018A80
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			writer.WriteElementValue(XmlNamespace.Types, "OofState", this.State);
			writer.WriteElementValue(XmlNamespace.Types, "ExternalAudience", this.ExternalAudience);
			if (this.Duration != null && this.State == OofState.Scheduled)
			{
				this.Duration.WriteToXml(writer, "Duration");
			}
			this.SerializeOofReply(this.InternalReply, writer, "InternalReply");
			this.SerializeOofReply(this.ExternalReply, writer, "ExternalReply");
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00019B08 File Offset: 0x00018B08
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("OofState", this.State);
			jsonObject.Add("ExternalAudience", this.ExternalAudience);
			if (this.Duration != null && this.State == OofState.Scheduled)
			{
				jsonObject.Add("Duration", this.Duration.InternalToJson(service));
			}
			if (this.InternalReply != null)
			{
				jsonObject.Add("InternalReply", this.InternalReply.InternalToJson(service));
			}
			if (this.ExternalReply != null)
			{
				jsonObject.Add("ExternalReply", this.ExternalReply.InternalToJson(service));
			}
			return jsonObject;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00019BAE File Offset: 0x00018BAE
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x00019BB6 File Offset: 0x00018BB6
		public OofState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00019BBF File Offset: 0x00018BBF
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x00019BC7 File Offset: 0x00018BC7
		public OofExternalAudience ExternalAudience
		{
			get
			{
				return this.externalAudience;
			}
			set
			{
				this.externalAudience = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00019BD0 File Offset: 0x00018BD0
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x00019BD8 File Offset: 0x00018BD8
		public TimeWindow Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00019BE1 File Offset: 0x00018BE1
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x00019BE9 File Offset: 0x00018BE9
		public OofReply InternalReply
		{
			get
			{
				return this.internalReply;
			}
			set
			{
				this.internalReply = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00019BF2 File Offset: 0x00018BF2
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x00019BFA File Offset: 0x00018BFA
		public OofReply ExternalReply
		{
			get
			{
				return this.externalReply;
			}
			set
			{
				this.externalReply = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00019C03 File Offset: 0x00018C03
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x00019C0B File Offset: 0x00018C0B
		public OofExternalAudience AllowExternalOof
		{
			get
			{
				return this.allowExternalOof;
			}
			internal set
			{
				this.allowExternalOof = value;
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00019C14 File Offset: 0x00018C14
		void ISelfValidate.Validate()
		{
			if (this.State == OofState.Scheduled)
			{
				if (this.Duration == null)
				{
					throw new ArgumentException(Strings.DurationMustBeSpecifiedWhenScheduled);
				}
				EwsUtilities.ValidateParam(this.Duration, "Duration");
			}
		}

		// Token: 0x0400027C RID: 636
		private OofState state;

		// Token: 0x0400027D RID: 637
		private OofExternalAudience externalAudience;

		// Token: 0x0400027E RID: 638
		private OofExternalAudience allowExternalOof;

		// Token: 0x0400027F RID: 639
		private TimeWindow duration;

		// Token: 0x04000280 RID: 640
		private OofReply internalReply;

		// Token: 0x04000281 RID: 641
		private OofReply externalReply;
	}
}
