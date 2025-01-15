using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000502 RID: 1282
	internal class ValidationErrorEventArgs : EventArgs
	{
		// Token: 0x06003F85 RID: 16261 RVA: 0x000D3835 File Offset: 0x000D1A35
		public ValidationErrorEventArgs(EdmItemError validationError)
		{
			this._validationError = validationError;
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x000D3844 File Offset: 0x000D1A44
		public EdmItemError ValidationError
		{
			get
			{
				return this._validationError;
			}
		}

		// Token: 0x04001598 RID: 5528
		private readonly EdmItemError _validationError;
	}
}
