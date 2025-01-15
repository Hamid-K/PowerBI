using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Training
{
	// Token: 0x0200048D RID: 1165
	public class StandardScalarCursor : TrainingCursorBase
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x0008B298 File Offset: 0x00089498
		public long BadWeightCount
		{
			get
			{
				return this._badWeightCount;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x0008B2A0 File Offset: 0x000894A0
		public long BadGroupCount
		{
			get
			{
				return this._badGroupCount;
			}
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0008B2A8 File Offset: 0x000894A8
		public StandardScalarCursor(RoleMappedData data, CursOpt opt, IRandom rand = null, params int[] extraCols)
			: this(TrainingCursorBase.CreateCursor(data, opt, rand, extraCols), data, opt, null)
		{
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0008B2C0 File Offset: 0x000894C0
		protected StandardScalarCursor(IRowCursor input, RoleMappedData data, CursOpt opt, Action<CursOpt> signal = null)
			: base(input, signal)
		{
			if ((opt & CursOpt.Weight) != (CursOpt)0U)
			{
				this._getWeight = base.Row.GetOptWeightFloatGetter(data);
				this._keepBadWeight = (opt & CursOpt.AllowBadWeights) != (CursOpt)0U;
			}
			if ((opt & CursOpt.Group) != (CursOpt)0U)
			{
				this._getGroup = base.Row.GetOptGroupGetter(data);
				this._keepBadGroup = (opt & CursOpt.AllowBadGroups) != (CursOpt)0U;
			}
			if ((opt & CursOpt.Id) != (CursOpt)0U)
			{
				this._getId = base.Row.GetIdGetter();
			}
			this.Weight = 1f;
			this.Group = 0UL;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0008B354 File Offset: 0x00089554
		protected override CursOpt CursoringCompleteFlags()
		{
			CursOpt cursOpt = base.CursoringCompleteFlags();
			if (this.BadWeightCount == 0L)
			{
				cursOpt |= CursOpt.AllowBadWeights;
			}
			if (this.BadGroupCount == 0L)
			{
				cursOpt |= CursOpt.AllowBadGroups;
			}
			return cursOpt;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0008B390 File Offset: 0x00089590
		public override bool Accept()
		{
			if (!base.Accept())
			{
				return false;
			}
			if (this._getWeight != null)
			{
				this._getWeight.Invoke(ref this.Weight);
				if (!this._keepBadWeight && (0f >= this.Weight || this.Weight >= float.PositiveInfinity))
				{
					this._badWeightCount += 1L;
					return false;
				}
			}
			if (this._getGroup != null)
			{
				this._getGroup.Invoke(ref this.Group);
				if (!this._keepBadGroup && this.Group == 0UL)
				{
					this._badGroupCount += 1L;
					return false;
				}
			}
			if (this._getId != null)
			{
				this._getId.Invoke(ref this.Id);
			}
			return true;
		}

		// Token: 0x04000EA6 RID: 3750
		private readonly ValueGetter<float> _getWeight;

		// Token: 0x04000EA7 RID: 3751
		private readonly ValueGetter<ulong> _getGroup;

		// Token: 0x04000EA8 RID: 3752
		private readonly ValueGetter<UInt128> _getId;

		// Token: 0x04000EA9 RID: 3753
		private readonly bool _keepBadWeight;

		// Token: 0x04000EAA RID: 3754
		private readonly bool _keepBadGroup;

		// Token: 0x04000EAB RID: 3755
		private long _badWeightCount;

		// Token: 0x04000EAC RID: 3756
		private long _badGroupCount;

		// Token: 0x04000EAD RID: 3757
		public float Weight;

		// Token: 0x04000EAE RID: 3758
		public ulong Group;

		// Token: 0x04000EAF RID: 3759
		public UInt128 Id;

		// Token: 0x0200048E RID: 1166
		public sealed class Factory : TrainingCursorBase.FactoryBase<StandardScalarCursor>
		{
			// Token: 0x0600185A RID: 6234 RVA: 0x0008B449 File Offset: 0x00089649
			public Factory(RoleMappedData data, CursOpt opt)
				: base(data, opt)
			{
			}

			// Token: 0x0600185B RID: 6235 RVA: 0x0008B453 File Offset: 0x00089653
			protected override StandardScalarCursor CreateCursorCore(IRowCursor input, RoleMappedData data, CursOpt opt, Action<CursOpt> signal)
			{
				return new StandardScalarCursor(input, data, opt, signal);
			}
		}
	}
}
