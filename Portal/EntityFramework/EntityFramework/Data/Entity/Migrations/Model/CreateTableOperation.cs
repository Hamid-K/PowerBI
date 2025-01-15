using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000B3 RID: 179
	public class CreateTableOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x06000F3A RID: 3898 RVA: 0x000203AA File Offset: 0x0001E5AA
		public CreateTableOperation(string name, object anonymousArguments = null)
			: this(name, null, anonymousArguments)
		{
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x000203B5 File Offset: 0x0001E5B5
		public CreateTableOperation(string name, IDictionary<string, object> annotations, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._annotations = annotations ?? new Dictionary<string, object>();
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x000203EC File Offset: 0x0001E5EC
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x000203F4 File Offset: 0x0001E5F4
		public virtual IList<ColumnModel> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x000203FC File Offset: 0x0001E5FC
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x00020404 File Offset: 0x0001E604
		public AddPrimaryKeyOperation PrimaryKey
		{
			get
			{
				return this._primaryKey;
			}
			set
			{
				Check.NotNull<AddPrimaryKeyOperation>(value, "value");
				this._primaryKey = value;
				this._primaryKey.Table = this.Name;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0002042A File Offset: 0x0001E62A
		public virtual IDictionary<string, object> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00020434 File Offset: 0x0001E634
		public override MigrationOperation Inverse
		{
			get
			{
				return new DropTableOperation(this.Name, this.Annotations, this.Columns.Where((ColumnModel c) => c.Annotations.Count > 0).ToDictionary((ColumnModel c) => c.Name, (ColumnModel c) => c.Annotations.ToDictionary((KeyValuePair<string, AnnotationValues> a) => a.Key, (KeyValuePair<string, AnnotationValues> a) => a.Value.NewValue)), null);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x000204C0 File Offset: 0x0001E6C0
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x000204C3 File Offset: 0x0001E6C3
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				if (!this.Annotations.Any<KeyValuePair<string, object>>())
				{
					return this.Columns.SelectMany((ColumnModel c) => c.Annotations).Any<KeyValuePair<string, AnnotationValues>>();
				}
				return true;
			}
		}

		// Token: 0x04000856 RID: 2134
		private readonly string _name;

		// Token: 0x04000857 RID: 2135
		private readonly List<ColumnModel> _columns = new List<ColumnModel>();

		// Token: 0x04000858 RID: 2136
		private AddPrimaryKeyOperation _primaryKey;

		// Token: 0x04000859 RID: 2137
		private readonly IDictionary<string, object> _annotations;
	}
}
