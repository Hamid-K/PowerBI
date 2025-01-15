using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000BC RID: 188
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class StateManagerServiceOperationTimeOutException : StateManagerBaseException
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x00013C30 File Offset: 0x00011E30
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00013C38 File Offset: 0x00011E38
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00013C40 File Offset: 0x00011E40
		public string FlowName
		{
			get
			{
				return this.m_flowName;
			}
			protected set
			{
				this.m_flowName = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00013C49 File Offset: 0x00011E49
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00013C51 File Offset: 0x00011E51
		public string TimeOut
		{
			get
			{
				return this.m_timeOut;
			}
			protected set
			{
				this.m_timeOut = value;
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00013C5A File Offset: 0x00011E5A
		public StateManagerServiceOperationTimeOutException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00013C73 File Offset: 0x00011E73
		public StateManagerServiceOperationTimeOutException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00013C8A File Offset: 0x00011E8A
		public StateManagerServiceOperationTimeOutException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00013CA8 File Offset: 0x00011EA8
		protected StateManagerServiceOperationTimeOutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("StateManagerServiceOperationTimeOutException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.FlowName = (string)info.GetValue("StateManagerServiceOperationTimeOutException_FlowName", typeof(string));
			}
			catch (SerializationException)
			{
				this.FlowName = null;
			}
			try
			{
				this.TimeOut = (string)info.GetValue("StateManagerServiceOperationTimeOutException_TimeOut", typeof(string));
			}
			catch (SerializationException)
			{
				this.TimeOut = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("StateManagerServiceOperationTimeOutException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00013DB8 File Offset: 0x00011FB8
		public StateManagerServiceOperationTimeOutException(string flowName, string timeOut)
		{
			this.FlowName = flowName;
			this.TimeOut = timeOut;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00013DD5 File Offset: 0x00011FD5
		public StateManagerServiceOperationTimeOutException(string flowName, string timeOut, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.FlowName = flowName;
			this.TimeOut = timeOut;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00013DFA File Offset: 0x00011FFA
		public StateManagerServiceOperationTimeOutException(string flowName, string timeOut, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.FlowName = flowName;
			this.TimeOut = timeOut;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00013E28 File Offset: 0x00012028
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00013E5F File Offset: 0x0001205F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00013E68 File Offset: 0x00012068
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(StateManagerServiceOperationTimeOutException))
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

		// Token: 0x060006D2 RID: 1746 RVA: 0x00013F38 File Offset: 0x00012138
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("StateManagerServiceOperationTimeOutException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("StateManagerServiceOperationTimeOutException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.FlowName != null)
			{
				info.AddValue("StateManagerServiceOperationTimeOutException_FlowName", this.FlowName, typeof(string));
			}
			if (this.TimeOut != null)
			{
				info.AddValue("StateManagerServiceOperationTimeOutException_TimeOut", this.TimeOut, typeof(string));
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00013FDC File Offset: 0x000121DC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Flow {0} couldn't complete operation in {1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.FlowName != null) ? this.FlowName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.FlowName != null) ? this.FlowName.MarkIfInternal() : string.Empty) : ((this.FlowName != null) ? this.FlowName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.TimeOut != null) ? this.TimeOut.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.TimeOut != null) ? this.TimeOut.MarkIfInternal() : string.Empty) : ((this.TimeOut != null) ? this.TimeOut.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x000140B6 File Offset: 0x000122B6
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

		// Token: 0x060006D5 RID: 1749 RVA: 0x000140D4 File Offset: 0x000122D4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "FlowName={0}", (this.FlowName != null) ? this.FlowName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "FlowName={0}", (this.FlowName != null) ? this.FlowName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "FlowName={0}", (this.FlowName != null) ? this.FlowName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "TimeOut={0}", (this.TimeOut != null) ? this.TimeOut.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "TimeOut={0}", (this.TimeOut != null) ? this.TimeOut.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "TimeOut={0}", (this.TimeOut != null) ? this.TimeOut.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00014240 File Offset: 0x00012440
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00014249 File Offset: 0x00012449
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00014252 File Offset: 0x00012452
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00014240 File Offset: 0x00012440
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001425C File Offset: 0x0001245C
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

		// Token: 0x04000242 RID: 578
		private string creationMessage;

		// Token: 0x04000243 RID: 579
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000244 RID: 580
		private string m_flowName;

		// Token: 0x04000245 RID: 581
		private string m_timeOut;
	}
}
