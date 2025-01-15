using System;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000143 RID: 323
	internal interface IJsonWriter
	{
		// Token: 0x06000876 RID: 2166
		void StartPaddingFunctionScope();

		// Token: 0x06000877 RID: 2167
		void EndPaddingFunctionScope();

		// Token: 0x06000878 RID: 2168
		void StartObjectScope();

		// Token: 0x06000879 RID: 2169
		void EndObjectScope();

		// Token: 0x0600087A RID: 2170
		void StartArrayScope();

		// Token: 0x0600087B RID: 2171
		void EndArrayScope();

		// Token: 0x0600087C RID: 2172
		void WriteDataWrapper();

		// Token: 0x0600087D RID: 2173
		void WriteDataArrayName();

		// Token: 0x0600087E RID: 2174
		void WriteName(string name);

		// Token: 0x0600087F RID: 2175
		void WritePaddingFunctionName(string functionName);

		// Token: 0x06000880 RID: 2176
		void WriteValue(bool value);

		// Token: 0x06000881 RID: 2177
		void WriteValue(int value);

		// Token: 0x06000882 RID: 2178
		void WriteValue(float value);

		// Token: 0x06000883 RID: 2179
		void WriteValue(short value);

		// Token: 0x06000884 RID: 2180
		void WriteValue(long value);

		// Token: 0x06000885 RID: 2181
		void WriteValue(double value);

		// Token: 0x06000886 RID: 2182
		void WriteValue(Guid value);

		// Token: 0x06000887 RID: 2183
		void WriteValue(decimal value);

		// Token: 0x06000888 RID: 2184
		void WriteValue(DateTime value, ODataVersion odataVersion);

		// Token: 0x06000889 RID: 2185
		void WriteValue(DateTimeOffset value, ODataVersion odataVersion);

		// Token: 0x0600088A RID: 2186
		void WriteValue(TimeSpan value);

		// Token: 0x0600088B RID: 2187
		void WriteValue(byte value);

		// Token: 0x0600088C RID: 2188
		void WriteValue(sbyte value);

		// Token: 0x0600088D RID: 2189
		void WriteValue(string value);

		// Token: 0x0600088E RID: 2190
		void WriteRawString(string value);

		// Token: 0x0600088F RID: 2191
		void Flush();

		// Token: 0x06000890 RID: 2192
		void WriteValueSeparator();

		// Token: 0x06000891 RID: 2193
		void StartScope(JsonWriter.ScopeType type);
	}
}
