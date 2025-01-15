using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x0200041C RID: 1052
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CcsConfigurationChangeNotificationException : ConfigurationException
	{
		// Token: 0x0600205D RID: 8285 RVA: 0x00079ED8 File Offset: 0x000780D8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00079EE0 File Offset: 0x000780E0
		public CcsConfigurationChangeNotificationException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00079EEF File Offset: 0x000780EF
		public CcsConfigurationChangeNotificationException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00079F06 File Offset: 0x00078106
		public CcsConfigurationChangeNotificationException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00079F24 File Offset: 0x00078124
		protected CcsConfigurationChangeNotificationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CcsConfigurationChangeNotificationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CcsConfigurationChangeNotificationException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00079FC0 File Offset: 0x000781C0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00079FF7 File Offset: 0x000781F7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x0007A000 File Offset: 0x00078200
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CcsConfigurationChangeNotificationException))
			{
				TraceSourceBase<ConfigurationTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ConfigurationTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ConfigurationTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0007A0D0 File Offset: 0x000782D0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CcsConfigurationChangeNotificationException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CcsConfigurationChangeNotificationException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x0007A12B File Offset: 0x0007832B
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "CCS configuration change notification failed", new object[0]);
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x0007A142 File Offset: 0x00078342
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

		// Token: 0x06002068 RID: 8296 RVA: 0x00076F47 File Offset: 0x00075147
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x0007A15F File Offset: 0x0007835F
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x0007A168 File Offset: 0x00078368
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0007A171 File Offset: 0x00078371
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0007A15F File Offset: 0x0007835F
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x0007A17C File Offset: 0x0007837C
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

		// Token: 0x04000B29 RID: 2857
		private string creationMessage;

		// Token: 0x04000B2A RID: 2858
		private ExceptionCulprit exceptionCulprit;
	}
}
