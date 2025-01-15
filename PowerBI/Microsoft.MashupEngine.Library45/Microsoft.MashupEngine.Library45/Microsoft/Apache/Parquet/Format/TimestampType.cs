using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002029 RID: 8233
	internal class TimestampType
	{
		// Token: 0x17002DA8 RID: 11688
		// (get) Token: 0x060112A9 RID: 70313 RVA: 0x003B1F9C File Offset: 0x003B019C
		// (set) Token: 0x060112AA RID: 70314 RVA: 0x003B1FA4 File Offset: 0x003B01A4
		public bool? IsAdjustedToUTC { get; set; }

		// Token: 0x17002DA9 RID: 11689
		// (get) Token: 0x060112AB RID: 70315 RVA: 0x003B1FAD File Offset: 0x003B01AD
		// (set) Token: 0x060112AC RID: 70316 RVA: 0x003B1FB5 File Offset: 0x003B01B5
		public TimeUnit Unit { get; set; }

		// Token: 0x060112AD RID: 70317 RVA: 0x003B1FC0 File Offset: 0x003B01C0
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
