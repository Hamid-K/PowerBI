using System;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001973 RID: 6515
	internal class DataSourceKey : IEquatable<DataSourceKey>
	{
		// Token: 0x0600A558 RID: 42328 RVA: 0x002237C6 File Offset: 0x002219C6
		public DataSourceKey(string dataSourceType, string dataSource)
		{
			this.dataSourceType = dataSourceType;
			this.dataSource = dataSource;
		}

		// Token: 0x17002A3B RID: 10811
		// (get) Token: 0x0600A559 RID: 42329 RVA: 0x002237DC File Offset: 0x002219DC
		public string DataSourceType
		{
			get
			{
				return this.dataSourceType;
			}
		}

		// Token: 0x17002A3C RID: 10812
		// (get) Token: 0x0600A55A RID: 42330 RVA: 0x002237E4 File Offset: 0x002219E4
		public string DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x0600A55B RID: 42331 RVA: 0x002237EC File Offset: 0x002219EC
		public override int GetHashCode()
		{
			return StringComparer.Ordinal.GetHashCode(this.dataSourceType) ^ StringComparer.OrdinalIgnoreCase.GetHashCode(this.dataSource);
		}

		// Token: 0x0600A55C RID: 42332 RVA: 0x0022380F File Offset: 0x00221A0F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataSourceKey);
		}

		// Token: 0x0600A55D RID: 42333 RVA: 0x0022381D File Offset: 0x00221A1D
		public bool Equals(DataSourceKey other)
		{
			return other != null && StringComparer.Ordinal.Compare(this.dataSourceType, other.dataSourceType) == 0 && StringComparer.OrdinalIgnoreCase.Compare(this.dataSource, other.dataSource) == 0;
		}

		// Token: 0x04005616 RID: 22038
		private string dataSourceType;

		// Token: 0x04005617 RID: 22039
		private string dataSource;
	}
}
