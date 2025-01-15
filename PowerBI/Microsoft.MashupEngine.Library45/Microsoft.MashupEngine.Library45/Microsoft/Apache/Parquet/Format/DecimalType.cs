using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002009 RID: 8201
	internal class DecimalType
	{
		// Token: 0x17002D5A RID: 11610
		// (get) Token: 0x060111D3 RID: 70099 RVA: 0x003AFB04 File Offset: 0x003ADD04
		// (set) Token: 0x060111D4 RID: 70100 RVA: 0x003AFB0C File Offset: 0x003ADD0C
		public int? Scale { get; set; }

		// Token: 0x17002D5B RID: 11611
		// (get) Token: 0x060111D5 RID: 70101 RVA: 0x003AFB15 File Offset: 0x003ADD15
		// (set) Token: 0x060111D6 RID: 70102 RVA: 0x003AFB1D File Offset: 0x003ADD1D
		public int? Precision { get; set; }

		// Token: 0x060111D7 RID: 70103 RVA: 0x003AFB28 File Offset: 0x003ADD28
		public void Read(FastProtocol prot)
		{
			this.Scale = null;
			this.Precision = null;
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
					short id = tfield.ID;
					if (id != 1)
					{
						if (id == 2)
						{
							if (tfield.Type == TType.I32)
							{
								this.Precision = new int?(prot.ReadI32());
								flag = false;
							}
						}
					}
					else if (tfield.Type == TType.I32)
					{
						this.Scale = new int?(prot.ReadI32());
						flag = false;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.Scale == null || this.Precision == null)
				{
					throw new InvalidOperationException("Required field not found");
				}
			}
			finally
			{
				prot.DecrementRecursionDepth();
			}
		}
	}
}
