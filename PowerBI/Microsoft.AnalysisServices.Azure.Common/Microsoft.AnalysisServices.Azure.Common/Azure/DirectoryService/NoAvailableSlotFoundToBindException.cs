using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.DirectoryService
{
	// Token: 0x02000024 RID: 36
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class NoAvailableSlotFoundToBindException : DirectoryServiceException
	{
		// Token: 0x0600024B RID: 587 RVA: 0x0000C090 File Offset: 0x0000A290
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000C098 File Offset: 0x0000A298
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
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

		// Token: 0x0600024E RID: 590 RVA: 0x0000C0A9 File Offset: 0x0000A2A9
		public NoAvailableSlotFoundToBindException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000C0BD File Offset: 0x0000A2BD
		public NoAvailableSlotFoundToBindException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		public NoAvailableSlotFoundToBindException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		protected NoAvailableSlotFoundToBindException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("NoAvailableSlotFoundToBindException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseFullName = (string)info.GetValue("NoAvailableSlotFoundToBindException_DatabaseFullName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseFullName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("NoAvailableSlotFoundToBindException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		public NoAvailableSlotFoundToBindException(string databaseFullName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseFullName = databaseFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000C1E6 File Offset: 0x0000A3E6
		public NoAvailableSlotFoundToBindException(string databaseFullName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseFullName = databaseFullName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C20C File Offset: 0x0000A40C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C248 File Offset: 0x0000A448
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("NoAvailableSlotFoundToBindException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("NoAvailableSlotFoundToBindException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseFullName != null)
			{
				info.AddValue("NoAvailableSlotFoundToBindException_DatabaseFullName", this.DatabaseFullName, typeof(string));
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Fail to find available slot to bind for '{0}'", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseFullName != null) ? this.DatabaseFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfInternal() : string.Empty) : ((this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000C343 File Offset: 0x0000A543
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

		// Token: 0x06000259 RID: 601 RVA: 0x0000C360 File Offset: 0x0000A560
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseFullName={0}", (this.DatabaseFullName != null) ? this.DatabaseFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000C424 File Offset: 0x0000A624
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000C42D File Offset: 0x0000A62D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000C436 File Offset: 0x0000A636
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000C424 File Offset: 0x0000A624
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000C440 File Offset: 0x0000A640
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

		// Token: 0x04000070 RID: 112
		private string creationMessage;

		// Token: 0x04000071 RID: 113
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000072 RID: 114
		private string m_databaseFullName;
	}
}
