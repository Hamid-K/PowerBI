using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C4 RID: 452
	internal class CsdlEnumType : CsdlNamedElement
	{
		// Token: 0x06000C81 RID: 3201 RVA: 0x000238E4 File Offset: 0x00021AE4
		public CsdlEnumType(string name, string underlyingTypeName, bool isFlags, IEnumerable<CsdlEnumMember> members, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.underlyingTypeName = underlyingTypeName;
			this.isFlags = isFlags;
			this.members = new List<CsdlEnumMember>(members);
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0002390C File Offset: 0x00021B0C
		public string UnderlyingTypeName
		{
			get
			{
				return this.underlyingTypeName;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00023914 File Offset: 0x00021B14
		public bool IsFlags
		{
			get
			{
				return this.isFlags;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002391C File Offset: 0x00021B1C
		public IEnumerable<CsdlEnumMember> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x040006D0 RID: 1744
		private readonly string underlyingTypeName;

		// Token: 0x040006D1 RID: 1745
		private readonly bool isFlags;

		// Token: 0x040006D2 RID: 1746
		private readonly List<CsdlEnumMember> members;
	}
}
