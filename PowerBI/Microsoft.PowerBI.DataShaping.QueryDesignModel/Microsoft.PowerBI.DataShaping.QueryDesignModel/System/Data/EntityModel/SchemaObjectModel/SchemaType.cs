using System;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000052 RID: 82
	internal abstract class SchemaType : SchemaElement
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00011928 File Offset: 0x0000FB28
		public string Namespace
		{
			get
			{
				return base.Schema.Namespace;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00011935 File Offset: 0x0000FB35
		public override string Identity
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0001194D File Offset: 0x0000FB4D
		public override string FQName
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00011965 File Offset: 0x0000FB65
		internal SchemaType(Schema parentElement)
			: base(parentElement)
		{
		}
	}
}
