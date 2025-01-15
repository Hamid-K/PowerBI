using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000321 RID: 801
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class AcquireLockTimeoutException : MonitoredException
	{
		// Token: 0x06001749 RID: 5961 RVA: 0x00055378 File Offset: 0x00053578
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x00055380 File Offset: 0x00053580
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x00055388 File Offset: 0x00053588
		public string Key
		{
			get
			{
				return this.m_key;
			}
			protected set
			{
				this.m_key = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x00055391 File Offset: 0x00053591
		// (set) Token: 0x0600174D RID: 5965 RVA: 0x00055399 File Offset: 0x00053599
		public string Timeout
		{
			get
			{
				return this.m_timeout;
			}
			protected set
			{
				this.m_timeout = value;
			}
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000553A2 File Offset: 0x000535A2
		public AcquireLockTimeoutException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000553BB File Offset: 0x000535BB
		public AcquireLockTimeoutException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000553D2 File Offset: 0x000535D2
		public AcquireLockTimeoutException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000553F0 File Offset: 0x000535F0
		protected AcquireLockTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AcquireLockTimeoutException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Key = (string)info.GetValue("AcquireLockTimeoutException_Key", typeof(string));
			}
			catch (SerializationException)
			{
				this.Key = null;
			}
			try
			{
				this.Timeout = (string)info.GetValue("AcquireLockTimeoutException_Timeout", typeof(string));
			}
			catch (SerializationException)
			{
				this.Timeout = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("AcquireLockTimeoutException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00055500 File Offset: 0x00053700
		public AcquireLockTimeoutException(string key, string timeout)
		{
			this.Key = key;
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0005551D File Offset: 0x0005371D
		public AcquireLockTimeoutException(string key, string timeout, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Key = key;
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00055542 File Offset: 0x00053742
		public AcquireLockTimeoutException(string key, string timeout, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Key = key;
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00055570 File Offset: 0x00053770
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000555A8 File Offset: 0x000537A8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AcquireLockTimeoutException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("AcquireLockTimeoutException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Key != null)
			{
				info.AddValue("AcquireLockTimeoutException_Key", this.Key, typeof(string));
			}
			if (this.Timeout != null)
			{
				info.AddValue("AcquireLockTimeoutException_Timeout", this.Timeout, typeof(string));
			}
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0005564C File Offset: 0x0005384C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to acquire Serializer lock with key '{0}' in specified timeout {1}ms", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Key != null) ? this.Key.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Key != null) ? this.Key.MarkIfInternal() : string.Empty) : ((this.Key != null) ? this.Key.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Timeout != null) ? this.Timeout.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Timeout != null) ? this.Timeout.MarkIfInternal() : string.Empty) : ((this.Timeout != null) ? this.Timeout.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00055734 File Offset: 0x00053934
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Key={0}", new object[] { (this.Key != null) ? this.Key.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Key={0}", new object[] { (this.Key != null) ? this.Key.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Key={0}", new object[] { (this.Key != null) ? this.Key.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Timeout={0}", new object[] { (this.Timeout != null) ? this.Timeout.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Timeout={0}", new object[] { (this.Timeout != null) ? this.Timeout.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Timeout={0}", new object[] { (this.Timeout != null) ? this.Timeout.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000558D6 File Offset: 0x00053AD6
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000558DF File Offset: 0x00053ADF
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x000558E8 File Offset: 0x00053AE8
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x000558D6 File Offset: 0x00053AD6
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x000558F4 File Offset: 0x00053AF4
		private string ToString(PrivateInformationMarkupKind markupKind)
		{
			string text = "[" + ExceptionsTemplateHelper.MagicLevel.ToString(CultureInfo.CurrentCulture) + "]" + base.GetType().FullName;
			string text2 = this.CreateMessageFromTemplate(markupKind);
			string text3 = text + ": ";
			if (string.IsNullOrEmpty(this.creationMessage))
			{
				text3 += text2;
			}
			else
			{
				if (markupKind == PrivateInformationMarkupKind.Private || markupKind == PrivateInformationMarkupKind.Internal)
				{
					text3 += this.creationMessage.ObfuscatePrivateValue(true);
				}
				else
				{
					text3 += this.creationMessage;
				}
				if (!string.Equals(this.creationMessage, text2))
				{
					text3 = text3 + Environment.NewLine + "  TemplateMessage: " + text2;
				}
			}
			text3 += this.GetPropertiesString(markupKind);
			text3 = text3 + Environment.NewLine + "ExceptionCulprit=" + this.exceptionCulprit.ToString();
			if (base.InnerException != null)
			{
				try
				{
					ExceptionsTemplateHelper.IncrementMagicLevel();
					IMonitoredError monitoredError = base.InnerException as MonitoredException;
					string text4;
					if (markupKind != PrivateInformationMarkupKind.None)
					{
						if (markupKind != PrivateInformationMarkupKind.Internal)
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfPrivate() : monitoredError.ToPrivateString());
							text4 = text4.ObfuscatePrivateValue(true);
						}
						else
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfInternal() : monitoredError.ToInternalString());
						}
					}
					else
					{
						text4 = ((monitoredError == null) ? base.InnerException.ToString() : monitoredError.ToOriginalString());
					}
					text3 = string.Concat(new string[]
					{
						text3,
						" --->",
						Environment.NewLine,
						text4,
						Environment.NewLine,
						"   --- End of inner exception stack trace ---",
						Environment.NewLine,
						"  (",
						text,
						".StackTrace:)"
					});
				}
				finally
				{
					ExceptionsTemplateHelper.DecrementMagicLevel();
				}
			}
			if (this.StackTrace != null)
			{
				text3 = text3 + Environment.NewLine + this.StackTrace;
			}
			return text3;
		}

		// Token: 0x04000820 RID: 2080
		private string creationMessage;

		// Token: 0x04000821 RID: 2081
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000822 RID: 2082
		private string m_key;

		// Token: 0x04000823 RID: 2083
		private string m_timeout;
	}
}
