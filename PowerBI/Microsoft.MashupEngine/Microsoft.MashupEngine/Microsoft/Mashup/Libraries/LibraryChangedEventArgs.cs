using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Libraries
{
	// Token: 0x020020D0 RID: 8400
	public sealed class LibraryChangedEventArgs : EventArgs
	{
		// Token: 0x0600CDCD RID: 52685 RVA: 0x0028E423 File Offset: 0x0028C623
		public LibraryChangedEventArgs(string[] added, string[] changed, string[] removed, Dictionary<string, Exception> failures)
		{
			this.added = added;
			this.changed = changed;
			this.removed = removed;
			this.failures = failures;
		}

		// Token: 0x0600CDCE RID: 52686 RVA: 0x0028E448 File Offset: 0x0028C648
		public LibraryChangedEventArgs(string[] added, string[] changed, string[] removed)
			: this(added, changed, removed, new Dictionary<string, Exception>())
		{
		}

		// Token: 0x17003177 RID: 12663
		// (get) Token: 0x0600CDCF RID: 52687 RVA: 0x0028E458 File Offset: 0x0028C658
		public string[] Added
		{
			get
			{
				return this.added;
			}
		}

		// Token: 0x17003178 RID: 12664
		// (get) Token: 0x0600CDD0 RID: 52688 RVA: 0x0028E460 File Offset: 0x0028C660
		public string[] Changed
		{
			get
			{
				return this.changed;
			}
		}

		// Token: 0x17003179 RID: 12665
		// (get) Token: 0x0600CDD1 RID: 52689 RVA: 0x0028E468 File Offset: 0x0028C668
		public string[] Removed
		{
			get
			{
				return this.removed;
			}
		}

		// Token: 0x1700317A RID: 12666
		// (get) Token: 0x0600CDD2 RID: 52690 RVA: 0x0028E470 File Offset: 0x0028C670
		public Dictionary<string, Exception> Failures
		{
			get
			{
				return this.failures;
			}
		}

		// Token: 0x04006810 RID: 26640
		private readonly string[] added;

		// Token: 0x04006811 RID: 26641
		private readonly string[] changed;

		// Token: 0x04006812 RID: 26642
		private readonly string[] removed;

		// Token: 0x04006813 RID: 26643
		private Dictionary<string, Exception> failures;
	}
}
