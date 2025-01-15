using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D3 RID: 467
	internal class CsdlEnumType : CsdlNamedElement
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x00025AAF File Offset: 0x00023CAF
		public CsdlEnumType(string name, string underlyingTypeName, bool isFlags, IEnumerable<CsdlEnumMember> members, CsdlLocation location)
			: base(name, location)
		{
			this.underlyingTypeName = underlyingTypeName;
			this.isFlags = isFlags;
			this.members = new List<CsdlEnumMember>(members);
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00025AD5 File Offset: 0x00023CD5
		public string UnderlyingTypeName
		{
			get
			{
				return this.underlyingTypeName;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00025ADD File Offset: 0x00023CDD
		public bool IsFlags
		{
			get
			{
				return this.isFlags;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00025AE5 File Offset: 0x00023CE5
		public IEnumerable<CsdlEnumMember> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x04000749 RID: 1865
		private readonly string underlyingTypeName;

		// Token: 0x0400074A RID: 1866
		private readonly bool isFlags;

		// Token: 0x0400074B RID: 1867
		private readonly List<CsdlEnumMember> members;
	}
}
