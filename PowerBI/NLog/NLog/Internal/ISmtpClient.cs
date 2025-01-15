using System;
using System.Net;
using System.Net.Mail;

namespace NLog.Internal
{
	// Token: 0x02000122 RID: 290
	internal interface ISmtpClient : IDisposable
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000ED2 RID: 3794
		// (set) Token: 0x06000ED3 RID: 3795
		SmtpDeliveryMethod DeliveryMethod { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000ED4 RID: 3796
		// (set) Token: 0x06000ED5 RID: 3797
		string Host { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000ED6 RID: 3798
		// (set) Token: 0x06000ED7 RID: 3799
		int Port { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000ED8 RID: 3800
		// (set) Token: 0x06000ED9 RID: 3801
		int Timeout { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000EDA RID: 3802
		// (set) Token: 0x06000EDB RID: 3803
		ICredentialsByHost Credentials { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000EDC RID: 3804
		// (set) Token: 0x06000EDD RID: 3805
		bool EnableSsl { get; set; }

		// Token: 0x06000EDE RID: 3806
		void Send(MailMessage msg);

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000EDF RID: 3807
		// (set) Token: 0x06000EE0 RID: 3808
		string PickupDirectoryLocation { get; set; }
	}
}
