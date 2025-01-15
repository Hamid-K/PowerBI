using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000177 RID: 375
	public abstract class MarshalSafeWarmedUpObjectsBase<UnsafeType> : MarshalByRefObject, ISafeWarmedUpObjectMarshaler, IDisposable
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x00022310 File Offset: 0x00020510
		public bool TryInitializeUnSafeObject(object creationData)
		{
			this.m_unsafeObject = this.TryCreateObject(creationData);
			return this.m_unsafeObject != null;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00009B3B File Offset: 0x00007D3B
		public virtual void WarmUp()
		{
		}

		// Token: 0x060009D7 RID: 2519
		public abstract SafeWarmedUpObjectManager.ExecuteStatus ExecuteFunction(int operationId, out string result, params string[] args);

		// Token: 0x060009D8 RID: 2520 RVA: 0x00022330 File Offset: 0x00020530
		public void Dispose()
		{
			if (this.m_unsafeObject != null)
			{
				UnsafeType unsafeObject = this.m_unsafeObject;
				this.m_unsafeObject = default(UnsafeType);
				this.Dispose(unsafeObject);
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00005EB7 File Offset: 0x000040B7
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060009DA RID: 2522
		protected abstract void Dispose(UnsafeType obj);

		// Token: 0x060009DB RID: 2523
		protected abstract UnsafeType TryCreateObject(object creationData);

		// Token: 0x040003CC RID: 972
		protected UnsafeType m_unsafeObject;
	}
}
