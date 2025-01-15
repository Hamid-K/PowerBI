using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000044 RID: 68
	internal sealed class XmlaLocationReference
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x0001C3D0 File Offset: 0x0001A5D0
		internal XmlaLocationReference(string dimension, string hierarchy, string attribute, string cube, string measureGroup, string memberName, string role, string tableName, string columnName, string partitionName, string measureName, string calculationItemName, string roleName)
		{
			this.dimension = dimension;
			this.hierarchy = hierarchy;
			this.attribute = attribute;
			this.cube = cube;
			this.measureGroup = measureGroup;
			this.memberName = memberName;
			this.role = role;
			this.tableName = tableName;
			this.columnName = columnName;
			this.partitionName = partitionName;
			this.measureName = measureName;
			this.calculationItemName = calculationItemName;
			this.roleName = roleName;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001C448 File Offset: 0x0001A648
		public string Dimension
		{
			get
			{
				return this.dimension;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001C450 File Offset: 0x0001A650
		public string Hierarchy
		{
			get
			{
				return this.hierarchy;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001C458 File Offset: 0x0001A658
		public string Attribute
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0001C460 File Offset: 0x0001A660
		public string Cube
		{
			get
			{
				return this.cube;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001C468 File Offset: 0x0001A668
		public string MeasureGroup
		{
			get
			{
				return this.measureGroup;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001C470 File Offset: 0x0001A670
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001C478 File Offset: 0x0001A678
		public string Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0001C480 File Offset: 0x0001A680
		public string TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001C488 File Offset: 0x0001A688
		public string ColumnName
		{
			get
			{
				return this.columnName;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0001C490 File Offset: 0x0001A690
		public string PartitionName
		{
			get
			{
				return this.partitionName;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0001C498 File Offset: 0x0001A698
		public string MeasureName
		{
			get
			{
				return this.measureName;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0001C4A0 File Offset: 0x0001A6A0
		public string CalculationItemName
		{
			get
			{
				return this.calculationItemName;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001C4A8 File Offset: 0x0001A6A8
		public string RoleName
		{
			get
			{
				return this.roleName;
			}
		}

		// Token: 0x040003A4 RID: 932
		private string dimension;

		// Token: 0x040003A5 RID: 933
		private string hierarchy;

		// Token: 0x040003A6 RID: 934
		private string attribute;

		// Token: 0x040003A7 RID: 935
		private string cube;

		// Token: 0x040003A8 RID: 936
		private string measureGroup;

		// Token: 0x040003A9 RID: 937
		private string memberName;

		// Token: 0x040003AA RID: 938
		private string role;

		// Token: 0x040003AB RID: 939
		private string tableName;

		// Token: 0x040003AC RID: 940
		private string columnName;

		// Token: 0x040003AD RID: 941
		private string partitionName;

		// Token: 0x040003AE RID: 942
		private string measureName;

		// Token: 0x040003AF RID: 943
		private string calculationItemName;

		// Token: 0x040003B0 RID: 944
		private string roleName;
	}
}
