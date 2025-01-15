using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000F1 RID: 241
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class GaugePanelObjectCollectionBase<T> : IEnumerable<T>, IEnumerable where T : GaugePanelObjectCollectionItem
	{
		// Token: 0x17000654 RID: 1620
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_collection == null)
				{
					this.m_collection = new T[this.Count];
				}
				if (this.m_collection[index] == null)
				{
					this.m_collection[index] = this.CreateGaugePanelObject(index);
				}
				return this.m_collection[index];
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06000B28 RID: 2856
		public abstract int Count { get; }

		// Token: 0x06000B29 RID: 2857
		protected abstract T CreateGaugePanelObject(int index);

		// Token: 0x06000B2A RID: 2858 RVA: 0x00032006 File Offset: 0x00030206
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00032015 File Offset: 0x00030215
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00032020 File Offset: 0x00030220
		internal void SetNewContext()
		{
			if (this.m_collection != null)
			{
				for (int i = 0; i < this.m_collection.Length; i++)
				{
					T t = this.m_collection[i];
					if (t != null)
					{
						t.SetNewContext();
					}
				}
			}
		}

		// Token: 0x040004C3 RID: 1219
		private T[] m_collection;
	}
}
