using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200024B RID: 587
	internal class StackElement
	{
		// Token: 0x060013AF RID: 5039 RVA: 0x0003DB4F File Offset: 0x0003BD4F
		internal StackElement(FixedDepthEnumeratorState state, BaseHashTable hashTable)
		{
			this._state = state;
			this._hashTable = hashTable;
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0003DB65 File Offset: 0x0003BD65
		internal FixedDepthEnumeratorState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0003DB6D File Offset: 0x0003BD6D
		internal BaseHashTable HashTable
		{
			get
			{
				return this._hashTable;
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0003DB75 File Offset: 0x0003BD75
		internal object MoveNextAndGetData()
		{
			return this._hashTable.MoveNextAndGetData(this.State);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0003DB88 File Offset: 0x0003BD88
		internal object GetData()
		{
			return this._hashTable.GetData(this.State);
		}

		// Token: 0x04000BCF RID: 3023
		private FixedDepthEnumeratorState _state;

		// Token: 0x04000BD0 RID: 3024
		private BaseHashTable _hashTable;
	}
}
