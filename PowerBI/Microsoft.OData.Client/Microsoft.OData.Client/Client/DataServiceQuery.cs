using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.OData.Client
{
	// Token: 0x020000CD RID: 205
	[SuppressMessage("Microsoft.Design", "CA1010", Justification = "required for this feature")]
	[SuppressMessage("Microsoft.Naming", "CA1710", Justification = "required for this feature")]
	public abstract class DataServiceQuery : DataServiceRequest, IQueryable, IEnumerable
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x0001C3DE File Offset: 0x0001A5DE
		internal DataServiceQuery()
		{
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060006A2 RID: 1698
		public abstract Expression Expression { get; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006A3 RID: 1699
		public abstract IQueryProvider Provider { get; }

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001C3E6 File Offset: 0x0001A5E6
		[SuppressMessage("Microsoft.Design", "CA1033", Justification = "required for this feature")]
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw Error.NotImplemented();
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001C3ED File Offset: 0x0001A5ED
		public IEnumerable Execute()
		{
			return this.ExecuteInternal();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001C3F5 File Offset: 0x0001A5F5
		public IAsyncResult BeginExecute(AsyncCallback callback, object state)
		{
			return this.BeginExecuteInternal(callback, state);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001C3FF File Offset: 0x0001A5FF
		public Task<IEnumerable> ExecuteAsync()
		{
			return Task<IEnumerable>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginExecute), new Func<IAsyncResult, IEnumerable>(this.EndExecute), null);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001C424 File Offset: 0x0001A624
		public IEnumerable EndExecute(IAsyncResult asyncResult)
		{
			return this.EndExecuteInternal(asyncResult);
		}

		// Token: 0x060006A9 RID: 1705
		internal abstract IEnumerable ExecuteInternal();

		// Token: 0x060006AA RID: 1706
		internal abstract IAsyncResult BeginExecuteInternal(AsyncCallback callback, object state);

		// Token: 0x060006AB RID: 1707
		internal abstract IEnumerable EndExecuteInternal(IAsyncResult asyncResult);
	}
}
