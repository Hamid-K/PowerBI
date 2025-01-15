using System;
using System.Collections;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000227 RID: 551
	internal sealed class DMSingleObjectEnumerator : IEnumerator
	{
		// Token: 0x06001260 RID: 4704 RVA: 0x0003A0E9 File Offset: 0x000382E9
		internal DMSingleObjectEnumerator(object data)
		{
			this._data = data;
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0003A0FF File Offset: 0x000382FF
		public object Current
		{
			get
			{
				if (this._moveNextOnce || this._data == null)
				{
					throw new InvalidOperationException();
				}
				return this._data;
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0003A11D File Offset: 0x0003831D
		public bool MoveNext()
		{
			if (this._moveNextOnce)
			{
				this._moveNextOnce = false;
				if (this._data != null)
				{
					return true;
				}
			}
			else
			{
				this._data = null;
			}
			return false;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000B26 RID: 2854
		private bool _moveNextOnce = true;

		// Token: 0x04000B27 RID: 2855
		private object _data;
	}
}
