using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000552 RID: 1362
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class TaggedActivityAlreadyExistsException : ConditionalExceptionAlreadyExistsException
	{
		// Token: 0x06002996 RID: 10646 RVA: 0x00095C30 File Offset: 0x00093E30
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002997 RID: 10647 RVA: 0x00095C38 File Offset: 0x00093E38
		// (set) Token: 0x06002998 RID: 10648 RVA: 0x00095C40 File Offset: 0x00093E40
		public ActivityTag Tag
		{
			get
			{
				return this.m_tag;
			}
			protected set
			{
				this.m_tag = value;
			}
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x00095C49 File Offset: 0x00093E49
		public TaggedActivityAlreadyExistsException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<ActivityTag>();
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x00095C5D File Offset: 0x00093E5D
		public TaggedActivityAlreadyExistsException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x00095C74 File Offset: 0x00093E74
		public TaggedActivityAlreadyExistsException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x00095C94 File Offset: 0x00093E94
		protected TaggedActivityAlreadyExistsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("TaggedActivityAlreadyExistsException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Tag = (ActivityTag)info.GetValue("TaggedActivityAlreadyExistsException_Tag", typeof(ActivityTag));
			}
			catch (SerializationException)
			{
				this.Tag = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("TaggedActivityAlreadyExistsException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x00095D68 File Offset: 0x00093F68
		public TaggedActivityAlreadyExistsException(ActivityTag tag)
		{
			this.Tag = tag;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x00095D7E File Offset: 0x00093F7E
		public TaggedActivityAlreadyExistsException(ActivityTag tag, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Tag = tag;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x00095D9C File Offset: 0x00093F9C
		public TaggedActivityAlreadyExistsException(ActivityTag tag, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Tag = tag;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x00095DC0 File Offset: 0x00093FC0
		public TaggedActivityAlreadyExistsException(ActivityTag tag, Activity activity, EntryPointIdentifier entryPoint)
			: base(activity, entryPoint)
		{
			this.Tag = tag;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x00095DD8 File Offset: 0x00093FD8
		public TaggedActivityAlreadyExistsException(Activity activity, EntryPointIdentifier entryPoint, string message, Exception innerException)
			: base(activity, entryPoint, message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x00095DF8 File Offset: 0x00093FF8
		public TaggedActivityAlreadyExistsException(Activity activity, EntryPointIdentifier entryPoint, string message)
			: base(activity, entryPoint, message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x00095E11 File Offset: 0x00094011
		public TaggedActivityAlreadyExistsException(ActivityTag tag, Activity activity, EntryPointIdentifier entryPoint, string message, Exception innerException)
			: base(activity, entryPoint, message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Tag = tag;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x00095E3A File Offset: 0x0009403A
		public TaggedActivityAlreadyExistsException(ActivityTag tag, Activity activity, EntryPointIdentifier entryPoint, string message)
			: base(activity, entryPoint, message)
		{
			this.creationMessage = message;
			this.Tag = tag;
			this.ConstructorInternal(false);
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00095E5C File Offset: 0x0009405C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x00095E93 File Offset: 0x00094093
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x00095E9C File Offset: 0x0009409C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(TaggedActivityAlreadyExistsException))
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

		// Token: 0x060029A8 RID: 10664 RVA: 0x00095F6C File Offset: 0x0009416C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("TaggedActivityAlreadyExistsException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("TaggedActivityAlreadyExistsException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Tag != null)
			{
				info.AddValue("TaggedActivityAlreadyExistsException_Tag", this.Tag, typeof(ActivityTag));
			}
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x00095FEC File Offset: 0x000941EC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "A tagged activity for tag: '{0}', with parent Activity: '{1}' is already registered ", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Tag != null) ? this.Tag.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Tag != null) ? this.Tag.MarkIfInternal() : string.Empty) : ((this.Tag != null) ? this.Tag.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((base.Activity != null) ? base.Activity.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((base.Activity != null) ? base.Activity.MarkIfInternal() : string.Empty) : ((base.Activity != null) ? base.Activity.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x000960D2 File Offset: 0x000942D2
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

		// Token: 0x060029AB RID: 10667 RVA: 0x000960F0 File Offset: 0x000942F0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Tag={0}", new object[] { (this.Tag != null) ? this.Tag.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Tag={0}", new object[] { (this.Tag != null) ? this.Tag.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Tag={0}", new object[] { (this.Tag != null) ? this.Tag.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000961CF File Offset: 0x000943CF
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000961D8 File Offset: 0x000943D8
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000961E1 File Offset: 0x000943E1
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x000961CF File Offset: 0x000943CF
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000961EC File Offset: 0x000943EC
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

		// Token: 0x04000EBE RID: 3774
		private string creationMessage;

		// Token: 0x04000EBF RID: 3775
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000EC0 RID: 3776
		private ActivityTag m_tag;
	}
}
