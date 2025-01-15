using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C2 RID: 1218
	internal struct FacetValueContainer<T>
	{
		// Token: 0x17000BCC RID: 3020
		// (set) Token: 0x06003C33 RID: 15411 RVA: 0x000C77A5 File Offset: 0x000C59A5
		internal T Value
		{
			set
			{
				this._isUnbounded = false;
				this._hasValue = true;
				this._value = value;
			}
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x000C77BC File Offset: 0x000C59BC
		private void SetUnbounded()
		{
			this._isUnbounded = true;
			this._hasValue = true;
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x000C77CC File Offset: 0x000C59CC
		public static implicit operator FacetValueContainer<T>(EdmConstants.Unbounded unbounded)
		{
			FacetValueContainer<T> facetValueContainer = default(FacetValueContainer<T>);
			facetValueContainer.SetUnbounded();
			return facetValueContainer;
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x000C77EC File Offset: 0x000C59EC
		public static implicit operator FacetValueContainer<T>(T value)
		{
			return new FacetValueContainer<T>
			{
				Value = value
			};
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x000C780A File Offset: 0x000C5A0A
		internal object GetValueAsObject()
		{
			if (this._isUnbounded)
			{
				return EdmConstants.UnboundedValue;
			}
			return this._value;
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06003C38 RID: 15416 RVA: 0x000C7825 File Offset: 0x000C5A25
		internal bool HasValue
		{
			get
			{
				return this._hasValue;
			}
		}

		// Token: 0x040014B8 RID: 5304
		private T _value;

		// Token: 0x040014B9 RID: 5305
		private bool _hasValue;

		// Token: 0x040014BA RID: 5306
		private bool _isUnbounded;
	}
}
