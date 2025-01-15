using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000316 RID: 790
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class MonitoredActivityInvokerException : MonitoredException
	{
		// Token: 0x06001666 RID: 5734 RVA: 0x00050D60 File Offset: 0x0004EF60
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x00050D68 File Offset: 0x0004EF68
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x00050D70 File Offset: 0x0004EF70
		public Activity Activity
		{
			get
			{
				return this.m_activity;
			}
			protected set
			{
				this.m_activity = value;
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00050D79 File Offset: 0x0004EF79
		public MonitoredActivityInvokerException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<Activity>();
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00050D8D File Offset: 0x0004EF8D
		public MonitoredActivityInvokerException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00050DA4 File Offset: 0x0004EFA4
		public MonitoredActivityInvokerException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00050DC4 File Offset: 0x0004EFC4
		protected MonitoredActivityInvokerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MonitoredActivityInvokerException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("MonitoredActivityInvokerException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00050E60 File Offset: 0x0004F060
		public MonitoredActivityInvokerException(Activity activity)
		{
			this.Activity = activity;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00050E76 File Offset: 0x0004F076
		public MonitoredActivityInvokerException(Activity activity, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Activity = activity;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00050E94 File Offset: 0x0004F094
		public MonitoredActivityInvokerException(Activity activity, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Activity = activity;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00050EB8 File Offset: 0x0004F0B8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00050EEF File Offset: 0x0004F0EF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00050EF8 File Offset: 0x0004F0F8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MonitoredActivityInvokerException))
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

		// Token: 0x06001673 RID: 5747 RVA: 0x00050FC8 File Offset: 0x0004F1C8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MonitoredActivityInvokerException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("MonitoredActivityInvokerException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00051024 File Offset: 0x0004F224
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "An activity ({0}) threw a non-monitored exception.", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.Activity != null) ? this.Activity.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Activity != null) ? this.Activity.MarkIfInternal() : string.Empty) : ((this.Activity != null) ? this.Activity.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x000510A8 File Offset: 0x0004F2A8
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

		// Token: 0x06001676 RID: 5750 RVA: 0x000510C8 File Offset: 0x0004F2C8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Activity={0}", new object[] { (this.Activity != null) ? this.Activity.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Activity={0}", new object[] { (this.Activity != null) ? this.Activity.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Activity={0}", new object[] { (this.Activity != null) ? this.Activity.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x000511A7 File Offset: 0x0004F3A7
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x000511B0 File Offset: 0x0004F3B0
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000511B9 File Offset: 0x0004F3B9
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000511A7 File Offset: 0x0004F3A7
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000511C4 File Offset: 0x0004F3C4
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

		// Token: 0x040007FF RID: 2047
		private string creationMessage;

		// Token: 0x04000800 RID: 2048
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000801 RID: 2049
		[NonSerialized]
		private Activity m_activity;
	}
}
