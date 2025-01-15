using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200001A RID: 26
	public class ExplorationDeserializationResult<T>
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003B13 File Offset: 0x00001D13
		public ExplorationDeserializationResult(T value, IEnumerable<ExplorationDeserializationWarning> warnings)
		{
			this.Value = value;
			this.Warnings = warnings;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003B29 File Offset: 0x00001D29
		public T Value { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003B31 File Offset: 0x00001D31
		public IEnumerable<ExplorationDeserializationWarning> Warnings { get; }
	}
}
