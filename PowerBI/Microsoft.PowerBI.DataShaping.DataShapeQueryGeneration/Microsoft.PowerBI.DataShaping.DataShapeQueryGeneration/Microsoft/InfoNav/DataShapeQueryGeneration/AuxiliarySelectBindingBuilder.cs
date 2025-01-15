using System;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000006 RID: 6
	internal sealed class AuxiliarySelectBindingBuilder
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		internal AuxiliarySelectBindingBuilder()
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002077 File Offset: 0x00000277
		internal void Populate(SelectBinding selectBinding)
		{
			this.Result = new AuxiliarySelectBinding
			{
				Value = selectBinding.Value,
				Depth = selectBinding.Depth,
				SecondaryDepth = selectBinding.SecondaryDepth,
				GroupKeys = selectBinding.GroupKeys
			};
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020B4 File Offset: 0x000002B4
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020BC File Offset: 0x000002BC
		internal AuxiliarySelectBinding Result { get; private set; }
	}
}
