using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000BD RID: 189
	public abstract class IndexOperation : MigrationOperation
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x0002092E File Offset: 0x0001EB2E
		public static string BuildDefaultName(IEnumerable<string> columns)
		{
			Check.NotNull<IEnumerable<string>>(columns, "columns");
			return string.Format(CultureInfo.InvariantCulture, "IX_{0}", new object[] { columns.Join(null, "_") }).RestrictTo(128);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0002096A File Offset: 0x0001EB6A
		protected IndexOperation(object anonymousArguments = null)
			: base(anonymousArguments)
		{
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0002097E File Offset: 0x0001EB7E
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x00020986 File Offset: 0x0001EB86
		public string Table
		{
			get
			{
				return this._table;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._table = value;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0002099B File Offset: 0x0001EB9B
		public IList<string> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x000209A3 File Offset: 0x0001EBA3
		public bool HasDefaultName
		{
			get
			{
				return string.Equals(this.Name, this.DefaultName, StringComparison.Ordinal);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x000209B7 File Offset: 0x0001EBB7
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x000209C9 File Offset: 0x0001EBC9
		public string Name
		{
			get
			{
				return this._name ?? this.DefaultName;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x000209D2 File Offset: 0x0001EBD2
		internal string DefaultName
		{
			get
			{
				return IndexOperation.BuildDefaultName(this.Columns);
			}
		}

		// Token: 0x0400086B RID: 2155
		private string _table;

		// Token: 0x0400086C RID: 2156
		private readonly List<string> _columns = new List<string>();

		// Token: 0x0400086D RID: 2157
		private string _name;
	}
}
