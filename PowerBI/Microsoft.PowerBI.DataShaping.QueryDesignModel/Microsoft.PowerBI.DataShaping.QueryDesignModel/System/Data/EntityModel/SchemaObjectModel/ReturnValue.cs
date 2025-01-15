using System;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000042 RID: 66
	internal sealed class ReturnValue<T>
	{
		// Token: 0x06000788 RID: 1928 RVA: 0x0000ED71 File Offset: 0x0000CF71
		internal ReturnValue()
		{
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0000ED79 File Offset: 0x0000CF79
		internal bool Succeeded
		{
			get
			{
				return this._succeeded;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0000ED81 File Offset: 0x0000CF81
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x0000ED89 File Offset: 0x0000CF89
		internal T Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				this._succeeded = true;
			}
		}

		// Token: 0x0400068A RID: 1674
		private bool _succeeded;

		// Token: 0x0400068B RID: 1675
		private T _value;
	}
}
