using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002BD RID: 701
	internal sealed class FunctionType : BaseInternalExpression
	{
		// Token: 0x060015AE RID: 5550 RVA: 0x00032E60 File Offset: 0x00031060
		internal FunctionType(IReportLink container, string typeName)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00032E6F File Offset: 0x0003106F
		internal FunctionType(TypeContext typeContext)
		{
			this.m_typeContext = typeContext;
			if (this.m_typeContext != null)
			{
				this.m_typeName = typeContext.Name;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00032E92 File Offset: 0x00031092
		internal TypeContext TypeContext
		{
			get
			{
				return this.m_typeContext;
			}
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00032E9A File Offset: 0x0003109A
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00032E9D File Offset: 0x0003109D
		public override string WriteSource(NameChanges nameChanges)
		{
			if (string.IsNullOrEmpty(this.TypeContext.MethodFullName))
			{
				return "<<UNKNOWN>>";
			}
			return this.TypeContext.MethodFullName;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00032EC2 File Offset: 0x000310C2
		public override object Evaluate()
		{
			return this.m_type;
		}

		// Token: 0x040006DF RID: 1759
		private string m_typeName;

		// Token: 0x040006E0 RID: 1760
		private readonly Type m_type;

		// Token: 0x040006E1 RID: 1761
		private readonly TypeContext m_typeContext;
	}
}
