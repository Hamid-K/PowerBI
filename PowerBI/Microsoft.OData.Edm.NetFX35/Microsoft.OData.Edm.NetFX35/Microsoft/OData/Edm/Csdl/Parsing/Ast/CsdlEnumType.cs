using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000029 RID: 41
	internal class CsdlEnumType : CsdlNamedElement
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x0000361B File Offset: 0x0000181B
		public CsdlEnumType(string name, string underlyingTypeName, bool isFlags, IEnumerable<CsdlEnumMember> members, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.underlyingTypeName = underlyingTypeName;
			this.isFlags = isFlags;
			this.members = new List<CsdlEnumMember>(members);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003643 File Offset: 0x00001843
		public string UnderlyingTypeName
		{
			get
			{
				return this.underlyingTypeName;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000364B File Offset: 0x0000184B
		public bool IsFlags
		{
			get
			{
				return this.isFlags;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003653 File Offset: 0x00001853
		public IEnumerable<CsdlEnumMember> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x0400003C RID: 60
		private readonly string underlyingTypeName;

		// Token: 0x0400003D RID: 61
		private readonly bool isFlags;

		// Token: 0x0400003E RID: 62
		private readonly List<CsdlEnumMember> members;
	}
}
