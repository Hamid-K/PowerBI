using System;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x02000502 RID: 1282
	[Serializable]
	public struct CVTEncodingValue
	{
		// Token: 0x04001B47 RID: 6983
		public byte nCvtType;

		// Token: 0x04001B48 RID: 6984
		public byte nScale;

		// Token: 0x04001B49 RID: 6985
		public byte nPrecision;

		// Token: 0x04001B4A RID: 6986
		public byte nPad;

		// Token: 0x04001B4B RID: 6987
		public byte nTRE;

		// Token: 0x04001B4C RID: 6988
		public byte nSign;

		// Token: 0x04001B4D RID: 6989
		public byte nTrailing;

		// Token: 0x04001B4E RID: 6990
		public byte nOverpunch;

		// Token: 0x04001B4F RID: 6991
		public byte nSOSI;

		// Token: 0x04001B50 RID: 6992
		public byte nAsIs;
	}
}
