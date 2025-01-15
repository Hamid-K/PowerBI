using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000078 RID: 120
	internal class SqlEnclaveSession
	{
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0001DE2C File Offset: 0x0001C02C
		internal long SessionId { get; }

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001DE34 File Offset: 0x0001C034
		internal byte[] GetSessionKey()
		{
			return this.Clone(this._sessionKey);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001DE44 File Offset: 0x0001C044
		private byte[] Clone(byte[] arrayToClone)
		{
			byte[] array = new byte[arrayToClone.Length];
			for (int i = 0; i < arrayToClone.Length; i++)
			{
				array[i] = arrayToClone[i];
			}
			return array;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001DE6F File Offset: 0x0001C06F
		internal SqlEnclaveSession(byte[] sessionKey, long sessionId)
		{
			if (sessionKey == null)
			{
				throw SQL.NullArgumentInConstructorInternal(SqlEnclaveSession._sessionKeyName, SqlEnclaveSession._className);
			}
			if (sessionKey.Length == 0)
			{
				throw SQL.EmptyArgumentInConstructorInternal(SqlEnclaveSession._sessionKeyName, SqlEnclaveSession._className);
			}
			this._sessionKey = sessionKey;
			this.SessionId = sessionId;
		}

		// Token: 0x04000240 RID: 576
		private static readonly string _sessionKeyName = "SessionKey";

		// Token: 0x04000241 RID: 577
		private static readonly string _className = "EnclaveSession";

		// Token: 0x04000242 RID: 578
		private readonly byte[] _sessionKey;
	}
}
