using System;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200034F RID: 847
	internal class NestedPropertyRef : PropertyRef
	{
		// Token: 0x060028A8 RID: 10408 RVA: 0x0007D868 File Offset: 0x0007BA68
		internal NestedPropertyRef(PropertyRef innerProperty, PropertyRef outerProperty)
		{
			PlanCompiler.Assert(!(innerProperty is NestedPropertyRef), "innerProperty cannot be a NestedPropertyRef");
			this.m_inner = innerProperty;
			this.m_outer = outerProperty;
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0007D894 File Offset: 0x0007BA94
		internal PropertyRef OuterProperty
		{
			get
			{
				return this.m_outer;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x0007D89C File Offset: 0x0007BA9C
		internal PropertyRef InnerProperty
		{
			get
			{
				return this.m_inner;
			}
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x0007D8A4 File Offset: 0x0007BAA4
		public override bool Equals(object obj)
		{
			NestedPropertyRef nestedPropertyRef = obj as NestedPropertyRef;
			return nestedPropertyRef != null && this.m_inner.Equals(nestedPropertyRef.m_inner) && this.m_outer.Equals(nestedPropertyRef.m_outer);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x0007D8E1 File Offset: 0x0007BAE1
		public override int GetHashCode()
		{
			return this.m_inner.GetHashCode() ^ this.m_outer.GetHashCode();
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x0007D8FA File Offset: 0x0007BAFA
		public override string ToString()
		{
			PropertyRef inner = this.m_inner;
			string text = ((inner != null) ? inner.ToString() : null);
			string text2 = ".";
			PropertyRef outer = this.m_outer;
			return text + text2 + ((outer != null) ? outer.ToString() : null);
		}

		// Token: 0x04000E29 RID: 3625
		private readonly PropertyRef m_inner;

		// Token: 0x04000E2A RID: 3626
		private readonly PropertyRef m_outer;
	}
}
