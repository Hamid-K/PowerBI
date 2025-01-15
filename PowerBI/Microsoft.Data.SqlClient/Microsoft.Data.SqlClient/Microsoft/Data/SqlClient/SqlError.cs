using System;
using System.Runtime.Serialization;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200007D RID: 125
	[Serializable]
	public sealed class SqlError
	{
		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001FD80 File Offset: 0x0001DF80
		internal SqlError(int infoNumber, byte errorState, byte errorClass, string server, string errorMessage, string procedure, int lineNumber, uint win32ErrorCode, Exception exception = null)
			: this(infoNumber, errorState, errorClass, server, errorMessage, procedure, lineNumber, exception)
		{
			this._server = server;
			this._win32ErrorCode = (int)win32ErrorCode;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001FDB0 File Offset: 0x0001DFB0
		internal SqlError(int infoNumber, byte errorState, byte errorClass, string server, string errorMessage, string procedure, int lineNumber, Exception exception = null)
		{
			this._number = infoNumber;
			this._state = errorState;
			this._errorClass = errorClass;
			this._server = server;
			this._message = errorMessage;
			this._procedure = procedure;
			this._lineNumber = lineNumber;
			this._win32ErrorCode = 0;
			this._exception = exception;
			if (errorClass != 0)
			{
				SqlClientEventSource.Log.TryTraceEvent<int, int, int, string, string, int>("SqlError.ctor | ERR | Info Number {0}, Error State {1}, Error Class {2}, Error Message '{3}', Procedure '{4}', Line Number {5}", infoNumber, (int)errorState, (int)errorClass, errorMessage, procedure ?? "None", lineNumber);
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001FE36 File Offset: 0x0001E036
		public override string ToString()
		{
			return typeof(SqlError).ToString() + ": " + this.Message;
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0001FE57 File Offset: 0x0001E057
		public string Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0001FE5F File Offset: 0x0001E05F
		public int Number
		{
			get
			{
				return this._number;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0001FE67 File Offset: 0x0001E067
		public byte State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0001FE6F File Offset: 0x0001E06F
		public byte Class
		{
			get
			{
				return this._errorClass;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0001FE77 File Offset: 0x0001E077
		public string Server
		{
			get
			{
				return this._server;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0001FE7F File Offset: 0x0001E07F
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0001FE87 File Offset: 0x0001E087
		public string Procedure
		{
			get
			{
				return this._procedure;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001FE8F File Offset: 0x0001E08F
		public int LineNumber
		{
			get
			{
				return this._lineNumber;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0001FE97 File Offset: 0x0001E097
		internal int Win32ErrorCode
		{
			get
			{
				return this._win32ErrorCode;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0001FE9F File Offset: 0x0001E09F
		internal Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x04000297 RID: 663
		private readonly string _source = "Framework Microsoft SqlClient Data Provider";

		// Token: 0x04000298 RID: 664
		private readonly int _number;

		// Token: 0x04000299 RID: 665
		private readonly byte _state;

		// Token: 0x0400029A RID: 666
		private readonly byte _errorClass;

		// Token: 0x0400029B RID: 667
		[OptionalField(VersionAdded = 2)]
		private readonly string _server;

		// Token: 0x0400029C RID: 668
		private readonly string _message;

		// Token: 0x0400029D RID: 669
		private readonly string _procedure;

		// Token: 0x0400029E RID: 670
		private readonly int _lineNumber;

		// Token: 0x0400029F RID: 671
		[OptionalField(VersionAdded = 4)]
		private readonly int _win32ErrorCode;

		// Token: 0x040002A0 RID: 672
		[OptionalField(VersionAdded = 5)]
		private readonly Exception _exception;
	}
}
