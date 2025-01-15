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
	// Token: 0x02000417 RID: 1047
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CcsMalformedConfigurationException : ConfigurationException
	{
		// Token: 0x06001FFF RID: 8191 RVA: 0x000783C0 File Offset: 0x000765C0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x000783C8 File Offset: 0x000765C8
		// (set) Token: 0x06002001 RID: 8193 RVA: 0x000783D0 File Offset: 0x000765D0
		public Type ConfigurationType
		{
			get
			{
				return this.m_configurationType;
			}
			protected set
			{
				this.m_configurationType = value;
			}
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000783D9 File Offset: 0x000765D9
		public CcsMalformedConfigurationException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<Type>();
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x000783ED File Offset: 0x000765ED
		public CcsMalformedConfigurationException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00078404 File Offset: 0x00076604
		public CcsMalformedConfigurationException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00078424 File Offset: 0x00076624
		protected CcsMalformedConfigurationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CcsMalformedConfigurationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ConfigurationType = (Type)info.GetValue("CcsMalformedConfigurationException_ConfigurationType", typeof(Type));
			}
			catch (SerializationException)
			{
				this.ConfigurationType = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CcsMalformedConfigurationException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000784F8 File Offset: 0x000766F8
		public CcsMalformedConfigurationException(Type configurationType)
		{
			this.ConfigurationType = configurationType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x0007850E File Offset: 0x0007670E
		public CcsMalformedConfigurationException(Type configurationType, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConfigurationType = configurationType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x0007852C File Offset: 0x0007672C
		public CcsMalformedConfigurationException(Type configurationType, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConfigurationType = configurationType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00078550 File Offset: 0x00076750
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x00078587 File Offset: 0x00076787
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x00078590 File Offset: 0x00076790
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CcsMalformedConfigurationException))
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

		// Token: 0x0600200C RID: 8204 RVA: 0x00078660 File Offset: 0x00076860
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CcsMalformedConfigurationException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CcsMalformedConfigurationException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ConfigurationType != null)
			{
				info.AddValue("CcsMalformedConfigurationException_ConfigurationType", this.ConfigurationType, typeof(Type));
			}
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000786E4 File Offset: 0x000768E4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Configuration of type {0} is not valid", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.ConfigurationType != null) ? this.ConfigurationType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ConfigurationType != null) ? this.ConfigurationType.MarkIfInternal() : string.Empty) : ((this.ConfigurationType != null) ? this.ConfigurationType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x0007877A File Offset: 0x0007697A
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

		// Token: 0x0600200F RID: 8207 RVA: 0x00078798 File Offset: 0x00076998
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ConfigurationType={0}", new object[] { (this.ConfigurationType != null) ? this.ConfigurationType.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ConfigurationType={0}", new object[] { (this.ConfigurationType != null) ? this.ConfigurationType.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ConfigurationType={0}", new object[] { (this.ConfigurationType != null) ? this.ConfigurationType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00078889 File Offset: 0x00076A89
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00078892 File Offset: 0x00076A92
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0007889B File Offset: 0x00076A9B
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00078889 File Offset: 0x00076A89
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x000788A4 File Offset: 0x00076AA4
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

		// Token: 0x04000B1D RID: 2845
		private string creationMessage;

		// Token: 0x04000B1E RID: 2846
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000B1F RID: 2847
		private Type m_configurationType;
	}
}
