using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002E0 RID: 736
	public class TypeMapping
	{
		// Token: 0x060016B6 RID: 5814 RVA: 0x00035B3E File Offset: 0x00033D3E
		public TypeMapping(Type type)
		{
			this.Type = type;
			this.Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition";
			this.Name = type.Name;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00035B64 File Offset: 0x00033D64
		internal void AddChildAttribute(MemberMapping mapping)
		{
			if (this.ChildAttributes == null)
			{
				this.ChildAttributes = new List<MemberMapping>();
			}
			this.ChildAttributes.Add(mapping);
		}

		// Token: 0x04000700 RID: 1792
		public Type Type;

		// Token: 0x04000701 RID: 1793
		public string Name;

		// Token: 0x04000702 RID: 1794
		public string Namespace;

		// Token: 0x04000703 RID: 1795
		public List<MemberMapping> ChildAttributes;
	}
}
