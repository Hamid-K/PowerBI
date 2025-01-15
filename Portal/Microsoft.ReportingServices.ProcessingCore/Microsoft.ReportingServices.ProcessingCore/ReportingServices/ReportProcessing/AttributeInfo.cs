using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006D4 RID: 1748
	[Serializable]
	internal sealed class AttributeInfo
	{
		// Token: 0x1700212D RID: 8493
		// (get) Token: 0x06005E7F RID: 24191 RVA: 0x001804E8 File Offset: 0x0017E6E8
		// (set) Token: 0x06005E80 RID: 24192 RVA: 0x001804F0 File Offset: 0x0017E6F0
		internal bool IsExpression
		{
			get
			{
				return this.m_isExpression;
			}
			set
			{
				this.m_isExpression = value;
			}
		}

		// Token: 0x1700212E RID: 8494
		// (get) Token: 0x06005E81 RID: 24193 RVA: 0x001804F9 File Offset: 0x0017E6F9
		// (set) Token: 0x06005E82 RID: 24194 RVA: 0x00180501 File Offset: 0x0017E701
		internal string Value
		{
			get
			{
				return this.m_stringValue;
			}
			set
			{
				this.m_stringValue = value;
			}
		}

		// Token: 0x1700212F RID: 8495
		// (get) Token: 0x06005E83 RID: 24195 RVA: 0x0018050A File Offset: 0x0017E70A
		// (set) Token: 0x06005E84 RID: 24196 RVA: 0x00180512 File Offset: 0x0017E712
		internal bool BoolValue
		{
			get
			{
				return this.m_boolValue;
			}
			set
			{
				this.m_boolValue = value;
			}
		}

		// Token: 0x17002130 RID: 8496
		// (get) Token: 0x06005E85 RID: 24197 RVA: 0x0018051B File Offset: 0x0017E71B
		// (set) Token: 0x06005E86 RID: 24198 RVA: 0x00180523 File Offset: 0x0017E723
		internal int IntValue
		{
			get
			{
				return this.m_intValue;
			}
			set
			{
				this.m_intValue = value;
			}
		}

		// Token: 0x06005E87 RID: 24199 RVA: 0x0018052C File Offset: 0x0017E72C
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.IsExpression, Token.Boolean),
				new MemberInfo(MemberName.StringValue, Token.String),
				new MemberInfo(MemberName.BoolValue, Token.Boolean),
				new MemberInfo(MemberName.IntValue, Token.Int32)
			});
		}

		// Token: 0x04003037 RID: 12343
		private bool m_isExpression;

		// Token: 0x04003038 RID: 12344
		private string m_stringValue;

		// Token: 0x04003039 RID: 12345
		private bool m_boolValue;

		// Token: 0x0400303A RID: 12346
		private int m_intValue;
	}
}
