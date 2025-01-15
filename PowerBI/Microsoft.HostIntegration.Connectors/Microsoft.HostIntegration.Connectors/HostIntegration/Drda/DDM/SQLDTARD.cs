using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008B9 RID: 2233
	public class SQLDTARD : SQLDTA
	{
		// Token: 0x060046DF RID: 18143 RVA: 0x000FB1C2 File Offset: 0x000F93C2
		public SQLDTARD(IDatabase database, Ccsid ccsid)
			: base(database, ccsid)
		{
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x000FB1CC File Offset: 0x000F93CC
		public SQLDTARD(int requesterId, Ccsid ccsid)
			: base(null, ccsid)
		{
			this._requesterId = requesterId;
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x060046E1 RID: 18145 RVA: 0x000FB1DD File Offset: 0x000F93DD
		public SQLCAGRP SqlCa
		{
			get
			{
				return this._sqlca;
			}
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x000FB1E5 File Offset: 0x000F93E5
		public override string ToString()
		{
			return string.Format("SQLDTARD[ccsid={0};parms={1}]", this._ccsid, base.GetParmsAsString());
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x000FB200 File Offset: 0x000F9400
		protected override async Task ParseFDODTAAsync(DdmReader reader, List<DrdaParameterInfo> parms, bool isAsync, CancellationToken cancellationToken)
		{
			SQLCAGRP sqlca = new SQLCAGRP(this._database, this._sqlamLevel);
			await sqlca.ReadAsync(reader, isAsync, cancellationToken);
			this._sqlca = (sqlca.IsNull ? null : sqlca);
			if (this.IsNullIndicator(await reader.ReadByteAsync(isAsync, cancellationToken)))
			{
				if (Logger.maxTracingLevel >= 2)
				{
					Logger.Warning(this._tracePoint, this._requesterId, "FDODTA has no row for parameter value.", Array.Empty<object>());
				}
			}
			else
			{
				int i = 0;
				while (i < parms.Count)
				{
					parms[i].CCSID = -1;
					parms[i].MDDoverride = false;
					foreach (FdocscDrdaTypeMapping fdocscDrdaTypeMapping in this.drdaTypeMappings)
					{
						if (parms[i].Type == fdocscDrdaTypeMapping.TripletLID)
						{
							parms[i].Type = fdocscDrdaTypeMapping.FromDrdaType & byte.MaxValue;
							parms[i].MDDoverride = true;
							parms[i].CCSID = fdocscDrdaTypeMapping.CCSID;
						}
					}
					if ((parms[i].Type & 1) != 1)
					{
						goto IL_02F6;
					}
					TaskAwaiter<int> taskAwaiter = reader.ReadUnsignedByteAsync(isAsync, cancellationToken).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<int> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<int>);
					}
					if (!this.IsNullIndicator((byte)(taskAwaiter.GetResult() & 255)))
					{
						goto IL_02F6;
					}
					IL_0371:
					i++;
					continue;
					IL_02F6:
					await base.ReadAndSetParamsAsync(reader, parms[i], isAsync, cancellationToken);
					goto IL_0371;
				}
			}
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x000FB268 File Offset: 0x000F9468
		protected override async Task ParseFDODSCAsync(DdmReader reader, List<DrdaParameterInfo> parms, bool isAsync, CancellationToken cancellationToken)
		{
			this.drdaTypeMappings.Clear();
			while (reader.DdmObjectLength > 6L)
			{
				int num = await reader.ReadUnsignedByteAsync(isAsync, cancellationToken);
				int dtaGrpLen = num;
				int numVarsInGrp = (dtaGrpLen - 3) / 3;
				int num2 = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
				if (num2 == 120)
				{
					await reader.ReadInt16Async(isAsync, cancellationToken);
					int num3 = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
					if (num3 == 1)
					{
						FdocscDrdaTypeMapping drdaTypeMapping = new FdocscDrdaTypeMapping();
						FdocscDrdaTypeMapping fdocscDrdaTypeMapping = drdaTypeMapping;
						fdocscDrdaTypeMapping.ReferencType = await reader.ReadByteAsync(isAsync, cancellationToken);
						fdocscDrdaTypeMapping = null;
						fdocscDrdaTypeMapping = drdaTypeMapping;
						fdocscDrdaTypeMapping.FromDrdaType = await reader.ReadByteAsync(isAsync, cancellationToken);
						fdocscDrdaTypeMapping = null;
						dtaGrpLen = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
						num2 = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
						if (num2 == 112)
						{
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.TripletLID = await reader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.ToDrdaType = await reader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							await reader.ReadInt16Async(isAsync, cancellationToken);
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.CCSID = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							if (base.CcsidConvert != null)
							{
								drdaTypeMapping.CCSID = (short)base.CcsidConvert((ushort)drdaTypeMapping.CCSID);
							}
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.CharSize = await reader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.Mode = await reader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.Precision = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							this.drdaTypeMappings.Add(drdaTypeMapping);
						}
						else
						{
							if (Logger.maxTracingLevel >= 2)
							{
								Logger.Warning(this._tracePoint, base.DatabaseSessionId, "MDD triplet does not have mapping SDA triplet type", Array.Empty<object>());
							}
							await reader.SkipBytesAsync(dtaGrpLen - 2, isAsync, cancellationToken);
							drdaTypeMapping = null;
						}
					}
					else if (num3 == 2)
					{
						await reader.ReadByteAsync(isAsync, cancellationToken);
						await reader.ReadByteAsync(isAsync, cancellationToken);
					}
					else if (num3 == 3)
					{
						await reader.ReadByteAsync(isAsync, cancellationToken);
						await reader.ReadByteAsync(isAsync, cancellationToken);
					}
					else
					{
						if (Logger.maxTracingLevel >= 2)
						{
							Logger.Warning(this._tracePoint, base.DatabaseSessionId, "Unknow descriptor Type: " + num3.ToString() + " with MDD_TRIPLET_TYPE", Array.Empty<object>());
						}
						await reader.SkipBytesAsync(dtaGrpLen - 5, isAsync, cancellationToken);
					}
				}
				else if (num2 == 118 || num2 == 127)
				{
					await reader.ReadByteAsync(isAsync, cancellationToken);
					for (int i = 0; i < numVarsInGrp; i = num + 1)
					{
						byte type = await reader.ReadByteAsync(isAsync, cancellationToken);
						parms.Add(new DrdaParameterInfo(type, await reader.ReadUInt16Async(EndianType.BigEndian, isAsync, cancellationToken)));
						num = i;
					}
				}
				else
				{
					if (Logger.maxTracingLevel >= 2)
					{
						Logger.Warning(this._tracePoint, this._requesterId, "FDOCDSC contains recognized triplet type: " + num2.ToString(), Array.Empty<object>());
					}
					await reader.SkipBytesAsync(dtaGrpLen - 2, isAsync, cancellationToken);
				}
			}
			await reader.ReadBytesAsync(6, isAsync, cancellationToken);
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x000FB2CE File Offset: 0x000F94CE
		private bool IsNullIndicator(byte nullIndicatorValue)
		{
			return nullIndicatorValue == byte.MaxValue || nullIndicatorValue == 128 || nullIndicatorValue == 254;
		}

		// Token: 0x0400330D RID: 13069
		private int _requesterId;

		// Token: 0x0400330E RID: 13070
		private SQLCAGRP _sqlca;
	}
}
