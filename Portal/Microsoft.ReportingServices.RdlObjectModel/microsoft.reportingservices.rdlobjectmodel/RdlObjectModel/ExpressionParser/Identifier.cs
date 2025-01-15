using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200028F RID: 655
	[Serializable]
	internal sealed class Identifier : BaseInternalExpression
	{
		// Token: 0x0600148F RID: 5263 RVA: 0x00030368 File Offset: 0x0002E568
		public Identifier(string value)
		{
			this._Value = value;
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00030377 File Offset: 0x0002E577
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0003037A File Offset: 0x0002E57A
		public override string WriteSource()
		{
			return this._Value;
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00030382 File Offset: 0x0002E582
		public string Value
		{
			get
			{
				return this._Value;
			}
		}

		// Token: 0x040006B4 RID: 1716
		private readonly string _Value;
	}
}
