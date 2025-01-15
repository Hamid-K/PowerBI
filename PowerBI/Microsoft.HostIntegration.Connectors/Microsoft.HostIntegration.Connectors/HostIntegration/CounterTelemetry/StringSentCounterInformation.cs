using System;
using System.Text;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x02000611 RID: 1553
	internal class StringSentCounterInformation : SentCounterInformation
	{
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000AEDA3 File Offset: 0x000ACFA3
		internal uint ByteCount
		{
			get
			{
				return (uint)this.bytes.Length;
			}
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000AEDAD File Offset: 0x000ACFAD
		internal StringSentCounterInformation(uint feature, uint subFeature, uint collection, string counter, uint value)
			: base(feature, subFeature, collection, value)
		{
			this.bytes = Encoding.Unicode.GetBytes(counter);
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000AEDCC File Offset: 0x000ACFCC
		internal unsafe override void GetBytes(ref byte* outputPointer)
		{
			uint* ptr = outputPointer;
			*(ptr++) = base.Feature;
			*(ptr++) = base.SubFeature;
			*(ptr++) = base.Collection;
			*(ptr++) = (uint)this.bytes.Length;
			outputPointer = ptr;
			byte[] array;
			byte* ptr2;
			if ((array = this.bytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			byte* ptr3 = ptr2;
			int num = 0;
			while ((long)num < (long)((ulong)this.ByteCount))
			{
				byte* ptr4 = outputPointer;
				outputPointer = ptr4 + 1;
				*ptr4 = *(ptr3++);
				num++;
			}
			array = null;
			ptr = outputPointer;
			*(ptr++) = base.Value;
			outputPointer = ptr;
		}

		// Token: 0x04001D9A RID: 7578
		private byte[] bytes;
	}
}
