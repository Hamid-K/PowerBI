using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011EA RID: 4586
	internal sealed class PagingQuerySpecification : QuerySpecification
	{
		// Token: 0x060078E8 RID: 30952 RVA: 0x001A24BA File Offset: 0x001A06BA
		public PagingQuerySpecification()
		{
			base.SelectItems = new List<SelectItem>();
			base.FromItems = new List<FromItem>();
		}

		// Token: 0x060078E9 RID: 30953 RVA: 0x001A24D8 File Offset: 0x001A06D8
		public PagingQuerySpecification(List<SelectItem> selectItems, List<FromItem> fromItems)
		{
			base.SelectItems = selectItems;
			base.FromItems = fromItems;
		}

		// Token: 0x17002113 RID: 8467
		// (get) Token: 0x060078EA RID: 30954 RVA: 0x001A24EE File Offset: 0x001A06EE
		// (set) Token: 0x060078EB RID: 30955 RVA: 0x001A24F6 File Offset: 0x001A06F6
		public PagingClause PagingClause { get; set; }

		// Token: 0x17002114 RID: 8468
		// (get) Token: 0x060078EC RID: 30956 RVA: 0x001A24FF File Offset: 0x001A06FF
		// (set) Token: 0x060078ED RID: 30957 RVA: 0x001A2507 File Offset: 0x001A0707
		public bool ReturnsAllColumns { get; set; }

		// Token: 0x060078EE RID: 30958 RVA: 0x001A2510 File Offset: 0x001A0710
		public PagingQuerySpecification ShallowCopy()
		{
			PagingQuerySpecification pagingQuerySpecification = base.ShallowCopyTo<PagingQuerySpecification>(new PagingQuerySpecification
			{
				PagingClause = this.PagingClause
			});
			pagingQuerySpecification.FromItems = new List<FromItem>(base.FromItems);
			pagingQuerySpecification.SelectItems = new List<SelectItem>(base.SelectItems);
			return pagingQuerySpecification;
		}

		// Token: 0x060078EF RID: 30959 RVA: 0x001A254B File Offset: 0x001A074B
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.Settings.PagingStrategy.WritePrologue(this, writer);
			base.WriteCreateScript(writer);
			writer.Settings.PagingStrategy.WriteEpilogue(this, writer);
		}

		// Token: 0x060078F0 RID: 30960 RVA: 0x001A2578 File Offset: 0x001A0778
		protected override void WriteSelectColumnList(ScriptWriter writer)
		{
			base.WriteSelectColumnList(writer);
			writer.Settings.PagingStrategy.WriteAdditionalSelectColumnList(this, writer);
		}

		// Token: 0x060078F1 RID: 30961 RVA: 0x001A2593 File Offset: 0x001A0793
		protected override void WriteSelectModifiers(ScriptWriter writer)
		{
			base.WriteSelectModifiers(writer);
			writer.Settings.PagingStrategy.WriteAdditionalSelectModifiers(this, writer);
		}

		// Token: 0x060078F2 RID: 30962 RVA: 0x001A25AE File Offset: 0x001A07AE
		protected override void WriteOrderByClause(ScriptWriter writer)
		{
			if (writer.Settings.PagingStrategy.ShouldWriteBaseOrderByClause(this))
			{
				base.WriteOrderByClause(writer);
			}
		}
	}
}
