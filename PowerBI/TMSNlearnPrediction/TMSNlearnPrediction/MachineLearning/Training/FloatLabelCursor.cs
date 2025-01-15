using System;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Training
{
	// Token: 0x02000491 RID: 1169
	public class FloatLabelCursor : FeatureFloatVectorCursor
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0008B571 File Offset: 0x00089771
		public long BadLabelCount
		{
			get
			{
				return this._badCount;
			}
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0008B579 File Offset: 0x00089779
		public FloatLabelCursor(RoleMappedData data, CursOpt opt = CursOpt.Label, IRandom rand = null, params int[] extraCols)
			: this(TrainingCursorBase.CreateCursor(data, opt, rand, extraCols), data, opt, null)
		{
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0008B590 File Offset: 0x00089790
		protected FloatLabelCursor(IRowCursor input, RoleMappedData data, CursOpt opt, Action<CursOpt> signal = null)
			: base(input, data, opt, signal)
		{
			if ((opt & CursOpt.Label) != (CursOpt)0U && data.Schema.Label != null)
			{
				this._get = base.Row.GetLabelFloatGetter(data);
				this._keepBad = (opt & CursOpt.AllowBadLabels) != (CursOpt)0U;
			}
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0008B5E0 File Offset: 0x000897E0
		protected override CursOpt CursoringCompleteFlags()
		{
			CursOpt cursOpt = base.CursoringCompleteFlags();
			if (this.BadLabelCount == 0L)
			{
				cursOpt |= CursOpt.AllowBadLabels;
			}
			return cursOpt;
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0008B608 File Offset: 0x00089808
		public override bool Accept()
		{
			if (this._get != null)
			{
				this._get.Invoke(ref this.Label);
				if (!this._keepBad && !FloatUtils.IsFinite(this.Label))
				{
					this._badCount += 1L;
					return false;
				}
			}
			return base.Accept();
		}

		// Token: 0x04000EB4 RID: 3764
		private readonly ValueGetter<float> _get;

		// Token: 0x04000EB5 RID: 3765
		private readonly bool _keepBad;

		// Token: 0x04000EB6 RID: 3766
		private long _badCount;

		// Token: 0x04000EB7 RID: 3767
		public float Label;

		// Token: 0x02000492 RID: 1170
		public new sealed class Factory : TrainingCursorBase.FactoryBase<FloatLabelCursor>
		{
			// Token: 0x06001868 RID: 6248 RVA: 0x0008B65A File Offset: 0x0008985A
			public Factory(RoleMappedData data, CursOpt opt = CursOpt.Label)
				: base(data, opt)
			{
			}

			// Token: 0x06001869 RID: 6249 RVA: 0x0008B664 File Offset: 0x00089864
			protected override FloatLabelCursor CreateCursorCore(IRowCursor input, RoleMappedData data, CursOpt opt, Action<CursOpt> signal)
			{
				return new FloatLabelCursor(input, data, opt, signal);
			}
		}
	}
}
