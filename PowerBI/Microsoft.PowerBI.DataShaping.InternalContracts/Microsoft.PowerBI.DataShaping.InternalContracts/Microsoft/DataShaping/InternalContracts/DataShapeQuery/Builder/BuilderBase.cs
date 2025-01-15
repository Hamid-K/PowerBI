using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x02000102 RID: 258
	internal abstract class BuilderBase<TType>
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		protected BuilderBase(TType activeObject)
		{
			this.ActiveObject = activeObject;
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0000F1C3 File Offset: 0x0000D3C3
		protected TType ActiveObject { get; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000F1CB File Offset: 0x0000D3CB
		public TType Result
		{
			get
			{
				return this.ActiveObject;
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		protected List<Calculation> AddCalculation(List<Calculation> calculations, string identifier, Expression value, bool suppressJoinPredicate = false, bool? respectInstanceFilters = null, string nativeReferenceName = null, bool isContextOnly = false)
		{
			object obj = calculations ?? new List<Calculation>();
			Calculation calculation = new Calculation
			{
				Id = new Identifier(identifier),
				Value = value,
				SuppressJoinPredicate = Candidate<bool>.Valid(suppressJoinPredicate),
				RespectInstanceFilters = respectInstanceFilters,
				NativeReferenceName = nativeReferenceName,
				IsContextOnly = isContextOnly
			};
			object obj2 = obj;
			obj2.Add(calculation);
			return obj2;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0000F230 File Offset: 0x0000D430
		protected List<Message> AddMessage(List<Message> messages, string code, string severity, string text)
		{
			object obj = messages ?? new List<Message>();
			Message message = new Message
			{
				Code = code,
				Severity = severity,
				Text = text
			};
			object obj2 = obj;
			obj2.Add(message);
			return obj2;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000F26C File Offset: 0x0000D46C
		protected DataShapeBuilder<T> AddDataShape<T>(T parent, List<DataShape> dataShapes, string identifier, string dataSourceId, bool filterEmptyGroups, Candidate<bool> contextOnly, bool independent, DataShapeUsage usage)
		{
			DataShape dataShape = BuilderBase<TType>.CreateDataShape(identifier, dataSourceId, filterEmptyGroups, contextOnly, independent, usage);
			return this.AddDataShape<T>(parent, dataShapes, dataShape);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0000F294 File Offset: 0x0000D494
		protected static DataShape CreateDataShape(string identifier, string dataSourceId, bool filterEmptyGroups, Candidate<bool> contextOnly, bool independent = false, DataShapeUsage usage = DataShapeUsage.Query)
		{
			DataShape dataShape = new DataShape
			{
				Id = new Identifier(identifier),
				DataSourceId = ((dataSourceId == null) ? null : new Identifier(dataSourceId)),
				ContextOnly = contextOnly,
				IsIndependent = independent,
				Usage = usage
			};
			if (filterEmptyGroups)
			{
				dataShape.Filters = new List<Filter>();
				dataShape.Filters.Add(new Filter
				{
					Condition = new FilterEmptyGroupsCondition(),
					Target = dataShape.Id.StructureReference()
				});
			}
			return dataShape;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000F31C File Offset: 0x0000D51C
		protected DataShapeBuilder<T> AddDataShape<T>(T parent, List<DataShape> dataShapes, DataShape dataShape)
		{
			dataShapes.Add(dataShape);
			return new DataShapeBuilder<T>(parent, dataShape);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000F32C File Offset: 0x0000D52C
		protected Expression Clone(Expression expr)
		{
			return new Expression(expr.OriginalNode);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0000F33C File Offset: 0x0000D53C
		private void AssertObjectType(Type type)
		{
			TType activeObject = this.ActiveObject;
			Type type2 = activeObject.GetType();
			if (type == type2 || type.IsAssignableFrom(type2))
			{
				return;
			}
			Contract.RetailFail(string.Concat(new string[] { "Active object has wrong type. Expected one of '", type.FullName, "' but found '", type2.FullName, "'." }));
		}
	}
}
