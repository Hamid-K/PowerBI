using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002B4 RID: 692
	[Serializable]
	internal sealed class FunctionCustomStatic : FunctionMultiArgument
	{
		// Token: 0x06001563 RID: 5475 RVA: 0x000319A6 File Offset: 0x0002FBA6
		public FunctionCustomStatic(string c, string f, IInternalExpression[] a, TypeCode type)
			: base(a)
		{
			this._Cls = c;
			this._Func = f;
			this._ReturnTypeCode = type;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x000319C5 File Offset: 0x0002FBC5
		public override TypeCode TypeCode()
		{
			return this._ReturnTypeCode;
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x000319CD File Offset: 0x0002FBCD
		// (set) Token: 0x06001566 RID: 5478 RVA: 0x000319D5 File Offset: 0x0002FBD5
		public string Cls
		{
			get
			{
				return this._Cls;
			}
			set
			{
				this._Cls = value;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x000319DE File Offset: 0x0002FBDE
		// (set) Token: 0x06001568 RID: 5480 RVA: 0x000319E6 File Offset: 0x0002FBE6
		public string Func
		{
			get
			{
				return this._Func;
			}
			set
			{
				this._Func = value;
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x000319EF File Offset: 0x0002FBEF
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Not Implemented";
		}

		// Token: 0x040006C3 RID: 1731
		private string _Cls;

		// Token: 0x040006C4 RID: 1732
		private string _Func;

		// Token: 0x040006C5 RID: 1733
		private readonly TypeCode _ReturnTypeCode;
	}
}
