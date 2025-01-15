using System;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Interop
{
	// Token: 0x02001FE8 RID: 8168
	internal struct ArraySegment
	{
		// Token: 0x060110D3 RID: 69843 RVA: 0x003AD106 File Offset: 0x003AB306
		public ArraySegment(Array array)
		{
			this = new ArraySegment(array, 0, array.Length);
		}

		// Token: 0x060110D4 RID: 69844 RVA: 0x003AD116 File Offset: 0x003AB316
		public ArraySegment(Array array, int offset, int count)
		{
			this.array = array;
			this.offset = offset;
			this.count = count;
		}

		// Token: 0x17002D00 RID: 11520
		// (get) Token: 0x060110D5 RID: 69845 RVA: 0x003AD12D File Offset: 0x003AB32D
		public Array Array
		{
			get
			{
				return this.array;
			}
		}

		// Token: 0x17002D01 RID: 11521
		// (get) Token: 0x060110D6 RID: 69846 RVA: 0x003AD135 File Offset: 0x003AB335
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17002D02 RID: 11522
		// (get) Token: 0x060110D7 RID: 69847 RVA: 0x003AD13D File Offset: 0x003AB33D
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x060110D8 RID: 69848 RVA: 0x003AD145 File Offset: 0x003AB345
		public ArraySegment<T> Cast<T>()
		{
			return new ArraySegment<T>((T[])this.array, this.offset, this.count);
		}

		// Token: 0x04006725 RID: 26405
		private readonly Array array;

		// Token: 0x04006726 RID: 26406
		private readonly int offset;

		// Token: 0x04006727 RID: 26407
		private readonly int count;
	}
}
