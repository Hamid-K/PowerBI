using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001AA RID: 426
	[KnownType(typeof(ExceptionCorrelation))]
	[KnownType(typeof(MonitoringScopeId))]
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class MonitoredException : Exception, IMonitoredError, IContainsPrivateInformation
	{
		// Token: 0x06000AD6 RID: 2774 RVA: 0x00025CD4 File Offset: 0x00023ED4
		public virtual string GetExtendedMessage(PrivateInformationMarkupKind markupKind, int? maxLength = null)
		{
			ExtendedStringBuilder extendedStringBuilder = new ExtendedStringBuilder();
			extendedStringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
			{
				base.GetType().FullName,
				this.CreateMessageFromTemplate(markupKind)
			}));
			extendedStringBuilder.AppendLine(this.GetPropertiesString(markupKind));
			foreach (Exception ex in this.GetInnerExceptions())
			{
				extendedStringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
				{
					ex.GetType().FullName,
					ex.Message
				}));
			}
			string text = extendedStringBuilder.ToString();
			if (maxLength != null)
			{
				int? num = maxLength;
				int length = text.Length;
				if ((num.GetValueOrDefault() < length) & (num != null))
				{
					return text.Substring(0, maxLength.Value) + "...";
				}
			}
			return text;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00025DDC File Offset: 0x00023FDC
		private void Constructor(bool deserializing)
		{
			if (!deserializing)
			{
				this.ExceptionCorrelation = new ExceptionCorrelation(base.InnerException);
				this.CreationActivityStack = UtilsContext.Current.GetActivityStack();
				this.CreationActivityStackString = UtilsContext.Current.GetActivityStackRepresentation();
				this.CreationContextTraceDump = new TraceDump();
				this.CreationContextTraceDump.Indent = 6;
				TraceDump creationContextTraceDump = this.CreationContextTraceDump;
				int[] array = new int[2];
				array[0] = 2;
				ContextManager.Dump(creationContextTraceDump, array);
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0000E568 File Offset: 0x0000C768
		public virtual bool IsFatal()
		{
			return false;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00025E49 File Offset: 0x00024049
		public virtual bool IsBenign()
		{
			return this.GetExceptionCulprit() == ExceptionCulprit.User;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00025E57 File Offset: 0x00024057
		public virtual string ErrorShortName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0000E568 File Offset: 0x0000C768
		public virtual bool IsPermanent()
		{
			return false;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00025E64 File Offset: 0x00024064
		public virtual ErrorCorrelationId ErrorCorrelationId
		{
			get
			{
				return this.ExceptionCorrelation.ErrorCorrelationId;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00025E71 File Offset: 0x00024071
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x00025E7E File Offset: 0x0002407E
		public virtual MonitoringScopeId MonitoringScope
		{
			get
			{
				return this.ExceptionCorrelation.MonitoringScope;
			}
			set
			{
				this.ExceptionCorrelation.MonitoringScope = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00025E8C File Offset: 0x0002408C
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x00025E99 File Offset: 0x00024099
		public virtual long ErrorEventId
		{
			get
			{
				return this.ExceptionCorrelation.ErrorEventId;
			}
			set
			{
				this.ExceptionCorrelation.ErrorEventId = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00025EA7 File Offset: 0x000240A7
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x00025EB4 File Offset: 0x000240B4
		public virtual string ErrorEventName
		{
			get
			{
				return this.ExceptionCorrelation.ErrorEventName;
			}
			set
			{
				this.ExceptionCorrelation.ErrorEventName = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00025EC2 File Offset: 0x000240C2
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x00025ECF File Offset: 0x000240CF
		public virtual long ErrorEventsKitId
		{
			get
			{
				return this.ExceptionCorrelation.ErrorEventsKitId;
			}
			set
			{
				this.ExceptionCorrelation.ErrorEventsKitId = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00025EDD File Offset: 0x000240DD
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x00025EEA File Offset: 0x000240EA
		public virtual string ErrorEventsKitName
		{
			get
			{
				return this.ExceptionCorrelation.ErrorEventsKitName;
			}
			set
			{
				this.ExceptionCorrelation.ErrorEventsKitName = value;
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00025EF8 File Offset: 0x000240F8
		public virtual ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00025F00 File Offset: 0x00024100
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00025F08 File Offset: 0x00024108
		public ExceptionCorrelation ExceptionCorrelation
		{
			get
			{
				return this.m_exceptionCorrelation;
			}
			protected set
			{
				this.m_exceptionCorrelation = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00025F11 File Offset: 0x00024111
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00025F19 File Offset: 0x00024119
		public IEnumerable<Activity> CreationActivityStack
		{
			get
			{
				return this.m_creationActivityStack;
			}
			protected set
			{
				this.m_creationActivityStack = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00025F22 File Offset: 0x00024122
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x00025F2A File Offset: 0x0002412A
		public string CreationActivityStackString
		{
			get
			{
				return this.m_creationActivityStackString;
			}
			protected set
			{
				this.m_creationActivityStackString = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00025F33 File Offset: 0x00024133
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x00025F3B File Offset: 0x0002413B
		public TraceDump CreationContextTraceDump
		{
			get
			{
				return this.m_creationContextTraceDump;
			}
			protected set
			{
				this.m_creationContextTraceDump = value;
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00025F44 File Offset: 0x00024144
		public MonitoredException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<ExceptionCorrelation>();
			CompileCheck.IsValidReferenceField<IEnumerable<Activity>>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<TraceDump>();
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00025F67 File Offset: 0x00024167
		public MonitoredException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00025F7E File Offset: 0x0002417E
		public MonitoredException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00025F9C File Offset: 0x0002419C
		protected MonitoredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MonitoredException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ExceptionCorrelation = (ExceptionCorrelation)info.GetValue("MonitoredException_ExceptionCorrelation", typeof(ExceptionCorrelation));
			}
			catch (SerializationException)
			{
				this.ExceptionCorrelation = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("MonitoredException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00026070 File Offset: 0x00024270
		public MonitoredException(ExceptionCorrelation exceptionCorrelation, IEnumerable<Activity> creationActivityStack, string creationActivityStackString, TraceDump creationContextTraceDump)
		{
			this.ExceptionCorrelation = exceptionCorrelation;
			this.CreationActivityStack = creationActivityStack;
			this.CreationActivityStackString = creationActivityStackString;
			this.CreationContextTraceDump = creationContextTraceDump;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002609C File Offset: 0x0002429C
		public MonitoredException(ExceptionCorrelation exceptionCorrelation, IEnumerable<Activity> creationActivityStack, string creationActivityStackString, TraceDump creationContextTraceDump, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ExceptionCorrelation = exceptionCorrelation;
			this.CreationActivityStack = creationActivityStack;
			this.CreationActivityStackString = creationActivityStackString;
			this.CreationContextTraceDump = creationContextTraceDump;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000260D2 File Offset: 0x000242D2
		public MonitoredException(ExceptionCorrelation exceptionCorrelation, IEnumerable<Activity> creationActivityStack, string creationActivityStackString, TraceDump creationContextTraceDump, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ExceptionCorrelation = exceptionCorrelation;
			this.CreationActivityStack = creationActivityStack;
			this.CreationActivityStackString = creationActivityStackString;
			this.CreationContextTraceDump = creationContextTraceDump;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00026110 File Offset: 0x00024310
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00026148 File Offset: 0x00024348
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MonitoredException))
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

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00026218 File Offset: 0x00024418
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MonitoredException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("MonitoredException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ExceptionCorrelation != null)
			{
				info.AddValue("MonitoredException_ExceptionCorrelation", this.ExceptionCorrelation, typeof(ExceptionCorrelation));
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00026298 File Offset: 0x00024498
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? (base.Message ?? string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? (base.Message ?? string.Empty) : (base.Message ?? string.Empty)) });
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x000262F5 File Offset: 0x000244F5
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

		// Token: 0x06000AFC RID: 2812 RVA: 0x00026314 File Offset: 0x00024514
		protected virtual string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExceptionCorrelation={0}", new object[] { (this.ExceptionCorrelation != null) ? this.ExceptionCorrelation.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExceptionCorrelation={0}", new object[] { (this.ExceptionCorrelation != null) ? this.ExceptionCorrelation.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ExceptionCorrelation={0}", new object[] { (this.ExceptionCorrelation != null) ? this.ExceptionCorrelation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CreationActivityStackString={0}", new object[] { (this.CreationActivityStackString != null) ? this.CreationActivityStackString.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CreationActivityStackString={0}", new object[] { (this.CreationActivityStackString != null) ? this.CreationActivityStackString.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "CreationActivityStackString={0}", new object[] { (this.CreationActivityStackString != null) ? this.CreationActivityStackString.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CreationContextTraceDump={0}", new object[] { (this.CreationContextTraceDump != null) ? this.CreationContextTraceDump.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CreationContextTraceDump={0}", new object[] { (this.CreationContextTraceDump != null) ? this.CreationContextTraceDump.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "CreationContextTraceDump={0}", new object[] { (this.CreationContextTraceDump != null) ? this.CreationContextTraceDump.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00026571 File Offset: 0x00024771
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002657A File Offset: 0x0002477A
		public virtual string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00026583 File Offset: 0x00024783
		public virtual string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00026571 File Offset: 0x00024771
		public virtual string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002658C File Offset: 0x0002478C
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

		// Token: 0x04000453 RID: 1107
		public static readonly IMonitoredError NoError = new NoError();

		// Token: 0x04000454 RID: 1108
		private string creationMessage;

		// Token: 0x04000455 RID: 1109
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000456 RID: 1110
		private ExceptionCorrelation m_exceptionCorrelation;

		// Token: 0x04000457 RID: 1111
		[NonSerialized]
		private IEnumerable<Activity> m_creationActivityStack;

		// Token: 0x04000458 RID: 1112
		[NonSerialized]
		private string m_creationActivityStackString;

		// Token: 0x04000459 RID: 1113
		[NonSerialized]
		private TraceDump m_creationContextTraceDump;
	}
}
