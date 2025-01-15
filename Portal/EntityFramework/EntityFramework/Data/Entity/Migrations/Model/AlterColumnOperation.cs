using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000AD RID: 173
	public class AlterColumnOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x06000F0F RID: 3855 RVA: 0x0001FCBC File Offset: 0x0001DEBC
		public AlterColumnOperation(string table, ColumnModel column, bool isDestructiveChange, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(table, "table");
			Check.NotNull<ColumnModel>(column, "column");
			this._table = table;
			this._column = column;
			this._destructiveChange = isDestructiveChange;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0001FCF3 File Offset: 0x0001DEF3
		public AlterColumnOperation(string table, ColumnModel column, bool isDestructiveChange, AlterColumnOperation inverse, object anonymousArguments = null)
			: this(table, column, isDestructiveChange, anonymousArguments)
		{
			Check.NotNull<AlterColumnOperation>(inverse, "inverse");
			this._inverse = inverse;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0001FD15 File Offset: 0x0001DF15
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0001FD1D File Offset: 0x0001DF1D
		public ColumnModel Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0001FD25 File Offset: 0x0001DF25
		public override MigrationOperation Inverse
		{
			get
			{
				return this._inverse;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0001FD2D File Offset: 0x0001DF2D
		public override bool IsDestructiveChange
		{
			get
			{
				return this._destructiveChange;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0001FD38 File Offset: 0x0001DF38
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				AlterColumnOperation alterColumnOperation = this.Inverse as AlterColumnOperation;
				return this.Column.Annotations.Any<KeyValuePair<string, AnnotationValues>>() || (alterColumnOperation != null && alterColumnOperation.Column.Annotations.Any<KeyValuePair<string, AnnotationValues>>());
			}
		}

		// Token: 0x04000846 RID: 2118
		private readonly string _table;

		// Token: 0x04000847 RID: 2119
		private readonly ColumnModel _column;

		// Token: 0x04000848 RID: 2120
		private readonly AlterColumnOperation _inverse;

		// Token: 0x04000849 RID: 2121
		private readonly bool _destructiveChange;
	}
}
