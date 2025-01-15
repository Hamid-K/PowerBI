using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000091 RID: 145
	internal struct FacetValueContainer<T>
	{
		// Token: 0x170003C2 RID: 962
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00018B17 File Offset: 0x00016D17
		internal T Value
		{
			set
			{
				this._isUnbounded = false;
				this._hasValue = true;
				this._value = value;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00018B2E File Offset: 0x00016D2E
		private void SetUnbounded()
		{
			this._isUnbounded = true;
			this._hasValue = true;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00018B40 File Offset: 0x00016D40
		public static implicit operator FacetValueContainer<T>(EdmConstants.Unbounded unbounded)
		{
			FacetValueContainer<T> facetValueContainer = default(FacetValueContainer<T>);
			facetValueContainer.SetUnbounded();
			return facetValueContainer;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00018B60 File Offset: 0x00016D60
		public static implicit operator FacetValueContainer<T>(T value)
		{
			return new FacetValueContainer<T>
			{
				Value = value
			};
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00018B7E File Offset: 0x00016D7E
		internal object GetValueAsObject()
		{
			if (this._isUnbounded)
			{
				return EdmConstants.UnboundedValue;
			}
			return this._value;
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00018B99 File Offset: 0x00016D99
		internal bool HasValue
		{
			get
			{
				return this._hasValue;
			}
		}

		// Token: 0x04000840 RID: 2112
		private T _value;

		// Token: 0x04000841 RID: 2113
		private bool _hasValue;

		// Token: 0x04000842 RID: 2114
		private bool _isUnbounded;
	}
}
