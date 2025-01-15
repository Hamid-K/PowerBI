using System;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000031 RID: 49
	internal sealed class PropertyRefElement : SchemaElement
	{
		// Token: 0x060006DC RID: 1756 RVA: 0x0000CA59 File Offset: 0x0000AC59
		public PropertyRefElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0000CA62 File Offset: 0x0000AC62
		public StructuredProperty Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0000CA6A File Offset: 0x0000AC6A
		internal override void ResolveTopLevelNames()
		{
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		internal bool ResolveNames(SchemaEntityType entityType)
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				return true;
			}
			this._property = entityType.FindProperty(this.Name);
			return this._property != null;
		}

		// Token: 0x04000666 RID: 1638
		private StructuredProperty _property;
	}
}
