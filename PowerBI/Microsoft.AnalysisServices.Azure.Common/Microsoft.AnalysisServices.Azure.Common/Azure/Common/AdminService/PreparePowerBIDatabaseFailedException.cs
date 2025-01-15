using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x0200012B RID: 299
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class PreparePowerBIDatabaseFailedException : AdminProvisioningServiceException
	{
		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003FA54 File Offset: 0x0003DC54
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0003FA5C File Offset: 0x0003DC5C
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x0003FA64 File Offset: 0x0003DC64
		public string DatabaseId
		{
			get
			{
				return this.m_databaseId;
			}
			protected set
			{
				this.m_databaseId = value;
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0003FA6D File Offset: 0x0003DC6D
		public PreparePowerBIDatabaseFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0003FA81 File Offset: 0x0003DC81
		public PreparePowerBIDatabaseFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0003FA98 File Offset: 0x0003DC98
		public PreparePowerBIDatabaseFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003FAB8 File Offset: 0x0003DCB8
		protected PreparePowerBIDatabaseFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("PreparePowerBIDatabaseFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseId = (string)info.GetValue("PreparePowerBIDatabaseFailedException_DatabaseId", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseId = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("PreparePowerBIDatabaseFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0003FB8C File Offset: 0x0003DD8C
		public PreparePowerBIDatabaseFailedException(string databaseId, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0003FBAA File Offset: 0x0003DDAA
		public PreparePowerBIDatabaseFailedException(string databaseId, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0003FBD0 File Offset: 0x0003DDD0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0003FC07 File Offset: 0x0003DE07
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0003FC10 File Offset: 0x0003DE10
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(PreparePowerBIDatabaseFailedException))
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

		// Token: 0x06000FDE RID: 4062 RVA: 0x0003FCE0 File Offset: 0x0003DEE0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("PreparePowerBIDatabaseFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("PreparePowerBIDatabaseFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseId != null)
			{
				info.AddValue("PreparePowerBIDatabaseFailedException_DatabaseId", this.DatabaseId, typeof(string));
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0003FD60 File Offset: 0x0003DF60
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "PreparePowerBIDatabase for database '{0}' failed", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : ((this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0003FDDB File Offset: 0x0003DFDB
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

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0003FDF8 File Offset: 0x0003DFF8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0003FEBC File Offset: 0x0003E0BC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003FEC5 File Offset: 0x0003E0C5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0003FECE File Offset: 0x0003E0CE
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0003FEBC File Offset: 0x0003E0BC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0003FED8 File Offset: 0x0003E0D8
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

		// Token: 0x040003A8 RID: 936
		private string creationMessage;

		// Token: 0x040003A9 RID: 937
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003AA RID: 938
		private string m_databaseId;
	}
}
