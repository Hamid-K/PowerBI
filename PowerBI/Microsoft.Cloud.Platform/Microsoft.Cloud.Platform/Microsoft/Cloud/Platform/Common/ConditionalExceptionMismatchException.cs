using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200054F RID: 1359
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ConditionalExceptionMismatchException : ConditionalExceptionThrowerException
	{
		// Token: 0x0600294E RID: 10574 RVA: 0x00094320 File Offset: 0x00092520
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x00094328 File Offset: 0x00092528
		// (set) Token: 0x06002950 RID: 10576 RVA: 0x00094330 File Offset: 0x00092530
		public EntryPointIdentifier Identifier
		{
			get
			{
				return this.m_identifier;
			}
			protected set
			{
				this.m_identifier = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x00094339 File Offset: 0x00092539
		// (set) Token: 0x06002952 RID: 10578 RVA: 0x00094341 File Offset: 0x00092541
		public string RequestingMethod
		{
			get
			{
				return this.m_requestingMethod;
			}
			protected set
			{
				this.m_requestingMethod = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x0009434A File Offset: 0x0009254A
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x00094352 File Offset: 0x00092552
		public string RegistrationType
		{
			get
			{
				return this.m_registrationType;
			}
			protected set
			{
				this.m_registrationType = value;
			}
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x0009435B File Offset: 0x0009255B
		public ConditionalExceptionMismatchException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<EntryPointIdentifier>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x00094379 File Offset: 0x00092579
		public ConditionalExceptionMismatchException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x00094390 File Offset: 0x00092590
		public ConditionalExceptionMismatchException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000943B0 File Offset: 0x000925B0
		protected ConditionalExceptionMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ConditionalExceptionMismatchException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Identifier = (EntryPointIdentifier)info.GetValue("ConditionalExceptionMismatchException_Identifier", typeof(EntryPointIdentifier));
			}
			catch (SerializationException)
			{
				this.Identifier = null;
			}
			try
			{
				this.RequestingMethod = (string)info.GetValue("ConditionalExceptionMismatchException_RequestingMethod", typeof(string));
			}
			catch (SerializationException)
			{
				this.RequestingMethod = null;
			}
			try
			{
				this.RegistrationType = (string)info.GetValue("ConditionalExceptionMismatchException_RegistrationType", typeof(string));
			}
			catch (SerializationException)
			{
				this.RegistrationType = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ConditionalExceptionMismatchException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000944F8 File Offset: 0x000926F8
		public ConditionalExceptionMismatchException(EntryPointIdentifier identifier, string requestingMethod, string registrationType)
		{
			this.Identifier = identifier;
			this.RequestingMethod = requestingMethod;
			this.RegistrationType = registrationType;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x0009451C File Offset: 0x0009271C
		public ConditionalExceptionMismatchException(EntryPointIdentifier identifier, string requestingMethod, string registrationType, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Identifier = identifier;
			this.RequestingMethod = requestingMethod;
			this.RegistrationType = registrationType;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x0009454A File Offset: 0x0009274A
		public ConditionalExceptionMismatchException(EntryPointIdentifier identifier, string requestingMethod, string registrationType, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Identifier = identifier;
			this.RequestingMethod = requestingMethod;
			this.RegistrationType = registrationType;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x00094580 File Offset: 0x00092780
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x000945B7 File Offset: 0x000927B7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000945C0 File Offset: 0x000927C0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ConditionalExceptionMismatchException))
			{
				TraceSourceBase<CommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<CommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<CommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x00094690 File Offset: 0x00092890
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ConditionalExceptionMismatchException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ConditionalExceptionMismatchException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Identifier != null)
			{
				info.AddValue("ConditionalExceptionMismatchException_Identifier", this.Identifier, typeof(EntryPointIdentifier));
			}
			if (this.RequestingMethod != null)
			{
				info.AddValue("ConditionalExceptionMismatchException_RequestingMethod", this.RequestingMethod, typeof(string));
			}
			if (this.RegistrationType != null)
			{
				info.AddValue("ConditionalExceptionMismatchException_RegistrationType", this.RegistrationType, typeof(string));
			}
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x00094754 File Offset: 0x00092954
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Conditional Exception '{0}' was registered for a method of type '{1}' but was requested for a method of type '{2}'", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Identifier != null) ? this.Identifier.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Identifier != null) ? this.Identifier.MarkIfInternal() : string.Empty) : ((this.Identifier != null) ? this.Identifier.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.RegistrationType != null) ? this.RegistrationType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.RegistrationType != null) ? this.RegistrationType.MarkIfInternal() : string.Empty) : ((this.RegistrationType != null) ? this.RegistrationType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.RequestingMethod != null) ? this.RequestingMethod.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.RequestingMethod != null) ? this.RequestingMethod.MarkIfInternal() : string.Empty) : ((this.RequestingMethod != null) ? this.RequestingMethod.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x0009489C File Offset: 0x00092A9C
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

		// Token: 0x06002962 RID: 10594 RVA: 0x000948BC File Offset: 0x00092ABC
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Identifier={0}", new object[] { (this.Identifier != null) ? this.Identifier.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Identifier={0}", new object[] { (this.Identifier != null) ? this.Identifier.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Identifier={0}", new object[] { (this.Identifier != null) ? this.Identifier.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "RequestingMethod={0}", new object[] { (this.RequestingMethod != null) ? this.RequestingMethod.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "RequestingMethod={0}", new object[] { (this.RequestingMethod != null) ? this.RequestingMethod.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "RequestingMethod={0}", new object[] { (this.RequestingMethod != null) ? this.RequestingMethod.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "RegistrationType={0}", new object[] { (this.RegistrationType != null) ? this.RegistrationType.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "RegistrationType={0}", new object[] { (this.RegistrationType != null) ? this.RegistrationType.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "RegistrationType={0}", new object[] { (this.RegistrationType != null) ? this.RegistrationType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x00094B21 File Offset: 0x00092D21
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x00094B2A File Offset: 0x00092D2A
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x00094B33 File Offset: 0x00092D33
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x00094B21 File Offset: 0x00092D21
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x00094B3C File Offset: 0x00092D3C
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

		// Token: 0x04000EB2 RID: 3762
		private string creationMessage;

		// Token: 0x04000EB3 RID: 3763
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000EB4 RID: 3764
		private EntryPointIdentifier m_identifier;

		// Token: 0x04000EB5 RID: 3765
		private string m_requestingMethod;

		// Token: 0x04000EB6 RID: 3766
		private string m_registrationType;
	}
}
