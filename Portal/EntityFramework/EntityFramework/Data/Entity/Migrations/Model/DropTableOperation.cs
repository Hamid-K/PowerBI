using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000B9 RID: 185
	public class DropTableOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x06000F5F RID: 3935 RVA: 0x00020733 File Offset: 0x0001E933
		public DropTableOperation(string name, object anonymousArguments = null)
			: this(name, null, null, null, anonymousArguments)
		{
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00020740 File Offset: 0x0001E940
		public DropTableOperation(string name, IDictionary<string, object> removedAnnotations, IDictionary<string, IDictionary<string, object>> removedColumnAnnotations, object anonymousArguments = null)
			: this(name, removedAnnotations, removedColumnAnnotations, null, anonymousArguments)
		{
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002074E File Offset: 0x0001E94E
		public DropTableOperation(string name, CreateTableOperation inverse, object anonymousArguments = null)
			: this(name, null, null, inverse, anonymousArguments)
		{
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002075C File Offset: 0x0001E95C
		public DropTableOperation(string name, IDictionary<string, object> removedAnnotations, IDictionary<string, IDictionary<string, object>> removedColumnAnnotations, CreateTableOperation inverse, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._removedAnnotations = removedAnnotations ?? new Dictionary<string, object>();
			this._removedColumnAnnotations = removedColumnAnnotations ?? new Dictionary<string, IDictionary<string, object>>();
			this._inverse = inverse;
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x000207AC File Offset: 0x0001E9AC
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x000207B4 File Offset: 0x0001E9B4
		public virtual IDictionary<string, object> RemovedAnnotations
		{
			get
			{
				return this._removedAnnotations;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x000207BC File Offset: 0x0001E9BC
		public IDictionary<string, IDictionary<string, object>> RemovedColumnAnnotations
		{
			get
			{
				return this._removedColumnAnnotations;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x000207C4 File Offset: 0x0001E9C4
		public override MigrationOperation Inverse
		{
			get
			{
				return this._inverse;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x000207CC File Offset: 0x0001E9CC
		public override bool IsDestructiveChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x000207D0 File Offset: 0x0001E9D0
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				CreateTableOperation createTableOperation = this.Inverse as CreateTableOperation;
				return this.RemovedAnnotations.Any<KeyValuePair<string, object>>() || this.RemovedColumnAnnotations.Any<KeyValuePair<string, IDictionary<string, object>>>() || (createTableOperation != null && ((IAnnotationTarget)createTableOperation).HasAnnotations);
			}
		}

		// Token: 0x04000862 RID: 2146
		private readonly string _name;

		// Token: 0x04000863 RID: 2147
		private readonly CreateTableOperation _inverse;

		// Token: 0x04000864 RID: 2148
		private readonly IDictionary<string, IDictionary<string, object>> _removedColumnAnnotations;

		// Token: 0x04000865 RID: 2149
		private readonly IDictionary<string, object> _removedAnnotations;
	}
}
