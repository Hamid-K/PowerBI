using System;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000089 RID: 137
	internal sealed class FlagSequenceHandler
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00009283 File Offset: 0x00007483
		public void SetCurrentValue(long value)
		{
			this._currentValue = value;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000928C File Offset: 0x0000748C
		public bool IsFlagSet(int position)
		{
			if (position >= 53)
			{
				return false;
			}
			long num = 1L << position;
			return (this._currentValue & num) != 0L;
		}

		// Token: 0x040001C0 RID: 448
		private long _currentValue;
	}
}
