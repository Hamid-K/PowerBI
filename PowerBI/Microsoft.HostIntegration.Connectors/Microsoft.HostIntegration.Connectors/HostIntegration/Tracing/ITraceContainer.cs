using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x0200065D RID: 1629
	public interface ITraceContainer
	{
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003641 RID: 13889
		bool SupportsInstances { get; }

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06003642 RID: 13890
		// (set) Token: 0x06003643 RID: 13891
		List<string> InstanceNamesInConfigurationFile { get; set; }

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003644 RID: 13892
		// (set) Token: 0x06003645 RID: 13893
		bool IsInConfigurationFile { get; set; }

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003646 RID: 13894
		bool UsesLevels { get; }

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003647 RID: 13895
		string Name { get; }

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003648 RID: 13896
		string DisplayName { get; }

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003649 RID: 13897
		// (set) Token: 0x0600364A RID: 13898
		string InstanceName { get; set; }

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600364B RID: 13899
		string InstanceDisplayName { get; }

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600364C RID: 13900
		int Identifier { get; }

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600364D RID: 13901
		List<ITracePointInformation> TracePoints { get; }

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600364E RID: 13902
		// (set) Token: 0x0600364F RID: 13903
		TraceTree TraceTree { get; set; }

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06003650 RID: 13904
		List<ITracePointPropertyInformation> AllProperties { get; }

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06003651 RID: 13905
		bool CanBeUsed { get; }

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003652 RID: 13906
		int ProcessId { get; }

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003653 RID: 13907
		int HighestPropertyIdentifier { get; }

		// Token: 0x06003654 RID: 13908
		ITracePointPropertyInformation PropertyInformationFromName(string name);

		// Token: 0x06003655 RID: 13909
		ITracePointPropertyInformation PropertyInformationFromIdentifier(int identifier);

		// Token: 0x06003656 RID: 13910
		ITracePointInformation TracePointInformationFromIdentifier(int identifier);

		// Token: 0x06003657 RID: 13911
		TraceTree CreateInstanceTracepoint(BaseTracePoint topLevelTracePoint);

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003658 RID: 13912
		long Correlator { get; }

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06003659 RID: 13913
		ILiveTraceWriter TraceWriter { get; }

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x0600365A RID: 13914
		int MaximumDataBytesTraced { get; }

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600365B RID: 13915
		// (set) Token: 0x0600365C RID: 13916
		int CodePageForData { get; set; }

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600365D RID: 13917
		bool HasSomeTracingSet { get; }

		// Token: 0x17000BD8 RID: 3032
		// (set) Token: 0x0600365E RID: 13918
		bool HasHisListener { set; }

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600365F RID: 13919
		bool ShouldTrace { get; }

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003660 RID: 13920
		int ExtraColumns { get; }

		// Token: 0x06003661 RID: 13921
		string GetColumnName(int columnNumber);

		// Token: 0x06003662 RID: 13922
		string GetLongestText(int columnNumber);

		// Token: 0x06003663 RID: 13923
		string GetColumn(string extraData, int columnNumber);

		// Token: 0x06003664 RID: 13924
		string PackExtraData();

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003665 RID: 13925
		bool[] FilterableColumns { get; }

		// Token: 0x06003666 RID: 13926
		int MapSharedTracePointToSpecific(SharedTracePoints sharedTracePoint);

		// Token: 0x06003667 RID: 13927
		int[] SharedPropertiesToSpecific(SharedTracePoints sharedTracePoint);
	}
}
