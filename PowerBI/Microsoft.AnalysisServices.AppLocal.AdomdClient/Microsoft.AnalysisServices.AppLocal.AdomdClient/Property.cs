using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000EA RID: 234
	public sealed class Property : ISubordinateObject
	{
		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002F8EC File Offset: 0x0002DAEC
		internal Property(DataRow dataRow, int propIndex, object propertyParent)
		{
			this.dataRow = dataRow;
			this.propColumn = this.dataRow.Table.Columns[propIndex];
			this.propIndex = propIndex;
			this.propertyParent = propertyParent;
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0002F925 File Offset: 0x0002DB25
		public string Name
		{
			get
			{
				return this.propColumn.Caption;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0002F934 File Offset: 0x0002DB34
		public object Value
		{
			get
			{
				object property = AdomdUtils.GetProperty(this.dataRow, this.propIndex);
				if (property is DBNull)
				{
					return null;
				}
				return property;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0002F95E File Offset: 0x0002DB5E
		public Type Type
		{
			get
			{
				return FormattersHelpers.GetColumnType(this.propColumn);
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002F96B File Offset: 0x0002DB6B
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0002F973 File Offset: 0x0002DB73
		object ISubordinateObject.Parent
		{
			get
			{
				return this.propertyParent;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0002F97B File Offset: 0x0002DB7B
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.propIndex;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0002F983 File Offset: 0x0002DB83
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Property);
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002F98F File Offset: 0x0002DB8F
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002F9B2 File Offset: 0x0002DBB2
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002F9C0 File Offset: 0x0002DBC0
		public static bool operator ==(Property o1, Property o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002F9C9 File Offset: 0x0002DBC9
		public static bool operator !=(Property o1, Property o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x0400081E RID: 2078
		private DataRow dataRow;

		// Token: 0x0400081F RID: 2079
		private DataColumn propColumn;

		// Token: 0x04000820 RID: 2080
		private int propIndex;

		// Token: 0x04000821 RID: 2081
		private object propertyParent;

		// Token: 0x04000822 RID: 2082
		private int hashCode;

		// Token: 0x04000823 RID: 2083
		private bool hashCodeCalculated;
	}
}
