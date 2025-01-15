using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000044 RID: 68
	internal sealed class XmlaLocationReference
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x0001C0A0 File Offset: 0x0001A2A0
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001C118 File Offset: 0x0001A318
		public string Dimension
		{
			get
			{
				return this.dimension;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0001C120 File Offset: 0x0001A320
		public string Hierarchy
		{
			get
			{
				return this.hierarchy;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001C128 File Offset: 0x0001A328
		public string Attribute
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001C130 File Offset: 0x0001A330
		public string Cube
		{
			get
			{
				return this.cube;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001C138 File Offset: 0x0001A338
		public string MeasureGroup
		{
			get
			{
				return this.measureGroup;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001C140 File Offset: 0x0001A340
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001C148 File Offset: 0x0001A348
		public string Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001C150 File Offset: 0x0001A350
		public string TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001C158 File Offset: 0x0001A358
		public string ColumnName
		{
			get
			{
				return this.columnName;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001C160 File Offset: 0x0001A360
		public string PartitionName
		{
			get
			{
				return this.partitionName;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0001C168 File Offset: 0x0001A368
		public string MeasureName
		{
			get
			{
				return this.measureName;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001C170 File Offset: 0x0001A370
		public string CalculationItemName
		{
			get
			{
				return this.calculationItemName;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001C178 File Offset: 0x0001A378
		public string RoleName
		{
			get
			{
				return this.roleName;
			}
		}

		// Token: 0x04000397 RID: 919
		private string dimension;

		// Token: 0x04000398 RID: 920
		private string hierarchy;

		// Token: 0x04000399 RID: 921
		private string attribute;

		// Token: 0x0400039A RID: 922
		private string cube;

		// Token: 0x0400039B RID: 923
		private string measureGroup;

		// Token: 0x0400039C RID: 924
		private string memberName;

		// Token: 0x0400039D RID: 925
		private string role;

		// Token: 0x0400039E RID: 926
		private string tableName;

		// Token: 0x0400039F RID: 927
		private string columnName;

		// Token: 0x040003A0 RID: 928
		private string partitionName;

		// Token: 0x040003A1 RID: 929
		private string measureName;

		// Token: 0x040003A2 RID: 930
		private string calculationItemName;

		// Token: 0x040003A3 RID: 931
		private string roleName;
	}
}
