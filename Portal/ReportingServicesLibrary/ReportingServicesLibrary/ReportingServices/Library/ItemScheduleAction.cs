using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000089 RID: 137
	internal class ItemScheduleAction
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x00017758 File Offset: 0x00015958
		public ItemScheduleAction()
		{
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000177BF File Offset: 0x000159BF
		public ItemScheduleAction(IDataRecord record)
			: this(record, false)
		{
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000177CC File Offset: 0x000159CC
		public ItemScheduleAction(IDataRecord record, bool reportActionTableOnly)
		{
			this.Action = (ReportScheduleActions)record.GetInt32(0);
			this.ScheduleID = record.GetGuid(1);
			this.ItemID = record.GetGuid(2);
			if (!record.IsDBNull(3))
			{
				this.SubscriptionID = record.GetGuid(3);
			}
			this.ItemPath = new CatalogItemPath(record.GetString(4));
			this.ItemType = (ItemType)record.GetInt32(5);
			if (reportActionTableOnly)
			{
				return;
			}
			this.ItemName = record.GetString(6);
			if (!record.IsDBNull(7))
			{
				this.Description = record.GetString(7);
			}
			this.ModifiedDate = record.GetDateTime(8);
			this.ModifiedUser = UserUtil.GetUserNameBySid(record, 9, 10);
			if (!record.IsDBNull(11))
			{
				this.ReportSize = DBInterface.ReadInt64AsInt32(record, 11);
			}
			if (!record.IsDBNull(12))
			{
				this.ExecutionTime = record.GetDateTime(12);
			}
			this.ScheduleType = (ScheduleType)record.GetInt32(13);
			if (!record.IsDBNull(14))
			{
				this.ReportSecurityDescriptor = DataReaderHelper.ReadAllBytes(record, 14);
			}
			if (!record.IsDBNull(15))
			{
				this.ItemZone = record.GetInt32(15);
			}
		}

		// Token: 0x04000310 RID: 784
		internal ReportScheduleActions Action;

		// Token: 0x04000311 RID: 785
		internal CatalogItemPath ItemPath = CatalogItemPath.Empty;

		// Token: 0x04000312 RID: 786
		internal string ItemName = "";

		// Token: 0x04000313 RID: 787
		internal ItemType ItemType;

		// Token: 0x04000314 RID: 788
		internal int ItemZone;

		// Token: 0x04000315 RID: 789
		internal Guid ScheduleID = Guid.Empty;

		// Token: 0x04000316 RID: 790
		internal Guid ItemID = Guid.Empty;

		// Token: 0x04000317 RID: 791
		internal Guid SubscriptionID = Guid.Empty;

		// Token: 0x04000318 RID: 792
		internal ScheduleType ScheduleType = ScheduleType.Scoped;

		// Token: 0x04000319 RID: 793
		internal byte[] ReportSecurityDescriptor;

		// Token: 0x0400031A RID: 794
		internal string Description;

		// Token: 0x0400031B RID: 795
		internal DateTime ModifiedDate = DateTime.MinValue;

		// Token: 0x0400031C RID: 796
		internal string ModifiedUser;

		// Token: 0x0400031D RID: 797
		internal int ReportSize;

		// Token: 0x0400031E RID: 798
		internal DateTime ExecutionTime = DateTime.MinValue;

		// Token: 0x0200045A RID: 1114
		private enum ActionSqlProjection
		{
			// Token: 0x04000F96 RID: 3990
			Action,
			// Token: 0x04000F97 RID: 3991
			ScheduleID,
			// Token: 0x04000F98 RID: 3992
			ItemID,
			// Token: 0x04000F99 RID: 3993
			SubscriptionID,
			// Token: 0x04000F9A RID: 3994
			ItemPath,
			// Token: 0x04000F9B RID: 3995
			ItemType,
			// Token: 0x04000F9C RID: 3996
			ItemName,
			// Token: 0x04000F9D RID: 3997
			Description,
			// Token: 0x04000F9E RID: 3998
			ModifiedDate,
			// Token: 0x04000F9F RID: 3999
			ModifiedUserNameBySid,
			// Token: 0x04000FA0 RID: 4000
			ModifiedUserNameBackup,
			// Token: 0x04000FA1 RID: 4001
			ReportSize,
			// Token: 0x04000FA2 RID: 4002
			ExecutionTime,
			// Token: 0x04000FA3 RID: 4003
			ScheduleType,
			// Token: 0x04000FA4 RID: 4004
			ReportSecurityDescriptor,
			// Token: 0x04000FA5 RID: 4005
			ItemZone
		}
	}
}
