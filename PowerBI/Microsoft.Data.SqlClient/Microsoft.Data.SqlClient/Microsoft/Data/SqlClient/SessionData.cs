using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000EF RID: 239
	internal class SessionData
	{
		// Token: 0x0600126D RID: 4717 RVA: 0x0004930C File Offset: 0x0004750C
		public SessionData(SessionData recoveryData)
		{
			this._initialDatabase = recoveryData._initialDatabase;
			this._initialCollation = recoveryData._initialCollation;
			this._initialLanguage = recoveryData._initialLanguage;
			this._resolvedAliases = recoveryData._resolvedAliases;
			for (int i = 0; i < 256; i++)
			{
				if (recoveryData._initialState[i] != null)
				{
					this._initialState[i] = (byte[])recoveryData._initialState[i].Clone();
				}
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000493A3 File Offset: 0x000475A3
		public SessionData()
		{
			this._resolvedAliases = new Dictionary<string, Tuple<string, string>>(2);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000493D7 File Offset: 0x000475D7
		public void Reset()
		{
			this._database = null;
			this._collation = null;
			this._language = null;
			if (this._deltaDirty)
			{
				this._delta = new SessionStateRecord[256];
				this._deltaDirty = false;
			}
			this._unrecoverableStatesCount = 0;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00049414 File Offset: 0x00047614
		[Conditional("DEBUG")]
		public void AssertUnrecoverableStateCountIsCorrect()
		{
			byte b = 0;
			foreach (SessionStateRecord sessionStateRecord in this._delta)
			{
				if (sessionStateRecord != null && !sessionStateRecord._recoverable)
				{
					b += 1;
				}
			}
		}

		// Token: 0x0400078A RID: 1930
		internal const int _maxNumberOfSessionStates = 256;

		// Token: 0x0400078B RID: 1931
		internal uint _tdsVersion;

		// Token: 0x0400078C RID: 1932
		internal bool _encrypted;

		// Token: 0x0400078D RID: 1933
		internal string _database;

		// Token: 0x0400078E RID: 1934
		internal SqlCollation _collation;

		// Token: 0x0400078F RID: 1935
		internal string _language;

		// Token: 0x04000790 RID: 1936
		internal string _initialDatabase;

		// Token: 0x04000791 RID: 1937
		internal SqlCollation _initialCollation;

		// Token: 0x04000792 RID: 1938
		internal string _initialLanguage;

		// Token: 0x04000793 RID: 1939
		internal byte _unrecoverableStatesCount;

		// Token: 0x04000794 RID: 1940
		internal Dictionary<string, Tuple<string, string>> _resolvedAliases;

		// Token: 0x04000795 RID: 1941
		internal SessionStateRecord[] _delta = new SessionStateRecord[256];

		// Token: 0x04000796 RID: 1942
		internal bool _deltaDirty;

		// Token: 0x04000797 RID: 1943
		internal byte[][] _initialState = new byte[256][];
	}
}
