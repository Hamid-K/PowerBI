using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000D4 RID: 212
	internal sealed class BindingInformationCollector
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x00022686 File Offset: 0x00020886
		internal void StartRecording(IScope scope)
		{
			if (this.m_records == null)
			{
				this.m_records = new Stack<KeyValuePair<IScope, int>>();
			}
			this.m_records.Push(new KeyValuePair<IScope, int>(scope, -1));
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000226B0 File Offset: 0x000208B0
		internal int StopRecording(IScope scope)
		{
			return this.m_records.Pop().Value;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000226D0 File Offset: 0x000208D0
		internal void RecordItemPlanIndex(int index)
		{
			if (this.m_records.Count > 0)
			{
				KeyValuePair<IScope, int> keyValuePair = this.m_records.Peek();
				if (keyValuePair.Value == -1)
				{
					this.m_records.Pop();
					this.m_records.Push(new KeyValuePair<IScope, int>(keyValuePair.Key, index));
					return;
				}
				Contract.RetailAssert(keyValuePair.Value == index, "Expected all child items to have the same binding index");
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002273A File Offset: 0x0002093A
		internal bool IsValidBindingIndex(int index)
		{
			return index > -1;
		}

		// Token: 0x04000437 RID: 1079
		private const int InvalidBindingIndex = -1;

		// Token: 0x04000438 RID: 1080
		private Stack<KeyValuePair<IScope, int>> m_records;
	}
}
