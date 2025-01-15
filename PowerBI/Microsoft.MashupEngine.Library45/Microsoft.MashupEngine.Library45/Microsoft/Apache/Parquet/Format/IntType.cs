using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002014 RID: 8212
	internal class IntType
	{
		// Token: 0x17002D6E RID: 11630
		// (get) Token: 0x0601120D RID: 70157 RVA: 0x003B05A4 File Offset: 0x003AE7A4
		// (set) Token: 0x0601120E RID: 70158 RVA: 0x003B05AC File Offset: 0x003AE7AC
		public sbyte? BitWidth { get; set; }

		// Token: 0x17002D6F RID: 11631
		// (get) Token: 0x0601120F RID: 70159 RVA: 0x003B05B5 File Offset: 0x003AE7B5
		// (set) Token: 0x06011210 RID: 70160 RVA: 0x003B05BD File Offset: 0x003AE7BD
		public bool? IsSigned { get; set; }

		// Token: 0x06011211 RID: 70161 RVA: 0x003B05C8 File Offset: 0x003AE7C8
		public void Read(FastProtocol prot)
		{
			this.BitWidth = null;
			this.IsSigned = null;
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
							if (tfield.Type == TType.Bool)
							{
								this.IsSigned = new bool?(prot.ReadBool());
								flag = false;
							}
						}
					}
					else if (tfield.Type == TType.I8)
					{
						this.BitWidth = new sbyte?(prot.ReadI8());
						flag = false;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.BitWidth == null || this.IsSigned == null)
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
