using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000550 RID: 1360
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class InvalidConditionalExceptionRegistrationException : ConditionalExceptionThrowerException
	{
		// Token: 0x06002968 RID: 10600 RVA: 0x00094D28 File Offset: 0x00092F28
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x00094D30 File Offset: 0x00092F30
		// (set) Token: 0x0600296A RID: 10602 RVA: 0x00094D38 File Offset: 0x00092F38
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

		// Token: 0x0600296B RID: 10603 RVA: 0x00094D41 File Offset: 0x00092F41
		public InvalidConditionalExceptionRegistrationException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<EntryPointIdentifier>();
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x00094D55 File Offset: 0x00092F55
		public InvalidConditionalExceptionRegistrationException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x00094D6C File Offset: 0x00092F6C
		public InvalidConditionalExceptionRegistrationException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x00094D8C File Offset: 0x00092F8C
		protected InvalidConditionalExceptionRegistrationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidConditionalExceptionRegistrationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Identifier = (EntryPointIdentifier)info.GetValue("InvalidConditionalExceptionRegistrationException_Identifier", typeof(EntryPointIdentifier));
			}
			catch (SerializationException)
			{
				this.Identifier = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("InvalidConditionalExceptionRegistrationException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x00094E60 File Offset: 0x00093060
		public InvalidConditionalExceptionRegistrationException(EntryPointIdentifier identifier)
		{
			this.Identifier = identifier;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x00094E76 File Offset: 0x00093076
		public InvalidConditionalExceptionRegistrationException(EntryPointIdentifier identifier, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Identifier = identifier;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x00094E94 File Offset: 0x00093094
		public InvalidConditionalExceptionRegistrationException(EntryPointIdentifier identifier, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Identifier = identifier;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x00094EB8 File Offset: 0x000930B8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x00094EEF File Offset: 0x000930EF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x00094EF8 File Offset: 0x000930F8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidConditionalExceptionRegistrationException))
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

		// Token: 0x06002975 RID: 10613 RVA: 0x00094FC8 File Offset: 0x000931C8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidConditionalExceptionRegistrationException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("InvalidConditionalExceptionRegistrationException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Identifier != null)
			{
				info.AddValue("InvalidConditionalExceptionRegistrationException_Identifier", this.Identifier, typeof(EntryPointIdentifier));
			}
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x00095048 File Offset: 0x00093248
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Invalid argument in registration of Conditional Exception for Entry Point: '{0}'", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.Identifier != null) ? this.Identifier.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Identifier != null) ? this.Identifier.MarkIfInternal() : string.Empty) : ((this.Identifier != null) ? this.Identifier.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002977 RID: 10615 RVA: 0x000950CC File Offset: 0x000932CC
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

		// Token: 0x06002978 RID: 10616 RVA: 0x000950EC File Offset: 0x000932EC
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Identifier={0}", new object[] { (this.Identifier != null) ? this.Identifier.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Identifier={0}", new object[] { (this.Identifier != null) ? this.Identifier.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Identifier={0}", new object[] { (this.Identifier != null) ? this.Identifier.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000951CB File Offset: 0x000933CB
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000951D4 File Offset: 0x000933D4
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000951DD File Offset: 0x000933DD
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000951CB File Offset: 0x000933CB
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000951E8 File Offset: 0x000933E8
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

		// Token: 0x04000EB7 RID: 3767
		private string creationMessage;

		// Token: 0x04000EB8 RID: 3768
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000EB9 RID: 3769
		private EntryPointIdentifier m_identifier;
	}
}
