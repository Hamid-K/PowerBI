using System;

namespace Microsoft.InfoNav.Data.Library
{
	// Token: 0x02000071 RID: 113
	public abstract class ConceptualBuilderBase<T> where T : class, IBuiltConceptualType
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00006FC2 File Offset: 0x000051C2
		protected ConceptualBuilderBase(T activeObject)
		{
			this._activeObject = activeObject;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00006FD1 File Offset: 0x000051D1
		protected T ActiveObject
		{
			get
			{
				return this._activeObject;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00006FD9 File Offset: 0x000051D9
		protected T CompleteBaseInitialization()
		{
			if (this.ActiveObject == null)
			{
				throw new InvalidOperationException("ActiveObject has already completed initialization.");
			}
			T activeObject = this.ActiveObject;
			this._activeObject = default(T);
			return activeObject;
		}

		// Token: 0x0400017B RID: 379
		private T _activeObject;
	}
}
