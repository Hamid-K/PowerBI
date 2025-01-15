using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000A7 RID: 167
	public sealed class Conflict : ComplexProperty
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x000196A4 File Offset: 0x000186A4
		internal Conflict(ConflictType conflictType)
		{
			this.conflictType = conflictType;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000196B4 File Offset: 0x000186B4
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "NumberOfMembers")
				{
					this.numberOfMembers = reader.ReadElementValue<int>();
					return true;
				}
				if (localName == "NumberOfMembersAvailable")
				{
					this.numberOfMembersAvailable = reader.ReadElementValue<int>();
					return true;
				}
				if (localName == "NumberOfMembersWithConflict")
				{
					this.numberOfMembersWithConflict = reader.ReadElementValue<int>();
					return true;
				}
				if (localName == "NumberOfMembersWithNoData")
				{
					this.numberOfMembersWithNoData = reader.ReadElementValue<int>();
					return true;
				}
				if (localName == "BusyType")
				{
					this.freeBusyStatus = reader.ReadElementValue<LegacyFreeBusyStatus>();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00019758 File Offset: 0x00018758
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "NumberOfMembers"))
					{
						if (!(text2 == "NumberOfMembersAvailable"))
						{
							if (!(text2 == "NumberOfMembersWithConflict"))
							{
								if (!(text2 == "NumberOfMembersWithNoData"))
								{
									if (text2 == "BusyType")
									{
										this.freeBusyStatus = jsonProperty.ReadEnumValue<LegacyFreeBusyStatus>(text);
									}
								}
								else
								{
									this.numberOfMembersWithNoData = jsonProperty.ReadAsInt(text);
								}
							}
							else
							{
								this.numberOfMembersWithConflict = jsonProperty.ReadAsInt(text);
							}
						}
						else
						{
							this.numberOfMembersAvailable = jsonProperty.ReadAsInt(text);
						}
					}
					else
					{
						this.numberOfMembers = jsonProperty.ReadAsInt(text);
					}
				}
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00019840 File Offset: 0x00018840
		public ConflictType ConflictType
		{
			get
			{
				return this.conflictType;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00019848 File Offset: 0x00018848
		public int NumberOfMembers
		{
			get
			{
				return this.numberOfMembers;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00019850 File Offset: 0x00018850
		public int NumberOfMembersAvailable
		{
			get
			{
				return this.numberOfMembersAvailable;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00019858 File Offset: 0x00018858
		public int NumberOfMembersWithConflict
		{
			get
			{
				return this.numberOfMembersWithConflict;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00019860 File Offset: 0x00018860
		public int NumberOfMembersWithNoData
		{
			get
			{
				return this.numberOfMembersWithNoData;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00019868 File Offset: 0x00018868
		public LegacyFreeBusyStatus FreeBusyStatus
		{
			get
			{
				return this.freeBusyStatus;
			}
		}

		// Token: 0x04000276 RID: 630
		private ConflictType conflictType;

		// Token: 0x04000277 RID: 631
		private int numberOfMembers;

		// Token: 0x04000278 RID: 632
		private int numberOfMembersAvailable;

		// Token: 0x04000279 RID: 633
		private int numberOfMembersWithConflict;

		// Token: 0x0400027A RID: 634
		private int numberOfMembersWithNoData;

		// Token: 0x0400027B RID: 635
		private LegacyFreeBusyStatus freeBusyStatus;
	}
}
