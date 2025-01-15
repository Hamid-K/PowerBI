using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000061 RID: 97
	[Serializable]
	public sealed class XmlaLocationReference
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x0001FD88 File Offset: 0x0001DF88
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0001FE00 File Offset: 0x0001E000
		public string Dimension
		{
			get
			{
				return this.dimension;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001FE08 File Offset: 0x0001E008
		public string Hierarchy
		{
			get
			{
				return this.hierarchy;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001FE10 File Offset: 0x0001E010
		public string Attribute
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001FE18 File Offset: 0x0001E018
		public string Cube
		{
			get
			{
				return this.cube;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001FE20 File Offset: 0x0001E020
		public string MeasureGroup
		{
			get
			{
				return this.measureGroup;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001FE28 File Offset: 0x0001E028
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001FE30 File Offset: 0x0001E030
		public string Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0001FE38 File Offset: 0x0001E038
		public string TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001FE40 File Offset: 0x0001E040
		public string ColumnName
		{
			get
			{
				return this.columnName;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0001FE48 File Offset: 0x0001E048
		public string PartitionName
		{
			get
			{
				return this.partitionName;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001FE50 File Offset: 0x0001E050
		public string MeasureName
		{
			get
			{
				return this.measureName;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001FE58 File Offset: 0x0001E058
		public string CalculationItemName
		{
			get
			{
				return this.calculationItemName;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001FE60 File Offset: 0x0001E060
		public string RoleName
		{
			get
			{
				return this.roleName;
			}
		}

		// Token: 0x040003D3 RID: 979
		private string dimension;

		// Token: 0x040003D4 RID: 980
		private string hierarchy;

		// Token: 0x040003D5 RID: 981
		private string attribute;

		// Token: 0x040003D6 RID: 982
		private string cube;

		// Token: 0x040003D7 RID: 983
		private string measureGroup;

		// Token: 0x040003D8 RID: 984
		private string memberName;

		// Token: 0x040003D9 RID: 985
		private string role;

		// Token: 0x040003DA RID: 986
		private string tableName;

		// Token: 0x040003DB RID: 987
		private string columnName;

		// Token: 0x040003DC RID: 988
		private string partitionName;

		// Token: 0x040003DD RID: 989
		private string measureName;

		// Token: 0x040003DE RID: 990
		private string calculationItemName;

		// Token: 0x040003DF RID: 991
		private string roleName;
	}
}
