using System;

namespace System.Web.Http
{
	// Token: 0x0200000E RID: 14
	internal class EfficientTypePropertyKey<T1, T2> : Tuple<T1, T2>
	{
		// Token: 0x06000064 RID: 100 RVA: 0x000034D2 File Offset: 0x000016D2
		public EfficientTypePropertyKey(T1 item1, T2 item2)
			: base(item1, item2)
		{
			this._hashCode = base.GetHashCode();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000034E8 File Offset: 0x000016E8
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x0400000F RID: 15
		private int _hashCode;
	}
}
