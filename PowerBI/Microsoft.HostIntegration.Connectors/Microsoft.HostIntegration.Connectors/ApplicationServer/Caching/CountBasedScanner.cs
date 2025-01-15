using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200026E RID: 622
	internal class CountBasedScanner : IScanner
	{
		// Token: 0x060014CE RID: 5326 RVA: 0x000409FD File Offset: 0x0003EBFD
		internal CountBasedScanner()
			: this(int.MaxValue)
		{
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00040A0A File Offset: 0x0003EC0A
		internal CountBasedScanner(int batchCount)
		{
			if (batchCount == 2147483647)
			{
				this._list = new List<object>();
			}
			else
			{
				this._list = new List<object>(batchCount);
			}
			this._batchCount = batchCount;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00040A41 File Offset: 0x0003EC41
		internal List<object> Batch
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00040A49 File Offset: 0x0003EC49
		public bool Scan(object item)
		{
			this._list.Add(item);
			return true;
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00040A58 File Offset: 0x0003EC58
		// (set) Token: 0x060014D3 RID: 5331 RVA: 0x00040A60 File Offset: 0x0003EC60
		public bool InvalidateOnChange
		{
			get
			{
				return this._invalidateOnChange;
			}
			set
			{
				this._invalidateOnChange = value;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00040A69 File Offset: 0x0003EC69
		public bool BatchCompleted
		{
			get
			{
				return this._list.Count == this._batchCount;
			}
		}

		// Token: 0x04000C59 RID: 3161
		private List<object> _list;

		// Token: 0x04000C5A RID: 3162
		private readonly int _batchCount;

		// Token: 0x04000C5B RID: 3163
		private bool _invalidateOnChange = true;
	}
}
