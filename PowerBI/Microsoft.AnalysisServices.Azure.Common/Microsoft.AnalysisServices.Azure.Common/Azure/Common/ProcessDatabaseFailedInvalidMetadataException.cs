using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000E3 RID: 227
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ProcessDatabaseFailedInvalidMetadataException : MonitoredException
	{
		// Token: 0x060009EF RID: 2543 RVA: 0x0002320C File Offset: 0x0002140C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00023214 File Offset: 0x00021414
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0002321C File Offset: 0x0002141C
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
			protected set
			{
				this.m_databaseName = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00023225 File Offset: 0x00021425
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0002322D File Offset: 0x0002142D
		public string IsOOL
		{
			get
			{
				return this.m_isOOL;
			}
			protected set
			{
				this.m_isOOL = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00023236 File Offset: 0x00021436
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x0002323E File Offset: 0x0002143E
		public string IsTM
		{
			get
			{
				return this.m_isTM;
			}
			protected set
			{
				this.m_isTM = value;
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00023247 File Offset: 0x00021447
		public ProcessDatabaseFailedInvalidMetadataException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00023265 File Offset: 0x00021465
		public ProcessDatabaseFailedInvalidMetadataException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002327C File Offset: 0x0002147C
		public ProcessDatabaseFailedInvalidMetadataException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002329C File Offset: 0x0002149C
		protected ProcessDatabaseFailedInvalidMetadataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ProcessDatabaseFailedInvalidMetadataException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("ProcessDatabaseFailedInvalidMetadataException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			try
			{
				this.IsOOL = (string)info.GetValue("ProcessDatabaseFailedInvalidMetadataException_IsOOL", typeof(string));
			}
			catch (SerializationException)
			{
				this.IsOOL = null;
			}
			try
			{
				this.IsTM = (string)info.GetValue("ProcessDatabaseFailedInvalidMetadataException_IsTM", typeof(string));
			}
			catch (SerializationException)
			{
				this.IsTM = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ProcessDatabaseFailedInvalidMetadataException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000233E4 File Offset: 0x000215E4
		public ProcessDatabaseFailedInvalidMetadataException(string databaseName, string isOOL, string isTM)
		{
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00023408 File Offset: 0x00021608
		public ProcessDatabaseFailedInvalidMetadataException(string databaseName, string isOOL, string isTM, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00023436 File Offset: 0x00021636
		public ProcessDatabaseFailedInvalidMetadataException(string databaseName, string isOOL, string isTM, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002346C File Offset: 0x0002166C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000234A4 File Offset: 0x000216A4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ProcessDatabaseFailedInvalidMetadataException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ProcessDatabaseFailedInvalidMetadataException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseName != null)
			{
				info.AddValue("ProcessDatabaseFailedInvalidMetadataException_DatabaseName", this.DatabaseName, typeof(string));
			}
			if (this.IsOOL != null)
			{
				info.AddValue("ProcessDatabaseFailedInvalidMetadataException_IsOOL", this.IsOOL, typeof(string));
			}
			if (this.IsTM != null)
			{
				info.AddValue("ProcessDatabaseFailedInvalidMetadataException_IsTM", this.IsTM, typeof(string));
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00023568 File Offset: 0x00021768
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "ProcessDatabase for database {0} failed", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000235E3 File Offset: 0x000217E3
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

		// Token: 0x06000A02 RID: 2562 RVA: 0x00023600 File Offset: 0x00021800
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00023814 File Offset: 0x00021A14
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002381D File Offset: 0x00021A1D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00023826 File Offset: 0x00021A26
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00023814 File Offset: 0x00021A14
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00023830 File Offset: 0x00021A30
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

		// Token: 0x040002BF RID: 703
		private string creationMessage;

		// Token: 0x040002C0 RID: 704
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040002C1 RID: 705
		private string m_databaseName;

		// Token: 0x040002C2 RID: 706
		private string m_isOOL;

		// Token: 0x040002C3 RID: 707
		private string m_isTM;
	}
}
