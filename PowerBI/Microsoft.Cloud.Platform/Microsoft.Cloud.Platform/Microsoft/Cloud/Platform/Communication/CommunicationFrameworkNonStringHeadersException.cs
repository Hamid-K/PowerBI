using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200050E RID: 1294
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationFrameworkNonStringHeadersException : CommunicationFrameworkECFExtendedResultException
	{
		// Token: 0x0600282C RID: 10284 RVA: 0x00090EA8 File Offset: 0x0008F0A8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x00090EB0 File Offset: 0x0008F0B0
		// (set) Token: 0x0600282E RID: 10286 RVA: 0x00090EB8 File Offset: 0x0008F0B8
		public string ClassName
		{
			get
			{
				return this.m_className;
			}
			protected set
			{
				this.m_className = value;
			}
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x00090EC1 File Offset: 0x0008F0C1
		public CommunicationFrameworkNonStringHeadersException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x00090ED5 File Offset: 0x0008F0D5
		public CommunicationFrameworkNonStringHeadersException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x00090EEC File Offset: 0x0008F0EC
		public CommunicationFrameworkNonStringHeadersException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x00090F0C File Offset: 0x0008F10C
		protected CommunicationFrameworkNonStringHeadersException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationFrameworkNonStringHeadersException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ClassName = (string)info.GetValue("CommunicationFrameworkNonStringHeadersException_ClassName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ClassName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationFrameworkNonStringHeadersException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x00090FE0 File Offset: 0x0008F1E0
		public CommunicationFrameworkNonStringHeadersException(string className, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ClassName = className;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x00090FFE File Offset: 0x0008F1FE
		public CommunicationFrameworkNonStringHeadersException(string className, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ClassName = className;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x00091024 File Offset: 0x0008F224
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x0009105B File Offset: 0x0008F25B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x00091064 File Offset: 0x0008F264
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationFrameworkNonStringHeadersException))
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<CommunicationFrameworkTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x00091134 File Offset: 0x0008F334
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationFrameworkNonStringHeadersException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationFrameworkNonStringHeadersException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ClassName != null)
			{
				info.AddValue("CommunicationFrameworkNonStringHeadersException_ClassName", this.ClassName, typeof(string));
			}
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000911B4 File Offset: 0x0008F3B4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The type {0} is not a valid ECFExtendedResult type since it has non-string properties decorated with ECFResponseHeaderAttribute", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.ClassName != null) ? this.ClassName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ClassName != null) ? this.ClassName.MarkIfInternal() : string.Empty) : ((this.ClassName != null) ? this.ClassName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x00091238 File Offset: 0x0008F438
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

		// Token: 0x0600283B RID: 10299 RVA: 0x00091258 File Offset: 0x0008F458
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ClassName={0}", new object[] { (this.ClassName != null) ? this.ClassName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ClassName={0}", new object[] { (this.ClassName != null) ? this.ClassName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ClassName={0}", new object[] { (this.ClassName != null) ? this.ClassName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x00091337 File Offset: 0x0008F537
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x00091340 File Offset: 0x0008F540
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x00091349 File Offset: 0x0008F549
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00091337 File Offset: 0x0008F537
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x00091354 File Offset: 0x0008F554
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

		// Token: 0x04000DED RID: 3565
		private string creationMessage;

		// Token: 0x04000DEE RID: 3566
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000DEF RID: 3567
		private string m_className;
	}
}
