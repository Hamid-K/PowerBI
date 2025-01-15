using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000E4 RID: 228
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ProcessDatabaseFailedInvalidUserdataException : MonitoredException
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x00023A1C File Offset: 0x00021C1C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00023A24 File Offset: 0x00021C24
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00023A2C File Offset: 0x00021C2C
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

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00023A35 File Offset: 0x00021C35
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x00023A3D File Offset: 0x00021C3D
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

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00023A46 File Offset: 0x00021C46
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x00023A4E File Offset: 0x00021C4E
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

		// Token: 0x06000A0F RID: 2575 RVA: 0x00023A57 File Offset: 0x00021C57
		public ProcessDatabaseFailedInvalidUserdataException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00023A75 File Offset: 0x00021C75
		public ProcessDatabaseFailedInvalidUserdataException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00023A8C File Offset: 0x00021C8C
		public ProcessDatabaseFailedInvalidUserdataException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00023AAC File Offset: 0x00021CAC
		protected ProcessDatabaseFailedInvalidUserdataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ProcessDatabaseFailedInvalidUserdataException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("ProcessDatabaseFailedInvalidUserdataException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			try
			{
				this.IsOOL = (string)info.GetValue("ProcessDatabaseFailedInvalidUserdataException_IsOOL", typeof(string));
			}
			catch (SerializationException)
			{
				this.IsOOL = null;
			}
			try
			{
				this.IsTM = (string)info.GetValue("ProcessDatabaseFailedInvalidUserdataException_IsTM", typeof(string));
			}
			catch (SerializationException)
			{
				this.IsTM = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ProcessDatabaseFailedInvalidUserdataException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00023BF4 File Offset: 0x00021DF4
		public ProcessDatabaseFailedInvalidUserdataException(string databaseName, string isOOL, string isTM)
		{
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00023C18 File Offset: 0x00021E18
		public ProcessDatabaseFailedInvalidUserdataException(string databaseName, string isOOL, string isTM, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00023C46 File Offset: 0x00021E46
		public ProcessDatabaseFailedInvalidUserdataException(string databaseName, string isOOL, string isTM, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00023C7C File Offset: 0x00021E7C
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

		// Token: 0x06000A17 RID: 2583 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00023CBC File Offset: 0x00021EBC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ProcessDatabaseFailedInvalidUserdataException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ProcessDatabaseFailedInvalidUserdataException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseName != null)
			{
				info.AddValue("ProcessDatabaseFailedInvalidUserdataException_DatabaseName", this.DatabaseName, typeof(string));
			}
			if (this.IsOOL != null)
			{
				info.AddValue("ProcessDatabaseFailedInvalidUserdataException_IsOOL", this.IsOOL, typeof(string));
			}
			if (this.IsTM != null)
			{
				info.AddValue("ProcessDatabaseFailedInvalidUserdataException_IsTM", this.IsTM, typeof(string));
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00023D80 File Offset: 0x00021F80
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "ProcessDatabase for database {0} failed", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00023DFB File Offset: 0x00021FFB
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

		// Token: 0x06000A1B RID: 2587 RVA: 0x00023E18 File Offset: 0x00022018
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002402C File Offset: 0x0002222C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00024035 File Offset: 0x00022235
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002403E File Offset: 0x0002223E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002402C File Offset: 0x0002222C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00024048 File Offset: 0x00022248
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

		// Token: 0x040002C4 RID: 708
		private string creationMessage;

		// Token: 0x040002C5 RID: 709
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040002C6 RID: 710
		private string m_databaseName;

		// Token: 0x040002C7 RID: 711
		private string m_isOOL;

		// Token: 0x040002C8 RID: 712
		private string m_isTM;
	}
}
