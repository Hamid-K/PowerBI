using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001A4 RID: 420
	internal class VelocityUri : Uri
	{
		// Token: 0x06000DC6 RID: 3526 RVA: 0x0002ECE2 File Offset: 0x0002CEE2
		public VelocityUri(string str)
			: base(str)
		{
			this._hashCode = base.GetHashCode();
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0002ECF7 File Offset: 0x0002CEF7
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x0400097D RID: 2429
		private int _hashCode;
	}
}
