using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000472 RID: 1138
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class EdmScalarPropertyAttribute : EdmPropertyAttribute
	{
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x000B6034 File Offset: 0x000B4234
		// (set) Token: 0x06003797 RID: 14231 RVA: 0x000B603C File Offset: 0x000B423C
		public bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
			set
			{
				this._isNullable = value;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000B6045 File Offset: 0x000B4245
		// (set) Token: 0x06003799 RID: 14233 RVA: 0x000B604D File Offset: 0x000B424D
		public bool EntityKeyProperty { get; set; }

		// Token: 0x040012D4 RID: 4820
		private bool _isNullable = true;
	}
}
