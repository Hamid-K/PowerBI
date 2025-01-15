using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000827 RID: 2087
	public struct ObjectInfo
	{
		// Token: 0x06004250 RID: 16976 RVA: 0x000DE240 File Offset: 0x000DC440
		public ObjectInfo(CodePoint cp, long len, int correlationId)
		{
			this._codepoint = cp;
			this._length = len;
			this._correlationId = correlationId;
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x000DE257 File Offset: 0x000DC457
		public static ObjectInfo InvalidInstance
		{
			get
			{
				return ObjectInfo._invalidInstance;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06004252 RID: 16978 RVA: 0x000DE25E File Offset: 0x000DC45E
		public int CorrelationId
		{
			get
			{
				return this._correlationId;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06004253 RID: 16979 RVA: 0x000DE266 File Offset: 0x000DC466
		public CodePoint Codepoint
		{
			get
			{
				return this._codepoint;
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06004254 RID: 16980 RVA: 0x000DE26E File Offset: 0x000DC46E
		public long Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x04002E71 RID: 11889
		private CodePoint _codepoint;

		// Token: 0x04002E72 RID: 11890
		private long _length;

		// Token: 0x04002E73 RID: 11891
		private int _correlationId;

		// Token: 0x04002E74 RID: 11892
		private static ObjectInfo _invalidInstance = new ObjectInfo(CodePoint.UNKNOWN, -1L, -1);
	}
}
