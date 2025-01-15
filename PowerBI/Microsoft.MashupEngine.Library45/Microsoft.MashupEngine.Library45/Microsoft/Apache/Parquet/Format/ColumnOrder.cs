using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002003 RID: 8195
	internal class ColumnOrder
	{
		// Token: 0x17002D4C RID: 11596
		// (get) Token: 0x060111AF RID: 70063 RVA: 0x003AF4BC File Offset: 0x003AD6BC
		// (set) Token: 0x060111B0 RID: 70064 RVA: 0x003AF4C4 File Offset: 0x003AD6C4
		public TypeDefinedOrder TYPE_ORDER { get; set; }

		// Token: 0x060111B1 RID: 70065 RVA: 0x003AF4D0 File Offset: 0x003AD6D0
		public void Read(FastProtocol prot)
		{
			this.TYPE_ORDER = null;
			prot.IncrementRecursionDepth();
			try
			{
				prot.ReadStructBegin();
				for (;;)
				{
					TField tfield = prot.ReadFieldBegin();
					if (tfield.Type == TType.Stop)
					{
						break;
					}
					bool flag = true;
					if (tfield.ID == 1 && tfield.Type == TType.Struct)
					{
						TypeDefinedOrder typeDefinedOrder = new TypeDefinedOrder();
						typeDefinedOrder.Read(prot);
						this.TYPE_ORDER = typeDefinedOrder;
						flag = false;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
			}
			finally
			{
				prot.DecrementRecursionDepth();
			}
		}
	}
}
