using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200003F RID: 63
	internal sealed class TableColumn
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x0001224F File Offset: 0x0001044F
		internal TableColumn(Table owner, TableColumn columnDef, int index)
		{
			this.m_owner = owner;
			this.m_columnDef = columnDef;
			this.m_index = index;
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001226C File Offset: 0x0001046C
		public string UniqueName
		{
			get
			{
				if (this.ColumnInstance == null)
				{
					return null;
				}
				return this.ColumnInstance.UniqueName.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001229B File Offset: 0x0001049B
		public ReportSize Width
		{
			get
			{
				if (this.m_columnDef.WidthForRendering == null)
				{
					this.m_columnDef.WidthForRendering = new ReportSize(this.m_columnDef.Width, this.m_columnDef.WidthValue);
				}
				return this.m_columnDef.WidthForRendering;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000122DC File Offset: 0x000104DC
		public bool Hidden
		{
			get
			{
				if (this.ColumnInstance == null)
				{
					return RenderingContext.GetDefinitionHidden(this.m_columnDef.Visibility);
				}
				if (this.m_columnDef.Visibility == null)
				{
					return false;
				}
				if (this.m_columnDef.Visibility.Toggle != null)
				{
					return this.m_owner.RenderingContext.IsItemHidden(this.ColumnInstance.UniqueName, false);
				}
				return this.ColumnInstance.StartHidden;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0001234B File Offset: 0x0001054B
		public bool HasToggle
		{
			get
			{
				return Visibility.HasToggle(this.m_columnDef.Visibility);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001235D File Offset: 0x0001055D
		public string ToggleItem
		{
			get
			{
				if (this.m_columnDef.Visibility == null)
				{
					return null;
				}
				return this.m_columnDef.Visibility.Toggle;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0001237E File Offset: 0x0001057E
		public TextBox ToggleParent
		{
			get
			{
				if (!this.HasToggle)
				{
					return null;
				}
				if (this.ColumnInstance == null)
				{
					return null;
				}
				return this.m_owner.RenderingContext.GetToggleParent(this.ColumnInstance.UniqueName);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x000123AF File Offset: 0x000105AF
		public SharedHiddenState SharedHidden
		{
			get
			{
				return Visibility.GetSharedHidden(this.m_columnDef.Visibility);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x000123C1 File Offset: 0x000105C1
		public bool IsToggleChild
		{
			get
			{
				return this.ColumnInstance != null && this.m_owner.RenderingContext.IsToggleChild(this.ColumnInstance.UniqueName);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x000123E8 File Offset: 0x000105E8
		internal TableColumnInstance ColumnInstance
		{
			get
			{
				if (this.m_columnInstance == null)
				{
					TableInstanceInfo tableInstanceInfo = (TableInstanceInfo)this.m_owner.InstanceInfo;
					if (tableInstanceInfo != null)
					{
						TableColumnInstance[] columnInstances = tableInstanceInfo.ColumnInstances;
						if (columnInstances != null)
						{
							this.m_columnInstance = columnInstances[this.m_index];
						}
					}
				}
				return this.m_columnInstance;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001242F File Offset: 0x0001062F
		public bool FixedHeader
		{
			get
			{
				return this.m_columnDef.FixedHeader;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0001243C File Offset: 0x0001063C
		internal TableColumn ColumnDefinition
		{
			get
			{
				return this.m_columnDef;
			}
		}

		// Token: 0x04000132 RID: 306
		private Table m_owner;

		// Token: 0x04000133 RID: 307
		private TableColumn m_columnDef;

		// Token: 0x04000134 RID: 308
		private TableColumnInstance m_columnInstance;

		// Token: 0x04000135 RID: 309
		private int m_index;
	}
}
