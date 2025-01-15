using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class EncryptCredentialsWithTestConnectionResult : TestDataSourceConnectionResult
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002661 File Offset: 0x00000861
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002669 File Offset: 0x00000869
		[DataMember(Name = "encryptedCredentials")]
		internal string SymmetricKeyEncryptedCredentials { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002672 File Offset: 0x00000872
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000267A File Offset: 0x0000087A
		[DataMember(Name = "encryptionAlgorithm")]
		internal string EncryptionAlgorithm { get; set; }
	}
}
