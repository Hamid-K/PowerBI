using System;
using System.Collections;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000016 RID: 22
	internal sealed class StructMapping : TypeMapping
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00005460 File Offset: 0x00003660
		public StructMapping()
		{
			this.elements = new NameTable();
			this.attributes = new NameTable();
			this.members = new ArrayList();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005489 File Offset: 0x00003689
		public MemberMapping GetElement(string name, string ns)
		{
			return (MemberMapping)this.elements[name, ns];
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000054A0 File Offset: 0x000036A0
		internal void AddMember(string name, string ns, MemberMapping member)
		{
			MemberMapping memberMapping = (MemberMapping)this.elements[name, ns];
			if (memberMapping != null)
			{
				this.members.Remove(memberMapping);
			}
			this.elements[name, ns] = member;
			this.members.Add(member);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000054EA File Offset: 0x000036EA
		public ArrayList Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x04000071 RID: 113
		internal NameTable elements;

		// Token: 0x04000072 RID: 114
		internal NameTable attributes;

		// Token: 0x04000073 RID: 115
		internal ArrayList members;
	}
}
