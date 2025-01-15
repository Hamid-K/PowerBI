using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000318 RID: 792
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CsvFormatInvalidNumberOfFieldsException : CsvFormatException
	{
		// Token: 0x0600168D RID: 5773 RVA: 0x00051840 File Offset: 0x0004FA40
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x00051848 File Offset: 0x0004FA48
		// (set) Token: 0x0600168F RID: 5775 RVA: 0x00051850 File Offset: 0x0004FA50
		public int Actual
		{
			get
			{
				return this.m_actual;
			}
			protected set
			{
				this.m_actual = value;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x00051859 File Offset: 0x0004FA59
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x00051861 File Offset: 0x0004FA61
		public int Expected
		{
			get
			{
				return this.m_expected;
			}
			protected set
			{
				this.m_expected = value;
			}
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0005186A File Offset: 0x0004FA6A
		public CsvFormatInvalidNumberOfFieldsException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<int>();
			CompileCheck.IsValidValueField<int>();
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00051883 File Offset: 0x0004FA83
		public CsvFormatInvalidNumberOfFieldsException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0005189A File Offset: 0x0004FA9A
		public CsvFormatInvalidNumberOfFieldsException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000518B8 File Offset: 0x0004FAB8
		protected CsvFormatInvalidNumberOfFieldsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CsvFormatInvalidNumberOfFieldsException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.Actual = (int)info.GetValue("CsvFormatInvalidNumberOfFieldsException_Actual", typeof(int));
			this.Expected = (int)info.GetValue("CsvFormatInvalidNumberOfFieldsException_Expected", typeof(int));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CsvFormatInvalidNumberOfFieldsException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00051994 File Offset: 0x0004FB94
		public CsvFormatInvalidNumberOfFieldsException(int actual, int expected)
		{
			this.Actual = actual;
			this.Expected = expected;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000519B1 File Offset: 0x0004FBB1
		public CsvFormatInvalidNumberOfFieldsException(int actual, int expected, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Actual = actual;
			this.Expected = expected;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x000519D6 File Offset: 0x0004FBD6
		public CsvFormatInvalidNumberOfFieldsException(int actual, int expected, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Actual = actual;
			this.Expected = expected;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00051A04 File Offset: 0x0004FC04
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00051A3B File Offset: 0x0004FC3B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00051A44 File Offset: 0x0004FC44
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CsvFormatInvalidNumberOfFieldsException))
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

		// Token: 0x0600169C RID: 5788 RVA: 0x00051B14 File Offset: 0x0004FD14
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CsvFormatInvalidNumberOfFieldsException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CsvFormatInvalidNumberOfFieldsException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("CsvFormatInvalidNumberOfFieldsException_Actual", this.Actual, typeof(int));
			info.AddValue("CsvFormatInvalidNumberOfFieldsException_Expected", this.Expected, typeof(int));
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00051BB0 File Offset: 0x0004FDB0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to parse a comma-separated value (CSV) string due to an invalid number of fields; actual='{0}', expected='{1}'.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? this.Actual.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.Actual.ToString(CultureInfo.InvariantCulture) : this.Actual.ToString(CultureInfo.InvariantCulture)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.Expected.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.Expected.ToString(CultureInfo.InvariantCulture) : this.Expected.ToString(CultureInfo.InvariantCulture))
			});
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00051C60 File Offset: 0x0004FE60
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

		// Token: 0x0600169F RID: 5791 RVA: 0x00051C80 File Offset: 0x0004FE80
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Actual={0}", new object[] { this.Actual.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Actual={0}", new object[] { this.Actual.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "Actual={0}", new object[] { this.Actual.ToString(CultureInfo.InvariantCulture) })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Expected={0}", new object[] { this.Expected.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Expected={0}", new object[] { this.Expected.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "Expected={0}", new object[] { this.Expected.ToString(CultureInfo.InvariantCulture) })));
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00051DEC File Offset: 0x0004FFEC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00051DF5 File Offset: 0x0004FFF5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00051DFE File Offset: 0x0004FFFE
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00051DEC File Offset: 0x0004FFEC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00051E08 File Offset: 0x00050008
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

		// Token: 0x04000804 RID: 2052
		private string creationMessage;

		// Token: 0x04000805 RID: 2053
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000806 RID: 2054
		private int m_actual;

		// Token: 0x04000807 RID: 2055
		private int m_expected;
	}
}
