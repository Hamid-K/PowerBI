using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.Correlation;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000034 RID: 52
	internal sealed class DataShape : Scope
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00005608 File Offset: 0x00003808
		internal DataShape(string id, IList<Calculation> calculations, IList<DataMember> secondaryHierarchy, IList<DataMember> primaryHierarchy, IList<DataShape> dataShapes, DataBinding dataBinding, DataWindow dataWindow, DataLimits dataLimits, FieldValueExpressionNode correlationExpression, IList<Message> messages, bool hasReusableSecondary, IList<IList<ExpressionNode>> restartDefinitions, IList<int> segmentationTableIndices, DataWindows dataWindows, CorrelationMode correlationMode, IList<int> restartIndicesWithStartPosition)
			: base(id)
		{
			this._calculations = calculations;
			this._secondaryHierarchy = secondaryHierarchy;
			this._primaryHierarchy = primaryHierarchy;
			this._dataShapes = dataShapes;
			this._dataBinding = dataBinding;
			this._dataWindow = dataWindow;
			this._dataLimits = dataLimits;
			this._correlationExpression = correlationExpression;
			this._messages = messages;
			this._hasReusableSecondary = hasReusableSecondary;
			this._restartDefinitions = restartDefinitions;
			this._segmentationTableIndices = segmentationTableIndices;
			this._dataWindows = dataWindows;
			this._correlationMode = correlationMode;
			this._restartIndicesWithStartPosition = restartIndicesWithStartPosition;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005692 File Offset: 0x00003892
		internal IList<Calculation> Calculations
		{
			get
			{
				return this._calculations;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000569A File Offset: 0x0000389A
		internal IList<DataMember> SecondaryHierarchy
		{
			get
			{
				return this._secondaryHierarchy;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000056A2 File Offset: 0x000038A2
		internal IList<DataMember> PrimaryHierarchy
		{
			get
			{
				return this._primaryHierarchy;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000056AA File Offset: 0x000038AA
		internal IList<DataShape> DataShapes
		{
			get
			{
				return this._dataShapes;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000056B2 File Offset: 0x000038B2
		internal DataBinding DataBinding
		{
			get
			{
				return this._dataBinding;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000056BA File Offset: 0x000038BA
		internal DataWindow DataWindow
		{
			get
			{
				return this._dataWindow;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000056C2 File Offset: 0x000038C2
		internal DataLimits DataLimits
		{
			get
			{
				return this._dataLimits;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000056CA File Offset: 0x000038CA
		internal FieldValueExpressionNode CorrelationExpression
		{
			get
			{
				return this._correlationExpression;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000056D2 File Offset: 0x000038D2
		internal IList<Message> Messages
		{
			get
			{
				return this._messages;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000056DA File Offset: 0x000038DA
		internal bool HasReusableSecondary
		{
			get
			{
				return this._hasReusableSecondary;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000056E2 File Offset: 0x000038E2
		internal IList<IList<ExpressionNode>> RestartDefinitions
		{
			get
			{
				return this._restartDefinitions;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000056EA File Offset: 0x000038EA
		public IList<int> SegmentationTableIndices
		{
			get
			{
				return this._segmentationTableIndices;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000056F2 File Offset: 0x000038F2
		public IList<int> RestartIndicesWithStartPosition
		{
			get
			{
				return this._restartIndicesWithStartPosition;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000056FA File Offset: 0x000038FA
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00005702 File Offset: 0x00003902
		internal CellScopeToIntersectionRangeMapping CellScopeToIntersectionRangeMapping
		{
			get
			{
				return this._cellScopeToIntersectionRangeMapping;
			}
			set
			{
				this._cellScopeToIntersectionRangeMapping = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000570C File Offset: 0x0000390C
		internal bool HasTopLevelPrimaryHierarchyGroup
		{
			get
			{
				if (this._hasTopLevelPrimaryHierarchyGroup == null)
				{
					if (!this._primaryHierarchy.IsNullOrEmpty<DataMember>())
					{
						this._hasTopLevelPrimaryHierarchyGroup = new bool?(this._primaryHierarchy.Any((DataMember m) => m.IsDynamic));
					}
					else
					{
						this._hasTopLevelPrimaryHierarchyGroup = new bool?(false);
					}
				}
				return this._hasTopLevelPrimaryHierarchyGroup.Value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005781 File Offset: 0x00003981
		internal CorrelationMode CorrelationMode
		{
			get
			{
				return this._correlationMode;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00005789 File Offset: 0x00003989
		internal DataWindows DataWindows
		{
			get
			{
				return this._dataWindows;
			}
		}

		// Token: 0x040000E6 RID: 230
		private readonly IList<Calculation> _calculations;

		// Token: 0x040000E7 RID: 231
		private readonly IList<DataMember> _secondaryHierarchy;

		// Token: 0x040000E8 RID: 232
		private readonly IList<DataMember> _primaryHierarchy;

		// Token: 0x040000E9 RID: 233
		private readonly IList<DataShape> _dataShapes;

		// Token: 0x040000EA RID: 234
		private readonly DataBinding _dataBinding;

		// Token: 0x040000EB RID: 235
		private readonly DataWindow _dataWindow;

		// Token: 0x040000EC RID: 236
		private readonly DataLimits _dataLimits;

		// Token: 0x040000ED RID: 237
		private readonly FieldValueExpressionNode _correlationExpression;

		// Token: 0x040000EE RID: 238
		private readonly IList<Message> _messages;

		// Token: 0x040000EF RID: 239
		private readonly bool _hasReusableSecondary;

		// Token: 0x040000F0 RID: 240
		private readonly IList<IList<ExpressionNode>> _restartDefinitions;

		// Token: 0x040000F1 RID: 241
		private readonly IList<int> _segmentationTableIndices;

		// Token: 0x040000F2 RID: 242
		private readonly CorrelationMode _correlationMode;

		// Token: 0x040000F3 RID: 243
		private readonly IList<int> _restartIndicesWithStartPosition;

		// Token: 0x040000F4 RID: 244
		private readonly DataWindows _dataWindows;

		// Token: 0x040000F5 RID: 245
		private CellScopeToIntersectionRangeMapping _cellScopeToIntersectionRangeMapping;

		// Token: 0x040000F6 RID: 246
		private bool? _hasTopLevelPrimaryHierarchyGroup;
	}
}
