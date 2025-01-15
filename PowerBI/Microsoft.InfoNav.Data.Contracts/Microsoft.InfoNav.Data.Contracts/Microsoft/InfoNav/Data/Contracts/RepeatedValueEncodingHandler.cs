using System;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200008C RID: 140
	internal class RepeatedValueEncodingHandler : EncodingHandlerBase
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00009321 File Offset: 0x00007521
		internal RepeatedValueEncodingHandler()
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009329 File Offset: 0x00007529
		public void SetLastSeenValues(long flagValue, object[] lastSeenValues)
		{
			this._lastSeenValues = lastSeenValues;
			base.SetCurrentValue(flagValue);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00009339 File Offset: 0x00007539
		protected override object GetValue(int position)
		{
			return this._lastSeenValues[position];
		}

		// Token: 0x040001C3 RID: 451
		private object[] _lastSeenValues;
	}
}
