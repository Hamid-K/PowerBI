using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Json
{
	// Token: 0x02000213 RID: 531
	[CLSCompliant(false)]
	public interface IJsonWriter
	{
		// Token: 0x0600173A RID: 5946
		void StartPaddingFunctionScope();

		// Token: 0x0600173B RID: 5947
		void EndPaddingFunctionScope();

		// Token: 0x0600173C RID: 5948
		void StartObjectScope();

		// Token: 0x0600173D RID: 5949
		void EndObjectScope();

		// Token: 0x0600173E RID: 5950
		void StartArrayScope();

		// Token: 0x0600173F RID: 5951
		void EndArrayScope();

		// Token: 0x06001740 RID: 5952
		void WriteName(string name);

		// Token: 0x06001741 RID: 5953
		void WritePaddingFunctionName(string functionName);

		// Token: 0x06001742 RID: 5954
		void WriteValue(bool value);

		// Token: 0x06001743 RID: 5955
		void WriteValue(int value);

		// Token: 0x06001744 RID: 5956
		void WriteValue(float value);

		// Token: 0x06001745 RID: 5957
		void WriteValue(short value);

		// Token: 0x06001746 RID: 5958
		void WriteValue(long value);

		// Token: 0x06001747 RID: 5959
		void WriteValue(double value);

		// Token: 0x06001748 RID: 5960
		void WriteValue(Guid value);

		// Token: 0x06001749 RID: 5961
		void WriteValue(decimal value);

		// Token: 0x0600174A RID: 5962
		void WriteValue(DateTimeOffset value);

		// Token: 0x0600174B RID: 5963
		void WriteValue(TimeSpan value);

		// Token: 0x0600174C RID: 5964
		void WriteValue(byte value);

		// Token: 0x0600174D RID: 5965
		void WriteValue(sbyte value);

		// Token: 0x0600174E RID: 5966
		void WriteValue(string value);

		// Token: 0x0600174F RID: 5967
		void WriteValue(byte[] value);

		// Token: 0x06001750 RID: 5968
		void WriteValue(Date value);

		// Token: 0x06001751 RID: 5969
		void WriteValue(TimeOfDay value);

		// Token: 0x06001752 RID: 5970
		void WriteRawValue(string rawValue);

		// Token: 0x06001753 RID: 5971
		void Flush();
	}
}
