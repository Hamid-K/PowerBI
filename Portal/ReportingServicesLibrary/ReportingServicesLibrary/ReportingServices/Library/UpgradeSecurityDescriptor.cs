using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000262 RID: 610
	internal sealed class UpgradeSecurityDescriptor : UpgradeMultipleItemsTask
	{
		// Token: 0x0600161A RID: 5658 RVA: 0x00058504 File Offset: 0x00056704
		public UpgradeSecurityDescriptor(UpgradePollWorker pollWorker)
			: base(pollWorker)
		{
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00058510 File Offset: 0x00056710
		protected override UpgradeMultipleItemsTask.ItemCollection GetItemsToUpdate()
		{
			UpgradeMultipleItemsTask.ItemCollection itemCollection = new UpgradeMultipleItemsTask.ItemCollection();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetPolicyRoots", null))
			{
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						itemCollection.Add(new UpgradeSecurityDescriptor.SecurityDescriptorItemInfo
						{
							Path = new ExternalItemPath(dataReader.GetString(0)),
							Type = (ItemType)dataReader.GetInt32(1)
						});
					}
				}
			}
			return itemCollection;
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x000585A0 File Offset: 0x000567A0
		public override string Name
		{
			get
			{
				return "UpgradedSecurityDesc";
			}
		}

		// Token: 0x04000812 RID: 2066
		private const string _UpgradeItemKey = "UpgradedSecurityDesc";

		// Token: 0x020004C0 RID: 1216
		private sealed class SecurityDescriptorItemInfo : UpgradeMultipleItemsTask.ItemInfo
		{
			// Token: 0x17000A92 RID: 2706
			// (get) Token: 0x0600242D RID: 9261 RVA: 0x00085B60 File Offset: 0x00083D60
			public override string TraceIdentifier
			{
				get
				{
					return this.Path.Value;
				}
			}

			// Token: 0x0600242E RID: 9262 RVA: 0x00085B70 File Offset: 0x00083D70
			public override void Upgrade(Storage storage)
			{
				Security security = new Security(new UserContext(WebConfigUtil.WebServerAuthMode), false);
				security.ConnectionManager = storage.ConnectionManager;
				bool flag;
				string text;
				security.GetPolicies(this.Path, out flag, out text);
				if (!flag)
				{
					security.RemoveBadUserNames(ref text);
					security.SetCatalogItemPolicies(this.Path, this.Type, text);
				}
			}

			// Token: 0x040010FF RID: 4351
			public ExternalItemPath Path = ExternalItemPath.Empty;

			// Token: 0x04001100 RID: 4352
			public ItemType Type;
		}
	}
}
