using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Json
{
	// Token: 0x020001E1 RID: 481
	[CLSCompliant(false)]
	public interface IJsonWriter
	{
		// Token: 0x060012D3 RID: 4819
		void StartPaddingFunctionScope();

		// Token: 0x060012D4 RID: 4820
		void EndPaddingFunctionScope();

		// Token: 0x060012D5 RID: 4821
		void StartObjectScope();

		// Token: 0x060012D6 RID: 4822
		void EndObjectScope();

		// Token: 0x060012D7 RID: 4823
		void StartArrayScope();

		// Token: 0x060012D8 RID: 4824
		void EndArrayScope();

		// Token: 0x060012D9 RID: 4825
		void WriteName(string name);

		// Token: 0x060012DA RID: 4826
		void WritePaddingFunctionName(string functionName);

		// Token: 0x060012DB RID: 4827
		void WriteValue(bool value);

		// Token: 0x060012DC RID: 4828
		void WriteValue(int value);

		// Token: 0x060012DD RID: 4829
		void WriteValue(float value);

		// Token: 0x060012DE RID: 4830
		void WriteValue(short value);

		// Token: 0x060012DF RID: 4831
		void WriteValue(long value);

		// Token: 0x060012E0 RID: 4832
		void WriteValue(double value);

		// Token: 0x060012E1 RID: 4833
		void WriteValue(Guid value);

		// Token: 0x060012E2 RID: 4834
		void WriteValue(decimal value);

		// Token: 0x060012E3 RID: 4835
		void WriteValue(DateTimeOffset value);

		// Token: 0x060012E4 RID: 4836
		void WriteValue(TimeSpan value);

		// Token: 0x060012E5 RID: 4837
		void WriteValue(byte value);

		// Token: 0x060012E6 RID: 4838
		void WriteValue(sbyte value);

		// Token: 0x060012E7 RID: 4839
		void WriteValue(string value);

		// Token: 0x060012E8 RID: 4840
		void WriteValue(byte[] value);

		// Token: 0x060012E9 RID: 4841
		void WriteValue(Date value);

		// Token: 0x060012EA RID: 4842
		void WriteValue(TimeOfDay value);

		// Token: 0x060012EB RID: 4843
		void WriteRawValue(string rawValue);

		// Token: 0x060012EC RID: 4844
		void Flush();
	}
}
