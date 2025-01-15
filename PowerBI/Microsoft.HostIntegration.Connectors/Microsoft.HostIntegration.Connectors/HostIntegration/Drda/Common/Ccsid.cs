using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200079A RID: 1946
	public class Ccsid
	{
		// Token: 0x06003EC1 RID: 16065 RVA: 0x000D2600 File Offset: 0x000D0800
		public override string ToString()
		{
			return string.Format("Ccsid[ccsidsbc={0};ccsidmbc={1};ccsiddbc={2};ccsidxml={3}]", new object[] { this._ccsidsbc, this._ccsidmbc, this._ccsiddbc, this._ccsidxml });
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x000D2655 File Offset: 0x000D0855
		public Ccsid Clone()
		{
			return new Ccsid
			{
				_ccsidsbc = this._ccsidsbc,
				_ccsidmbc = this._ccsidmbc,
				_ccsiddbc = this._ccsiddbc
			};
		}

		// Token: 0x04002549 RID: 9545
		public int _ccsidsbc = 1208;

		// Token: 0x0400254A RID: 9546
		public int _ccsidmbc = 1208;

		// Token: 0x0400254B RID: 9547
		public int _ccsiddbc = 1200;

		// Token: 0x0400254C RID: 9548
		public int _ccsidxml = 1208;
	}
}
