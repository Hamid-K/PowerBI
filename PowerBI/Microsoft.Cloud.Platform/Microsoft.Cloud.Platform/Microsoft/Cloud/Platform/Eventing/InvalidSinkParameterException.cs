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
	// Token: 0x020003A8 RID: 936
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class InvalidSinkParameterException : MonitoredException
	{
		// Token: 0x06001CD9 RID: 7385 RVA: 0x0006D340 File Offset: 0x0006B540
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x0006D348 File Offset: 0x0006B548
		// (set) Token: 0x06001CDB RID: 7387 RVA: 0x0006D350 File Offset: 0x0006B550
		public SinkIdentifier Sid
		{
			get
			{
				return this.m_sid;
			}
			protected set
			{
				this.m_sid = value;
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0006D359 File Offset: 0x0006B559
		public InvalidSinkParameterException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<SinkIdentifier>();
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0006D36D File Offset: 0x0006B56D
		public InvalidSinkParameterException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0006D384 File Offset: 0x0006B584
		public InvalidSinkParameterException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0006D3A4 File Offset: 0x0006B5A4
		protected InvalidSinkParameterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidSinkParameterException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Sid = (SinkIdentifier)info.GetValue("InvalidSinkParameterException_Sid", typeof(SinkIdentifier));
			}
			catch (SerializationException)
			{
				this.Sid = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("InvalidSinkParameterException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0006D478 File Offset: 0x0006B678
		public InvalidSinkParameterException(SinkIdentifier sid)
		{
			this.Sid = sid;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0006D48E File Offset: 0x0006B68E
		public InvalidSinkParameterException(SinkIdentifier sid, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Sid = sid;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0006D4AC File Offset: 0x0006B6AC
		public InvalidSinkParameterException(SinkIdentifier sid, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Sid = sid;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0006D4D0 File Offset: 0x0006B6D0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0006D507 File Offset: 0x0006B707
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0006D510 File Offset: 0x0006B710
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidSinkParameterException))
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

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0006D5E0 File Offset: 0x0006B7E0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidSinkParameterException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("InvalidSinkParameterException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Sid != null)
			{
				info.AddValue("InvalidSinkParameterException_Sid", this.Sid, typeof(SinkIdentifier));
			}
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0004CDC3 File Offset: 0x0004AFC3
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "", new object[0]);
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x0006D65E File Offset: 0x0006B85E
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

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0006D67C File Offset: 0x0006B87C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Sid={0}", new object[] { (this.Sid != null) ? this.Sid.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Sid={0}", new object[] { (this.Sid != null) ? this.Sid.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Sid={0}", new object[] { (this.Sid != null) ? this.Sid.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0006D75B File Offset: 0x0006B95B
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0006D764 File Offset: 0x0006B964
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0006D76D File Offset: 0x0006B96D
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0006D75B File Offset: 0x0006B95B
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0006D778 File Offset: 0x0006B978
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

		// Token: 0x040009AD RID: 2477
		private string creationMessage;

		// Token: 0x040009AE RID: 2478
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040009AF RID: 2479
		private SinkIdentifier m_sid;
	}
}
