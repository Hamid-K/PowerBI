using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000011 RID: 17
	public sealed class ExecutionMetricsColumnNames
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public ExecutionMetricsColumnNames(string id, string parentid, string name, string component, string start, string end, string metrics)
		{
			this._slotMapping = new Dictionary<string, ExecutionMetricsSlot>(StringComparer.Ordinal);
			this._slotMapping.Add(id, ExecutionMetricsSlot.Id);
			this._slotMapping.Add(parentid, ExecutionMetricsSlot.ParentId);
			this._slotMapping.Add(name, ExecutionMetricsSlot.Name);
			this._slotMapping.Add(component, ExecutionMetricsSlot.Component);
			this._slotMapping.Add(start, ExecutionMetricsSlot.Start);
			this._slotMapping.Add(end, ExecutionMetricsSlot.End);
			this._slotMapping.Add(metrics, ExecutionMetricsSlot.Metrics);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002E52 File Offset: 0x00001052
		internal int Count
		{
			get
			{
				return this._slotMapping.Count;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E5F File Offset: 0x0000105F
		internal bool TryGetSlot(string name, out ExecutionMetricsSlot slot)
		{
			return this._slotMapping.TryGetValue(name, out slot);
		}

		// Token: 0x04000077 RID: 119
		private readonly Dictionary<string, ExecutionMetricsSlot> _slotMapping;
	}
}
