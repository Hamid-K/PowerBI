using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using JetBrains.Annotations;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000045 RID: 69
	[Target("Mail")]
	public class MailTarget : TargetWithLayoutHeaderAndFooter
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x000110C8 File Offset: 0x0000F2C8
		public MailTarget()
		{
			this.Body = "${message}${newline}";
			this.Subject = "Message from NLog on ${machinename}";
			this.Encoding = Encoding.UTF8;
			this.SmtpPort = 25;
			this.SmtpAuthentication = SmtpAuthenticationMode.None;
			this.Timeout = 10000;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00011120 File Offset: 0x0000F320
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00011194 File Offset: 0x0000F394
		internal SmtpSection SmtpSection
		{
			get
			{
				if (this._currentailSettings == null)
				{
					try
					{
						this._currentailSettings = global::System.Configuration.ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;
					}
					catch (Exception ex)
					{
						InternalLogger.Warn(ex, "MailTarget(Name={0}): Reading 'From' from .config failed.", new object[] { base.Name });
						if (ex.MustBeRethrown())
						{
							throw;
						}
						this._currentailSettings = new SmtpSection();
					}
				}
				return this._currentailSettings;
			}
			set
			{
				this._currentailSettings = value;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001119D File Offset: 0x0000F39D
		public MailTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000111AC File Offset: 0x0000F3AC
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x000111D5 File Offset: 0x0000F3D5
		public Layout From
		{
			get
			{
				if (this.UseSystemNetMailSettings && this._from == null)
				{
					return this.SmtpSection.From;
				}
				return this._from;
			}
			set
			{
				this._from = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x000111DE File Offset: 0x0000F3DE
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x000111E6 File Offset: 0x0000F3E6
		[RequiredParameter]
		public Layout To { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x000111EF File Offset: 0x0000F3EF
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x000111F7 File Offset: 0x0000F3F7
		public Layout CC { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00011200 File Offset: 0x0000F400
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x00011208 File Offset: 0x0000F408
		public Layout Bcc { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00011211 File Offset: 0x0000F411
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x00011219 File Offset: 0x0000F419
		public bool AddNewLines { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00011222 File Offset: 0x0000F422
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0001122A File Offset: 0x0000F42A
		[DefaultValue("Message from NLog on ${machinename}")]
		[RequiredParameter]
		public Layout Subject { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00011233 File Offset: 0x0000F433
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0001123B File Offset: 0x0000F43B
		[DefaultValue("${message}${newline}")]
		public Layout Body
		{
			get
			{
				return this.Layout;
			}
			set
			{
				this.Layout = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00011244 File Offset: 0x0000F444
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0001124C File Offset: 0x0000F44C
		[DefaultValue("UTF8")]
		public Encoding Encoding { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00011255 File Offset: 0x0000F455
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0001125D File Offset: 0x0000F45D
		[DefaultValue(false)]
		public bool Html { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00011266 File Offset: 0x0000F466
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x0001126E File Offset: 0x0000F46E
		public Layout SmtpServer { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00011277 File Offset: 0x0000F477
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x0001127F File Offset: 0x0000F47F
		[DefaultValue("None")]
		public SmtpAuthenticationMode SmtpAuthentication { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00011288 File Offset: 0x0000F488
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x00011290 File Offset: 0x0000F490
		public Layout SmtpUserName { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00011299 File Offset: 0x0000F499
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x000112A1 File Offset: 0x0000F4A1
		public Layout SmtpPassword { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x000112AA File Offset: 0x0000F4AA
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x000112B2 File Offset: 0x0000F4B2
		[DefaultValue(false)]
		public bool EnableSsl { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x000112BB File Offset: 0x0000F4BB
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x000112C3 File Offset: 0x0000F4C3
		[DefaultValue(25)]
		public int SmtpPort { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x000112CC File Offset: 0x0000F4CC
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x000112D4 File Offset: 0x0000F4D4
		[DefaultValue(false)]
		public bool UseSystemNetMailSettings { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x000112DD File Offset: 0x0000F4DD
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x000112E5 File Offset: 0x0000F4E5
		[DefaultValue(SmtpDeliveryMethod.Network)]
		public SmtpDeliveryMethod DeliveryMethod { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x000112EE File Offset: 0x0000F4EE
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x000112F6 File Offset: 0x0000F4F6
		[DefaultValue(null)]
		public string PickupDirectoryLocation { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x000112FF File Offset: 0x0000F4FF
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x00011307 File Offset: 0x0000F507
		public Layout Priority { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00011310 File Offset: 0x0000F510
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00011318 File Offset: 0x0000F518
		[DefaultValue(false)]
		public bool ReplaceNewlineWithBrTagInHtml { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00011321 File Offset: 0x0000F521
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x00011329 File Offset: 0x0000F529
		[DefaultValue(10000)]
		public int Timeout { get; set; }

		// Token: 0x06000701 RID: 1793 RVA: 0x00011332 File Offset: 0x0000F532
		internal virtual ISmtpClient CreateSmtpClient()
		{
			return new MySmtpClient();
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00011339 File Offset: 0x0000F539
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			this.Write(new AsyncLogEventInfo[] { logEvent });
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001134F File Offset: 0x0000F54F
		[Obsolete("Instead override Write(IList<AsyncLogEventInfo> logEvents. Marked obsolete on NLog 4.5")]
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			this.Write(logEvents);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00011358 File Offset: 0x0000F558
		protected override void Write(IList<AsyncLogEventInfo> logEvents)
		{
			foreach (KeyValuePair<string, IList<AsyncLogEventInfo>> keyValuePair in logEvents.BucketSort((AsyncLogEventInfo c) => this.GetSmtpSettingsKey(c.LogEvent)))
			{
				IList<AsyncLogEventInfo> value = keyValuePair.Value;
				this.ProcessSingleMailMessage(value);
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000113C4 File Offset: 0x0000F5C4
		protected override void InitializeTarget()
		{
			this.CheckRequiredParameters();
			base.InitializeTarget();
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000113D4 File Offset: 0x0000F5D4
		private void ProcessSingleMailMessage([NotNull] IList<AsyncLogEventInfo> events)
		{
			try
			{
				if (events.Count == 0)
				{
					throw new NLogRuntimeException("We need at least one event.");
				}
				LogEventInfo logEvent = events[0].LogEvent;
				LogEventInfo logEvent2 = events[events.Count - 1].LogEvent;
				StringBuilder stringBuilder = this.CreateBodyBuffer(events, logEvent, logEvent2);
				using (MailMessage mailMessage = this.CreateMailMessage(logEvent2, stringBuilder.ToString()))
				{
					using (ISmtpClient smtpClient = this.CreateSmtpClient())
					{
						if (!this.UseSystemNetMailSettings)
						{
							this.ConfigureMailClient(logEvent2, smtpClient);
						}
						if (smtpClient.EnableSsl)
						{
							InternalLogger.Debug("MailTarget(Name={0}): Sending mail to {1} using {2}:{3} (ssl=true)", new object[] { base.Name, mailMessage.To, smtpClient.Host, smtpClient.Port });
						}
						else
						{
							InternalLogger.Debug("MailTarget(Name={0}): Sending mail to {1} using {2}:{3} (ssl=false)", new object[] { base.Name, mailMessage.To, smtpClient.Host, smtpClient.Port });
						}
						InternalLogger.Trace<string, string>("MailTarget(Name={0}):   Subject: '{1}'", base.Name, mailMessage.Subject);
						InternalLogger.Trace<string, string>("MailTarget(Name={0}):   From: '{1}'", base.Name, mailMessage.From.ToString());
						smtpClient.Send(mailMessage);
						foreach (AsyncLogEventInfo asyncLogEventInfo in events)
						{
							asyncLogEventInfo.Continuation(null);
						}
					}
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "MailTarget(Name={0}): Error sending mail.", new object[] { base.Name });
				if (ex.MustBeRethrown())
				{
					throw;
				}
				foreach (AsyncLogEventInfo asyncLogEventInfo2 in events)
				{
					asyncLogEventInfo2.Continuation(ex);
				}
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00011640 File Offset: 0x0000F840
		private StringBuilder CreateBodyBuffer(IEnumerable<AsyncLogEventInfo> events, LogEventInfo firstEvent, LogEventInfo lastEvent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (base.Header != null)
			{
				stringBuilder.Append(base.Header.Render(firstEvent));
				if (this.AddNewLines)
				{
					stringBuilder.Append("\n");
				}
			}
			foreach (AsyncLogEventInfo asyncLogEventInfo in events)
			{
				stringBuilder.Append(this.Layout.Render(asyncLogEventInfo.LogEvent));
				if (this.AddNewLines)
				{
					stringBuilder.Append("\n");
				}
			}
			if (base.Footer != null)
			{
				stringBuilder.Append(base.Footer.Render(lastEvent));
				if (this.AddNewLines)
				{
					stringBuilder.Append("\n");
				}
			}
			return stringBuilder;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00011714 File Offset: 0x0000F914
		internal void ConfigureMailClient(LogEventInfo lastEvent, ISmtpClient client)
		{
			this.CheckRequiredParameters();
			if (this.SmtpServer == null && string.IsNullOrEmpty(this.PickupDirectoryLocation))
			{
				throw new NLogRuntimeException(string.Format("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", "SmtpServer/PickupDirectoryLocation"));
			}
			if (this.DeliveryMethod == SmtpDeliveryMethod.Network && this.SmtpServer == null)
			{
				throw new NLogRuntimeException(string.Format("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", "SmtpServer"));
			}
			if (this.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory && string.IsNullOrEmpty(this.PickupDirectoryLocation))
			{
				throw new NLogRuntimeException(string.Format("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", "PickupDirectoryLocation"));
			}
			if (this.SmtpServer != null && this.DeliveryMethod == SmtpDeliveryMethod.Network)
			{
				string text = this.SmtpServer.Render(lastEvent);
				if (string.IsNullOrEmpty(text))
				{
					throw new NLogRuntimeException(string.Format("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", "SmtpServer"));
				}
				client.Host = text;
				client.Port = this.SmtpPort;
				client.EnableSsl = this.EnableSsl;
				if (this.SmtpAuthentication == SmtpAuthenticationMode.Ntlm)
				{
					InternalLogger.Trace<string>("MailTarget(Name={0}):   Using NTLM authentication.", base.Name);
					client.Credentials = CredentialCache.DefaultNetworkCredentials;
				}
				else if (this.SmtpAuthentication == SmtpAuthenticationMode.Basic)
				{
					string text2 = this.SmtpUserName.Render(lastEvent);
					string text3 = this.SmtpPassword.Render(lastEvent);
					InternalLogger.Trace<string, string, string>("MailTarget(Name={0}):   Using basic authentication: Username='{1}' Password='{2}'", base.Name, text2, new string('*', text3.Length));
					client.Credentials = new NetworkCredential(text2, text3);
				}
			}
			if (!string.IsNullOrEmpty(this.PickupDirectoryLocation) && this.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
			{
				client.PickupDirectoryLocation = MailTarget.ConvertDirectoryLocation(this.PickupDirectoryLocation);
			}
			client.DeliveryMethod = this.DeliveryMethod;
			client.Timeout = this.Timeout;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000118B4 File Offset: 0x0000FAB4
		internal static string ConvertDirectoryLocation(string pickupDirectoryLocation)
		{
			if (!pickupDirectoryLocation.StartsWith("~/"))
			{
				return pickupDirectoryLocation;
			}
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string text = pickupDirectoryLocation.Substring("~/".Length).Replace('/', Path.DirectorySeparatorChar);
			return Path.Combine(baseDirectory, text);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00011900 File Offset: 0x0000FB00
		private void CheckRequiredParameters()
		{
			if (!this.UseSystemNetMailSettings && this.SmtpServer == null && this.DeliveryMethod == SmtpDeliveryMethod.Network)
			{
				throw new NLogConfigurationException("The MailTarget's '{0}' properties are not set - but needed because useSystemNetMailSettings=false and DeliveryMethod=Network. The email message will not be sent.", new object[] { "SmtpServer" });
			}
			if (!this.UseSystemNetMailSettings && string.IsNullOrEmpty(this.PickupDirectoryLocation) && this.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
			{
				throw new NLogConfigurationException("The MailTarget's '{0}' properties are not set - but needed because useSystemNetMailSettings=false and DeliveryMethod=SpecifiedPickupDirectory. The email message will not be sent.", new object[] { "PickupDirectoryLocation" });
			}
			if (this.From == null)
			{
				throw new NLogConfigurationException("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", new object[] { "From" });
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00011998 File Offset: 0x0000FB98
		private string GetSmtpSettingsKey(LogEventInfo logEvent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			MailTarget.AppendLayout(stringBuilder, logEvent, this.From);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.To);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.CC);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.Bcc);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.SmtpServer);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.SmtpPassword);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.SmtpUserName);
			return stringBuilder.ToString();
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00011A0A File Offset: 0x0000FC0A
		private static void AppendLayout(StringBuilder sb, LogEventInfo logEvent, Layout layout)
		{
			sb.Append("|");
			if (layout != null)
			{
				sb.Append(layout.Render(logEvent));
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00011A2C File Offset: 0x0000FC2C
		private MailMessage CreateMailMessage(LogEventInfo lastEvent, string body)
		{
			MailMessage mailMessage = new MailMessage();
			Layout from = this.From;
			string text = ((from != null) ? from.Render(lastEvent) : null);
			if (string.IsNullOrEmpty(text))
			{
				throw new NLogRuntimeException("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", new object[] { "From" });
			}
			mailMessage.From = new MailAddress(text);
			bool flag = MailTarget.AddAddresses(mailMessage.To, this.To, lastEvent);
			bool flag2 = MailTarget.AddAddresses(mailMessage.CC, this.CC, lastEvent);
			bool flag3 = MailTarget.AddAddresses(mailMessage.Bcc, this.Bcc, lastEvent);
			if (!flag && !flag2 && !flag3)
			{
				throw new NLogRuntimeException("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", new object[] { "To/Cc/Bcc" });
			}
			mailMessage.Subject = ((this.Subject == null) ? string.Empty : this.Subject.Render(lastEvent).Trim());
			mailMessage.BodyEncoding = this.Encoding;
			mailMessage.IsBodyHtml = this.Html;
			if (this.Priority != null)
			{
				string text2 = this.Priority.Render(lastEvent);
				MailPriority mailPriority;
				if (ConversionHelpers.TryParseEnum<MailPriority>(text2, out mailPriority, MailPriority.Normal))
				{
					mailMessage.Priority = mailPriority;
				}
				else
				{
					mailMessage.Priority = MailPriority.Normal;
					InternalLogger.Warn<string, string>("MailTarget(Name={0}): Could not convert '{1}' to MailPriority, valid values are Low, Normal and High. Using normal priority as fallback.", base.Name, text2);
				}
			}
			mailMessage.Body = body;
			if (mailMessage.IsBodyHtml && this.ReplaceNewlineWithBrTagInHtml && mailMessage.Body != null)
			{
				mailMessage.Body = mailMessage.Body.Replace(EnvironmentHelper.NewLine, "<br/>");
			}
			return mailMessage;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00011B98 File Offset: 0x0000FD98
		private static bool AddAddresses(MailAddressCollection mailAddressCollection, Layout layout, LogEventInfo logEvent)
		{
			bool flag = false;
			if (layout != null)
			{
				foreach (string text in layout.Render(logEvent).SplitAndTrimTokens(';'))
				{
					mailAddressCollection.Add(text);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x04000134 RID: 308
		private const string RequiredPropertyIsEmptyFormat = "After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.";

		// Token: 0x04000135 RID: 309
		private Layout _from;

		// Token: 0x04000136 RID: 310
		private SmtpSection _currentailSettings;
	}
}
