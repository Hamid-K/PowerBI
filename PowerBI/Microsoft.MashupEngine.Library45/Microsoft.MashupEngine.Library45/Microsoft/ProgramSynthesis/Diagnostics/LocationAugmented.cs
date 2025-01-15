using System;

namespace Microsoft.ProgramSynthesis.Diagnostics
{
	// Token: 0x020008C1 RID: 2241
	internal struct LocationAugmented<T>
	{
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x0008E3D2 File Offset: 0x0008C5D2
		public readonly T Value { get; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x0008E3DA File Offset: 0x0008C5DA
		public readonly Location Location { get; }

		// Token: 0x06003036 RID: 12342 RVA: 0x0008E3E2 File Offset: 0x0008C5E2
		internal LocationAugmented(T value, Location location)
		{
			this = default(LocationAugmented<T>);
			this.Value = value;
			this.Location = location;
		}
	}
}
