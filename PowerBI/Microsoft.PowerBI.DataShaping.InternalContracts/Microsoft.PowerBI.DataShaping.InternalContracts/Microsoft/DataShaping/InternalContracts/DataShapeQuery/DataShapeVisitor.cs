using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000077 RID: 119
	internal abstract class DataShapeVisitor
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00006387 File Offset: 0x00004587
		protected virtual void Enter(DataShape dataShape)
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00006389 File Offset: 0x00004589
		protected virtual void Exit(DataShape dataShape)
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000638B File Offset: 0x0000458B
		protected virtual void Enter(DataMember dataMember)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000638D File Offset: 0x0000458D
		protected virtual void Exit(DataMember dataMember)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000638F File Offset: 0x0000458F
		protected virtual void Enter(DataIntersection dataIntersection)
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00006391 File Offset: 0x00004591
		protected virtual void Exit(DataIntersection dataIntersection)
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00006393 File Offset: 0x00004593
		protected virtual void Visit(Calculation calculation)
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00006395 File Offset: 0x00004595
		protected virtual void Enter(DataHierarchy dataHierarchy)
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00006397 File Offset: 0x00004597
		protected virtual void Exit(DataHierarchy dataHierarchy)
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00006399 File Offset: 0x00004599
		protected virtual void Enter(DataRow dataRow)
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000639B File Offset: 0x0000459B
		protected virtual void Exit(DataRow dataRow)
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000063A0 File Offset: 0x000045A0
		protected virtual void Visit(DataShape dataShape)
		{
			this.Enter(dataShape);
			this.Visit(dataShape.ExtensionSchema);
			this.Visit<DataShape>(dataShape.DataShapes, new Action<DataShape>(this.Visit));
			this.Visit<Calculation>(dataShape.Calculations, new Action<Calculation>(this.Visit));
			this.TraverseDataShapeStructure(dataShape);
			this.Visit(dataShape.Filters, dataShape.Id);
			this.Visit<Limit, DataShape>(dataShape.Limits, dataShape, new Action<Limit, DataShape>(this.Visit));
			this.Visit<DataTransform>(dataShape.Transforms, new Action<DataTransform>(this.Visit));
			this.Visit<ModelParameter>(dataShape.ModelParameters, new Action<ModelParameter>(this.Visit));
			this.Visit(dataShape.VisualCalculationMetadata, dataShape.Id);
			this.Exit(dataShape);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00006470 File Offset: 0x00004670
		protected void Visit(List<VisualAxis> visualAxes, Identifier dataShapeId)
		{
			if (visualAxes == null)
			{
				return;
			}
			foreach (VisualAxis visualAxis in visualAxes)
			{
				this.Visit(visualAxis, dataShapeId);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000064C4 File Offset: 0x000046C4
		protected virtual void Visit(VisualAxis visualAxes, Identifier dataShapeId)
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000064C6 File Offset: 0x000046C6
		protected virtual void Visit(ModelParameter modelParameter)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000064C8 File Offset: 0x000046C8
		protected virtual void TraverseDataShapeStructure(DataShape dataShape)
		{
			if (dataShape == null)
			{
				return;
			}
			this.Visit(dataShape.PrimaryHierarchy);
			this.Visit(dataShape.SecondaryHierarchy);
			this.Visit<DataRow>(dataShape.DataRows, new Action<DataRow>(this.Visit));
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00006500 File Offset: 0x00004700
		protected virtual void Visit(DataMember dataMember)
		{
			this.Enter(dataMember);
			this.Visit<DataShape>(dataMember.DataShapes, new Action<DataShape>(this.Visit));
			this.Visit<Calculation>(dataMember.Calculations, new Action<Calculation>(this.Visit));
			this.TraverseDataMemberStructure(dataMember);
			this.Exit(dataMember);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00006554 File Offset: 0x00004754
		protected virtual void TraverseDataMemberStructure(DataMember dataMember)
		{
			this.Visit<DataMember>(dataMember.DataMembers, new Action<DataMember>(this.Visit));
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00006570 File Offset: 0x00004770
		protected virtual void Visit(DataIntersection dataIntersection)
		{
			this.Enter(dataIntersection);
			this.Visit<DataShape>(dataIntersection.DataShapes, new Action<DataShape>(this.Visit));
			this.Visit<Calculation>(dataIntersection.Calculations, new Action<Calculation>(this.Visit));
			this.Exit(dataIntersection);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000065BD File Offset: 0x000047BD
		protected void Visit(DataHierarchy dataHierarchy)
		{
			if (dataHierarchy == null)
			{
				return;
			}
			this.Enter(dataHierarchy);
			this.Visit<DataMember>(dataHierarchy.DataMembers, new Action<DataMember>(this.Visit));
			this.Exit(dataHierarchy);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000065EA File Offset: 0x000047EA
		protected void Visit(DataRow dataRow)
		{
			if (dataRow == null)
			{
				return;
			}
			this.Enter(dataRow);
			this.Visit<DataIntersection>(dataRow.Intersections, new Action<DataIntersection>(this.Visit));
			this.Exit(dataRow);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00006618 File Offset: 0x00004818
		protected void Visit<T>(IEnumerable<T> collection, Action<T> visitAction)
		{
			if (collection == null)
			{
				return;
			}
			foreach (T t in collection)
			{
				visitAction(t);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00006664 File Offset: 0x00004864
		protected void Visit(List<Filter> filters, Identifier dataShapeId)
		{
			if (filters == null)
			{
				return;
			}
			foreach (Filter filter in filters)
			{
				this.Visit(filter, dataShapeId);
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000066B8 File Offset: 0x000048B8
		protected virtual void Visit(Filter filter, Identifier dataShapeId)
		{
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000066BC File Offset: 0x000048BC
		protected void Visit<T, Identifier>(List<T> limits, Identifier dataShapeId, Action<T, Identifier> visitAction)
		{
			if (limits == null)
			{
				return;
			}
			foreach (T t in limits)
			{
				visitAction(t, dataShapeId);
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00006710 File Offset: 0x00004910
		protected virtual void Visit(Limit limit, DataShape dataShape)
		{
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00006712 File Offset: 0x00004912
		protected virtual void Visit(DataTransform transform)
		{
			this.Visit(transform.Input);
			this.Visit(transform.Output);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000672C File Offset: 0x0000492C
		protected virtual void Visit(DataTransformInput input)
		{
			if (input == null)
			{
				return;
			}
			this.Visit(input.Table);
			this.Visit<DataTransformParameter>(input.Parameters, new Action<DataTransformParameter>(this.Visit));
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00006757 File Offset: 0x00004957
		protected virtual void Visit(DataTransformParameter param)
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000675B File Offset: 0x0000495B
		protected virtual void Visit(DataTransformOutput output)
		{
			if (output == null)
			{
				return;
			}
			this.Visit(output.Table);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000676D File Offset: 0x0000496D
		protected virtual void Visit(DataTransformTable table)
		{
			if (table == null)
			{
				return;
			}
			this.Visit<DataTransformTableColumn>(table.Columns, new Action<DataTransformTableColumn>(this.Visit));
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000678C File Offset: 0x0000498C
		protected virtual void Visit(DataTransformTableColumn column)
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00006790 File Offset: 0x00004990
		protected virtual void Visit(ExtensionSchema schema)
		{
			if (schema == null)
			{
				return;
			}
			List<ExtensionEntity> entities = schema.Entities;
			for (int i = 0; i < entities.Count; i++)
			{
				this.VisitExtensionEntity(entities[i]);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000067C6 File Offset: 0x000049C6
		protected virtual void VisitExtensionEntity(ExtensionEntity extensionEntity)
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000067C8 File Offset: 0x000049C8
		protected void TraverseExtensionEntityContents(ExtensionEntity extensionEntity)
		{
			List<ExtensionMeasure> measures = extensionEntity.Measures;
			for (int i = 0; i < measures.Count; i++)
			{
				this.VisitExtensionMeasure(measures[i]);
			}
			List<ExtensionColumn> columns = extensionEntity.Columns;
			for (int j = 0; j < columns.Count; j++)
			{
				this.VisitExtensionColumn(columns[j]);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000681F File Offset: 0x00004A1F
		protected virtual void VisitExtensionColumn(ExtensionColumn extensionColumn)
		{
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00006821 File Offset: 0x00004A21
		protected virtual void VisitExtensionMeasure(ExtensionMeasure extensionMeasure)
		{
		}
	}
}
