using System;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000303 RID: 771
	internal sealed class PropertyRefElement : SchemaElement
	{
		// Token: 0x0600248A RID: 9354 RVA: 0x00067808 File Offset: 0x00065A08
		public PropertyRefElement(SchemaElement parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x00067812 File Offset: 0x00065A12
		public StructuredProperty Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0006781A File Offset: 0x00065A1A
		internal override void ResolveTopLevelNames()
		{
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x0006781C File Offset: 0x00065A1C
		internal bool ResolveNames(SchemaEntityType entityType)
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				return true;
			}
			this._property = entityType.FindProperty(this.Name);
			return this._property != null;
		}

		// Token: 0x04000CFB RID: 3323
		private StructuredProperty _property;
	}
}
