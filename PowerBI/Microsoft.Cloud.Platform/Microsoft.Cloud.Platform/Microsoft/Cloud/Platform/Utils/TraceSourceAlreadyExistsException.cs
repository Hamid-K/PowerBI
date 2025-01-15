using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000313 RID: 787
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class TraceSourceAlreadyExistsException : MonitoredException
	{
		// Token: 0x0600162E RID: 5678 RVA: 0x0004FD94 File Offset: 0x0004DF94
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x0004FD9C File Offset: 0x0004DF9C
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x0004FDA4 File Offset: 0x0004DFA4
		public TraceSourceIdentifier ID
		{
			get
			{
				return this.m_iD;
			}
			protected set
			{
				this.m_iD = value;
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0004FDAD File Offset: 0x0004DFAD
		public TraceSourceAlreadyExistsException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<TraceSourceIdentifier>();
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0004FDC1 File Offset: 0x0004DFC1
		public TraceSourceAlreadyExistsException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0004FDD8 File Offset: 0x0004DFD8
		public TraceSourceAlreadyExistsException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
		protected TraceSourceAlreadyExistsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("TraceSourceAlreadyExistsException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ID = (TraceSourceIdentifier)info.GetValue("TraceSourceAlreadyExistsException_ID", typeof(TraceSourceIdentifier));
			}
			catch (SerializationException)
			{
				this.ID = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("TraceSourceAlreadyExistsException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0004FECC File Offset: 0x0004E0CC
		public TraceSourceAlreadyExistsException(TraceSourceIdentifier iD)
		{
			this.ID = iD;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0004FEE2 File Offset: 0x0004E0E2
		public TraceSourceAlreadyExistsException(TraceSourceIdentifier iD, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ID = iD;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0004FF00 File Offset: 0x0004E100
		public TraceSourceAlreadyExistsException(TraceSourceIdentifier iD, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ID = iD;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0004FF24 File Offset: 0x0004E124
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0004FF5B File Offset: 0x0004E15B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0004FF64 File Offset: 0x0004E164
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(TraceSourceAlreadyExistsException))
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

		// Token: 0x0600163B RID: 5691 RVA: 0x00050034 File Offset: 0x0004E234
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("TraceSourceAlreadyExistsException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("TraceSourceAlreadyExistsException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ID != null)
			{
				info.AddValue("TraceSourceAlreadyExistsException_ID", this.ID, typeof(TraceSourceIdentifier));
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000500B4 File Offset: 0x0004E2B4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Trying to register a Trace source with an already existing ID: '{0}'", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.ID != null) ? this.ID.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ID != null) ? this.ID.MarkIfInternal() : string.Empty) : ((this.ID != null) ? this.ID.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x00050138 File Offset: 0x0004E338
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

		// Token: 0x0600163E RID: 5694 RVA: 0x00050158 File Offset: 0x0004E358
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ID={0}", new object[] { (this.ID != null) ? this.ID.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ID={0}", new object[] { (this.ID != null) ? this.ID.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ID={0}", new object[] { (this.ID != null) ? this.ID.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00050237 File Offset: 0x0004E437
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00050240 File Offset: 0x0004E440
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00050249 File Offset: 0x0004E449
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00050237 File Offset: 0x0004E437
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00050254 File Offset: 0x0004E454
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

		// Token: 0x040007F8 RID: 2040
		private string creationMessage;

		// Token: 0x040007F9 RID: 2041
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007FA RID: 2042
		private TraceSourceIdentifier m_iD;
	}
}
