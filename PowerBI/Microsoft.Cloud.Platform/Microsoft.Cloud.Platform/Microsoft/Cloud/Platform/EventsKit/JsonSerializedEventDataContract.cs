using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000331 RID: 817
	[DataContract]
	public class JsonSerializedEventDataContract
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x000585A9 File Offset: 0x000567A9
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x000585B1 File Offset: 0x000567B1
		[DataMember(Name = "s")]
		public string Source { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x000585BA File Offset: 0x000567BA
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x000585C2 File Offset: 0x000567C2
		[DataMember(Name = "pid")]
		public int ProcessId { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x000585CB File Offset: 0x000567CB
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x000585D3 File Offset: 0x000567D3
		[DataMember(Name = "tid")]
		public int ThreadId { get; set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x000585DC File Offset: 0x000567DC
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x000585E4 File Offset: 0x000567E4
		[DataMember(Name = "id", IsRequired = true)]
		public long EventId { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x000585ED File Offset: 0x000567ED
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x000585F5 File Offset: 0x000567F5
		[DataMember(Name = "n", IsRequired = true)]
		public string EventName { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x000585FE File Offset: 0x000567FE
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x00058606 File Offset: 0x00056806
		[DataMember(Name = "m")]
		public string Message { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0005860F File Offset: 0x0005680F
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x00058617 File Offset: 0x00056817
		[DataMember(Name = "aid", IsRequired = true)]
		public Guid ActivityId { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x00058620 File Offset: 0x00056820
		// (set) Token: 0x06001812 RID: 6162 RVA: 0x00058628 File Offset: 0x00056828
		[DataMember(Name = "at", IsRequired = true)]
		public string ActivityType { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x00058631 File Offset: 0x00056831
		// (set) Token: 0x06001814 RID: 6164 RVA: 0x00058639 File Offset: 0x00056839
		[DataMember(Name = "raid", IsRequired = true)]
		public Guid RootActivityId { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00058642 File Offset: 0x00056842
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x0005864A File Offset: 0x0005684A
		[DataMember(Name = "caid", IsRequired = true)]
		public string ClientActivityId { get; set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00058653 File Offset: 0x00056853
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x0005865B File Offset: 0x0005685B
		[DataMember(Name = "t", IsRequired = true)]
		public DateTime Timestamp { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x00058664 File Offset: 0x00056864
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x0005866C File Offset: 0x0005686C
		[DataMember(Name = "l")]
		public EventLevel Level { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x00058675 File Offset: 0x00056875
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x0005867D File Offset: 0x0005687D
		[DataMember(Name = "el", IsRequired = true)]
		public string ElementId { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x00058686 File Offset: 0x00056886
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x0005868E File Offset: 0x0005688E
		[DataMember(Name = "p")]
		public IEnumerable<KeyValuePair<string, object>> EventParameters { get; set; }

		// Token: 0x0400085C RID: 2140
		public const string DatePrefix = "/Date(";

		// Token: 0x0400085D RID: 2141
		public const string DateSuffix = ")/";

		// Token: 0x0400085E RID: 2142
		public const string GuidPrefix = "/Guid(";

		// Token: 0x0400085F RID: 2143
		public const string GuidSuffix = ")/";
	}
}
