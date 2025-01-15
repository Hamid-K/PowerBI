using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200012E RID: 302
	internal class EncryptionAlgorithmsHelper : OptionsHelper<EncryptionAlgorithm>
	{
		// Token: 0x060014C2 RID: 5314 RVA: 0x00090DB0 File Offset: 0x0008EFB0
		private EncryptionAlgorithmsHelper()
		{
			base.AddOptionMapping(EncryptionAlgorithm.RC2, "RC2");
			base.AddOptionMapping(EncryptionAlgorithm.RC4, "RC4");
			base.AddOptionMapping(EncryptionAlgorithm.RC4_128, "RC4_128");
			base.AddOptionMapping(EncryptionAlgorithm.Des, "DES");
			base.AddOptionMapping(EncryptionAlgorithm.TripleDes, "TRIPLE_DES");
			base.AddOptionMapping(EncryptionAlgorithm.DesX, "DESX");
			base.AddOptionMapping(EncryptionAlgorithm.Aes128, "AES_128");
			base.AddOptionMapping(EncryptionAlgorithm.Aes192, "AES_192");
			base.AddOptionMapping(EncryptionAlgorithm.Aes256, "AES_256");
			base.AddOptionMapping(EncryptionAlgorithm.Rsa512, "RSA_512");
			base.AddOptionMapping(EncryptionAlgorithm.Rsa1024, "RSA_1024");
			base.AddOptionMapping(EncryptionAlgorithm.Rsa2048, "RSA_2048");
		}

		// Token: 0x0400115A RID: 4442
		internal static readonly EncryptionAlgorithmsHelper Instance = new EncryptionAlgorithmsHelper();
	}
}
