using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003F6 RID: 1014
	public class MatrixItem : DataRegionItem
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x0007F23B File Offset: 0x0007D43B
		// (set) Token: 0x0600202A RID: 8234 RVA: 0x0007F243 File Offset: 0x0007D443
		public MatrixCell Corner
		{
			get
			{
				return this.m_corner;
			}
			set
			{
				this.m_corner = value;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x0007F24C File Offset: 0x0007D44C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<ColumnGrouping> ColumnGroupings
		{
			get
			{
				return this.m_columnGroups;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x0007F254 File Offset: 0x0007D454
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<RowGrouping> RowGroupings
		{
			get
			{
				return this.m_rowGroups;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x0007F25C File Offset: 0x0007D45C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<MatrixColumn> MatrixColumns
		{
			get
			{
				return this.m_matrixColumns;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x0007F264 File Offset: 0x0007D464
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<MatrixRow> MatrixRows
		{
			get
			{
				return this.m_matrixRows;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x0007F26C File Offset: 0x0007D46C
		// (set) Token: 0x06002030 RID: 8240 RVA: 0x0007F274 File Offset: 0x0007D474
		[DefaultValue("LTR")]
		public string LayoutDirection
		{
			get
			{
				return this.m_layoutDirection;
			}
			set
			{
				StringListConverter.ValidStandardValue("LayoutDirection", LayoutDirectionStringConverter.StringValuesArray, LayoutDirectionStringConverter.StringValuesArray[0], value, ref this.m_layoutDirection);
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x0007F293 File Offset: 0x0007D493
		// (set) Token: 0x06002032 RID: 8242 RVA: 0x0007F29B File Offset: 0x0007D49B
		[DefaultValue(0)]
		public int GroupsBeforeRowHeaders
		{
			get
			{
				return this.m_groupsBeforeRowHeaders;
			}
			set
			{
				Utils.ValidateValueRange("GroupsBeforeRowHeaders", value, 0, null);
				this.m_groupsBeforeRowHeaders = value;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x0007F2B6 File Offset: 0x0007D4B6
		// (set) Token: 0x06002034 RID: 8244 RVA: 0x0007F2BE File Offset: 0x0007D4BE
		[DefaultValue("")]
		public string CellDataElementName
		{
			get
			{
				return this.m_cellDataElementName;
			}
			set
			{
				this.m_cellDataElementName = value;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x0007F2C7 File Offset: 0x0007D4C7
		// (set) Token: 0x06002036 RID: 8246 RVA: 0x0007F2CF File Offset: 0x0007D4CF
		[DefaultValue(CellDataElementOutputs.Output)]
		public CellDataElementOutputs CellDataElementOutput
		{
			get
			{
				return this.m_cellDataElementOutput;
			}
			set
			{
				this.m_cellDataElementOutput = value;
			}
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x0007F2D8 File Offset: 0x0007D4D8
		public MatrixItem()
		{
			this.m_columnGroups = new List<ColumnGrouping>();
			this.m_rowGroups = new List<RowGrouping>();
			this.m_matrixRows = new List<MatrixRow>();
			this.m_matrixColumns = new List<MatrixColumn>();
		}

		// Token: 0x04000E03 RID: 3587
		private List<ColumnGrouping> m_columnGroups;

		// Token: 0x04000E04 RID: 3588
		private List<RowGrouping> m_rowGroups;

		// Token: 0x04000E05 RID: 3589
		private string m_layoutDirection = "LTR";

		// Token: 0x04000E06 RID: 3590
		private int m_groupsBeforeRowHeaders;

		// Token: 0x04000E07 RID: 3591
		private List<MatrixRow> m_matrixRows;

		// Token: 0x04000E08 RID: 3592
		private List<MatrixColumn> m_matrixColumns;

		// Token: 0x04000E09 RID: 3593
		private MatrixCell m_corner;

		// Token: 0x04000E0A RID: 3594
		private string m_cellDataElementName;

		// Token: 0x04000E0B RID: 3595
		private CellDataElementOutputs m_cellDataElementOutput;
	}
}
