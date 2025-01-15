using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000204 RID: 516
	public sealed class ResolvedQueryEquivalenceComparerContext
	{
		// Token: 0x06000E4D RID: 3661 RVA: 0x0001C153 File Offset: 0x0001A353
		internal ResolvedQueryEquivalenceComparerContext()
		{
			this._leftSourceNameToRightSourceNameMaps = new Stack<Dictionary<string, string>>();
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0001C166 File Offset: 0x0001A366
		public void BeginQuery()
		{
			this._leftSourceNameToRightSourceNameMaps.Push(new Dictionary<string, string>(QueryNameComparer.Instance));
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0001C17D File Offset: 0x0001A37D
		public void EndQuery()
		{
			this._leftSourceNameToRightSourceNameMaps.Pop();
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0001C18B File Offset: 0x0001A38B
		public void AddSourceNameMapping(string leftName, string rightName)
		{
			this._leftSourceNameToRightSourceNameMaps.Peek().Add(leftName, rightName);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0001C19F File Offset: 0x0001A39F
		public bool TryGetSourceNameMapping(string leftName, out string rightName)
		{
			if (this._leftSourceNameToRightSourceNameMaps.Count == 0)
			{
				rightName = null;
				return false;
			}
			return this._leftSourceNameToRightSourceNameMaps.Peek().TryGetValue(leftName, out rightName);
		}

		// Token: 0x0400070F RID: 1807
		private readonly Stack<Dictionary<string, string>> _leftSourceNameToRightSourceNameMaps;
	}
}
