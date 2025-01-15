using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200006E RID: 110
	internal abstract class ModelLinguisticSchemaVisitor
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00004AD5 File Offset: 0x00002CD5
		protected virtual bool StopVisiting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00004AD8 File Offset: 0x00002CD8
		protected virtual void Visit(ModelLinguisticSchema schema)
		{
			this.VisitList<ModelSchemaReference>(schema.SchemaReferences, new Func<ModelSchemaReference, bool>(this.Visit));
			if (this.StopVisiting)
			{
				return;
			}
			this.VisitList<ModelLinguisticEntity>(schema.Entities, new Func<ModelLinguisticEntity, bool>(this.Visit));
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00004B15 File Offset: 0x00002D15
		protected virtual bool Visit(ModelSchemaReference schemaReference)
		{
			return true;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00004B18 File Offset: 0x00002D18
		protected virtual bool Visit(ModelLinguisticEntity entity)
		{
			this.VisitSourcedItem(entity);
			if (this.StopVisiting)
			{
				return true;
			}
			this.VisitList<Word>(entity.Words, (Word w) => this.Visit(w, entity));
			bool stopVisiting = this.StopVisiting;
			return true;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00004B74 File Offset: 0x00002D74
		protected virtual bool Visit(Word word, ModelLinguisticEntity parentEntity)
		{
			this.VisitSourcedItem(word);
			return true;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00004B7E File Offset: 0x00002D7E
		protected virtual void VisitSourcedItem(IModelLinguisticSourcedItem item)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00004B80 File Offset: 0x00002D80
		private void VisitList<T>(List<T> list, Func<T, bool> visitItem)
		{
			if (list == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (!visitItem(list[i]))
				{
					list.RemoveAt(i);
					i--;
				}
				if (this.StopVisiting)
				{
					return;
				}
			}
		}
	}
}
