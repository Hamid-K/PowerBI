using System;
using System.Runtime.InteropServices;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;
using MsolapWrapper;

namespace Microsoft.DataShaping.RawDataProcessing
{
	// Token: 0x0200000A RID: 10
	internal class RawConnectionExtractor : IRawConnectionExtractor
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002438 File Offset: 0x00000638
		public unsafe bool TryExtractRawConnection(IDbConnection dbConnection, out IDBCreateSession rawConnection)
		{
			IUnderlyingConnectionProvider underlyingConnectionProvider = dbConnection as IUnderlyingConnectionProvider;
			if (underlyingConnectionProvider == null)
			{
				rawConnection = null;
				return false;
			}
			Connection connection = underlyingConnectionProvider.UnderlyingConnection as Connection;
			if (connection == null)
			{
				rawConnection = null;
				return false;
			}
			rawConnection = (IDBCreateSession)Marshal.GetObjectForIUnknown((IntPtr)((void*)connection.GetDBInitialize()));
			return true;
		}
	}
}
