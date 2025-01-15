using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x020006E3 RID: 1763
	internal abstract class OdbcHandle : SafeHandle
	{
		// Token: 0x060034FA RID: 13562 RVA: 0x000AAB7C File Offset: 0x000A8D7C
		protected OdbcHandle(IOdbcInterop odbcInterop, Odbc32.SQL_HANDLE handleType, OdbcHandle parentHandle)
			: base(IntPtr.Zero, true)
		{
			this.handleType = handleType;
			this.odbcInterop = odbcInterop;
			bool flag = false;
			bool flag2 = false;
			Odbc32.RetCode retCode = Odbc32.RetCode.SUCCESS;
			SafeHandle libraryHandle = odbcInterop.LibraryHandle;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				libraryHandle.DangerousAddRef(ref flag);
				if (handleType != Odbc32.SQL_HANDLE.ENV)
				{
					if (handleType - Odbc32.SQL_HANDLE.DBC <= 1)
					{
						parentHandle.DangerousAddRef(ref flag2);
						retCode = this.odbcInterop.SQLAllocHandle(handleType, parentHandle, out this.handle);
					}
				}
				else
				{
					retCode = this.odbcInterop.SQLAllocHandle(handleType, IntPtr.Zero, out this.handle);
				}
			}
			finally
			{
				bool flag3 = IntPtr.Zero != this.handle;
				if (flag2 && handleType - Odbc32.SQL_HANDLE.DBC <= 1)
				{
					if (flag3)
					{
						this.parentHandle = parentHandle;
					}
					else
					{
						this.parentHandle.DangerousRelease();
					}
				}
				if (flag)
				{
					if (flag3)
					{
						this.odbcLibraryHandle = libraryHandle;
					}
					else
					{
						libraryHandle.DangerousRelease();
					}
				}
			}
			if (IntPtr.Zero == this.handle || retCode != Odbc32.RetCode.SUCCESS)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Failed to allocate ODBC handle of type '{0}'.", handleType));
			}
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000AAC90 File Offset: 0x000A8E90
		protected OdbcHandle(IOdbcInterop odbcInterop, OdbcStatementHandle parentHandle, Odbc32.SQL_ATTR attribute)
			: base(IntPtr.Zero, true)
		{
			this.odbcInterop = odbcInterop;
			this.handleType = Odbc32.SQL_HANDLE.DESC;
			bool flag = false;
			bool flag2 = false;
			SafeHandle libraryHandle = this.odbcInterop.LibraryHandle;
			RuntimeHelpers.PrepareConstrainedRegions();
			Odbc32.RetCode statementAttribute;
			try
			{
				libraryHandle.DangerousAddRef(ref flag);
				parentHandle.DangerousAddRef(ref flag2);
				int num;
				statementAttribute = parentHandle.GetStatementAttribute(attribute, out this.handle, out num);
			}
			finally
			{
				bool flag3 = IntPtr.Zero != this.handle;
				if (flag2)
				{
					if (flag3)
					{
						this.parentHandle = parentHandle;
					}
					else
					{
						this.parentHandle.DangerousRelease();
					}
				}
				if (flag && !flag3)
				{
					libraryHandle.DangerousRelease();
				}
			}
			if (IntPtr.Zero == this.handle)
			{
				throw new InvalidOperationException("Failed to get description handle: " + statementAttribute.ToString());
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x060034FC RID: 13564 RVA: 0x000AAD6C File Offset: 0x000A8F6C
		public Odbc32.SQL_HANDLE HandleType
		{
			get
			{
				return this.handleType;
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x060034FD RID: 13565 RVA: 0x000A91E9 File Offset: 0x000A73E9
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000AAD74 File Offset: 0x000A8F74
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				Odbc32.SQL_HANDLE sql_HANDLE = this.HandleType;
				if (sql_HANDLE - Odbc32.SQL_HANDLE.ENV > 2)
				{
					if (sql_HANDLE != Odbc32.SQL_HANDLE.DESC)
					{
					}
				}
				else
				{
					this.odbcInterop.SQLFreeHandle(sql_HANDLE, handle);
				}
			}
			OdbcHandle odbcHandle = this.parentHandle;
			this.parentHandle = null;
			if (odbcHandle != null)
			{
				odbcHandle.DangerousRelease();
			}
			SafeHandle safeHandle = this.odbcLibraryHandle;
			this.odbcLibraryHandle = null;
			if (safeHandle != null)
			{
				safeHandle.DangerousRelease();
			}
			return true;
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000AADF0 File Offset: 0x000A8FF0
		public Odbc32.RetCode GetDiagnosticField(int number, Odbc32.SQL_DIAG diagIdentifier, StringBuilder resultValue)
		{
			resultValue.Length = 0;
			short num;
			return this.odbcInterop.SQLGetDiagFieldW(this.HandleType, this, (short)number, (short)diagIdentifier, resultValue, checked((short)(2 * resultValue.Capacity)), out num);
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000AAE28 File Offset: 0x000A9028
		public unsafe Odbc32.RetCode GetDiagnosticField(int number, Odbc32.SQL_DIAG diagIdentifier, out long returnValue)
		{
			long num;
			short num2;
			Odbc32.RetCode retCode = this.odbcInterop.SQLGetDiagFieldW(this.HandleType, this, (short)number, (short)diagIdentifier, (IntPtr)((void*)(&num)), 0, out num2);
			returnValue = num;
			return retCode;
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000AAE58 File Offset: 0x000A9058
		public Odbc32.RetCode GetDiagnosticRecord(int number, StringBuilder messageBuffer, StringBuilder sqlStateBuffer, out OdbcError error)
		{
			int num;
			short num2;
			Odbc32.RetCode retCode = this.odbcInterop.SQLGetDiagRecW(this.HandleType, this, (short)number, sqlStateBuffer, out num, messageBuffer, checked((short)messageBuffer.Capacity), out num2);
			if (retCode == Odbc32.RetCode.SUCCESS_WITH_INFO && num2 > 0)
			{
				messageBuffer.Capacity += (int)(num2 + 1);
				retCode = this.odbcInterop.SQLGetDiagRecW(this.HandleType, this, (short)number, sqlStateBuffer, out num, messageBuffer, checked((short)messageBuffer.Capacity), out num2);
			}
			if (retCode == Odbc32.RetCode.SUCCESS || retCode == Odbc32.RetCode.SUCCESS_WITH_INFO)
			{
				string text = sqlStateBuffer.ToString();
				string text2 = messageBuffer.ToString();
				if (string.Equals(text, "IM014", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(text2) && text2.Contains("[Microsoft][ODBC Driver Manager]"))
				{
					text2 = ((IntPtr.Size == 8) ? Strings.OdbcMismatchArchitectureInX64App : Strings.OdbcMismatchArchitectureInX86App);
				}
				error = new OdbcError(text2, text, num);
			}
			else
			{
				error = null;
			}
			return retCode;
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000AAF2A File Offset: 0x000A912A
		public override string ToString()
		{
			return this.handle.ToString();
		}

		// Token: 0x04001B71 RID: 7025
		private readonly Odbc32.SQL_HANDLE handleType;

		// Token: 0x04001B72 RID: 7026
		protected readonly IOdbcInterop odbcInterop;

		// Token: 0x04001B73 RID: 7027
		private SafeHandle odbcLibraryHandle;

		// Token: 0x04001B74 RID: 7028
		private OdbcHandle parentHandle;
	}
}
