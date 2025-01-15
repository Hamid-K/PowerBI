using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000094 RID: 148
	internal class DatabaseEncryptionKeyAlgorithmHelper : OptionsHelper<DatabaseEncryptionKeyAlgorithm>
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000B877 File Offset: 0x00009A77
		private DatabaseEncryptionKeyAlgorithmHelper()
		{
			base.AddOptionMapping(DatabaseEncryptionKeyAlgorithm.Aes128, "AES_128");
			base.AddOptionMapping(DatabaseEncryptionKeyAlgorithm.Aes192, "AES_192");
			base.AddOptionMapping(DatabaseEncryptionKeyAlgorithm.Aes256, "AES_256");
			base.AddOptionMapping(DatabaseEncryptionKeyAlgorithm.TripleDes3Key, "TRIPLE_DES_3KEY");
		}

		// Token: 0x040003A2 RID: 930
		internal static readonly DatabaseEncryptionKeyAlgorithmHelper Instance = new DatabaseEncryptionKeyAlgorithmHelper();
	}
}
