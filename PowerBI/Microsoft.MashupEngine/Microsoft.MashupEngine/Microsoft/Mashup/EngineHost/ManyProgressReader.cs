using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200196E RID: 6510
	public class ManyProgressReader : IProgressReader, IManyProgressReader
	{
		// Token: 0x0600A53B RID: 42299 RVA: 0x002231F0 File Offset: 0x002213F0
		public ManyProgressReader()
		{
			this.readers = new List<IProgressReader>();
			this.removed = new List<byte[]>();
		}

		// Token: 0x0600A53C RID: 42300 RVA: 0x00223210 File Offset: 0x00221410
		public void AddReader(IProgressReader reader)
		{
			List<IProgressReader> list = this.readers;
			lock (list)
			{
				this.readers.Add(reader);
			}
		}

		// Token: 0x0600A53D RID: 42301 RVA: 0x00223258 File Offset: 0x00221458
		public void RemoveReader(IProgressReader reader)
		{
			List<IProgressReader> list = this.readers;
			lock (list)
			{
				this.readers.Remove(reader);
				this.removed.AddRange(reader.ReadAllProgress());
			}
		}

		// Token: 0x0600A53E RID: 42302 RVA: 0x002232B0 File Offset: 0x002214B0
		public IEnumerable<byte[]> ReadAllProgress()
		{
			List<IProgressReader> list = this.readers;
			IEnumerable<byte[]> enumerable2;
			lock (list)
			{
				IEnumerable<byte[]> enumerable = this.removed.Concat(this.readers.SelectMany((IProgressReader r) => r.ReadAllProgress())).ToArray<byte[]>();
				this.removed.Clear();
				enumerable2 = enumerable;
			}
			return enumerable2;
		}

		// Token: 0x04005608 RID: 22024
		private readonly List<IProgressReader> readers;

		// Token: 0x04005609 RID: 22025
		private readonly List<byte[]> removed;
	}
}
