using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x0200202B RID: 8235
	internal class TimeUnit
	{
		// Token: 0x17002DAC RID: 11692
		// (get) Token: 0x060112B5 RID: 70325 RVA: 0x003B21B4 File Offset: 0x003B03B4
		// (set) Token: 0x060112B6 RID: 70326 RVA: 0x003B21BC File Offset: 0x003B03BC
		public MilliSeconds MILLIS { get; set; }

		// Token: 0x17002DAD RID: 11693
		// (get) Token: 0x060112B7 RID: 70327 RVA: 0x003B21C5 File Offset: 0x003B03C5
		// (set) Token: 0x060112B8 RID: 70328 RVA: 0x003B21CD File Offset: 0x003B03CD
		public MicroSeconds MICROS { get; set; }

		// Token: 0x17002DAE RID: 11694
		// (get) Token: 0x060112B9 RID: 70329 RVA: 0x003B21D6 File Offset: 0x003B03D6
		// (set) Token: 0x060112BA RID: 70330 RVA: 0x003B21DE File Offset: 0x003B03DE
		public NanoSeconds NANOS { get; set; }

		// Token: 0x060112BB RID: 70331 RVA: 0x003B21E8 File Offset: 0x003B03E8
		public void Read(FastProtocol prot)
		{
			this.MILLIS = null;
			this.MICROS = null;
			this.NANOS = null;
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
					switch (tfield.ID)
					{
					case 1:
						if (tfield.Type == TType.Struct)
						{
							MilliSeconds milliSeconds = new MilliSeconds();
							milliSeconds.Read(prot);
							this.MILLIS = milliSeconds;
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.Struct)
						{
							MicroSeconds microSeconds = new MicroSeconds();
							microSeconds.Read(prot);
							this.MICROS = microSeconds;
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.Struct)
						{
							NanoSeconds nanoSeconds = new NanoSeconds();
							nanoSeconds.Read(prot);
							this.NANOS = nanoSeconds;
							flag = false;
						}
						break;
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
