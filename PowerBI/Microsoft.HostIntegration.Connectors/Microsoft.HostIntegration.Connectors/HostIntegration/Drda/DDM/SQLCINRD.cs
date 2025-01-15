using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008B7 RID: 2231
	public class SQLCINRD : SQLDARD
	{
		// Token: 0x060046DB RID: 18139 RVA: 0x000FAEDA File Offset: 0x000F90DA
		public SQLCINRD(IDatabase database, int sqlamLevel, string typeDef)
			: base(database, sqlamLevel, typeDef)
		{
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x000FAEE8 File Offset: 0x000F90E8
		public override async Task ReadAsync(DdmReader reader, bool isAsync, bool useAccelerator, CancellationToken cancellationToken)
		{
			SQLCAGRP sqlcagrp = new SQLCAGRP(this._database, this._sqlamLevel);
			base.InitializeCodepoint(sqlcagrp);
			await sqlcagrp.ReadAsync(reader, isAsync, cancellationToken);
			if (!sqlcagrp.IsNull)
			{
				base.SqlCa = sqlcagrp;
			}
			base.SqlNum = await reader.ReadInt16Async(isAsync, cancellationToken);
			for (int i = 0; i < (int)base.SqlNum; i++)
			{
				List<SQLDAGRP> list = base.ListSqldagrp;
				list.Add(await base.ReadSQLDAGRPAsync(reader, isAsync, useAccelerator, cancellationToken));
				list = null;
			}
		}
	}
}
