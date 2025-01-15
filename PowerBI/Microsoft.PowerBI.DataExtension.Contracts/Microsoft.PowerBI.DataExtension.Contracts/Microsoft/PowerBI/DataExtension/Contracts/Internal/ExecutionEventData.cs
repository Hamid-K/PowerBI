using System;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x0200001F RID: 31
	public readonly struct ExecutionEventData
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00002ECA File Offset: 0x000010CA
		public ExecutionEventData(string id, string parentId, string name, string component, DateTime start, DateTime? end, string metrics)
		{
			this.Id = id;
			this.ParentId = parentId;
			this.Name = name;
			this.Component = component;
			this.Start = start;
			this.End = end;
			this.Metrics = metrics;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002F01 File Offset: 0x00001101
		public string Id { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002F09 File Offset: 0x00001109
		public string ParentId { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002F11 File Offset: 0x00001111
		public string Name { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002F19 File Offset: 0x00001119
		public string Component { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002F21 File Offset: 0x00001121
		public DateTime Start { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002F29 File Offset: 0x00001129
		public DateTime? End { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002F31 File Offset: 0x00001131
		public string Metrics { get; }
	}
}
