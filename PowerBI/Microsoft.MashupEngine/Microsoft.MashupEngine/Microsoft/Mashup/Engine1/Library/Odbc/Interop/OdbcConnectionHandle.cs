using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x020006DD RID: 1757
	internal sealed class OdbcConnectionHandle : OdbcHandle
	{
		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x000AA1A4 File Offset: 0x000A83A4
		public bool IsV3Driver
		{
			get
			{
				if (this.driverVersion == null)
				{
					this.GetDriverVersion();
				}
				int? num = this.driverVersion;
				int num2 = 3;
				return (num.GetValueOrDefault() >= num2) & (num != null);
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000AA1E4 File Offset: 0x000A83E4
		public bool IsBigIntSupportedByDriver
		{
			get
			{
				string infoStringUnhandled = this.GetInfoStringUnhandled(Odbc32.SQL_INFO.SQL_DRIVER_NAME);
				return infoStringUnhandled == null || !string.Equals(infoStringUnhandled, "SQORA32.DLL", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000AA210 File Offset: 0x000A8410
		public unsafe OdbcConnectionHandle(IOdbcInterop odbcInterop, OdbcEnvironmentHandle environmentHandle, string connectionString, int? connectionTimeout, Dictionary<int, object> connectionAttributes)
			: base(odbcInterop, Odbc32.SQL_HANDLE.DBC, environmentHandle)
		{
			Odbc32.RetCode retCode2;
			if (connectionTimeout != null)
			{
				bool flag = OdbcUtils.HandleErrorNoThrow(this, this.SetConnectionAttribute2(Odbc32.SQL_ATTR.LOGIN_TIMEOUT, (IntPtr)connectionTimeout.Value, -5)) != null;
				Odbc32.RetCode retCode = this.SetConnectionAttribute2(Odbc32.SQL_ATTR.CONNECTION_TIMEOUT, (IntPtr)connectionTimeout.Value, -5);
				if (flag)
				{
					OdbcUtils.HandleError(this, retCode);
				}
			}
			else
			{
				retCode2 = this.SetConnectionAttribute2(Odbc32.SQL_ATTR.LOGIN_TIMEOUT, (IntPtr)15, -5);
			}
			if (connectionAttributes != null)
			{
				foreach (KeyValuePair<int, object> keyValuePair in connectionAttributes)
				{
					if (keyValuePair.Value is int)
					{
						OdbcUtils.HandleError(this, this.SetConnectionAttribute2((Odbc32.SQL_ATTR)keyValuePair.Key, (IntPtr)((int)keyValuePair.Value), -5));
					}
					else
					{
						if (!(keyValuePair.Value is string))
						{
							if (keyValuePair.Value is byte[])
							{
								byte[] array = (byte[])keyValuePair.Value;
								try
								{
									byte[] array2;
									byte* ptr;
									if ((array2 = array) == null || array2.Length == 0)
									{
										ptr = null;
									}
									else
									{
										ptr = &array2[0];
									}
									OdbcUtils.HandleError(this, this.SetConnectionAttribute2((Odbc32.SQL_ATTR)keyValuePair.Key, (IntPtr)((void*)ptr), Odbc32.SQL_LEN_BINARY_ATTR(array.Length)));
									continue;
								}
								finally
								{
									byte[] array2 = null;
								}
							}
							throw new InvalidOperationException();
						}
						OdbcUtils.HandleError(this, this.SetConnectionAttribute3((Odbc32.SQL_ATTR)keyValuePair.Key, (string)keyValuePair.Value, -3));
					}
				}
			}
			retCode2 = this.Connect(connectionString);
			OdbcUtils.HandleError(this, retCode2);
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000AA3BC File Offset: 0x000A85BC
		protected override bool ReleaseHandle()
		{
			this.Disconnect();
			return base.ReleaseHandle();
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000AA3CA File Offset: 0x000A85CA
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Disconnect();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000AA3DC File Offset: 0x000A85DC
		public Odbc32.RetCode GetConnectionAttribute(Odbc32.SQL_ATTR attribute, byte[] buffer, out int cbActual)
		{
			return this.odbcInterop.SQLGetConnectAttrW(this, attribute, buffer, buffer.Length, out cbActual);
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000AA3F0 File Offset: 0x000A85F0
		public Odbc32.RetCode GetConnectionAttribute(Odbc32.SQL_ATTR attribute, out string value)
		{
			int num;
			Odbc32.RetCode retCode = this.odbcInterop.SQLGetConnectAttrW(this, attribute, null, 0, out num);
			if (OdbcUtils.IsSuccess(retCode) && num >= 0)
			{
				StringBuilder stringBuilder = new StringBuilder((num + 2) / 2);
				retCode = this.odbcInterop.SQLGetConnectAttrW(this, attribute, stringBuilder, num + 2, out num);
				if (OdbcUtils.IsSuccess(retCode))
				{
					value = stringBuilder.ToString();
					return retCode;
				}
			}
			value = null;
			return retCode;
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000AA450 File Offset: 0x000A8650
		public Odbc32.RetCode GetFunctions(Odbc32.SQL_API fFunction, out short fExists)
		{
			return this.odbcInterop.SQLGetFunctions(this, fFunction, out fExists);
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000AA460 File Offset: 0x000A8660
		public bool TryGetFunction(Odbc32.SQL_API fFunction)
		{
			short num;
			return this.GetFunctions(fFunction, out num) == Odbc32.RetCode.SUCCESS && num == 1;
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000AA480 File Offset: 0x000A8680
		public unsafe Odbc32.RetCode GetInfo2(Odbc32.SQL_INFO info, byte[] buffer, out short cbActual)
		{
			short num = 0;
			IntPtr intPtr = (IntPtr)((void*)(&num));
			Odbc32.RetCode retCode = this.odbcInterop.SQLGetInfoW(this, info, buffer, checked((short)buffer.Length), intPtr);
			cbActual = num;
			return retCode;
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000AA4B0 File Offset: 0x000A86B0
		public Odbc32.RetCode GetInfo1(Odbc32.SQL_INFO info, byte[] buffer)
		{
			short num;
			return this.GetInfo2(info, buffer, out num);
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000AA4C7 File Offset: 0x000A86C7
		public Odbc32.RetCode SetConnectionAttribute2(Odbc32.SQL_ATTR attribute, IntPtr value, int length)
		{
			return this.odbcInterop.SQLSetConnectAttrW(this, attribute, value, length);
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000AA4D8 File Offset: 0x000A86D8
		public Odbc32.RetCode SetConnectionAttribute3(Odbc32.SQL_ATTR attribute, string buffer, int length)
		{
			return this.odbcInterop.SQLSetConnectAttrW(this, attribute, buffer, length);
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000AA4EC File Offset: 0x000A86EC
		public Odbc32.RetCode GetInfoInt16Unhandled(Odbc32.SQL_INFO info, out short resultValue)
		{
			byte[] array = new byte[2];
			Odbc32.RetCode info2 = this.GetInfo1(info, array);
			resultValue = BitConverter.ToInt16(array, 0);
			return info2;
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000AA514 File Offset: 0x000A8714
		public Odbc32.RetCode GetInfoInt32Unhandled(Odbc32.SQL_INFO info, out int resultValue)
		{
			byte[] array = new byte[4];
			Odbc32.RetCode info2 = this.GetInfo1(info, array);
			resultValue = BitConverter.ToInt32(array, 0);
			return info2;
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000AA53C File Offset: 0x000A873C
		public int GetInfoInt32Unhandled(Odbc32.SQL_INFO infotype)
		{
			byte[] array = new byte[4];
			this.GetInfo1(infotype, array);
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000AA560 File Offset: 0x000A8760
		public short GetInfoInt16Unhandled(Odbc32.SQL_INFO infotype)
		{
			byte[] array = new byte[4];
			this.GetInfo1(infotype, array);
			return BitConverter.ToInt16(array, 0);
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000AA584 File Offset: 0x000A8784
		public string GetInfoStringUnhandled(Odbc32.SQL_INFO info)
		{
			return this.GetInfoStringUnhandled(info, false);
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000AA58E File Offset: 0x000A878E
		public string GetInfoString(Odbc32.SQL_INFO info)
		{
			return this.GetInfoStringUnhandled(info, true);
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000AA598 File Offset: 0x000A8798
		private void GetDriverVersion()
		{
			if (this.driverVersion == null)
			{
				string infoStringUnhandled = this.GetInfoStringUnhandled(Odbc32.SQL_INFO.SQL_DRIVER_ODBC_VER);
				int num;
				if (infoStringUnhandled != null && infoStringUnhandled.Length >= 2 && int.TryParse(infoStringUnhandled.Substring(0, 2), out num))
				{
					this.driverVersion = new int?(num);
				}
			}
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x000AA5E4 File Offset: 0x000A87E4
		private string GetInfoStringUnhandled(Odbc32.SQL_INFO info, bool handleError)
		{
			string text = null;
			short num = 0;
			byte[] array = new byte[100];
			if (this != null)
			{
				Odbc32.RetCode retCode = this.GetInfo2(info, array, out num);
				if (array.Length < (int)(num - 2))
				{
					array = new byte[(int)(num + 2)];
					retCode = this.GetInfo2(info, array, out num);
				}
				if (retCode == Odbc32.RetCode.SUCCESS || retCode == Odbc32.RetCode.SUCCESS_WITH_INFO)
				{
					text = Encoding.Unicode.GetString(array, 0, Math.Min((int)num, array.Length));
				}
				else if (handleError)
				{
					OdbcUtils.HandleError(this, retCode);
				}
			}
			else if (handleError)
			{
				text = "";
			}
			return text;
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000AA664 File Offset: 0x000A8864
		private Odbc32.RetCode Connect(string connectionString)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			Odbc32.RetCode retCode;
			try
			{
			}
			finally
			{
				short num;
				retCode = this.odbcInterop.SQLDriverConnectW(this, IntPtr.Zero, connectionString, -3, IntPtr.Zero, 0, out num, 0);
				if (retCode <= Odbc32.RetCode.SUCCESS_WITH_INFO)
				{
					this.handleState = OdbcConnectionHandle.HandleState.Connected;
				}
			}
			return retCode;
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000AA6B4 File Offset: 0x000A88B4
		private void Disconnect()
		{
			if (OdbcConnectionHandle.HandleState.Connected == this.handleState || OdbcConnectionHandle.HandleState.TransactionInProgress == this.handleState)
			{
				Odbc32.RetCode retCode = this.odbcInterop.SQLDisconnect(this.handle);
				if (retCode <= Odbc32.RetCode.SUCCESS_WITH_INFO)
				{
					this.handleState = OdbcConnectionHandle.HandleState.Allocated;
				}
			}
		}

		// Token: 0x04001B61 RID: 7009
		private OdbcConnectionHandle.HandleState handleState;

		// Token: 0x04001B62 RID: 7010
		private int? driverVersion;

		// Token: 0x04001B63 RID: 7011
		private const string OracleOdbcDriverName = "SQORA32.DLL";

		// Token: 0x020006DE RID: 1758
		private enum HandleState
		{
			// Token: 0x04001B65 RID: 7013
			Allocated,
			// Token: 0x04001B66 RID: 7014
			Connected,
			// Token: 0x04001B67 RID: 7015
			Transacted,
			// Token: 0x04001B68 RID: 7016
			TransactionInProgress
		}
	}
}
