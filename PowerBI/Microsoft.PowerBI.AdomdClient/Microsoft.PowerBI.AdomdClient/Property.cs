using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000EA RID: 234
	public sealed class Property : ISubordinateObject
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002F5BC File Offset: 0x0002D7BC
		internal Property(DataRow dataRow, int propIndex, object propertyParent)
		{
			this.dataRow = dataRow;
			this.propColumn = this.dataRow.Table.Columns[propIndex];
			this.propIndex = propIndex;
			this.propertyParent = propertyParent;
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002F5F5 File Offset: 0x0002D7F5
		public string Name
		{
			get
			{
				return this.propColumn.Caption;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0002F604 File Offset: 0x0002D804
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

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002F62E File Offset: 0x0002D82E
		public Type Type
		{
			get
			{
				return FormattersHelpers.GetColumnType(this.propColumn);
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002F63B File Offset: 0x0002D83B
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0002F643 File Offset: 0x0002D843
		object ISubordinateObject.Parent
		{
			get
			{
				return this.propertyParent;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0002F64B File Offset: 0x0002D84B
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.propIndex;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002F653 File Offset: 0x0002D853
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Property);
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002F65F File Offset: 0x0002D85F
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002F682 File Offset: 0x0002D882
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002F690 File Offset: 0x0002D890
		public static bool operator ==(Property o1, Property o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002F699 File Offset: 0x0002D899
		public static bool operator !=(Property o1, Property o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x04000811 RID: 2065
		private DataRow dataRow;

		// Token: 0x04000812 RID: 2066
		private DataColumn propColumn;

		// Token: 0x04000813 RID: 2067
		private int propIndex;

		// Token: 0x04000814 RID: 2068
		private object propertyParent;

		// Token: 0x04000815 RID: 2069
		private int hashCode;

		// Token: 0x04000816 RID: 2070
		private bool hashCodeCalculated;
	}
}
