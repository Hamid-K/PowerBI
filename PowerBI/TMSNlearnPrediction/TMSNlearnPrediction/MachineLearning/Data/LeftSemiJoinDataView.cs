using System;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000108 RID: 264
	public sealed class LeftSemiJoinDataView : LeftJoinDataViewBase
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001D13D File Offset: 0x0001B33D
		public override ISchema Schema
		{
			get
			{
				return this._leftDv.Schema;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001D14A File Offset: 0x0001B34A
		public LeftSemiJoinDataView(IHostEnvironment env, IDataView left, IDataView right, LeftJoinDataViewBase.JoinKeyColumn[] jointKeyColumns)
			: base(env, "LeftSemiJoinDataView", left, right, jointKeyColumns, false)
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001D15D File Offset: 0x0001B35D
		protected override IRowCursor GetRowCursorFromLeftCursor(IRowCursor leftCursor, Func<int, bool> predicate)
		{
			return new LeftSemiJoinDataView.SemiJoinRowCursor(this, leftCursor, predicate);
		}

		// Token: 0x0400029F RID: 671
		public const string RegistrationName = "LeftSemiJoinDataView";

		// Token: 0x02000109 RID: 265
		private sealed class SemiJoinRowCursor : LinkedRowFilterCursorBase
		{
			// Token: 0x06000560 RID: 1376 RVA: 0x0001D178 File Offset: 0x0001B378
			public SemiJoinRowCursor(LeftSemiJoinDataView parent, IRowCursor leftCursor, Func<int, bool> predicate)
				: base(parent._host, leftCursor, leftCursor.Schema, Utils.BuildArray<bool>(leftCursor.Schema.ColumnCount, predicate))
			{
				this._parent = parent;
				this._rightRow = this._parent._rightDv.GetSeeker((int x) => this._parent._isRightColumnKey[x]);
				this._kc = LeftJoinDataViewBase.KeyComparer.Create(base.Input, this._rightRow, this._parent._leftKeyIndices, this._parent._rightKeyIndices);
				this._irowRightLim = this._parent._nextSameKeys.Length;
			}

			// Token: 0x06000561 RID: 1377 RVA: 0x0001D21C File Offset: 0x0001B41C
			protected override bool Accept()
			{
				uint hash = this._kc.GetHash1();
				int num;
				if (!this._parent._hashToHead.TryGetValue(hash, out num))
				{
					return false;
				}
				for (;;)
				{
					this._rightRow.MoveTo((long)num);
					if (this._kc.Same())
					{
						break;
					}
					num = this._parent._nextSameHash[num];
					if (num < 0)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000562 RID: 1378 RVA: 0x0001D27C File Offset: 0x0001B47C
			public override void Dispose()
			{
				this._rightRow.Dispose();
				base.Dispose();
			}

			// Token: 0x040002A0 RID: 672
			private readonly LeftSemiJoinDataView _parent;

			// Token: 0x040002A1 RID: 673
			private readonly IRowSeeker _rightRow;

			// Token: 0x040002A2 RID: 674
			private readonly int _irowRightLim;

			// Token: 0x040002A3 RID: 675
			private readonly LeftJoinDataViewBase.KeyComparer _kc;
		}
	}
}
