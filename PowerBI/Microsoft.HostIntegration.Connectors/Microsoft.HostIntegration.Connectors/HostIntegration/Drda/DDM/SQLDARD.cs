using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008C5 RID: 2245
	public class SQLDARD : AbstractDdmObject
	{
		// Token: 0x06004775 RID: 18293 RVA: 0x000FCE72 File Offset: 0x000FB072
		public SQLDARD(IDatabase database, int sqlamLevel, string typeDef)
			: base(database, sqlamLevel, typeDef)
		{
			this.SqlCa = null;
			this.SqlDhgrp = null;
			this.SqlNum = 0;
			this.ListSqldagrp = new List<SQLDAGRP>();
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x000FCE9D File Offset: 0x000FB09D
		// (set) Token: 0x06004777 RID: 18295 RVA: 0x000FCEA5 File Offset: 0x000FB0A5
		public SQLCAGRP SqlCa { get; set; }

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x000FCEAE File Offset: 0x000FB0AE
		// (set) Token: 0x06004779 RID: 18297 RVA: 0x000FCEB6 File Offset: 0x000FB0B6
		public SQLDHGRP SqlDhgrp { get; set; }

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x000FCEBF File Offset: 0x000FB0BF
		// (set) Token: 0x0600477B RID: 18299 RVA: 0x000FCEC7 File Offset: 0x000FB0C7
		public short SqlNum { get; set; }

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x0600477C RID: 18300 RVA: 0x000FCED0 File Offset: 0x000FB0D0
		// (set) Token: 0x0600477D RID: 18301 RVA: 0x000FCED8 File Offset: 0x000FB0D8
		public List<SQLDAGRP> ListSqldagrp { get; private set; }

		// Token: 0x0600477E RID: 18302 RVA: 0x000FCEE1 File Offset: 0x000FB0E1
		public override string ToString()
		{
			return string.Format("SQLDARD[sqlca={0};]", (this.SqlCa == null) ? "none" : this.SqlCa.ToString());
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x000FCF08 File Offset: 0x000FB108
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			SQLCAGRP sqlcagrp = new SQLCAGRP(this._database, this._sqlamLevel);
			base.InitializeCodepoint(sqlcagrp);
			await sqlcagrp.ReadAsync(reader, isAsync, cancellationToken);
			if (!sqlcagrp.IsNull)
			{
				this.SqlCa = sqlcagrp;
			}
			this.SqlDhgrp = await this.ReadSQLDHGRP(reader, isAsync, cancellationToken);
			this.SqlNum = await reader.ReadInt16Async(isAsync, cancellationToken);
			for (int i = 0; i < (int)this.SqlNum; i++)
			{
				List<SQLDAGRP> list = this.ListSqldagrp;
				list.Add(await this.ReadSQLDAGRPAsync(reader, isAsync, false, cancellationToken));
				list = null;
			}
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x000FCF68 File Offset: 0x000FB168
		protected async Task<SQLDHGRP> ReadSQLDHGRP(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<byte> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			SQLDHGRP sqldhgrp2;
			if (taskAwaiter.GetResult() == 255)
			{
				sqldhgrp2 = null;
			}
			else
			{
				SQLDHGRP sqldhgrp = new SQLDHGRP();
				SQLDHGRP sqldhgrp3 = sqldhgrp;
				sqldhgrp3.SqldHold = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldhgrp3 = null;
				sqldhgrp3 = sqldhgrp;
				sqldhgrp3.SqldReturn = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldhgrp3 = null;
				sqldhgrp3 = sqldhgrp;
				sqldhgrp3.SqldScroll = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldhgrp3 = null;
				sqldhgrp3 = sqldhgrp;
				sqldhgrp3.SqldSensitive = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldhgrp3 = null;
				sqldhgrp3 = sqldhgrp;
				sqldhgrp3.SqldFCode = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldhgrp3 = null;
				sqldhgrp3 = sqldhgrp;
				sqldhgrp3.SqldKeyType = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldhgrp3 = null;
				if (this._sqlamLevel >= 9)
				{
					sqldhgrp3 = sqldhgrp;
					sqldhgrp3.SqldOptlck = await reader.ReadInt16Async(isAsync, cancellationToken);
					sqldhgrp3 = null;
				}
				sqldhgrp3 = sqldhgrp;
				sqldhgrp3.SqldRdbnam = await reader.ReadLDStringAsync(reader.Ccsid._ccsidsbc, isAsync, cancellationToken);
				sqldhgrp3 = null;
				sqldhgrp3 = sqldhgrp;
				sqldhgrp3.SqldSchema = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
				sqldhgrp3 = null;
				if (this._sqlamLevel >= 10)
				{
					sqldhgrp3 = sqldhgrp;
					sqldhgrp3.SqldModule = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
					sqldhgrp3 = null;
				}
				sqldhgrp2 = sqldhgrp;
			}
			return sqldhgrp2;
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x000FCFC8 File Offset: 0x000FB1C8
		protected async Task<SQLDAGRP> ReadSQLDAGRPAsync(DdmReader reader, bool isAsync, bool useAccelerator, CancellationToken cancellationToken)
		{
			SQLDAGRP sqldagrp = new SQLDAGRP();
			SQLDAGRP sqldagrp2 = sqldagrp;
			short num = await reader.ReadInt16Async(isAsync, cancellationToken);
			sqldagrp2.SqlPrecision = num;
			sqldagrp2 = null;
			sqldagrp2 = sqldagrp;
			num = await reader.ReadInt16Async(isAsync, cancellationToken);
			sqldagrp2.SqlScale = num;
			sqldagrp2 = null;
			sqldagrp2 = sqldagrp;
			sqldagrp2.SqlLength = await reader.ReadInt64Async(isAsync, cancellationToken);
			sqldagrp2 = null;
			sqldagrp2 = sqldagrp;
			num = await reader.ReadInt16Async(isAsync, cancellationToken);
			sqldagrp2.SqlType = num;
			sqldagrp2 = null;
			sqldagrp2 = sqldagrp;
			num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
			sqldagrp2.SqlCcsid = num;
			sqldagrp2 = null;
			if (this._sqlamLevel >= 9 || useAccelerator)
			{
				sqldagrp2 = sqldagrp;
				sqldagrp2.SqlArrExtent = await reader.ReadInt64Async(isAsync, cancellationToken);
				sqldagrp2 = null;
			}
			if (this._sqlamLevel >= 11 || useAccelerator)
			{
				sqldagrp2 = sqldagrp;
				num = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldagrp2.SqlSuSize = num;
				sqldagrp2 = null;
			}
			sqldagrp2 = sqldagrp;
			sqldagrp2.SqldOptGrp = await this.ReadSqldoptgrpAsync(reader, isAsync, useAccelerator, cancellationToken);
			sqldagrp2 = null;
			return sqldagrp;
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x000FD030 File Offset: 0x000FB230
		protected async Task<SQLDOPTGRP> ReadSqldoptgrpAsync(DdmReader reader, bool isAsync, bool useAccelerator, CancellationToken cancellationToken)
		{
			TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<byte> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			SQLDOPTGRP sqldoptgrp;
			if (taskAwaiter.GetResult() == 255)
			{
				sqldoptgrp = null;
			}
			else
			{
				SQLDOPTGRP sqldOptGrp = new SQLDOPTGRP();
				SQLDOPTGRP sqldoptgrp2 = sqldOptGrp;
				sqldoptgrp2.SqlUnnamed = (int)(await reader.ReadInt16Async(isAsync, cancellationToken));
				sqldoptgrp2 = null;
				sqldoptgrp2 = sqldOptGrp;
				sqldoptgrp2.SqlName = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
				sqldoptgrp2 = null;
				sqldoptgrp2 = sqldOptGrp;
				sqldoptgrp2.SqlLabel = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
				sqldoptgrp2 = null;
				sqldoptgrp2 = sqldOptGrp;
				sqldoptgrp2.SqlComments = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
				sqldoptgrp2 = null;
				sqldoptgrp2 = sqldOptGrp;
				sqldoptgrp2.SqlUdtGrp = await this.ReadSqludtgrpAsync(reader, isAsync, cancellationToken);
				sqldoptgrp2 = null;
				sqldoptgrp2 = sqldOptGrp;
				sqldoptgrp2.SqlDxGrp = await this.ReadSqldxgrpAsync(reader, isAsync, cancellationToken);
				sqldoptgrp2 = null;
				if (this._sqlamLevel >= 11 || useAccelerator)
				{
					sqldoptgrp2 = sqldOptGrp;
					sqldoptgrp2.SqlDcdt = await this.ReadSqldcdtAsync(reader, isAsync, cancellationToken);
					sqldoptgrp2 = null;
				}
				sqldoptgrp = sqldOptGrp;
			}
			return sqldoptgrp;
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x000FD098 File Offset: 0x000FB298
		protected async Task<SQLUDTGRP> ReadSqludtgrpAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<byte> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			SQLUDTGRP sqludtgrp;
			if (taskAwaiter.GetResult() == 255)
			{
				sqludtgrp = null;
			}
			else
			{
				SQLUDTGRP sqlUdtgrp = new SQLUDTGRP();
				SQLUDTGRP sqludtgrp2 = sqlUdtgrp;
				sqludtgrp2.SqlUdtXType = await reader.ReadInt32Async(isAsync, cancellationToken);
				sqludtgrp2 = null;
				sqludtgrp2 = sqlUdtgrp;
				sqludtgrp2.SqlUdtRdb = await reader.ReadLDStringAsync(reader.Ccsid._ccsidsbc, isAsync, cancellationToken);
				sqludtgrp2 = null;
				sqludtgrp2 = sqlUdtgrp;
				sqludtgrp2.SqlUdtSchema = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
				sqludtgrp2 = null;
				sqludtgrp2 = sqlUdtgrp;
				sqludtgrp2.SqlUdtName = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
				sqludtgrp2 = null;
				if (this._sqlamLevel >= 11)
				{
					sqludtgrp2 = sqlUdtgrp;
					sqludtgrp2.SqlUdtModule = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
					sqludtgrp2 = null;
				}
				sqludtgrp = sqlUdtgrp;
			}
			return sqludtgrp;
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x000FD0F8 File Offset: 0x000FB2F8
		protected async Task<SQLDXGRP> ReadSqldxgrpAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<byte> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			SQLDXGRP sqldxgrp;
			if (taskAwaiter.GetResult() == 255)
			{
				sqldxgrp = null;
			}
			else
			{
				SQLDXGRP sqlDxgrp = new SQLDXGRP();
				SQLDXGRP sqldxgrp2 = sqlDxgrp;
				sqldxgrp2.SqlxKeyMem = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldxgrp2 = null;
				sqldxgrp2 = sqlDxgrp;
				sqldxgrp2.SqlxUpdateable = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldxgrp2 = null;
				sqldxgrp2 = sqlDxgrp;
				sqldxgrp2.SqlxGenerated = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldxgrp2 = null;
				sqldxgrp2 = sqlDxgrp;
				sqldxgrp2.SqlxParmMode = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldxgrp2 = null;
				if (this._sqlamLevel >= 9)
				{
					sqldxgrp2 = sqlDxgrp;
					sqldxgrp2.SqlxOptlck = await reader.ReadInt16Async(isAsync, cancellationToken);
					sqldxgrp2 = null;
					sqldxgrp2 = sqlDxgrp;
					sqldxgrp2.SqlxHidden = await reader.ReadInt16Async(isAsync, cancellationToken);
					sqldxgrp2 = null;
				}
				sqldxgrp2 = sqlDxgrp;
				sqldxgrp2.SqlxRdbnam = await reader.ReadLDStringAsync(reader.Ccsid._ccsidsbc, isAsync, cancellationToken);
				sqldxgrp2 = null;
				sqldxgrp2 = sqlDxgrp;
				sqldxgrp2.SqlxCorName = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
				sqldxgrp2 = null;
				sqldxgrp2 = sqlDxgrp;
				sqldxgrp2.SqlxBaseName = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
				sqldxgrp2 = null;
				sqldxgrp2 = sqlDxgrp;
				sqldxgrp2.SqlxSchema = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
				sqldxgrp2 = null;
				sqldxgrp2 = sqlDxgrp;
				sqldxgrp2.SqlxName = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
				sqldxgrp2 = null;
				if (this._sqlamLevel >= 10)
				{
					sqldxgrp2 = sqlDxgrp;
					sqldxgrp2.SqlxModule = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
					sqldxgrp2 = null;
				}
				sqldxgrp = sqlDxgrp;
			}
			return sqldxgrp;
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x000FD158 File Offset: 0x000FB358
		protected async Task<SQLDCDT> ReadSqldcdtAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<byte> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			SQLDCDT sqldcdt2;
			if (taskAwaiter.GetResult() == 255)
			{
				sqldcdt2 = null;
			}
			else
			{
				SQLDCDT sqldcdt = new SQLDCDT();
				SQLDCDT sqldcdt3 = sqldcdt;
				sqldcdt3.SqlNum = await reader.ReadInt16Async(isAsync, cancellationToken);
				sqldcdt3 = null;
				for (int i = 0; i < (int)sqldcdt.SqlNum; i++)
				{
					List<SQLDCDTGRP> list = sqldcdt.ListSqldcdtgrp;
					list.Add(await this.ReadSqldcdtgrpAsync(reader, isAsync, cancellationToken));
					list = null;
				}
				sqldcdt2 = sqldcdt;
			}
			return sqldcdt2;
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x000FD1B8 File Offset: 0x000FB3B8
		protected async Task<SQLDCDTGRP> ReadSqldcdtgrpAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			SQLDCDTGRP sqldcdtgrp = new SQLDCDTGRP();
			SQLDCDTGRP sqldcdtgrp2 = sqldcdtgrp;
			short num = await reader.ReadInt16Async(isAsync, cancellationToken);
			sqldcdtgrp2.SqlPrecision = num;
			sqldcdtgrp2 = null;
			sqldcdtgrp2 = sqldcdtgrp;
			num = await reader.ReadInt16Async(isAsync, cancellationToken);
			sqldcdtgrp2.SqlScale = num;
			sqldcdtgrp2 = null;
			sqldcdtgrp2 = sqldcdtgrp;
			sqldcdtgrp2.SqlLength = await reader.ReadInt64Async(isAsync, cancellationToken);
			sqldcdtgrp2 = null;
			sqldcdtgrp2 = sqldcdtgrp;
			num = await reader.ReadInt16Async(isAsync, cancellationToken);
			sqldcdtgrp2.SqlType = num;
			sqldcdtgrp2 = null;
			sqldcdtgrp2 = sqldcdtgrp;
			num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
			sqldcdtgrp2.SqlCcsid = num;
			sqldcdtgrp2 = null;
			sqldcdtgrp2 = sqldcdtgrp;
			sqldcdtgrp2.SqlArrExtent = await reader.ReadInt64Async(isAsync, cancellationToken);
			sqldcdtgrp2 = null;
			sqldcdtgrp2 = sqldcdtgrp;
			sqldcdtgrp2.SqlName = await base.ParseVCMorVCSAsync(reader, reader.Ccsid, isAsync, cancellationToken);
			sqldcdtgrp2 = null;
			sqldcdtgrp2 = sqldcdtgrp;
			sqldcdtgrp2.SqlUdtGrp = await this.ReadSqludtgrpAsync(reader, isAsync, cancellationToken);
			sqldcdtgrp2 = null;
			sqldcdtgrp2 = sqldcdtgrp;
			sqldcdtgrp2.SqlDcdt = await this.ReadSqldcdtAsync(reader, isAsync, cancellationToken);
			sqldcdtgrp2 = null;
			return sqldcdtgrp;
		}
	}
}
