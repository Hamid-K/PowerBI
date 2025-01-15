using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008AC RID: 2220
	public class SQLCAGRP : AbstractDdmObject
	{
		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x0600469D RID: 18077 RVA: 0x000F79BA File Offset: 0x000F5BBA
		public bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x0600469E RID: 18078 RVA: 0x000F79C2 File Offset: 0x000F5BC2
		// (set) Token: 0x0600469F RID: 18079 RVA: 0x000F79CA File Offset: 0x000F5BCA
		public EndianType ByteOrder
		{
			get
			{
				return this._byteOrder;
			}
			set
			{
				this._byteOrder = value;
			}
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x000F79D3 File Offset: 0x000F5BD3
		public SQLCAGRP(IDatabase database, int sqlamLevel)
			: base(database, sqlamLevel)
		{
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x000F7A08 File Offset: 0x000F5C08
		public override string ToString()
		{
			return string.Format("SQLCAGRP[rdbnam={0};updatecount={1};rowcount={2};sqlstat{3};sqlcode={4}]", new object[] { this._rdbname, this._updateCount, this._rowCount, this._sqlState, this._sqlCode });
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x000F7A64 File Offset: 0x000F5C64
		public override void Reset()
		{
			this._updateCount = 0;
			this._rowCount = 0L;
			this._diagnosticLevel = 240;
			this._rdbname = null;
			this._sqlServerName = "MSAS0100";
			this._exceptionInfo = null;
			this._sqlState = null;
			this._sqlCode = 0;
			this._byteOrder = EndianType.LittleEndian;
			this.SqlWarningMessage = string.Empty;
			Array.Clear(this._errorCode, 0, this._errorCode.Length);
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x000F7AD8 File Offset: 0x000F5CD8
		// (set) Token: 0x060046A4 RID: 18084 RVA: 0x000F7AE0 File Offset: 0x000F5CE0
		public string SqlServerName
		{
			get
			{
				return this._sqlServerName;
			}
			set
			{
				this._sqlServerName = value;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x060046A5 RID: 18085 RVA: 0x000F7AE9 File Offset: 0x000F5CE9
		// (set) Token: 0x060046A6 RID: 18086 RVA: 0x000F7AF1 File Offset: 0x000F5CF1
		public int SqlCode
		{
			get
			{
				return this._sqlCode;
			}
			set
			{
				this._sqlCode = value;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x060046A7 RID: 18087 RVA: 0x000F7AFA File Offset: 0x000F5CFA
		// (set) Token: 0x060046A8 RID: 18088 RVA: 0x000F7B02 File Offset: 0x000F5D02
		public string SqlState
		{
			get
			{
				return this._sqlState;
			}
			set
			{
				this._sqlState = value;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x060046A9 RID: 18089 RVA: 0x000F7B0B File Offset: 0x000F5D0B
		// (set) Token: 0x060046AA RID: 18090 RVA: 0x000F7B13 File Offset: 0x000F5D13
		public int UpdateCount
		{
			get
			{
				return this._updateCount;
			}
			set
			{
				this._updateCount = value;
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x060046AB RID: 18091 RVA: 0x000F7B1C File Offset: 0x000F5D1C
		// (set) Token: 0x060046AC RID: 18092 RVA: 0x000F7B24 File Offset: 0x000F5D24
		public long RowCount
		{
			get
			{
				return this._rowCount;
			}
			set
			{
				this._rowCount = value;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x060046AD RID: 18093 RVA: 0x000F7B2D File Offset: 0x000F5D2D
		// (set) Token: 0x060046AE RID: 18094 RVA: 0x000F7B35 File Offset: 0x000F5D35
		public byte DiagnosticLevel
		{
			get
			{
				return this._diagnosticLevel;
			}
			set
			{
				this._diagnosticLevel = value;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x060046AF RID: 18095 RVA: 0x000F7B3E File Offset: 0x000F5D3E
		// (set) Token: 0x060046B0 RID: 18096 RVA: 0x000F7B46 File Offset: 0x000F5D46
		public IDbExceptionInfo ExceptionInfo
		{
			get
			{
				return this._exceptionInfo;
			}
			set
			{
				this._exceptionInfo = value;
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x060046B1 RID: 18097 RVA: 0x000F7B4F File Offset: 0x000F5D4F
		// (set) Token: 0x060046B2 RID: 18098 RVA: 0x000F7B57 File Offset: 0x000F5D57
		public string DatabaseName
		{
			get
			{
				return this._rdbname;
			}
			set
			{
				this._rdbname = value;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x060046B3 RID: 18099 RVA: 0x000F7B60 File Offset: 0x000F5D60
		public string SqlErrorMessage
		{
			get
			{
				return this._sqlerrmc;
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x060046B4 RID: 18100 RVA: 0x000F7B68 File Offset: 0x000F5D68
		public int[] SqlErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x060046B5 RID: 18101 RVA: 0x000F7B70 File Offset: 0x000F5D70
		// (set) Token: 0x060046B6 RID: 18102 RVA: 0x000F7B78 File Offset: 0x000F5D78
		public string SqlWarningMessage { get; private set; }

		// Token: 0x060046B7 RID: 18103 RVA: 0x000F7B84 File Offset: 0x000F5D84
		private void WriteBytes(DdmWriter writer, string p)
		{
			string[] array = p.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				writer.WriteByte((int)byte.Parse(array[i], NumberStyles.HexNumber));
			}
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x000F7BC8 File Offset: 0x000F5DC8
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			TaskAwaiter<byte> taskAwaiter2;
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			if (taskAwaiter.GetResult() == 255)
			{
				this._isNull = true;
			}
			else
			{
				int encoding = ((reader.Ccsid != null && reader.Ccsid._ccsidmbc > 0) ? reader.Ccsid._ccsidmbc : 1208);
				this._sqlCode = await reader.ReadInt32Async(isAsync, cancellationToken);
				this._sqlState = await reader.ReadStringAsync(5, encoding, isAsync, cancellationToken);
				this._sqlServerName = await reader.ReadStringAsync(8, encoding, isAsync, cancellationToken);
				taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<byte>);
				}
				if (taskAwaiter.GetResult() != 255)
				{
					await this.ReadSQLCAERRWARNAsync(reader, encoding, isAsync, cancellationToken);
					this._rdbname = (await reader.ReadLDStringAsync(encoding, isAsync, cancellationToken)).Trim();
					this._sqlerrmc = await base.ParseVCMorVCSAsync(reader, (reader.Ccsid != null && reader.Ccsid._ccsidmbc > 0) ? reader.Ccsid : new Ccsid(), isAsync, cancellationToken);
				}
				if (this._sqlamLevel >= 7)
				{
					taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<byte>);
					}
					if (taskAwaiter.GetResult() != 255)
					{
						await this.ReadSQLDIAGGRPAsync(reader, encoding, isAsync, cancellationToken);
					}
				}
			}
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x000F7C28 File Offset: 0x000F5E28
		private async Task ReadSQLDIAGGRPAsync(DdmReader reader, int encoding, bool isAsync, CancellationToken cancellationToken)
		{
			TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			TaskAwaiter<byte> taskAwaiter2;
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			if (taskAwaiter.GetResult() != 255)
			{
				await this.ReadSQLDIAGSTTAsync(reader, encoding, isAsync, cancellationToken);
			}
			taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			if (taskAwaiter.GetResult() != 255)
			{
				await this.ReadSQLDIAGCIAsync(reader, encoding, isAsync, cancellationToken);
			}
			taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			if (taskAwaiter.GetResult() != 255)
			{
				await this.ReadSQLDIAGCNAsync(reader, encoding, isAsync, cancellationToken);
			}
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x000F7C90 File Offset: 0x000F5E90
		private async Task ReadSQLDIAGSTTAsync(DdmReader reader, int encoding, bool isAsync, CancellationToken cancellationToken)
		{
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt64Async(isAsync, cancellationToken);
			await reader.ReadInt64Async(isAsync, cancellationToken);
			await reader.ReadInt64Async(isAsync, cancellationToken);
			await reader.ReadStringAsync(8, encoding, isAsync, cancellationToken);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x000F7CF0 File Offset: 0x000F5EF0
		private async Task ReadSQLDIAGCIAsync(DdmReader reader, int encoding, bool isAsync, CancellationToken cancellationToken)
		{
			short num = await reader.ReadInt16Async(isAsync, cancellationToken);
			int numberOfExceptions = (int)num;
			for (int i = 0; i < numberOfExceptions; i++)
			{
				await this.ReadSQLDCGRPAsync(reader, encoding, isAsync, cancellationToken);
			}
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x000F7D58 File Offset: 0x000F5F58
		private async Task ReadSQLDCGRPAsync(DdmReader reader, int encoding, bool isAsync, CancellationToken cancellationToken)
		{
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadStringAsync(5, isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt64Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadStringAsync(10, isAsync, cancellationToken);
			await reader.ReadStringAsync(8, isAsync, cancellationToken);
			await reader.ReadStringAsync(5, isAsync, cancellationToken);
			await reader.ReadLDStringAsync(encoding, isAsync, cancellationToken);
			TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			TaskAwaiter<byte> taskAwaiter2;
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			if (taskAwaiter.GetResult() != 255)
			{
				await this.ReadSQLDCTOKSAsync(reader, isAsync, cancellationToken);
			}
			Ccsid ccsid = ((reader.Ccsid != null && reader.Ccsid._ccsidmbc > 0 && reader.Ccsid._ccsidsbc > 0) ? reader.Ccsid : new Ccsid());
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			if (taskAwaiter.GetResult() != 255)
			{
				await this.ReadSQLDCXGRPAsync(reader, encoding, isAsync, cancellationToken);
			}
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x000F7DC0 File Offset: 0x000F5FC0
		private async Task ReadSQLDCTOKSAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			short num = await reader.ReadInt16Async(isAsync, cancellationToken);
			int numberOfTokens = (int)num;
			Ccsid ccsid = ((reader.Ccsid != null && reader.Ccsid._ccsidmbc > 0 && reader.Ccsid._ccsidsbc > 0) ? reader.Ccsid : new Ccsid());
			for (int i = 0; i < numberOfTokens; i++)
			{
				await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			}
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x000F7E20 File Offset: 0x000F6020
		private async Task ReadSQLDCXGRPAsync(DdmReader reader, int encoding, bool isAsync, CancellationToken cancellationToken)
		{
			Ccsid ccsid = ((reader.Ccsid != null && reader.Ccsid._ccsidmbc > 0 && reader.Ccsid._ccsidsbc > 0) ? reader.Ccsid : new Ccsid());
			await reader.ReadLDStringAsync(encoding, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			await reader.ReadLDStringAsync(encoding, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			await reader.ReadLDStringAsync(encoding, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
			await base.ParseNVCMorNVCSAsync(reader, ccsid, isAsync, cancellationToken);
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x000F7E88 File Offset: 0x000F6088
		private async Task ReadSQLDIAGCNAsync(DdmReader reader, int encoding, bool isAsync, CancellationToken cancellationToken)
		{
			short num = await reader.ReadInt16Async(isAsync, cancellationToken);
			int numberOfRow = (int)num;
			for (int i = 0; i < numberOfRow; i++)
			{
				await this.ReadSQLCNGRPAsync(reader, encoding, isAsync, cancellationToken);
			}
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x000F7EF0 File Offset: 0x000F60F0
		private async Task ReadSQLCNGRPAsync(DdmReader reader, int encoding, bool isAsync, CancellationToken cancellationToken)
		{
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadInt32Async(isAsync, cancellationToken);
			await reader.ReadStringAsync(10, encoding, isAsync, cancellationToken);
			await reader.ReadLDStringAsync(encoding, isAsync, cancellationToken);
			await reader.ReadLDStringAsync(encoding, isAsync, cancellationToken);
			await reader.ReadLDStringAsync(encoding, isAsync, cancellationToken);
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x000F7F50 File Offset: 0x000F6150
		private async Task ReadSQLCAERRWARNAsync(DdmReader reader, int encoding, bool isAsync, CancellationToken cancellationToken)
		{
			int num = await reader.ReadInt32Async(isAsync, cancellationToken);
			this._errorCode[0] = num;
			num = await reader.ReadInt32Async(isAsync, cancellationToken);
			this._errorCode[1] = num;
			num = await reader.ReadInt32Async(isAsync, cancellationToken);
			this._updateCount = num;
			this._errorCode[2] = this._updateCount;
			num = await reader.ReadInt32Async(isAsync, cancellationToken);
			this._errorCode[3] = num;
			num = await reader.ReadInt32Async(isAsync, cancellationToken);
			this._errorCode[4] = num;
			num = await reader.ReadInt32Async(isAsync, cancellationToken);
			this._errorCode[5] = num;
			this.SqlWarningMessage = await reader.ReadStringAsync(11, encoding, isAsync, cancellationToken);
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x000F7FB8 File Offset: 0x000F61B8
		public override void Write(DdmWriter writer)
		{
			writer.WriteByte(0);
			if (this._exceptionInfo == null)
			{
				writer.WriteInt32(this.SqlCode, this.ByteOrder);
				if (this.SqlState == null)
				{
					writer.WriteStringSBCS(Constants.NULL_SQL_STATE_STR);
				}
				else
				{
					writer.WriteStringSBCS(this.SqlState);
				}
			}
			else
			{
				writer.WriteInt32(this._exceptionInfo.SqlCode, this.ByteOrder);
				writer.WriteStringSBCS(this._exceptionInfo.SqlState);
			}
			writer.WriteStringSBCS(this._sqlServerName);
			writer.WriteByte(0);
			if (this._rdbname == null)
			{
				this._rdbname = string.Empty;
			}
			int num = this._rdbname.Length;
			if (num < 18)
			{
				num = 18;
			}
			if (this._sqlamLevel < 7)
			{
				writer.WriteScalarPaddedString(this._rdbname, num);
				this.WriteSQLCAERRWARN(writer);
			}
			else
			{
				this.WriteSQLCAERRWARN(writer);
				writer.WriteScalar2Bytes(num);
				writer.WriteScalarPaddedString(this._rdbname, num);
			}
			string text = null;
			if (this._exceptionInfo != null)
			{
				text = this._exceptionInfo.Message;
			}
			base.WriteVCMorVCS(writer, text);
			if (this._sqlamLevel >= 7)
			{
				this.WriteSQLDIAGGRP(writer, this._diagnosticLevel, this._exceptionInfo);
			}
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x000F80E0 File Offset: 0x000F62E0
		private void WriteSQLDIAGGRP(DdmWriter writer, byte diagnosticLevel, IDbExceptionInfo exceptionInfo)
		{
			if (this._exceptionInfo == null || this._diagnosticLevel == 240)
			{
				writer.WriteByte(255);
				return;
			}
			writer.WriteByte(0);
			writer.WriteByte(255);
			this.WriteSQLDIAGCI(writer, (int)diagnosticLevel, exceptionInfo);
			writer.WriteByte(255);
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x000F8134 File Offset: 0x000F6334
		private void WriteSQLDIAGCI(DdmWriter writer, int diagnosticLevel, IDbExceptionInfo exceptionInfo)
		{
			int num = 0;
			if (exceptionInfo != null)
			{
				for (IDbExceptionInfo dbExceptionInfo = exceptionInfo; dbExceptionInfo != null; dbExceptionInfo = dbExceptionInfo.InnerExceptionInfo)
				{
					num++;
				}
			}
			writer.WriteInt16(num);
			if (exceptionInfo != null)
			{
				int num2 = 1;
				IDbExceptionInfo dbExceptionInfo2 = exceptionInfo;
				while (dbExceptionInfo2 != null)
				{
					SeverityCode severityCode = exceptionInfo.SeverityCode;
					int num3;
					if (severityCode != SeverityCode.Info)
					{
						if (severityCode == SeverityCode.Warning)
						{
							num3 = 1;
						}
						else
						{
							num3 = -1;
						}
					}
					else
					{
						num3 = 0;
					}
					this.WriteSQLDCGRP(writer, (long)num2, num3, dbExceptionInfo2.SqlState, "", dbExceptionInfo2.Message);
					dbExceptionInfo2 = dbExceptionInfo2.InnerExceptionInfo;
					num2++;
				}
			}
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x000F81B4 File Offset: 0x000F63B4
		private void WriteSQLDCGRP(DdmWriter writer, long rowNum, int sqlCode, string sqlState, string dbname, string sqlerrmc)
		{
			writer.WriteInt32(sqlCode);
			writer.WriteStringSBCS(sqlState);
			writer.WriteInt32(0);
			writer.WriteInt32(0);
			writer.WriteInt64(rowNum);
			writer.WriteScalarPaddedBytes(new byte[1], 47, 0);
			writer.WriteInt16(0);
			writer.WriteByte(255);
			writer.WriteLDString(sqlerrmc);
			base.WriteVCMorVCS(writer, null);
			base.WriteVCMorVCS(writer, null);
			base.WriteVCMorVCS(writer, null);
			writer.WriteByte(255);
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x000F8234 File Offset: 0x000F6434
		private void WriteSQLCAERRWARN(DdmWriter writer)
		{
			writer.WriteInt32((int)BitUtils.UnsignedRightShift(this._rowCount, 32));
			writer.WriteInt32((int)(this._rowCount & (long)((ulong)(-1))));
			writer.WriteInt32(this._updateCount);
			writer.WriteBytes(Constants.ERROR_D4_D6);
			writer.WriteStringSBCS(Constants.WARN_0_A_STR);
		}

		// Token: 0x0400328E RID: 12942
		private int _updateCount;

		// Token: 0x0400328F RID: 12943
		private long _rowCount;

		// Token: 0x04003290 RID: 12944
		private byte _diagnosticLevel = 240;

		// Token: 0x04003291 RID: 12945
		private string _rdbname;

		// Token: 0x04003292 RID: 12946
		private string _sqlServerName = "MSAS0100";

		// Token: 0x04003293 RID: 12947
		private IDbExceptionInfo _exceptionInfo;

		// Token: 0x04003294 RID: 12948
		private string _sqlState;

		// Token: 0x04003295 RID: 12949
		private int _sqlCode;

		// Token: 0x04003296 RID: 12950
		private EndianType _byteOrder = EndianType.LittleEndian;

		// Token: 0x04003297 RID: 12951
		private string _sqlerrmc;

		// Token: 0x04003298 RID: 12952
		private bool _isNull;

		// Token: 0x04003299 RID: 12953
		private int[] _errorCode = new int[6];
	}
}
