using System;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x0200005F RID: 95
	internal class TelemetrySinkCollection : SnapshottingList<TelemetrySink>
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000D906 File Offset: 0x0000BB06
		public TelemetrySink DefaultSink
		{
			get
			{
				return this[0];
			}
		}

		// Token: 0x170000BF RID: 191
		public override TelemetrySink this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				if (index == 0 && base.Count != 0)
				{
					throw new InvalidOperationException("Default sink cannot be changed");
				}
				base[index] = value;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000D938 File Offset: 0x0000BB38
		public override bool Remove(TelemetrySink item)
		{
			if (this.IsDefaultSink(item))
			{
				throw new InvalidOperationException("Default sink cannot be removed");
			}
			return base.Remove(item);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000D955 File Offset: 0x0000BB55
		public override void Clear()
		{
			throw new InvalidOperationException("TelemetrySinkCollection cannot be cleared--default sink must always be available");
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000D961 File Offset: 0x0000BB61
		public override void Insert(int index, TelemetrySink item)
		{
			if (index == 0 && base.Count != 0)
			{
				throw new InvalidOperationException("Default sink cannot be changed");
			}
			base.Insert(index, item);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000D981 File Offset: 0x0000BB81
		public override void RemoveAt(int index)
		{
			if (index == 0)
			{
				throw new InvalidOperationException("Default sink cannot be changed");
			}
			base.RemoveAt(index);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000D998 File Offset: 0x0000BB98
		private bool IsDefaultSink(TelemetrySink sink)
		{
			return sink == this[0];
		}

		// Token: 0x04000138 RID: 312
		private const int DefaultSinkIndex = 0;

		// Token: 0x04000139 RID: 313
		private const string DefaultSinkCannotBeChanged = "Default sink cannot be changed";
	}
}
