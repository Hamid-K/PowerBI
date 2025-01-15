using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000834 RID: 2100
	internal sealed class BatchEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
	{
		// Token: 0x06003C77 RID: 15479 RVA: 0x000C4301 File Offset: 0x000C2501
		public BatchEnumerator(ODataReaderEnumerator innerEnumerator, ODataEnvironment environment)
		{
			this.innerEnumerator = innerEnumerator;
			this.storedValues = new Queue<IValueReference>();
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x000C431B File Offset: 0x000C251B
		public IValueReference Current
		{
			get
			{
				return this.storedValues.Peek();
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x06003C79 RID: 15481 RVA: 0x000C4328 File Offset: 0x000C2528
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x000C4330 File Offset: 0x000C2530
		public void Dispose()
		{
			this.storedValues.Clear();
			this.innerEnumerator.Dispose();
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x000C4348 File Offset: 0x000C2548
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

		// Token: 0x06003C7C RID: 15484 RVA: 0x000091AE File Offset: 0x000073AE
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x000C4384 File Offset: 0x000C2584
		private void ReadPage()
		{
			int num = 0;
			while (num < 100 && this.innerEnumerator.MoveNext())
			{
				this.storedValues.Enqueue(this.innerEnumerator.Current);
				num++;
			}
		}

		// Token: 0x04001F9B RID: 8091
		private const int rowCountLimit = 100;

		// Token: 0x04001F9C RID: 8092
		private readonly ODataReaderEnumerator innerEnumerator;

		// Token: 0x04001F9D RID: 8093
		private readonly Queue<IValueReference> storedValues;
	}
}
