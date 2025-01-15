using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x0200202A RID: 8234
	internal class TimeType
	{
		// Token: 0x17002DAA RID: 11690
		// (get) Token: 0x060112AF RID: 70319 RVA: 0x003B20A8 File Offset: 0x003B02A8
		// (set) Token: 0x060112B0 RID: 70320 RVA: 0x003B20B0 File Offset: 0x003B02B0
		public bool? IsAdjustedToUTC { get; set; }

		// Token: 0x17002DAB RID: 11691
		// (get) Token: 0x060112B1 RID: 70321 RVA: 0x003B20B9 File Offset: 0x003B02B9
		// (set) Token: 0x060112B2 RID: 70322 RVA: 0x003B20C1 File Offset: 0x003B02C1
		public TimeUnit Unit { get; set; }

		// Token: 0x060112B3 RID: 70323 RVA: 0x003B20CC File Offset: 0x003B02CC
		public void Read(FastProtocol prot)
		{
			this.IsAdjustedToUTC = null;
			this.Unit = null;
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
							if (tfield.Type == TType.Struct)
							{
								TimeUnit timeUnit = new TimeUnit();
								timeUnit.Read(prot);
								this.Unit = timeUnit;
								flag = false;
							}
						}
					}
					else if (tfield.Type == TType.Bool)
					{
						this.IsAdjustedToUTC = new bool?(prot.ReadBool());
						flag = false;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.IsAdjustedToUTC == null || this.Unit == null)
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
