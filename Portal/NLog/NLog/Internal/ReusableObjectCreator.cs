using System;

namespace NLog.Internal
{
	// Token: 0x0200013B RID: 315
	internal class ReusableObjectCreator<T> where T : class
	{
		// Token: 0x06000F8A RID: 3978 RVA: 0x00027998 File Offset: 0x00025B98
		protected ReusableObjectCreator(T reusableObject, Action<T> clearObject)
		{
			this._reusableObject = reusableObject;
			this._clearObject = clearObject;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x000279AE File Offset: 0x00025BAE
		public ReusableObjectCreator<T>.LockOject Allocate()
		{
			return new ReusableObjectCreator<T>.LockOject(this);
		}

		// Token: 0x04000424 RID: 1060
		protected T _reusableObject;

		// Token: 0x04000425 RID: 1061
		private readonly Action<T> _clearObject;

		// Token: 0x04000426 RID: 1062
		public readonly ReusableObjectCreator<T>.LockOject None;

		// Token: 0x0200027B RID: 635
		public struct LockOject : IDisposable
		{
			// Token: 0x0600165E RID: 5726 RVA: 0x0003A9E1 File Offset: 0x00038BE1
			public LockOject(ReusableObjectCreator<T> owner)
			{
				this.Result = owner._reusableObject;
				owner._reusableObject = default(T);
				this._owner = owner;
			}

			// Token: 0x0600165F RID: 5727 RVA: 0x0003AA02 File Offset: 0x00038C02
			public void Dispose()
			{
				if (this.Result != null)
				{
					this._owner._clearObject(this.Result);
					this._owner._reusableObject = this.Result;
				}
			}

			// Token: 0x040006C4 RID: 1732
			public readonly T Result;

			// Token: 0x040006C5 RID: 1733
			private readonly ReusableObjectCreator<T> _owner;
		}
	}
}
