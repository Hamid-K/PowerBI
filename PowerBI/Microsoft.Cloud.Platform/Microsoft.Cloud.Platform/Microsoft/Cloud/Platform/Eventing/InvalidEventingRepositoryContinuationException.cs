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
	// Token: 0x020003A9 RID: 937
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class InvalidEventingRepositoryContinuationException : MonitoredException
	{
		// Token: 0x06001CEF RID: 7407 RVA: 0x0006D964 File Offset: 0x0006BB64
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0006D96C File Offset: 0x0006BB6C
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x0006D974 File Offset: 0x0006BB74
		public Type EventingRepositoryType
		{
			get
			{
				return this.m_eventingRepositoryType;
			}
			protected set
			{
				this.m_eventingRepositoryType = value;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x0006D97D File Offset: 0x0006BB7D
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x0006D985 File Offset: 0x0006BB85
		public string ContinuationString
		{
			get
			{
				return this.m_continuationString;
			}
			protected set
			{
				this.m_continuationString = value;
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0006D98E File Offset: 0x0006BB8E
		public InvalidEventingRepositoryContinuationException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<Type>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0006D9A7 File Offset: 0x0006BBA7
		public InvalidEventingRepositoryContinuationException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0006D9BE File Offset: 0x0006BBBE
		public InvalidEventingRepositoryContinuationException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0006D9DC File Offset: 0x0006BBDC
		protected InvalidEventingRepositoryContinuationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidEventingRepositoryContinuationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.EventingRepositoryType = (Type)info.GetValue("InvalidEventingRepositoryContinuationException_EventingRepositoryType", typeof(Type));
			}
			catch (SerializationException)
			{
				this.EventingRepositoryType = null;
			}
			try
			{
				this.ContinuationString = (string)info.GetValue("InvalidEventingRepositoryContinuationException_ContinuationString", typeof(string));
			}
			catch (SerializationException)
			{
				this.ContinuationString = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("InvalidEventingRepositoryContinuationException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0006DAEC File Offset: 0x0006BCEC
		public InvalidEventingRepositoryContinuationException(Type eventingRepositoryType, string continuationString)
		{
			this.EventingRepositoryType = eventingRepositoryType;
			this.ContinuationString = continuationString;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0006DB09 File Offset: 0x0006BD09
		public InvalidEventingRepositoryContinuationException(Type eventingRepositoryType, string continuationString, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.EventingRepositoryType = eventingRepositoryType;
			this.ContinuationString = continuationString;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0006DB2E File Offset: 0x0006BD2E
		public InvalidEventingRepositoryContinuationException(Type eventingRepositoryType, string continuationString, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.EventingRepositoryType = eventingRepositoryType;
			this.ContinuationString = continuationString;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0006DB5C File Offset: 0x0006BD5C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0006DB93 File Offset: 0x0006BD93
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0006DB9C File Offset: 0x0006BD9C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidEventingRepositoryContinuationException))
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

		// Token: 0x06001CFE RID: 7422 RVA: 0x0006DC6C File Offset: 0x0006BE6C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidEventingRepositoryContinuationException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("InvalidEventingRepositoryContinuationException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.EventingRepositoryType != null)
			{
				info.AddValue("InvalidEventingRepositoryContinuationException_EventingRepositoryType", this.EventingRepositoryType, typeof(Type));
			}
			if (this.ContinuationString != null)
			{
				info.AddValue("InvalidEventingRepositoryContinuationException_ContinuationString", this.ContinuationString, typeof(string));
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0006DD14 File Offset: 0x0006BF14
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Cannot deserialize continuation string. Invalid continuation string for '{0}'. Continuation: {1}", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.EventingRepositoryType != null) ? this.EventingRepositoryType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.EventingRepositoryType != null) ? this.EventingRepositoryType.MarkIfInternal() : string.Empty) : ((this.EventingRepositoryType != null) ? this.EventingRepositoryType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.ContinuationString != null) ? this.ContinuationString.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ContinuationString != null) ? this.ContinuationString.MarkIfInternal() : string.Empty) : ((this.ContinuationString != null) ? this.ContinuationString.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0006DE0C File Offset: 0x0006C00C
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

		// Token: 0x06001D01 RID: 7425 RVA: 0x0006DE2C File Offset: 0x0006C02C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EventingRepositoryType={0}", new object[] { (this.EventingRepositoryType != null) ? this.EventingRepositoryType.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EventingRepositoryType={0}", new object[] { (this.EventingRepositoryType != null) ? this.EventingRepositoryType.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "EventingRepositoryType={0}", new object[] { (this.EventingRepositoryType != null) ? this.EventingRepositoryType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ContinuationString={0}", new object[] { (this.ContinuationString != null) ? this.ContinuationString.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ContinuationString={0}", new object[] { (this.ContinuationString != null) ? this.ContinuationString.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ContinuationString={0}", new object[] { (this.ContinuationString != null) ? this.ContinuationString.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0006DFE0 File Offset: 0x0006C1E0
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0006DFE9 File Offset: 0x0006C1E9
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0006DFF2 File Offset: 0x0006C1F2
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0006DFE0 File Offset: 0x0006C1E0
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0006DFFC File Offset: 0x0006C1FC
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

		// Token: 0x040009B0 RID: 2480
		private string creationMessage;

		// Token: 0x040009B1 RID: 2481
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040009B2 RID: 2482
		private Type m_eventingRepositoryType;

		// Token: 0x040009B3 RID: 2483
		private string m_continuationString;
	}
}
