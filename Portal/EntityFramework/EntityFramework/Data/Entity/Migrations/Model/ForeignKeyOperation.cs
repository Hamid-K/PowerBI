using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000BA RID: 186
	public abstract class ForeignKeyOperation : MigrationOperation
	{
		// Token: 0x06000F69 RID: 3945 RVA: 0x00020810 File Offset: 0x0001EA10
		protected ForeignKeyOperation(object anonymousArguments = null)
			: base(anonymousArguments)
		{
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00020824 File Offset: 0x0001EA24
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x0002082C File Offset: 0x0001EA2C
		public string PrincipalTable
		{
			get
			{
				return this._principalTable;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._principalTable = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x00020841 File Offset: 0x0001EA41
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x00020849 File Offset: 0x0001EA49
		public string DependentTable
		{
			get
			{
				return this._dependentTable;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._dependentTable = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0002085E File Offset: 0x0001EA5E
		public IList<string> DependentColumns
		{
			get
			{
				return this._dependentColumns;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00020866 File Offset: 0x0001EA66
		public bool HasDefaultName
		{
			get
			{
				return string.Equals(this.Name, this.DefaultName, StringComparison.Ordinal);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0002087A File Offset: 0x0001EA7A
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x0002088C File Offset: 0x0001EA8C
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

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00020898 File Offset: 0x0001EA98
		internal string DefaultName
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "FK_{0}_{1}_{2}", new object[]
				{
					this.DependentTable,
					this.PrincipalTable,
					this.DependentColumns.Join(null, "_")
				}).RestrictTo(128);
			}
		}

		// Token: 0x04000866 RID: 2150
		private string _principalTable;

		// Token: 0x04000867 RID: 2151
		private string _dependentTable;

		// Token: 0x04000868 RID: 2152
		private readonly List<string> _dependentColumns = new List<string>();

		// Token: 0x04000869 RID: 2153
		private string _name;
	}
}
