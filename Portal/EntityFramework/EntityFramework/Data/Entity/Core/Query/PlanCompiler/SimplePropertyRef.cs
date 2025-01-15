using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000366 RID: 870
	internal class SimplePropertyRef : PropertyRef
	{
		// Token: 0x06002A49 RID: 10825 RVA: 0x0008A394 File Offset: 0x00088594
		internal SimplePropertyRef(EdmMember property)
		{
			this.m_property = property;
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002A4A RID: 10826 RVA: 0x0008A3A3 File Offset: 0x000885A3
		internal EdmMember Property
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x0008A3AC File Offset: 0x000885AC
		public override bool Equals(object obj)
		{
			SimplePropertyRef simplePropertyRef = obj as SimplePropertyRef;
			return simplePropertyRef != null && Command.EqualTypes(this.m_property.DeclaringType, simplePropertyRef.m_property.DeclaringType) && simplePropertyRef.m_property.Name.Equals(this.m_property.Name);
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x0008A3FD File Offset: 0x000885FD
		public override int GetHashCode()
		{
			return this.m_property.Name.GetHashCode();
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x0008A40F File Offset: 0x0008860F
		public override string ToString()
		{
			return this.m_property.Name;
		}

		// Token: 0x04000E90 RID: 3728
		private readonly EdmMember m_property;
	}
}
