using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000AA RID: 170
	public class AddColumnOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x06000F00 RID: 3840 RVA: 0x0001FAB3 File Offset: 0x0001DCB3
		public AddColumnOperation(string table, ColumnModel column, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(table, "table");
			Check.NotNull<ColumnModel>(column, "column");
			this._table = table;
			this._column = column;
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0001FAE2 File Offset: 0x0001DCE2
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0001FAEA File Offset: 0x0001DCEA
		public ColumnModel Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
		public override MigrationOperation Inverse
		{
			get
			{
				return new DropColumnOperation(this.Table, this.Column.Name, this.Column.Annotations.ToDictionary((KeyValuePair<string, AnnotationValues> a) => a.Key, (KeyValuePair<string, AnnotationValues> a) => a.Value.NewValue), null);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0001FB66 File Offset: 0x0001DD66
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0001FB69 File Offset: 0x0001DD69
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				return this.Column.Annotations.Any<KeyValuePair<string, AnnotationValues>>();
			}
		}

		// Token: 0x04000842 RID: 2114
		private readonly string _table;

		// Token: 0x04000843 RID: 2115
		private readonly ColumnModel _column;
	}
}
