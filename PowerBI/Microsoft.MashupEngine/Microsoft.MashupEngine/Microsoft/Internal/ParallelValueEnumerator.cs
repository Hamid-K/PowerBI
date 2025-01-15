using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Internal
{
	// Token: 0x020001BF RID: 447
	internal class ParallelValueEnumerator : ParallelEnumerator<IValueReference>
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x00010E71 File Offset: 0x0000F071
		public ParallelValueEnumerator(IEngineHost engineHost, int readers, Func<int, IValueReference> getBufferedValue)
			: base(engineHost, null, readers, getBufferedValue, delegate(IValueReference value)
			{
			})
		{
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00010E9C File Offset: 0x0000F09C
		protected override bool TryGetNextItem(out ParallelEnumerator<IValueReference>.Item item)
		{
			bool flag;
			using (EngineContext.Leave())
			{
				flag = base.TryGetNextItem(out item);
			}
			return flag;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		protected override IValueReference GetItemValue(int index)
		{
			IValueReference itemValue;
			using (EngineContext.Enter())
			{
				itemValue = base.GetItemValue(index);
			}
			return itemValue;
		}
	}
}
