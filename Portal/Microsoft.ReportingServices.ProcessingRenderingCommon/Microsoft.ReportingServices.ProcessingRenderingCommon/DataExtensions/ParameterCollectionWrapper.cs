using System;
using System.Collections;
using System.Data;
using Microsoft.ReportingServices.DataProcessing;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x0200001A RID: 26
	public class ParameterCollectionWrapper : BaseDataWrapper, Microsoft.ReportingServices.DataProcessing.IDataParameterCollection, IEnumerable
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00004EA0 File Offset: 0x000030A0
		protected internal ParameterCollectionWrapper(global::System.Data.IDataParameterCollection paramCollection)
			: base(paramCollection)
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004EB4 File Offset: 0x000030B4
		public virtual int Add(Microsoft.ReportingServices.DataProcessing.IDataParameter parameter)
		{
			ParameterWrapper parameterWrapper = (ParameterWrapper)parameter;
			int num = this.UnderlyingCollection.Add(parameterWrapper.UnderlyingParameter);
			this.Parameters.Add(parameterWrapper);
			return num;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004EE6 File Offset: 0x000030E6
		public virtual ParameterCollectionWrapper.Enumerator GetEnumerator()
		{
			return new ParameterCollectionWrapper.Enumerator(this.Parameters.GetEnumerator());
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004EF8 File Offset: 0x000030F8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004F00 File Offset: 0x00003100
		protected global::System.Data.IDataParameterCollection UnderlyingCollection
		{
			get
			{
				return (global::System.Data.IDataParameterCollection)base.UnderlyingObject;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004F0D File Offset: 0x0000310D
		protected ArrayList Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x04000085 RID: 133
		private ArrayList m_parameters = new ArrayList();

		// Token: 0x020000DC RID: 220
		public sealed class Enumerator : IEnumerator
		{
			// Token: 0x06000795 RID: 1941 RVA: 0x000143FF File Offset: 0x000125FF
			internal Enumerator(IEnumerator underlyingEnumerator)
			{
				this.m_underlyingEnumerator = underlyingEnumerator;
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x0001440E File Offset: 0x0001260E
			public bool MoveNext()
			{
				return this.m_underlyingEnumerator.MoveNext();
			}

			// Token: 0x170002C9 RID: 713
			// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001441B File Offset: 0x0001261B
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000798 RID: 1944 RVA: 0x00014423 File Offset: 0x00012623
			public void Reset()
			{
				this.m_underlyingEnumerator.Reset();
			}

			// Token: 0x170002CA RID: 714
			// (get) Token: 0x06000799 RID: 1945 RVA: 0x00014430 File Offset: 0x00012630
			public ParameterWrapper Current
			{
				get
				{
					return (ParameterWrapper)this.m_underlyingEnumerator.Current;
				}
			}

			// Token: 0x04000488 RID: 1160
			private IEnumerator m_underlyingEnumerator;
		}
	}
}
