using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200017F RID: 383
	[Serializable]
	public sealed class ServiceResponseCollection<TResponse> : IEnumerable<TResponse>, IEnumerable where TResponse : ServiceResponse
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x00031D57 File Offset: 0x00030D57
		internal ServiceResponseCollection()
		{
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00031D6C File Offset: 0x00030D6C
		internal void Add(TResponse response)
		{
			EwsUtilities.Assert(response != null, "EwsResponseList.Add", "response is null");
			if (response.Result > this.overallResult)
			{
				this.overallResult = response.Result;
			}
			this.responses.Add(response);
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x00031DC8 File Offset: 0x00030DC8
		public int Count
		{
			get
			{
				return this.responses.Count;
			}
		}

		// Token: 0x17000390 RID: 912
		public TResponse this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
				}
				return this.responses[index];
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00031E05 File Offset: 0x00030E05
		public ServiceResult OverallResult
		{
			get
			{
				return this.overallResult;
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00031E0D File Offset: 0x00030E0D
		public IEnumerator<TResponse> GetEnumerator()
		{
			return this.responses.GetEnumerator();
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00031E1F File Offset: 0x00030E1F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.responses.GetEnumerator();
		}

		// Token: 0x040009D8 RID: 2520
		private List<TResponse> responses = new List<TResponse>();

		// Token: 0x040009D9 RID: 2521
		private ServiceResult overallResult;
	}
}
