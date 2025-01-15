using System;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BDD RID: 3037
	public class FieldSelectorInfo
	{
		// Token: 0x060052D5 RID: 21205 RVA: 0x00117E7F File Offset: 0x0011607F
		public FieldSelectorInfo(string fieldName, Type fieldType, Func<object, object> fieldSelector)
			: this(fieldName, fieldType, fieldSelector, false)
		{
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x00117E8B File Offset: 0x0011608B
		public FieldSelectorInfo(string fieldName, Type fieldType, Func<object, object> fieldSelector, bool foldable)
		{
			this.fieldName = fieldName;
			this.fieldType = fieldType;
			this.fieldSelector = fieldSelector;
			this.foldable = foldable;
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x060052D7 RID: 21207 RVA: 0x00117EB0 File Offset: 0x001160B0
		public string FieldName
		{
			get
			{
				return this.fieldName;
			}
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x060052D8 RID: 21208 RVA: 0x00117EB8 File Offset: 0x001160B8
		public bool Foldable
		{
			get
			{
				return this.foldable;
			}
		}

		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x060052D9 RID: 21209 RVA: 0x00117EC0 File Offset: 0x001160C0
		public Type FieldType
		{
			get
			{
				return this.fieldType;
			}
		}

		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x060052DA RID: 21210 RVA: 0x00117EC8 File Offset: 0x001160C8
		public Func<object, object> FieldSelector
		{
			get
			{
				return this.fieldSelector;
			}
		}

		// Token: 0x04002DB3 RID: 11699
		private Type fieldType;

		// Token: 0x04002DB4 RID: 11700
		private Func<object, object> fieldSelector;

		// Token: 0x04002DB5 RID: 11701
		private bool foldable;

		// Token: 0x04002DB6 RID: 11702
		private string fieldName;
	}
}
