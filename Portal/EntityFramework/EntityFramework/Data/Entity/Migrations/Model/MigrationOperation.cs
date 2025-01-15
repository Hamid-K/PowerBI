using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000BE RID: 190
	public abstract class MigrationOperation
	{
		// Token: 0x06000F80 RID: 3968 RVA: 0x000209E0 File Offset: 0x0001EBE0
		protected MigrationOperation(object anonymousArguments)
		{
			MigrationOperation <>4__this = this;
			if (anonymousArguments != null)
			{
				anonymousArguments.GetType().GetNonIndexerProperties().Each(delegate(PropertyInfo p)
				{
					<>4__this._anonymousArguments.Add(p.Name, p.GetValue(anonymousArguments, null));
				});
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00020A3B File Offset: 0x0001EC3B
		public IDictionary<string, object> AnonymousArguments
		{
			get
			{
				return this._anonymousArguments;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x00020A43 File Offset: 0x0001EC43
		public virtual MigrationOperation Inverse
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000F83 RID: 3971
		public abstract bool IsDestructiveChange { get; }

		// Token: 0x0400086E RID: 2158
		private readonly IDictionary<string, object> _anonymousArguments = new Dictionary<string, object>();
	}
}
