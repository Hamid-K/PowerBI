using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200001E RID: 30
	[CatalogItemType(ItemType.Kpi)]
	internal sealed class KpiCatalogItem : CatalogItem
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00004F8E File Offset: 0x0000318E
		internal KpiCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000050AD File Offset: 0x000032AD
		protected override void ContentLoadSecurityCheck()
		{
			this.ThrowIfNoAccess(ResourceOperation.ReadContent);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000050B6 File Offset: 0x000032B6
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000050D8 File Offset: 0x000032D8
		internal DataSetInfoCollection SharedDataSets
		{
			get
			{
				if (this.m_sharedDataSets == null)
				{
					this.m_sharedDataSets = base.GetSyncedItemSharedDataSets(this.m_itemID);
				}
				return this.m_sharedDataSets;
			}
			set
			{
				this.m_sharedDataSets = value;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000050E1 File Offset: 0x000032E1
		internal void ThrowIfNoAccess(ResourceOperation operation)
		{
			if (!base.Service.SecMgr.CheckAccess(base.ThisItemType, base.SecurityDescriptor, operation, base.ItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005120 File Offset: 0x00003320
		internal override void Create()
		{
			base.Create();
			this.SaveDataSets();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000512E File Offset: 0x0000332E
		protected override void Update()
		{
			this.SaveDataSets();
			base.AdjustModificationInfo();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000513C File Offset: 0x0000333C
		internal void SaveDataSets()
		{
			base.Service.Storage.DeleteDataSets(base.ItemID);
			base.Service.AddDataSets(base.ItemID, this.SharedDataSets, null);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000516C File Offset: 0x0000336C
		internal override void ProtectProperties()
		{
			string text = base.Properties["EncryptedValues"];
			if (string.IsNullOrEmpty(text) || !text.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
			{
				this.ProtectProperty("Value.Value");
				this.ProtectProperty("Status.Value");
				this.ProtectProperty("TrendSet.Value");
				this.ProtectProperty("Goal.Value");
				this.ProtectProperty(string.Format("{0}.Parameters", "Value"));
				this.ProtectProperty(string.Format("{0}.Parameters", "Status"));
				this.ProtectProperty(string.Format("{0}.Parameters", "TrendSet"));
				this.ProtectProperty(string.Format("{0}.Parameters", "Goal"));
				this.ProtectProperty("DrillthroughTarget.Url");
				this.ProtectProperty("DrillthroughTarget.Parameters");
				base.Properties["EncryptedValues"] = bool.TrueString;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005250 File Offset: 0x00003450
		internal override void UnprotectProperties()
		{
			string text = base.Properties["EncryptedValues"];
			if (!string.IsNullOrEmpty(text) && text.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
			{
				this.UnprotectProperty("Value.Value");
				this.UnprotectProperty("Status.Value");
				this.UnprotectProperty("TrendSet.Value");
				this.UnprotectProperty("Goal.Value");
				this.UnprotectProperty(string.Format("{0}.Parameters", "Value"));
				this.UnprotectProperty(string.Format("{0}.Parameters", "Status"));
				this.UnprotectProperty(string.Format("{0}.Parameters", "TrendSet"));
				this.UnprotectProperty(string.Format("{0}.Parameters", "Goal"));
				this.UnprotectProperty("DrillthroughTarget.Url");
				this.UnprotectProperty("DrillthroughTarget.Parameters");
				base.Properties["EncryptedValues"] = bool.FalseString;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005335 File Offset: 0x00003535
		private void ProtectProperty(string propertyName)
		{
			base.Properties.Protect(propertyName);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005344 File Offset: 0x00003544
		private void UnprotectProperty(string propertyName)
		{
			try
			{
				base.Properties.UnProtect(propertyName);
			}
			catch (Exception)
			{
				base.Properties[propertyName] = string.Empty;
			}
		}

		// Token: 0x040000B1 RID: 177
		private DataSetInfoCollection m_sharedDataSets;
	}
}
