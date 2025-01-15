using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000030 RID: 48
	internal sealed class DataMember : Scope
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00005190 File Offset: 0x00003390
		internal DataMember(string id, IList<DataMember> dataMembers, IList<Calculation> calculations, Group group, IList<DataIntersection> intersections, DataBinding dataBinding, MatchCondition matchCondition, StartPosition startPosition, RestartKindDefinition restartKindDefinition, DiscardCondition discardCondition)
			: base(id)
		{
			this._dataMembers = dataMembers;
			this._calculations = calculations;
			this._group = group;
			this._intersections = intersections;
			this._dataBinding = dataBinding;
			this._matchCondition = matchCondition;
			this._discardCondition = discardCondition;
			this._startPosition = startPosition;
			this._restartKindDefinition = restartKindDefinition;
			this._cellScopeIndex = -1;
			this._restartIndex = -1;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000051F8 File Offset: 0x000033F8
		internal IList<DataMember> DataMembers
		{
			get
			{
				return this._dataMembers;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00005200 File Offset: 0x00003400
		internal IList<Calculation> Calculations
		{
			get
			{
				return this._calculations;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00005208 File Offset: 0x00003408
		internal Group Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005210 File Offset: 0x00003410
		internal IList<DataIntersection> Intersections
		{
			get
			{
				return this._intersections;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00005218 File Offset: 0x00003418
		internal DataBinding DataBinding
		{
			get
			{
				return this._dataBinding;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005220 File Offset: 0x00003420
		internal MatchCondition MatchCondition
		{
			get
			{
				return this._matchCondition;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005228 File Offset: 0x00003428
		internal DiscardCondition DiscardCondition
		{
			get
			{
				return this._discardCondition;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005230 File Offset: 0x00003430
		internal StartPosition StartPosition
		{
			get
			{
				return this._startPosition;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005238 File Offset: 0x00003438
		internal RestartKindDefinition RestartKindDefinition
		{
			get
			{
				return this._restartKindDefinition;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005240 File Offset: 0x00003440
		internal bool IsLeaf
		{
			get
			{
				return this._dataMembers.IsNullOrEmpty<DataMember>();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000524D File Offset: 0x0000344D
		internal bool IsDynamic
		{
			get
			{
				return this._group != null;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005258 File Offset: 0x00003458
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005260 File Offset: 0x00003460
		public int CellScopeIndex
		{
			get
			{
				return this._cellScopeIndex;
			}
			set
			{
				this._cellScopeIndex = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005269 File Offset: 0x00003469
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005271 File Offset: 0x00003471
		public int RestartIndex
		{
			get
			{
				return this._restartIndex;
			}
			set
			{
				this._restartIndex = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000527A File Offset: 0x0000347A
		public bool HasRestartIndex
		{
			get
			{
				return this._restartIndex >= 0;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005288 File Offset: 0x00003488
		public bool HasStartPosition
		{
			get
			{
				return this._startPosition != null && this._restartIndex > -1;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000052A0 File Offset: 0x000034A0
		internal bool IsCountedForDataWindow
		{
			get
			{
				if (this._isCountedForDataWindow == null)
				{
					if (!this._dataMembers.IsNullOrEmpty<DataMember>() && this.HasAnyDescendantCountedForDataWindow)
					{
						this._isCountedForDataWindow = new bool?(false);
						return false;
					}
					this._isCountedForDataWindow = new bool?(this.IsDynamic || this._matchCondition != null);
				}
				return this._isCountedForDataWindow.Value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005308 File Offset: 0x00003508
		internal bool HasAnyDescendantCountedForDataWindow
		{
			get
			{
				if (this._hasAnyDescendantCountedForDataWindow == null)
				{
					this._hasAnyDescendantCountedForDataWindow = new bool?(false);
					if (!this._dataMembers.IsNullOrEmpty<DataMember>())
					{
						foreach (DataMember dataMember in this._dataMembers)
						{
							if (dataMember.IsCountedForDataWindow || dataMember.HasAnyDescendantCountedForDataWindow)
							{
								this._hasAnyDescendantCountedForDataWindow = new bool?(true);
								return true;
							}
						}
					}
				}
				return this._hasAnyDescendantCountedForDataWindow.Value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000053A4 File Offset: 0x000035A4
		internal bool HasAnyDescendantWithStartPosition
		{
			get
			{
				if (this._hasAnyDescendantWithStartPosition == null)
				{
					this._hasAnyDescendantWithStartPosition = new bool?(false);
					if (!this._dataMembers.IsNullOrEmpty<DataMember>())
					{
						foreach (DataMember dataMember in this._dataMembers)
						{
							if (dataMember.HasStartPosition || dataMember.HasAnyDescendantWithStartPosition)
							{
								this._hasAnyDescendantWithStartPosition = new bool?(true);
								return true;
							}
						}
					}
				}
				return this._hasAnyDescendantWithStartPosition.Value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005440 File Offset: 0x00003640
		internal bool HasAllEmptyIntersections
		{
			get
			{
				if (this._hasAllEmptyIntersections == null)
				{
					if (!this._intersections.IsNullOrEmpty<DataIntersection>())
					{
						this._hasAllEmptyIntersections = new bool?(this._intersections.All((DataIntersection inters) => inters.IsEmpty));
					}
					else
					{
						this._hasAllEmptyIntersections = new bool?(false);
					}
				}
				return this._hasAllEmptyIntersections.Value;
			}
		}

		// Token: 0x040000C9 RID: 201
		private readonly IList<DataMember> _dataMembers;

		// Token: 0x040000CA RID: 202
		private readonly IList<Calculation> _calculations;

		// Token: 0x040000CB RID: 203
		private readonly Group _group;

		// Token: 0x040000CC RID: 204
		private readonly IList<DataIntersection> _intersections;

		// Token: 0x040000CD RID: 205
		private readonly DataBinding _dataBinding;

		// Token: 0x040000CE RID: 206
		private readonly MatchCondition _matchCondition;

		// Token: 0x040000CF RID: 207
		private readonly DiscardCondition _discardCondition;

		// Token: 0x040000D0 RID: 208
		private readonly StartPosition _startPosition;

		// Token: 0x040000D1 RID: 209
		private readonly RestartKindDefinition _restartKindDefinition;

		// Token: 0x040000D2 RID: 210
		private int _cellScopeIndex;

		// Token: 0x040000D3 RID: 211
		private int _restartIndex;

		// Token: 0x040000D4 RID: 212
		private bool? _hasAllEmptyIntersections;

		// Token: 0x040000D5 RID: 213
		private bool? _hasAnyDescendantCountedForDataWindow;

		// Token: 0x040000D6 RID: 214
		private bool? _isCountedForDataWindow;

		// Token: 0x040000D7 RID: 215
		private bool? _hasAnyDescendantWithStartPosition;
	}
}
