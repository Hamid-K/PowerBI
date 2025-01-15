using System;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200031C RID: 796
	internal abstract class SchemaType : SchemaElement
	{
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x0006C59B File Offset: 0x0006A79B
		public string Namespace
		{
			get
			{
				return base.Schema.Namespace;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x0006C5A8 File Offset: 0x0006A7A8
		public override string Identity
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x0006C5C0 File Offset: 0x0006A7C0
		public override string FQName
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0006C5D8 File Offset: 0x0006A7D8
		internal SchemaType(Schema parentElement)
			: base(parentElement, null)
		{
		}
	}
}
