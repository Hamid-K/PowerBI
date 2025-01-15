using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x0200078D RID: 1933
	internal sealed class BatchEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
	{
		// Token: 0x060038B0 RID: 14512 RVA: 0x000B7017 File Offset: 0x000B5217
		public BatchEnumerator(ODataReaderEnumerator innerEnumerator, ODataEnvironment environment)
		{
			this.innerEnumerator = innerEnumerator;
			this.storedValues = new Queue<IValueReference>();
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x060038B1 RID: 14513 RVA: 0x000B7031 File Offset: 0x000B5231
		public IValueReference Current
		{
			get
			{
				return this.storedValues.Peek();
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000B703E File Offset: 0x000B523E
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000B7046 File Offset: 0x000B5246
		public void Dispose()
		{
			this.storedValues.Clear();
			this.innerEnumerator.Dispose();
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000B705E File Offset: 0x000B525E
		public bool MoveNext()
		{
			if (this.storedValues.Count != 0)
			{
				this.storedValues.Dequeue();
			}
			if (this.storedValues.Count == 0)
			{
				this.ReadPage();
			}
			return this.storedValues.Count != 0;
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000091AE File Offset: 0x000073AE
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000B709C File Offset: 0x000B529C
		private void ReadPage()
		{
			int num = 0;
			while (num < 100 && this.innerEnumerator.MoveNext())
			{
				this.storedValues.Enqueue(this.innerEnumerator.Current);
				num++;
			}
		}

		// Token: 0x04001D51 RID: 7505
		private const int rowCountLimit = 100;

		// Token: 0x04001D52 RID: 7506
		private readonly ODataReaderEnumerator innerEnumerator;

		// Token: 0x04001D53 RID: 7507
		private readonly Queue<IValueReference> storedValues;
	}
}
