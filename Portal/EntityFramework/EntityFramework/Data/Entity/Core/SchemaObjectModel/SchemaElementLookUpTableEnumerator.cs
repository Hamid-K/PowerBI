using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000315 RID: 789
	internal sealed class SchemaElementLookUpTableEnumerator<T, S> : IEnumerator<T>, IDisposable, IEnumerator where T : S where S : SchemaElement
	{
		// Token: 0x060025AE RID: 9646 RVA: 0x0006B82B File Offset: 0x00069A2B
		public SchemaElementLookUpTableEnumerator(Dictionary<string, S> data, List<string> keysInOrder)
		{
			this._data = data;
			this._enumerator = keysInOrder.GetEnumerator();
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x0006B846 File Offset: 0x00069A46
		public void Reset()
		{
			((IEnumerator)this._enumerator).Reset();
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x0006B858 File Offset: 0x00069A58
		public T Current
		{
			get
			{
				string text = this._enumerator.Current;
				return this._data[text] as T;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x0006B88C File Offset: 0x00069A8C
		object IEnumerator.Current
		{
			get
			{
				string text = this._enumerator.Current;
				return this._data[text] as T;
			}
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x0006B8C5 File Offset: 0x00069AC5
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

		// Token: 0x060025B3 RID: 9651 RVA: 0x0006B8E6 File Offset: 0x00069AE6
		public void Dispose()
		{
		}

		// Token: 0x04000D3F RID: 3391
		private readonly Dictionary<string, S> _data;

		// Token: 0x04000D40 RID: 3392
		private List<string>.Enumerator _enumerator;
	}
}
