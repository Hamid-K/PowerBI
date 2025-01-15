using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000096 RID: 150
	public sealed class RulePredicates : ComplexProperty
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x00017A38 File Offset: 0x00016A38
		internal RulePredicates()
		{
			this.categories = new StringList();
			this.containsBodyStrings = new StringList();
			this.containsHeaderStrings = new StringList();
			this.containsRecipientStrings = new StringList();
			this.containsSenderStrings = new StringList();
			this.containsSubjectOrBodyStrings = new StringList();
			this.containsSubjectStrings = new StringList();
			this.fromAddresses = new EmailAddressCollection("Address");
			this.fromConnectedAccounts = new StringList();
			this.itemClasses = new StringList();
			this.messageClassifications = new StringList();
			this.sentToAddresses = new EmailAddressCollection("Address");
			this.withinDateRange = new RulePredicateDateRange();
			this.withinSizeRange = new RulePredicateSizeRange();
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00017AEF File Offset: 0x00016AEF
		public StringList Categories
		{
			get
			{
				return this.categories;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00017AF7 File Offset: 0x00016AF7
		public StringList ContainsBodyStrings
		{
			get
			{
				return this.containsBodyStrings;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00017AFF File Offset: 0x00016AFF
		public StringList ContainsHeaderStrings
		{
			get
			{
				return this.containsHeaderStrings;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00017B07 File Offset: 0x00016B07
		public StringList ContainsRecipientStrings
		{
			get
			{
				return this.containsRecipientStrings;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00017B0F File Offset: 0x00016B0F
		public StringList ContainsSenderStrings
		{
			get
			{
				return this.containsSenderStrings;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00017B17 File Offset: 0x00016B17
		public StringList ContainsSubjectOrBodyStrings
		{
			get
			{
				return this.containsSubjectOrBodyStrings;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00017B1F File Offset: 0x00016B1F
		public StringList ContainsSubjectStrings
		{
			get
			{
				return this.containsSubjectStrings;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00017B27 File Offset: 0x00016B27
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x00017B2F File Offset: 0x00016B2F
		public FlaggedForAction? FlaggedForAction
		{
			get
			{
				return this.flaggedForAction;
			}
			set
			{
				this.SetFieldValue<FlaggedForAction?>(ref this.flaggedForAction, value);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00017B3E File Offset: 0x00016B3E
		public EmailAddressCollection FromAddresses
		{
			get
			{
				return this.fromAddresses;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00017B46 File Offset: 0x00016B46
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00017B4E File Offset: 0x00016B4E
		public bool HasAttachments
		{
			get
			{
				return this.hasAttachments;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.hasAttachments, value);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00017B5D File Offset: 0x00016B5D
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x00017B65 File Offset: 0x00016B65
		public Importance? Importance
		{
			get
			{
				return this.importance;
			}
			set
			{
				this.SetFieldValue<Importance?>(ref this.importance, value);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00017B74 File Offset: 0x00016B74
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x00017B7C File Offset: 0x00016B7C
		public bool IsApprovalRequest
		{
			get
			{
				return this.isApprovalRequest;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isApprovalRequest, value);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00017B8B File Offset: 0x00016B8B
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x00017B93 File Offset: 0x00016B93
		public bool IsAutomaticForward
		{
			get
			{
				return this.isAutomaticForward;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isAutomaticForward, value);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00017BA2 File Offset: 0x00016BA2
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x00017BAA File Offset: 0x00016BAA
		public bool IsAutomaticReply
		{
			get
			{
				return this.isAutomaticReply;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isAutomaticReply, value);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00017BB9 File Offset: 0x00016BB9
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x00017BC1 File Offset: 0x00016BC1
		public bool IsEncrypted
		{
			get
			{
				return this.isEncrypted;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isEncrypted, value);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00017BD0 File Offset: 0x00016BD0
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x00017BD8 File Offset: 0x00016BD8
		public bool IsMeetingRequest
		{
			get
			{
				return this.isMeetingRequest;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isMeetingRequest, value);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00017BE7 File Offset: 0x00016BE7
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x00017BEF File Offset: 0x00016BEF
		public bool IsMeetingResponse
		{
			get
			{
				return this.isMeetingResponse;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isMeetingResponse, value);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00017BFE File Offset: 0x00016BFE
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x00017C06 File Offset: 0x00016C06
		public bool IsNonDeliveryReport
		{
			get
			{
				return this.isNonDeliveryReport;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isNonDeliveryReport, value);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00017C15 File Offset: 0x00016C15
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x00017C1D File Offset: 0x00016C1D
		public bool IsPermissionControlled
		{
			get
			{
				return this.isPermissionControlled;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isPermissionControlled, value);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00017C2C File Offset: 0x00016C2C
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x00017C34 File Offset: 0x00016C34
		public bool IsSigned
		{
			get
			{
				return this.isSigned;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isSigned, value);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00017C43 File Offset: 0x00016C43
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x00017C4B File Offset: 0x00016C4B
		public bool IsVoicemail
		{
			get
			{
				return this.isVoicemail;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isVoicemail, value);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00017C5A File Offset: 0x00016C5A
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00017C62 File Offset: 0x00016C62
		public bool IsReadReceipt
		{
			get
			{
				return this.isReadReceipt;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isReadReceipt, value);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00017C71 File Offset: 0x00016C71
		public StringList FromConnectedAccounts
		{
			get
			{
				return this.fromConnectedAccounts;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00017C79 File Offset: 0x00016C79
		public StringList ItemClasses
		{
			get
			{
				return this.itemClasses;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00017C81 File Offset: 0x00016C81
		public StringList MessageClassifications
		{
			get
			{
				return this.messageClassifications;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00017C89 File Offset: 0x00016C89
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x00017C91 File Offset: 0x00016C91
		public bool NotSentToMe
		{
			get
			{
				return this.notSentToMe;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.notSentToMe, value);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00017CA0 File Offset: 0x00016CA0
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x00017CA8 File Offset: 0x00016CA8
		public bool SentCcMe
		{
			get
			{
				return this.sentCcMe;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.sentCcMe, value);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00017CB7 File Offset: 0x00016CB7
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x00017CBF File Offset: 0x00016CBF
		public bool SentOnlyToMe
		{
			get
			{
				return this.sentOnlyToMe;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.sentOnlyToMe, value);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00017CCE File Offset: 0x00016CCE
		public EmailAddressCollection SentToAddresses
		{
			get
			{
				return this.sentToAddresses;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00017CD6 File Offset: 0x00016CD6
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x00017CDE File Offset: 0x00016CDE
		public bool SentToMe
		{
			get
			{
				return this.sentToMe;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.sentToMe, value);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x00017CED File Offset: 0x00016CED
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x00017CF5 File Offset: 0x00016CF5
		public bool SentToOrCcMe
		{
			get
			{
				return this.sentToOrCcMe;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.sentToOrCcMe, value);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00017D04 File Offset: 0x00016D04
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x00017D0C File Offset: 0x00016D0C
		public Sensitivity? Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
			set
			{
				this.SetFieldValue<Sensitivity?>(ref this.sensitivity, value);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00017D1B File Offset: 0x00016D1B
		public RulePredicateDateRange WithinDateRange
		{
			get
			{
				return this.withinDateRange;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00017D23 File Offset: 0x00016D23
		public RulePredicateSizeRange WithinSizeRange
		{
			get
			{
				return this.withinSizeRange;
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00017D2C File Offset: 0x00016D2C
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60006b2-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(34);
					dictionary.Add("Categories", 0);
					dictionary.Add("ContainsBodyStrings", 1);
					dictionary.Add("ContainsHeaderStrings", 2);
					dictionary.Add("ContainsRecipientStrings", 3);
					dictionary.Add("ContainsSenderStrings", 4);
					dictionary.Add("ContainsSubjectOrBodyStrings", 5);
					dictionary.Add("ContainsSubjectStrings", 6);
					dictionary.Add("FlaggedForAction", 7);
					dictionary.Add("FromAddresses", 8);
					dictionary.Add("FromConnectedAccounts", 9);
					dictionary.Add("HasAttachments", 10);
					dictionary.Add("Importance", 11);
					dictionary.Add("IsApprovalRequest", 12);
					dictionary.Add("IsAutomaticForward", 13);
					dictionary.Add("IsAutomaticReply", 14);
					dictionary.Add("IsEncrypted", 15);
					dictionary.Add("IsMeetingRequest", 16);
					dictionary.Add("IsMeetingResponse", 17);
					dictionary.Add("IsNDR", 18);
					dictionary.Add("IsPermissionControlled", 19);
					dictionary.Add("IsSigned", 20);
					dictionary.Add("IsVoicemail", 21);
					dictionary.Add("IsReadReceipt", 22);
					dictionary.Add("ItemClasses", 23);
					dictionary.Add("MessageClassifications", 24);
					dictionary.Add("NotSentToMe", 25);
					dictionary.Add("SentCcMe", 26);
					dictionary.Add("SentOnlyToMe", 27);
					dictionary.Add("SentToAddresses", 28);
					dictionary.Add("SentToMe", 29);
					dictionary.Add("SentToOrCcMe", 30);
					dictionary.Add("Sensitivity", 31);
					dictionary.Add("WithinDateRange", 32);
					dictionary.Add("WithinSizeRange", 33);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60006b2-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60006b2-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.categories.LoadFromXml(reader, reader.LocalName);
						return true;
					case 1:
						this.containsBodyStrings.LoadFromXml(reader, reader.LocalName);
						return true;
					case 2:
						this.containsHeaderStrings.LoadFromXml(reader, reader.LocalName);
						return true;
					case 3:
						this.containsRecipientStrings.LoadFromXml(reader, reader.LocalName);
						return true;
					case 4:
						this.containsSenderStrings.LoadFromXml(reader, reader.LocalName);
						return true;
					case 5:
						this.containsSubjectOrBodyStrings.LoadFromXml(reader, reader.LocalName);
						return true;
					case 6:
						this.containsSubjectStrings.LoadFromXml(reader, reader.LocalName);
						return true;
					case 7:
						this.flaggedForAction = new FlaggedForAction?(reader.ReadElementValue<FlaggedForAction>());
						return true;
					case 8:
						this.fromAddresses.LoadFromXml(reader, reader.LocalName);
						return true;
					case 9:
						this.fromConnectedAccounts.LoadFromXml(reader, reader.LocalName);
						return true;
					case 10:
						this.hasAttachments = reader.ReadElementValue<bool>();
						return true;
					case 11:
						this.importance = new Importance?(reader.ReadElementValue<Importance>());
						return true;
					case 12:
						this.isApprovalRequest = reader.ReadElementValue<bool>();
						return true;
					case 13:
						this.isAutomaticForward = reader.ReadElementValue<bool>();
						return true;
					case 14:
						this.isAutomaticReply = reader.ReadElementValue<bool>();
						return true;
					case 15:
						this.isEncrypted = reader.ReadElementValue<bool>();
						return true;
					case 16:
						this.isMeetingRequest = reader.ReadElementValue<bool>();
						return true;
					case 17:
						this.isMeetingResponse = reader.ReadElementValue<bool>();
						return true;
					case 18:
						this.isNonDeliveryReport = reader.ReadElementValue<bool>();
						return true;
					case 19:
						this.isPermissionControlled = reader.ReadElementValue<bool>();
						return true;
					case 20:
						this.isSigned = reader.ReadElementValue<bool>();
						return true;
					case 21:
						this.isVoicemail = reader.ReadElementValue<bool>();
						return true;
					case 22:
						this.isReadReceipt = reader.ReadElementValue<bool>();
						return true;
					case 23:
						this.itemClasses.LoadFromXml(reader, reader.LocalName);
						return true;
					case 24:
						this.messageClassifications.LoadFromXml(reader, reader.LocalName);
						return true;
					case 25:
						this.notSentToMe = reader.ReadElementValue<bool>();
						return true;
					case 26:
						this.sentCcMe = reader.ReadElementValue<bool>();
						return true;
					case 27:
						this.sentOnlyToMe = reader.ReadElementValue<bool>();
						return true;
					case 28:
						this.sentToAddresses.LoadFromXml(reader, reader.LocalName);
						return true;
					case 29:
						this.sentToMe = reader.ReadElementValue<bool>();
						return true;
					case 30:
						this.sentToOrCcMe = reader.ReadElementValue<bool>();
						return true;
					case 31:
						this.sensitivity = new Sensitivity?(reader.ReadElementValue<Sensitivity>());
						return true;
					case 32:
						this.withinDateRange.LoadFromXml(reader, reader.LocalName);
						return true;
					case 33:
						this.withinSizeRange.LoadFromXml(reader, reader.LocalName);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000181F8 File Offset: 0x000171F8
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.Categories.Count > 0)
			{
				this.Categories.WriteToXml(writer, "Categories");
			}
			if (this.ContainsBodyStrings.Count > 0)
			{
				this.ContainsBodyStrings.WriteToXml(writer, "ContainsBodyStrings");
			}
			if (this.ContainsHeaderStrings.Count > 0)
			{
				this.ContainsHeaderStrings.WriteToXml(writer, "ContainsHeaderStrings");
			}
			if (this.ContainsRecipientStrings.Count > 0)
			{
				this.ContainsRecipientStrings.WriteToXml(writer, "ContainsRecipientStrings");
			}
			if (this.ContainsSenderStrings.Count > 0)
			{
				this.ContainsSenderStrings.WriteToXml(writer, "ContainsSenderStrings");
			}
			if (this.ContainsSubjectOrBodyStrings.Count > 0)
			{
				this.ContainsSubjectOrBodyStrings.WriteToXml(writer, "ContainsSubjectOrBodyStrings");
			}
			if (this.ContainsSubjectStrings.Count > 0)
			{
				this.ContainsSubjectStrings.WriteToXml(writer, "ContainsSubjectStrings");
			}
			if (this.FlaggedForAction != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "FlaggedForAction", this.FlaggedForAction.Value);
			}
			if (this.FromAddresses.Count > 0)
			{
				this.FromAddresses.WriteToXml(writer, "FromAddresses");
			}
			if (this.FromConnectedAccounts.Count > 0)
			{
				this.FromConnectedAccounts.WriteToXml(writer, "FromConnectedAccounts");
			}
			if (this.HasAttachments)
			{
				writer.WriteElementValue(XmlNamespace.Types, "HasAttachments", this.HasAttachments);
			}
			if (this.Importance != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "Importance", this.Importance.Value);
			}
			if (this.IsApprovalRequest)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsApprovalRequest", this.IsApprovalRequest);
			}
			if (this.IsAutomaticForward)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsAutomaticForward", this.IsAutomaticForward);
			}
			if (this.IsAutomaticReply)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsAutomaticReply", this.IsAutomaticReply);
			}
			if (this.IsEncrypted)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsEncrypted", this.IsEncrypted);
			}
			if (this.IsMeetingRequest)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsMeetingRequest", this.IsMeetingRequest);
			}
			if (this.IsMeetingResponse)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsMeetingResponse", this.IsMeetingResponse);
			}
			if (this.IsNonDeliveryReport)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsNDR", this.IsNonDeliveryReport);
			}
			if (this.IsPermissionControlled)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsPermissionControlled", this.IsPermissionControlled);
			}
			if (this.isReadReceipt)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsReadReceipt", this.IsReadReceipt);
			}
			if (this.IsSigned)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsSigned", this.IsSigned);
			}
			if (this.IsVoicemail)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsVoicemail", this.IsVoicemail);
			}
			if (this.ItemClasses.Count > 0)
			{
				this.ItemClasses.WriteToXml(writer, "ItemClasses");
			}
			if (this.MessageClassifications.Count > 0)
			{
				this.MessageClassifications.WriteToXml(writer, "MessageClassifications");
			}
			if (this.NotSentToMe)
			{
				writer.WriteElementValue(XmlNamespace.Types, "NotSentToMe", this.NotSentToMe);
			}
			if (this.SentCcMe)
			{
				writer.WriteElementValue(XmlNamespace.Types, "SentCcMe", this.SentCcMe);
			}
			if (this.SentOnlyToMe)
			{
				writer.WriteElementValue(XmlNamespace.Types, "SentOnlyToMe", this.SentOnlyToMe);
			}
			if (this.SentToAddresses.Count > 0)
			{
				this.SentToAddresses.WriteToXml(writer, "SentToAddresses");
			}
			if (this.SentToMe)
			{
				writer.WriteElementValue(XmlNamespace.Types, "SentToMe", this.SentToMe);
			}
			if (this.SentToOrCcMe)
			{
				writer.WriteElementValue(XmlNamespace.Types, "SentToOrCcMe", this.SentToOrCcMe);
			}
			if (this.Sensitivity != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "Sensitivity", this.Sensitivity.Value);
			}
			if (this.WithinDateRange.Start != null || this.WithinDateRange.End != null)
			{
				this.WithinDateRange.WriteToXml(writer, "WithinDateRange");
			}
			if (this.WithinSizeRange.MaximumSize != null || this.WithinSizeRange.MinimumSize != null)
			{
				this.WithinSizeRange.WriteToXml(writer, "WithinSizeRange");
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00018694 File Offset: 0x00017694
		internal override void InternalValidate()
		{
			base.InternalValidate();
			EwsUtilities.ValidateParam(this.fromAddresses, "FromAddresses");
			EwsUtilities.ValidateParam(this.sentToAddresses, "SentToAddresses");
			EwsUtilities.ValidateParam(this.withinDateRange, "WithinDateRange");
			EwsUtilities.ValidateParam(this.withinSizeRange, "WithinSizeRange");
		}

		// Token: 0x04000236 RID: 566
		private StringList categories;

		// Token: 0x04000237 RID: 567
		private StringList containsBodyStrings;

		// Token: 0x04000238 RID: 568
		private StringList containsHeaderStrings;

		// Token: 0x04000239 RID: 569
		private StringList containsRecipientStrings;

		// Token: 0x0400023A RID: 570
		private StringList containsSenderStrings;

		// Token: 0x0400023B RID: 571
		private StringList containsSubjectOrBodyStrings;

		// Token: 0x0400023C RID: 572
		private StringList containsSubjectStrings;

		// Token: 0x0400023D RID: 573
		private FlaggedForAction? flaggedForAction;

		// Token: 0x0400023E RID: 574
		private EmailAddressCollection fromAddresses;

		// Token: 0x0400023F RID: 575
		private StringList fromConnectedAccounts;

		// Token: 0x04000240 RID: 576
		private bool hasAttachments;

		// Token: 0x04000241 RID: 577
		private Importance? importance;

		// Token: 0x04000242 RID: 578
		private bool isApprovalRequest;

		// Token: 0x04000243 RID: 579
		private bool isAutomaticForward;

		// Token: 0x04000244 RID: 580
		private bool isAutomaticReply;

		// Token: 0x04000245 RID: 581
		private bool isEncrypted;

		// Token: 0x04000246 RID: 582
		private bool isMeetingRequest;

		// Token: 0x04000247 RID: 583
		private bool isMeetingResponse;

		// Token: 0x04000248 RID: 584
		private bool isNonDeliveryReport;

		// Token: 0x04000249 RID: 585
		private bool isPermissionControlled;

		// Token: 0x0400024A RID: 586
		private bool isSigned;

		// Token: 0x0400024B RID: 587
		private bool isVoicemail;

		// Token: 0x0400024C RID: 588
		private bool isReadReceipt;

		// Token: 0x0400024D RID: 589
		private StringList itemClasses;

		// Token: 0x0400024E RID: 590
		private StringList messageClassifications;

		// Token: 0x0400024F RID: 591
		private bool notSentToMe;

		// Token: 0x04000250 RID: 592
		private bool sentCcMe;

		// Token: 0x04000251 RID: 593
		private bool sentOnlyToMe;

		// Token: 0x04000252 RID: 594
		private EmailAddressCollection sentToAddresses;

		// Token: 0x04000253 RID: 595
		private bool sentToMe;

		// Token: 0x04000254 RID: 596
		private bool sentToOrCcMe;

		// Token: 0x04000255 RID: 597
		private Sensitivity? sensitivity;

		// Token: 0x04000256 RID: 598
		private RulePredicateDateRange withinDateRange;

		// Token: 0x04000257 RID: 599
		private RulePredicateSizeRange withinSizeRange;
	}
}
