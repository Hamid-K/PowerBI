using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000577 RID: 1399
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class CollectionAccessAttribute : Attribute
	{
		// Token: 0x06001F02 RID: 7938 RVA: 0x00059ADB File Offset: 0x00057CDB
		public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
		{
			this.CollectionAccessType = collectionAccessType;
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x00059AEA File Offset: 0x00057CEA
		public CollectionAccessType CollectionAccessType { get; }
	}
}
