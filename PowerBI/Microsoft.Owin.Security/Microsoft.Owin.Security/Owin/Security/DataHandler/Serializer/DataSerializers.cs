using System;

namespace Microsoft.Owin.Security.DataHandler.Serializer
{
	// Token: 0x0200002D RID: 45
	public static class DataSerializers
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003619 File Offset: 0x00001819
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003620 File Offset: 0x00001820
		public static IDataSerializer<AuthenticationProperties> Properties { get; set; } = new PropertiesSerializer();

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003628 File Offset: 0x00001828
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000362F File Offset: 0x0000182F
		public static IDataSerializer<AuthenticationTicket> Ticket { get; set; } = new TicketSerializer();
	}
}
