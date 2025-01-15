using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000516 RID: 1302
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class ConditionalExceptionEntryPointsAttribute : Attribute
	{
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x00091A3A File Offset: 0x0008FC3A
		// (set) Token: 0x06002860 RID: 10336 RVA: 0x00091A42 File Offset: 0x0008FC42
		public Type EntryPointType { get; private set; }

		// Token: 0x06002861 RID: 10337 RVA: 0x00091A4B File Offset: 0x0008FC4B
		public ConditionalExceptionEntryPointsAttribute(Type entryPointType)
		{
			ExtendedDiagnostics.EnsureArgument("entryPointType", typeof(EntryPointIdentifier).IsAssignableFrom(entryPointType), "entryPointType must derive from EntryPointIdentifier");
			this.EntryPointType = entryPointType;
		}
	}
}
