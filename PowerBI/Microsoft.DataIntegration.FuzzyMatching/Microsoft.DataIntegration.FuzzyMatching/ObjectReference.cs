using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000D7 RID: 215
	public class ObjectReference
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x00028AB0 File Offset: 0x00026CB0
		public object TryGetStrongReference()
		{
			object obj = this.StrongReference;
			if (obj == null && this.WeakReference != null)
			{
				obj = this.WeakReference.Target;
			}
			return obj;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00028ADC File Offset: 0x00026CDC
		public void Weaken()
		{
			object strongReference = this.StrongReference;
			if (strongReference != null)
			{
				this.WeakReference = new WeakReference(strongReference);
				this.StrongReference = null;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00028B06 File Offset: 0x00026D06
		public bool InMemory
		{
			get
			{
				return this.StrongReference != null || (this.WeakReference != null && this.WeakReference.IsAlive);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00028B27 File Offset: 0x00026D27
		public bool StronglyReferenced
		{
			get
			{
				return this.StrongReference != null;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00028B32 File Offset: 0x00026D32
		public int HandleCount
		{
			get
			{
				return this.Handles.Count;
			}
		}

		// Token: 0x04000355 RID: 853
		public string SqlSchemaName;

		// Token: 0x04000356 RID: 854
		public string Name;

		// Token: 0x04000357 RID: 855
		public Type Type;

		// Token: 0x04000358 RID: 856
		public List<TemporalHandle> Handles = new List<TemporalHandle>();

		// Token: 0x04000359 RID: 857
		public object StrongReference;

		// Token: 0x0400035A RID: 858
		public WeakReference WeakReference;

		// Token: 0x0400035B RID: 859
		public long PersistedSize = -1L;

		// Token: 0x0400035C RID: 860
		public DateTime LastLoaded;

		// Token: 0x0400035D RID: 861
		public DateTime LastAccessed;

		// Token: 0x0400035E RID: 862
		public object InstanceState;
	}
}
