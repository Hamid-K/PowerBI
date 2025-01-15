using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006CE RID: 1742
	public abstract class DbModificationCommandTree : DbCommandTree
	{
		// Token: 0x06005124 RID: 20772 RVA: 0x001232FA File Offset: 0x001214FA
		internal DbModificationCommandTree()
		{
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x00123302 File Offset: 0x00121502
		internal DbModificationCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target)
			: base(metadata, dataSpace, true, false)
		{
			this._target = target;
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06005126 RID: 20774 RVA: 0x00123315 File Offset: 0x00121515
		public DbExpressionBinding Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06005127 RID: 20775
		internal abstract bool HasReader { get; }

		// Token: 0x06005128 RID: 20776 RVA: 0x0012331D File Offset: 0x0012151D
		internal override IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters()
		{
			if (this._parameters == null)
			{
				this._parameters = ParameterRetriever.GetParameters(this);
			}
			return this._parameters.Select((DbParameterReferenceExpression p) => new KeyValuePair<string, TypeUsage>(p.ParameterName, p.ResultType));
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x0012335D File Offset: 0x0012155D
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			if (this.Target != null)
			{
				dumper.Dump(this.Target, "Target");
			}
		}

		// Token: 0x04001DAF RID: 7599
		private readonly DbExpressionBinding _target;

		// Token: 0x04001DB0 RID: 7600
		private ReadOnlyCollection<DbParameterReferenceExpression> _parameters;
	}
}
