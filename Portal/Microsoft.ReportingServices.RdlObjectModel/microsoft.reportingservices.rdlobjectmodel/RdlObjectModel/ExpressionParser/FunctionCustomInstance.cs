using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002B3 RID: 691
	[Serializable]
	internal sealed class FunctionCustomInstance : FunctionMultiArgument
	{
		// Token: 0x0600155C RID: 5468 RVA: 0x00031956 File Offset: 0x0002FB56
		public FunctionCustomInstance(Class rc, string f, IInternalExpression[] a, TypeCode type)
			: base(a)
		{
			this._Cls = null;
			this._Func = f;
			this._ReturnTypeCode = type;
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00031975 File Offset: 0x0002FB75
		public override TypeCode TypeCode()
		{
			return this._ReturnTypeCode;
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0003197D File Offset: 0x0002FB7D
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x00031985 File Offset: 0x0002FB85
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

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0003198E File Offset: 0x0002FB8E
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x00031996 File Offset: 0x0002FB96
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

		// Token: 0x06001562 RID: 5474 RVA: 0x0003199F File Offset: 0x0002FB9F
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Not Implemented";
		}

		// Token: 0x040006C0 RID: 1728
		private string _Cls;

		// Token: 0x040006C1 RID: 1729
		private string _Func;

		// Token: 0x040006C2 RID: 1730
		private readonly TypeCode _ReturnTypeCode;
	}
}
