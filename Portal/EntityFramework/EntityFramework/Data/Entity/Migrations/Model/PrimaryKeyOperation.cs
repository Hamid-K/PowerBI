using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000C3 RID: 195
	public abstract class PrimaryKeyOperation : MigrationOperation
	{
		// Token: 0x06000F9B RID: 3995 RVA: 0x00020BAA File Offset: 0x0001EDAA
		public static string BuildDefaultName(string table)
		{
			Check.NotEmpty(table, "table");
			return string.Format(CultureInfo.InvariantCulture, "PK_{0}", new object[] { table }).RestrictTo(128);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00020BDB File Offset: 0x0001EDDB
		protected PrimaryKeyOperation(object anonymousArguments = null)
			: base(anonymousArguments)
		{
			this.IsClustered = true;
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00020BF6 File Offset: 0x0001EDF6
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x00020BFE File Offset: 0x0001EDFE
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

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00020C13 File Offset: 0x0001EE13
		public IList<string> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00020C1B File Offset: 0x0001EE1B
		public bool HasDefaultName
		{
			get
			{
				return string.Equals(this.Name, this.DefaultName, StringComparison.Ordinal);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00020C2F File Offset: 0x0001EE2F
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x00020C41 File Offset: 0x0001EE41
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

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00020C4A File Offset: 0x0001EE4A
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00020C4D File Offset: 0x0001EE4D
		internal string DefaultName
		{
			get
			{
				return PrimaryKeyOperation.BuildDefaultName(this.Table);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00020C5A File Offset: 0x0001EE5A
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x00020C62 File Offset: 0x0001EE62
		public bool IsClustered { get; set; }

		// Token: 0x04000878 RID: 2168
		private readonly List<string> _columns = new List<string>();

		// Token: 0x04000879 RID: 2169
		private string _table;

		// Token: 0x0400087A RID: 2170
		private string _name;
	}
}
