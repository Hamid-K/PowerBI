using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000D6 RID: 214
	public class TemporalHandle
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00028A36 File Offset: 0x00026C36
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x00028A3E File Offset: 0x00026C3E
		public DateTime LastAccessed
		{
			get
			{
				return this.m_lastAccessed;
			}
			set
			{
				this.m_lastAccessed = value;
				if (this.Reference != null)
				{
					this.Reference.LastAccessed = value;
				}
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00028A5B File Offset: 0x00026C5B
		public void Touch()
		{
			this.LastAccessed = DateTime.Now;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00028A68 File Offset: 0x00026C68
		public object GetObject()
		{
			object obj = this.Reference.TryGetStrongReference();
			if (obj == null)
			{
				throw new InvalidOperationException("Object handle expired.  Try increasing the object timeout and/or ensure that the object has been committed.");
			}
			this.LastAccessed = DateTime.Now;
			return obj;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00028A8E File Offset: 0x00026C8E
		public object TryGetObject()
		{
			object obj = this.Reference.TryGetStrongReference();
			this.LastAccessed = DateTime.Now;
			return obj;
		}

		// Token: 0x04000351 RID: 849
		public int Id;

		// Token: 0x04000352 RID: 850
		public ObjectReference Reference;

		// Token: 0x04000353 RID: 851
		public int Timeout;

		// Token: 0x04000354 RID: 852
		private DateTime m_lastAccessed;
	}
}
