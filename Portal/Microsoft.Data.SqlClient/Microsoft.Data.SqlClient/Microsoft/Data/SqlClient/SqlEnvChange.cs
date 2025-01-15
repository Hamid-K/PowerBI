using System;
using System.Buffers;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200007C RID: 124
	internal sealed class SqlEnvChange
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001FC94 File Offset: 0x0001DE94
		internal void Clear()
		{
			this._type = 0;
			this._oldLength = 0;
			this._newLength = 0;
			this._length = 0;
			this._newValue = null;
			this._oldValue = null;
			if (this._newBinValue != null)
			{
				Array.Clear(this._newBinValue, 0, this._newBinValue.Length);
				if (this._newBinRented)
				{
					ArrayPool<byte>.Shared.Return(this._newBinValue, false);
				}
				this._newBinValue = null;
			}
			if (this._oldBinValue != null)
			{
				Array.Clear(this._oldBinValue, 0, this._oldBinValue.Length);
				if (this._oldBinRented)
				{
					ArrayPool<byte>.Shared.Return(this._oldBinValue, false);
				}
				this._oldBinValue = null;
			}
			this._newBinRented = false;
			this._oldBinRented = false;
			this._newLongValue = 0L;
			this._oldLongValue = 0L;
			this._newCollation = null;
			this._oldCollation = null;
			this._newRoutingInfo = null;
			this._next = null;
		}

		// Token: 0x04000287 RID: 647
		internal byte _type;

		// Token: 0x04000288 RID: 648
		internal byte _oldLength;

		// Token: 0x04000289 RID: 649
		internal int _newLength;

		// Token: 0x0400028A RID: 650
		internal int _length;

		// Token: 0x0400028B RID: 651
		internal string _newValue;

		// Token: 0x0400028C RID: 652
		internal string _oldValue;

		// Token: 0x0400028D RID: 653
		internal byte[] _newBinValue;

		// Token: 0x0400028E RID: 654
		internal byte[] _oldBinValue;

		// Token: 0x0400028F RID: 655
		internal long _newLongValue;

		// Token: 0x04000290 RID: 656
		internal long _oldLongValue;

		// Token: 0x04000291 RID: 657
		internal SqlCollation _newCollation;

		// Token: 0x04000292 RID: 658
		internal SqlCollation _oldCollation;

		// Token: 0x04000293 RID: 659
		internal RoutingInfo _newRoutingInfo;

		// Token: 0x04000294 RID: 660
		internal bool _newBinRented;

		// Token: 0x04000295 RID: 661
		internal bool _oldBinRented;

		// Token: 0x04000296 RID: 662
		internal SqlEnvChange _next;
	}
}
