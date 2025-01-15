using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000309 RID: 777
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class SequencerTimeoutException : MonitoredException
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x0004C3C0 File Offset: 0x0004A5C0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0004C3C8 File Offset: 0x0004A5C8
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x0004C3D0 File Offset: 0x0004A5D0
		public Sequencer Sequencer
		{
			get
			{
				return this.m_sequencer;
			}
			protected set
			{
				this.m_sequencer = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0004C3D9 File Offset: 0x0004A5D9
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x0004C3E1 File Offset: 0x0004A5E1
		public TimeSpan Timeout
		{
			get
			{
				return this.m_timeout;
			}
			protected set
			{
				this.m_timeout = value;
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0004C3EA File Offset: 0x0004A5EA
		public SequencerTimeoutException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<Sequencer>();
			CompileCheck.IsValidValueField<TimeSpan>();
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0004C403 File Offset: 0x0004A603
		public SequencerTimeoutException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0004C41A File Offset: 0x0004A61A
		public SequencerTimeoutException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0004C438 File Offset: 0x0004A638
		protected SequencerTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("SequencerTimeoutException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.Timeout = (TimeSpan)info.GetValue("SequencerTimeoutException_Timeout", typeof(TimeSpan));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("SequencerTimeoutException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0004C4F4 File Offset: 0x0004A6F4
		public SequencerTimeoutException(Sequencer sequencer, TimeSpan timeout)
		{
			this.Sequencer = sequencer;
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0004C511 File Offset: 0x0004A711
		public SequencerTimeoutException(Sequencer sequencer, TimeSpan timeout, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Sequencer = sequencer;
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0004C536 File Offset: 0x0004A736
		public SequencerTimeoutException(Sequencer sequencer, TimeSpan timeout, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Sequencer = sequencer;
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0004C564 File Offset: 0x0004A764
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0004C59B File Offset: 0x0004A79B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0004C5A4 File Offset: 0x0004A7A4
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(SequencerTimeoutException))
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<UtilsTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0004C674 File Offset: 0x0004A874
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("SequencerTimeoutException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("SequencerTimeoutException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("SequencerTimeoutException_Timeout", this.Timeout, typeof(TimeSpan));
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0004C6F0 File Offset: 0x0004A8F0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "A sequencer operation (implemented by {0}) has exceeded its timeout ({1}).", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Sequencer != null) ? this.Sequencer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Sequencer != null) ? this.Sequencer.MarkIfInternal() : string.Empty) : ((this.Sequencer != null) ? this.Sequencer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.Timeout.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.Timeout.ToString() : this.Timeout.ToString())
			});
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0004C7BE File Offset: 0x0004A9BE
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

		// Token: 0x0600157C RID: 5500 RVA: 0x0004C7DC File Offset: 0x0004A9DC
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Sequencer={0}", new object[] { (this.Sequencer != null) ? this.Sequencer.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Sequencer={0}", new object[] { (this.Sequencer != null) ? this.Sequencer.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Sequencer={0}", new object[] { (this.Sequencer != null) ? this.Sequencer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Timeout={0}", new object[] { this.Timeout.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Timeout={0}", new object[] { this.Timeout.ToString() }) : string.Format(CultureInfo.CurrentCulture, "Timeout={0}", new object[] { this.Timeout.ToString() })));
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0004C966 File Offset: 0x0004AB66
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0004C96F File Offset: 0x0004AB6F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0004C978 File Offset: 0x0004AB78
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0004C966 File Offset: 0x0004AB66
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0004C984 File Offset: 0x0004AB84
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

		// Token: 0x040007DD RID: 2013
		private string creationMessage;

		// Token: 0x040007DE RID: 2014
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007DF RID: 2015
		[NonSerialized]
		private Sequencer m_sequencer;

		// Token: 0x040007E0 RID: 2016
		private TimeSpan m_timeout;
	}
}
