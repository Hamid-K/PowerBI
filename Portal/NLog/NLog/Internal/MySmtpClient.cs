using System;
using System.Net;
using System.Net.Mail;

namespace NLog.Internal
{
	// Token: 0x0200012B RID: 299
	internal class MySmtpClient : SmtpClient, ISmtpClient, IDisposable
	{
		// Token: 0x06000F06 RID: 3846 RVA: 0x00025880 File Offset: 0x00023A80
		SmtpDeliveryMethod ISmtpClient.get_DeliveryMethod()
		{
			return base.DeliveryMethod;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00025888 File Offset: 0x00023A88
		void ISmtpClient.set_DeliveryMethod(SmtpDeliveryMethod value)
		{
			base.DeliveryMethod = value;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00025891 File Offset: 0x00023A91
		string ISmtpClient.get_Host()
		{
			return base.Host;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00025899 File Offset: 0x00023A99
		void ISmtpClient.set_Host(string value)
		{
			base.Host = value;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x000258A2 File Offset: 0x00023AA2
		int ISmtpClient.get_Port()
		{
			return base.Port;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x000258AA File Offset: 0x00023AAA
		void ISmtpClient.set_Port(int value)
		{
			base.Port = value;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x000258B3 File Offset: 0x00023AB3
		int ISmtpClient.get_Timeout()
		{
			return base.Timeout;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x000258BB File Offset: 0x00023ABB
		void ISmtpClient.set_Timeout(int value)
		{
			base.Timeout = value;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x000258C4 File Offset: 0x00023AC4
		ICredentialsByHost ISmtpClient.get_Credentials()
		{
			return base.Credentials;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000258CC File Offset: 0x00023ACC
		void ISmtpClient.set_Credentials(ICredentialsByHost value)
		{
			base.Credentials = value;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000258D5 File Offset: 0x00023AD5
		bool ISmtpClient.get_EnableSsl()
		{
			return base.EnableSsl;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x000258DD File Offset: 0x00023ADD
		void ISmtpClient.set_EnableSsl(bool value)
		{
			base.EnableSsl = value;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000258E6 File Offset: 0x00023AE6
		void ISmtpClient.Send(MailMessage msg)
		{
			base.Send(msg);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x000258EF File Offset: 0x00023AEF
		string ISmtpClient.get_PickupDirectoryLocation()
		{
			return base.PickupDirectoryLocation;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x000258F7 File Offset: 0x00023AF7
		void ISmtpClient.set_PickupDirectoryLocation(string value)
		{
			base.PickupDirectoryLocation = value;
		}
	}
}
