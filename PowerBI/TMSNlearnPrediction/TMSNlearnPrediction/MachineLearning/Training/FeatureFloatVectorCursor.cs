using System;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Training
{
	// Token: 0x0200048F RID: 1167
	public class FeatureFloatVectorCursor : StandardScalarCursor
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x0008B45F File Offset: 0x0008965F
		public long BadFeaturesRowCount
		{
			get
			{
				return this._badCount;
			}
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0008B467 File Offset: 0x00089667
		public FeatureFloatVectorCursor(RoleMappedData data, CursOpt opt = CursOpt.Features, IRandom rand = null, params int[] extraCols)
			: this(TrainingCursorBase.CreateCursor(data, opt, rand, extraCols), data, opt, null)
		{
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0008B47C File Offset: 0x0008967C
		protected FeatureFloatVectorCursor(IRowCursor input, RoleMappedData data, CursOpt opt, Action<CursOpt> signal = null)
			: base(input, data, opt, signal)
		{
			if ((opt & CursOpt.Features) != (CursOpt)0U && data.Schema.Feature != null)
			{
				this._get = base.Row.GetFeatureFloatVectorGetter(data);
				this._keepBad = (opt & CursOpt.AllowBadFeatures) != (CursOpt)0U;
			}
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0008B4CC File Offset: 0x000896CC
		protected override CursOpt CursoringCompleteFlags()
		{
			CursOpt cursOpt = base.CursoringCompleteFlags();
			if (this.BadFeaturesRowCount == 0L)
			{
				cursOpt |= CursOpt.AllowBadFeatures;
			}
			return cursOpt;
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x0008B4F4 File Offset: 0x000896F4
		public override bool Accept()
		{
			if (!base.Accept())
			{
				return false;
			}
			if (this._get != null)
			{
				this._get.Invoke(ref this.Features);
				if (!this._keepBad && !FloatUtils.IsFinite(this.Features.Values, this.Features.Count))
				{
					this._badCount += 1L;
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000EB0 RID: 3760
		private readonly ValueGetter<VBuffer<float>> _get;

		// Token: 0x04000EB1 RID: 3761
		private readonly bool _keepBad;

		// Token: 0x04000EB2 RID: 3762
		private long _badCount;

		// Token: 0x04000EB3 RID: 3763
		public VBuffer<float> Features;

		// Token: 0x02000490 RID: 1168
		public new sealed class Factory : TrainingCursorBase.FactoryBase<FeatureFloatVectorCursor>
		{
			// Token: 0x06001861 RID: 6241 RVA: 0x0008B55B File Offset: 0x0008975B
			public Factory(RoleMappedData data, CursOpt opt = CursOpt.Features)
				: base(data, opt)
			{
			}

			// Token: 0x06001862 RID: 6242 RVA: 0x0008B565 File Offset: 0x00089765
			protected override FeatureFloatVectorCursor CreateCursorCore(IRowCursor input, RoleMappedData data, CursOpt opt, Action<CursOpt> signal)
			{
				return new FeatureFloatVectorCursor(input, data, opt, signal);
			}
		}
	}
}
