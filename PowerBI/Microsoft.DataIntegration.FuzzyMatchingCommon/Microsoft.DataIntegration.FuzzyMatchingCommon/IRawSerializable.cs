using System;
using System.IO;
using System.Runtime.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000025 RID: 37
	public interface IRawSerializable : ISerializable
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BF RID: 191
		// (set) Token: 0x060000C0 RID: 192
		bool EnableRawSerialization { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C1 RID: 193
		// (set) Token: 0x060000C2 RID: 194
		int RawSerializationID { get; set; }

		// Token: 0x060000C3 RID: 195
		void Serialize(Stream stream);

		// Token: 0x060000C4 RID: 196
		void Deserialize(Stream stream);
	}
}
