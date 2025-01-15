using System;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000302 RID: 770
	internal abstract class Property : SchemaElement
	{
		// Token: 0x06002487 RID: 9351 RVA: 0x000677A7 File Offset: 0x000659A7
		internal Property(StructuredType parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002488 RID: 9352
		public abstract SchemaType Type { get; }

		// Token: 0x06002489 RID: 9353 RVA: 0x000677B4 File Offset: 0x000659B4
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				if (base.CanHandleElement(reader, "ValueAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "TypeAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
			}
			return false;
		}
	}
}
