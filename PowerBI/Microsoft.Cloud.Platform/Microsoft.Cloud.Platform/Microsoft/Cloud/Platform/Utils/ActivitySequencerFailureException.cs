using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200016E RID: 366
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ActivitySequencerFailureException : MonitoredException
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00020CC0 File Offset: 0x0001EEC0
		public override string ErrorShortName
		{
			get
			{
				if (base.InnerException != null)
				{
					return "{0}.{1}".FormatWithInvariantCulture(new object[]
					{
						base.GetType().Name,
						base.InnerException.GetType().Name
					});
				}
				return base.ErrorShortName;
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00020D0D File Offset: 0x0001EF0D
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00020D15 File Offset: 0x0001EF15
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x00020D1D File Offset: 0x0001EF1D
		public ActivitySequencer ActivitySequencer
		{
			get
			{
				return this.m_activitySequencer;
			}
			protected set
			{
				this.m_activitySequencer = value;
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00020D26 File Offset: 0x0001EF26
		public ActivitySequencerFailureException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<ActivitySequencer>();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00020D3A File Offset: 0x0001EF3A
		public ActivitySequencerFailureException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00020D51 File Offset: 0x0001EF51
		public ActivitySequencerFailureException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00020D70 File Offset: 0x0001EF70
		protected ActivitySequencerFailureException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ActivitySequencerFailureException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ActivitySequencerFailureException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00020E0C File Offset: 0x0001F00C
		public ActivitySequencerFailureException(ActivitySequencer activitySequencer)
		{
			this.ActivitySequencer = activitySequencer;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00020E22 File Offset: 0x0001F022
		public ActivitySequencerFailureException(ActivitySequencer activitySequencer, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ActivitySequencer = activitySequencer;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00020E40 File Offset: 0x0001F040
		public ActivitySequencerFailureException(ActivitySequencer activitySequencer, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ActivitySequencer = activitySequencer;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00020E64 File Offset: 0x0001F064
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00020E9B File Offset: 0x0001F09B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00020EA4 File Offset: 0x0001F0A4
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ActivitySequencerFailureException))
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

		// Token: 0x0600099F RID: 2463 RVA: 0x00020F74 File Offset: 0x0001F174
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ActivitySequencerFailureException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ActivitySequencerFailureException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00020FD0 File Offset: 0x0001F1D0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "An ActivitySequencer-derived class ({0}) threw a non-monitored exception.", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.ActivitySequencer != null) ? this.ActivitySequencer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ActivitySequencer != null) ? this.ActivitySequencer.MarkIfInternal() : string.Empty) : ((this.ActivitySequencer != null) ? this.ActivitySequencer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00021054 File Offset: 0x0001F254
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

		// Token: 0x060009A2 RID: 2466 RVA: 0x00021074 File Offset: 0x0001F274
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ActivitySequencer={0}", new object[] { (this.ActivitySequencer != null) ? this.ActivitySequencer.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ActivitySequencer={0}", new object[] { (this.ActivitySequencer != null) ? this.ActivitySequencer.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ActivitySequencer={0}", new object[] { (this.ActivitySequencer != null) ? this.ActivitySequencer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00021153 File Offset: 0x0001F353
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002115C File Offset: 0x0001F35C
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00021165 File Offset: 0x0001F365
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00021153 File Offset: 0x0001F353
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00021170 File Offset: 0x0001F370
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

		// Token: 0x040003A3 RID: 931
		private string creationMessage;

		// Token: 0x040003A4 RID: 932
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003A5 RID: 933
		[NonSerialized]
		private ActivitySequencer m_activitySequencer;
	}
}
