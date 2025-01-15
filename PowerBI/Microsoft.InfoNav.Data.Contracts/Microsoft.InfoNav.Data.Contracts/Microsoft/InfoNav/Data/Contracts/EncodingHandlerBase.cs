using System;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200008A RID: 138
	internal abstract class EncodingHandlerBase
	{
		// Token: 0x0600032F RID: 815 RVA: 0x000092BC File Offset: 0x000074BC
		protected EncodingHandlerBase()
		{
			this._flagsHandler = new FlagSequenceHandler();
		}

		// Token: 0x06000330 RID: 816
		protected abstract object GetValue(int position);

		// Token: 0x06000331 RID: 817 RVA: 0x000092CF File Offset: 0x000074CF
		public void SetCurrentValue(long flagValue)
		{
			this._flagsHandler.SetCurrentValue(flagValue);
			this._isDisabled = false;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000092E4 File Offset: 0x000074E4
		public void Disable()
		{
			this._isDisabled = true;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000092ED File Offset: 0x000074ED
		public bool TryHandleValue(int position, out object parsedValue)
		{
			parsedValue = null;
			if (this._isDisabled)
			{
				return false;
			}
			if (this._flagsHandler.IsFlagSet(position))
			{
				parsedValue = this.GetValue(position);
				return true;
			}
			return false;
		}

		// Token: 0x040001C1 RID: 449
		private FlagSequenceHandler _flagsHandler;

		// Token: 0x040001C2 RID: 450
		private bool _isDisabled;
	}
}
