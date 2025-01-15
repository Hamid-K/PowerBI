using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.OData.Client
{
	// Token: 0x020000D3 RID: 211
	[SuppressMessage("Microsoft.Design", "CA1010", Justification = "required for this feature")]
	[SuppressMessage("Microsoft.Naming", "CA1710", Justification = "required for this feature")]
	public class QueryOperationResponse : OperationResponse, IEnumerable
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x0001CA7F File Offset: 0x0001AC7F
		internal QueryOperationResponse(HeaderCollection headers, DataServiceRequest query, MaterializeAtom results)
			: base(headers)
		{
			this.query = query;
			this.results = results;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001CA96 File Offset: 0x0001AC96
		public DataServiceRequest Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0000A08D File Offset: 0x0000828D
		public virtual long TotalCount
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001CA9E File Offset: 0x0001AC9E
		internal MaterializeAtom Results
		{
			get
			{
				if (base.Error != null)
				{
					throw Microsoft.OData.Client.Error.InvalidOperation(Strings.Context_BatchExecuteError, base.Error);
				}
				return this.results;
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001CABF File Offset: 0x0001ACBF
		public IEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper<IEnumerator>(() => this.Results.GetEnumerator());
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001CAD3 File Offset: 0x0001ACD3
		public DataServiceQueryContinuation GetContinuation()
		{
			return this.results.GetContinuation(null);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001CAE1 File Offset: 0x0001ACE1
		public DataServiceQueryContinuation GetContinuation(IEnumerable collection)
		{
			return this.results.GetContinuation(collection);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001CAEF File Offset: 0x0001ACEF
		public DataServiceQueryContinuation<T> GetContinuation<T>(IEnumerable<T> collection)
		{
			return (DataServiceQueryContinuation<T>)this.results.GetContinuation(collection);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001CB04 File Offset: 0x0001AD04
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal static QueryOperationResponse GetInstance(Type elementType, HeaderCollection headers, DataServiceRequest query, MaterializeAtom results)
		{
			Type type = typeof(QueryOperationResponse<>).MakeGenericType(new Type[] { elementType });
			return (QueryOperationResponse)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[] { headers, query, results }, CultureInfo.InvariantCulture);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001CB54 File Offset: 0x0001AD54
		protected T GetEnumeratorHelper<T>(Func<T> getEnumerator) where T : IEnumerator
		{
			if (getEnumerator == null)
			{
				throw new ArgumentNullException("getEnumerator");
			}
			if (this.Results.Context != null)
			{
				bool? singleResult = this.Query.QueryComponents(this.Results.Context.Model).SingleResult;
				if (singleResult != null && !singleResult.Value)
				{
					IEnumerator enumerator = this.Results.GetEnumerator();
					if (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ICollection collection = obj as ICollection;
						if (collection == null)
						{
							throw new DataServiceClientException(Strings.AtomMaterializer_CollectionExpectedCollection(obj.GetType().ToString()));
						}
						return (T)((object)collection.GetEnumerator());
					}
				}
			}
			return getEnumerator();
		}

		// Token: 0x040002F7 RID: 759
		private readonly DataServiceRequest query;

		// Token: 0x040002F8 RID: 760
		private readonly MaterializeAtom results;
	}
}
