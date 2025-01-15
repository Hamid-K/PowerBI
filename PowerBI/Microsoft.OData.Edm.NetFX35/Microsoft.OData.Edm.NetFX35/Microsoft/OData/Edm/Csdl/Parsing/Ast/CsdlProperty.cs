using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000189 RID: 393
	internal class CsdlProperty : CsdlNamedElement
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x00011A57 File Offset: 0x0000FC57
		public CsdlProperty(string name, CsdlTypeReference type, bool isFixedConcurrency, string defaultValue, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.type = type;
			this.isFixedConcurrency = isFixedConcurrency;
			this.defaultValue = defaultValue;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00011A7A File Offset: 0x0000FC7A
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00011A82 File Offset: 0x0000FC82
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00011A8A File Offset: 0x0000FC8A
		public bool IsFixedConcurrency
		{
			get
			{
				return this.isFixedConcurrency;
			}
		}

		// Token: 0x040003D3 RID: 979
		private readonly CsdlTypeReference type;

		// Token: 0x040003D4 RID: 980
		private readonly string defaultValue;

		// Token: 0x040003D5 RID: 981
		private readonly bool isFixedConcurrency;
	}
}
