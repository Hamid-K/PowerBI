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
	// Token: 0x020003A6 RID: 934
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class SinkFactoryNotFoundException : MonitoredException
	{
		// Token: 0x06001CAD RID: 7341 RVA: 0x0006C5E8 File Offset: 0x0006A7E8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x0006C5F0 File Offset: 0x0006A7F0
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x0006C5F8 File Offset: 0x0006A7F8
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

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0006C601 File Offset: 0x0006A801
		public SinkFactoryNotFoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<ISinkFactory>();
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0006C615 File Offset: 0x0006A815
		public SinkFactoryNotFoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0006C62C File Offset: 0x0006A82C
		public SinkFactoryNotFoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0006C64C File Offset: 0x0006A84C
		protected SinkFactoryNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("SinkFactoryNotFoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Factory = (ISinkFactory)info.GetValue("SinkFactoryNotFoundException_Factory", typeof(ISinkFactory));
			}
			catch (SerializationException)
			{
				this.Factory = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("SinkFactoryNotFoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0006C720 File Offset: 0x0006A920
		public SinkFactoryNotFoundException(ISinkFactory factory)
		{
			this.Factory = factory;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0006C736 File Offset: 0x0006A936
		public SinkFactoryNotFoundException(ISinkFactory factory, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Factory = factory;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0006C754 File Offset: 0x0006A954
		public SinkFactoryNotFoundException(ISinkFactory factory, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Factory = factory;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0006C778 File Offset: 0x0006A978
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0006C7AF File Offset: 0x0006A9AF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0006C7B8 File Offset: 0x0006A9B8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(SinkFactoryNotFoundException))
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

		// Token: 0x06001CBA RID: 7354 RVA: 0x0006C888 File Offset: 0x0006AA88
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("SinkFactoryNotFoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("SinkFactoryNotFoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Factory != null)
			{
				info.AddValue("SinkFactoryNotFoundException_Factory", this.Factory, typeof(ISinkFactory));
			}
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0006C908 File Offset: 0x0006AB08
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Factory for protocol '{0}' is not registered", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.Factory != null) ? this.Factory.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Factory != null) ? this.Factory.MarkIfInternal() : string.Empty) : ((this.Factory != null) ? this.Factory.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0006C98C File Offset: 0x0006AB8C
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

		// Token: 0x06001CBD RID: 7357 RVA: 0x0006C9AC File Offset: 0x0006ABAC
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Factory={0}", new object[] { (this.Factory != null) ? this.Factory.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Factory={0}", new object[] { (this.Factory != null) ? this.Factory.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Factory={0}", new object[] { (this.Factory != null) ? this.Factory.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0006CA8B File Offset: 0x0006AC8B
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0006CA94 File Offset: 0x0006AC94
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0006CA9D File Offset: 0x0006AC9D
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0006CA8B File Offset: 0x0006AC8B
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0006CAA8 File Offset: 0x0006ACA8
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

		// Token: 0x040009A7 RID: 2471
		private string creationMessage;

		// Token: 0x040009A8 RID: 2472
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040009A9 RID: 2473
		private ISinkFactory m_factory;
	}
}
