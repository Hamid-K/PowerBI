using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200004D RID: 77
	internal sealed class SchemaElementLookUpTableEnumerator<T, S> : IEnumerator<T>, IDisposable, IEnumerator where T : S where S : SchemaElement
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x00011214 File Offset: 0x0000F414
		public SchemaElementLookUpTableEnumerator(Dictionary<string, S> data, List<string> keysInOrder)
		{
			this._data = data;
			this._enumerator = keysInOrder.GetEnumerator();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001122F File Offset: 0x0000F42F
		public void Reset()
		{
			((IEnumerator)this._enumerator).Reset();
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00011244 File Offset: 0x0000F444
		public T Current
		{
			get
			{
				string text = this._enumerator.Current;
				return this._data[text] as T;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00011278 File Offset: 0x0000F478
		object IEnumerator.Current
		{
			get
			{
				string text = this._enumerator.Current;
				return this._data[text] as T;
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000112B1 File Offset: 0x0000F4B1
		public bool MoveNext()
		{
			while (this._enumerator.MoveNext())
			{
				if (this.Current != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000112D2 File Offset: 0x0000F4D2
		public void Dispose()
		{
		}

		// Token: 0x040006B8 RID: 1720
		private Dictionary<string, S> _data;

		// Token: 0x040006B9 RID: 1721
		private List<string>.Enumerator _enumerator;
	}
}
