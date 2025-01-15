using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200050B RID: 1291
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationFrameworkInvalidOperationContextException : CommunicationFrameworkException
	{
		// Token: 0x060027F1 RID: 10225 RVA: 0x0008FCA0 File Offset: 0x0008DEA0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x0008FCA8 File Offset: 0x0008DEA8
		// (set) Token: 0x060027F3 RID: 10227 RVA: 0x0008FCB0 File Offset: 0x0008DEB0
		public string OperationName
		{
			get
			{
				return this.m_operationName;
			}
			protected set
			{
				this.m_operationName = value;
			}
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x0008FCB9 File Offset: 0x0008DEB9
		public CommunicationFrameworkInvalidOperationContextException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x0008FCCD File Offset: 0x0008DECD
		public CommunicationFrameworkInvalidOperationContextException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x0008FCE4 File Offset: 0x0008DEE4
		public CommunicationFrameworkInvalidOperationContextException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x0008FD04 File Offset: 0x0008DF04
		protected CommunicationFrameworkInvalidOperationContextException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationFrameworkInvalidOperationContextException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.OperationName = (string)info.GetValue("CommunicationFrameworkInvalidOperationContextException_OperationName", typeof(string));
			}
			catch (SerializationException)
			{
				this.OperationName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationFrameworkInvalidOperationContextException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x0008FDD8 File Offset: 0x0008DFD8
		public CommunicationFrameworkInvalidOperationContextException(string operationName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.OperationName = operationName;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x0008FDF6 File Offset: 0x0008DFF6
		public CommunicationFrameworkInvalidOperationContextException(string operationName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.OperationName = operationName;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x0008FE1C File Offset: 0x0008E01C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x0008FE53 File Offset: 0x0008E053
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x0008FE5C File Offset: 0x0008E05C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationFrameworkInvalidOperationContextException))
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

		// Token: 0x060027FD RID: 10237 RVA: 0x0008FF2C File Offset: 0x0008E12C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationFrameworkInvalidOperationContextException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationFrameworkInvalidOperationContextException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.OperationName != null)
			{
				info.AddValue("CommunicationFrameworkInvalidOperationContextException_OperationName", this.OperationName, typeof(string));
			}
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x0008FFAC File Offset: 0x0008E1AC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failure during {0} while accessing OperationContext properties", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.OperationName != null) ? this.OperationName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.OperationName != null) ? this.OperationName.MarkIfInternal() : string.Empty) : ((this.OperationName != null) ? this.OperationName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x00090030 File Offset: 0x0008E230
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

		// Token: 0x06002800 RID: 10240 RVA: 0x00090050 File Offset: 0x0008E250
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "OperationName={0}", new object[] { (this.OperationName != null) ? this.OperationName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "OperationName={0}", new object[] { (this.OperationName != null) ? this.OperationName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "OperationName={0}", new object[] { (this.OperationName != null) ? this.OperationName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x0009012F File Offset: 0x0008E32F
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x00090138 File Offset: 0x0008E338
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x00090141 File Offset: 0x0008E341
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0009012F File Offset: 0x0008E32F
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x0009014C File Offset: 0x0008E34C
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

		// Token: 0x04000DE5 RID: 3557
		private string creationMessage;

		// Token: 0x04000DE6 RID: 3558
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000DE7 RID: 3559
		private string m_operationName;
	}
}
