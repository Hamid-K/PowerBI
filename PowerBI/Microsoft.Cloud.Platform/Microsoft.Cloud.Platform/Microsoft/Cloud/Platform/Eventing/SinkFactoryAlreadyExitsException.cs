using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x020003A7 RID: 935
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class SinkFactoryAlreadyExitsException : MonitoredException
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x0006CC94 File Offset: 0x0006AE94
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x0006CC9C File Offset: 0x0006AE9C
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x0006CCA4 File Offset: 0x0006AEA4
		public ISinkFactory Factory
		{
			get
			{
				return this.m_factory;
			}
			protected set
			{
				this.m_factory = value;
			}
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0006CCAD File Offset: 0x0006AEAD
		public SinkFactoryAlreadyExitsException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<ISinkFactory>();
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0006CCC1 File Offset: 0x0006AEC1
		public SinkFactoryAlreadyExitsException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0006CCD8 File Offset: 0x0006AED8
		public SinkFactoryAlreadyExitsException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0006CCF8 File Offset: 0x0006AEF8
		protected SinkFactoryAlreadyExitsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("SinkFactoryAlreadyExitsException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Factory = (ISinkFactory)info.GetValue("SinkFactoryAlreadyExitsException_Factory", typeof(ISinkFactory));
			}
			catch (SerializationException)
			{
				this.Factory = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("SinkFactoryAlreadyExitsException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0006CDCC File Offset: 0x0006AFCC
		public SinkFactoryAlreadyExitsException(ISinkFactory factory)
		{
			this.Factory = factory;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0006CDE2 File Offset: 0x0006AFE2
		public SinkFactoryAlreadyExitsException(ISinkFactory factory, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Factory = factory;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0006CE00 File Offset: 0x0006B000
		public SinkFactoryAlreadyExitsException(ISinkFactory factory, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Factory = factory;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0006CE24 File Offset: 0x0006B024
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0006CE5B File Offset: 0x0006B05B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0006CE64 File Offset: 0x0006B064
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(SinkFactoryAlreadyExitsException))
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<EventingTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0006CF34 File Offset: 0x0006B134
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("SinkFactoryAlreadyExitsException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("SinkFactoryAlreadyExitsException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Factory != null)
			{
				info.AddValue("SinkFactoryAlreadyExitsException_Factory", this.Factory, typeof(ISinkFactory));
			}
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0006CFB4 File Offset: 0x0006B1B4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Factory for protocol '{0}' is already registered", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.Factory != null) ? this.Factory.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Factory != null) ? this.Factory.MarkIfInternal() : string.Empty) : ((this.Factory != null) ? this.Factory.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0006D038 File Offset: 0x0006B238
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

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0006D058 File Offset: 0x0006B258
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Factory={0}", new object[] { (this.Factory != null) ? this.Factory.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Factory={0}", new object[] { (this.Factory != null) ? this.Factory.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Factory={0}", new object[] { (this.Factory != null) ? this.Factory.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0006D137 File Offset: 0x0006B337
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0006D140 File Offset: 0x0006B340
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0006D149 File Offset: 0x0006B349
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0006D137 File Offset: 0x0006B337
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0006D154 File Offset: 0x0006B354
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

		// Token: 0x040009AA RID: 2474
		private string creationMessage;

		// Token: 0x040009AB RID: 2475
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040009AC RID: 2476
		private ISinkFactory m_factory;
	}
}
