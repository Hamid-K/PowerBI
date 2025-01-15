using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006D8 RID: 1752
	public sealed class DbQueryCommandTree : DbCommandTree
	{
		// Token: 0x06005156 RID: 20822 RVA: 0x00123658 File Offset: 0x00121858
		public DbQueryCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query, bool validate, bool useDatabaseNullSemantics, bool disableFilterOverProjectionSimplificationForCustomFunctions)
			: base(metadata, dataSpace, useDatabaseNullSemantics, disableFilterOverProjectionSimplificationForCustomFunctions)
		{
			Check.NotNull<DbExpression>(query, "query");
			if (validate)
			{
				DbExpressionValidator dbExpressionValidator = new DbExpressionValidator(metadata, dataSpace);
				dbExpressionValidator.ValidateExpression(query, "query");
				this._parameters = new ReadOnlyCollection<DbParameterReferenceExpression>(dbExpressionValidator.Parameters.Select((KeyValuePair<string, DbParameterReferenceExpression> paramInfo) => paramInfo.Value).ToList<DbParameterReferenceExpression>());
			}
			this._query = query;
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x001236D6 File Offset: 0x001218D6
		public DbQueryCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query, bool validate, bool useDatabaseNullSemantics)
			: this(metadata, dataSpace, query, validate, useDatabaseNullSemantics, false)
		{
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x001236E6 File Offset: 0x001218E6
		public DbQueryCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query, bool validate)
			: this(metadata, dataSpace, query, validate, true, false)
		{
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x001236F5 File Offset: 0x001218F5
		public DbQueryCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query)
			: this(metadata, dataSpace, query, true, true, false)
		{
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x0600515A RID: 20826 RVA: 0x00123703 File Offset: 0x00121903
		public DbExpression Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x0600515B RID: 20827 RVA: 0x0012370B File Offset: 0x0012190B
		public override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Query;
			}
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x0012370E File Offset: 0x0012190E
		internal override IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters()
		{
			if (this._parameters == null)
			{
				this._parameters = ParameterRetriever.GetParameters(this);
			}
			return this._parameters.Select((DbParameterReferenceExpression p) => new KeyValuePair<string, TypeUsage>(p.ParameterName, p.ResultType));
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x0012374E File Offset: 0x0012194E
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			if (this.Query != null)
			{
				dumper.Dump(this.Query, "Query");
			}
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x00123769 File Offset: 0x00121969
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x00123772 File Offset: 0x00121972
		internal static DbQueryCommandTree FromValidExpression(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query, bool useDatabaseNullSemantics, bool disableFilterOverProjectionSimplificationForCustomFunctions)
		{
			return new DbQueryCommandTree(metadata, dataSpace, query, false, useDatabaseNullSemantics, disableFilterOverProjectionSimplificationForCustomFunctions);
		}

		// Token: 0x04001DBB RID: 7611
		private readonly DbExpression _query;

		// Token: 0x04001DBC RID: 7612
		private ReadOnlyCollection<DbParameterReferenceExpression> _parameters;
	}
}
