using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000200 RID: 512
	internal class CsdlProperty : CsdlNamedElement
	{
		// Token: 0x06000DDB RID: 3547 RVA: 0x0002644A File Offset: 0x0002464A
		public CsdlProperty(string name, CsdlTypeReference type, string defaultValue, CsdlLocation location)
			: base(name, location)
		{
			this.type = type;
			this.defaultValue = defaultValue;
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00026463 File Offset: 0x00024663
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0002646B File Offset: 0x0002466B
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x0400079E RID: 1950
		private readonly CsdlTypeReference type;

		// Token: 0x0400079F RID: 1951
		private readonly string defaultValue;
	}
}
