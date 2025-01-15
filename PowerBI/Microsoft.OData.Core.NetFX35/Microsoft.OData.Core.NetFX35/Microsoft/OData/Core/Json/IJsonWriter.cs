using System;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x0200010F RID: 271
	internal interface IJsonWriter
	{
		// Token: 0x06000A36 RID: 2614
		void StartPaddingFunctionScope();

		// Token: 0x06000A37 RID: 2615
		void EndPaddingFunctionScope();

		// Token: 0x06000A38 RID: 2616
		void StartObjectScope();

		// Token: 0x06000A39 RID: 2617
		void EndObjectScope();

		// Token: 0x06000A3A RID: 2618
		void StartArrayScope();

		// Token: 0x06000A3B RID: 2619
		void EndArrayScope();

		// Token: 0x06000A3C RID: 2620
		void WriteName(string name);

		// Token: 0x06000A3D RID: 2621
		void WritePaddingFunctionName(string functionName);

		// Token: 0x06000A3E RID: 2622
		void WriteValue(bool value);

		// Token: 0x06000A3F RID: 2623
		void WriteValue(int value);

		// Token: 0x06000A40 RID: 2624
		void WriteValue(float value);

		// Token: 0x06000A41 RID: 2625
		void WriteValue(short value);

		// Token: 0x06000A42 RID: 2626
		void WriteValue(long value);

		// Token: 0x06000A43 RID: 2627
		void WriteValue(double value);

		// Token: 0x06000A44 RID: 2628
		void WriteValue(Guid value);

		// Token: 0x06000A45 RID: 2629
		void WriteValue(decimal value);

		// Token: 0x06000A46 RID: 2630
		void WriteValue(DateTimeOffset value);

		// Token: 0x06000A47 RID: 2631
		void WriteValue(TimeSpan value);

		// Token: 0x06000A48 RID: 2632
		void WriteValue(byte value);

		// Token: 0x06000A49 RID: 2633
		void WriteValue(sbyte value);

		// Token: 0x06000A4A RID: 2634
		void WriteValue(string value);

		// Token: 0x06000A4B RID: 2635
		void WriteValue(byte[] value);

		// Token: 0x06000A4C RID: 2636
		void WriteValue(Date value);

		// Token: 0x06000A4D RID: 2637
		void WriteValue(TimeOfDay value);

		// Token: 0x06000A4E RID: 2638
		void WriteRawValue(string rawValue);

		// Token: 0x06000A4F RID: 2639
		void Flush();
	}
}
