using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002B1 RID: 689
	[Serializable]
	internal sealed class FunctionArrayInit : BaseInternalExpression
	{
		// Token: 0x06001551 RID: 5457 RVA: 0x000317F5 File Offset: 0x0002F9F5
		internal FunctionArrayInit(List<IInternalExpression> items)
		{
			this.m_items = items;
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x00031804 File Offset: 0x0002FA04
		internal List<IInternalExpression> Items
		{
			get
			{
				return this.m_items;
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0003180C File Offset: 0x0002FA0C
		public override TypeCode TypeCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00031814 File Offset: 0x0002FA14
		public override string WriteSource(NameChanges nameChanges)
		{
			string text = "{";
			if (this.m_items != null)
			{
				foreach (IInternalExpression internalExpression in this.m_items)
				{
					text = text + internalExpression.WriteSource() + ",";
				}
				text.Remove(text.Length - 1);
			}
			text += "}";
			return text;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0003189C File Offset: 0x0002FA9C
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			if (this.m_items != null)
			{
				foreach (IInternalExpression internalExpression in this.m_items)
				{
					internalExpression.Traverse(callback);
				}
			}
		}

		// Token: 0x040006BD RID: 1725
		private readonly List<IInternalExpression> m_items;
	}
}
