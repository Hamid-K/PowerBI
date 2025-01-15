using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000047 RID: 71
	internal class TableRow
	{
		// Token: 0x0600059B RID: 1435 RVA: 0x0001314B File Offset: 0x0001134B
		internal TableRow(Table owner, TableRow rowDef, TableRowInstance rowInstance)
		{
			this.m_owner = owner;
			this.m_rowDef = rowDef;
			this.m_rowInstance = rowInstance;
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00013168 File Offset: 0x00011368
		public string ID
		{
			get
			{
				if (this.m_rowDef.RenderingModelID == null)
				{
					this.m_rowDef.RenderingModelID = this.m_rowDef.ID.ToString(CultureInfo.InvariantCulture);
				}
				return this.m_rowDef.RenderingModelID;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x000131B0 File Offset: 0x000113B0
		public string UniqueName
		{
			get
			{
				if (this.m_rowInstance == null)
				{
					return null;
				}
				return this.m_rowInstance.UniqueName.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000131DF File Offset: 0x000113DF
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0001320B File Offset: 0x0001140B
		public object SharedRenderingInfo
		{
			get
			{
				return this.m_owner.RenderingContext.RenderingInfoManager.SharedRenderingInfo[this.m_rowDef.ID];
			}
			set
			{
				this.m_owner.RenderingContext.RenderingInfoManager.SharedRenderingInfo[this.m_rowDef.ID] = value;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00013238 File Offset: 0x00011438
		public ReportSize Height
		{
			get
			{
				if (this.m_rowDef.HeightForRendering == null)
				{
					this.m_rowDef.HeightForRendering = new ReportSize(this.m_rowDef.Height, this.m_rowDef.HeightValue);
				}
				return this.m_rowDef.HeightForRendering;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00013278 File Offset: 0x00011478
		public TableCellCollection TableCellCollection
		{
			get
			{
				TableCellCollection tableCellCollection = this.m_rowCells;
				if (this.m_rowCells == null)
				{
					tableCellCollection = new TableCellCollection(this.m_owner, this.m_rowDef, this.m_rowInstance);
					if (this.m_owner.RenderingContext.CacheState)
					{
						this.m_rowCells = tableCellCollection;
					}
				}
				return tableCellCollection;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000132C8 File Offset: 0x000114C8
		public virtual bool Hidden
		{
			get
			{
				if (this.m_rowInstance == null)
				{
					return RenderingContext.GetDefinitionHidden(this.m_rowDef.Visibility);
				}
				if (this.m_rowDef.Visibility == null)
				{
					return false;
				}
				if (this.m_rowDef.Visibility.Toggle != null)
				{
					return this.m_owner.RenderingContext.IsItemHidden(this.m_rowInstance.UniqueName, false);
				}
				return this.InstanceInfo.StartHidden;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00013337 File Offset: 0x00011537
		public virtual bool HasToggle
		{
			get
			{
				return Visibility.HasToggle(this.m_rowDef.Visibility);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00013349 File Offset: 0x00011549
		public virtual string ToggleItem
		{
			get
			{
				if (this.m_rowDef.Visibility == null)
				{
					return null;
				}
				return this.m_rowDef.Visibility.Toggle;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001336A File Offset: 0x0001156A
		public virtual TextBox ToggleParent
		{
			get
			{
				if (!this.HasToggle)
				{
					return null;
				}
				if (this.m_rowInstance == null)
				{
					return null;
				}
				return this.m_owner.RenderingContext.GetToggleParent(this.m_rowInstance.UniqueName);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001339B File Offset: 0x0001159B
		public virtual SharedHiddenState SharedHidden
		{
			get
			{
				return Visibility.GetSharedHidden(this.m_rowDef.Visibility);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x000133AD File Offset: 0x000115AD
		public virtual bool IsToggleChild
		{
			get
			{
				return this.m_rowInstance != null && this.m_owner.RenderingContext.IsToggleChild(this.m_rowInstance.UniqueName);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000133D4 File Offset: 0x000115D4
		internal TableRowInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_rowInstance == null)
				{
					return null;
				}
				if (this.m_tableRowInstanceInfo == null)
				{
					this.m_tableRowInstanceInfo = this.m_rowInstance.GetInstanceInfo(this.m_owner.RenderingContext.ChunkManager);
				}
				return this.m_tableRowInstanceInfo;
			}
		}

		// Token: 0x04000156 RID: 342
		internal Table m_owner;

		// Token: 0x04000157 RID: 343
		internal TableRow m_rowDef;

		// Token: 0x04000158 RID: 344
		internal TableRowInstance m_rowInstance;

		// Token: 0x04000159 RID: 345
		internal TableCellCollection m_rowCells;

		// Token: 0x0400015A RID: 346
		internal TableRowInstanceInfo m_tableRowInstanceInfo;
	}
}
