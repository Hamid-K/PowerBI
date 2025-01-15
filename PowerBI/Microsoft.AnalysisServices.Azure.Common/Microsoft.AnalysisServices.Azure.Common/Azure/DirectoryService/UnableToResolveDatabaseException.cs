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
	// Token: 0x0200001F RID: 31
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class UnableToResolveDatabaseException : DirectoryServiceException
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x0000A230 File Offset: 0x00008430
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000A238 File Offset: 0x00008438
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000A240 File Offset: 0x00008440
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

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A249 File Offset: 0x00008449
		public UnableToResolveDatabaseException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A25D File Offset: 0x0000845D
		public UnableToResolveDatabaseException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A274 File Offset: 0x00008474
		public UnableToResolveDatabaseException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A294 File Offset: 0x00008494
		protected UnableToResolveDatabaseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("UnableToResolveDatabaseException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseFullName = (string)info.GetValue("UnableToResolveDatabaseException_DatabaseFullName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseFullName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("UnableToResolveDatabaseException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A368 File Offset: 0x00008568
		public UnableToResolveDatabaseException(string databaseFullName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseFullName = databaseFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000A386 File Offset: 0x00008586
		public UnableToResolveDatabaseException(string databaseFullName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseFullName = databaseFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A3AC File Offset: 0x000085AC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A3E3 File Offset: 0x000085E3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A3EB File Offset: 0x000085EB
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A3F0 File Offset: 0x000085F0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(UnableToResolveDatabaseException))
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

		// Token: 0x060001F1 RID: 497 RVA: 0x0000A4C0 File Offset: 0x000086C0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("UnableToResolveDatabaseException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("UnableToResolveDatabaseException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseFullName != null)
			{
				info.AddValue("UnableToResolveDatabaseException_DatabaseFullName", this.DatabaseFullName, typeof(string));
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000A540 File Offset: 0x00008740
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Cannot resolve the database '{0}' from StateManager", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseFullName != null) ? this.DatabaseFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfInternal() : string.Empty) : ((this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000A5BB File Offset: 0x000087BB
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

		// Token: 0x060001F4 RID: 500 RVA: 0x0000A5D8 File Offset: 0x000087D8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000A69C File Offset: 0x0000889C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000A6AE File Offset: 0x000088AE
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A69C File Offset: 0x0000889C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A6B8 File Offset: 0x000088B8
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

		// Token: 0x04000062 RID: 98
		private string creationMessage;

		// Token: 0x04000063 RID: 99
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000064 RID: 100
		private string m_databaseFullName;
	}
}
