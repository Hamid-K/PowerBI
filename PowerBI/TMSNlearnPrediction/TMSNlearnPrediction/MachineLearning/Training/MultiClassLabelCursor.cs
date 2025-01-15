using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Training
{
	// Token: 0x02000493 RID: 1171
	public class MultiClassLabelCursor : FeatureFloatVectorCursor
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0008B670 File Offset: 0x00089870
		public long BadLabelCount
		{
			get
			{
				return this._badCount;
			}
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0008B678 File Offset: 0x00089878
		public MultiClassLabelCursor(int classCount, RoleMappedData data, CursOpt opt = CursOpt.Label, IRandom rand = null, params int[] extraCols)
			: this(classCount, TrainingCursorBase.CreateCursor(data, opt, rand, extraCols), data, opt, null)
		{
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0008B690 File Offset: 0x00089890
		protected MultiClassLabelCursor(int classCount, IRowCursor input, RoleMappedData data, CursOpt opt, Action<CursOpt> signal = null)
			: base(input, data, opt, signal)
		{
			this._classCount = classCount;
			if ((opt & CursOpt.Label) != (CursOpt)0U && data.Schema.Label != null)
			{
				this._get = base.Row.GetLabelFloatGetter(data);
				this._keepBad = (opt & CursOpt.AllowBadLabels) != (CursOpt)0U;
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0008B6EC File Offset: 0x000898EC
		protected override CursOpt CursoringCompleteFlags()
		{
			CursOpt cursOpt = base.CursoringCompleteFlags();
			if (this.BadLabelCount == 0L)
			{
				cursOpt |= CursOpt.AllowBadLabels;
			}
			return cursOpt;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0008B714 File Offset: 0x00089914
		public override bool Accept()
		{
			if (this._get != null)
			{
				this._get.Invoke(ref this._raw);
				this.Label = (int)this._raw;
				if (!this._keepBad && ((float)this.Label != this._raw || 0f > this._raw || (this._raw >= (float)this._classCount && this._classCount != 0)))
				{
					this._badCount += 1L;
					return false;
				}
			}
			return base.Accept();
		}

		// Token: 0x04000EB8 RID: 3768
		private readonly int _classCount;

		// Token: 0x04000EB9 RID: 3769
		private readonly ValueGetter<float> _get;

		// Token: 0x04000EBA RID: 3770
		private readonly bool _keepBad;

		// Token: 0x04000EBB RID: 3771
		private long _badCount;

		// Token: 0x04000EBC RID: 3772
		private float _raw;

		// Token: 0x04000EBD RID: 3773
		public int Label;

		// Token: 0x02000494 RID: 1172
		public new sealed class Factory : TrainingCursorBase.FactoryBase<MultiClassLabelCursor>
		{
			// Token: 0x0600186F RID: 6255 RVA: 0x0008B799 File Offset: 0x00089999
			public Factory(int classCount, RoleMappedData data, CursOpt opt = CursOpt.Label)
				: base(data, opt)
			{
				Contracts.CheckParamValue<int>(classCount >= 0, classCount, "classCount", "Must be non-negative");
				this._classCount = classCount;
			}

			// Token: 0x06001870 RID: 6256 RVA: 0x0008B7C1 File Offset: 0x000899C1
			protected override MultiClassLabelCursor CreateCursorCore(IRowCursor input, RoleMappedData data, CursOpt opt, Action<CursOpt> signal)
			{
				return new MultiClassLabelCursor(this._classCount, input, data, opt, signal);
			}

			// Token: 0x04000EBE RID: 3774
			private readonly int _classCount;
		}
	}
}
