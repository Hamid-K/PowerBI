using System;
using System.Collections.Generic;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200007D RID: 125
	internal sealed class IgnoreReferenceResolver : ReferenceResolver
	{
		// Token: 0x06000825 RID: 2085 RVA: 0x00024B2D File Offset: 0x00022D2D
		internal override void PopReferenceForCycleDetection()
		{
			this._stackForCycleDetection.Pop();
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00024B3B File Offset: 0x00022D3B
		internal override bool ContainsReferenceForCycleDetection(object value)
		{
			Stack<ReferenceEqualsWrapper> stackForCycleDetection = this._stackForCycleDetection;
			return stackForCycleDetection != null && stackForCycleDetection.Contains(new ReferenceEqualsWrapper(value));
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00024B54 File Offset: 0x00022D54
		internal override void PushReferenceForCycleDetection(object value)
		{
			ReferenceEqualsWrapper referenceEqualsWrapper = new ReferenceEqualsWrapper(value);
			if (this._stackForCycleDetection == null)
			{
				this._stackForCycleDetection = new Stack<ReferenceEqualsWrapper>();
			}
			this._stackForCycleDetection.Push(referenceEqualsWrapper);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00024B88 File Offset: 0x00022D88
		public override void AddReference(string referenceId, object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00024B8F File Offset: 0x00022D8F
		public override string GetReference(object value, out bool alreadyExists)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00024B96 File Offset: 0x00022D96
		public override object ResolveReference(string referenceId)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x040002E3 RID: 739
		private Stack<ReferenceEqualsWrapper> _stackForCycleDetection;
	}
}
