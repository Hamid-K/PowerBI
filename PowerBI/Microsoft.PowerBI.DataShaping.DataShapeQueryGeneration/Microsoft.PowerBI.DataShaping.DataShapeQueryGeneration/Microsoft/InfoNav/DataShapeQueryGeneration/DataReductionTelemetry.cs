using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000020 RID: 32
	[DataContract]
	internal sealed class DataReductionTelemetry
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00008A88 File Offset: 0x00006C88
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00008A90 File Offset: 0x00006C90
		[DataMember(Name = "DataVolume", EmitDefaultValue = false, Order = 10)]
		internal int? DataVolume { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00008A99 File Offset: 0x00006C99
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00008AA1 File Offset: 0x00006CA1
		[DataMember(Name = "MaxInters", EmitDefaultValue = false, Order = 20)]
		internal int? MaxIntersections { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00008AAA File Offset: 0x00006CAA
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00008AB2 File Offset: 0x00006CB2
		[DataMember(Name = "MaxPtnlInters", EmitDefaultValue = false, Order = 25)]
		internal int? MaxPotentialIntersections { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00008ABB File Offset: 0x00006CBB
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00008AC3 File Offset: 0x00006CC3
		[DataMember(Name = "Dyn", EmitDefaultValue = false, Order = 26)]
		internal bool Dynamic { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00008ACC File Offset: 0x00006CCC
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00008AD4 File Offset: 0x00006CD4
		[DataMember(Name = "EligibleMeasureC", EmitDefaultValue = false, Order = 27)]
		internal int? EligibleMeasureCount { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00008ADD File Offset: 0x00006CDD
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00008AE5 File Offset: 0x00006CE5
		[DataMember(Name = "Primary", EmitDefaultValue = false, Order = 30)]
		internal DataReductionAxisTelemetry Primary { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00008AEE File Offset: 0x00006CEE
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00008AF6 File Offset: 0x00006CF6
		[DataMember(Name = "Secondary", EmitDefaultValue = false, Order = 40)]
		internal DataReductionAxisTelemetry Secondary { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00008AFF File Offset: 0x00006CFF
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00008B07 File Offset: 0x00006D07
		[DataMember(Name = "Intersection", EmitDefaultValue = false, Order = 50)]
		internal DataReductionAxisTelemetry Intersection { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00008B10 File Offset: 0x00006D10
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00008B18 File Offset: 0x00006D18
		[DataMember(Name = "Fallback", EmitDefaultValue = false, Order = 60)]
		internal IReadOnlyList<string> FallbackReasons { get; set; }

		// Token: 0x06000165 RID: 357 RVA: 0x00008B21 File Offset: 0x00006D21
		internal DataReductionAxisTelemetry GetOrCreatePrimary()
		{
			if (this.Primary == null)
			{
				this.Primary = new DataReductionAxisTelemetry();
			}
			return this.Primary;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008B3C File Offset: 0x00006D3C
		internal DataReductionAxisTelemetry GetOrCreateSecondary()
		{
			if (this.Secondary == null)
			{
				this.Secondary = new DataReductionAxisTelemetry();
			}
			return this.Secondary;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008B57 File Offset: 0x00006D57
		internal DataReductionAxisTelemetry GetOrCreateIntersection()
		{
			if (this.Intersection == null)
			{
				this.Intersection = new DataReductionAxisTelemetry();
			}
			return this.Intersection;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008B72 File Offset: 0x00006D72
		internal void CaptureInitialState(IntermediateDataReduction reduction, int primaryGroupCount, int secondaryGroupCount)
		{
			this.CaptureState(reduction, primaryGroupCount, secondaryGroupCount, true);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008B7E File Offset: 0x00006D7E
		internal void CaptureFinalState(IntermediateDataReduction reduction, int primaryGroupCount, int secondaryGroupCount)
		{
			this.CaptureState(reduction, primaryGroupCount, secondaryGroupCount, false);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008B8C File Offset: 0x00006D8C
		private void CaptureState(IntermediateDataReduction reduction, int primaryGroupCount, int secondaryGroupCount, bool isInitial)
		{
			if (reduction == null)
			{
				return;
			}
			if (!isInitial)
			{
				this.DataVolume = reduction.DataVolume;
			}
			if (reduction.Primary != null)
			{
				this.GetOrCreatePrimary().AddState(new DataReductionAlgorithmTelemetryState(reduction.Primary, null), isInitial);
			}
			if (reduction.Secondary != null)
			{
				this.GetOrCreateSecondary().AddState(new DataReductionAlgorithmTelemetryState(reduction.Secondary, null), isInitial);
			}
			if (reduction.Intersection != null)
			{
				this.GetOrCreateIntersection().AddState(new DataReductionAlgorithmTelemetryState(reduction.Intersection, null), isInitial);
			}
			List<IntermediateScopedReductionAlgorithm> scoped = reduction.Scoped;
			if (scoped != null)
			{
				for (int i = 0; i < scoped.Count; i++)
				{
					IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm = scoped[i];
					if (intermediateScopedReductionAlgorithm.TelemetryId == null)
					{
						IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm2 = intermediateScopedReductionAlgorithm;
						int nextTelemetryId = this._nextTelemetryId;
						this._nextTelemetryId = nextTelemetryId + 1;
						intermediateScopedReductionAlgorithm2.TelemetryId = new int?(nextTelemetryId);
					}
					IntermediateReductionScope scope = intermediateScopedReductionAlgorithm.Scope;
					if (scope.Primary.IsNullOrEmpty<int>())
					{
						DataReductionAlgorithmTelemetryState dataReductionAlgorithmTelemetryState = DataReductionTelemetry.CreateAlgorithmState(intermediateScopedReductionAlgorithm, scope.Secondary.Count != secondaryGroupCount);
						this.GetOrCreateSecondary().AddState(dataReductionAlgorithmTelemetryState, isInitial);
					}
					else if (scope.Secondary.IsNullOrEmpty<int>())
					{
						DataReductionAlgorithmTelemetryState dataReductionAlgorithmTelemetryState2 = DataReductionTelemetry.CreateAlgorithmState(intermediateScopedReductionAlgorithm, scope.Primary.Count != primaryGroupCount);
						this.GetOrCreatePrimary().AddState(dataReductionAlgorithmTelemetryState2, isInitial);
					}
					else
					{
						DataReductionAlgorithmTelemetryState dataReductionAlgorithmTelemetryState3 = DataReductionTelemetry.CreateAlgorithmState(intermediateScopedReductionAlgorithm, scope.Primary.Count != primaryGroupCount || scope.Secondary.Count != secondaryGroupCount);
						this.GetOrCreateIntersection().AddState(dataReductionAlgorithmTelemetryState3, isInitial);
					}
				}
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008D38 File Offset: 0x00006F38
		private static DataReductionAlgorithmTelemetryState CreateAlgorithmState(IntermediateScopedReductionAlgorithm scopedReduction, bool captureScope)
		{
			if (captureScope)
			{
				return new DataReductionAlgorithmTelemetryState(scopedReduction);
			}
			return new DataReductionAlgorithmTelemetryState(scopedReduction.Algorithm, scopedReduction.TelemetryId);
		}

		// Token: 0x040000A1 RID: 161
		private int _nextTelemetryId;
	}
}
