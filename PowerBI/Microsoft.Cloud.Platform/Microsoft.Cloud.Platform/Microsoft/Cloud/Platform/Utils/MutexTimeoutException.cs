using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000308 RID: 776
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class MutexTimeoutException : MonitoredException
	{
		// Token: 0x06001555 RID: 5461 RVA: 0x0004BE40 File Offset: 0x0004A040
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0004BE48 File Offset: 0x0004A048
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x0004BE50 File Offset: 0x0004A050
		public double MutexTimeoutInSeconds
		{
			get
			{
				return this.m_mutexTimeoutInSeconds;
			}
			protected set
			{
				this.m_mutexTimeoutInSeconds = value;
			}
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0004BE59 File Offset: 0x0004A059
		public MutexTimeoutException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<double>();
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0004BE6D File Offset: 0x0004A06D
		public MutexTimeoutException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0004BE84 File Offset: 0x0004A084
		public MutexTimeoutException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0004BEA4 File Offset: 0x0004A0A4
		protected MutexTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MutexTimeoutException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.MutexTimeoutInSeconds = (double)info.GetValue("MutexTimeoutException_MutexTimeoutInSeconds", typeof(double));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("MutexTimeoutException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0004BF60 File Offset: 0x0004A160
		public MutexTimeoutException(double mutexTimeoutInSeconds)
		{
			this.MutexTimeoutInSeconds = mutexTimeoutInSeconds;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0004BF76 File Offset: 0x0004A176
		public MutexTimeoutException(double mutexTimeoutInSeconds, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.MutexTimeoutInSeconds = mutexTimeoutInSeconds;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0004BF94 File Offset: 0x0004A194
		public MutexTimeoutException(double mutexTimeoutInSeconds, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.MutexTimeoutInSeconds = mutexTimeoutInSeconds;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0004BFB8 File Offset: 0x0004A1B8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0004BFF0 File Offset: 0x0004A1F0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MutexTimeoutException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("MutexTimeoutException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("MutexTimeoutException_MutexTimeoutInSeconds", this.MutexTimeoutInSeconds, typeof(double));
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0004C06C File Offset: 0x0004A26C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Could not gain mutex in the allotted timeout of {0} seconds.", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? this.MutexTimeoutInSeconds.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.MutexTimeoutInSeconds.ToString(CultureInfo.InvariantCulture) : this.MutexTimeoutInSeconds.ToString(CultureInfo.InvariantCulture)) });
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0004C0D5 File Offset: 0x0004A2D5
		public override string Message
		{
			get
			{
				if (!string.IsNullOrEmpty(this.creationMessage))
				{
					return this.creationMessage;
				}
				return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.None);
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0004C0F4 File Offset: 0x0004A2F4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "MutexTimeoutInSeconds={0}", new object[] { this.MutexTimeoutInSeconds.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "MutexTimeoutInSeconds={0}", new object[] { this.MutexTimeoutInSeconds.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "MutexTimeoutInSeconds={0}", new object[] { this.MutexTimeoutInSeconds.ToString(CultureInfo.InvariantCulture) })));
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0004C1B8 File Offset: 0x0004A3B8
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0004C1C1 File Offset: 0x0004A3C1
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0004C1CA File Offset: 0x0004A3CA
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0004C1B8 File Offset: 0x0004A3B8
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0004C1D4 File Offset: 0x0004A3D4
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

		// Token: 0x040007DA RID: 2010
		private string creationMessage;

		// Token: 0x040007DB RID: 2011
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007DC RID: 2012
		private double m_mutexTimeoutInSeconds;
	}
}
