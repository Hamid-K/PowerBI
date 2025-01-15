using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x0200089F RID: 2207
	public class OUTOVR : AbstractDdmObject
	{
		// Token: 0x0600464D RID: 17997 RVA: 0x000F5197 File Offset: 0x000F3397
		public override void Reset()
		{
			this.outovr_drdaType = null;
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x0600464E RID: 17998 RVA: 0x000F51A0 File Offset: 0x000F33A0
		// (set) Token: 0x0600464F RID: 17999 RVA: 0x000F51A8 File Offset: 0x000F33A8
		public int[] Outovr_drdaType
		{
			get
			{
				return this.outovr_drdaType;
			}
			set
			{
				this.outovr_drdaType = value;
			}
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x000F51B4 File Offset: 0x000F33B4
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			bool first = true;
			int start = 0;
			for (;;)
			{
				int num = await reader.ReadUnsignedByteAsync(isAsync, cancellationToken);
				int dtaGrpLen = num;
				num = await reader.ReadUnsignedByteAsync(isAsync, cancellationToken);
				int tripType = num;
				await reader.ReadUnsignedByteAsync(isAsync, cancellationToken);
				if (tripType == 113)
				{
					break;
				}
				int numVars = (dtaGrpLen - 3) / 3;
				int[] outovr_drdaType = null;
				if (first)
				{
					outovr_drdaType = new int[numVars];
					first = false;
				}
				else
				{
					int[] array = this.Outovr_drdaType;
					int num2 = array.Length;
					outovr_drdaType = new int[num2 + numVars];
					Array.Copy(array, 0, outovr_drdaType, 0, num2);
					start = num2;
				}
				for (int i = start; i < numVars + start; i = num + 1)
				{
					int num3 = await reader.ReadUnsignedByteAsync(isAsync, cancellationToken);
					if (num3 >= 24 && num3 <= 27)
					{
						await reader.ReadNetworkShortAsync(isAsync, cancellationToken);
					}
					else
					{
						outovr_drdaType[i] = num3;
						int num4 = await reader.ReadNetworkShortAsync(isAsync, cancellationToken);
						outovr_drdaType[i] |= num4 << 8;
					}
					num = i;
				}
				this.Outovr_drdaType = outovr_drdaType;
				outovr_drdaType = null;
			}
			await reader.SkipRemainderAsync(true, isAsync, cancellationToken);
		}

		// Token: 0x04003221 RID: 12833
		private int[] outovr_drdaType;
	}
}
