using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000300 RID: 768
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ReportElementCollectionBase<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000FCC RID: 4044
		public virtual T this[int i]
		{
			get
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			set
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06001C33 RID: 7219
		public abstract int Count { get; }

		// Token: 0x06001C34 RID: 7220 RVA: 0x00070781 File Offset: 0x0006E981
		public IEnumerator<T> GetEnumerator()
		{
			return new ReportElementCollectionBase<T>.ReportElementEnumerator(this);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00070789 File Offset: 0x0006E989
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x02000947 RID: 2375
		public class ReportElementEnumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06007FA3 RID: 32675 RVA: 0x0020DEE5 File Offset: 0x0020C0E5
			internal ReportElementEnumerator(ReportElementCollectionBase<T> collection)
			{
				this.m_collection = collection;
			}

			// Token: 0x1700296F RID: 10607
			// (get) Token: 0x06007FA4 RID: 32676 RVA: 0x0020DEFC File Offset: 0x0020C0FC
			public T Current
			{
				get
				{
					if (this.m_currentIndex < 0 || this.m_currentIndex >= this.m_collection.Count)
					{
						return default(T);
					}
					return this.m_collection[this.m_currentIndex];
				}
			}

			// Token: 0x06007FA5 RID: 32677 RVA: 0x0020DF40 File Offset: 0x0020C140
			public void Dispose()
			{
			}

			// Token: 0x17002970 RID: 10608
			// (get) Token: 0x06007FA6 RID: 32678 RVA: 0x0020DF42 File Offset: 0x0020C142
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06007FA7 RID: 32679 RVA: 0x0020DF4F File Offset: 0x0020C14F
			public bool MoveNext()
			{
				this.m_currentIndex++;
				return this.m_currentIndex < this.m_collection.Count;
			}

			// Token: 0x06007FA8 RID: 32680 RVA: 0x0020DF72 File Offset: 0x0020C172
			public void Reset()
			{
				this.m_currentIndex = -1;
			}

			// Token: 0x04004042 RID: 16450
			private ReportElementCollectionBase<T> m_collection;

			// Token: 0x04004043 RID: 16451
			private int m_currentIndex = -1;
		}
	}
}
