using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F6 RID: 502
	internal class CsdlOperationParameter : CsdlNamedElement
	{
		// Token: 0x06000DA8 RID: 3496 RVA: 0x00026139 File Offset: 0x00024339
		public CsdlOperationParameter(string name, CsdlTypeReference type, CsdlLocation location)
			: base(name, location)
		{
			this.type = type;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0002614A File Offset: 0x0002434A
		public CsdlOperationParameter(string name, CsdlTypeReference type, CsdlLocation location, bool isOptional, string defaultValue)
			: this(name, type, location)
		{
			this.isOptional = isOptional;
			this.defaultValue = defaultValue;
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00026165 File Offset: 0x00024365
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0002616D File Offset: 0x0002436D
		public bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00026175 File Offset: 0x00024375
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x04000781 RID: 1921
		private readonly CsdlTypeReference type;

		// Token: 0x04000782 RID: 1922
		private readonly bool isOptional;

		// Token: 0x04000783 RID: 1923
		private readonly string defaultValue;
	}
}
