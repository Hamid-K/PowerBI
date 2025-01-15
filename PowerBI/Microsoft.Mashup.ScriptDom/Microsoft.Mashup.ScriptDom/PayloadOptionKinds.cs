using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200013D RID: 317
	[Flags]
	internal enum PayloadOptionKinds
	{
		// Token: 0x04001192 RID: 4498
		None = 0,
		// Token: 0x04001193 RID: 4499
		WebMethod = 1,
		// Token: 0x04001194 RID: 4500
		Batches = 2,
		// Token: 0x04001195 RID: 4501
		Wsdl = 4,
		// Token: 0x04001196 RID: 4502
		Sessions = 8,
		// Token: 0x04001197 RID: 4503
		LoginType = 16,
		// Token: 0x04001198 RID: 4504
		SessionTimeout = 32,
		// Token: 0x04001199 RID: 4505
		Database = 64,
		// Token: 0x0400119A RID: 4506
		Namespace = 128,
		// Token: 0x0400119B RID: 4507
		Schema = 256,
		// Token: 0x0400119C RID: 4508
		CharacterSet = 512,
		// Token: 0x0400119D RID: 4509
		HeaderLimit = 1024,
		// Token: 0x0400119E RID: 4510
		Authentication = 2048,
		// Token: 0x0400119F RID: 4511
		Encryption = 4096,
		// Token: 0x040011A0 RID: 4512
		MessageForwarding = 8192,
		// Token: 0x040011A1 RID: 4513
		MessageForwardSize = 16384,
		// Token: 0x040011A2 RID: 4514
		Role = 32768,
		// Token: 0x040011A3 RID: 4515
		SoapOptions = 2047,
		// Token: 0x040011A4 RID: 4516
		ServiceBrokerOptions = 30720,
		// Token: 0x040011A5 RID: 4517
		DatabaseMirroringOptions = 38912
	}
}
