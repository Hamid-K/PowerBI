using System;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000296 RID: 662
	internal class InterceptionContextMutableData<TResult> : InterceptionContextMutableData
	{
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x0005CB01 File Offset: 0x0005AD01
		// (set) Token: 0x06002103 RID: 8451 RVA: 0x0005CB09 File Offset: 0x0005AD09
		public TResult OriginalResult { get; set; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x0005CB12 File Offset: 0x0005AD12
		// (set) Token: 0x06002105 RID: 8453 RVA: 0x0005CB1A File Offset: 0x0005AD1A
		public TResult Result
		{
			get
			{
				return this._result;
			}
			set
			{
				if (!base.HasExecuted)
				{
					base.SuppressExecution();
				}
				this._result = value;
			}
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x0005CB31 File Offset: 0x0005AD31
		public void SetExecuted(TResult result)
		{
			base.HasExecuted = true;
			this.OriginalResult = result;
			this.Result = result;
		}

		// Token: 0x04000B91 RID: 2961
		private TResult _result;
	}
}
