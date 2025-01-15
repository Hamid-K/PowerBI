using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200050A RID: 1290
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationFrameworkAggregatedException : CommunicationFrameworkException
	{
		// Token: 0x060027DB RID: 10203 RVA: 0x0008F63C File Offset: 0x0008D83C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x0008F644 File Offset: 0x0008D844
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x0008F64C File Offset: 0x0008D84C
		public EndpointFault[] Exceptions
		{
			get
			{
				return this.m_exceptions;
			}
			protected set
			{
				this.m_exceptions = value;
			}
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x0008F655 File Offset: 0x0008D855
		public CommunicationFrameworkAggregatedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<EndpointFault[]>();
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x0008F669 File Offset: 0x0008D869
		public CommunicationFrameworkAggregatedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x0008F680 File Offset: 0x0008D880
		public CommunicationFrameworkAggregatedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x0008F6A0 File Offset: 0x0008D8A0
		protected CommunicationFrameworkAggregatedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationFrameworkAggregatedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Exceptions = (EndpointFault[])info.GetValue("CommunicationFrameworkAggregatedException_Exceptions", typeof(EndpointFault[]));
			}
			catch (SerializationException)
			{
				this.Exceptions = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationFrameworkAggregatedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x0008F774 File Offset: 0x0008D974
		public CommunicationFrameworkAggregatedException(EndpointFault[] exceptions)
		{
			this.Exceptions = exceptions;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x0008F78A File Offset: 0x0008D98A
		public CommunicationFrameworkAggregatedException(EndpointFault[] exceptions, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Exceptions = exceptions;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x0008F7A8 File Offset: 0x0008D9A8
		public CommunicationFrameworkAggregatedException(EndpointFault[] exceptions, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Exceptions = exceptions;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x0008F7CC File Offset: 0x0008D9CC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x0008F803 File Offset: 0x0008DA03
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x0008F80C File Offset: 0x0008DA0C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationFrameworkAggregatedException))
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

		// Token: 0x060027E8 RID: 10216 RVA: 0x0008F8DC File Offset: 0x0008DADC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationFrameworkAggregatedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationFrameworkAggregatedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Exceptions != null)
			{
				info.AddValue("CommunicationFrameworkAggregatedException_Exceptions", this.Exceptions, typeof(EndpointFault[]));
			}
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x0008F95A File Offset: 0x0008DB5A
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Aggregated exception was thrown on broadcast operation", new object[0]);
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x0008F971 File Offset: 0x0008DB71
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

		// Token: 0x060027EB RID: 10219 RVA: 0x0008F990 File Offset: 0x0008DB90
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Exceptions={0}", new object[] { (this.Exceptions != null) ? string.Join(Environment.NewLine, new object[] { this.Exceptions }) : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Exceptions={0}", new object[] { (this.Exceptions != null) ? string.Join(Environment.NewLine, new object[] { this.Exceptions }) : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Exceptions={0}", new object[] { (this.Exceptions != null) ? string.Join(Environment.NewLine, new object[] { this.Exceptions }) : string.Empty })));
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x0008FA99 File Offset: 0x0008DC99
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x0008FAA2 File Offset: 0x0008DCA2
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x0008FAAB File Offset: 0x0008DCAB
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x0008FA99 File Offset: 0x0008DC99
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x0008FAB4 File Offset: 0x0008DCB4
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

		// Token: 0x04000DE2 RID: 3554
		private string creationMessage;

		// Token: 0x04000DE3 RID: 3555
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000DE4 RID: 3556
		private EndpointFault[] m_exceptions;
	}
}
