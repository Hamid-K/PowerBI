using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000551 RID: 1361
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ConditionalExceptionAlreadyExistsException : InvalidConditionalExceptionRegistrationException
	{
		// Token: 0x0600297E RID: 10622 RVA: 0x000953D4 File Offset: 0x000935D4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x000953DC File Offset: 0x000935DC
		// (set) Token: 0x06002980 RID: 10624 RVA: 0x000953E4 File Offset: 0x000935E4
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

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x000953ED File Offset: 0x000935ED
		// (set) Token: 0x06002982 RID: 10626 RVA: 0x000953F5 File Offset: 0x000935F5
		public EntryPointIdentifier EntryPoint
		{
			get
			{
				return this.m_entryPoint;
			}
			protected set
			{
				this.m_entryPoint = value;
			}
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000953FE File Offset: 0x000935FE
		public ConditionalExceptionAlreadyExistsException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<Activity>();
			CompileCheck.IsValidReferenceField<EntryPointIdentifier>();
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x00095417 File Offset: 0x00093617
		public ConditionalExceptionAlreadyExistsException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x0009542E File Offset: 0x0009362E
		public ConditionalExceptionAlreadyExistsException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0009544C File Offset: 0x0009364C
		protected ConditionalExceptionAlreadyExistsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ConditionalExceptionAlreadyExistsException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Activity = (Activity)info.GetValue("ConditionalExceptionAlreadyExistsException_Activity", typeof(Activity));
			}
			catch (SerializationException)
			{
				this.Activity = null;
			}
			try
			{
				this.EntryPoint = (EntryPointIdentifier)info.GetValue("ConditionalExceptionAlreadyExistsException_EntryPoint", typeof(EntryPointIdentifier));
			}
			catch (SerializationException)
			{
				this.EntryPoint = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ConditionalExceptionAlreadyExistsException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x0009555C File Offset: 0x0009375C
		public ConditionalExceptionAlreadyExistsException(Activity activity, EntryPointIdentifier entryPoint)
		{
			this.Activity = activity;
			this.EntryPoint = entryPoint;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x00095579 File Offset: 0x00093779
		public ConditionalExceptionAlreadyExistsException(Activity activity, EntryPointIdentifier entryPoint, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Activity = activity;
			this.EntryPoint = entryPoint;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x0009559E File Offset: 0x0009379E
		public ConditionalExceptionAlreadyExistsException(Activity activity, EntryPointIdentifier entryPoint, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Activity = activity;
			this.EntryPoint = entryPoint;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000955CC File Offset: 0x000937CC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x00095603 File Offset: 0x00093803
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x0009560C File Offset: 0x0009380C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ConditionalExceptionAlreadyExistsException))
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

		// Token: 0x0600298D RID: 10637 RVA: 0x000956DC File Offset: 0x000938DC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ConditionalExceptionAlreadyExistsException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ConditionalExceptionAlreadyExistsException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Activity != null)
			{
				info.AddValue("ConditionalExceptionAlreadyExistsException_Activity", this.Activity, typeof(Activity));
			}
			if (this.EntryPoint != null)
			{
				info.AddValue("ConditionalExceptionAlreadyExistsException_EntryPoint", this.EntryPoint, typeof(EntryPointIdentifier));
			}
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x00095780 File Offset: 0x00093980
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "A conditional exception for entry point: '{0}' in Activity: '{1}' is already registered ", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.EntryPoint != null) ? this.EntryPoint.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.EntryPoint != null) ? this.EntryPoint.MarkIfInternal() : string.Empty) : ((this.EntryPoint != null) ? this.EntryPoint.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Activity != null) ? this.Activity.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Activity != null) ? this.Activity.MarkIfInternal() : string.Empty) : ((this.Activity != null) ? this.Activity.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x00095866 File Offset: 0x00093A66
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

		// Token: 0x06002990 RID: 10640 RVA: 0x00095884 File Offset: 0x00093A84
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Activity={0}", new object[] { (this.Activity != null) ? this.Activity.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Activity={0}", new object[] { (this.Activity != null) ? this.Activity.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Activity={0}", new object[] { (this.Activity != null) ? this.Activity.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EntryPoint={0}", new object[] { (this.EntryPoint != null) ? this.EntryPoint.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EntryPoint={0}", new object[] { (this.EntryPoint != null) ? this.EntryPoint.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "EntryPoint={0}", new object[] { (this.EntryPoint != null) ? this.EntryPoint.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x00095A26 File Offset: 0x00093C26
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x00095A2F File Offset: 0x00093C2F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x00095A38 File Offset: 0x00093C38
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x00095A26 File Offset: 0x00093C26
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x00095A44 File Offset: 0x00093C44
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

		// Token: 0x04000EBA RID: 3770
		private string creationMessage;

		// Token: 0x04000EBB RID: 3771
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000EBC RID: 3772
		private Activity m_activity;

		// Token: 0x04000EBD RID: 3773
		private EntryPointIdentifier m_entryPoint;
	}
}
