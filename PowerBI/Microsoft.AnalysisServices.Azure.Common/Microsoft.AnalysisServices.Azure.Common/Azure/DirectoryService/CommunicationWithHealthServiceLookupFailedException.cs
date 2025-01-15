using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.DirectoryService
{
	// Token: 0x02000023 RID: 35
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationWithHealthServiceLookupFailedException : DirectoryServiceException
	{
		// Token: 0x06000236 RID: 566 RVA: 0x0000BA20 File Offset: 0x00009C20
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000BA28 File Offset: 0x00009C28
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000BA30 File Offset: 0x00009C30
		public string DatabaseFullName
		{
			get
			{
				return this.m_databaseFullName;
			}
			protected set
			{
				this.m_databaseFullName = value;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000BA39 File Offset: 0x00009C39
		public CommunicationWithHealthServiceLookupFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000BA4D File Offset: 0x00009C4D
		public CommunicationWithHealthServiceLookupFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000BA64 File Offset: 0x00009C64
		public CommunicationWithHealthServiceLookupFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000BA84 File Offset: 0x00009C84
		protected CommunicationWithHealthServiceLookupFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationWithHealthServiceLookupFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseFullName = (string)info.GetValue("CommunicationWithHealthServiceLookupFailedException_DatabaseFullName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseFullName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationWithHealthServiceLookupFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000BB58 File Offset: 0x00009D58
		public CommunicationWithHealthServiceLookupFailedException(string databaseFullName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseFullName = databaseFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000BB76 File Offset: 0x00009D76
		public CommunicationWithHealthServiceLookupFailedException(string databaseFullName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseFullName = databaseFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000BB9C File Offset: 0x00009D9C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000BBD3 File Offset: 0x00009DD3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000BBDC File Offset: 0x00009DDC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationWithHealthServiceLookupFailedException))
			{
				TraceSourceBase<ANCommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ANCommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000BCAC File Offset: 0x00009EAC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationWithHealthServiceLookupFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationWithHealthServiceLookupFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseFullName != null)
			{
				info.AddValue("CommunicationWithHealthServiceLookupFailedException_DatabaseFullName", this.DatabaseFullName, typeof(string));
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000BD2C File Offset: 0x00009F2C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Communication failed with HealthServiceLookup while getting optimal servers to bind for '{0}'", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseFullName != null) ? this.DatabaseFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfInternal() : string.Empty) : ((this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000BDA7 File Offset: 0x00009FA7
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

		// Token: 0x06000245 RID: 581 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000BE88 File Offset: 0x0000A088
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000BE91 File Offset: 0x0000A091
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000BE9A File Offset: 0x0000A09A
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000BE88 File Offset: 0x0000A088
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
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

		// Token: 0x0400006D RID: 109
		private string creationMessage;

		// Token: 0x0400006E RID: 110
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400006F RID: 111
		private string m_databaseFullName;
	}
}
