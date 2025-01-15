using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.DataShaping;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000021 RID: 33
	[DataContract]
	internal sealed class DataReductionAxisTelemetry
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00008D5D File Offset: 0x00006F5D
		internal IReadOnlyList<DataReductionAlgorithmTelemetryState> InitialState
		{
			get
			{
				return this._initialState;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00008D65 File Offset: 0x00006F65
		internal IReadOnlyList<DataReductionAlgorithmTelemetryState> FinalState
		{
			get
			{
				return this._finalState;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00008D6D File Offset: 0x00006F6D
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00008D75 File Offset: 0x00006F75
		[DataMember(Name = "KeyDCStat", EmitDefaultValue = false, Order = 40)]
		internal int StatsDistinctCount { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00008D7E File Offset: 0x00006F7E
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00008D86 File Offset: 0x00006F86
		[DataMember(Name = "KeyDCFilter", EmitDefaultValue = false, Order = 50)]
		internal int FilterDistinctCount { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00008D8F File Offset: 0x00006F8F
		internal IReadOnlyList<string> Flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008D97 File Offset: 0x00006F97
		internal void AddState(DataReductionAlgorithmTelemetryState algorithmState, bool isInitial)
		{
			if (isInitial)
			{
				Util.AddToLazyList<DataReductionAlgorithmTelemetryState>(ref this._initialState, algorithmState);
				return;
			}
			Util.AddToLazyList<DataReductionAlgorithmTelemetryState>(ref this._finalState, algorithmState);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008DB5 File Offset: 0x00006FB5
		internal void AddFlag(string flag)
		{
			Util.AddToLazyList<string>(ref this._flags, flag);
		}

		// Token: 0x040000AB RID: 171
		[DataMember(Name = "Init", EmitDefaultValue = true, Order = 10)]
		[JsonConverter(typeof(DataReductionAlgorithmTelemetryStateCollectionConverter))]
		private List<DataReductionAlgorithmTelemetryState> _initialState;

		// Token: 0x040000AC RID: 172
		[DataMember(Name = "Final", EmitDefaultValue = true, Order = 20)]
		[JsonConverter(typeof(DataReductionAlgorithmTelemetryStateCollectionConverter))]
		private List<DataReductionAlgorithmTelemetryState> _finalState;

		// Token: 0x040000AD RID: 173
		[DataMember(Name = "Flags", EmitDefaultValue = false, Order = 30)]
		private List<string> _flags;
	}
}
