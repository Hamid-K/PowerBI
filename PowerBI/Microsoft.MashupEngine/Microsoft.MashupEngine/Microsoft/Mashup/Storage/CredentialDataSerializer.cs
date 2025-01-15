using System;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200204D RID: 8269
	public static class CredentialDataSerializer
	{
		// Token: 0x0600CA60 RID: 51808 RVA: 0x002871A0 File Offset: 0x002853A0
		public static bool TryDeserialize(byte[] credentialData, out CredentialDataCollection credentialDataCollection)
		{
			return Xml<CredentialDataCollection>.TryDeserializeBytes(credentialData, out credentialDataCollection);
		}

		// Token: 0x0600CA61 RID: 51809 RVA: 0x002871A9 File Offset: 0x002853A9
		public static byte[] Serialize(CredentialDataCollection credentials)
		{
			return Xml<CredentialDataCollection>.SerializeBytes(credentials);
		}
	}
}
