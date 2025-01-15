using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000033 RID: 51
	public sealed class ApprovalRequestData : ComplexProperty
	{
		// Token: 0x06000250 RID: 592 RVA: 0x00009E36 File Offset: 0x00008E36
		internal ApprovalRequestData()
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009E40 File Offset: 0x00008E40
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "IsUndecidedApprovalRequest")
				{
					this.isUndecidedApprovalRequest = reader.ReadElementValue<bool>();
					return true;
				}
				if (localName == "ApprovalDecision")
				{
					this.approvalDecision = reader.ReadElementValue<int>();
					return true;
				}
				if (localName == "ApprovalDecisionMaker")
				{
					this.approvalDecisionMaker = reader.ReadElementValue<string>();
					return true;
				}
				if (localName == "ApprovalDecisionTime")
				{
					this.approvalDecisionTime = reader.ReadElementValueAsDateTime().Value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009ED0 File Offset: 0x00008ED0
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "IsUndecidedApprovalRequest"))
					{
						if (!(text2 == "ApprovalDecision"))
						{
							if (!(text2 == "ApprovalDecisionMaker"))
							{
								if (text2 == "ApprovalDecisionTime")
								{
									this.approvalDecisionTime = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text)).Value;
								}
							}
							else
							{
								this.approvalDecisionMaker = jsonProperty.ReadAsString(text);
							}
						}
						else
						{
							this.approvalDecision = jsonProperty.ReadAsInt(text);
						}
					}
					else
					{
						this.isUndecidedApprovalRequest = jsonProperty.ReadAsBool(text);
					}
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00009FA8 File Offset: 0x00008FA8
		public bool IsUndecidedApprovalRequest
		{
			get
			{
				return this.isUndecidedApprovalRequest;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009FB0 File Offset: 0x00008FB0
		public int ApprovalDecision
		{
			get
			{
				return this.approvalDecision;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00009FB8 File Offset: 0x00008FB8
		public string ApprovalDecisionMaker
		{
			get
			{
				return this.approvalDecisionMaker;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00009FC0 File Offset: 0x00008FC0
		public DateTime ApprovalDecisionTime
		{
			get
			{
				return this.approvalDecisionTime;
			}
		}

		// Token: 0x0400011B RID: 283
		private bool isUndecidedApprovalRequest;

		// Token: 0x0400011C RID: 284
		private int approvalDecision;

		// Token: 0x0400011D RID: 285
		private string approvalDecisionMaker;

		// Token: 0x0400011E RID: 286
		private DateTime approvalDecisionTime;
	}
}
