using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020002AB RID: 683
	public sealed class ODataProperty : ODataAnnotatable
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0004EF36 File Offset: 0x0004D136
		// (set) Token: 0x060015D1 RID: 5585 RVA: 0x0004EF3E File Offset: 0x0004D13E
		public string Name { get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0004EF48 File Offset: 0x0004D148
		// (set) Token: 0x060015D3 RID: 5587 RVA: 0x0004EF80 File Offset: 0x0004D180
		public object Value
		{
			get
			{
				if (this.odataOrUntypedValue == null)
				{
					return null;
				}
				ODataUntypedValue odataUntypedValue = this.odataOrUntypedValue as ODataUntypedValue;
				if (odataUntypedValue != null)
				{
					return odataUntypedValue;
				}
				return ((ODataValue)this.odataOrUntypedValue).FromODataValue();
			}
			set
			{
				ODataUntypedValue odataUntypedValue = value as ODataUntypedValue;
				if (odataUntypedValue != null)
				{
					this.odataOrUntypedValue = odataUntypedValue;
					return;
				}
				this.odataOrUntypedValue = value.ToODataValue();
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0004EFAB File Offset: 0x0004D1AB
		internal ODataValue ODataValue
		{
			get
			{
				return (ODataValue)this.odataOrUntypedValue;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x0004EFB8 File Offset: 0x0004D1B8
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x0004EFC0 File Offset: 0x0004D1C0
		internal ODataPropertySerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = value;
			}
		}

		// Token: 0x04000989 RID: 2441
		private ODataAnnotatable odataOrUntypedValue;

		// Token: 0x0400098A RID: 2442
		private ODataPropertySerializationInfo serializationInfo;
	}
}
