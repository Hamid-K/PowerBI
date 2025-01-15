using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008BC RID: 2236
	public class SQLCARD : AbstractDdmObject
	{
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x060046EA RID: 18154 RVA: 0x000FC62E File Offset: 0x000FA82E
		// (set) Token: 0x060046EB RID: 18155 RVA: 0x000FC636 File Offset: 0x000FA836
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

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x060046EC RID: 18156 RVA: 0x000FC63F File Offset: 0x000FA83F
		internal SQLCAGRP Sqlcagrp
		{
			get
			{
				if (this._sqlcarp == null)
				{
					this._sqlcarp = new SQLCAGRP(this._database, this._sqlamLevel);
				}
				else
				{
					this._sqlcarp.Reset();
				}
				return this._sqlcarp;
			}
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x000FC674 File Offset: 0x000FA874
		public override string ToString()
		{
			return string.Format("SQLCARD[rdbnam={0};updatecount={1};rowcount={2};sqlstat={3};sqlcode={4};sqlcagrp={5}]", new object[] { this._rdbnam, this._updateCount, this._rowCount, this._sqlState, this._sqlCode, this._sqlcarp });
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x000FC6D6 File Offset: 0x000FA8D6
		public SQLCARD(IDatabase database, int sqlamlelvel)
			: base(database, sqlamlelvel)
		{
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x000FC6FD File Offset: 0x000FA8FD
		public SQLCARD(IDatabase database, int sqlamlelvel, string accrdbTypedefname)
			: base(database, sqlamlelvel, accrdbTypedefname)
		{
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x000FC728 File Offset: 0x000FA928
		public override void Reset()
		{
			this._updateCount = 0;
			this._rowCount = 0L;
			this._diagnosticLevel = 240;
			this._rdbnam = string.Empty;
			this._exceptionInfo = null;
			this._sqlState = null;
			this._sqlCode = 0;
			this._byteOrder = EndianType.LittleEndian;
			this._sqlcarp.Reset();
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x060046F1 RID: 18161 RVA: 0x000FC781 File Offset: 0x000FA981
		// (set) Token: 0x060046F2 RID: 18162 RVA: 0x000FC789 File Offset: 0x000FA989
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

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x060046F3 RID: 18163 RVA: 0x000FC792 File Offset: 0x000FA992
		// (set) Token: 0x060046F4 RID: 18164 RVA: 0x000FC79A File Offset: 0x000FA99A
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

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x060046F5 RID: 18165 RVA: 0x000FC7A3 File Offset: 0x000FA9A3
		// (set) Token: 0x060046F6 RID: 18166 RVA: 0x000FC7AB File Offset: 0x000FA9AB
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

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x060046F7 RID: 18167 RVA: 0x000FC7B4 File Offset: 0x000FA9B4
		// (set) Token: 0x060046F8 RID: 18168 RVA: 0x000FC7BC File Offset: 0x000FA9BC
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

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x060046F9 RID: 18169 RVA: 0x000FC7C5 File Offset: 0x000FA9C5
		// (set) Token: 0x060046FA RID: 18170 RVA: 0x000FC7CD File Offset: 0x000FA9CD
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

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x000FC7D6 File Offset: 0x000FA9D6
		// (set) Token: 0x060046FC RID: 18172 RVA: 0x000FC7DE File Offset: 0x000FA9DE
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

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x060046FD RID: 18173 RVA: 0x000FC7E7 File Offset: 0x000FA9E7
		// (set) Token: 0x060046FE RID: 18174 RVA: 0x000FC7EF File Offset: 0x000FA9EF
		public string DatabaseName
		{
			get
			{
				return this._rdbnam;
			}
			set
			{
				this._rdbnam = value;
			}
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x000FC7F8 File Offset: 0x000FA9F8
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<byte> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<byte>);
			}
			if (taskAwaiter.GetResult() != 255)
			{
				await this.Sqlcagrp.ReadAsync(reader, isAsync, cancellationToken);
				this._sqlCode = this._sqlcarp.SqlCode;
				this._sqlState = this._sqlcarp.SqlState;
				this._updateCount = this._sqlcarp.UpdateCount;
				this._rdbnam = this._sqlcarp.DatabaseName;
				this._rowCount = this._sqlcarp.RowCount;
				this._exceptionInfo = this._sqlcarp.ExceptionInfo;
			}
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x000FC855 File Offset: 0x000FAA55
		public static void WriteNullSqlCard(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.SQLCARD);
			writer.WriteByte(255);
			writer.WriteEndDdm();
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x000FC874 File Offset: 0x000FAA74
		public override void Write(DdmWriter writer)
		{
			if (this.RowCount < 0L && this.UpdateCount < 0)
			{
				SQLCARD.WriteNullSqlCard(writer);
				return;
			}
			writer.WriteBeginDdm(CodePoint.SQLCARD);
			SQLCAGRP sqlcagrp = this.Sqlcagrp;
			sqlcagrp.RowCount = this.RowCount;
			sqlcagrp.UpdateCount = this.UpdateCount;
			sqlcagrp.DatabaseName = this.DatabaseName;
			sqlcagrp.SqlCode = this.SqlCode;
			sqlcagrp.SqlState = this.SqlState;
			sqlcagrp.ExceptionInfo = this.ExceptionInfo;
			sqlcagrp.ByteOrder = this._byteOrder;
			sqlcagrp.Write(writer);
			writer.WriteEndDdm();
		}

		// Token: 0x0400332E RID: 13102
		private int _updateCount;

		// Token: 0x0400332F RID: 13103
		private long _rowCount;

		// Token: 0x04003330 RID: 13104
		private byte _diagnosticLevel = 240;

		// Token: 0x04003331 RID: 13105
		private string _rdbnam = string.Empty;

		// Token: 0x04003332 RID: 13106
		private IDbExceptionInfo _exceptionInfo;

		// Token: 0x04003333 RID: 13107
		private string _sqlState;

		// Token: 0x04003334 RID: 13108
		private int _sqlCode;

		// Token: 0x04003335 RID: 13109
		private SQLCAGRP _sqlcarp;

		// Token: 0x04003336 RID: 13110
		private EndianType _byteOrder = EndianType.LittleEndian;
	}
}
