using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000B4 RID: 180
	public class DropColumnOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x00020503 File Offset: 0x0001E703
		public DropColumnOperation(string table, string name, object anonymousArguments = null)
			: this(table, name, null, null, anonymousArguments)
		{
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00020510 File Offset: 0x0001E710
		public DropColumnOperation(string table, string name, IDictionary<string, object> removedAnnotations, object anonymousArguments = null)
			: this(table, name, removedAnnotations, null, anonymousArguments)
		{
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0002051E File Offset: 0x0001E71E
		public DropColumnOperation(string table, string name, AddColumnOperation inverse, object anonymousArguments = null)
			: this(table, name, null, inverse, anonymousArguments)
		{
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0002052C File Offset: 0x0001E72C
		public DropColumnOperation(string table, string name, IDictionary<string, object> removedAnnotations, AddColumnOperation inverse, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			this._table = table;
			this._name = name;
			this._removedAnnotations = removedAnnotations ?? new Dictionary<string, object>();
			this._inverse = inverse;
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0002057F File Offset: 0x0001E77F
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00020587 File Offset: 0x0001E787
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0002058F File Offset: 0x0001E78F
		public IDictionary<string, object> RemovedAnnotations
		{
			get
			{
				return this._removedAnnotations;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00020597 File Offset: 0x0001E797
		public override MigrationOperation Inverse
		{
			get
			{
				return this._inverse;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0002059F File Offset: 0x0001E79F
		public override bool IsDestructiveChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x000205A4 File Offset: 0x0001E7A4
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				AddColumnOperation addColumnOperation = this.Inverse as AddColumnOperation;
				return this.RemovedAnnotations.Any<KeyValuePair<string, object>>() || (addColumnOperation != null && ((IAnnotationTarget)addColumnOperation).HasAnnotations);
			}
		}

		// Token: 0x0400085A RID: 2138
		private readonly string _table;

		// Token: 0x0400085B RID: 2139
		private readonly string _name;

		// Token: 0x0400085C RID: 2140
		private readonly AddColumnOperation _inverse;

		// Token: 0x0400085D RID: 2141
		private readonly IDictionary<string, object> _removedAnnotations;
	}
}
