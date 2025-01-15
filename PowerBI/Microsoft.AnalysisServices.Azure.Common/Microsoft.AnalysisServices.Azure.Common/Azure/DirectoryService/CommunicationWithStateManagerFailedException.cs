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
	// Token: 0x02000022 RID: 34
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationWithStateManagerFailedException : DirectoryServiceException
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000B3B0 File Offset: 0x000095B0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000B3B8 File Offset: 0x000095B8
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000B3C0 File Offset: 0x000095C0
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

		// Token: 0x06000224 RID: 548 RVA: 0x0000B3C9 File Offset: 0x000095C9
		public CommunicationWithStateManagerFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000B3DD File Offset: 0x000095DD
		public CommunicationWithStateManagerFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000B3F4 File Offset: 0x000095F4
		public CommunicationWithStateManagerFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000B414 File Offset: 0x00009614
		protected CommunicationWithStateManagerFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationWithStateManagerFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseFullName = (string)info.GetValue("CommunicationWithStateManagerFailedException_DatabaseFullName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseFullName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationWithStateManagerFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000B4E8 File Offset: 0x000096E8
		public CommunicationWithStateManagerFailedException(string databaseFullName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseFullName = databaseFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000B506 File Offset: 0x00009706
		public CommunicationWithStateManagerFailedException(string databaseFullName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseFullName = databaseFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000B52C File Offset: 0x0000972C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000B563 File Offset: 0x00009763
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B56C File Offset: 0x0000976C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationWithStateManagerFailedException))
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

		// Token: 0x0600022D RID: 557 RVA: 0x0000B63C File Offset: 0x0000983C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationWithStateManagerFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationWithStateManagerFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseFullName != null)
			{
				info.AddValue("CommunicationWithStateManagerFailedException_DatabaseFullName", this.DatabaseFullName, typeof(string));
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B6BC File Offset: 0x000098BC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Communication failed with StateManager while resolving for '{0}'", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseFullName != null) ? this.DatabaseFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfInternal() : string.Empty) : ((this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000B737 File Offset: 0x00009937
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

		// Token: 0x06000230 RID: 560 RVA: 0x0000B754 File Offset: 0x00009954
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000B818 File Offset: 0x00009A18
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B821 File Offset: 0x00009A21
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000B82A File Offset: 0x00009A2A
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B818 File Offset: 0x00009A18
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000B834 File Offset: 0x00009A34
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

		// Token: 0x0400006A RID: 106
		private string creationMessage;

		// Token: 0x0400006B RID: 107
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400006C RID: 108
		private string m_databaseFullName;
	}
}
