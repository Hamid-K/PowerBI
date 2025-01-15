using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000AF RID: 175
	public class AlterTableOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x06000F18 RID: 3864 RVA: 0x0001FD8C File Offset: 0x0001DF8C
		public AlterTableOperation(string name, IDictionary<string, AnnotationValues> annotations, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._annotations = annotations ?? new Dictionary<string, AnnotationValues>();
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0001FDC3 File Offset: 0x0001DFC3
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0001FDCB File Offset: 0x0001DFCB
		public virtual IList<ColumnModel> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0001FDD3 File Offset: 0x0001DFD3
		public virtual IDictionary<string, AnnotationValues> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0001FDDC File Offset: 0x0001DFDC
		public override MigrationOperation Inverse
		{
			get
			{
				AlterTableOperation alterTableOperation = new AlterTableOperation(this.Name, this.Annotations.ToDictionary((KeyValuePair<string, AnnotationValues> a) => a.Key, (KeyValuePair<string, AnnotationValues> a) => new AnnotationValues(a.Value.NewValue, a.Value.OldValue)), null);
				alterTableOperation._columns.AddRange(this._columns);
				return alterTableOperation;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0001FE4F File Offset: 0x0001E04F
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0001FE52 File Offset: 0x0001E052
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				if (!this.Annotations.Any<KeyValuePair<string, AnnotationValues>>())
				{
					return this.Columns.SelectMany((ColumnModel c) => c.Annotations).Any<KeyValuePair<string, AnnotationValues>>();
				}
				return true;
			}
		}

		// Token: 0x0400084A RID: 2122
		private readonly string _name;

		// Token: 0x0400084B RID: 2123
		private readonly List<ColumnModel> _columns = new List<ColumnModel>();

		// Token: 0x0400084C RID: 2124
		private readonly IDictionary<string, AnnotationValues> _annotations;
	}
}
