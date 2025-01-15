using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.PlanCompiler;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003F3 RID: 1011
	internal class TableMD
	{
		// Token: 0x06002F46 RID: 12102 RVA: 0x00095A06 File Offset: 0x00093C06
		private TableMD(EntitySetBase extent)
		{
			this.m_columns = new List<ColumnMD>();
			this.m_keys = new List<ColumnMD>();
			this.m_extent = extent;
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x00095A2B File Offset: 0x00093C2B
		internal TableMD(TypeUsage type, EntitySetBase extent)
			: this(extent)
		{
			this.m_columns.Add(new ColumnMD("element", type));
			this.m_flattened = !TypeUtils.IsStructuredType(type);
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x00095A5C File Offset: 0x00093C5C
		internal TableMD(IEnumerable<EdmProperty> properties, IEnumerable<EdmMember> keyProperties, EntitySetBase extent)
			: this(extent)
		{
			Dictionary<string, ColumnMD> dictionary = new Dictionary<string, ColumnMD>();
			this.m_flattened = true;
			foreach (EdmProperty edmProperty in properties)
			{
				ColumnMD columnMD = new ColumnMD(edmProperty);
				this.m_columns.Add(columnMD);
				dictionary[edmProperty.Name] = columnMD;
			}
			foreach (EdmMember edmMember in keyProperties)
			{
				ColumnMD columnMD2;
				if (dictionary.TryGetValue(edmMember.Name, out columnMD2))
				{
					this.m_keys.Add(columnMD2);
				}
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002F49 RID: 12105 RVA: 0x00095B28 File Offset: 0x00093D28
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extent;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x00095B30 File Offset: 0x00093D30
		internal List<ColumnMD> Columns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x00095B38 File Offset: 0x00093D38
		internal List<ColumnMD> Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x00095B40 File Offset: 0x00093D40
		internal bool Flattened
		{
			get
			{
				return this.m_flattened;
			}
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x00095B48 File Offset: 0x00093D48
		public override string ToString()
		{
			if (this.m_extent == null)
			{
				return "Transient";
			}
			return this.m_extent.Name;
		}

		// Token: 0x04000FF3 RID: 4083
		private readonly List<ColumnMD> m_columns;

		// Token: 0x04000FF4 RID: 4084
		private readonly List<ColumnMD> m_keys;

		// Token: 0x04000FF5 RID: 4085
		private readonly EntitySetBase m_extent;

		// Token: 0x04000FF6 RID: 4086
		private readonly bool m_flattened;
	}
}
