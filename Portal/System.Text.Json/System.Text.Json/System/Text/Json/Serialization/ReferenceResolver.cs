using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200009A RID: 154
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ReferenceResolver
	{
		// Token: 0x06000910 RID: 2320
		public abstract void AddReference(string referenceId, object value);

		// Token: 0x06000911 RID: 2321
		public abstract string GetReference(object value, out bool alreadyExists);

		// Token: 0x06000912 RID: 2322
		public abstract object ResolveReference(string referenceId);

		// Token: 0x06000913 RID: 2323 RVA: 0x000273B8 File Offset: 0x000255B8
		internal virtual void PopReferenceForCycleDetection()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000273BF File Offset: 0x000255BF
		internal virtual void PushReferenceForCycleDetection(object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000273C6 File Offset: 0x000255C6
		internal virtual bool ContainsReferenceForCycleDetection(object value)
		{
			throw new InvalidOperationException();
		}
	}
}
