using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000E2 RID: 226
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ProcessDatabaseFailedDatatypeConversionException : MonitoredException
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x000229F4 File Offset: 0x00020BF4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000229FC File Offset: 0x00020BFC
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x00022A04 File Offset: 0x00020C04
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00022A0D File Offset: 0x00020C0D
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x00022A15 File Offset: 0x00020C15
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

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00022A1E File Offset: 0x00020C1E
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x00022A26 File Offset: 0x00020C26
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

		// Token: 0x060009DD RID: 2525 RVA: 0x00022A2F File Offset: 0x00020C2F
		public ProcessDatabaseFailedDatatypeConversionException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00022A4D File Offset: 0x00020C4D
		public ProcessDatabaseFailedDatatypeConversionException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00022A64 File Offset: 0x00020C64
		public ProcessDatabaseFailedDatatypeConversionException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00022A84 File Offset: 0x00020C84
		protected ProcessDatabaseFailedDatatypeConversionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ProcessDatabaseFailedDatatypeConversionException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("ProcessDatabaseFailedDatatypeConversionException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			try
			{
				this.IsOOL = (string)info.GetValue("ProcessDatabaseFailedDatatypeConversionException_IsOOL", typeof(string));
			}
			catch (SerializationException)
			{
				this.IsOOL = null;
			}
			try
			{
				this.IsTM = (string)info.GetValue("ProcessDatabaseFailedDatatypeConversionException_IsTM", typeof(string));
			}
			catch (SerializationException)
			{
				this.IsTM = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ProcessDatabaseFailedDatatypeConversionException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00022BCC File Offset: 0x00020DCC
		public ProcessDatabaseFailedDatatypeConversionException(string databaseName, string isOOL, string isTM)
		{
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00022BF0 File Offset: 0x00020DF0
		public ProcessDatabaseFailedDatatypeConversionException(string databaseName, string isOOL, string isTM, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00022C1E File Offset: 0x00020E1E
		public ProcessDatabaseFailedDatatypeConversionException(string databaseName, string isOOL, string isTM, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00022C54 File Offset: 0x00020E54
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			this.exceptionCulprit = ExceptionCulprit.User;
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00022C94 File Offset: 0x00020E94
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ProcessDatabaseFailedDatatypeConversionException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ProcessDatabaseFailedDatatypeConversionException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseName != null)
			{
				info.AddValue("ProcessDatabaseFailedDatatypeConversionException_DatabaseName", this.DatabaseName, typeof(string));
			}
			if (this.IsOOL != null)
			{
				info.AddValue("ProcessDatabaseFailedDatatypeConversionException_IsOOL", this.IsOOL, typeof(string));
			}
			if (this.IsTM != null)
			{
				info.AddValue("ProcessDatabaseFailedDatatypeConversionException_IsTM", this.IsTM, typeof(string));
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00022D58 File Offset: 0x00020F58
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "ProcessDatabase for database {0} failed", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00022DD3 File Offset: 0x00020FD3
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

		// Token: 0x060009E9 RID: 2537 RVA: 0x00022DF0 File Offset: 0x00020FF0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00023004 File Offset: 0x00021204
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002300D File Offset: 0x0002120D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00023016 File Offset: 0x00021216
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00023004 File Offset: 0x00021204
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00023020 File Offset: 0x00021220
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

		// Token: 0x040002BA RID: 698
		private string creationMessage;

		// Token: 0x040002BB RID: 699
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040002BC RID: 700
		private string m_databaseName;

		// Token: 0x040002BD RID: 701
		private string m_isOOL;

		// Token: 0x040002BE RID: 702
		private string m_isTM;
	}
}
