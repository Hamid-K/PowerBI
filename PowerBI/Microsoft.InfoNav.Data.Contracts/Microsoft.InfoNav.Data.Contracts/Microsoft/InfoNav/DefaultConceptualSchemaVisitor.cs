using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000032 RID: 50
	public abstract class DefaultConceptualSchemaVisitor : ConceptualSchemaVisitor
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000029A7 File Offset: 0x00000BA7
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000029AF File Offset: 0x00000BAF
		protected bool ContinueVisiting { get; set; } = true;

		// Token: 0x060000B0 RID: 176 RVA: 0x000029B8 File Offset: 0x00000BB8
		public override void Visit(IConceptualSchema schema)
		{
			if (!this.ContinueVisiting)
			{
				return;
			}
			foreach (IConceptualEntity conceptualEntity in schema.Entities)
			{
				if (!this.ContinueVisiting)
				{
					break;
				}
				this.Visit(conceptualEntity);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002A18 File Offset: 0x00000C18
		public override void Visit(IConceptualEntity entity)
		{
			this.VisitDisplayItem(entity);
			foreach (IConceptualProperty conceptualProperty in entity.Properties)
			{
				if (!this.ContinueVisiting)
				{
					return;
				}
				this.Visit(conceptualProperty);
			}
			foreach (IConceptualHierarchy conceptualHierarchy in entity.Hierarchies)
			{
				if (!this.ContinueVisiting)
				{
					break;
				}
				this.Visit(conceptualHierarchy);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002ABC File Offset: 0x00000CBC
		public override void Visit(IConceptualProperty property)
		{
			if (this.ContinueVisiting)
			{
				this.VisitDisplayItem(property);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public override void Visit(IConceptualHierarchy hierarchy)
		{
			this.VisitDisplayItem(hierarchy);
			foreach (IConceptualHierarchyLevel conceptualHierarchyLevel in hierarchy.Levels)
			{
				if (!this.ContinueVisiting)
				{
					break;
				}
				this.Visit(conceptualHierarchyLevel);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002B30 File Offset: 0x00000D30
		public override void Visit(IConceptualHierarchyLevel level)
		{
			if (this.ContinueVisiting)
			{
				this.VisitDisplayItem(level);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002B41 File Offset: 0x00000D41
		public virtual void VisitDisplayItem(IConceptualDisplayItem displayItem)
		{
		}
	}
}
