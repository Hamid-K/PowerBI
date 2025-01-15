using System;
using System.Data.SqlClient;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000837 RID: 2103
	public class SqlExceptionInfo : Exception, IDbExceptionInfo
	{
		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x000E0557 File Offset: 0x000DE757
		public bool Mapped
		{
			get
			{
				return this._mapped;
			}
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x000E055F File Offset: 0x000DE75F
		public SqlExceptionInfo(SqlException ex, string errorcode, string sqlstate, string msg)
		{
			this._sqlException = ex;
			this._errorcode = errorcode;
			this._sqlstate = sqlstate;
			this._msg = msg;
			this._mapped = true;
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x000E058B File Offset: 0x000DE78B
		public SqlExceptionInfo(Exception ex, string errorcode, string sqlstate, string msg)
		{
			this._ex = ex;
			this._errorcode = errorcode;
			this._sqlstate = sqlstate;
			this._msg = msg;
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x000E05B0 File Offset: 0x000DE7B0
		public SqlExceptionInfo(SqlException ex)
		{
			this._sqlException = ex;
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x000E05C0 File Offset: 0x000DE7C0
		public string ErrorCode
		{
			get
			{
				if (this._ex != null)
				{
					return this._errorcode;
				}
				if (!this._mapped)
				{
					return this._sqlException.ErrorCode.ToString();
				}
				return this._errorcode;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x060042EC RID: 17132 RVA: 0x000E0600 File Offset: 0x000DE800
		public IDbExceptionInfo InnerExceptionInfo
		{
			get
			{
				if (this._ex != null && this._ex.InnerException != null && this._ex.InnerException is IDbExceptionInfo)
				{
					return (IDbExceptionInfo)this._ex.InnerException;
				}
				if (this._sqlException.InnerException != null)
				{
					return new SqlExceptionInfo((SqlException)this._sqlException.InnerException);
				}
				return null;
			}
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x060042ED RID: 17133 RVA: 0x000E0669 File Offset: 0x000DE869
		public int LineNumber
		{
			get
			{
				if (this._ex == null)
				{
					return this._sqlException.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x060042EE RID: 17134 RVA: 0x000E0680 File Offset: 0x000DE880
		public override string Message
		{
			get
			{
				if (this._ex != null)
				{
					return this._msg;
				}
				if (!this._mapped)
				{
					return string.Format("SqlExceptionInfo: Code={0}; State={1}; Message:{2}", this._sqlException.ErrorCode, this._sqlException.State, this._sqlException.Message);
				}
				return this._msg;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x060042EF RID: 17135 RVA: 0x000E06E0 File Offset: 0x000DE8E0
		public int Number
		{
			get
			{
				if (this._ex == null)
				{
					return this._sqlException.Number;
				}
				return 0;
			}
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x060042F0 RID: 17136 RVA: 0x000E06F7 File Offset: 0x000DE8F7
		public string Procedure
		{
			get
			{
				if (this._ex == null)
				{
					return this._sqlException.Procedure;
				}
				return "";
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x060042F1 RID: 17137 RVA: 0x000E0712 File Offset: 0x000DE912
		public string Server
		{
			get
			{
				if (this._ex == null)
				{
					return this._sqlException.Server;
				}
				return "";
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x060042F2 RID: 17138 RVA: 0x000E072D File Offset: 0x000DE92D
		public SeverityCode SeverityCode
		{
			get
			{
				if (this._ex != null)
				{
					return SeverityCode.Error;
				}
				if (this._sqlException.Class <= 10)
				{
					return SeverityCode.Info;
				}
				return SeverityCode.Error;
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x060042F3 RID: 17139 RVA: 0x000E074B File Offset: 0x000DE94B
		public int SqlCode
		{
			get
			{
				if (this._ex != null)
				{
					return int.Parse(this._errorcode);
				}
				if (!this._mapped)
				{
					return this.GetSqlCode(this.SeverityCode);
				}
				return int.Parse(this._errorcode);
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x060042F4 RID: 17140 RVA: 0x000E0781 File Offset: 0x000DE981
		public string SqlState
		{
			get
			{
				if (this._ex != null)
				{
					return this._sqlstate;
				}
				if (!this._mapped)
				{
					return "     ";
				}
				return string.Format("{0,5:X}", this._sqlstate);
			}
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000E07B0 File Offset: 0x000DE9B0
		private int GetSqlCode(SeverityCode severity)
		{
			if (severity == SeverityCode.Info)
			{
				return 0;
			}
			if (severity == SeverityCode.Warning)
			{
				return 100;
			}
			return -1;
		}

		// Token: 0x04002EEE RID: 12014
		private SqlException _sqlException;

		// Token: 0x04002EEF RID: 12015
		private string _errorcode;

		// Token: 0x04002EF0 RID: 12016
		private string _sqlstate;

		// Token: 0x04002EF1 RID: 12017
		private string _msg;

		// Token: 0x04002EF2 RID: 12018
		private bool _mapped;

		// Token: 0x04002EF3 RID: 12019
		private Exception _ex;
	}
}
