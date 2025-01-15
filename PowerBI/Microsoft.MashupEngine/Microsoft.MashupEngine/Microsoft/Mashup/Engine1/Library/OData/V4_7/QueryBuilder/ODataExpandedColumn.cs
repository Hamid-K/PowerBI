using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007CA RID: 1994
	internal class ODataExpandedColumn
	{
		// Token: 0x06003A07 RID: 14855 RVA: 0x000BB93F File Offset: 0x000B9B3F
		public ODataExpandedColumn(string columnToExpandName, Keys fieldsToProject, IEnumerable<ODataExpandedColumn> innerExpandedColumns, FunctionValue selectRowsCondition)
		{
			this.columnToExpandName = columnToExpandName;
			this.fieldsToProject = fieldsToProject;
			this.innerExpandedColumns = innerExpandedColumns;
			this.selectRowsCondition = selectRowsCondition;
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000BB964 File Offset: 0x000B9B64
		public string ColumnToExpandName
		{
			get
			{
				return this.columnToExpandName;
			}
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000BB96C File Offset: 0x000B9B6C
		public Keys FieldsToProject
		{
			get
			{
				return this.fieldsToProject;
			}
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06003A0A RID: 14858 RVA: 0x000BB974 File Offset: 0x000B9B74
		public IEnumerable<ODataExpandedColumn> InnerExpandedColumns
		{
			get
			{
				return this.innerExpandedColumns;
			}
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000BB97C File Offset: 0x000B9B7C
		public FunctionValue SelectRowsCondition
		{
			get
			{
				return this.selectRowsCondition;
			}
		}

		// Token: 0x04001E1D RID: 7709
		private readonly string columnToExpandName;

		// Token: 0x04001E1E RID: 7710
		private readonly Keys fieldsToProject;

		// Token: 0x04001E1F RID: 7711
		private readonly IEnumerable<ODataExpandedColumn> innerExpandedColumns;

		// Token: 0x04001E20 RID: 7712
		private readonly FunctionValue selectRowsCondition;
	}
}
