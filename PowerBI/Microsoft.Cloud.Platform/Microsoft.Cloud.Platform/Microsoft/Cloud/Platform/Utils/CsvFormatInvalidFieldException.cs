using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000319 RID: 793
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CsvFormatInvalidFieldException : CsvFormatException
	{
		// Token: 0x060016A5 RID: 5797 RVA: 0x00051FF4 File Offset: 0x000501F4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x00051FFC File Offset: 0x000501FC
		// (set) Token: 0x060016A7 RID: 5799 RVA: 0x00052004 File Offset: 0x00050204
		public string Name
		{
			get
			{
				return this.m_name;
			}
			protected set
			{
				this.m_name = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0005200D File Offset: 0x0005020D
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x00052015 File Offset: 0x00050215
		public string Value
		{
			get
			{
				return this.m_value;
			}
			protected set
			{
				this.m_value = value;
			}
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0005201E File Offset: 0x0005021E
		public CsvFormatInvalidFieldException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00052037 File Offset: 0x00050237
		public CsvFormatInvalidFieldException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0005204E File Offset: 0x0005024E
		public CsvFormatInvalidFieldException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0005206C File Offset: 0x0005026C
		protected CsvFormatInvalidFieldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CsvFormatInvalidFieldException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Name = (string)info.GetValue("CsvFormatInvalidFieldException_Name", typeof(string));
			}
			catch (SerializationException)
			{
				this.Name = null;
			}
			try
			{
				this.Value = (string)info.GetValue("CsvFormatInvalidFieldException_Value", typeof(string));
			}
			catch (SerializationException)
			{
				this.Value = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CsvFormatInvalidFieldException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0005217C File Offset: 0x0005037C
		public CsvFormatInvalidFieldException(string name, string value)
		{
			this.Name = name;
			this.Value = value;
			this.ConstructorInternal(false);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00052199 File Offset: 0x00050399
		public CsvFormatInvalidFieldException(string name, string value, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Name = name;
			this.Value = value;
			this.ConstructorInternal(false);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000521BE File Offset: 0x000503BE
		public CsvFormatInvalidFieldException(string name, string value, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Name = name;
			this.Value = value;
			this.ConstructorInternal(false);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000521EC File Offset: 0x000503EC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00052223 File Offset: 0x00050423
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0005222C File Offset: 0x0005042C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CsvFormatInvalidFieldException))
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

		// Token: 0x060016B4 RID: 5812 RVA: 0x000522FC File Offset: 0x000504FC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CsvFormatInvalidFieldException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CsvFormatInvalidFieldException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Name != null)
			{
				info.AddValue("CsvFormatInvalidFieldException_Name", this.Name, typeof(string));
			}
			if (this.Value != null)
			{
				info.AddValue("CsvFormatInvalidFieldException_Value", this.Value, typeof(string));
			}
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x000523A0 File Offset: 0x000505A0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to parse a comma-separated value (CSV) string due to a field with name '{0}' and value '{1}'. See inner exception for details", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Name != null) ? this.Name.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Name != null) ? this.Name.MarkIfInternal() : string.Empty) : ((this.Name != null) ? this.Name.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Value != null) ? this.Value.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Value != null) ? this.Value.MarkIfInternal() : string.Empty) : ((this.Value != null) ? this.Value.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x00052486 File Offset: 0x00050686
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

		// Token: 0x060016B7 RID: 5815 RVA: 0x000524A4 File Offset: 0x000506A4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Name={0}", new object[] { (this.Name != null) ? this.Name.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Name={0}", new object[] { (this.Name != null) ? this.Name.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Name={0}", new object[] { (this.Name != null) ? this.Name.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Value={0}", new object[] { (this.Value != null) ? this.Value.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Value={0}", new object[] { (this.Value != null) ? this.Value.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Value={0}", new object[] { (this.Value != null) ? this.Value.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00052646 File Offset: 0x00050846
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0005264F File Offset: 0x0005084F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00052658 File Offset: 0x00050858
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00052646 File Offset: 0x00050846
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00052664 File Offset: 0x00050864
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

		// Token: 0x04000808 RID: 2056
		private string creationMessage;

		// Token: 0x04000809 RID: 2057
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400080A RID: 2058
		private string m_name;

		// Token: 0x0400080B RID: 2059
		private string m_value;
	}
}
