using System;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000372 RID: 882
	internal class TypeIdPropertyRef : PropertyRef
	{
		// Token: 0x06002AB7 RID: 10935 RVA: 0x0008CA54 File Offset: 0x0008AC54
		private TypeIdPropertyRef()
		{
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x0008CA5C File Offset: 0x0008AC5C
		public override string ToString()
		{
			return "TYPEID";
		}

		// Token: 0x04000EC7 RID: 3783
		internal static TypeIdPropertyRef Instance = new TypeIdPropertyRef();
	}
}
