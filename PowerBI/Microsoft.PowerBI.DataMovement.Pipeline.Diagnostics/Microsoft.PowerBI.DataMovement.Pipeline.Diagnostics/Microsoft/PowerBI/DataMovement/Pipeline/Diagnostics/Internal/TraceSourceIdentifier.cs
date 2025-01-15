using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000E2 RID: 226
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class TraceSourceIdentifier : IEquatable<TraceSourceIdentifier>
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x00046854 File Offset: 0x00044A54
		internal TraceSourceIdentifier(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x00046863 File Offset: 0x00044A63
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x0004686B File Offset: 0x00044A6B
		public string Name { get; private set; }

		// Token: 0x0600110A RID: 4362 RVA: 0x00046874 File Offset: 0x00044A74
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0004687C File Offset: 0x00044A7C
		public override bool Equals(object obj)
		{
			TraceSourceIdentifier traceSourceIdentifier = obj as TraceSourceIdentifier;
			return this.Equals(traceSourceIdentifier);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00046897 File Offset: 0x00044A97
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000468A4 File Offset: 0x00044AA4
		public bool Equals(TraceSourceIdentifier other)
		{
			return other != null && this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
		}
	}
}
