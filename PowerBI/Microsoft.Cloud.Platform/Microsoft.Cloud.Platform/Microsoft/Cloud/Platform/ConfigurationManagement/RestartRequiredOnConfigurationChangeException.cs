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
	// Token: 0x0200041A RID: 1050
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class RestartRequiredOnConfigurationChangeException : ConfigurationException
	{
		// Token: 0x06002037 RID: 8247 RVA: 0x000793B0 File Offset: 0x000775B0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x000793B8 File Offset: 0x000775B8
		// (set) Token: 0x06002039 RID: 8249 RVA: 0x000793C0 File Offset: 0x000775C0
		public string RestartRequiredConfigurationClasses
		{
			get
			{
				return this.m_restartRequiredConfigurationClasses;
			}
			protected set
			{
				this.m_restartRequiredConfigurationClasses = value;
			}
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000793C9 File Offset: 0x000775C9
		public RestartRequiredOnConfigurationChangeException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x000793DD File Offset: 0x000775DD
		public RestartRequiredOnConfigurationChangeException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000793F4 File Offset: 0x000775F4
		public RestartRequiredOnConfigurationChangeException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00079414 File Offset: 0x00077614
		protected RestartRequiredOnConfigurationChangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("RestartRequiredOnConfigurationChangeException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.RestartRequiredConfigurationClasses = (string)info.GetValue("RestartRequiredOnConfigurationChangeException_RestartRequiredConfigurationClasses", typeof(string));
			}
			catch (SerializationException)
			{
				this.RestartRequiredConfigurationClasses = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("RestartRequiredOnConfigurationChangeException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000794E8 File Offset: 0x000776E8
		public RestartRequiredOnConfigurationChangeException(string restartRequiredConfigurationClasses, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.RestartRequiredConfigurationClasses = restartRequiredConfigurationClasses;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x00079506 File Offset: 0x00077706
		public RestartRequiredOnConfigurationChangeException(string restartRequiredConfigurationClasses, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.RestartRequiredConfigurationClasses = restartRequiredConfigurationClasses;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x0007952C File Offset: 0x0007772C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x00079563 File Offset: 0x00077763
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0007956C File Offset: 0x0007776C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(RestartRequiredOnConfigurationChangeException))
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

		// Token: 0x06002043 RID: 8259 RVA: 0x0007963C File Offset: 0x0007783C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("RestartRequiredOnConfigurationChangeException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("RestartRequiredOnConfigurationChangeException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.RestartRequiredConfigurationClasses != null)
			{
				info.AddValue("RestartRequiredOnConfigurationChangeException_RestartRequiredConfigurationClasses", this.RestartRequiredConfigurationClasses, typeof(string));
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000796BC File Offset: 0x000778BC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Notification of configuration change was invoked with one or more types that are not AutoReconfigure: {0}", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.RestartRequiredConfigurationClasses != null) ? this.RestartRequiredConfigurationClasses.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.RestartRequiredConfigurationClasses != null) ? this.RestartRequiredConfigurationClasses.MarkIfInternal() : string.Empty) : ((this.RestartRequiredConfigurationClasses != null) ? this.RestartRequiredConfigurationClasses.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x00079740 File Offset: 0x00077940
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

		// Token: 0x06002046 RID: 8262 RVA: 0x00079760 File Offset: 0x00077960
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "RestartRequiredConfigurationClasses={0}", new object[] { (this.RestartRequiredConfigurationClasses != null) ? this.RestartRequiredConfigurationClasses.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "RestartRequiredConfigurationClasses={0}", new object[] { (this.RestartRequiredConfigurationClasses != null) ? this.RestartRequiredConfigurationClasses.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "RestartRequiredConfigurationClasses={0}", new object[] { (this.RestartRequiredConfigurationClasses != null) ? this.RestartRequiredConfigurationClasses.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0007983F File Offset: 0x00077A3F
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00079848 File Offset: 0x00077A48
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00079851 File Offset: 0x00077A51
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0007983F File Offset: 0x00077A3F
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0007985C File Offset: 0x00077A5C
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

		// Token: 0x04000B24 RID: 2852
		private string creationMessage;

		// Token: 0x04000B25 RID: 2853
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000B26 RID: 2854
		private string m_restartRequiredConfigurationClasses;
	}
}
